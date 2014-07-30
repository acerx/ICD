$(document).ready(function() {
    
    //$('#login-username').focus();

    $('#registerBtn').unbind("click").click(function () {
        var usernameStr = $('#usernameTxt').val();
        var userpasswordStr = $('#passwordTxt').val();

        //$('#loadingCol').show();
        $.ajax({
            type: "POST",
            url: '../User/Register',
            data: { username: usernameStr, password: userpasswordStr },
            dataType: "html",
            success: function (data) {
                if (data) {
                    //$('#searResultsContainer').html(data);
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
