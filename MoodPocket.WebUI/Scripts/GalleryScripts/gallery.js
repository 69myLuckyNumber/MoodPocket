function initGalleryScripts(deleteUrl) {
    $(document).on('click', '.delete-meme', function (e) {
        e.preventDefault();
        var memeUrl = $(this).next('#meme-url').val();
        var memeCard = $(this).closest("#meme-card");
        var pathName = window.location.pathname;
        var parts = pathName.split('/');
        var hostUserName = parts[parts.length - 1];

        deleteMeme(hostUserName, deleteUrl, memeUrl, memeCard);
    });
    
}

function deleteMeme(hostUserName, postUrl, memeUrl, memeCard) {
    var meme = {
        HostedBy: hostUserName,
        Url: memeUrl
    };
    $.ajax({
        url: postUrl,
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ picture: meme }),
        success: function (response) {
            memeCard.hide();
            Materialize.toast(response, 3000);
        }
    });
}