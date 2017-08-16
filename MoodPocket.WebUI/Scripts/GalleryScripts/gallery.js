function initGalleryScripts(deleteUrl) {
    $(document).on('click', '.delete-meme', function (e) {
        e.preventDefault();
        var memeUrl = $(this).next('#meme-url').val();
        var memeCard = $(this).closest("#meme-card");
        deleteMeme(deleteUrl, memeUrl, memeCard);
    });
    $(document).ajaxError(function (e, xhr) {
        if (xhr.status == 400) {
            var response = $.parseJSON(xhr.responseText);
            Materialize.toast(response, 3000);
        } else if (xhr.status == 403) {
            var response = $.parseJSON(xhr.responseText);
            var $toastContent = $('<a class="btn-flat toast-login" style="color: #fffbfb;margin-left: 0rem;">Join us <i class="material-icons right">person_add</i></a>');
            Materialize.toast($toastContent, 10000);
            $(".toast-login").on('click', function (e) {
                window.location = response.Url;
            });
        }
    });
}

function deleteMeme(postUrl, memeUrl, memeCard) {
    $.ajax({
        url: postUrl,
        type: 'POST',
        data: { url: memeUrl },
        success: function (response) {
            memeCard.hide();
            Materialize.toast(response, 3000);
        }
    });
}