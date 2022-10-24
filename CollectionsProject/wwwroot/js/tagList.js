let tag = $('#name-tag').val();
let page = -1;
let _inCallback = false;
let pageFlag = false;
function loadItems(url, element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.ajax({
            type: 'POST',
            url: url,
            data: { Page: page, tagName: tag },
            dataType: 'html',
            success: function (data, textstatus) {
                if (data !== '') {
                    $(element).append(data);
                } else {
                    pageFlag = true;
                }
                _inCallback = false;
            }

        });
    }
};
loadItems('/Tag/ItemPage/', '#tableBody');
$(window).scroll(function () {
    if ((Math.trunc($(window).scrollTop())) === $(document).height() - $(window).height()) {
        loadItems('/Tag/ItemPage/', '#tableBody');
    }
});