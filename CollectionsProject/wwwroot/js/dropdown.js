let userList = new Array();
let timeInterval = 20000;
const urlController = '/Tag/GetAllTags/';
function updateUserList(url) {
    $.ajax({
        url: url,
        method: 'post',
        dataType: 'json',
        success: function (data) {
            if (data !== '') {
                userList = data;
            }
        }
    });
}
updateUserList(urlController);
setInterval(updateUserList, timeInterval, urlController);
function appendDropdown(list, target) {
    if (list.length === 0)
        return;
    $.each(list, function (key, value) {
        let element = $("<li></li>").text(value);
        $('#dropdown').append(element);
    });
    $('#dropdown').show();
    $('#dropdown').children().each(function () { $(this).click(() => { $(target).val($(this).text()); $('#dropdown').children().remove(); }); });
}


function keyUpDropdown(e) {
    $('#dropdown').children().remove();
    let inputTarget = $(e.target);
    let textVal = $(inputTarget).val();
    if (textVal === '' || textVal === '#')
        return;
    $('#dropdown').detach().appendTo(inputTarget.parent().parent());
    let newList = new Array();
    $.each(userList, function (key, value) {
        if (value.startsWith(textVal))
            newList.push(value);
    });
    appendDropdown(newList, inputTarget);
}
$('.tag-container').each(function (i) {
    $(this).keyup(keyUpDropdown);
});
let prevTarget = $('.tag-input').attr('id');
$(document).mouseup(function (e) {
    let tId = $(e.target).attr('id');
    if (!e.target.classList.contains('tag-input') || prevTarget!==tId) {
        $('#dropdown').hide();
        prevTarget = tId;
    }
    else
        $('#dropdown').show();
});

let i = 1;
$('#btn-add-tag').click(function () {
    $.post('/Item/AddTagToItem/', { i: i }, function (data) {
        $('#table-body').append(data);
        $('.tag-container').last().keyup(keyUpDropdown);
    });
    i++;
});
$('#btn-delete-tag').click(function () {
    if (i === 1)
        return;
    i--;
    $('#dropdown').detach().appendTo('#table-body');
    $('.t-row').last().remove();
});