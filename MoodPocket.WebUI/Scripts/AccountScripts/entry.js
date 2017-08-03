function initAccountScripts(registerUrl, loginUrl, homeUrl, entryUrl) {
    changeForm();
    $("#register-submit").on('click',function (e) {
        e.preventDefault();
        loginOrRegister($("#register-form"), registerUrl, entryUrl, "Register");
    });
    $("#login-submit").on('click', function (e) {
        e.preventDefault();
        loginOrRegister($("#login-form"),loginUrl, homeUrl, "Login");
    });
}

function loginOrRegister(form, url, redirectUrl, prefix) {
    var token = $('input[name="__RequestVerificationToken"]', form).val();
    var formData = form.serialize();
    var errorFields = $(".error"+prefix);
    $.extend(formData, { '__RequestVerificationToken': token });
    $.ajax({
        url: url,
        type: "POST",
        data: formData,
        success: function (data) {
            window.location.replace(redirectUrl)
        },
        error: function (xhr) {
            errorFields.empty();
            var jsonErrorBundle = $.parseJSON(xhr.responseText);
            for (var i = 0; i < jsonErrorBundle.length; i++) {
                var field = jsonErrorBundle[i]
                var error = field.errors[0];
                $("span[data-valmsg-for=" + field.key + "]", form).toggleClass("field-validation-valid field-validation-error")
                    .append('<span for="' + field.key + '" class="error'+prefix+'">' + error + '</span>');
                $("input[name=" + field.key + "]").addClass("input-validation-error");
            }
        }
    });
}
function changeForm() {
    $('#login-form-link').click(function (e) {
        $("#login-form").delay(100).fadeIn(100);
        $("#register-form").fadeOut(100);
        $('#register-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
    $('#register-form-link').click(function (e) {
        $("#register-form").delay(100).fadeIn(100);
        $("#login-form").fadeOut(100);
        $('#login-form-link').removeClass('active');
        $(this).addClass('active');
        e.preventDefault();
    });
}