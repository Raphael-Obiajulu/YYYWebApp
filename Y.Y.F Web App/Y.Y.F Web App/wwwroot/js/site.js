﻿
function register() {
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

function adminRegister() {
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
            url: '/Account/AdminRegistration',
            dataType: 'json',
            data:
            {
                userDetails: userDetails,
            },
            success: function (result) {

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
    var data = {};
    data.eventTitle = $('#eventTitle').val();
    data.eventDate = $('#eventDate').val();
    data.eventTime = $('#eventTime').val();
    data.eventDetails = $('#eventDetails').val();
    var base64 = document.getElementById("eventImage").files;

    if (data.eventTitle != "" && data.eventDate != "" && data.eventTime != "" && data.eventDetails != "") {
        if (base64[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(base64[0]);
            reader.onload = function () {
                base64 = reader.result;
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
        } else {
            let userDetails = JSON.stringify(data);
            $.ajax({
                type: 'Post',
                url: '/Admin/Events',
                dataType: 'json',
                data:
                {
                    userDetails: userDetails,
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
        }

    }
    else {
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
    
    var data = {};
    data.PrayerRequestTitle = $('#prayerRequestTitle').val();
    data.PrayerRequestDetails = $('#prayerRequestDetails').val();
  
    if (data.PrayerRequestTitle != "" && data.PrayerRequestDetails != "") {
        
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
                $("#editprayerRequestModal").modal("show");
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
    var data = {};
    data.Id = $('#prayerRequest_Id').val();
    data.PrayerRequestTitle = $('#editprayerRequestTitle').val();
    data.PrayerRequestDetails = $('#editprayerRequestDetails').val();

    if (data.PrayerRequestTitle != "" && data.PrayerRequestDetails != "") {
        
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
    
    var data = {};
    data.DiscussionTitle = $('#discussionTitle').val();
    data.DiscussionDetails = $('#discussionDetails').val();

    if (data.DiscussionTitle != "" && data.DiscussionDetails != "") {
        
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
    
    Message = $('#mainComment').val();
    DiscussionForumId = $('#discussion_Id').val();
    
    if (Message != "" && DiscussionForumId > 0) {
        
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
function Approvecomment(id) {
    
    $.ajax({
        type: 'Post',
        url: '/Admin/ApproveComment',
        dataType: 'json',
        data:
        {
            commentId: id,
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Comments';
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
function Declinecomment(id) {
    
    $.ajax({
        type: 'Post',
        url: '/Admin/DeclineComment',
        dataType: 'json',
        data:
        {
            commentId: id,
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Comments';
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

function Deactivate(id) {
    $.ajax({
        type: 'Post',
        url: '/Admin/DeactivateUser',
        dataType: 'json',
        data:
        {
            userId: id,
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/AllUsers';
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

function EditProfileDetails() {
    var data = {};
    data.Id = $('#userId').val();
    data.FirstName = $('#edited_fName').val();
    data.LastName = $('#edited_lName').val();
    data.Phonenumber = $('#edited_Phone').val();
    data.Email = $('#edited_Email').val();
    var base64 = document.getElementById("profile_image").files;

    if (data.FirstName != "" && data.LastName != "" && data.Id != "" && data.Phonenumber != ""
        && data.Email != "") {
        if (base64[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(base64[0]);
            reader.onload = function () {
                base64 = reader.result;
                let profileDetails = JSON.stringify(data);
                $.ajax({
                    type: 'Post',
                    url: '/User/EditProfile',
                    dataType: 'json',
                    data:
                    {
                        profileDetails: profileDetails,
                        base64: base64
                    },
                    success: function (result) {
                        if (!result.isError) {
                            var url = '/User/Profile';
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
        } else {
            let profileDetails = JSON.stringify(data);
            $.ajax({
                type: 'Post',
                url: '/User/EditProfile',
                dataType: 'json',
                data:
                {
                    profileDetails: profileDetails,
                },
                success: function (result) {
                    if (!result.isError) {
                        var url = '/User/Profile';
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
        }

    }
    else {
        errorAlert("Please fill the form Correctly");
    }
}


function AddAnnouncement() {
    debugger
    var data = {};
    data.AnnouncementTitle = $('#announcementTitle').val();
    data.AnnouncementDetails = $('#announcementDetails').val();
    data.DurationFrom = $('#dateFrom').val();
    data.DurationTill = $('#dateTill').val();

    if (data.AnnouncementTitle != "" && data.AnnouncementDetails != "" && data.DurationFrom != "" && data.DurationTill != "") {

        let details = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/CreateAnnouncement',
            dataType: 'json',
            data:
            {
                details: details,

            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/AddAnnouncement';
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