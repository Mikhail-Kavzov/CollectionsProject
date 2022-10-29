
    let lightSwitch = document.getElementById('theme-switcher');
    function darkMode() {
        document.querySelectorAll('.bg-light').forEach((element) => {
            element.className = element.className.replace(/-light/g, '-dark');
        });

        document.body.classList.replace('light-body', 'bg-dark');
        $('.text-dark').addClass('text-light').removeClass('text-dark');
        if (document.body.classList.contains('text-dark')) {
            document.body.classList.replace('text-dark', 'text-light');
        } else {
            document.body.classList.add('text-light');
        }

        // Tables
        var tables = document.querySelectorAll('tbody');
        for (var i = 0; i < tables.length; i++) {
            // add table-dark class to each table
            tables[i].classList.add('table-dark');
        }

        document.querySelectorAll('.bg-white').forEach((element) => {
            element.classList.add('bg-dark', 'bg-gradient');
            element.classList.remove('bg-white');
        });
        document.querySelectorAll('.nav-link').forEach((element) => {
            element.classList.add('text-light');
        });
        $('.message-time').css('color', 'white');
        $('.collection-item').css('border-color', 'white');
        $('.comment-container').css('background-color', 'dimgray');
        let control = $('.form-control');
        control.css('background-color', 'lightgray');
        control.css('color', 'white');
        $('.form-container').css('background-color', 'gray');
        $('#update_col_cont').css('background-color', 'rgba(0,0,0,0.8)');
        $('#pagination-list').removeClass('bg-dark').css('background-color', 'dimgray');
        // set light switch input to true
        lightSwitch.checked = true;
        localStorage.setItem('lightSwitch', 'dark');
    }
    function lightMode() {
        document.querySelectorAll('.bg-dark').forEach((element) => {
            element.className = element.className.replace(/-dark/g, '-light');
        });
        document.body.classList.replace('bg-light', 'light-body');
        $('.text-light').addClass('text-dark').removeClass('text-light');
        if (document.body.classList.contains('text-light')) {
            document.body.classList.replace('text-light', 'text-dark');
        } else {
            document.body.classList.add('text-dark');
        }

        // Tables
        var tables = document.querySelectorAll('tbody');
        for (var i = 0; i < tables.length; i++) {
            tables[i].classList.remove('table-dark');

        }
        document.querySelectorAll('.bg-gradient').forEach((element) => {
            element.classList.remove('bg-dark', 'bg-gradient');
            element.classList.add('bg-white');
        });
        document.querySelectorAll('.nav-link').forEach((element) => {
            element.classList.remove('text-light');
        });
        $('.collection-item').css('border-color', 'black');
        $('.comment-container').css('background-color', '#ededed');
        $('.message-time').css('color', 'black');
        let control = $('.form-control');
        control.css('background-color', 'white');
        control.css('color', 'black');
        $('.form-container').css('background-color', 'white');
        $('#update_col_cont').css('background-color', 'rgba(255,255,255,0.9)');
        $('#pagination-list').css('background-color', 'white');
        lightSwitch.checked = false;
        localStorage.setItem('lightSwitch', 'light');
    }
    function onToggleMode() {
        if (lightSwitch.checked) {
            darkMode();
        } else {
            lightMode();
        }
    }
    function getSystemDefaultTheme() {
        const darkThemeMq = window.matchMedia('(prefers-color-scheme: dark)');
        if (darkThemeMq.matches) {
            return 'dark';
        }
        return 'light';
    }

    function setup() {
        var settings = localStorage.getItem('lightSwitch');
        if (settings == null) {
            settings = getSystemDefaultTheme();
        }

        if (settings == 'dark') {
            lightSwitch.checked = true;
        }

        lightSwitch.addEventListener('change', onToggleMode);
        onToggleMode();
        console.log(settings);
    }
    setup();