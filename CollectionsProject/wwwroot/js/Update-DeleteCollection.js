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
    let idCollection = item.attr('id');
    updateItem = idCollection;
    $('#Id').val(idCollection);
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
$(document).on('click', function (e) {
    let target = $(e.target);
    if (target.is('.btn-update'))
        return;
    if (!target.closest('#update_col_cont').length)
        hideForm();
});
$('#update-hide').click(hideForm);
function defineCollectionTheme(item){
    let theme = localStorage.getItem('lightSwitch');
    if (theme === 'dark') {
        $(item).css('border-color', 'white');
        $(item).removeClass('bg-light').addClass('bg-dark');
    }
    else {
        $(item).css('border-color', 'black');
        $(item).removeClass('bg-dark').addClass('bg-light');
    }
}
function SuccessUpdate(data) {
    hideForm();
    let colItem = '#'+updateItem;
    $(colItem).replaceWith(data);
    defineCollectionTheme(colItem);
}