<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeBehind="Prezantohu.aspx.cs" Inherits="PlatinumWeb.login" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<html>
<head runat="server">
    <title>Alpha Web</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <%--    <meta http-equiv="X-UA-Compatible" content="IE=8" >--%>
    <meta name="description" content="Login-i AlphaWeb" />
    <meta name="identifikuesLogin" content="LoginPage" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/css/materialize.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js"></script>
      <link href="https://fonts.googleapis.com/icon?family=Material+Icons" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.1/jquery.min.js" integrity="sha512-aVKKRRi/Q/YV+4mjoKBsE4x3H+BkegoM/em46NNlCqNTmUYADjBbeNefNxYV7giUp0VxICtqdrbqU7iVaeZNXA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
    <%--  <script src="js/jquery-1.10.2.min.js"></script>        --%>
    <%--<script src="public/modernizr.min.js"></script>--%>
    <%--<script src="public/placeholder.js"></script>--%>
    <%--<link href="public/login.css" rel="stylesheet" />--%>
    <script type="module">
        import { initializeApp } from "https://www.gstatic.com/firebasejs/9.8.3/firebase-app.js";
        import { getAuth, signInWithPopup, GoogleAuthProvider, signInWithEmailAndPassword, signInWithRedirect } from "https://www.gstatic.com/firebasejs/9.8.3/firebase-auth.js";
        const firebaseConfig = {
            apiKey: "AIzaSyAbxtG7R8ueB5slHXlDCkB74p2NnPiATqY",
            authDomain: "alpha-secure-login.alpha.al",
            databaseURL: "https://imb-payment.firebaseio.com",
            projectId: "imb-payment",
            storageBucket: "imb-payment.appspot.com",
            messagingSenderId: "269963445243",
            appId: "1:269963445243:web:6b8347c72e861cd438415e"
        };
        const app = initializeApp(firebaseConfig);
        window.signInWithPopup = signInWithPopup;
        window.signInWithEmail = signInWithEmailAndPassword;
        window.getAuth = getAuth;
        window.signInWithRedirect = signInWithRedirect;
        window.GoogleAuthProvider = await new GoogleAuthProvider();
        window.GoogleAuthProvider.setCustomParameters({
            prompt: 'select_account'
        });
        window.auth = await getAuth();
    </script>
    <style>
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
    position: fixed;
    z-index: 999;
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
        position: fixed;
        z-index: 999;
        margin: auto;
        top: 0;
        left: 0;
        bottom: 0;
        right: 0;
        background-color: rgba(255,255,255,0.7);
    }
        @font-face {
            font-family: 'Open Sans';
            font-style: normal;
            font-weight: 400;
            src: local('Open Sans'), local('OpenSans'), url('css/fonts/OpenSans-Regular.ttf') format('truetype');
        }
        @font-face {
            font-family: 'hk groteks';
            src: url('css/fonts/HKGrotesk-Light.otf');
        }
        .loginForm{
            margin-top:-1000px;
            transition: 0.8s ease-in-out;
            height: max-content !important;
        }
        body{
            overflow-x:hidden;
        }
        html > /**/ body body * {
            font-family: 'Open Sans', serif;
            font-size: 16px;
        }

        body {
            margin: 0;
        }

        .cpyright, .ndihmeKontakt, article, .kosove, .vizitori, .labelInfo, .infoLbl, .keniHarruar {
            font-family: Tahoma, Geneva, sans-serif;
        }


        .styleLoginButton {
            height: 28px;
            float: right;
            vertical-align: top;
            margin-top: 0;
        }


        .labelInfo {
            padding-top: 20px;
            padding-bottom: 20px;
            text-align: center;
            display: block;
            height: auto;
        }

        .login {
            display: block;
            padding: 40px;
            margin: 0;
            border-width: 0px;
            background-color: transparent;
            box-shadow: 0 0px 0px;
            border-collapse: separate !important;
            height: max-content;
        }

        .input {
            display: block;
            width: 248px;
            padding-top: 10px;
            height: 40px;
            position: relative;
            border: 0.5px #999999;
        }

        .inputPassword {
            padding-top: 0px;
            border-top: 1px solid #e7e7e7;
            padding-bottom: 10px;
        }

        .inputUsername {
            border-top: 1px solid #e7e7e7;
            padding-top: 0px;
        }

        .input table tbody tr td table {
            border: 0;
        }

        .input table tbody tr td {
            border: 0;
        }

        .submain {
            display: flex !important;
            height: 100% !important;
            width: 100% !important;
            justify-content: center !important;
        }

        .klient .main .submain {
            width: 340px;
        }

        .oshe .main .submain {
            width: 340px;
        }

        .vodafone .main .submain {
            width: 340px;
        }

        .moh .main .submain {
            width: 340px;
        }

        .mzhu .main .submain {
            width: 340px;
        }

        .header {
            display: flex;
            background-color:#0187dd;
        }

        .gjuha {
            float: none;
            margin-right: 5px;
            position: relative;
            top: 50%;
            transform: translateY(-50%);
            overflow: hidden;
            display: inline;
        }

        .content {
            padding: 0 0 50px 0;
            margin: 0;
            clear: both;
        }

        .klient .main .content {
            border-top: 1px #5D9AD3 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        .oshe .main .content {
            border-top: 1px #002857 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        .vodafone .main .content {
            border-top: 1px #ed1b24 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        .moh .main .content {
            border-top: 1px #dcc4c0 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        .mzhu .main .content {
            border-top: 1px #000000 solid;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        footer {
            clear: both;
            margin: 0;
            padding-top: 20px;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            height: 40%;
            border-top: 1px solid #e0e0e0;
            background-color: #f9f9f9;
        }
		.button {
		  background-color: #4CAF50; /* Green */
		  color:white;
		  border: none;
		  height: 40px;
		width: 100px;
		max-width: 246px;
		padding-left: 1px;
		font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
		}
        .klient footer {
            display: none;
        }

        .oshe footer {
            display: none;
        }

        .vodafone footer {
            display: none;
        }

        .moh footer {
            display: none;
        }

        .mzhu footer {
            display: none;
        }

        .vodafone .dxh2h, .vodafone .dxh1s {
            background-color: #f06666 !important;
        }

        .moh .dxh2h, .moh .dxh1s {
            background-color: #f06666 !important;
        }

        .oshe .dxh1s, .oshe .dxh2h {
            background-color: #99a9bb !important;
        }

        .mzhu .dxh1s, .mzhu .dxh2h {
            background-color: #985fa0 !important;
        }

        .cpyright {
            clear: both;
            float: none;
            font-size: 16px;
            text-align: center;
            width: 20%;
            margin-bottom: 4px;
            max-width: 30%;
            margin-left: auto;
            margin-right: auto;
            font: 13px "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
        }

        .cpyrightText {
            font-size: smaller;
            font: 16px "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
        }

        .cpyrightLink {
            color:#0187dd;
            text-decoration: underline;
            font-family: "Segoe UI";
            font: 14px "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
        }

        .contact {
            float: right;
            line-height: 24px;
        }

        article {
            display: block;
        }

        .ndihmeKontakt, .kosove, .vizitori {
            font-size: 14px;
            text-align: right;
        }

        .LoginError {
            height: auto;
            color: red;
            text-align: left;
            padding-top: 0;
        }

        

        .mainContent {
            display: table-cell;
            vertical-align: middle;
            padding: 0;
            position: relative;
            height: 478px;
            max-height:478px;
            width: auto !important;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            /* bottom: 50%; */
            /* transform: translate(-50%, -50%); */
            height: 100%;
            left: 0px;
            margin: 10% auto;
}
        }
        .hero-list{
            display:none;
        }
        .info {
            max-width: 300px;
            display: table-cell;
            text-align: left;
        }

        .loginForm {
            border-radius:20px !important;
            text-align: right;
            border-radius: 8px;
            height: 100%;
            background: #0187dd;
            background: linear-gradient(90deg, rgba(2,0,36,1) 0%, rgba(244,244,244,1) 0%, rgba(103,165,245,0.3925945378151261) 0%, rgba(103,165,245,0.3925945378151261) 0%, rgb(0 108 247) 0%, rgb(45 122 225) 0%);
            margin: auto;
            display: flex;
            width: 100%;
            height: 100%;
        }

        .oshe .loginForm, .vodafone .loginForm {
            background: white;
            box-shadow: 1px 4px 3px 0px #bfbdbd;
        }

        .moh .loginForm {
            background: rgba(255, 255, 255, 0.3);
            box-shadow: 1px 2px 3px 0px #131313;
        }

        .mzhu .loginForm {
            background: white;
            box-shadow: 1px 4px 3px 0px #777777;
        }

        .infoLbl {
            font-size: 16px;
            color: #333333;
            width: auto;
        }

        .gjuhet {
            float: none;
            height: 20px;
            max-width: 100%;
            text-align: center;
        }

        .logo {
            background-image: url(images/FaqjaPare/alpha-logo.png);
            background-repeat: no-repeat;
            float: none;
            width: 45px;
            height: 53px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            margin-left: auto;
            margin-right: auto;
            margin-top: 10px;
        }

        .klient .main .logo {
            background-image: url(images/FaqjaPare/keshLogo.png);
            background-repeat: no-repeat;
            float: left;
            width: 227px;
            height: 68px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        .oshe .main .logo {
            background-image: url(images/FaqjaPare/osheLogo.png);
            background-repeat: no-repeat;
            float: left;
            width: 235px;
            height: 68px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        .vodafone .main .logo {
            background-image: url(images/FaqjaPare/vodSlogan.png);
            background-repeat: no-repeat;
            float: left;
            width: 285px;
            height: 68px;
            margin-bottom: 0;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        .moh .main .logo {
            background-image: url(images/FaqjaPare/mohSlogan.png);
            background-repeat: no-repeat;
            float: left;
            width: 285px;
            height: 205px;
            margin-bottom: 15px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        .mzhu .main .logo {
            background-image: url(images/FaqjaPare/mzhuLogo.png);
            background-repeat: no-repeat;
            float: left;
            width: 235px;
            height: 68px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
        }

        .kesh .headerKesh {
            width: 93%;
            margin: auto;
        }

        .mzhu .header {
            width: 340px;
            border-radius: 8px 8px 0px 0px !important;
            margin: 5% auto auto auto;
            height: 78px;
            background-color: #f7f7f7;
            box-shadow: 0 2px 2px rgba(0, 0, 0, 0.3);
            border-radius: 2px;
        }

        .vodafone .header {
            background-color: transparent;
            margin-top: 7%;
        }

        .moh .header {
            background-color: transparent;
            margin-top: 2%;
        }

        .oshe .header {
            background-color: transparent;
            margin-top: 5%;
        }

        .infoIcon {
            width: 32px;
            height: 32px;
        }

        input[type="text"].dxeEditAreaSys, input[type="password"].dxeEditAreaSys {
            padding-left: 7px;
        }

        .left, .right {
            display: table;
            height: inherit;
        }

        .left {
            margin-top: 20px;
        }

        .mainContent .left {
            float: left;
        }

        .mainContent .right {
            float: right;
        }

        .klient .mainContent .left {
            display: none;
        }

        .oshe .mainContent .left {
            display: none;
        }

        .vodafone .mainContent .left {
            display: none;
        }

        .moh .mainContent .left {
            display: none;
        }

        .mzhu .mainContent .left {
            display: none;
        }

        .klient .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .oshe .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .vodafone .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .moh .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .mzhu .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .mylink {
            color: white;
            font: 12px 'Segoe UI', Helvetica, 'Droid Sans', Tahoma, Geneva, sans-serif;
        }

            .mylink:hover {
                color: #f9f9f9 !important;
            }

        .klient .main .mylink {
            color: #5D9AD3;
        }

        .oshe .main .mylink {
            color: #002857;
        }

        .vodafone .main .mylink {
            color: #e60000;
        }

            .vodafone .main .mylink:hover {
                color: #f06666 !important;
            }

        .moh .main .mylink {
            color: #99cfd1;
        }

            .moh .main .mylink:hover {
                color: #7aa5a7 !important;
            }

        .oshe .main .mylink {
            color: #002857;
        }

            .oshe .main .mylink:hover {
                color: #325278 !important;
            }

        .mzhu .main .mylink {
            color: black;
            font-weight: bold;
        }

        .hyrje {
            color: white;
            background-color:#1251b8;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
            font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif;
            transition-duration:0.3s;
        }

            .hyrje:hover {
                background-color: white;
                color:black;
            }

        .vodafone .hyrje {
            background-color: #e60000;
        }

            .vodafone .hyrje:hover {
                background-color: #eb3232;
            }

        .moh .hyrje {
            background-color: #99cfd1;
        }

            .moh .hyrje:hover {
                background-color: #7aa5a7;
            }

        .oshe .hyrje {
            background-color: #ff9e19;
        }

            .oshe .hyrje:hover {
                background-color: #ffac3b;
            }

        .mzhu .hyrje {
            background-color: #6c1b78;
        }

            .mzhu .hyrje:hover {
                background-color: #7a3185;
            }

        .klient .main .right .input .hyrje {
            color: white;
            background-color: #5D9AD3;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .oshe .main .right .input .hyrje {
            color: white;
            background-color: #ff9e19;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .vodafone .main .right .input .hyrje {
            color: white;
            background-color: #ed1b24;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .moh .main .right .input .hyrje {
            color: white;
            background-color: #ed1b24;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .mzhu .main .right .input .hyrje {
            color: white;
            background-color: #6c1b78;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

        .klient .main .right .input .hyrje:hover {
            background-color: #75a9da;
        }

        .oshe .main .right .input .hyrje:hover {
            background-color: #ffac3b;
        }

        .vodafone .main .right .input .hyrje:hover {
            background-color: #f03d45;
        }

        .moh .main .right .input .hyrje:hover {
            background-color: #f03d45;
        }

        .mzhu .main .right .input .hyrje:hover {
            background-color: #7a3185;
        }

        .keniHarruar {
            text-align: center;
            display: block;
            font: medium;
            font-weight: normal;
            color: white;
            height: auto;
            font-size: 14px;
           margin: 0px auto 20px auto !important;
        }

        .keniHarruar:hover {
            color: white !important;
        }

        .vodafone .keniHarruar {
            color: #e60000;
            margin-top: 5px;
        }

            .vodafone .keniHarruar:hover {
                color: #f06666;
            }

        .moh .keniHarruar {
            color: #99cfd1;
            margin-top: 5px;
        }

            .moh .keniHarruar:hover {
                color: #7aa5a7;
            }

        .oshe .keniHarruar {
            color: #ff9e19;
            margin-top: 5px;
        }

            .oshe .keniHarruar:hover {
                color: #ffac3b;
            }

        .mzhu .keniHarruar {
            color: #6c1b78;
            margin-top: 5px;
        }

            .mzhu .keniHarruar:hover {
                color: #7a3185;
            }

        .klient .main .right .input .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #5D9AD3;
            height: auto;
            font-size: 13px;
        }

        .oshe .main .right .input .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #ff9e19;
            height: auto;
            font-size: 13px;
        }

        .vodafone .main .right .input .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #ed1b24;
            height: auto;
            font-size: 13px;
        }

        .moh .main .right .input .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #ed1b24;
            height: auto;
            font-size: 13px;
        }

        .mzhu .main .right .input .keniHarruar {
            text-align: left;
            display: block;
            font: medium;
            font-weight: bold;
            color: #6c1b78;
            height: auto;
            font-size: 13px;
        }

        .klient .main .right .input .keniHarruar:hover {
            color: #75a9da;
        }

        .oshe .main .right .input .keniHarruar:hover {
            color: #ffac3b;
        }

        .vodafone .main .right .input .keniHarruar:hover {
            color: #f03d45;
        }

        .moh .main .right .input .keniHarruar:hover {
            color: #f03d45;
        }

        .mzhu .main .right .input .keniHarruar:hover {
            color: #7a3185;
        }

        .mzhu #Login1_lblGjuhaEN {
            content: url(images/FaqjaPare/united_kingdom_round_icon_64.png);
            height: 25px;
            width: 25px;
        }

            .mzhu #Login1_lblGjuhaEN:hover {
                content: url(images/FaqjaPare/united_kingdom_round_icon_64.png);
                height: 28px;
                width: 28px;
            }

        .mzhu #Login1_lblGjuhaAL {
            content: url(images/FaqjaPare/albania_round_icon_64.png);
            height: 25px;
            width: 25px;
        }

            .mzhu #Login1_lblGjuhaAL:hover {
                content: url(images/FaqjaPare/albania_round_icon_64.png);
                height: 28px;
                width: 28px;
            }

        .klient {
            background-image: url(images/FaqjaPare/keshBCG.jpg);
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .oshe {
            background-image: url(images/FaqjaPare/osheBCG.jpg);
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .vodafone {
            background-image: url(images/FaqjaPare/vodBCG.png);
            background-position: top left;
            background-repeat: no-repeat;
        }

        .moh {
            background: url(images/FaqjaPare/mohBcgBlur.jpg);
            background-position: top left;
            background-size: cover;
            background-repeat: no-repeat;
        }

        .mzhu {
            background-image: url(images/FaqjaPare/mzhuBCG.jpg);
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

        .kesh .programText {
            font-family: Tahoma;
            color: #1478b4;
            font-weight: bold;
            font-size: xx-large;
            display: inline-block;
            margin: 0;
        }

        .kesh .main .submain {
            width: 340px;
            margin-top: 0px;
        }

        .kesh .main .content {
            border: 0;
            border-bottom: none;
            padding: 0;
            margin: 0;
            clear: both;
        }

        .kesh footer {
            display: none;
        }

        .kesh .main .logoKesh {
            background-image: url(images/FaqjaPare/LogoKESH2.png);
            background-repeat: no-repeat;
            float: left;
            position: relative;
            width: 82%;
            height: 121px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            display: inline;
            border-bottom: 1px solid;
            border-bottom-color: #d9008c;
        }

        .kesh .main .logoKesh2 {
            background-image: url(images/FaqjaPare/LogoKESH3.png);
            background-repeat: no-repeat;
            float: left;
            position: relative;
            width: 82%;
            height: 40px;
            margin-bottom: 10px;
            border: none;
            -moz-box-sizing: border-box;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            display: inline;
        }

        .kesh .mainContent .left {
            display: none;
        }

        .kesh .mainContent .right {
            float: none;
            display: inline-block;
            margin: auto;
        }

        .kesh .main .mylink {
            color: #6e6e6e;
        }

            .kesh .main .mylink:hover {
                color: #cd379b !important;
            }

        .kesh .main .input .hyrje {
            color: white;
            background-color: #1478b4;
            height: 40px;
            width: 100%;
            max-width: 246px;
            padding-left: 1px;
        }

            .kesh .main .input .hyrje:hover {
                background-color: #4293c3;
            }

        .kesh .main .input .keniHarruar {
            text-align: center;
            display: block;
            font: medium;
            font-weight: bold;
            color: #1478b4;
            height: auto;
            font-size: 13px;
            margin-top: 5px;
        }

        .kesh .main .input .keniHarruarLabel {
            text-align: center;
            display: block;
            font: medium;
            font-weight: bold;
            color: #6e6e6e;
            height: auto;
            font-size: 13px;
        }

        .kesh .main .input .keniHarruar:hover {
            color: #4293c3;
        }

        .kesh .main .input .keniHarruarLabel:hover {
            color: #6e6e6e;
        }
        .kesh {
            background-image: url(images/FaqjaPare/backgroundKESH.jpg);
            background-position: center;
            background-repeat: no-repeat;
            background-size: cover;
        }

            .kesh .login {
                display: block;
                padding: 40px;
                margin: 0;
                background-color: #b8c7e7 !important;
                border-radius: 2px;
                border: solid 1px #1478b4 !important;
                box-shadow: none !important;
                border-collapse: separate !important;
            }

        .gjuhetKesh {
            float: right;
            height: 120px;
            max-width: 18%;
            position: relative;
            width: 18%;
            display: inline;
            border-bottom: 1px solid;
            border-bottom-color: #d9008c;
        }

        .gjuhaKesh {
            float: right;
            margin-right: 10px;
            margin-top: 27px;
            position: relative;
            top: 50%;
            transform: translateY(-50%);
            overflow: hidden;
        }

        input:-webkit-autofill {
            -webkit-box-shadow: 0 0 0 1000px white inset !important;
        }

        @media all and (max-width:1450px) {
            .vodafone .main {
                margin-left: 8%;
            }

            .moh .main {
                margin-left: 8%;
            }
        }

        @media all and (max-width:760px) {
            .login {
                padding: 20px;
            }

            .infoLbl {
                max-width: 250px;
            }

            .ndihmeKontakt, .kosove, .vizitori {
                font-size: 11px;
            }
        }
        @media only screen and (max-width:1020px) {
            .fixed-action-btn{
                height: max-content;
                width: max-content !important;
                left: 0;
                margin: 0px auto;
            }
             .fixed-action-btn{
                width:100%;
                right: 0px;
            }
            .fixed-action-btn a{
                width: max-content;
                margin:auto;
            }
            #Login1_loginButton{
                width:100% !important;
            }
            .main{
                margin-right:0px !important;
                width:100%;
                }

            .BlogUrl {
                height:100%;
                width:100%;
            }
            .tblbuttondiv {
                display: none;
                z-index:999;
                text-align:center;
                position:absolute;
                top:5px;
                width:250px;
                height:50px;
                left: calc(50% - 125px);
                background-color:#1e73be;
            }

            .toggleblog { 
                text-align:center;
                border-color:white;
                font-size:30px;
                z-index:999;
                color:white;
                font-family:hg-grotesk,-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "Helvetica Neue", Arial, sans-serif, "Apple Color Emoji", "Segoe UI Emoji", "Segoe UI Symbol", "Noto Color Emoji";
                font-weight: 400;
                letter-spacing: -0.025em;
                text-transform: none;
                padding-top:5px;
            }

            .main {
                width: 100%;
                height: 100%;
                position: relative;
                right:0;
                text-align:center;
            }

            .blog-container {
                width: 100%;
                height: 100%;
                position: absolute;
                display: block;
            }
            .main {
                width:100%;
                text-align:center;
            }
            .labelInfo {
                padding-top: 20px;
                padding-bottom: 20px;
                text-align: center;
                display: block;
                height: auto;
            }
            .mainContent {
                display:inline-block;
            }

        }
        @media only screen and (min-width:1020px) {
                        .fixed-action-btn{
                height: max-content;
            }
            .blog-container {
                position: absolute;
                width: 100%;
                height: 100%;
            }
            .toggleblog {
                display:none;
            }
            .BlogUrl {
                width:100%;
                height:100%;
            }
            .main {
            float:right;
            width: 200px;
            margin:0;
            text-align: center;
            }

        }

        @media all and (max-width:450px) {
            .main {
                width: 100%;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }

            .gjuhet {
                height: 42.13px;
            }

            .gjuhetKesh {
                height: 42.13px;
            }

            /*.submain {
                width: 100%;
                margin-top: 40px;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }*/

            .mainContent {
                width: 100%;
                -moz-box-sizing: border-box;
                -webkit-box-sizing: border-box;
                box-sizing: border-box;
            }
        }

        @media all and (min-width:690px) {
            .info, .loginForm {
                vertical-align: middle;
            }
            

        }

        @media all and (min-width:1020px) and (max-width:1550px) {
            .fixed-action-btn{
                margin-right: 30%;
            }
        }

        @media all and (max-width:690px) {
            .google-div{
                margin-bottom:10%;
            }
            .left{
                margin-left: -15%;
            }
            .left, .right, .info {
                float: none;
                display: inline-block;
            }

            .left, .right {
                height: auto;
            }

            .loginForm, .info {
                margin-bottom: 20px;
            }

            .input {
                padding-top: 0;
                padding-bottom: 0;
            }

            .mainContent {
                margin-top: 20px;
            }

            .info {
                padding-top: 0;
                margin-top: 20px;
            }
        }
                @media all and (min-width:1800px)
                {
                    .loginForm{
                        margin-top:-20%;
                    }
                }
        @media all and (min-width:1200px) and (max-width:1689px){
            #showlogin{
                left:-460px;

            }
            .main{
                margin: auto;
                float: none !important;
            }
        }
		        .privacy .privacy-nav {
  position: -webkit-sticky;
  position: sticky;
  top: 15px;
  background: rgb(76, 175, 80, 0.075);
  padding: 30px 0;
  display: flex;
  justify-content: center;
  font-family: "Muli", sans-serif;
  font-weight: 300;
  color: rgb(92, 107, 131);
}

.privacy .block {
  background: #000;
  padding: 40px 50px;
  text-align: justify;
}

.privacy .block .policy-item {
  padding-bottom: 40px;
  text-align: justify;
}

.privacy .block .policy-item .title {
  margin-bottom: 20px;
  text-align: justify;
}

.privacy .block .policy-item .title h3 {
  border-bottom: 1px solid #cccccc;
  padding-bottom: 15px;
  text-align: justify;
}

.privacy .block .policy-item .policy-details p {
  margin-bottom: 40px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .privacy-nav ol {
  margin-bottom: 20px;
 
}

.privacy .privacy-nav ol li {
   color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
  }

.privacy .privacy-nav ul {
  padding-left: 0;
  margin-bottom: 20;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .privacy-nav ul li {
  list-style: none;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .privacy-nav ul li a {
  font-size: 16px;
  padding: 10px 0;
  font-weight: bold;
  display: block;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
  margin-bottom: 20;
}

.privacy .privacy-nav ul li a.active {
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
  font-size: 16px;
  padding: 10px 0;
  font-weight: bold;
  display: block;
}

.privacy .block {
  background: #fff;
  padding: 40px 50px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .block .policy-item {
  padding-bottom: 40px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .block .policy-item .title {
  margin-bottom: 20px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .block .policy-item .title h3 {
  border-bottom: 1px solid #cccccc;
  padding-bottom: 15px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.privacy .block .policy-item .policy-details p {
  margin-bottom: 20px;
  text-align: justify;
  color: rgb(92, 107, 131);
  font-family: "Muli", sans-serif;
}

.policy-details ul {
  margin-bottom: 20px;
  text-align: justify;
  color: #556b2f;
  font-family: "Muli", sans-serif;
}

.policy-details ul li {
  text-align: justify;
  color: #556b2f;
  font-family: "Muli", sans-serif;
}

.toggleblog {
    border:0px;
    background-color:transparent;
    z-index:999;
    font-family: "Muli", sans-serif;
    text-shadow: rgba(0,0,0,.01) 0 0 1px;
    margin:auto;
}
.loadingspinner {
    left: 0;
    position: absolute;
    top: 0;
    height: 100%;
    width: 100%;
    align-items: center;
    display: flex;
    justify-content: center;
}
.footer{
    display:none;
}
footer{
    display:none;
}
#comboBox{
    margin-bottom: 20px !important;
}
#Login1_cmbServerat_I{
    margin-bottom:0px;
}
#Login1_UserName_I{
        margin-bottom:0px;
}
#Login1_Password_I{
    margin-bottom:0px;
}
.inputUsername{
    margin-bottom: 20px !important;
}
.inputPassword{
    margin-bottom:20px !important;
}
.header{
    background-color: transparent;
}
.submain{
    display:none;
    z-index:99999;
}
label#Login1_PasswordRecoveryLink{
    font-family: 'hk groteks' ;
    cursor: pointer;
    margin-left: 90px;
    margin-top: 20px !important;
}
label#Login1_PasswordRecoveryLink:hover{
    color: #1251b8
}
.gjuhet{
    margin-top:20px;
}
.info-imb{
    margin-top: 20px;
    text-align: center;
}
.info-imb a{
    display:block;
    color:white;
    margin-top:5px;
    text-decoration:none;
    font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif
}
.info-imb a:hover{
    color: #1251b8;
}
a#Login1_lblGjuhaEN:hover{
    color: #1251b8;
}
.copyright{
    margin-top: 15px;
    text-align:center;
    color:white;
    user-select: none;
    font-family: "Segoe UI", Helvetica, "Droid Sans", Tahoma, Geneva, sans-serif
}
/*.hyrje{
    border-radius: 7%;
}*/
#maintest{
    transition: 0.2s all linear;
        position: absolute;
    /* top: 50%; */
    /* bottom: 50%; */
    /* transform: translate(-50%, -50%); */
    width: 100%;
    height: 100%;
    position: absolute;
    top: 50%;
    /* bottom: 50%; */
    left: 50%;
    transform: translate(-50%, -50%);
    z-index: 9999999999999;
    opacity: 1;
    position: fixed;
    /* display: flex; */
    /* margin: auto; */
/*    backdrop-filter: blur(5px);*/
}
.blur{
        transition: 0.2s all linear;
        position: absolute;
    /* top: 50%; */
    /* bottom: 50%; */
    /* transform: translate(-50%, -50%); */
    backdrop-filter: blur(5px);
    width: 100%;
    height: 100%;
    position: absolute;
    top: 50%;
    /* bottom: 50%; */
    left: 50%;
    transform: translate(-50%, -50%);
    background: #ffffff24;
    opacity: 1;
    position: fixed;
    /* display: flex; */
    /* margin: auto; */
    backdrop-filter: blur(5px);
}
section#submain header {
    position: absolute;
}
#toast-container{
    z-index: 9999999999999999999 !important;
}
.custom-css-a{
    width:100%;
    height:100%;
    background-color:#1251b8 !important;
}
.custom-css-a:hover{
    background-color: white !important;
    color: black !important;
}
.toast-container-login{
    cursor:pointer;
}


    </style>
    
    <script>

        function getClientDate() {
            var date = new Date();
            var dt = date.getDate();
            var m = date.getMonth();
            var year = date.getFullYear();
            var hours = date.getHours();
            var min = date.getMinutes();
            var sec = date.getSeconds();
            var dateStr = (m + 1) + "/" + dt + "/" + year + " " + hours + ":" + min + ":" + sec;
            clientDate.Set('clientDate', dateStr);
        }

        function makeVisibleLogin(s, e) {
            if (document.getElementById('authForm').className == 'klient')
                s.GetMainElement().style.backgroundColor = "#5D9AD3";
            s.SetVisible(true);
        }

        function merrUsername(s, e) {
            var username = txtUserName.GetText();
            if (username == null || username == "") {
                var toastHTML = '<span>Ju lutem vendosni perdoruesin !</span>';
                M.toast({ html: toastHTML, classes: 'rounded red darken-2' });
                txtUserName.SetIsValid(false);
                return;
            }
            var url = window.location.href;
            url = replaceQueryString(url, "harroPw", 1);
            url = replaceQueryString(url, "user", username);
            window.location.href = url;
        }
        function replaceQueryString(url, param, value) {
            var re = new RegExp("([?|&])" + param + "=.*?(&|$)", "i");
            if (url.match(re))
                return url.replace(re, '$1' + param + "=" + value + '$2');
            else {
                var isQuestionMarkPresent = url && url.indexOf('?') !== -1,
                    separator = '';
                separator = isQuestionMarkPresent ? '&' : '?';
                return url + separator + param + "=" + value;
            }
        }
        function IsOldIE() {
            return ASPxClientUtils.ie && (ASPxClientUtils.browserMajorVersion < 9);
        }



        function txtPassword_Init(s, e) {
            var elem = s.GetInputElement();
            elem.placeholder = loginHiddenField.Get("passlbl");
        }

        function txtUsername_Init(s, e) {
            var elem = s.GetInputElement();
            elem.placeholder = loginHiddenField.Get("userlbl");
            if (s.GetText() == "") {
                s.SetFocus();
            }
            else {
                loginButton.SetFocus();
            }
        }

        function IsNotNullOrEmpty(stringu) {
            return stringu && stringu != undefined && stringu != "undefined" && stringu != "";
        }

        function checkExistUrl() {
            if ((getUrlVar('organizata') && getUrlVar('organizata') != "undefined" && getUrlVar('organizata') != '' &&
                getUrlVar('gjuha') && getUrlVar('gjuha') != "undefined" && getUrlVar('gjuha') != '')
                || (getUrlVar('arsye') && getUrlVar('arsye') != "undefined"))
                return true;
            return false;
        }

        function cmbServerInit(s, e) {

            var input = s.GetInputElement();

            input.placeholder = loginHiddenField.Get("srvlbl");
            var lastSelectedItem = localStorage.getItem("server_key");
            if (IsNotNullOrEmpty(getUrlVar('organizata')) && IsNotNullOrEmpty(getUrlVar('gjuha'))) {
                document.getElementById("Login1_OrganisationSwitch").style.display = "block";
                //document.getElementById("comboBox").style.display = "none";
                if (cmbServerat.FindItemByText(getUrlVar('organizata')) === null)
                    cmbServerat.SetSelectedItem(null);
                else {
                    cmbServerat.SetSelectedItem(cmbServerat.FindItemByText(getUrlVar('organizata')));
                    localStorage.setItem("server_key", JSON.stringify({ value: cmbServerat.GetValue(), text: cmbServerat.GetText() }));
                }
            }
            else {
                if (IsNotNullOrEmpty(getUrlVar('arsye')) || IsNotNullOrEmpty(getUrlVar('ReturnUrl'))) {
                    document.getElementById("Login1_OrganisationSwitch").style.display = "block";
                    //    document.getElementById("comboBox").style.display = "none";
                }
                if (lastSelectedItem != null) {
                    try {
                        lastSelectedItem = JSON.parse(lastSelectedItem);
                    }
                    catch (e) {
                        lastSelectedItem = { text: 'Kryesor', value: 0 };
                        localStorage.setItem("server_key", lastSelectedItem);
                    }
                    var item = lastSelectedItem.value != undefined && cmbServerat.FindItemByValue(lastSelectedItem.value);
                    if (!lastSelectedItem.value) {
                        if (cmbServerat.FindItemByText('Kryesor'))
                            cmbServerat.SetSelectedItem(cmbServerat.FindItemByText('Kryesor')); //kryesori eshte default nga sp T_LICENCA_merrLicencatMeDbMeFilter
                        else
                            cmbServerat.SetSelectedIndex(cmbServerat.AddItem('Kryesor', '0'));

                        return;
                    }
                    else {
                        if (!cmbServerat.FindItemByValue(lastSelectedItem.value))//kur ka last selected item por nuk eshte ne dt filestar, e shtojme 
                            cmbServerat.SetSelectedIndex(cmbServerat.AddItem(lastSelectedItem.text, lastSelectedItem.value));
                        else
                            cmbServerat.SetSelectedItem(item);
                        return;
                    }
                }
            }
        }

        function cmbServerSelectedChanged(s, e) {
            var item = cmbServerat.GetSelectedItem();
            if (item == undefined) return;
            localStorage.setItem("server_key", JSON.stringify({ value: item.value, text: item.text }));
        }

        function TermsOk() {

            var terms = window.parent.document.getElementById("hfTerms").value;
            localStorage.setItem("TermsAccept", terms);

        }

        function tbl() {
            var x = document.getElementById("bc");
            if (x.style.display != "none") {
                x.style.display = "none";
                document.querySelector(".header").style.backgroundColor = "#0081db";
                document.querySelector(".header").style.width = "100%";
                document.querySelector("#maintest").style.width = "100%";
                document.querySelector("#maintest").style.removeProperty("margin-right");
                document.querySelector("#maintest").style.marginRight = "0px !IMPORTANT";
                document.querySelector(".logo").style.backgroundSize = "cover";
                document.querySelector(".mainContent").style.marginLeft = "-60%";

                document.getElementById("toggleblog").style.display = "none";
                document.getElementById("maintest").style.display = "block";
                document.getElementById("tblbd").style.display = "none";
                window.history.pushState("shl", "showlogin", "#showLogin");
                window.addEventListener("popstate", detecthistorychange);
            }
            flag = true;
        }


        function detecthistorychange() {
            document.getElementById("toggleblog").style.display = "inline-block";
            document.getElementById("bc").style.display = "block";
            document.getElementById("maintest").style.display = "none";
            document.getElementById("tblbd").style.display = "block";
        }

        function initLabelInfo(s, e) {
            //if (s.GetText().length > 0)
            //    s.SetVisible(true);
        }
        function frameLoaded() {
            document.querySelector(".fixed-action-btn").style.display = "block";
            document.getElementById("submain").style.display = "block";

        }
        function showLogin() {
            document.querySelector("#maintest").style.zIndex = "99999"
            document.querySelector("#maintest").style.opacity = "1"

        }
        function hideLogin() {
            document.querySelector("#maintest").style.opacity = "0"
            document.querySelector("#maintest").style.zIndex = "-1"

        }
        //function imagesInit(s, e) {
        //    if (document.getElementById('authForm').className == 'klient')
        //        s.SetImageUrl("/images/FaqjaPare/keshLogo.png");
        //    s.SetVisible(true);
        //}

        $(document).ready(function () {

            const url = new URL(window.location.href);
            if (url.search.includes("confirmEmail")) M.toast({ html: "<p style='color:white;'>Email-i u verifikua, logohuni me gmail per te vazhduar!</p>", classes: 'blue darken-2' });
            if (url.search.includes("linkedEmail")) M.toast({ html: "<p style='color:white;'>Email-i u verifikua, logohuni me gmail per te vazhduar!</p>", classes: 'blue darken-2' });
        });
        function shfaqHapaPerLogim() {
            $("#panelDiv").css("display", "none");
            window.open('https://alphawiki.notion.site/Aktivizo-Sign-in-me-Gmail-n-Alpha-28e7a1b0dc714bdc8fa076bc90530f74', '_blank');
        }
    </script>
    <meta charset="UTF-8">

</head>

<body onload="load()">
    <div class="loader-overlay" id="loader-overlay"></div>
<div class="loader" id="loader">
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
    <asp:Panel runat="server" ID="panelDiv" Visible="false">
        <div onClick="shfaqHapaPerLogim()" id="toast-container" class="toast-container-login" style="
    /* background-color: red; */
"><div class="toast" style="cursor:pointer; top: 0px;opacity: 1;background-color: red;">Email-i juaj nuk eshte i lidhur me ndonje ndermarrje. Ndiqni keto hapa per te mesuar si te aktivizoni login me gmail
</div></div></asp:Panel>
    <div class="fixed-action-btn" style="bottom:0px !important;display:none; top:-5px; z-index:9999;" >
        <a onclick="showLogin()" id="pulse-btn" class="btn pulse" style="border-radius:5px; background-color: #2d7ae1; display:flex;">Hyr ne alpha
    
        <i class="large material-icons" style="padding-left: 10px;">account_circle</i>
        </a>
    </div>
    <form id="authForm" runat="server" style="width: 100%">
        <dx:ASPxHiddenField ID="hasAlpha" ClientInstanceName="hasAlpha" runat="server">
        </dx:ASPxHiddenField>
        <dx:ASPxHiddenField ID="clientDate" ClientInstanceName="clientDate" runat="server">
        </dx:ASPxHiddenField>
        <asp:HiddenField ID="step1Complete" Value="false" runat="server"></asp:HiddenField>
        <div  id="bc" class="blog-container">
<%--            <div class="loadingspinner" id="loadingspinner"><img src="images/FaqjaPare/GIFWEB_BLUE_1.svg" onerror="this.style.display='none'"/></div>    --%>
            <iframe src="https://alphablog.al/" class="BlogUrl" id="blogUrl" onload="frameLoaded()"frameBorder="0" style="opacity:0;">
                </iframe>
                </div>
        <div class="tblbuttondiv" id="tblbd">    
            <button id="toggleblog" class="toggleblog" type="button" onclick="tbl()">Hyr ne Alpha</button></div>
        <div class="main" id="maintest" style="z-index:-1;opacity:0;">
            <div class="blur">

            </div>
            <header class="header">
                <div class="logo"></div>
                <a onclick="showLogin()" id="showlogin"class="waves-effect waves-light btn-large pulse" style="
    margin-top: 2px;
    margin-left: 190px; background-color: #2860b2; opacity:0; z-index:-999; transition: .5s ease-in-out;
">Logohu</a>
            </header>
            <header class="headerKesh" style="display: none">
                <div class="logoKesh"></div>
                <section class="gjuhetKesh">
                    <div id="Div3Kesh" class="gjuhaKesh" runat="server">
                        <dx:ASPxHyperLink ID="lblGjuhaENKesh" CssClass="mylink" Font-Underline="false" runat="server" Text="ENGLISH" Font-Size="11pt">
                        </dx:ASPxHyperLink>
                    </div>
                    <div class="gjuhaKesh" runat="server">
                        <dx:ASPxLabel CssClass="mylink" Font-Underline="false" runat="server" Text="|" Font-Size="11pt">
                        </dx:ASPxLabel>
                    </div>
                    <div class="gjuhaKesh" runat="server" id="Div4Kesh">
                        <dx:ASPxHyperLink ID="lblGjuhaALKesh" CssClass="mylink" NavigateUrl="login.aspx?gjuha=AL" Font-Underline="false" runat="server" Text="SHQIP" Cursor="pointer"
                            Font-Size="11pt">
                        </dx:ASPxHyperLink>
                    </div>
                    <div class="gjuhaKesh" runat="server">
                        <dx:ASPxLabel CssClass="mylink" Font-Underline="false" runat="server" Text="|" Font-Size="11pt">
                        </dx:ASPxLabel>
                    </div>
                    <div class="gjuhaKesh" runat="server" id="Div2">
                        <dx:ASPxHyperLink ID="lblGjuhaFRKesh" CssClass="mylink" NavigateUrl="login.aspx?gjuha=FR" Font-Underline="false" runat="server" Text="FRANCAIS" Cursor="pointer"
                            Font-Size="11pt">
                        </dx:ASPxHyperLink>
                    </div>
                </section>
                <div class="logoKesh2"></div>
                <p class="programText">PROGRAMI I ADMINISTRIMIT FINANCIAR</p>
            </header>
            <section class="submain" id="submain">

                <header>
                    <dx:ASPxLabel ID="LabelInfo" ClientInstanceName="LabelInfo" CssClass="labelInfo" runat="server" Style="color: #FF0000; font-size: medium; font-weight: bold; font-family: Calibri">
                        <ClientSideEvents Init="initLabelInfo" />
                    </dx:ASPxLabel>
                    <%--<dx:ASPxLabel ID="lblCapsLock" ClientInstanceName="lblCapsLock" runat="server" Style="display: none; color: #FF0000; font-size: medium; font-weight: bold; font-family: Calibri"></dx:ASPxLabel>--%>
                </header>
                <section class="mainContent">
                    <section class="loginForm">

                        <asp:Login ID="Login1" CssClass="login" runat="server" LoginButtonType="Image"
                            BorderStyle="Solid" Font-Size="0.8em"
                            ForeColor="#333333" OnAuthenticate="Login1_Authenticate" FailureText="Perdoruesi ose fjalekalimi eshte i pasakte, ju lutem provojeni perseri."
                            FailureText2="Otp nuk eshte e sakte."
                            LoginButtonText="Hyrje" PasswordLabelText="Fjalekalimi:" PasswordRequiredErrorMessage="Duhet te jepet fjalekalimi."
                            Login2ButtonText="Hyrje"
                            TitleText="Hyrje ne Sistem"
                            UserNameLabelText="Perdoruesi:" UserNameRequiredErrorMessage="Duhet te jepet perdoruesi."
                            DestinationPageUrl="FaqeKryesore.aspx" EnableTheming="True" TextLayout="TextOnLeft"
                            PasswordRecoveryText="Harruar Fjalekalimin?" PasswordRecoveryUrl="LoginFail.aspx?harroPw=1&user='<%# Eval(Login1.UserName) %>'"
                            InstructionText="Nuk eshte aktivizuar" LoginButtonImageUrl="~/images/FaqjaPare/loginbutton.png">
                            <LayoutTemplate>
                                <section runat="server" id="login_div">

                                    <div id="comboBox" class="input" style="height: auto; max-height: 40px; padding-top: 0px; top: 0px; left: 0px; ">
									<dx:ASPxComboBox ID="cmbServerat" ButtonStyle-HoverStyle-BackColor="#5D9AD3"
                                            IncrementalFilteringMode="Contains" DropDownStyle="DropDownList"
                                            ItemStyle-SelectedStyle-BackColor="#5D9AD3" Border-BorderColor="#999999" 
                                            Paddings-Padding="0" Width="100%" Height="40px" Font-Size="16px" Theme="Metropolis"
                                            ClientInstanceName="cmbServerat" runat="server" EnableSynchronization="True" >
                                            <ClientSideEvents Init="cmbServerInit" SelectedIndexChanged="cmbServerSelectedChanged" />
                                            <ValidationSettings ValidationGroup="Login1" RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                        </dx:ASPxComboBox>
                                    </div>
                                    <div class="input inputUsername">
                                        <dx:ASPxTextBox ID="UserName" ClientInstanceName="txtUserName" Width="100%" Height="40px" Border-BorderColor="#999999" Border-BorderStyle="Solid" Font-Size="16px" runat="server" Paddings-Padding="0" Theme="Metropolis">
                                            <ClientSideEvents Init="txtUsername_Init" />
                                            <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input inputPassword">
                                        <dx:ASPxTextBox ID="Password" ClientInstanceName="txtPassword" Password="true" runat="server" Font-Size="16px" Border-BorderStyle="Solid" Theme="Metropolis"
                                            Paddings-Padding="0" Height="40" Width="100%" Border-BorderColor="#999999">
                                            <ClientSideEvents Init="txtPassword_Init" />
                                            <ValidationSettings RequiredField-IsRequired="true" ValidationGroup="Login1" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom"></ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input LoginError ">
                                        <dx:ASPxLabel ID="FailureText" ForeColor="Red" runat="server"></dx:ASPxLabel>
                                    </div>
                                    <%--<div class="input" style="height: auto; display: none;">
                                        <dx:ASPxLabel ID="PasswordRecoveryLabel" CssClass="keniHarruarLabel" runat="server" Text="Keni harruar fjalekalimin?" AssociatedControlID="UserName">
                                        </dx:ASPxLabel>
                                    </div>
                                    <div class="input" style="height: auto">
                                        <dx:ASPxLabel ID="PasswordRecoveryLink" CssClass="keniHarruar" runat="server" Cursor="pointer" Text="Keni harruar fjalëkalimin?" AssociatedControlID="UserName" ClientSideEvents-Click="function(s,e){merrUsername(s,e)}">
                                        </dx:ASPxLabel>
                                    </div>--%>
                                    <div class="col s12 m6 offset-m3 center-align google-div" style="margin-top:10%;">
                                        <a class="oauth-container btn darken-4 white white-text custom-css-a" style="text-transform:none">
                                            <div class="left" style="margin-top:0px; height:20px; position:absolute;">
                                                <img width="20px" style="margin-top:7px; margin-right:8px" alt="Google sign-in" class="cutom-css-img"
                                                    src="images/FaqjaPare/google.png" />
                                            </div>
                                            Sign in with Google
                                        </a>
                                    </div>
                                    <div class="input" style="display: flex; justify-content: center;">
                                        <%-- style="padding-top:10px;max-width:246px;padding-left:1px;"--%>
                                        <dx:ASPxButton runat="server" Height="40px" Width="100%" ClientVisible="false" ClientInstanceName="loginButton" CssClass="hyrje"
                                            CommandName="Login"
                                            ValidationGroup="Login1"
                                            ID="loginButton"
                                            Theme="Metropolis"
                                            Border-BorderStyle="None"
                                            HorizontalAlign="Center"
                                            AllowFocus="False"
                                            Font-Size="17px" Font-Bold="true">
                                            <ClientSideEvents Click="function(s,e){getClientDate();}"
                                                Init="function(s,e){makeVisibleLogin(s,e);}" />

                                        </dx:ASPxButton>
                                        
                                        

                                    </div>
                                    
                                    
                                    
                                    <%--<h1 class="hyrje google"><img src="images/gogle.png"/>Log in with Google</h1>--%>
                                    <section>  
                                        <%--<a class="material-icons waves-effect waves-light btn-small pulse" id="hidelogin" style="
                                    margin-left: 80%;
                                    background-color: rgb(40, 96, 178);
                                    padding-top: 5px;
                                    left: auto;
                                    margin-top: -12%;
                                
                                " onclick="hideLogin()">arrow_upward</a>--%>
                                        </section>
                                    <dx:ASPxHiddenField runat="server" ID="loginHiddenField" ClientInstanceName="loginHiddenField"></dx:ASPxHiddenField>
                                    <%-- <dx:ASPxLabel runat="server" CssClass="input" ID="lblHyrje" Text="Hyrje në Sistem" Font-Names="Arial" Font-Size="Large"></dx:ASPxLabel>

                                    --%>
                                </section>
                                    <div id="OrganisationSwitch" style="text-align: center; display: none; font: medium; font-weight: normal; color: white; height: auto; font-size: 14px; margin-top: 30px;" runat="server">
                                        <dx:ASPxHyperLink ID="NdryshoOrganizate" CssClass="mylink" ClientVisible="false" Font-Underline="false" runat="server" Text="Nderroni organizate" Font-Size="11pt" Cursor="pointer" name="NdryshoOrganizate"
                                            NavigateUrl="https://app.alphaweb.al/?clearOrg=true" onClick="clickSwitchOrganisation()">
                                        </dx:ASPxHyperLink>
                                    </div>
                                    <div class="input" style="height: auto; display: none;">
                                        <dx:ASPxLabel ID="PasswordRecoveryLabel" CssClass="keniHarruarLabel" runat="server" Text="Keni harruar fjalekalimin?" AssociatedControlID="UserName">
                                        </dx:ASPxLabel>
                                    </div>
                                    <div class="input" style="height: auto">
                                        <dx:ASPxLabel ID="PasswordRecoveryLink" CssClass="keniHarruar" runat="server" Cursor="pointer" Text="Keni harruar fjalëkalimin?" AssociatedControlID="UserName" ClientSideEvents-Click="function(s,e){merrUsername(s,e)}">
                                        </dx:ASPxLabel>
                                    </div>
                                    <section class="gjuhet">
                                        <div id="Div3" class="gjuha" runat="server">
                                            <dx:ASPxHyperLink ID="lblGjuhaEN" CssClass="mylink" Font-Underline="false" runat="server" Text="EN" Font-Size="11pt">
                                            </dx:ASPxHyperLink>

                                        </div>
                                        <div class="gjuha" runat="server" id="Div4">
                                            <dx:ASPxHyperLink ID="lblGjuhaAL" CssClass="mylink" Font-Underline="false" runat="server" Text="AL" Cursor="pointer"
                                                Font-Size="11pt">
                                            </dx:ASPxHyperLink>
                                        </div>
                                        <div class="gjuha" runat="server" id="Div1">
                                            <dx:ASPxHyperLink ID="lblGjuhaFR" CssClass="mylink" Font-Underline="false" runat="server" Text="FR" Cursor="pointer"
                                                Font-Size="11pt">
                                            </dx:ASPxHyperLink>
                                        </div>
                                    </section>
                                    <section>
    
                                        <div class="info-imb">
                                            <a href="mailto:support@imb.al" onclick="function(){sendMail()}">Kontakto</a>
                                            <a href="" onclick="function(){demo(event)}">Demo</a>
                                            <a href="https://imb.al/alpha-kushtet-e-sherbimit" target="_blank">Kushtet e sh&euml;rbimit</a>
                                        </div>
                                        <div class="copyright">
                                            <p id="copyright">&#169</p>
                                        </div>
                                    </section>
                                    
                                

                                <section runat="server" id="otp_div"  visible="false">

                                    <div class="">
                                        <asp:Literal ID="Otp_qr" runat="server"></asp:Literal>
                                    </div>

                                    <br />
                                    <br />
                                    <br />
                                    <br />

                                    <div class="input inputUsername">
                                        <dx:ASPxTextBox ID="Kodi" ClientInstanceName="txtKodi" Width="100%" Height="40px" Border-BorderColor="#999999" Border-BorderStyle="Solid" Font-Size="16px" runat="server" Paddings-Padding="0" Theme="Metropolis">
                                            <ValidationSettings RequiredField-IsRequired="true" ErrorFrameStyle-Paddings-Padding="0" ErrorDisplayMode="None" SetFocusOnError="false" ErrorTextPosition="Bottom">
                                            </ValidationSettings>
                                        </dx:ASPxTextBox>
                                    </div>
                                    <div class="input LoginError ">
                                        <asp:label ID="otpError" ForeColor="Red" runat="server"></asp:label>
                                    </div>
                                    <p>
                                        Shkruani kodin tuaj OTP.
                                    </p>
                                <div class="input">
                                        <%-- style="padding-top:10px;max-width:246px;padding-left:1px;"--%>
                                        <dx:ASPxButton runat="server" Height="40px" Width="100%" ClientVisible="false" ClientInstanceName="loginButton2" CssClass="hyrje"
                                            CommandName="Login"
                                            ValidationGroup="Login1"
                                            ID="loginButton2"
                                            Theme="Metropolis"
                                            Border-BorderStyle="None"
                                            HorizontalAlign="Center"
                                            AllowFocus="False"
                                            Font-Size="17px" Font-Bold="true">
                                            <ClientSideEvents Click="function(s,e){getClientDate();}"
                                                Init="function(s,e){makeVisibleLogin(s,e);}" />

                                        </dx:ASPxButton>

                                    </div>
                                </section>
                                
                            </LayoutTemplate>

                            <ValidatorTextStyle Font-Size="Medium" />
                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                            <LabelStyle Font-Bold="False" Font-Size="Medium" ForeColor="#666666" Height="40px" />
                            <TitleTextStyle BackColor="#8FC74A" Font-Bold="True" Font-Size="Large" ForeColor="White"
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Top"
                                Wrap="False" Height="30" />
                            <HyperLinkStyle Font-Size="Small" Font-Underline="False" ForeColor="#666666" CssClass="styleFloatLeft" />
                        </asp:Login>
<dx:ASPxHiddenField ID="hfPeriudhKontabel" runat="server" ClientInstanceName="hfPeriudhKontabel">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>
            <asp:HiddenField ID="hfTerms" runat="server" />
                        <asp:HiddenField ID="hflocal" runat="server" />
                        <dx:ASPxHiddenField ID="ASPxHiddenLocal" runat="server" ClientInstanceName="hflocal">
            </dx:ASPxHiddenField>        
                                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal1" runat="server" AllowDragging="False" ClientInstanceName="popupUniversal1"
                EnableAnimation="False" EnableViewState="False" HeaderText="Kushtet e Sherbimit" ShowCloseButton="false"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" CloseAction="None" 
                Width="100%" Height="100%" ClientIDMode="AutoID" CssPostfix="Glass" Style="background-color: #EDF3F4; text-align :center">
                <HeaderStyle HorizontalAlign="Center">
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px"   />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" Width="600px" >
                                            <PanelCollection>
                                                <dx:PanelContent>
                                                   
                                                        
      
                                                           <iframe id="TOS" clientidmode="Static" scrolling="yes" frameborder="0" runat="server"
                            width="600" height="500" style="background-color: #EDF3F4" src="https://terms-of-service.imb.al/"></iframe>
                                                  
                                                   <dx:ASPxButton ID="ButtonOk"  runat="server" Text="Prano" CausesValidation="False" CssClass="button"   
                                            Border-BorderStyle="None"
                                            Theme="Metropolis"
                                            AllowFocus="False"
                                            Font-Size="17px" Font-Bold="true" OnClick="ButtonOk_Click" >
                                                  <ClientSideEvents Click="function(s, e){ TermsOk(); } " />    
                                                      
                                                       </dx:ASPxButton>
                                                        <dx:ASPxHiddenField ID="ASPxHiddenFieldTerms" runat="server" ClientInstanceName="hfTerms">
            </dx:ASPxHiddenField>          
                                                  
                                                </dx:PanelContent>
                                            </PanelCollection>
                                        </dx:ASPxPanel >
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
                    </section>
                </section>
            </section>

            <footer>
                <article class="cpyright">
                    <dx:ASPxLabel ID="ASPxLabel8" CssClass="cpyrightText" runat="server" Text="© 2015 IMB" />
                </article>
                <article class="cpyright">
                    <%-- --%>
                    <dx:ASPxHyperLink ID="ASPxLabelKontakto" CssClass="cpyrightLink" runat="server" Text="Kontakto" NavigateUrl="mailto:support@imb.al" Cursor="pointer"
                        Height="20" ClientSideEvents-Click="function(){sendMail()}">
                    </dx:ASPxHyperLink>

                    <dx:ASPxHyperLink ID="ASPxLabel11Demo" CssClass="cpyrightLink" runat="server" Text="Demo" Height="20" Cursor="pointer"
                        ClientSideEvents-Click="function(){demo(event)}">
                    </dx:ASPxHyperLink>
					<dx:ASPxHyperLink ID="ASPxHyperTerms" CssClass="cpyrightLink" runat="server"  Style=" color:#0187dd " Text="Kushtet e sherbimit" NavigateUrl="https://terms-of-service.imb.al/" 
                        Height="20" Target="_blank" >
						</dx:ASPxHyperLink>
                </article>
                <article class="cpyright">
                </article>
                        <asp:Button ID="logInWithGmailButton" runat="server" Text="" OnClick="logInWithGmail" />
                        <dx:ASPxTextBox ID="txtUID" runat="server"></dx:ASPxTextBox>
                        <dx:ASPxTextBox ID="txtFirstLogin" runat="server"></dx:ASPxTextBox>
                        
            </footer>
        </div>
    </form>
    <script>
        window.onload = function (e) {
            var termsAndServices = document.getElementById('TOS');
            if (termsAndServices.src != "https://terms-of-service.imb.al/") {
                termsAndServices.src = "https://terms-of-service.imb.al/";
            }
        };

        // var inputPass = txtPassword.GetInputElement();
        //inputPass.addEventListener("keyup", function (event) {
        //    if (event.getModifierState("CapsLock")) {                
        //        $("#lblCapsLock").style.display = "block"
        //    } else {                
        //        $("#lblCapsLock").style.display = "none"
        //    }
        //});
        function getUrlVars() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;

        }
        function clickSwitchOrganisation() {
            localStorage.removeItem("organisation");
        }

        function getUrlVar(name) {
            return decodeURIComponent(getUrlVars()[name]);
        }

        function load() {
            if (sessionStorage)
                sessionStorage.clear();

            var ua = window.navigator.userAgent;
            var msie = ua.indexOf("MSIE ");

            if (msie > 0 || !!navigator.userAgent.match(/Trident.*rv\:11\./)) {// If Internet Explorer, return version number
                var pass = txtPassword.GetInputElement();
                var txtUser = txtUserName.GetInputElement();
                var inputcmb = cmbServerat.GetInputElement();

                pass.setAttribute('placeholder', loginHiddenField.Get("passlbl"));
                txtUser.setAttribute('placeholder', loginHiddenField.Get("userlbl"));
                inputcmb.setAttribute('placeholder', loginHiddenField.Get("srvlbl"));

            } else                 // If another browser, return 0
            {
                //duhet hequr 
                if (typeof cmbServerat !== "undefined" && cmbServerat.GetVisible())
                    cmbServerat.Focus();
            }
            if (document.getElementsByTagName('body')[0].className == 'kesh') {
                document.querySelector('.keniHarruar').textContent = "<<Kliko Ketu>>";
                document.querySelector('.keniHarruarLabel').parentNode.style.display = 'block';
                document.querySelector('.header').style.display = 'none';
                document.querySelector('.gjuhet').style.display = 'none';
                document.querySelector('.headerKesh').style.display = 'block';
                if (getUrlVar('gjuha') == "EN") {
                    document.querySelector('.keniHarruarLabel').textContent = "Forgot your password";
                    document.querySelector('.keniHarruar').textContent = "<<Click here>>";
                }
                else {
                    document.querySelector('.keniHarruarLabel').textContent = "Keni harruar fjalekalimin?";
                    document.querySelector('.keniHarruar').textContent = "<<Kliko Ketu>>";
                }
            }
        };

        function sendMail() {
            window.open("https://direct.lc.chat/13799409/");
        }

        async function signInWithGooglePopup() {

            await signInWithPopup(auth, GoogleAuthProvider)
                .then(function (result) {
                    var logInWithGoogle = document.getElementById("logInWithGmailButton");
                    txtUID.SetText(result.user.uid);
                    logInWithGoogle.click();
                    return "";
                }).catch(function (err) {
                    // M.toast({ html: "<span>"+err+"</span>", classes: 'rounded red darken-2' });
                    // if(!second) signInWithGooglePopup(true);


                })

        }
        async function signInWithRedirectGoogle() {
            signInWithRedirect(getAuth(), new GoogleAuthProvider())
                .then(function (result) {
                    return getRedirectResult(auth);
                }).then(function (result) {
                    var logInWithGoogle = document.getElementById("logInWithGmailButton");
                    txtUID.SetText(result.user.uid);
                    logInWithGoogle.click();
                }).catch(function (err) {
                    M.toast({ html: "<span>" + err + "</span>", classes: 'rounded red darken-2' });
                })

        }
        function demo(event) {
            var x = document.getElementById('Login1_UserName_I');
            x.value = "Vizitor";
            var y = document.getElementById('Login1_Password_I');
            y.value = "vizitor";
            event.preventDefault();

        }

        const iframeEle = document.getElementById('blogUrl');
        iframeEle.addEventListener('load', function () {
            window.setTimeout(function () {
                document.getElementById("pulse-btn").classList.remove("pulse");
            }, 5000)
            document.getElementById('loader-overlay').style.display = 'none';
            document.getElementById('loader').style.display = 'none';
            iframeEle.style.opacity = 1;
            var html = document.getElementById("Login1_FailureText").innerHTML;
            var html2 = document.getElementById("LabelInfo").innerHTML;
            var toastHTML = '<span>' + html + '</span>';
            if (html !== "")
                M.toast({ html: toastHTML, classes: 'rounded red darken-2' });
            else if (html2 !== "") M.toast({ html: '<span>' + html2 + '</span>', classes: 'rounded red darken-2' });
        });

        $(".blur").on("click", (e) => {
            hideLogin();

        })
        function isSafariBrowser() {
            var is_chrome = navigator.userAgent.indexOf('Chrome') > -1;
            var is_safari = navigator.userAgent.indexOf("Safari") > -1;
            if (is_safari) {
                if (is_chrome)  // Chrome seems to have both Chrome and Safari userAgents
                    return false;
                else
                    return true;
            }
            return false;
        }
        if (navigator.userAgent.match(/(iPhone|iPod|iPad)/i) && isSafariBrowser()) {

            $(".google-div").on("click touchstart mouseenter focus", signInWithGooglePopup);

        }
        else {
            $(".google-div").on("click", signInWithGooglePopup);
        }
        window.onload = async function (e) {

            document.getElementById("copyright").innerHTML +=  + new Date().getFullYear()+" IMB"
            var sessionStorageValue = sessionStorage.getItem("GoogleLogInAttemps") == null ? 0 : parseInt(sessionStorage.getItem("GoogleLogInAttemps"));
            await getAuth();
            var url = new URL(window.location.href);
            var arsye = url.searchParams.get("arsye");
            if (arsye == "logout") auth.signOut();
            else {
                if (auth.currentUser != null && sessionStorageValue < 1) {
                    sessionStorage.setItem("GoogleLogInAttemps", sessionStorageValue + 1);
                    var logInWithGoogle = document.getElementById("logInWithGmailButton");
                    txtUID.SetText(auth.currentUser.uid);
                    txtFirstLogin.SetText("true");
                    logInWithGoogle.click();
                    document.getElementById('loader-overlay').style.display = 'block';
                    document.getElementById('loader-overlay').style.zIndex = 10000;
                    document.getElementById('loader').style.display = 'block';
                    document.getElementById('loader').style.zIndex = 10000;
                }
                else if (sessionStorageValue >= 1) {
                    auth.signOut();
                }
            }
            
            
            
        }
    </script>
       
</body>
</html>
