loadItems("/Collection/CollectionPage/", '#collection-list');
$(window).scroll(function () {
    if ((Math.trunc($(window).scrollTop())) === $(document).height() - $(window).height()) {
        loadItems("/Collection/CollectionPage/", '#collection-list');
    }
});


