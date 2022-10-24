let page = -1;
let _inCallback = false;
let pageFlag = false;
let updateItem = null;
function loadItems(url, element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.ajax({
            type: 'GET',
            url: url + page,
            success: function (data, textstatus) {
                if (data != '') {
                    $(element).append(data);
                    $('.btn-delete').each(function (index) {
                        $(this).click(function () {
                            let item = $(this).closest('.collection-item');
                            deleteItem('/Collection/Delete/', item.attr('id'));
                            item.remove();
                        });
                    });
                    $('.btn-update').each(function (index) {
                        $(this).click(function () {
                            let formUpdate = $('#update_col_cont');
                            formUpdate.show();
                            let item = $(this).closest('.collection-item');
                            updateItem = item;
                            $('#Id').val(item.attr('id'));

                            let oldNameVal = item.find('.text-name').first().text();
                            console.log(oldNameVal);
                            let oldDescVal = item.find('.collection-description').first().html();
                            console.log(oldDescVal);
                            let oldTypeVal = item.find('.collection-theme').first().text();
                            let oldPhotoSrc = item.find('.collection-photo').first().attr('src');
                            $('#Name').val(oldNameVal);
                            $('#Description').val(oldDescVal);
                            $('.jq-selectbox__select-text').first().text(oldTypeVal);
                            $('#image-photo').attr('src', oldPhotoSrc);
                        });
                    });
                }
                else {
                    pageFlag = true;
                }
                _inCallback = false;
            }
        });
    }
}
function deleteItem(url, id) {
    $.ajax({
        type: 'DELETE',
        url: url,
        data: { id: id }
    });
}
loadItems("/Collection/CollectionPage/", '#collection-list');
$(window).scroll(function () {
    if ((Math.trunc($(window).scrollTop())) === $(document).height() - $(window).height()) {
        loadItems("/Collection/CollectionPage/", '#collection-list');
    }
});

function hideForm() {
    $('#update_col_cont').hide();
}
$('#update-hide').click(hideForm);

