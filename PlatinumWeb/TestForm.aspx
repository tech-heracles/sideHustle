<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestForm.aspx.cs" Inherits="PlatinumWeb.TestForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/Utils-IMB.2.1.js;~/JsGlobal.js;~/js/json2.js;~/js/srcCRM/js/jquery.mmenu.min.all.js;~/js/myNrAuto-IMB.2.1.js;~/js/aspx.js/CRMDefault.aspx-IMB.4.7.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    

        <script>

          

           /*form params*/
            function callWs()
            {
                $.ajax({
                    url: Utils.getServerUrlHost() + "/api/Konfigurime/ktheKonfigDB",
                    data: JSON.stringify({
                        idKomp: 506,
                        kodKonf: "FSHmag",
                        idNdermarrje: 481,
                        kodKontrollKlienti: "btnKlienti",
                        idKlienti: -1,
                        shtim: true,
                        merrFormatKursi: true,
                        idGjuha: 0
                    })
                    ,contentType: 'application/json'
                }).done(function (r) {

                    console.log(r);
                }).fail(function (e) {
                    console.log(e);
                });
            }


            /*json jobject*/
            function callWs2() {
                $.ajax({
                    url: Utils.getServerUrlHost() + "/api/Konfig/ktheKonfigDB2/",
                data:JSON.stringify({
                        idKomp: 506,
                        kodKonf: "FSHmag",
                        idNdermarrje: 481,
                        kodKontrollKlienti: "btnKlienti",
                        idKlienti: -1,
                        shtim: true,
                        merrFormatKursi: true,
                        idGjuha: 0
                }),
                contentType: 'application/json'
                }).done(function (r) {

                    console.log(r);
                }).fail(function (e) {
                    console.log(e);
                });
            }


            /*url params*/
            function callWs3() {
                $.ajax({
                    url: Utils.getServerUrlHost() + "/api/Konfig/ktheKonfigDB?"+$.param({
                        idKomp: 506,
                        kodKonf: "FSHmag",
                        idNdermarrje: 481,
                        kodKontrollKlienti: "btnKlienti",
                        idKlienti: -1,
                        shtim: true,
                        merrFormatKursi: true,
                        idGjuha: 0
                    }),
                    contentType: 'application/json'
                }).done(function (r) {

                    console.log(r);
                }).fail(function (e) {
                    console.log(e);
                });
            }

            function callPerdorues()
            {
                $.ajax({
                    url: Utils.getServerUrlHost() + "/api/Konfig/TestSession"
                }).done(function (r) {

                    console.log(r);
                }).fail(function (e) {
                    console.log(e);
                });
            }

        </script>



    </div>
    </form>
</body>
</html>
