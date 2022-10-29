let tag = $('#name-tag').val();
let page = -1;
let _inCallback = false;
let pageFlag = false;
function loadTag(url, element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.post(url,{ TagName: tag, Page: page }, function (data) {
            if (data !== '') {
                {
                    $(element).append(data);
                    setup();
                }
            } else {
                pageFlag = true;
            }
            _inCallback = false;
        });
    }
};
loadTag('/Tag/ItemPage/', '#tableBody');
$(window).scroll(function () {
    if ((Math.trunc($(window).scrollTop())) === $(document).height() - $(window).height()) {
        loadTag('/Tag/ItemPage/', '#tableBody');
    }
});