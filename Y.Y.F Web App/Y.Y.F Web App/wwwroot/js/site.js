
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

function CreateEvents() {
    debugger;
    var data = {};
    data.eventTitle = $('#eventTitle').val();
    data.eventDate = $('#eventDate').val();
    data.eventTime = $('#eventTime').val();
    data.eventDetails = $('#eventDetails').val();
    //data.eventImage = $('#eventImage').val();
    var base64 = document.getElementById("eventImage").files;
    if (base64.Length < 0) {
        const reader = new FileReader();
        reader.readAsDataURL(base64[0]);
        reader.onload = function () { }
        base64 = reader.result;
    } else {
        base64 = "";
    }
    if (data.eventTitle != "" && data.eventDate != "" && data.eventTime != "" && data.eventDetails != "") {
        debugger
            let userDetails = JSON.stringify(data);
            $.ajax({
                type: 'Post',
                url: '/Admin/Events',
                dataType: 'json',
                data:
                {
                    userDetails: userDetails,
                    base64: base64
                },
                success: function (result) {
                    debugger;
                    if (!result.isError) {
                        var url = '/Admin/UpComingEvents';
                        successAlertWithRedirect(result.msg, url);
                    }
                    else {
                        errorAlert(result.msg);
                    }
                },
                error: function (ex) {
                    errorAlert("Please check and try again. Contact Admin if issue persists..");
                }
            });
        

    }
    else {
        $.ajax({
            type: 'Post',
            url: '/Admin/Events',
            dataType: 'json',
            data:
            {
                userDetails: userDetails,
               /* base64: base64*/
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/UpComingEvents';
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
        //$('#submit_btn').html(defaultBtnValue);
        //$('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }
}

function geteventdetails(id) {
    debugger
    $.ajax({
        type: 'Post',
        url: '/Admin/GetEventDetails',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                $("#eventDet").val(result.eventDetails);
            }
            else {
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please try again.");
        }
    });
}

function submitPrayerRequest() {
    debugger;
    var data = {};
    data.PrayerRequestTitle = $('#prayerRequestTitle').val();
    data.PrayerRequestDetails = $('#prayerRequestDetails').val();
  
    if (data.PrayerRequestTitle != "" && data.PrayerRequestDetails != "") {
        debugger
        let prayerDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/User/AddPrayerRequest',
            dataType: 'json',
            data:
            {
                prayerRequest: prayerDetails,
                
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/User/PrayerRequest';
                    successAlertWithRedirect(result.msg, url);
                }
                else {
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                errorAlert("Please check and try again. Contact Admin if issue persists..");
            }
        });
    }
    else {
        //$('#submit_btn').html(defaultBtnValue);
        //$('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }

}

function EditPrayerRequest(id) {
    debugger
    $.ajax({
        type: 'Get',
        url: '/User/GetPrayerRequest',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                $("#prayerRequest_Id").val(result.id);
                $("#editprayerRequestTitle").val(result.prayerRequestTitle);
                $("#editprayerRequestDetails").val(result.prayerRequestDetails);
                //$("#editprayerRequestModal").model("show");
            }
            else {
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please try again.");
        }
    });
}

function SaveEditedPrayerRequest() {
    debugger;
    var data = {};
    data.Id = $('#prayerRequest_Id').val();
    data.PrayerRequestTitle = $('#editprayerRequestTitle').val();
    data.PrayerRequestDetails = $('#editprayerRequestDetails').val();

    if (data.PrayerRequestTitle != "" && data.PrayerRequestDetails != "") {
        debugger
        let prayerDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/User/EditedPrayerRequest',
            dataType: 'json',
            data:
            {
                prayerDetails: prayerDetails,

            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/User/PrayerRequest';
                    successAlertWithRedirect(result.msg, url);
                }
                else {
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                errorAlert("Please check and try again. Contact Admin if issue persists..");
            }
        });
    }
    else {
        errorAlert("Please fill the form Correctly");
    }

}

function ApprovePrayerRequest(id) {
    debugger
    $.ajax({
        type: 'Post',
        url: '/Admin/ApproveRequest',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/PrayerRequest';
                successAlertWithRedirect(result.msg, url);
            }
            else {
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please try again.");
        }
    });
}

function DeclinePrayerRequest(id) {
    debugger
    $.ajax({
        type: 'Post',
        url: '/Admin/DeclineRequest',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/PrayerRequest';
                successAlertWithRedirect(result.msg, url);
            }
            else {
                errorAlert(result.msg);
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please try again.");
        }
    });
}

function CreateDiscussion() {
    debugger;
    var data = {};
    data.DiscussionTitle = $('#discussionTitle').val();
    data.DiscussionDetails = $('#discussionDetails').val();

    if (data.DiscussionTitle != "" && data.DiscussionDetails != "") {
        debugger
        let details = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/AddDiscussion',
            dataType: 'json',
            data:
            {
                details: details,

            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/CreateDiscussion';
                    successAlertWithRedirect(result.msg, url);
                }
                else {
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                errorAlert("Please check and try again. Contact Admin if issue persists..");
            }
        });
    }
    else {
        //$('#submit_btn').html(defaultBtnValue);
        //$('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }

}

function CreateComment() {
    debugger;
    Message = $('#mainComment').val();
    DiscussionForumId = $('#discussion_Id').val();
    
    if (Message != "" && DiscussionForumId > 0) {
        debugger
        $.ajax({
            type: 'Post',
            url: '/User/CreateComment',
            dataType: 'json',
            data:
            {
                message: Message,
                id: DiscussionForumId,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/User/Discussion';
                    successAlertWithRedirect(result.msg, url);
                }
                else {
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                errorAlert("Please check and try again. Contact Admin if issue persists..");
            }
        });
    }
    else {
        //$('#submit_btn').html(defaultBtnValue);
        //$('#submit_btn').attr("disabled", false);
        errorAlert("Please fill the form Correctly");
    }

}

function SaveLike(id) {

    if ( id > 0) {
        $.ajax({
            type: 'Post',
            url: '/User/AddLike',
            dataType: 'json',
            data:
            {
                id: id,
            },
            success: function (result) {
                if (!result.isError) {
                    $("#heartIcon").css("fill", "red");
                }
            },
        });
    }

}
