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