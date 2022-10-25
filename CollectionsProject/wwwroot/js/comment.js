let dateTimeSend = new Date();
let dateTimeLoad = dateTimeSend.toJSON();
let itemIden = $('#ItemIdentificator').val();
const timeInterval = 5000;
let inCallBack = false;
let page = -1;
let isEnd = false;
const liked = "liked";
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
        }
    });
}
function OnCommentSuccess() {
    getComments(dateTimeSend, '/Comment/CommentPage/');
}
function PrevPage(url, timeJSON) {
    if (isEnd)
        return;
    page++;
    $.ajax({
        url: url,
        method: 'post',
        dataType: 'html',
        data: { itemId: itemIden, Time: timeJSON, Page: page },
        success: function (data) {
            if (data !== '') {
                $('#comment-wrapper').prepend(data);
            } else {
                isEnd = true;
            }
        }
    });
}
PrevPage('/Comment/PreviousPage/', dateTimeLoad);
setInterval(OnCommentSuccess, timeInterval);
$('#ItemId').val(itemIden);
$('#comment-wrapper').scroll(function () {
    if (Math.trunc($('#comment-wrapper').scrollTop() === 0)) {
        PrevPage('/Comment/PreviousPage/', dateTimeLoad);
    }
});

function updateLike(e) {
    let oldval = false;
    let target = $(e.target);
    let counterLike = target.prev();
    let oldCounterVal = Number(counterLike.text());
    let commentId = target.closest('.message').attr('id');
    if (target.hasClass(liked)) {
        oldval = true;
        --oldCounterVal
    }
    else {
        ++oldCounterVal      
    }
    counterLike.text(oldCounterVal);
    target.toggleClass(liked);
    $.post('/Comment/UpdateLike/', { commentId: commentId, oldLikeState: oldval }, function (data) { });
}

