
function Register() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {};
    data.FirstName = $('#firstname').val();
    data.LastName = $('#lastname').val();
    data.Phonenumber = $('#phone').val();
    data.Email = $('#email').val();
    data.Password = $('#password').val();
    data.ConfirmPassword = $('#confirmPassword').val();

    if (data.FirstName == "" || data.FirstName == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your FirstName");
        return;
    }
    if (data.LastName == "" || data.LastName == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your LastName");
        return;
    }
    if (data.Phonenumber == "" || data.Phonenumber == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your Phonenumber");
        return;
    }
    if (data.Email == "" || data.Email == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your Email");
        return;
    }
    if (data.Password == "" || data.Password == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your Password");
        return;
    }
    if (data.ConfirmPassword == "" || data.ConfirmPassword == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your Password");
        return;
    }
    debugger
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
        },
    })

}

function login() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var email = $('#email').val();
    var password = $('#password').val();
    $.ajax({
        type: 'Post',
        url: '/Account/Login',
        dataType: 'json',
        data:
        {
            email: email,
            password: password
        },
        success: function (result) {
            debugger
            if (!result.isError) {
                var n = 1;
                localStorage.removeItem("on_load_counter");
                localStorage.setItem("on_load_counter", n);
                location.replace(result.dashboard);
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

function addEvent() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {};
    data.Title = $('#eventTitle').val();
    data.Summary = $('#eventSummary').val();
    data.EventLocation = $('#eventLocation').val();
    data.EventCapacity = $('#eventCapacity').val();
    data.EventDate = $('#eventDate').val();
    if (data.EventDate == "") {
        data.EventDate = "0001-01-01T00:00:00"
    };

    if (data.Title == "" || data.Title == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please add the event title");
        return;
    }
    if (data.Summary == "" || data.Summary == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please add the event summary");
        return;
    }
   
    if (data.EventLocation == "" || data.EventLocation == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please add the event location");
        return;
    }
    if (data.EventCapacity == 0 || data.EventCapacity == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please add the event capacity");
        return;
    }

        let eventDetails = JSON.stringify(data);
        $.ajax({
            type: 'Post',
            url: '/Admin/CreateEvent',
            dataType: 'json',
            data:
            {
                eventDetails: eventDetails,
            },
            success: function (result) {
                debugger;
                if (!result.isError) {
                    var url = '/Admin/Events';
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

function Eventedit(id) {
    debugger
    $.ajax({
        type: 'Get',
        dataType: 'Json',
        url: '/Admin/EditEvent',
        data: {
            id: id
        },
        success: function (result) {
            debugger
            if (!result.isError) {
                $('#eventId').val(result.id);
                $('#edit_eventTitle').val(result.title);
                $('#edit_eventSummary').val(result.summary);
                $('#edit_eventLocation').val(result.eventLocation);
                $('#edit_eventCapacity').val(result.eventCapacity);
                $('#edit_eventDate').val(result.eventDate);

                $('#edit_event').modal('show');
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

function SaveEditedEvent() {
    debugger
    var defaultBtnValue = $('#submit_Btn').html();
    $('#submit_Btn').html("Please wait...");
    $('#submit_Btn').attr("disabled", true);

    var data = {};
    data.Id = $("#eventId").val();
    data.Title = $('#edit_eventTitle').val();
    data.Summary = $('#edit_eventSummary').val();
    data.EventLocation = $('#edit_eventLocation').val();
    data.EventCapacity = $('#edit_eventCapacity').val();
    data.EventDate = $('#edit_eventDate').val();
    if (data.EventDate == "") {
        data.EventDate = "0001-01-01T00:00:00"
    };

    if (data.Title != "" && data.Summary != "" && data.EventLocation != "" && data.eventCapacity != "") {
        let eventDetails = JSON.stringify(data);
        $.ajax({
            type: 'POST',
            url: '/Admin/EditedEvent',
            dataType: 'json',
            data:
            {
                eventDetails: eventDetails,
            },
            success: function (result) {
                if (!result.isError) {
                    debugger
                    var url = '/Admin/Events'
                    successAlertWithRedirect(result.msg, url)
                    $('#submit_Btn').html(defaultBtnValue);
                }
                else {
                    $('#submit_Btn').html(defaultBtnValue);
                    $('#submit_Btn').attr("disabled", false);
                    errorAlert(result.msg);
                }
            },
            error: function (ex) {
                $('#submit_Btn').html(defaultBtnValue);
                $('#submit_Btn').attr("disabled", false);
                errorAlert(result.msg);
            }
        });
    } else {
        $('#submit_Btn').html(defaultBtnValue);
        $('#submit_Btn').attr("disabled", false);
        errorAlert("Invalid, Please fill the form correctly.");
    }
}

function DeleteEvent() {
    var id = $('#eventId').val();
    $.ajax({
        type: 'Post',
        dataType: 'Json',
        url: '/Admin/DeleteEvent',
        data: {
            id: id
        },
        success: function (result) {
            if (!result.isError) {
                var url = '/Admin/Events'
                successAlertWithRedirect(result.msg, url)
                $('#submit_Btn').html(defaultBtnValue);
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

function eventToDelete(id) {
    debugger
    $('#eventId').val(id);
    $('#delete_event').modal('show');
}

function GetBookingForm(id) {
    debugger
    $.ajax({
        type: 'GET',
        url: '/User/BookingForm',
        data: {
            eventid: id,
        },
        success: function (data) {
            $('#bookingContent').html(data);
            $('#bookEventModal').modal("show");
        },
    })
}

function BookNow() {
    debugger
    var defaultBtnValue = $('#submit_btn').html();
    $('#submit_btn').html("Please wait...");
    $('#submit_btn').attr("disabled", true);

    var data = {};
    data.Name = $('#name').val();
    data.Email = $('#email').val();
    data.BookerId = $('#booker_Id').val();
    data.EventId = $('#event_Id').val();
    data.Note = $('#note').val();

    if (data.Name == "" || data.Name == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your name");
        return;
    }
    if (data.Email == "" || data.Email == undefined) {
        $('#submit_btn').html(defaultBtnValue);
        $('#submit_btn').attr("disabled", false);
        errorAlert("Please fill in your email");
        return;
    }

    let bookingDetails = JSON.stringify(data);
    $.ajax({
        type: 'Post',
        url: '/User/BookEvent',
        dataType: 'json',
        data:
        {
            bookingDetails: bookingDetails,
        },
        success: function (result) {
            debugger;
            if (!result.isError) {
                var url = '/User/Events';
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
