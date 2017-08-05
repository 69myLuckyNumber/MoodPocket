function initMemeScripts(showMemesUrl) {
    $("#show-meme").on('click', function (e) {
        $("#show-meme").hide();
        $(".preloader-wrapper").addClass('active');
        e.preventDefault();
        showMemes(showMemesUrl);
        
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            showMemes(showMemesUrl);
        }
    });

}

function showMemes(url) {
    $.ajax({
        url: url,
        type: 'POST',
        success: function (response) {
            $.each(response, function (index, item) {
                var memeCard = '<div class="col m4" id="meme-card">' +
                                    '<div class="card hoverable">' +
                                        '<div class="card-image">' +
                                        '<img class="materialboxed" src="' + item.Link + '" />' +
                                        '</div>' +
                                        '<div class="card-content meme-card-content" >' +
                                            '<span class="card-title activator grey-text text-darken-4">'+
                                                '<a class="btn-floating halfway-fab waves-effect waves-light blue-grey lighten-4 meme-save-btn tooltipped" data-position="left" data-delay="50" data-tooltip="Save">' +
                                                    '<i class="material-icons" style="color: #666;">sentiment_very_satisfied</i>' +
                                                '</a>' +
                                            '</span>' +
                                            '<p><i class="material-icons">visibility</i>' + item.Views + '</p>' +
                                        '</div>' +
                                    '</div>' +
                                '</div >';
                $("#meme-container").append(memeCard);
                
            })
            $(".preloader-wrapper").removeClass('active');
            materializeJsInit();
        }
    });
}

function materializeJsInit() {
    $('.materialboxed').materialbox();
    $('.tooltipped').tooltip({ delay: 50 });
}