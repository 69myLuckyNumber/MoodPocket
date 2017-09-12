function initGalleryScripts(deleteUrl) {
    $(document).on('click', '.delete-meme', function (e) {
        e.preventDefault();
        var memeUrl = $(this).next('#meme-url').val();
        var memeCard = $(this).closest("#meme-card");
 
        deleteMeme(deleteUrl, memeUrl, memeCard);
    });
    
}

function deleteMeme(postUrl, memeUrl, memeCard) {
    var meme = {
        Url: memeUrl
    };
    $.ajax({
        url: postUrl,
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ picture: meme }),
        success: function (response) {
            var deletebtn = memeCard.find("a.delete-meme");
            var savebtn = '<a class="btn-floating halfway-fab waves-effect waves-light blue-grey lighten-4 meme-save-btn tooltipped save-meme" data-position="left" data-delay="50" data-tooltip="Undo">' +
                '<i class="material-icons " style="color: #666;">plus_one</i></a>';

            deletebtn.trigger('mouseleave');
            deletebtn.replaceWith(savebtn);
            
            materializeJsInit();
            Materialize.toast(response, 3000);
        }
    });
}