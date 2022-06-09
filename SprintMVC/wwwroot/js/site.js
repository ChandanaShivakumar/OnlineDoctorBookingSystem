// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.
//modified by me

function myFunction() {
    var x = document.getElementById("Password");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
}


$('#toggle').click(function () {
    //check if checkbox is checked
    if ($(this).is(':checked')) {

        $('#sendNewSms').removeAttr('disabled'); //enable input

    } else {
        $('#sendNewSms').attr('disabled', true); //disable input
    }
});