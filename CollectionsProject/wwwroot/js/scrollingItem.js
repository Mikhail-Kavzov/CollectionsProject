let colId = $('.collection-item').first().attr('id');
let pageCount = Number($('#max-page-value').val());
let minPage = 1;
let currentPage = minPage;
const LabelWidth = 37;
let offset = 0;
const overflowPages = 6;
const isOverflow = pageCount > overflowPages;
let maxPage = overflowPages;
const desc = "_Desc";
let currentSortAttr = "Name";
let currSearchString = "";
function loadItems(url, element, page, sortOrder="",searchString="") {
    $.ajax({
        type: 'POST',
        url: url,
        data: {collectionId:colId, id:page, sortRule:sortOrder, searchString:searchString},
        dataType: 'html',
        success: function (data, textstatus) {
            if (data !== '') {
                $(element).children().remove();
                $(element).append(data);
            }

        }

    });
};
if (pageCount > 0) {
    loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1,currentSortAttr,currSearchString);
    $('#href-left').click(moveLeft);
    $('#href-right').click(moveRight);
    let radios = $('.p-radio');
    let pageWrapper = document.getElementById('pagination-wrapper');
    radios.each(function (index) {
        $(this).click(function () {
            let newPage = Number($(this).val());
            if (newPage !== currentPage) {
                currentPage = newPage;
                loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1,currentSortAttr,currSearchString);
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
        loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1,currentSortAttr,currSearchString);
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
        loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1,currentSortAttr,currSearchString);
    }
    $('#btn-filter').click(function () {
        let text = $('#filter-input').val();
        if (text === '')
            return;
        currentPage = 1;
        minPage = currentPage;
        maxPage = overflowPages;
        offset = 0;
        pageWrapper.style.left = offset + 'px';
        $('.p-radio').eq(currentPage - 1).prop('checked', true);
        currSearchString = text;
        loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1, currentSortAttr, currSearchString);

    });
    $('.attribSort').each(function (index) {
        $(this).click(function () {
            let sortAttr = $(this).attr('data-sort');
            if (sortAttr.includes(desc))
                currentSortAttr = sortAttr.replace(desc, "");
            else
                currentSortAttr = sortAttr.concat(desc);
            $(this).attr('data-sort', currentSortAttr);
            loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1, currentSortAttr, currSearchString);

        });
    });
    document.getElementById('filter-input').oninput=function () {
        if ($(this).val() === '') {
            currSearchString = "";
            currentSortAttr = "Name";
            loadItems("/Item/ItemsPagination/", '#tableBody', currentPage - 1, currentSortAttr, currSearchString);
        }
    };
}

