function addRow() {

    var table = document.getElementById("Fatura_e_blerjeve");
    var Numri_rendor = document.getElementById("Numri_Rendor");
    var EIC = document.getElementById("EIC");
    var Nr_dok = document.getElementById("Numri_i_dokumentit_te_fatures");
    var Data = document.getElementById("Data_e_fatures");
    var Statusi = document.getElementById("Statusi_i_fatures");
    var Vlefta = document.getElementById("Vlera _e_fatures");
    var Pdf = document.getElementById("butoin_PDF");
    //Nuk edi nqs duhet apo jo

    var rowCount = table.rows.length;
    var row = table.insertRow(rowCount);

    row.insertCell(0).innerHTML = Numri_rendor.value;
    row.insertCell(1).innerHTML = EIC.value;
    row.insertCell(2).innerHTML = Nr_dok.value;
    row.insertCell(3).innerHTML = Data.value;
    row.insertCell(4).innerHTML = Statusi.value;
    row.insertCell(5).innerHTML = Vlefta.value;

}
addRow();


function populateTable() {
    var table = document.getElementById("Fatura_e_blerjeve");
    var xmlNodes = hfState.value;
 

    for (var i = 0; i < xmlNodes.length; i++) {
        var tr = document.createElement('tr');
        table.appendChild(tr);
        console.log(xmlNodes[0]);
        for (var j = 0; j < 6; j++) {
            var td = document.createElement('td');
            td.width = '75';
            td.appendChild(document.createTextNode( + i + "," + j));
            tr.appendChild(td);
        }
    }
    //myTableDiv.appendChild(table);
    //function addTableRowsFromXmlDoc() {

    //    var xmlNodes = hfState.value;
    //    var theTable = document.getElementById("Fatura_e_blerjeve");
    //    var newRow, newCell, i;
    //    console.log(xmlNodes.length);
    //    for (i = 0; i < xmlNodes.length; i++) {
    //        newRow = tableNode.insertRow(i);
    //        for (j = 0; j < xmlNodes[i].childNodes.length; j++) {
    //            newCell = newRow.insertCell(newRow.cells.length);
    //            if (xmlNodes[i].childNodes[j].firstChild) {
    //                newCell.innerHTML = xmlNodes[i].childNodes[j].firstChild.nodeValue;
    //            } else {
    //                newCell.innerHTML = "-";
    //            }
    //            console.log(hfState);
    //        }
    //    }
    //    theTable.appendChild(hfState);

}
populateTable();

/*Perdorim funksionin buildTable(),qe do na ndihmoje per te popoulluar tabelen*/

window.addEventListener("load", function () {
    getRows();
});

/*Krijome funksionin getRows() per te pritur te dhenat qe kemi ne 
XML data duke perdorur AJAX.*/

//function getRows() {
//    var xmlhttp = new XMLHttpRequest();
//    xmlhttp.open("get", "Einvoices", true);
//    xmlhttp.onreadystatechange = function () {
//        if (this.readyState == 4 && this.status == 200) {
//            showResult(this);
//        }
//    };
//    xmlhttp.send(null);
//}


/*Funksioni removeWhiteSpace(), qe do te asimiloje karakteret
 * qe sna duhen ne XML per ta lehtesuar*/
//function removeWhitespace(xml) {
//    var loopIndex;
//    for (loopIndex = 0; loopIndex < xml.childNodes.length; loopIndex++) {
//        var currentNode = xml.childNodes[loopIndex];
//        if (currentNode.nodeType == 1) {
//            removeWhitespace(currentNode);
//        }
//        if (!(/\S/.test(currentNode.nodeValue)) && (currentNode.nodeType == 3)) {
//            xml.removeChild(xml.childNodes[loopIndex--]);
//        }
//    }
//}

//Funxioni addTableRowsFromXmlDoc mbush reshtat e tabeles se krijuar


//Funksioni showResult(), i cili do na sherbeje per te theritur funxionet qe te shohim rezultatet
    function showResult(xmlhttp) {

    var xmlDoc = xmlhttp.responseXML.documentElement;
    
    var rowData = xmlDoc.getElementsByTagName("rowElement");
    var outputResult = document.getElementById("BodyRows");
    addTableRowsFromXmlDoc(rowData, outputResult);
}


