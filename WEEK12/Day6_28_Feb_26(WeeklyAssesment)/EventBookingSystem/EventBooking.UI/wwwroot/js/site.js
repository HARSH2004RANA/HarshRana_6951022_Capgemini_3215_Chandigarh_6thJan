// ✅ API base address candidates for local development
const HTTP_API = "http://localhost:5098/api";
const HTTPS_API = "https://localhost:7031/api";

const API_CANDIDATES = window.location.protocol === "https:"
    ? [HTTPS_API]
    : [HTTP_API, HTTPS_API];

async function callApi(path, options) {
    const urls = API_CANDIDATES;
    let lastError;

    for (const base of urls) {
        try {
            const response = await fetch(`${base}${path}`, options);
            if (response.ok || response.status < 500) {
                return response;
            }
            lastError = new Error(`API returned status ${response.status} for ${base}${path}`);
        } catch (error) {
            lastError = error;
        }
    }

    throw lastError;
}

console.log("Using API candidates:", API_CANDIDATES);

function escapeInlineString(value) {
    return String(value || "")
        .replace(/\\/g, "\\\\")
        .replace(/'/g, "\\'")
        .replace(/\"/g, "&quot;");
}

// 🔐 LOGIN
async function login() {
    const email = document.getElementById("email").value;
    const password = document.getElementById("password").value;

    try {
        const res = await callApi(`/auth/login`, {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({ email, password })
        });

        if (!res.ok) {
            alert("❌ Invalid login credentials");
            return;
        }

        const data = await res.json();

        if (data.token) {
            localStorage.setItem("token", data.token);
            window.location.href = "/Dashboard";
        } else {
            alert("❌ Token not received");
        }

    } catch (error) {
        console.error("ERROR:", error);
        alert("❌ API not reachable.\n\nFix:\n1. Run backend\n2. Check port\n3. Use HTTP not HTTPS");
    }
}

// 📥 LOAD EVENTS
async function loadEvents() {
    const token = localStorage.getItem("token");

    if (!token) {
        window.location.href = "/";
        return;
    }

    try {
        const res = await callApi(`/events`, {
            method: "GET",
            mode: "cors",
            headers: {
                "Authorization": `Bearer ${token}`
            }
        });

        if (!res.ok) {
            alert("❌ Failed to load events");
            return;
        }

        const events = await res.json();
        renderEventSummary(events);
        renderEventTable(events);
        renderEventCards(events);

    } catch (error) {
        console.error("ERROR:", error);
        alert("❌ Error loading events");
    }
}

function renderEventSummary(events) {
    const totalEvents = events.length;
    const totalSeats = events.reduce((sum, event) => sum + event.availableSeats, 0);
    const selectedEventName = document.getElementById("eventTitle")?.value || "None";

    document.getElementById("totalEvents")?.textContent = totalEvents;
    document.getElementById("totalSeats")?.textContent = totalSeats;
    document.getElementById("selectedEventName")?.textContent = selectedEventName || "None";
    document.getElementById("eventCountLabel")?.textContent = `${totalEvents} event${totalEvents === 1 ? "" : "s"} found`;
}

function renderEventTable(events) {
    const table = document.getElementById("eventTable");
    if (!table) return;

    if (!events.length) {
        table.innerHTML = `<tr><td colspan="6" class="text-center text-muted py-4">No events found.</td></tr>`;
        return;
    }

    const tableHTML = events.map(e => {
        const safeTitle = escapeInlineString(e.title);
        return `<tr>
            <td>${e.id}</td>
            <td>${e.title}</td>
            <td>${new Date(e.date).toLocaleDateString()}</td>
            <td>${e.location}</td>
            <td>${e.availableSeats}</td>
            <td><button class="btn btn-sm btn-primary" onclick="selectEvent(${e.id}, ${e.availableSeats}, '${safeTitle}')">Book</button></td>
        </tr>`;
    }).join("");

    table.innerHTML = tableHTML;
}

function renderEventCards(events) {
    const container = document.getElementById("eventCards");
    if (!container) return;

    if (!events.length) {
        container.innerHTML = `<div class="col-12 text-center text-muted py-5">No events available yet.</div>`;
        return;
    }

    const cards = events.map(e => {
        const safeTitle = escapeInlineString(e.title);
        const dateLabel = new Date(e.date).toLocaleDateString();
        const seatsBadge = e.availableSeats > 0 ? "success" : "danger";
        return `<div class="col-md-6">
            <div class="card event-card h-100 shadow-sm">
                <div class="card-body">
                    <div class="d-flex justify-content-between align-items-start mb-3">
                        <div>
                            <h5>${e.title}</h5>
                            <p class="text-muted mb-0">${e.location}</p>
                        </div>
                        <span class="badge bg-${seatsBadge}">${e.availableSeats} seats</span>
                    </div>
                    <p class="text-secondary">${e.description}</p>
                    <div class="d-flex justify-content-between align-items-center mt-4">
                        <small class="text-muted">${dateLabel}</small>
                        <button class="btn btn-sm btn-outline-primary" onclick="selectEvent(${e.id}, ${e.availableSeats}, '${safeTitle}')">Select</button>
                    </div>
                </div>
            </div>
        </div>`;
    }).join("");

    container.innerHTML = cards;
}

// 🎯 SELECT EVENT
function selectEvent(id, seats, title) {
    const eventIdField = document.getElementById("eventId");
    const eventTitleField = document.getElementById("eventTitle");

    if (eventIdField) {
        eventIdField.value = id;
        eventIdField.setAttribute("data-seats", seats);
    }
    if (eventTitleField) {
        eventTitleField.value = title || "";
    }
    if (document.getElementById("selectedEventName")) {
        document.getElementById("selectedEventName").textContent = title || `Event ${id}`;
    }
}

// 🎟️ BOOK EVENT
async function bookEvent() {
    const token = localStorage.getItem("token");
    const eventId = document.getElementById("eventId")?.value;
    const seats = parseInt(document.getElementById("seats")?.value || "0", 10);
    const availableSeats = parseInt(document.getElementById("eventId")?.getAttribute("data-seats") || "0", 10);

    if (!eventId || !seats) {
        alert("Fill all fields");
        return;
    }

    if (seats <= 0) {
        alert("Seats must be greater than 0");
        return;
    }

    if (seats > availableSeats) {
        alert("Seats exceed available limit");
        return;
    }

    try {
        const res = await callApi(`/bookings`, {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                eventId: parseInt(eventId, 10),
                seatsBooked: seats
            })
        });

        const result = document.getElementById("result");
        if (res.ok) {
            if (result) result.innerHTML = `<div class="alert alert-success">✅ Booking successful. Great choice!</div>`;
            loadEvents();
        } else {
            if (result) result.innerHTML = `<div class="alert alert-danger">❌ Booking failed. Try again.</div>`;
        }

    } catch (error) {
        console.error("ERROR:", error);
        alert("❌ Booking error. Please try again.");
    }
}

// 🔄 AUTO LOAD DASHBOARD
if (window.location.pathname.toLowerCase().includes("dashboard")) {
    loadEvents();
}