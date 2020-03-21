<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="progressbar.ascx.cs" Inherits="TechnoDemokratia.UserControls.progressbar" %>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        var loading = document.getElementById('modalloading')
        loading.style.display = 'block';
        setTimeout(function () {
            //var modal = $('<div />');
            //modal.addClass("modal");
            //$('body').append(modal);
            HideProgress()       
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 6000);
    }

    function HideProgress() {
        var loading = document.getElementById('modalloading')
        loading.style.display = 'hidden';
    }


    $('form').live("submit", function () {
        ShowProgress();
    });
</script>

<style>
    /* The Modal (background) */
    .modal {
        display: none; /* Hidden by default */
        position: fixed; /* Stay in place */
        z-index: 1; /* Sit on top */
        left: 0;
        top: 0;
        width: 100%; /* Full width */
        height: 100%; /* Full height */
        overflow: auto; /* Enable scroll if needed */
        background-color: rgb(0,0,0); /* Fallback color */
        background-color: rgba(0,0,0,0.6); /* Black w/ opacity */
    }

    /* Modal Content/Box */
    .modal-content {
        background-color: #fefefe;
        margin-top: 15%;
        margin-right: 15%; /* 15% from the top and centered */
        margin-left: 15%;
        margin-bottom: 15%;
        border: 1px hidden #888;
        background-color: none;
        width: 55%; /* Could be more or less, depending on screen size */
    }

    /* The Close Button */
    .close {
        color: #aaa;
        float: right;
        font-size: 28px;
        font-weight: bold;
    }

        .close:hover,
        .close:focus {
            color: black;
            text-decoration: none;
            cursor: pointer;
        }
</style>

<!-- Trigger/Open The Modal -->
<asp:UpdatePanel ID="upnlMdlLoading" runat="server" ChildrenAsTriggers="true">
    <ContentTemplate>
        <!-- The Modal -->
        <div id="modalloading" class="modal" style="height: 100%">
            <!-- Modal content -->
            <div class="modal-content" style="background: none">
                <div id="loading" align="center">
                    <img src="../Images/loader1.gif" alt="" />
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>


