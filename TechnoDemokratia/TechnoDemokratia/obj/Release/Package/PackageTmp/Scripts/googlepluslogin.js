
//var GoogleAuth;

//function onLoad() {
//    gapi.load('auth2', function () {
//        gapi.auth2.init({
//            client_id: '651792012391-0soak3elmitjaab822m0gkei0o8k9lt5.apps.googleusercontent.com',
//            fetch_basic_profile: false,
//            scope: 'profile'
//        });
//        GoogleAuth = gapi.auth2.getAuthInstance();
//        //GoogleAuth.isSignedIn.listen(signinChanged);
//        //console.log(GoogleAuth.currentUser.get());

//    });

//}

//function GoogleSignIn() {
//    GoogleAuth = gapi.auth2.getAuthInstance();
//    GoogleAuth.signIn()
//}



//function signinChanged() {
//    console.log('logado');

//    onSuccess();
    //googleplusConnectedResponse(GoogleAuth.currentUser.get());

//}

//function signOut() {
//    GoogleAuth = gapi.auth2.getAuthInstance();
//    GoogleAuth.signOut().then(function () {
//        console.log('User signed out.');
//    });
//}


// Load the API client and auth2 library
function onLoad() {

    gapi.load('client:auth2', initClient);
}



function initClient() {
    gapi.client.init({
        apiKey: 'AIzaSyBNxWrr4Hipmls_OVxJbk1keZK8QThATZ4',
        discoveryDocs: ["https://people.googleapis.com/$discovery/rest?version=v1"],
        clientId: '651792012391-0soak3elmitjaab822m0gkei0o8k9lt5.apps.googleusercontent.com',
        scope: 'profile'
    }).then(function () {
        // Listen for sign-in state changes.
        gapi.auth2.getAuthInstance().isSignedIn.listen(updateSigninStatus);

        // Handle the initial sign-in state.
        updateSigninStatus(gapi.auth2.getAuthInstance().isSignedIn.get());
    });
}

function updateSigninStatus(isSignedIn) {
    if (isSignedIn) {
        googleplusConnectedResponse();
    }
}

function GoogleLoginClick() {
    gapi.auth2.getAuthInstance().signIn();
}

function handleSignoutClick(event) {
    gapi.auth2.getAuthInstance().signOut();
}

//function makeApiCall() {
 
//}

//--------------------------------
//var auth2; // The Sign-In object.
//var googleUser; // The current user.

///**
// * Calls startAuth after Sign in V2 finishes setting up.
// */
//var appStart = function () {
//    gapi.load('auth2', initSigninV2);
//};

///**
// * Initializes Signin v2 and sets up listeners.
// */
//var initSigninV2 = function () {
//    auth2 = gapi.auth2.init({
//        client_id: '651792012391-0soak3elmitjaab822m0gkei0o8k9lt5.apps.googleusercontent.com',
//        scope: 'profile'
//    });

//    // Listen for sign-in state changes.
//    auth2.isSignedIn.listen(signinChanged);

//    // Listen for changes to current user.
//    auth2.currentUser.listen(userChanged);

//    // Sign in the user if they are currently signed in.
//    if (auth2.isSignedIn.get() == true) {
//        auth2.signIn();
//    }

//    // Start with the current live values.
//    refreshValues();
//};

///**
// * Listener method for sign-out live value.
// *
// * @param {boolean} val the updated signed out state.
// */
//var signinChanged = function (val) {
//    googleplusConnectedResponse(auth2.currentUser.get());
//};

///**
// * Listener method for when the user changes.
// *
// * @param {GoogleUser} user the updated user.
// */
//var userChanged = function (user) {
//    console.log('User now: ', user);
//    googleUser = user;
//    updateGoogleUser();

//    var j = JSON.stringify(user, undefined, 2);
//};

///**
// * Updates the properties in the Google User table using the current user.
// */
//var updateGoogleUser = function () {
//    if (googleUser) {
//        //document.getElementById('user-id').innerText = googleUser.getId();
//        //document.getElementById('user-scopes').innerText =
//        //  googleUser.getGrantedScopes();
//        //document.getElementById('auth-response').innerText =
//        //  JSON.stringify(googleUser.getAuthResponse(), undefined, 2);
//    } else {
//        //document.getElementById('user-id').innerText = '--';
//        //document.getElementById('user-scopes').innerText = '--';
//        //document.getElementById('auth-response').innerText = '--';
//    }
//};

///**
// * Retrieves the current user and signed in states from the GoogleAuth
// * object.
// */
//var refreshValues = function () {
//    if (auth2) {
//        console.log('Refreshing values...');

//        googleUser = auth2.currentUser.get();

//        //document.getElementById('curr-user-cell').innerText =
//        //  JSON.stringify(googleUser, undefined, 2);
//        //document.getElementById('signed-in-cell').innerText =
//        //  auth2.isSignedIn.get();

//        updateGoogleUser();
//    }
//}


