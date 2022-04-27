<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FaturaBlerjeEinvoice.aspx.cs" Inherits="PlatinumWeb.FaturaBlerjeEinvoice" %>

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
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css">
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.css">
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.js"></script>
    <link href="https://unpkg.com/bootstrap-table@1.19.1/dist/bootstrap-table.min.css" rel="stylesheet">
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/extensions/multiple-sort/bootstrap-table-multiple-sort.js"></script>
    <script src="https://unpkg.com/bootstrap-table@1.19.1/dist/extensions/filter-control/bootstrap-table-filter-control.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous">
    <link rel="stylesheet" href="https://use.fontawesome.com/releases/v5.6.3/css/all.css" integrity="sha384-UHRtZLI+pbxtHCWp1t77Bi1L4ZtiqrqD80Kn4Z8NTSRyMA2Fd33n5dQ8lWUE00s/" crossorigin="anonymous">
    
    
    

    <style>
        table {
            border-collapse: collapse;
            width: 100%;
        }

        td, th {
            border: 1px solid #000000;
            text-align: left;
            padding: 8px;
        }

            td:nth-child(even), th:nth-child(even) {
                background-color: #D6EEEE;
            }
            .hidden{
                display: none;
            }
    </style>        
</head>
<body>
    <form runat="server">
        <asp:HiddenField ID="hfState" runat="server" />
        <asp:HiddenField ID="hfStateNdermarrje" runat="server" />


    </form>


    <div> 
       
      
     <table id="table" ></table>
     </div>
 
    <script>
     
        function PdfLinkFormatter(value, row, id) {
            var span = "<span style='background-color:yellow; cursor:pointer;' class='span' >Shiko PDF</span>";
            return span;
        }
        
        function onClickCell(event, field, value, row, $element) {
           
            if (value === 'Shiko Pdf') {
                console.log($element[0]);
                var eic = $element[0].nextSibling.innerHTML;
                getEinvoice(eic);
            }

        }
        var func = function () {

            var columns = [{ field: 'Numri', title: 'Numri', sortable: true }, { field: 'EIC', title: 'EIC', sortable: true, filterControl: 'input' },
                { field: 'DocNumber', title: 'DocNumber', sortable: true, filterControl: 'input' }, { field: 'DueDateTime', title: 'DueDateTime', sortable: true, filterControl: 'select' },
                { field: 'Status', title: 'Status', sortable: true, filterControl: 'input' }, { field: 'Amount', title: 'Amount', sortable: true, filterControl: 'input'},
                { field: 'PDF', title: 'PDF', class: 'shikoPdf', sortable: false, formatter: PdfLinkFormatter }, { field: "x", title: "x", class: "hidden", sortable: false }];
            var test = hfState.value;
            test = test.replaceAll("@", "");

            var data = JSON.parse(test).Einvoice;
            console.log(data)
            console.log(typeof(data))
           for (var i = 0; i < data.length; i++) {
               data[i].Numri = i + 1;
               data[i].PDF = "Shiko Pdf";
               data[i].x = data[i]["EIC"];
               data[i].DueDateTime = data[i].DueDateTime.split("T")[0];
           }     
       
            var $table = $('#table')
                $("#table").bootstrapTable({
                    pagination: true,
                    paginationParts: [`pageList`],
                    striped: true,
                    sortable:true,
                    search: true,
                    columns: columns,
                    data: data,
                    filterControl:true
                    
                });

            $table.on('click-cell.bs.table.span', onClickCell);

           

       
        
        }
        func();
   
        //marrja e kerkeses 
        function getEinvoice(e) {
            $.ajax({
                pritPergjigje: true,
                method: "POST",
                url: Utils.getServerApiUrl("Rregjistrime", "merrEinvoiceEIC"),
                //bera ndryshiminn e eic
                data: { EIC: e, idNdermarrje: hfStateNdermarrje.value }
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
                window.open(url,'_blank');
            }).fail(function (result) {
                myMesazh.ShtoMesazhGabimi("Ndryshimi i statusit deshtoi!");
            });

        }
       
    </script>
</body>

</html>
