
function CheckPassword(value) {
    var text = document.getElementById('txtSenha').value;
    var score = 0;

    if (text.length >= 8) {
        score = score + 20;
    }

    if (text.length < 8) {
        score = score - 30;
    }

    if (text.indexOf('0') >= 0 || text.indexOf('1') >= 0 || text.indexOf('2') >= 0 || text.indexOf('3') >= 0 || text.indexOf('4') >= 0 ||
        text.indexOf('5') >= 0 || text.indexOf('6') >= 0 || text.indexOf('7') >= 0 || text.indexOf('8') >= 0 || text.indexOf('9') >= 0) {
        score = score + 20;
    }

    if (text.indexOf('q') >= 0 || text.indexOf('w') >= 0 || text.indexOf('e') >= 0 || text.indexOf('r') >= 0 || text.indexOf('t') >= 0 ||
        text.indexOf('y') >= 0 || text.indexOf('u') >= 0 || text.indexOf('i') >= 0 || text.indexOf('o') >= 0 || text.indexOf('p') >= 0 ||
        text.indexOf('a') >= 0 || text.indexOf('s') >= 0 || text.indexOf('d') >= 0 || text.indexOf('f') >= 0 || text.indexOf('f') >= 0 ||
        text.indexOf('g') >= 0 || text.indexOf('h') >= 0 || text.indexOf('j') >= 0 || text.indexOf('k') >= 0 || text.indexOf('l') >= 0 ||
        text.indexOf('ç') >= 0 || text.indexOf('z') >= 0 || text.indexOf('x') >= 0 || text.indexOf('c') >= 0 || text.indexOf('v') >= 0 ||
        text.indexOf('b') >= 0 || text.indexOf('n') >= 0 || text.indexOf('m') >= 0) {
        score = score + 20;

    }

    if (text.indexOf('Q') >= 0 || text.indexOf('W') >= 0 || text.indexOf('E') >= 0 || text.indexOf('R') >= 0 || text.indexOf('T') >= 0 ||
        text.indexOf('Y') >= 0 || text.indexOf('U') >= 0 || text.indexOf('I') >= 0 || text.indexOf('O') >= 0 || text.indexOf('P') >= 0 ||
        text.indexOf('A') >= 0 || text.indexOf('S') >= 0 || text.indexOf('D') >= 0 || text.indexOf('F') >= 0 || text.indexOf('F') >= 0 ||
        text.indexOf('G') >= 0 || text.indexOf('H') >= 0 || text.indexOf('J') >= 0 || text.indexOf('K') >= 0 || text.indexOf('L') >= 0 ||
        text.indexOf('Ç') >= 0 || text.indexOf('Z') >= 0 || text.indexOf('X') >= 0 || text.indexOf('C') >= 0 || text.indexOf('V') >= 0 ||
        text.indexOf('B') >= 0 || text.indexOf('N') >= 0 || text.indexOf('M') >= 0) {
        score = score + 20;
    }

    if (text.indexOf('!') >= 0 || text.indexOf('@') >= 0 || text.indexOf('#') >= 0 || text.indexOf('$') >= 0 ||
    text.indexOf('%') >= 0 || text.indexOf('¨') >= 0 || text.indexOf('&') >= 0 || text.indexOf('*') >= 0 ||
    text.indexOf('(') >= 0 || text.indexOf(')') >= 0 || text.indexOf('_') >= 0 || text.indexOf('+') >= 0 ||
    text.indexOf('-') >= 0 || text.indexOf('=') >= 0 || text.indexOf('¹') >= 0 || text.indexOf('²') >= 0 ||
    text.indexOf('³') >= 0 || text.indexOf('£') >= 0 || text.indexOf('¢') >= 0 || text.indexOf('¬') >= 0 ||
    text.indexOf('§') >= 0 || text.indexOf('`') >= 0 || text.indexOf('{') >= 0 || text.indexOf('^') >= 0 ||
    text.indexOf('}') >= 0 || text.indexOf(':') >= 0 || text.indexOf('?') >= 0 || text.indexOf('>') >= 0 ||
    text.indexOf('<') >= 0 || text.indexOf('|') >= 0 || text.indexOf('°') >= 0 || text.indexOf('º') >= 0 ||
    text.indexOf('ª') >= 0 || text.indexOf('\\') >= 0 || text.indexOf(',') >= 0 || text.indexOf('.') >= 0 ||
    text.indexOf(';') >= 0 || text.indexOf('/') >= 0 || text.indexOf('~') >= 0 || text.indexOf(']') >= 0 ||
    text.indexOf('´') >= 0 || text.indexOf('[') >= 0 || text.indexOf(')') >= 0 || text.indexOf(' ') >= 0) {
        score = score + 20;
    }


    if (score < 30) {
        color = "width: 30%; color:white; background-color:red";
        strength = "muito fraco";
    }

    else if (score >= 30 && score < 60) {
        color = "width: 30%; color:black ;background-color:orange";
        strength = "fraco";
    }

    else if (score >= 60 && score < 80) {
        color = "width: 30%; color:black; background-color:yellow";
        strength = "moderado";
    }

    else if (score >= 80 && score < 90) {
        color = "width: 30%; color:black; background-color:lawngreen";
        strength = "forte";
    }

    else if (score >= 90) {
        color = "width: 30%; color:black; background-color:lightgreen";
        strength = "muito forte";
    }

    if (text === "") {
        color = "width: 30%; color:white; background-color:white";
        strength = "";
    }


    $('#passcheck').val(strength);
    $('#passcheck').attr('style', color)
}


function ConfirmPassword(value) {

    var text = document.getElementById('txtSenha').value;
    var confirmtext = document.getElementById('txtConfirmar').value;
    var strength = document.getElementById('passcheck').value;

    var acceptablestrength = false;

    if (strength === "muito fraco" || strength === "fraco") {
        acceptablestrength = false;
    } else {
        acceptablestrength = true;
    }

    if (confirmtext === "" || text === "") {
        $('#passconfirm').val('');
        $('#passconfirm').attr('style', 'background-color:white; width: 30%');
    }
    else {
        if (text === confirmtext && text !== "" && confirmtext !== "" && acceptablestrength) {

            $('#passconfirm').val('OK');

            $('#passconfirm').attr('style', 'background-color:lightgreen; width: 30%');

            $('#btnCadastrarPessoa').removeAttr('disabled');

        } else {
            $('#passconfirm').val('Ñ OK');
            $('#hfPassconfirm').val('');
            $('#passconfirm').attr('style', 'background-color:red ;width: 30%; color:white');
            $('#btnCadastrarPessoa').attr('disabled', 'disabled');
        }
    }
}