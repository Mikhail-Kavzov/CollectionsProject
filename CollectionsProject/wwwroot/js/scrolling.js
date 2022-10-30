let page = -1;
let _inCallback = false;
let pageFlag = false;
function defineTheme() {
    themeName = localStorage.getItem('lightSwitch');
    let colIt = $('.collection-item:gt(-6)');
    if (themeName === 'dark') {
        colIt.css('border-color', 'white');
        colIt.removeClass('bg-light').addClass('bg-dark');
    }
    else {
        colIt.css('border-color', 'black');
        colIt.removeClass('bg-dark').addClass('bg-light');
    }
}
function loadItems(url, element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.ajax({
            type: 'POST',
            url: url,
            data: { id: page },
            success: function (data, textstatus) {
                if (data != '') {
                    $(element).append(data);
                    defineTheme();
                }
                else {
                    pageFlag = true;
                }
                _inCallback = false;
            }
        });
    }
}
