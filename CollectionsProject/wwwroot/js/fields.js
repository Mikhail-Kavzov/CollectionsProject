let tbody = $('#table-body');
let i = 0;
$('#btn-add-field').click(function () {
    ++i;
    $.get('/Collection/AddNewField/', { i: i }, function (data) {
        if (data !== '') {
            tbody.append(data);
        }
    });
});
$('#btn-remove-field').click(function () {
    if (i !== -1) {
        --i;
        tbody.children().last().remove();
    }
});