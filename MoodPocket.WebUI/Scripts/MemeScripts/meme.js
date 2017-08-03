function initMemeScripts(showMemeUrl) {
    $("#show-meme").on('click', function (e) {
        e.preventDefault();
        $('.materialboxed').materialbox();
        showMeme(showMemeUrl);
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            showMeme(showMemeUrl);
        }
    });
}

function showMeme(url) {
    $.ajax({
        url: url,
        type: 'POST',
        success: function (response) {
            $.each(response, function (index, item) {
                var memeCard = '<div class="col m4" id="meme-card">' +
                    '<div class="card hoverable">' +
                    '<div class="card-image">' +
                    '<img class="materialboxed" data-caption="'+ item.Title +'" src="' + item.Link + '" />' +
                    '</div>' +
                    '<div class="card-content">' +
                    '<span class="card-title activator grey-text text-darken-4">' + item.Title + '<i class="material-icons right">more_vert</i></span>' +
                    '<p><i class="material-icons">visibility</i>' + item.Views + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div >';
                $("#meme-container").append(memeCard);
                
            })
            $('.materialboxed').materialbox();
        }
    });
}