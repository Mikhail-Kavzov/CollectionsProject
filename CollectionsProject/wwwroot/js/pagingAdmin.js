
let pageCount = Number($('#max-page-value').val());
let minPage = 1;
let currentPage = minPage;
const LabelWidth = 37;
let offset = 0;
const overflowPages = 6;
const isOverflow = pageCount > overflowPages;
let maxPage = overflowPages;
function loadItems(url, element, currPage) {
    $.ajax({
        type: 'POST',
        url: url,
        data: { Page: currPage },
        success: function (data, textstatus) {
            if (data !== '') {
                $(element).children().remove();
                $(element).append(data);
            }
        }
    });
}
if (pageCount > 0) {

    loadItems("/Admin/UserPage/", '#tableBody', currentPage - 1);
    $('#href-left').click(moveLeft);
    $('#href-right').click(moveRight);
    let radios = $('.p-radio');
    let pageWrapper = document.getElementById('pagination-wrapper');
    radios.each(function (index) {
        $(this).click(function () {
            let newPage = Number($(this).val());
            if (newPage !== currentPage) {
                currentPage = newPage;
                loadItems("/Admin/UserPage/", '#tableBody', currentPage - 1);
            }
        });
    });
    function moveLeft() {
        if (currentPage === 1)
            return;
        if (currentPage === minPage && isOverflow) { // if it's left border
            --minPage;
            --maxPage;
            offset += LabelWidth;
            pageWrapper.style.left = offset + 'px';
        }
        currentPage--;
        $('.p-radio').eq(currentPage - 1).prop('checked', true);
        loadItems("/Admin/UserPage/", '#tableBody', currentPage - 1);
    }
    function moveRight() {
        if (currentPage === pageCount)
            return;
        if (currentPage === maxPage && isOverflow) { //if it's right border
            ++minPage;
            ++maxPage;
            offset -= LabelWidth;
            pageWrapper.style.left = offset + 'px';
        }
        currentPage++;
        $('.p-radio').eq(currentPage - 1).prop('checked', true);
        loadItems("/Admin/UserPage/", '#tableBody', currentPage - 1);
    }
}
