function initMemeScripts(currentUserName, showMemesUrl, saveMemeUrl, downloadMemeUrl) {
    var memeUrl;
    $("#show-meme").on('click', function (e) {
        $("#show-meme").hide();
        $(".preloader-wrapper").addClass('active');
        e.preventDefault();
        showMemes(showMemesUrl);
    });
    $(window).scroll(function () {
        if ($(window).scrollTop() === $(document).height() - $(window).height()) {
            showMemes(showMemesUrl);
        }
    });
    $(document).on('click', '.save-meme',function (e) {
        e.preventDefault();
        memeUrl = $(this).next('#meme-url').val();
        saveMeme(currentUserName,saveMemeUrl, memeUrl);
    });

    $(document).on('click', '.download-meme', function (e) {
        e.preventDefault();
        var url = $(this).closest('div').prev().find("img").attr('src');
        downloadMeme(url);
    });
}
function downloadMeme(url) {
    var filename = url.substring(url.lastIndexOf("/") + 1).split("?")[0];
    var xhr = new XMLHttpRequest();
    xhr.responseType = 'blob';
    xhr.onload = function () {
        var a = document.createElement('a');
        a.href = window.URL.createObjectURL(xhr.response);
        a.download = filename;
        a.style.display = 'none';
        document.body.appendChild(a);
        a.click();
        delete a;
    };
    xhr.onreadystatechange = function () {
        if (xhr.readyState == 4 && xhr.status === 200) {
            Materialize.toast("Done", 2000);
        }
    };
    xhr.open('GET', url); // open the connection with address: url
    xhr.send(); // send empty request
}
function saveMeme(currentUserName, postUrl, imageUrl) {
    var meme = {
        HostedBy: currentUserName,
        Url: imageUrl
    };

    $.ajax({
        url: postUrl,
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ picture: meme }),
        success: function (response) {

            Materialize.toast(response, 3000);
        }
    });
}

function showMemes(url) {
    $.ajax({
        url: url,
        type: 'POST',
        success: function (response) {
            $.each(response, function (index, item) {
                var memeCard = '<div class="col s4" id="meme-card">' +
                    '<div class="card hoverable">' +
                    '<div class="card-image">' +
                    '<img class="materialboxed" src="' + item.Link + '" />' +
                    '</div>' +
                    '<div class="card-content meme-card-content" >' +
                    '<a class="btn-floating halfway-fab waves-effect waves-light blue-grey lighten-4 meme-download-btn tooltipped download-meme" data-position="left" data-delay="50" data-tooltip="Download" >'+
                    '<i class="material-icons" style="color: #666;">save</i>'+
                    '</a >'+
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