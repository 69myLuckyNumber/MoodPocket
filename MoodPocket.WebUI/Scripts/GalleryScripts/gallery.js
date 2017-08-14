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