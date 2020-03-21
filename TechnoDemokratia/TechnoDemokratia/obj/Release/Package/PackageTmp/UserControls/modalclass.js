$(document).ready(function () {
   

// Get the modal
var modalcontatar = document.getElementById('ModalContatar');

// Get the button that opens the modal
var btn = document.getElementById("btnModalContatar");

// Get the <span> element that closes the modal
var span = document.getElementsByClassName("close")[0];

// When the user clicks on the button, open the modal 
btn.onclick = function () {
    modalcontatar.style.display = "block";
}

// When the user clicks on <span> (x), close the modal
span.onclick = function () {
    modalcontatar.style.display = "none";
}

// When the user clicks anywhere outside of the modal, close it
window.onclick = function (event) {
    if (event.target == modalcontatar) {
        modalcontatar.style.display = "none";
    }
}

});