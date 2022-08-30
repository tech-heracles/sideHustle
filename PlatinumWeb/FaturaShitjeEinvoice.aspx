    <%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaturaShitjeEinvoice.aspx.cs" Inherits="PlatinumWeb.FaturaShitjeEinvoice" %>
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
            right:20px;
            opacity: 0.8;
            border-radius: 6px;
            border: 0px solid teal;
            z-index: 9999999;
            overflow-y: hidden;
            height: 165px;
        }
        .margin-nr div{
            margin-bottom: 50%;
        }
        .margin-status div{
            margin-bottom: 30%;
        }
        .ShfaqPdf div{
            margin-bottom: 85%
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

        function leave(event) {
            const element = event.parentElement.parentElement.lastChild;
            clearTimeout(element.dataset.timerRef);

            element.dataset.timerRef = setTimeout(function () {
                event.parentElement.parentElement.removeChild(event.parentElement.parentElement.lastChild);
            }, 5000);
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
            var eic = $(element).closest("tr").find("td")[8].innerHTML;
            var vleraMsg = myMesazh.ShtoPyetjeStatusi("Jeni i sigurt qe doni te ndryshoni statusin?", eic, selected)
                 
        }
      

        //funksioni iFrame  
        function iFrameCell(element) {           
            var eic = $(element).closest("tr").find("td")[8].innerHTML;
            getEinvoice(eic, true, element);

        }

      
         function func () {

             var columns = [{ field: 'Numri', class: 'margin-nr', title: 'Numri', sortable: true }, { field: 'EIC', title: 'EIC', sortable: true, filterControl: 'input' },
                 { field: 'DocNumber', title: 'Numri i dokumentit', sortable: true, filterControl: 'input' }, { field: 'RecDateTime', title: 'Dt. Fature ', sortable: true, filterControl: 'input' },
                 { field: 'DueDateTime', title: 'Afati i pageses', sortable: true, filterControl: 'input' }, { field: 'Status', title: 'Statusi', sortable: true, filterControl: 'input' },
                 { field: 'Amount', title: 'Totali i fatures', sortable: true, filterControl: 'input' },{ field: 'Ndrysho Status', title: 'Ndrysho Status', class: 'margin-status', sortable: true, formatter: StatusLinkFormatter },
                 { field: 'PDF', title: 'PDF', class: 'ShfaqPdf', sortable: false, formatter: PdfLinkFormatter },{ field: "x", title: "x", class: "hidden", sortable: false }];

            var test = hfState.Get("json")
            test = test.replaceAll("@", "");
             //kto testet do mi heqesh se sben

             var data = JSON.parse(test).Einvoice;
             var data2 = [];
             var j = data.length - 1;
             for (var i = 0; i < data.length; i++) {
                 data2[j] = data[i];
                 data2[j].Numri = i + 1;
                 data2[j].PDF = "Shfaq Pdf";
                 data2[j].x = data2[j]["EIC"];
                 if (data2[j].hasOwnProperty("DueDateTime"))
                     data2[j].DueDateTime = data2[j].DueDateTime.substring(0, 10);
                 if (data2[j].hasOwnProperty("RecDateTime"))
                     data2[j].RecDateTime = data2[j].RecDateTime.substring(0, 10);
                j--;
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
            if (hover && el.parentElement.parentElement.children.length == 10)
                return;
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
                    var iframe = document.createElement("iframe");
                    iframe.classList = "pdf-preview"
                    iframe.src = url;
                    el.parentElement.parentElement.appendChild(iframe);
                    elPosition = el.parentElement.getBoundingClientRect();
                    $(".pdf-preview").css("right", elPosition.width + "px");
                    $(".pdf-preview").css("top", elPosition.top + "px");

                }
                else
                    window.open(url,'_blank');
            }).fail(function (result) {
                myMesazh.ShtoMesazhGabimi("Ndryshimi i statusit deshtoi!");
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
