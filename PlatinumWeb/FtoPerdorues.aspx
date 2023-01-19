<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FtoPerdorues.aspx.cs" Inherits="PlatinumWeb.FtoPerdorues" Async="true" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@4.1.3/dist/css/bootstrap.min.css" integrity="sha384-MCw98/SFnGE8fJT3GXwEOngsV7Zt27NXFoaoApmYm81iuXoPkFOJwJ8ERdknLPMO" crossorigin="anonymous"/>
    <script src="https://code.jquery.com/jquery-3.3.1.slim.min.js" integrity="sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo" crossorigin="anonymous"></script>--%>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/css/materialize.min.css"/>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.3/jquery.min.js" integrity="sha512-STof4xm1wgkfm7heWqFJVn58Hm3EtS31XFaagaa8VMReCXAkQnJZ+jEy8PCC/iT18dFy95WcExNHFTqLyp72eQ==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js"></script>
    <style>
        .btn{
            float: right;
            margin: 1% 1%;
        }
        .row{
            position: absolute;
            left: 0;
            background-color: white;
            right: 0;
            margin-left: auto;
            margin-right: auto;
            width: 40%;
        }
        .row-container{
            background-color: white;
            width: 100% !important;
            margin-top: 5%;
        }
        #modal1{
            height:55%;
        }
        .card-p{
            width: 70%;
        }
        .confirm-button{
            color:white;
            background-color: #0072c6;
            border:1px;
            border-radius:4px;
            cursor:pointer;
            padding:10px;
        }
        #cmbRolet{
            width:100%;
            border:none;
            border-bottom: 1px solid black !important;
        }
        #cmbRolet:focus-visible{
            border:none !important;
        }
        #cmbRolet_B-1{
            display:none;
        }
        .modal-footer{
            margin-top:6%;
        }
        .lds-grid {
            display: inline-block;
            position: relative;
            width: 80px;
            height: 80px;
        }
        .lds-grid div {
        position: absolute;
        width: 16px;
        height: 16px;
        border-radius: 50%;
        background: rgb(255, 255, 255);
        animation: lds-grid 3s linear infinite;
        }
        .lds-grid div:nth-child(1) {
        top: 8px;
        left: 8px;
        animation-delay: 0s;
        }
        .lds-grid div:nth-child(2) {
        top: 8px;
        left: 32px;
        animation-delay: -0.4s;
        }
        .lds-grid div:nth-child(3) {
        top: 8px;
        left: 56px;
        animation-delay: -0.8s;
        }
        .lds-grid div:nth-child(4) {
        top: 32px;
        left: 8px;
        animation-delay: -0.4s;
        }
        .lds-grid div:nth-child(5) {
        top: 32px;
        left: 32px;
        animation-delay: -0.8s;
        }
        .lds-grid div:nth-child(6) {
        top: 32px;
        left: 56px;
        animation-delay: -1.2s;
        }
        .lds-grid div:nth-child(7) {
        top: 56px;
        left: 8px;
        animation-delay: -0.8s;
        }
        .lds-grid div:nth-child(8) {
        top: 56px;
        left: 32px;
        animation-delay: -1.2s;
        }
        .lds-grid div:nth-child(9) {
        top: 56px;
        left: 56px;
        animation-delay: -1.6s;
        }
        @keyframes lds-grid {
        0%,100% {
            background-color: #4584ec;
        }
        25% {background-color: #38a555;}
        50% {
            background-color: #e44d40;
        }
        75% {background-color: #f3ba15;}
    }
        .loader {
        display: none;
        position: fixed;
        z-index: 99999999999;
        height: 2em;
        width: 2em;
        overflow: show;
        margin: auto;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
    }

    .loader-overlay {
        
        display: none;
        position: fixed;
        z-index: 99999999999;
        margin: auto;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
        background-color: rgba(255,255,255,0.7);
    }
    </style>
    <script> 
        $(document).ready(function () {
            $('.modal').modal();
        });
        function checkEmail() {
            if ($("#email_inline").val() == "") alert("Email-i nuk duhet te jete bosh!")
        }
        function showLoadingGif() {
            document.querySelector(".loader").style.display = "block";
            document.querySelector(".loader-overlay").style.display = "block";
        }
        function hideLoadingGif() {
            $(".modal").css("display", "none");
            var toastHTML = '<span>Ju lutem kontrolloni email-in!</span>';
            M.toast({ html: toastHTML, classes: 'rounded blue darken-3' });
            document.querySelector(".loader").style.display = "none";
            document.querySelector(".loader-overlay").style.display = "none";
        }
    </script>
    <title></title>
</head>
<body>
        <div class="loader-overlay"></div>
        <div class="loader">
            <div class="lds-grid">
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
                <div></div>
            </div>
        </div>
    
    <form id="form1" runat="server">
        <div id="modal1" class="modal">
            <div class="modal-content">
              <h4>Shto perdorues</h4>
                <br />
              <div class="input-field">
                <asp:TextBox runat="server"  ID="email_inline" type="email" class="validate"/>
                <label for="email_inline">Email</label>
              </div>
            <div class="input-field">
                <p>Roli</p>
                <dx:ASPxComboBox ID="cmbRolet" runat="server" ClientInstanceName="cmbRolet">
                </dx:ASPxComboBox>
                
            </div>
            </div>
            <div class="modal-footer">
                <a href="#!" class="modal-close waves-effect waves-green btn-flat">Mbyll</a>
                <asp:Button runat="server" type="button" id="button" class="confirm-button" Text="Konfirmo" OnClick="button_click" OnClientClick="showLoadingGif()"/>
            </div>
        </div>
        <div class="row">
            <div class="col s12 m6 row-container">
              <div class="card grey lighten-5">
                <a class="waves-effect blue darken-1 btn modal-trigger btn btn-primary" href="#modal1">Shto perdorues</a>
                <div class="card-content white-text">
                  <span class="card-title" style="color:black">Shto perdorues</span>
                  <p class="card-p" style="color:black;">Si perdorues Administrator i programit Alpha ju mund te shtoni perdorues te autentifiikuar me gmail. Per te shtuar perdorues vendosni email-in e perdoruesit qe doni te shtoni.</p>
                </div>
              </div>
            </div>
        </div>

    </form>
</body>
    <script>
    </script>
</html>
