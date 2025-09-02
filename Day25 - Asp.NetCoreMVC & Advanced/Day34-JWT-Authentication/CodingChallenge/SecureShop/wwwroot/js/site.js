// Example: alert on TempData message
document.addEventListener("DOMContentLoaded", function () {
    var message = document.getElementById("tempMessage");
    if (message) {
        alert(message.innerText);
    }
});
