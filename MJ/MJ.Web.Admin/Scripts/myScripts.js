$(document).ready(function() {

    $('#registerBtn').unbind("click").click(function () {
        var usernameStr = $('#usernameTxt').val();
        var userpasswordStr = $('#passwordTxt').val();
        var userconfirmpasswordStr = $('#confPasswordTxt').val();

        $('#registerLoading').show();

        var postData = {
            __RequestVerificationToken: $('[name= "__RequestVerificationToken"]').val(),
            username: usernameStr,
            password: userpasswordStr,
            confirmpassword: userconfirmpasswordStr
        };

        $.ajax({
            type: "POST",
            url: '/Home/Register',
            dataType: "json",
            data: postData,
            success: function (result) {
                if (result == "Success") {
                    $('#closeBtn').click();
                    location.reload();
                }
                else if (result == "SamePassword") {
                    $('.modalTitle').val("Error Message");
                    $('#modalMessage').html("Passwords does not match. Please try again.");
                    $('#registerLoading').hide();
                    $('#closeBtn').html("Ok");
                    $('#registerBtn').hide();
                    $('#conData').hide();
                }
                else if (result == "Exists") {
                    $('.modalTitle').val("Error Message");
                    $('#modalMessage').html("Email already exists. Please input another email address.");
                    $('#registerLoading').hide();
                    $('#closeBtn').html("Ok");
                    $('#registerBtn').hide();
                    $('#conData').hide();
                }
                else {
                    $('.modalTitle').val("Specialty not deleted.");
                    $('#modalMessage').html("Failed in creating new account. Fields must not be empty.");
                    $('#registerLoading').hide();
                    $('#closeBtn').html("Ok");
                    $('#registerBtn').hide();
                    $('#conData').hide();
                }
            },
        });
    });

});

function refr() {
    location.reload();
}