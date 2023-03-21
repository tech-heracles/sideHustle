    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaturaBlerjeEinvoice.aspx.cs" Inherits="PlatinumWeb.FaturaBlerjeEinvoice" %>
<%@ Register Src="~/ucPopUpEinvoice.ascx" TagPrefix="ucPopUpEinvoice" TagName="ucPopUpEinvoice" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxnb" %>



<%@ Register Src="TimeoutControl.ascx" TagName="TimeoutControl" TagPrefix="uc1" %>
<!DOCTYPE html>

<html lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"> 
    <meta charset="UTF-8" />
    <title>Fatura e-invoice</title>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/async.min.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/toolbar.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/memoryObject.js;~/js/aspx.js/B_Shto_RegjistrimDokumentBuxheti.aspx-IMB.7.1.js&v76"
            type ="text/javascript"></script>    
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"
            integrity="sha256-/xUj+3OJU5yExlq6GSYGSHk7tPXikynS7ogEvDej/m4="
            crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"/>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.css"/>
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.js"></script>
    <link href="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.css" rel="stylesheet"/>
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/extensions/multiple-sort/bootstrap-table-multiple-sort.js"></script>
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/extensions/filter-control/bootstrap-table-filter-control.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous"/>
    
    
    

    <style>
        #table{
            border-collapse: collapse;
            width: 100%
        }
        .filter-control input {  
            width: 95% !important;   
            margin-left: 2.5%;  
           margin-bottom: 10px;
        }
        
        #table td {
           border-top: 1px solid #ddd;
            padding: 8px;
        }
       
        #table th {
            text-align: left;  
            padding-top: 12px;
            padding-bottom: 12px;
            color: black;
            position: static;
            text-align: center;
            
            
        }

        td:nth-child(even), th:nth-child(even) {
            background-color: none;
            
        }
        .hidden{
            display: none;
        }
        .pdf-preview{
            position:fixed;
            opacity: 0.8;
            border-radius: 6px;
            border: 0px solid teal;
            z-index: 9999999;
            overflow-y: hidden;
            height: 50%;
            width:50%;
        }
/*        .margin-nr div{
            margin-bottom: 50%;
        }*/
        .margin-status div{
            margin-bottom: 30%;
        }

        .gjenero{
            text-align:center;
        }
        .gjenero button{
            color: black;
            border-radius: 5%;
            border: none;
            /* font-size: 20px; */
            background-color: #0081db;
            padding: 5px 20px;
            color: white;
            transition: .2s ease-in-out;
            outline: none;
        }
        .gjenero button:hover{
            border: none;
            background-color:#0096ff;
        }
        .gjenero button:hover{
            border: none;
            background-color:#0096ff;
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
    </style>        
</head>
<body>
    <form runat="server">
        <asp:HiddenField ID="hfStateNdermarrje" runat="server" />
        <ucPopUpEinvoice:ucPopUpEinvoice runat="server" ID="ucPopUpEinvoice"/>
          <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
    </form>


    <div> 
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
     <table id="table" width: 70% ></table>
       <%--  <div class="dropdown">
                    <button class="btn btn-outline-secondary dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        Active
                    </button>
                    <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                        <a class="dropdown-item" href="#">Partially Paid</a>
                        <a class="dropdown-item" href="#">Inactive</a>
                    </div>
                </div>--%>

     </div>
 
    <script>
         
      function PdfLinkFormatter(value, row, id) {
         
          var span = "<span  class='span'  onmouseover='iFrameCell(this)' onmouseleave='leave(this)'  position: static; left: 3500px; border: 3px solid #73AD21;cursor:pointer;' id='ShfaqPDF' >Shfaq PDF</span>";
            return span;
        }

        
        function StatusLinkFormatter() {
         
            var dropdown = `<div class='dropdown'  id='Statusi' > 
                <select onchange='onSelectCell(this)' id="selectStatus">
                    <option>Zgjidh Statusin</option>
                    <option>Approved</option>
                    <option>Refused</option>
                 
                </select>
               
                    </div `;
            return dropdown ;
        }
        function gjeneroButton() {
         
            var button = `<div class='gjenero'  id='gjenero' > 
                        <button onClick="gjeneroFatureBlerje(this)">Gjenero</button>
               
                    </div `;
            return button ;
        }

        function leave(event) {
            const element = document.querySelector(".pdf-preview");
            element.dataset.timerRef = setTimeout(function () {
                $(".pdf-preview").each(function (val){
                    this.parentNode.removeChild(this)
                });
            }, 300);
        }
        
        function onClickCell(event, field, value, row, $element) {
           
            if (value === 'Shfaq Pdf') {
                var eic = $element[0].nextSibling.innerHTML;
                getEinvoice(eic, false, $element);
            }

        }

        function onSelectCell(element) {
            var selected = $(element).find(":selected").text();
            if (selected == "Zgjidh Statusin")
                return;
            switch (selected) {
                case "Approved":
                    selected = "Aprovuar";
                    break;
                case "Refused":
                    selected = "Refuzuar";
                    break;
                default:
                    break;
            }
            var eic = $(element).closest("tr").find("td")[10].innerHTML;
            var vleraMsg = myMesazh.ShtoPyetjeStatusi("Jeni i sigurt qe doni te ndryshoni statusin?", eic, selected)
                 
        }
      

        //funksioni iFrame  
        function iFrameCell(element) {           
            var eic = $(element).closest("tr").find("td")[10].innerHTML;
            getEinvoice(eic, true, element);

        }
        function gjeneroFatureBlerje(element) {
            document.querySelector(".loader-overlay").style.display = "block";
            document.querySelector(".loader").style.display = "block";
            var eic = $(element).closest("tr").find("td")[1].innerHTML;
            var nrDok = $(element).closest("tr").find("td")[2].innerHTML;
            var dtFature = $(element).closest("tr").find("td")[3].innerHTML;
            var afatPagese = $(element).closest("tr").find("td")[4].innerHTML;
            var totali = $(element).closest("tr").find("td")[6].innerHTML;
            location.href = (`/Shto_RegjistrimDokumentash.aspx?shitje_blerje=blerje&shtim_modifikim=shtim&gjenerim=true&eic=${eic}&nrDok=${nrDok}&dtFature=${dtFature}&afatPagese=${afatPagese}&totali=${totali}`);
        }
      
         function func () {

             var columns = [{ field: 'Numri', class: 'margin-nr', title: 'Numri', sortable: true, filterControl: 'input'}, { field: 'EIC', title: 'EIC', sortable: true, filterControl: 'input' },
                 { field: 'DocNumber', title: 'Numri i dokumentit', sortable: true, filterControl: 'input' }, { field: 'RecDateTime', id: "RecDateTime", title: 'Dt. Fature ', sortable: true, filterControl: 'input' },
                 { field: 'DueDateTime', title: 'Afati i pageses', sortable: true, filterControl: 'input' }, { field: 'Status', title: 'Statusi', sortable: true, filterControl: 'input' },
                 { field: 'Amount', title: 'Totali i fatures', sortable: true, filterControl: 'input' }, { field: 'Ndrysho Status', title: 'Ndrysho Status', class: 'margin-status', sortable: true, formatter: StatusLinkFormatter },
                 { field: 'PDF', title: 'PDF', class: 'ShfaqPdf margin-nr', sortable: false, formatter: PdfLinkFormatter, filterControl: 'input' }, { field: "Gjenero Fature Blerje", class: "margin-nr", title: "Gjenero Fature Blerje", sortable: false, formatter: gjeneroButton, filterControl: 'input' }, { field: "x", title: "x", class: "hidden", sortable: false }];

            var test = hfState.Get("json")
            test = test.replaceAll("@", "");

             var data = JSON.parse(test).Einvoice;
             var data2 = [];
             var j = data.length -1 ;
             for (var i = 0; i < data.length; i++) {
                data2[i] = data[j];
                data2[i].Numri = i + 1;
                data2[i].PDF = "Shfaq Pdf";
                data2[i].x = data2[i]["EIC"];
                if (data2[i].hasOwnProperty("DueDateTime"))
                    data2[i].DueDateTime = data2[i].DueDateTime.substring(0, 10);
                if (data2[i].hasOwnProperty("RecDateTime"))
                    data2[i].RecDateTime = data2[i].RecDateTime.substring(0, 10);
                 j--;
                 // ye po pse duhet kjo ktu n kte rast
                
            }

            var $table = $('#table')
            $("#table").bootstrapTable({
                pagination: true,
                paginationParts: [`pageList`],
                striped: true,
                sortable: true,
                search: true,
                columns: columns,
                data: data2,
                filterControl: true

            });


             $table.on('click-cell.bs.table.span', onClickCell);
             
         
            
        }
        
        func();
      
        

        
        //marrja e kerkeses 
        function getEinvoice(e, hover, el) {
            $.ajax({
                pritPergjigje: true,
                method: "POST",
                url: Utils.getServerApiUrl("Rregjistrime", "merrEinvoiceEIC"),
                //bera ndryshiminn e eic
                data: { EIC: e, idNdermarrje: hfStateNdermarrje.value}
            }).done(function (result) {
                var base64str = result[0];

                // dekodimi me base64 string
                var binary = atob(base64str.replace(/\s/g, ''));
                var len = binary.length;
                var buffer = new ArrayBuffer(len);
                var view = new Uint8Array(buffer);
                for (var i = 0; i < len; i++) {
                    view[i] = binary.charCodeAt(i);
                }

                // krijimi i nje objekti blob  me nje  content-type "application/pdf"               
                var blob = new Blob([view], { type: "application/pdf" });
                var url = URL.createObjectURL(blob);


                if (hover) {
                    setTimeout(function () {
                        var iframe = document.createElement("iframe");
                        iframe.classList = "pdf-preview"
                        iframe.src = url + "#zoom=200";
                        document.getElementById("table").insertBefore(iframe, document.getElementById("table").firstChild);
                    }, 300)

                }
                else
                    window.open(url,'_blank');
            }).fail(function (result) {
            });

        }
        //Ndryshimi i statusit
        function changeEinvoiceStatus(e,status) {
           
            $.ajax({
                pritPergjigje: true,
                method: "POST",
                url: Utils.getServerApiUrl("Rregjistrime", "DergoNdryshimStatusiEinvoice"),

                data: { EIC: e, statusi: status, idNdermarrje: hfState.Get("idNdermarrje") }
            }).done(function (result) {
                if (!result) myMesazh.ShtoMesazhGabimi("Ndryshimi i statusit deshtoi!");
                else myMesazh.ShtoMesazhSuksesi("Statusi u ndryshua me sukses!");

            
            });
      }
                

       
        
    </script>
</body>

</html>
