function SelectedCheckbox() {
    var selectedItems = new Array();
    $(".selected").each(function (index) { selectedItems.push(this.id); });
    return (selectedItems);
}
$('#BlockBtn').click(() => queryPutToolbar('Blocked', '/Admin/BlockUsers/',updateStatus));
$('#UnblockBtn').click(() => queryPutToolbar('Active', '/Admin/UnBlockUsers/',updateStatus));
$('#AddAdminBtn').click(() => queryPutToolbar('Admin', '/Admin/AddAdminRole/',updateRole));
$('#DeleteAdminBtn').click(() => queryPutToolbar('User', '/Admin/RemoveAdminRole/',updateRole));
$('#DeleteBtn').click(() => queryDeleteToolbar('/Admin/DeleteUsers/'));

function updateStatus(status){
    $('.selected').each(function (index) { $(this).children('td').last().prev().text(status) });
}
function updateRole(role){
    $('.selected').each(function (index) { $(this).children('td').last().text(role) });
}
function queryPutToolbar(status, Url,callBack) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'put',
        dataType: 'json',
        data: { id: idCheckBoxes },
        success: function (data) {
            if (data === '')
                callBack(status);
            else
                window.location.href = data.redirectToUrl;
        }
    });
}

function queryDeleteToolbar(Url) {
    let idCheckBoxes = SelectedCheckbox();
    $.ajax({
        url: Url,
        method: 'delete',
        dataType: 'json',
        data: { id: idCheckBoxes },
        success: function (data) {
            if (data === '')
                $(".selected").remove();
            else
                window.location.href = data.redirectToUrl;
        }
    });
}