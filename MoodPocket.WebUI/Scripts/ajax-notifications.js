$(document).ajaxError(function (e, xhr) {
    var response = $.parseJSON(xhr.responseText);
    if (xhr.status == 400) {
        Materialize.toast(response, 3000);
    } else if (xhr.status == 403) {
        var $toastContent = $('<a class="btn-flat toast-login" style="color: #fffbfb;margin-left: 0rem;">Join us<i class="material-icons right">person_add</i></a>');
        Materialize.toast($toastContent, 10000);
        $(".toast-login").on('click', function (e) {
            window.location = response.Url;
        });
    }
});