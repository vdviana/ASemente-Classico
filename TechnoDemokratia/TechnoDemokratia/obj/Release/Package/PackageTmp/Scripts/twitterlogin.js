//POST /oauth2/token HTTP/1.1
//Host: api.twitter.com
//User-Agent: My Twitter App v1.0.23
//Authorization: Basic eHZ6MWV2RlM0d0VFUFRHRUZQSEJvZzpMOHFxOVBaeVJn
//NmllS0dFS2hab2xHQzB2SldMdzhpRUo4OERSZHlPZw==
//Content-Type: application/x-www-form-urlencoded;charset=UTF-8
//Content-Length: 29
//Accept-Encoding: gzip

//grant_type=client_credentials

//window.onload = //ajaxget();
//window.onload = agoraseraqvai();
function ajaxget() {

    try {
        $.post({
            //method: "post",
            headers: {
                'access_control_allow_origin': document.URLUnencoded,
                dataType: "jsonp",
                url: "https://api.twitter.com/oauth/request_token",
                'Client_id': 'yncXgnpA6uNLKpuKh04rzk1NW',
                'content-type': 'application/x-www-form-urlencoded;charset=UTF-8',
            },
            // 'content-length': '29',
            // 'accept-encoding': 'gzip',
            success: function (data) {
                console.log('twitter');
                console.log(data);
                twitterConnectedResponse(data);
            },
            error: function (data) {
                console.log('failure twitter');
                console.log(data);

            }
        }, { 'grant_type': 'client_credentials' });
    } catch (e) {
        console.log('failure twitter');
        console.log(data);

    }
}

function agoraseraqvai() {
    
    $.post({
        url: 'https://api.twitter.com/oauth/request_token',
        //type: 'POST',
        headers: {
            //'access-control-allow-origin': document.URL,
            //'authorization': 'Basic yncXgnpA6uNLKpuKh04rzk1NW',
            //content_type: 'application/x-www-form-urlencoded',
            content_type: 'text/plain',
            oauth_consumer_key: "'authorization': 'Basic ','yncXgnpA6uNLKpuKh04rzk1NW'",
            oauth_signature_method: "HMAC-SHA1",
            oauth_timestamp: parseInt((new Date()).getTime() / 1000),
            oauth_nonce: "R" + parseInt((new Date()).getTime() / 1000),
            oauth_version: "1.0"

        },
        //data: {
        //    'uid': 36,
        //},
        success: function (data) {
            console.log(data);
        },
        error: function (err) {
            console.log(err);
        }
    }, { 'grant_type': 'client_credentials' });
}
function httpGet(theUrl) {
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.open("GET", theUrl, false); // false for synchronous request
    xmlHttp.send(null);
    return xmlHttp.responseText;
}

$.ajax({
    url: "http://api.twitter.com/oauth/request_token",
    type: "POST",
    contentType: "json",
    data: {
        oauth_consumer_key: "DFwQqCnOTaYVbZQdBFqpR",
        oauth_signature_method: "HMAC-SHA1",
        oauth_timestamp: parseInt((new Date()).getTime() / 1000),
        oauth_nonce: "R" + parseInt((new Date()).getTime() / 1000),
        oauth_version: "1.0"
    },
    success: function (data) {
        debugger;
    },
    error: function (a, b, c) {
        debugger;
    }
});