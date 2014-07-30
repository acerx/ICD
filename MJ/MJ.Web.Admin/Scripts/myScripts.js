$(document).ready(function() {
    
    //$('#login-username').focus();

    $('#registerBtn').unbind("click").click(function () {
        var usernameStr = $('#usernameTxt').val();
        var userpasswordStr = $('#passwordTxt').val();

        //$('#loadingCol').show();
        $.ajax({
            type: "POST",
            url: '../Home/Register',
            data: { username: usernameStr, password: userpasswordStr },
            dataType: "json",
            success: function (result) {
                if (result == "Success") {
                    $('#closeBtn').click();
                    location.reload();
                } else {
                    $('.modalTitle').val("Specialty not deleted.");
                    $('#modalMessage').html("Failed in creating new account. Please try again.");
                    $('#closeBtn').html("Ok");
                    $('#registerBtn').hide();
                    $('#conData').hide();
                }
                //$('#loadingCol').hide();
                //$('#defaultResultTable').hide();
            },
        });
    });

});

//function register() {

//    var usernameStr = $('#usernameTxt').val();
//    var userpasswordStr = $('#passwordTxt').val();

//    $.ajax({
//        url: '../User/Register',
//        type: 'POST',
//        data: {
//            username: usernameStr,
//            password: userpasswordStr
//        },
//        dataType: "json",
//        success: function (data) {
//            alert(data.success);
//        },
//        error: function () {
//            alert("error");
//        }
//    });
//}

function refr() {
    location.reload();
}