<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RestoreDatabase.aspx.cs" Inherits="PlatinumWeb.RestoreDatabase" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Restore Database</title>
        <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/css/materialize.min.css" />
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.2.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-Zenh87qX5JnK2Jl0vWa8Ck2rdkQ2Bzep5IDxbcnCeuOxjzrPF/et3URy9Bv1WTRi" crossorigin="anonymous" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/plugins/ui.multiselect.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.ui.datepicker-sq.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/customCombobox.js;~/js/myJQGrid-IMB.2.1.js;~/js/memoryObject.js;~/js/async.min.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/arkiva.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/fine-uploader/jquery.fine-uploader.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/toolbar.js;"
        type="text/javascript"></script>
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
    display: none;
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
    display: none;
    position: fixed;
    z-index: 999;
    margin: auto;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    background-color: rgba(255,255,255,0.7);
}
.modal{
    width: 30%;
    height: 30%;
}
.modal-content{
    border-radius: 0px;
}
.modal-cont{
    position: absolute;
    width: 100%;
    height: 100%;
    display:none;
}
.modal-footer a{
    text-decoration: none;
}
    </style>
</head>
<body>
  <!-- Modal Structure -->
    <div class="modal-cont" id="modal-cont">
  <div id="modal1" class="modal modal-fixed-footer">
    <div class="modal-content">
      <h4>Rikthim Databaze</h4>
      <p>Ju jeni duke rikthyer databazën me gjithë të dhënat e organizates tuaj ne gjendjën e datës së zgjedhur. Mbasi të shtypni po, fillimisht do të bëhet një backup i gjendjës së tanishme të databazës. Nese në të ardhmen deshironi, do të keni mundësi të ktheheni në gjendjën e tanishme. Jeni të sigurt që doni të vazhdoni?</p>
    </div>
    <div class="modal-footer">
      <a href="#!" class="modal-close waves-effect waves-green btn-flat" onclick="NdryshoDatbazen()">Po</a>
      <a href="#!" class="modal-close waves-effect waves-green btn-flat" onclick="hiqPopup()">Jo</a>
        </div>
  </div>
        </div>
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
        <nav>
            <div class="nav-wrapper" style="background-color: #0072c6 !important">
              <ul class="left hide-on-med-and-down">
                <li><a onclick="tregoPopup()">Kthe databazen ne gjendjen e zgjedhur</a></li>
                <li><a onclick="ShkarkoDatabazen()">Shkarko Databazen</a></li>
              </ul>
            </div>
        </nav>
        <div>
            <asp:DropDownList runat="server" ID="select" AppendDataBoundItems="true" CssClass="form-select">
                <asp:ListItem Text="Zgjidhni versionin" Value="" />
            </asp:DropDownList>
        </div>
    </form>
</body>
<script src="https://cdnjs.cloudflare.com/ajax/libs/materialize/1.0.0/js/materialize.min.js">
</script>
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.6.1/jquery.min.js" integrity="sha512-aVKKRRi/Q/YV+4mjoKBsE4x3H+BkegoM/em46NNlCqNTmUYADjBbeNefNxYV7giUp0VxICtqdrbqU7iVaeZNXA==" crossorigin="anonymous" referrerpolicy="no-referrer"></script>
<script>
    function NdryshoDatbazen() {
        hiqPopup();
        document.querySelector(".loader").style.display = "block";
        document.querySelector(".loader-overlay").style.display = "block";
        var generations = [];
        document.querySelectorAll("option").forEach(value => {
            generations.push(value.id);
        })
        $.ajax({
            type: "POST",
            url: Utils.getServerApiUrl("Rregjistrime", "restoreDatabase"),
            data: JSON.stringify({ uri: $("select option:selected").attr("name"), generations: generations }),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        }).done(function (response) {
            if (response.status = "SUCCESS") {
                document.querySelector(".loader").style.display = "none";
                document.querySelector(".loader-overlay").style.display = "none";
                $.ajax({
                    type: "POST",
                    url: Utils.getServerApiUrl("Rregjistrime", "destroySession"),
                    data: JSON.stringify({}),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json"
                }).done(function (response) {
                    if (response.status = "SUCCESS") {
                        console.log("success");
                        window.parent.location.reload();
                    } else {
                        console.log("FAILED");
                        window.parent.location.reload();
                    }
                }).fail(function (response) {
                    console.log("FAILED");
                    window.parent.location.reload();
                });
            } else {
                document.querySelector(".loader").style.display = "none";
                document.querySelector(".loader-overlay").style.display = "none";
                console.log("FAILED");
                window.parent.location.reload();
            }
        }).fail(function (response) {
            document.querySelector(".loader").style.display = "none";
            document.querySelector(".loader-overlay").style.display = "none";
            console.log("FAILED");
            window.parent.location.reload();
        });
    }

    function ShkarkoDatabazen() {
        window.parent.window.open($("select option:selected").val(), "_blank");
    }
    function tregoPopup() {
        document.getElementById("modal1").style.display = "block";
        document.getElementById("modal-cont").style.display = "block";
        document.getElementById("modal1").style.zIndex = "99999";
    }
    function hiqPopup() {
        document.getElementById("modal1").style.display = "none";
        document.getElementById("modal1").style.zIndex = "-99999";
        document.getElementById("modal-cont").style.display = "none";
    }

</script>
</html>
