function initMemeScripts(showMemesUrl, saveMemeUrl) {
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
    $(document).on('click', '.save-meme',function (e) {
        e.preventDefault();
        var memeUrl = $(this).next('#meme-url').val();
        saveMeme(saveMemeUrl, memeUrl);
    });

    $(document).ajaxError(function (e, xhr) {
        if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            var $toastContent = $('<a class="btn-flat toast-login" style="color: #fffbfb;margin-left: 0rem;">Join us <i class="material-icons right">person_add</i></a>');
            Materialize.toast($toastContent, 10000);
            $(".toast-login").on('click', function (e) {
                window.location = response.Url;
            });
        } else if (xhr.status == 400) {
            var response = $.parseJSON(xhr.responseText);
            Materialize.toast(response, 3000);
        }
    });
}

function saveMeme(postUrl, imageUrl) {
    var meme = {
        Url: imageUrl
    };

    $.ajax({
        url: postUrl,
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ picture: meme }),
        success: function (response) {
            Materialize.toast('Saved', 3000);
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

                    '<a class="btn-floating halfway-fab waves-effect waves-light blue-grey lighten-4 meme-save-btn tooltipped save-meme" data-position="left" data-delay="50" data-tooltip="Save" type="submit">' +
                    '<i class="material-icons " style="color: #666;">sentiment_very_satisfied</i>' +
                    '</a>' +
                    '<input type="hidden" value="' + item.Link + '" id="meme-url"/>' +

                    '<p><i class="material-icons">visibility</i>' + item.Views + '</p>' +
                    '</div>' +
                    '</div>' +
                    '</div >';
                $("#meme-container").append(memeCard);

            });
            $(".preloader-wrapper").removeClass('active');
            materializeJsInit();
        }
    });
}

function materializeJsInit() {
    $('.materialboxed').materialbox();
    $('.tooltipped').tooltip({ delay: 50 });
}