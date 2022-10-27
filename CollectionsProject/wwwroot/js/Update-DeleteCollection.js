let updateItem = null;
const turndownService = new TurndownService();
const mapEnum = new Map([
    ['Book', 0],
    ['Sign', 1],
    ['Silverware',2],
]
);
function deleteCollectionAjax(url, id) {
    $.ajax({
        type: 'DELETE',
        url: url,
        data: { id: id }
    });
}

function deleteCollection(e) {
    let item = $(e.target).closest('.collection-item');
    deleteCollectionAjax('/Collection/Delete/', item.attr('id'));
    item.remove();
}
function updateCollection(e) {
    let formUpdate = $('#update_col_cont');
    formUpdate.show();
    let item = $(e.target).closest('.collection-item');
    updateItem = item;
    $('#Id').val(item.attr('id'));
    let oldNameVal = item.find('.text-name').first().text();
    let descHtml = item.find('.collection-description').first().html();
    let murkdown = turndownService.turndown(descHtml);
    let oldTypeVal = item.find('.collection-theme').first().text();
    let oldPhotoSrc = item.find('.collection-photo').first().attr('src');
    $('#Name').val(oldNameVal);
    $('#Description').val(murkdown);
    $('#selector-type').val(mapEnum.get(oldTypeVal));
    $('#image-photo').attr('src', oldPhotoSrc);
}

function hideForm() {
    $('#update_col_cont').hide();
}
$('#update-hide').click(hideForm);
function SuccessUpdate(data) {
    hideForm();
    updateItem.replaceWith(data);
}