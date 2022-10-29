
function deleteItem(url, element, id) {
    $.ajax({
        type: 'DELETE',
        url: url,
        data: { id: id },
        dataType: 'text',
        success: function (data, textstatus) {
            if (textstatus === "success") {
                $(element).remove();
            }
            if (!$('#tableBody').children().length) {
                location.reload();
            }
        }
    });
};

function OnSearchSuccess(data) {
    RemoveSearchList();
    $('#search-list').append(data);
}
function RemoveSearchList() {
    $('#search-list').children().remove();
}
$('#search-input').on('input',(function () {
    if ($(this).val() === '')
        RemoveSearchList();
}));
$(document).on('click', (function (e) {
    let target=$(e.target);
    let isTargetSearch = target.is('#search-input') || target.is('#search-btn');
    if (!isTargetSearch)
        RemoveSearchList();
}));
$('#pop-up-icon').click(function () {
    $('#pop-up-menu').toggleClass('hider');
});
$(document).on('click', (function (e) {
    let target = $(e.target);
    if (target.closest('#pop-up-icon').length) // if click in icon block
        return;
    if (!target.closest('#pop-up-menu').length) // if click not in list
        $('#pop-up-menu').removeClass('hider');
}));
