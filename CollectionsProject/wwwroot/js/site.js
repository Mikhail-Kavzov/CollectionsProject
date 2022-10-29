
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
$(document).click(function (e) {
    let targetId = $(e.target).attr('id');
    let isTargetSearch = targetId === 'search-input' || targetId === 'search-btn';
    if (!isTargetSearch)
        RemoveSearchList();
})

