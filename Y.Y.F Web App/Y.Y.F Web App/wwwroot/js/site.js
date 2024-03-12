
function register() {
    debugger;
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    
    var data = {};
    data.FirstName = $('#firstName').val();
    data.LastName = $('#lastName').val();
    data.UserName = $('#userName').val();
    data.Phonenumber = $('#phoneNumber').val();
    data.Email = $('#email').val();
    data.GenderId = $('#genderId').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#confirmPassword').val();

    if (data.FirstName != "" && data.LastName != "" && data.UserName != "" && data.Phonenumber != ""
        && data.Email != "" && data.Password != "" && data.ConfirmPassword != "") {
        let userDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Account/Registration',
            dataType: 'json',
            data:
            {
                userDetails: userDetails,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Account/Login';
                    successAlertWithRedirect(result.msg, url);
                    $('#submit_btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_btn').html(defaultBtnValue);
                    $('#submit_btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert("Please check and try again. Contact Admin if issue persists..");
            }
        });
    } else {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }
}

function login() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);
    var userName = $('#username').val();
    var password = $('#password').val();
    $.ajax({
        type: 'Post',
        url: '/Account/Login',
        dataType: 'json',
        data:
        {
            username: userName,
            password: password
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                var n = 1;
                localStorage.removeItem("on_load_counter");
                localStorage.setItem("on_load_counter", n);
                location.replace(result.dashboard);
                sessionStorage.removeItem('user');
                return;
            }
            else {
                $('#submit_btn').html(defaultBtnValue);
                $('#submit_btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            $('#submit_btn').html(defaultBtnValue);
            $('#submit_btn').attr("disabled", false);
            errorAlert("An error occured, please try again.");
        }
    });
}

//function CreateEvents() {
//    debugger;
//    var defaultBtnValue = $('#submit_btn').html();
//    $('#submit_btn').html("Please wait...");
//    $('#submit_btn').attr("disabled", true);

//    var data = {};
//    data.eventTitle = $('#eventTitle').val();
//    data.eventDate = $('#eventDate').val();
//    data.eventTime = $('#eventTime').val();
//    data.eventDetails = $('#eventDetails').val();
//    //data.eventImage = $('#eventImage').val();
//    var base64 = document.getElementById("eventImage").files;

//    if (data.eventTitle != "" && data.eventDate != "" && data.eventTime != "" && data.eventDetails != "") {
//        const reader = new FileReader();
//        reader.readAsDataURL(base64[0]);
//        reader.onload = function () {
//            base64 = reader.result;
//            let userDetails = JSON.stringify(data);
//            $.ajax({
//                type: 'Post',
//                url: '/Admin/CreateEvent',
//                dataType: 'json',
//                data:
//                {
//                    userDetails: userDetails,
//                    base64: base64
//                },
//                success: function (result) {
//                    debugger;
//                    if (!result.isError) {
//                        var url = '/Admin/UpComingEvents';
//                        successAlertWithRedirect(result.msg, url);
//                        $('#submit_btn').html(defaultBtnValue);
//                    }
//                    else {
//                        $('#submit_btn').html(defaultBtnValue);
//                        $('#submit_btn').attr("disabled", false);
//                        errorAlert(result.msg);
//                    }
//                },
//                error: function (ex) {
//                    $('#submit_btn').html(defaultBtnValue);
//                    $('#submit_btn').attr("disabled", false);
//                    errorAlert("Please check and try again. Contact Admin if issue persists..");
//                }
//            });
//        } else {
//            $('#submit_btn').html(defaultBtnValue);
//            $('#submit_btn').attr("disabled", false);
//            errorAlert("Please fill the form Correctly");
//        }


//        const reader = new FileReader();
//        reader.readAsDataURL(validId[0]);
//        reader.onload = function () {
//            validId = reader.result;
//            let userDetails = JSON.stringify(data);
//            $.ajax({
//                type: 'Post',
//                url: '/Account/StaffRegistration',
//                dataType: 'json',
//                data:
//                {
//                    userDetails: userDetails,
//                    staffPosition: StaffPosition,
//                    appLetter: appLetter,
//                    validId: validId
//                },
//                success: function (result) {
//                    debugger;
//                    if (!result.isError) {
//                        var url = '/Account/Login';
//                        successAlertWithRedirect(result.msg, url);
//                        $('#submit_btn').html(defaultBtnValue);
//                    }
//                    else {
//                        $('#submit_btn').html(defaultBtnValue);
//                        $('#submit_btn').attr("disabled", false);
//                        errorAlert(result.msg);
//                    }
//                },
//                error: function (ex) {
//                    $('#submit_btn').html(defaultBtnValue);
//                    $('#submit_btn').attr("disabled", false);
//                    errorAlert("Please check and try again. Contact Admin if issue persists..");
//                },
//            })

//        }
//    }
//}
