document.addEventListener('DOMContentLoaded', function () {
    const moonIcon = document.getElementById("moonIcon");
    const sunIcon = document.getElementById("sunIcon");
    const isDarkMode = localStorage.getItem('darkMode') === 'true';

    applyDarkMode(isDarkMode);
    moonIcon.addEventListener('click', toggleDarkMode);
    sunIcon.addEventListener('click', toggleDarkMode);

    function applyDarkMode(enabled) {
        if (enabled) {
            document.body.classList.add('dark-mode');
            moonIcon.style.display = 'none';
            sunIcon.style.display = 'inline';
        } else {
            document.body.classList.remove('dark-mode');
            moonIcon.style.display = 'inline';
            sunIcon.style.display = 'none';
        }
    }

    function toggleDarkMode() {
        const darkModeEnabled = document.body.classList.toggle('dark-mode');
        localStorage.setItem('darkMode', darkModeEnabled.toString());
        applyDarkMode(darkModeEnabled);
    }
});
