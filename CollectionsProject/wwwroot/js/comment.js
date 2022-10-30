let dateTimeSend = new Date();
let dateTimeLoad = dateTimeSend.toJSON();
let itemIden = $('#ItemIdentificator').val();
let page = -1;
let isEnd = false;
let callBackPrev = false;
let userName = $('#Uid').val();
const liked = "liked";
function PrevPage(url, timeJSON) {
    if (isEnd || callBackPrev)
        return;
    callBackPrev = true;
    page++;
    $.post(url, { itemId: itemIden, Time: timeJSON, Page: page }, function (data) {
        if (data !== '') {
            $('#comment-wrapper').prepend(data);
        } else {
            isEnd = true;
        }
        callBackPrev = false;
    });
}
PrevPage('/Comment/PreviousPage/', dateTimeLoad);
$('#comment-wrapper').scroll(function () {
    if ($('#comment-wrapper').scrollTop() < 10) {
        PrevPage('/Comment/PreviousPage/', dateTimeLoad);
    }
});
    let connection = new signalR.HubConnectionBuilder().withUrl("/CommentsHub").build();

    connection.start().then(function () {
        connection.invoke("SubscribeComment", itemIden);
    }).catch(function (err) {
        return console.error(err.toString());
    });

    connection.on("GetNewComment", function (comment) {
        $.post('/Comment/GetComment/', { comment: comment }, function (data) {
            if (data !== '') {
                $('#comment-wrapper').append(data);
            }
        })
    });
function updateLike(e) {
    if (userName === "")
        return;
    let oldval = false;
    let target = $(e.target);
    let counterLike = target.prev();
    let commentId = target.closest('.message').attr('id');
    if (target.hasClass(liked)) {
        oldval = true;
    }
    target.toggleClass(liked);
    $.post('/Comment/UpdateLike/', { commentId: commentId, oldLikeState: oldval }, function (data) {
        if (data !== '')
            counterLike.text(data);
    });
}
if (userName !== "") {
    $('#submit-btn').click(function () {
        let comment = {
            ItemId: itemIden,
            Text: $('#text-comment').val(),
            UserName: userName,
        };
        connection.invoke("CreateComment", comment).catch(function (err) {
            return console.error(err.toString());
        });
    });
}

