$('document').ready(function () {
    $("#login-form").submit(function (e) {
        e.preventDefault();
        $("#error-callout").addClass("display-none");
        $("#log-in-callout").removeClass("display-none");
        $.ajax({
            url: window.location.protocol + "//" + window.location.host + "/Home/Login",
            method: "POST",
            data: $(this).serialize(),
            success: function (data) {
                if (data.Status === "200") {
                    location.reload(true);
                }
                else {
                    $("#log-in-callout").addClass("display-none");
                    $("#error-callout").removeClass("display-none");
                    $("#error-message").html('<i class="fa fa-times"></i> ' + data.Message);
                }
            }
        });
    });
    $("#register-form").submit(function (e) {
        e.preventDefault();
        $("#error-callout").addClass("display-none");
        $("#register-callout").removeClass("display-none");
        $.ajax({
            url: window.location.protocol + "//" + window.location.host + "/Home/Register",
            method: "POST",
            data: $(this).serialize(),
            success: function (data) {
                if (data.Status === "200") {
                    location.reload(true);
                }
                else {
                    $("#register-callout").addClass("display-none");
                    $("#error-callout").removeClass("display-none");
                    $("#error-message").html('<i class="fa fa-times"></i> ' + data.Message);
                }
            }
        });
    });
});