//var login_event = function (response) {
//console.log("login_event");
//console.log(response.status);
//console.log(response);
//    alert('reload');
//    document.location.reload();
// }
    

function onLinkedInLogin() {
    linkedinConnectedResponse();
}

function onLinkedInLoad() {
    IN.Event.on(IN, "auth", function () { onLinkedInLogin(); });
    IN.Event.on(IN, "logout", function () { onLinkedInLogout(); });
}
//function onLinkedInLogin() {
//    IN.API.Profile("me")
//      .fields(["id", "firstName", "lastName", "pictureUrl", "publicProfileUrl", "emailAddress"])
//      .result(function (result) {

//          console.log(result);
//      })
//      .error(function (err) {
//          alert(err);
//      });
//}
function liAuth() {
    // 
    IN.User.authorize(function () {
        callback();
    });
    //IN.UI.Authorize().place();
}

function liSai() {
    // 
    IN.User.setLoggedOut(function () {
        callback();
    });
    //IN.UI.Authorize().place();
}
