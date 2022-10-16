let dateTimeSend = new Date(0);
let itemIden = $('#ItemIdentificator').val();
const timeInterval = 5000;
let inCallBack = false;
function getComments(time, url) {
    if (inCallBack)
        return;
    inCallBack = true;
    $.ajax({
        url: url,
        method: 'post',
        dataType: 'html',
        data: { itemId: itemIden, Time: time.toJSON() },
        success: function (data) {   
            dateTimeSend = new Date();        
            if (data !== '') {
                $('#comment-wrapper').append(data);
            }
            
            inCallBack = false;
        },
        error: function (error) {
            console.log(error);
        }
    });
}
OnCommentSuccess();
setInterval(OnCommentSuccess, timeInterval);
function OnCommentSuccess() {
    getComments(dateTimeSend, '/Comment/CommentPage/');
}
$('#ItemId').val(itemIden);
