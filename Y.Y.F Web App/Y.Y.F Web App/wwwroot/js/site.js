
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
            debugger
            if (!result.isError) {
                $("#prayerRequest_Id").val(result.id);
                $("#editprayerRequestTitle").val(result.prayerRequestTitle);
                $("#editprayerRequestDetails").val(result.prayerRequestDetails);
                $("#editprayerRequestModal").modal("show");
            }
            else {
                debugger
                errorAlert(result.msg);
               $("#editprayerRequestModal").modal("hide");
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
                    successAlertWithRedirect(result.msg, result.url);
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

function viewIDImage(imageUrl) {
    var imageElement = document.getElementById('ImageId');
    imageElement.src = imageUrl;
}

function EventToDelete(id) {
   
    $('#event_Id').val(id);
    $('#eventDeleteModal').modal('show');
}

function DeleteEvent() {
   
    var id = $('#event_Id').val();
    $.ajax({
        type: 'Post',
        url: '/Admin/DeleteEvent',
        dataType: 'json',
        data:
        {
            id: id,
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
            errorAlert("An error occured, please try again.");
        }
    });
}

function EditDiscussion(id) {
    
    $.ajax({
        type: 'Get',
        url: '/Admin/GetDiscussionToEdit',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                $("#discussion_Id").val(result.id);
                $("#edit_discussionTitle").val(result.discussionTitle);
                $("#edit_discussionDetails").val(result.discussionDetails);
                $("#discussionEditModal").modal("show");
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

function SaveDiscussion() {
    var data = {};
    data.Id = $('#discussion_Id').val();
    data.DiscussionTitle = $('#edit_discussionTitle').val();
    data.DiscussionDetails = $('#edit_discussionDetails').val();

    if (data.DiscussionTitle != "" && data.DiscussionDetails != "") {

        let details = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/SaveEditedDiscussion',
            dataType: 'json',
            data:
            {
                details: details,

            },
            success: function (result) {
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
        errorAlert("Please fill the form Correctly");
    }

}

function DeleteDiscussion(id) {
   /* var id = $('#del_disId').val();*/
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DelDiscussion',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                var url = '/Admin/CreateDiscussion'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

//function DiscussionToDelete(id) {
   
//    $('#del_disId').val(id);
//    $('#discussionDeleteModal').modal('show');
//}

function EditAnnouncement(id) {
    $.ajax({
        type: 'Get',
        url: '/Admin/GetAnnounceToEdit',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                var durationFrom;
                if (result.durationFrom != "0001-01-01T00:00:00") {
                    var fromDate = result.durationFrom.split("T");
                    durationFrom = fromDate[0];
                } else {
                    durationFrom = null;
                }
                var durationTill;
                if (result.durationTill != "0001-01-01T00:00:00") {
                    var tillDate = result.durationTill.split("T");
                    durationTill = tillDate[0];
                } else {
                    durationTill = null;
                }

                $("#annouce_Id").val(result.id);
                $("#edit_announcementTitle").val(result.announcementTitle);
                $("#edit_announcementDetails").val(result.announcementDetails);

                $("#edit_dateFrom").val(durationFrom);
                $("#edit_dateTill").val(durationTill);
                $("#announcementEditModal").modal("show");
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

function SaveEditedAnnouncement() {
   
    var data = {};
    data.Id = $('#annouce_Id').val();
    data.AnnouncementTitle = $('#edit_announcementTitle').val();
    data.AnnouncementDetails = $('#edit_announcementDetails').val();
    data.DurationFrom = $('#edit_dateFrom').val();
    data.DurationTill = $('#edit_dateTill').val();

    if (data.AnnouncementTitle != "" && data.AnnouncementDetails != "" && data.DurationFrom != "" && data.DurationTill != "") {

        let announcedetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/SaveEditedAnnouncement',
            dataType: 'json',
            data:
            {
                announcedetails: announcedetails,

            },
            success: function (result) {
                debugger
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
        errorAlert("Please fill the form Correctly");
    }

}

function DeleteAnnounce(id) {
    
   /* var id = $('#annouce_Id').val();*/
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DelAnnounce',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/AddAnnouncement'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

//function DeleteAnnouncement(id) {
    
//    $('#annouce_Id').val(id);
//    $('#announcementToDelete').modal('show');
//}

function DeletePrayerRequest(id) {
   
   /* var id = $('#request_Id').val();*/
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/User/DeleteRequest',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/User/PrayerRequest'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

//function DeletePrayer(id) {
    
//    $('#request_Id').val(id);
//    $('#userPrayerDeleteModal').modal('show');
//}

function addBibleStudy() {
    var data = {};
    data.Title = $('#title').val();
    data.Details = $('#biblestudyDetails').val();

    if (data.Title != "" && data.Details != "") {

        let details = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/CreateBibleStudy',
            dataType: 'json',
            data:
            {
                details: details,

            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/AddBibleStudy';
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

function DeleteBibleStudy(id) {
    $('#bible_Id').val(id);
    $('#biblestudyToDelete').modal('show');
}

function EditBibleStudy(id) {
    debugger
    $.ajax({
        type: 'Get',
        url: '/Admin/GetBibleToEdit',
        dataType: 'json',
        data:
        {
            id: id,
        },
        success: function (result) {
            if (!result.isError) {
                debugger
                $("#bible_Id").val(result.id);
                $("#edit_biblestudyTitle").val(result.biblestudyTitle);
                $("#edit_biblestudyDetails").val(result.biblestudyDetails);
                $("#biblestudyEditModal").modal("show");
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

function AddMedia() {
    debugger

    var data = {};
    data.MediaTitle = $('#mediaTitle').val();
    data.Sermons = $('#mediasermon').val();
    data.Video = $('#mediaVideo').val();
    data.WorshipMusic = $('#mediamusic').val();
    var base64 = document.getElementById("mediaImage").files;

    if (data.MediaTitle != "") {
        debugger
        if (base64[0] != null) {
            const reader = new FileReader();
            reader.readAsDataURL(base64[0]);
            reader.onload = function () {
                base64 = reader.result;
                let mediaDetails = JSON.stringify(data);
                $.ajax({
                    type: 'Post',
                    url: '/Admin/CreateMedia',
                    dataType: 'json',
                    data:
                    {
                        mediaDetails: mediaDetails,
                        base64: base64
                    },
                    success: function (result) {
                        if (!result.isError) {
                            debugger;
                            var url = '/Admin/MediaGallery';
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
            errorAlert("Please fill the form Correctly");
        }

    }
    else {
        errorAlert("Please fill the form Correctly");
    }
}

function DeleteMedia() {
    var id = $('#media_Id').val(); 
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DelMedia',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/MediaGallery'
                successAlertWithRedirect(result.msg, url)
            }
            else {
                errorAlert(result.msg)
            }
        },
        error: function (ex) {
            errorAlert("An error occured, please check and try again. Please contact admin if issue persists..");
        }
    })
}

function MediaToDelete(id) {
    debugger
    $('#media_Id').val(id);
/*    $('#delete_media').modal('show');*/
}