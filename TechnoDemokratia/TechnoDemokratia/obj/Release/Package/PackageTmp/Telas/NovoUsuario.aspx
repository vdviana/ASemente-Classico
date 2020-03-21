<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NovoUsuario.aspx.cs" Inherits="TechnoDemokratia.Telas.NovoUsuario" %>

<%@ Register Src="~/UserControls/MenuPrincipal.ascx" TagPrefix="uc1" TagName="MenuPrincipal" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="../Style/bootstrap-3.3.5-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Style/social_btn_css/bootstrap-social.css" rel="stylesheet" />
    <link href="../Style/social_btn_css/assets/css/docs.css" rel="stylesheet" />
    <link href="../Style/social_btn_css/assets/css/font-awesome.css" rel="stylesheet" />
    <link href="../UserControls/resizetostretch.css" rel="stylesheet" />

    <style>
        .text-center {
            margin: 1.5%;
            width: 100%;
        }
    </style>

</head>

<script src="../Scripts/jquery-1.10.2.js"></script>

<script src="../Scripts/instagramLogin.js"></script>
<script src="../Scripts/facebooklogin.js"></script>
<script type="text/javascript" src="https://platform.linkedin.com/in.js">
    api_key: 785ud8k1mdc5nl
    authorize: true
    onLoad: onLinkedInLoad
</script>
<script src="../Scripts/linkedinlogin.js"></script>
<script src="https://apis.google.com/js/platform.js?onload=onLoad" async defer>
</script>
<script src="../Scripts/googlepluslogin.js"></script>
<%--<script src="../Scripts/twitterlogin.js"></script>--%>
<%--<script src="../Style/bootstrap-3.3.5-dist/js/facebooklogin.js"></script>--%>
<script>
    //var login_event = function (response) {
    //console.log("login_event");
    //console.log(response.status);
    //console.log(response);
    //    alert('reload');
    //    document.location.reload();
    // }

    //FB.login(function (response) {
    //    if (response.status === 'connected') {
    //        window.location.reload();
    //    } else if (response.status === 'not_authorized') {
    //        // The person is logged into Facebook, but not your app.
    //    } else {
    //        // The person is not logged into Facebook, so we're not sure if
    //        // they are logged into this app or not.
    //    }
    //}, {
    //    scope: 'public_profile,user_birthday,user_hometown,email,user_photos,user_location,user_religion_politics,user_status,pages_messaging_phone_number',
    //    return_scopes: true
    //});

    //IN.API.Profile("me").fields("first-name", "last-name", "email-address").result(function (data) {
    //    console.log(data);
    //}).error(function (data) {
    //    console.log(data);
    //});

    function linkedinConnectedResponse() {



        IN.API.Profile("me")
          .fields(["id", "firstName", "formattedName", "lastName", "pictureUrl", "publicProfileUrl", "headline", "emailAddress", "dateOfBirth"])
          .result(function (result) {
              console.log(result);

              $("#hfSocialID").val(result.values[0].id + "|LinkedIn");
              $("#txtNome").val(result.values[0].formattedName);
              $('#txtEmail').val(result.values[0].emailAddress);
              $('#txtDataNascimento').val(result.values[0].dateOfBirth);
              //if (result.value[0].gender == "male") {
              //    dropdownlist.selectedIndex = 1;
              //}
              //else if (result.value[0].gender == 'female') {
              //    dropdownlist.selectedIndex = 2;
              //}

              document.getElementById('imgPreview').removeAttribute('class');
              document.getElementById('imgPreview').setAttribute('src', result.values[0].pictureUrl);
              $("#hfImgRedeSocial").val(result.values[0].pictureUrl);

              //$("#btnCadastrarPessoa").click();
              PostChanges();


              console.log(result);
          })
          .error(function (err) {
              console.log("linkedin err");
              console.log(err);
          });
    }

    function facebookConnectedResponse(response) {

        //FB.api('/me?fields=email', function (response) {
        //    console.log(response);
        //});


        FB.api('/me?fields=id,name,birthday,email,gender', function (response) {
            $("#hfSocialID").val(response.id + "|Facebook");
            $("#txtNome").val(response.name);
            try { $('#txtEmail').val(response.email); } catch (e) { $('#txtEmail').val(response.email); }
            try {
                var bday = response.birthday.split('/');
                $('#txtDataNascimento').val(bday[1] + '/' + bday[0] + '/' + bday[2]);
            } catch (e) { }

            dropdownlist = document.getElementById('ddlSexo');
            if (response.gender == 'male') {
                dropdownlist.selectedIndex = 1;
            }
            else if (response.gender == 'female') {
                dropdownlist.selectedIndex = 2;
            }
            //  document.getElementById("btnCadastrarPessoa").click();

        });

        FB.api('http://graph.facebook.com/me/picture?type=large', function (responsepic) {
            document.getElementById('imgPreview').removeAttribute('class');
            document.getElementById('imgPreview').setAttribute('src', responsepic.data.url);
            $("#hfImgRedeSocial").val(responsepic.data.url);
        });

        PostChanges();


    }

    function googleplusConnectedResponse() {

        gapi.client.people.people.get({
            resourceName: 'people/me'
        }).then(function (resp) {

            $("#hfSocialID").val(resp.result.metadata.sources[0].id + "|Google+");
            $("#txtNome").val(resp.result.names["0"].displayName);
            $('#txtEmail').val(resp.result.emailAddresses["0"].value);
            try {
                $('#txtDataNascimento').val(resp.result.birthdays[0].date.day.toString() + '/' + resp.result.birthdays[0].date.month.toString());
            } catch (response) {
            }

            dropdownlist = document.getElementById('ddlSexo');
            if (resp.result.genders["0"].value === "male") {
                dropdownlist.selectedIndex = 1;
            }
            else if (resp.result.genders["0"].value === 'female') {
                dropdownlist.selectedIndex = 2;
            }

            document.getElementById('imgPreview').removeAttribute('class');
            document.getElementById('imgPreview').setAttribute('src', resp.result.photos["0"].url);
            $("#hfImgRedeSocial").val(resp.result.photos["0"].url);
            console.log(resp.result.metadata.sources[0]);
        }, function (reason) {
            console.log('Error: ' + reason.result.error.message);
        });
        //var data = result.getBasicProfile()
        //   console.log(result);
        //console.log(data);
        ////console.log(data.Eea);
        //console.log(data.Paa);
        //console.log(data.U3);
        //console.log(data.ig);

        //$("#hfSocialID").val(data.Eea + "Google+");
        //$("#txtNome").val(data.ig);
        //$('#txtEmail').val(data.U3);
        //$('#txtDataNascimento').val(result.values[0].dateOfBirth);
        //if (result.value[0].gender == "male") {
        //    dropdownlist.selectedIndex = 1;
        //}
        //else if (result.value[0].gender == 'female') {
        //    dropdownlist.selectedIndex = 2;
        //}

        //document.getElementById('imgPreview').removeAttribute('class');
        //document.getElementById('imgPreview').setAttribute('src', data.Paa);
        //$("#btnCadastrarPessoa").click();

        PostChanges();

    }

    //function twitterConnectedResponse() { } //em desenvolvimento

    function instagramConnectedResponse(response) {
        try {

            $("#hfSocialID").val(response.data.id + "|Instagram");
            $("#txtNome").val(response.data.full_name);
            //$('#txtEmail').val(resp.result.emailAddresses["0"].value);

            document.getElementById('imgPreview').removeAttribute('class');
            document.getElementById('imgPreview').setAttribute('src', response.data.profile_picture);
            $("#hfImgRedeSocial").val(response.data.profile_picture);


            console.log(response);

            PostChanges();
        } catch (e) { return; }

    }

    function PostChanges() {
        //setTimeout(function () { __doPostBack('btnCadastrarPessoa', '') }, 2000);
        $("#btnCadastrarPessoa").removeAttr('disabled');
        setTimeout(function () { $("#btnCadastrarPessoa").click(); }, 2000);
    }

</script>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">

        <asp:ScriptManager ID="scrptmgr" runat="server" />
        <%--<uc1:FacebookAuthent runat="server" ID="FacebookAuthent" />--%>
        <uc1:MenuPrincipal runat="server" ID="MenuPrincipal" />
        <div class="container" style="width: 80%">

            <div style="align-content: center;">
                <asp:UpdatePanel ID="upPanel" runat="server" ChildrenAsTriggers="true">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnCadastrarPessoa" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="active">
                            <div class="col-md-6 ">
                                <div style="text-align: center">
                                    <label>Preencha o formulário: </label>
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </div>
                                <div class="col-md-6 ">
                                    <asp:HiddenField ID="hfImgRedeSocial" runat="server" />
                                    <asp:FileUpload ID="imgInput" class="inputfile " runat="server" onchange="javascript:readImageURL(this);" />
                                    <label for="imgInput" class="btnfupload btn btn-link " style="width: 70%; margin-left: 17%; float: left; background-color: lightblue; border-top-left-radius: 20px; border-top-right-radius: 20px" id="addimg">Anexar Imagem</label>
                                    <br />
                                    <img id="imgPreview" src="../Images/imgprs.png" style="width: 70%; margin-left: 17%; float: left; height: 80%; border-bottom-left-radius: 20px; border-bottom-right-radius: 20px" class="resizeimage" />
                                </div>
                                <div class="col-md-6">
                                    <asp:HiddenField ID="hfSocialID" runat="server" />
                                    <asp:TextBox placeholder="Digite seu nome" runat="server" ID="txtNome" CssClass="text-center" Style="align-self: flex-start" />
                                    <asp:TextBox placeholder="Digite seu E-mail" runat="server" ID="txtEmail" CssClass="text-center" Style="align-self: flex-start" />
                                    <asp:TextBox ID="txtDataNascimento" runat="server" placeholder="Insira data nascimento" CssClass="text-center" />
                                    <asp:DropDownList runat="server" ID="ddlSexo" CssClass="dropdown text-center" Style="width: 100%;">
                                        <asp:ListItem Text="selecione o sexo" Value="--"></asp:ListItem>
                                        <asp:ListItem Text="Masculino" Value="M"></asp:ListItem>
                                        <asp:ListItem Text="Feminino" Value="F"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox placeholder="Digite seu CPF" runat="server" ID="txtCPF" CssClass="text-center" Style="align-self: flex-start" />
                                    <asp:TextBox placeholder="Digite seu RG" runat="server" ID="txtRG" CssClass="text-center" Style="align-self: flex-start" />
                                    <asp:TextBox placeholder="Digite seu Telefone" runat="server" ID="txtTelefone" CssClass="text-center" Style="align-self: flex-start" />
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div style="text-align: center">
                                    <label>ou integre com outras contas para se cadastrar:</label>
                                    <br />
                                    <br />
                                    <div class="col-md-12">
                                        <!--
  Below we include the Login Button social plugin. This button uses
  the JavaScript SDK to present a graphical Login button that triggers
  the FB.login() function when clicked.
-->
                                        <br />
                                        <br />
                                        <div class="col-md-6"></div>
                                        <div class="col-md-12">
                                            <br />
                                            <%-- <a class="btn btn-block btn-social btn-twitter" onclick="window.Alert('a implementar')">
                                                <span class="fa fa-twitter"></span>Integrar com Twitter  </a>--%>
                                            <a class="btn btn-block btn-social btn-facebook" onclick="FB.login(function(response){if (response.status === 'connected') {window.location.reload();}},{scope:'public_profile,email,user_photos'});">
                                                <span class="fa fa-facebook"></span>Integrar com Facebook</a>
                                            <a class="btn btn-block btn-social btn-linkedin" onclick="liAuth()">
                                                <span class="fa fa-linkedin"></span>Integrar com LinkedIn</a>
                                            <a class="btn btn-block btn-social btn-google" onclick="GoogleLoginClick()">
                                                <span class="fa fa-google"></span>Integrar com Google+</a>
                                            <a class="btn btn-block btn-social btn-instagram" onclick="IGLogin();">
                                                <span class="fa fa-instagram"></span>Integrar com Instagram</a>
                                        </div>
                                        <br />
                                        <br />
                                        <br />
                                        <%--       <asp:ImageButton ImageUrl="../Images/facebookbutton.png" ID="HyperLink1" runat="server"></asp:ImageButton>--%>
                                        <br />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <hr />
                        <div class="col-md-12 ">
                            <div class="col-md-3"></div>
                            <div class="col-md-6" style="text-align: center; margin: 3%;">
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtSenha" name="txtSenha" placeholder="Insira a senha" class="text-center" TextMode="Password"
                                        onkeyup="CheckPassword(this);" Style="width: 50%" />
                                    <asp:TextBox runat="server" class="btn btn-default" disabled="disabled" ID="passcheck" value=" " Style="width: 30%" />
                                </div>
                                <div class="col-md-12">
                                    <asp:TextBox runat="server" ID="txtConfirmar" name="txtConfirmar" placeholder="Confirme a senha" class="text-center" TextMode="Password"
                                        onkeyup="ConfirmPassword(this);" Style="width: 50%" />
                                    <asp:HiddenField runat="server" ID="hfPassconfirm" Value="" />
                                    <asp:TextBox runat="server" class="btn btn-default" disabled="disabled" ID="passconfirm" value=" " Style="width: 30%" />
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                            <div class="col-md-12 ">
                                <div style="float: right">
                                    <asp:Button runat="server" ID="btncancelar" OnClick="btncancelar_Click" Text="Cancelar" class=" btn btn" Style="border-radius: 80px; background-color: red; color: white" />
                                    &nbsp; 
                                <asp:Button runat="server" ID="btnCadastrarPessoa" disabled="disabled" OnClick="btnCadastrarPessoa_Click" Text="Cadastrar-se" class=" btn btn" Style="border-radius: 80px; background-color: lightblue" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </form>

    <script src="../Scripts/chkpwd.js"></script>
    <script src="../UserControls/jsuploadimage.js"></script>


</body>
</html>
