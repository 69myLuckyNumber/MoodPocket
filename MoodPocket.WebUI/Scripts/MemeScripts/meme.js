﻿function initMemeScripts(showMemesUrl, saveMemeUrl, downloadMemeUrl) {
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
        var memeCard = $(this).closest("#meme-card");
        saveMeme(saveMemeUrl, memeUrl, memeCard);
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
function saveMeme(postUrl, imageUrl, memeCard) {
    var meme = {
        Url: imageUrl
    };

    $.ajax({
        url: postUrl,
        type: 'POST',
        contentType: "application/json",
        data: JSON.stringify({ meme: meme }),
        success: function (response) {
            var savebtn = memeCard.find('a.save-meme');
            var deletebtn = '<a class="btn-floating halfway-fab waves-effect waves-light blue-grey lighten-4 meme-save-btn tooltipped delete-meme" data-position="left" data-delay="50" data-tooltip="Undo">' +
                '<i class="material-icons" style="color: #666;">delete</i></a>';
            savebtn.trigger('mouseleave');
            savebtn.replaceWith(deletebtn);

            materializeJsInit();
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
                    '<i class="material-icons " style="color: #666;">plus_one</i>' +
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
    $('.tooltipped').tooltip('remove');
    $('.tooltipped').tooltip({ delay: 50 });
}