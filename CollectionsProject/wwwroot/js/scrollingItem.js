
let page = -1;
let _inCallback = false;
let pageFlag = false;
let firstFlag = true;
let colId = $('.collection-item').first().attr('id');
function loadItems(url, element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.ajax({
            type: 'POST',
            url: url + colId + '/' + page,
            dataType: 'html',
            success: function (data, textstatus) {
                if (data !== '') {

                    $(element).append(data);
                }
                else {
                    pageFlag = true;

                }
                _inCallback = false;
                firstFlag = false;
            }

        });
    }
};
loadItems("/Item/ItemsPagination/", '#tableBody');
$(window).scroll(function () {
    if ((Math.trunc($(window).scrollTop())) === $(document).height() - $(window).height()) {
        loadItems("/Item/ItemsPagination/", '#tableBody');
    }
});