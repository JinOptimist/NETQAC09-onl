(() => {
    const board = document.getElementById("maze-board");
    const messageLog = document.getElementById("message-log");
    const overlay = document.getElementById("maze-overlay");
    const overlayText = document.getElementById("maze-overlay-text");

    const stats = {
        health: document.getElementById("stat-health"),
        coins: document.getElementById("stat-coins"),
        potions: document.getElementById("stat-potions"),
        sand: document.getElementById("stat-sand"),
        flowers: document.getElementById("stat-flowers"),
        seed: document.getElementById("stat-seed"),
    };

    let busy = false;
    let knownMessageCount = 0;

    async function api(url, options = {}) {
        const response = await fetch(url, {
            headers: { "Content-Type": "application/json", Accept: "application/json" },
            ...options,
        });
        if (!response.ok) {
            throw new Error(`Request failed: ${response.status}`);
        }
        return response.json();
    }

    function iconHref(type) {
        return `#icon-${type}`;
    }

    function renderBoard(state) {
        board.style.gridTemplateColumns = `repeat(${state.width}, var(--cell))`;
        board.replaceChildren();

        const byPos = new Map(state.cells.map((c) => [`${c.x},${c.y}`, c]));

        for (let y = 0; y < state.height; y++) {
            for (let x = 0; x < state.width; x++) {
                const cell = byPos.get(`${x},${y}`) || { type: "Ground", isPlayer: false };
                const el = document.createElement("div");
                el.className = "maze-cell" + (cell.isPlayer ? " maze-cell--player" : "");
                el.setAttribute("role", "gridcell");
                el.title = cell.type;

                const svg = document.createElementNS("http://www.w3.org/2000/svg", "svg");
                svg.setAttribute("viewBox", "0 0 32 32");
                const use = document.createElementNS("http://www.w3.org/2000/svg", "use");
                use.setAttribute("href", iconHref(cell.type));
                svg.appendChild(use);
                el.appendChild(svg);
                board.appendChild(el);
            }
        }
    }

    function renderStats(state) {
        const p = state.player;
        stats.health.textContent = `${p.currentHealth} / ${p.maxHealth}`;
        stats.coins.textContent = String(p.coin);
        stats.potions.textContent = String(p.healthPotion);
        stats.sand.textContent = String(p.sand);
        stats.flowers.textContent = String(p.flowers);
        stats.seed.textContent = String(state.seed);
    }

    function renderMessages(state) {
        const messages = state.messages || [];
        if (messages.length < knownMessageCount) {
            knownMessageCount = 0;
            messageLog.replaceChildren();
        }

        for (let i = knownMessageCount; i < messages.length; i++) {
            const li = document.createElement("li");
            li.textContent = messages[i];
            messageLog.appendChild(li);
        }
        knownMessageCount = messages.length;
        messageLog.scrollTop = messageLog.scrollHeight;
    }

    function renderOverlay(state) {
        if (!state.isAlive) {
            overlay.hidden = false;
            overlayText.textContent = "Вы погибли. Лабиринт затих.";
            return;
        }
        if (state.isFailed) {
            overlay.hidden = false;
            overlayText.textContent = state.errorMessage || "Путь оборвался.";
            return;
        }
        overlay.hidden = true;
        overlayText.textContent = "";
    }

    function render(state) {
        renderBoard(state);
        renderStats(state);
        renderMessages(state);
        renderOverlay(state);
    }

    async function loadState() {
        const state = await api("/Maze/State");
        knownMessageCount = 0;
        messageLog.replaceChildren();
        render(state);
    }

    async function newGame() {
        if (busy) return;
        busy = true;
        try {
            const state = await api("/Maze/NewGame", { method: "POST" });
            knownMessageCount = 0;
            messageLog.replaceChildren();
            render(state);
        } finally {
            busy = false;
        }
    }

    async function move(action) {
        if (busy) return;
        busy = true;
        try {
            const state = await api("/Maze/Move", {
                method: "POST",
                body: JSON.stringify({ action }),
            });
            render(state);
        } finally {
            busy = false;
        }
    }

    document.querySelectorAll("[data-move]").forEach((btn) => {
        btn.addEventListener("click", () => move(btn.getAttribute("data-move")));
    });

    document.getElementById("btn-new-game").addEventListener("click", newGame);
    document.getElementById("btn-overlay-restart").addEventListener("click", newGame);

    document.addEventListener("keydown", (e) => {
        if (e.target && ["INPUT", "TEXTAREA"].includes(e.target.tagName)) return;

        const map = {
            ArrowUp: "up",
            KeyW: "up",
            ArrowDown: "down",
            KeyS: "down",
            ArrowLeft: "left",
            KeyA: "left",
            ArrowRight: "right",
            KeyD: "right",
        };

        const action = map[e.code];
        if (!action) return;
        e.preventDefault();
        move(action);
    });

    loadState().catch((err) => {
        overlay.hidden = false;
        overlayText.textContent = "Не удалось открыть лабиринт: " + err.message;
    });
})();
