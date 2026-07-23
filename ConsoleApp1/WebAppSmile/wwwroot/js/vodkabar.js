document.addEventListener("DOMContentLoaded", () => {
    const title = document.querySelector(".torches-header");
    const banner = document.getElementById("bar-status-banner");
    const statusText = banner?.querySelector(".status-text");

    if (title) {
        title.style.cursor = "pointer";
        title.title = "Кликни, чтобы мир качнулся!";

        const extraPhrases = [
            "Эффект бабочки отменяется, наступает эффект белочки 🐿️",
            "Мир в лабиринте слегка качнулся 🍻 ...",
            "Кэш исчезает 💸, интоксикация нарастает 🫠",
            "Портал в измерение похмелья успешно активирован 🌀"
        ];

        title.addEventListener("click", () => {
            // Эффект покачивания страницы
            document.body.style.transition = "transform 1.5s ease-in-out";
            document.body.style.transform = "rotate(1deg) translateY(5px)";

            setTimeout(() => {
                document.body.style.transform = "rotate(-1deg) translateY(-5px)";
            }, 750);

            setTimeout(() => {
                document.body.style.transform = "none";
            }, 1500);

            // Выбираем случайную фразу
            const randomPhrase = extraPhrases[Math.floor(Math.random() * extraPhrases.length)];

            // Показываем плашку с фразой прямо над карточкой
            if (banner && statusText) {
                banner.classList.add("hidden");

                setTimeout(() => {
                    statusText.textContent = randomPhrase;
                    banner.classList.remove("hidden");
                }, 200);
            }
        });
    }
});