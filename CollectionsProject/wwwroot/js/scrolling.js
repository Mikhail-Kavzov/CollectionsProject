
let page = -1;
let _inCallback = false;
let pageFlag=false;
let firstFlag = true;
function loadItems(url,element) {
    if (!pageFlag && !_inCallback) {
        _inCallback = true;
        page++;
        $.ajax({
            type: 'GET',
            url: url + page,
            success: function (data, textstatus) {
                if (data != '') {
                    $(element).append(data);
                }
                else {
                    pageFlag=true;
                }
                _inCallback = false;
                firstFlag = false;
            }
        });
    }
}
