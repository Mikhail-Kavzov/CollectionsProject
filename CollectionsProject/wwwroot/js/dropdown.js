let userList = new Array();
let timeInterval=20000;
const urlController='/Tag/GetAllTags/';
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
setInterval(updateUserList,timeInterval,urlController);
function appendDropdown(list,target) {
    if (list.length === 0)
        return;
    $.each(list, function (key, value) {
        let element = $("<li></li>").text(value);
        $('#dropdown').append(element);
    });
    $('#dropdown').children().each(function () { $(this).click(() => { $(target).val($(this).text()); $('#dropdown').children().remove(); }); });
}
$('.tag-container').last().keyup(keyUpDropdown);

function keyUpDropdown(e) {
    $('#dropdown').children().remove();
    let inputTarget=$(e.target);
    let textVal = $(inputTarget).val();
    if (textVal === ''|| textVal==='#')
        return;
    $('#dropdown').detach().appendTo(inputTarget.parent());
    let newList = new Array();
    $.each(userList, function (key, value) {
        if (value.startsWith(textVal))
            newList.push(value);
    });
    appendDropdown(newList,inputTarget);
}
$(document).mouseup(function (e) {
    if (!e.target.classList.contains('tag-input'))
        $('#dropdown').hide();
    else
        $('#dropdown').show();
});

let i = 1;
let btnTag = document.getElementById('btn-add-tag');
let tableBody = document.getElementById('table-body');
btnTag.onclick = function () {
    let tr = document.createElement('tr');
    let td = document.createElement('td');
    td.classList.add('tag-container');
    td.appendChild(getHtmlFromStr(i));
    tr.appendChild(td);
    tableBody.appendChild(tr);
    $('.tag-container').last().keyup(keyUpDropdown);
    i++;
};
function getHtmlFromStr(num) {
    let htmlElemStr = "<input class=\"tag-input\" style=\"width: 100% \" type = \"text\" autocomplete = \"off\" data - val=\"true\" data - val - regex=\"Use only eng letter or numbers, length is up to 10\" data - val - regex - pattern=\" ^#[a - zA - Z0 - 9]{ 1, 10 }$\" data - val - required=\"Tag is required\" id = \"Tags_" + num + "__TagName\" name = \"Tags[" + num + "].TagName\" value = \"#\" >";
    return new DOMParser().parseFromString(htmlElemStr, 'text/html').getElementsByTagName('input')[0];
}
$('#btn-delete-tag').click(function () {
    if (i === 1)
        return;
    i--;
    $('#dropdown').detach().appendTo('#table-body');
    $('.t-row').last().remove();
});

$('.checkbox-field').each(function (index) {
    $(this).click(function () {
        if ($(this).is(':checked')) {
            $(this).next().text('Yes');
            $(this).val('Yes');
        } else {
            $(this).next().text('No');
            $(this).val('No');
        }
    });
});