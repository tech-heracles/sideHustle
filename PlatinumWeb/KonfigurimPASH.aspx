<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimPASH.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimPASH" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxrp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxpc" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2.Export, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dxwtl" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Alpha Web</title>
    <script src="js/myFaqeCelje.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 70%;
            height: 28px;
        }
        .style2
        {
            width: 10%;
            height: 28px;
        }
    </style>
    <link href="css/redmond/jquery-ui-1.7.2.custom.css" rel="stylesheet" type="text/css" />
    <link href="js/src/css/ui.jqgrid.css" media="screen" rel="stylesheet" type="text/css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/src/css/AutoComplete.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .ui-jqgrid tr.jqgrow td
        {
            height: auto;
        }
    </style>
    <style>
        html, body
        {
            margin: 0;
            padding: 0;
            font-size: 75%;
        }
    </style>
    <script src="JsGlobal.js" type="text/javascript"></script>
    <script src="js/jquery-1.6.2.min.js" type="text/javascript"></script>
    <script src="js/src/i18n/grid.locale-en.js" type="text/javascript"></script>
    <script src="js/src/grid.base.js" type="text/javascript"></script>
    <script src="js/jquery.jqGrid.min.js" type="text/javascript"></script>
    <script src="js/AutoComplete.js" type="text/javascript"></script>
    <script src="js/src/grid.subgrid.js" type="text/javascript"> </script>
    <script src="js/myWebServices.js" type="text/javascript"></script>
    <script src="js/myCookies.js" type="text/javascript"></script>
</head>
<body onload="Init()">
    <script type="text/javascript">
        //scriptet per ndertimin e grides se pash bashke me subgridat
        var lastsel2 = 1;
        var lastselsub = 1;
        var lastselsub2 = 1;
        var lastselsub3 = 1;
        var lastselsub4 = 1;
        var lastselsubgrid = "";
        var lastselsubgrid2 = "";
        var lastselsubgrid3 = "";
        var lastselsubgrid4 = "";
        var indexglobalpash = -1;
        var indexgloballlogari = -1;
        var lastprindi = "";
        var indexpasheditim = 0;

        var arrayIdKolonaGrides = new Array();
        var arrayPershkrimiKolonaGrides = new Array();
        var arrayVisibleKolonaGrides = new Array();
        var arrayWidthKolonaGrides = new Array();
        var arrayReadOnlyKolonaGrides = new Array();
        var arrayRenditjeKolonaGrides = new Array();

        var arrayIdKolonaSubGrides = new Array();
        var arrayPershkrimiKolonaSubGrides = new Array();
        var arrayVisibleKolonaSubGrides = new Array();
        var arrayWidthKolonaSubGrides = new Array();
        var arrayReadOnlyKolonaSubGrides = new Array();
        var arrayRenditjeKolonaSubGrides = new Array();

        //ndryshon imazhin e butonit fshi kur mausi i vete siper
        function ndryshoImazhin(nr, index) {
            var id = "butonFshi" + index;

            if (document.getElementById(id) != null) {
                if (nr == 1)
                    document.getElementById(id).src = "images/blue-square-icon.png";
                else if (nr == 0)
                    document.getElementById(id).src = "images/square-icon.png";
            }
        }
        //perdoret per te ndryshuar griden sipas llojit qe zgjedh perdoruesi llogari apo ze 
        //  dhe fshin te dhenat e grides te tipit te meparshem dhe te bijve te saj nqs ka te dhena
        function changeGrid(gridid, index, nivel, id) {
            var prind = gridid.split('_');
            if (prind.length == 3) {
                if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            }
            else if (prind.length == 5) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            }
            else if (prind.length == 7) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            }
            else if (prind.length == 9) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            }
            lastprindi = pershkrimi;
            var lloj = $('#cmbLloji' + gridid + id)[0].value;
            if (index == 1 & lloj == 1) {
                $("table[id$='" + gridid + "']").GridUnload("&quot;" + gridid + "&quot;");

                if (nivel == 2) {
                    inicializoGride2(gridid);
                    fshibij(2, lastprindi, "PASH");
                }
                else if (nivel == 3) {
                    inicializoGride3(gridid);
                    fshibij(3, lastprindi, "PASH");
                }
                else if (nivel == 4) {
                    inicializoGride4(gridid);
                    fshibij(4, lastprindi, "PASH");
                }
                else if (nivel == 5) {
                    inicializoGride5(gridid);
                    fshibij(5, lastprindi, "PASH");
                }
            }
            else if (index == 2) {
                $("table[id$='" + gridid + "']").GridUnload("&quot;" + gridid + "&quot;");

                inicializoSubGride(gridid);

                fshibij(nivel, lastprindi, "PASH");
            }
            else {

                jQuery("#txtKodi" + gridid + id)[0].value = "";
                var idKontrolli = "#txtPershkrimiLlogaria" + gridid + id;
                jQuery(idKontrolli)[0].value = '';
                if (lloj == 2)
                    newid = eksistonLL("PASH", lastprindi, id, "Llogari standarte");
                else if (lloj == 3)
                    newid = eksistonLL("PASH", lastprindi, id, "Llogari");
                if (arrLL[0][newid] != undefined) {
                    arrLL[0][newid] = arrLL[0][newid].split(':')[0] + ":";
                    arrLL[1][newid] = arrLL[1][newid].split(':')[0] + ":";
                    arrLL[2][newid] = arrLL[2][newid].split(':')[0] + ":Gjithmone";
                    arrLL[3][newid] = arrLL[3][newid].split(':')[0] + ":Pozitive";
                    arrLL[4][newid] = arrLL[4][newid].split(':')[0] + ":";
                    arrLL[5][newid] = arrLL[5][newid].split(':')[0] + ":PASH";
                    arrLL[6][newid] = arrLL[6][newid].split(':')[0] + ":";
                }

            }
            ruajllogari();
            ruajpash();
        }
        //kontrollon nqs eksistojne te dhena per kete resht dhe kthen indexin e vendodhjes te ketij rreshti ose indexin e ri nqs nuk eksiston

        function eksiston(nivel, prind, id) {
            indexglobalpash++;
            var newid = indexglobalpash;
            for (i = 0; i < arr[0].length; i++) {
                if (arr[0][i].split(':')[1] == nivel & arr[1][i].split(':')[1] == prind)
                    if (arr[0][i].split(':')[0] == id) {
                        newid = i;
                        indexglobalpash--;
                    }
            }
            return newid;
        }
        //fshin bijte e nje niveli te caktuar ne menyre rekursive

        function fshibij(nivel, prind, lloj) {
            if (nivel < 6) {
                for (i = 0; i < arr[0].length; i++) {
                    if (arr[0][i].split(':')[1] == nivel & arr[1][i].split(':')[1] == prind) {
                        fshibij(nivel + 1, arr[2][i].split(':')[1], "PASH");

                    }
                }
                for (j = 0; j < arr[0].length; j++) {
                    if (arr[0][j].split(':')[1] == nivel & arr[1][j].split(':')[1] == prind) {

                        arr[2][j] = arr[2][j].split(':')[0] + ":";
                        arr[3][j] = arr[3][j].split(':')[0] + ":True";
                    }
                }
                for (k = 0; k < arrLL[0].length; k++) {
                    if (arrLL[5][k].split(':')[1] == lloj & arrLL[4][k].split(':')[1] == prind) {
                        arrLL[0][k] = arrLL[0][k].split(':')[0] + ":";
                        arrLL[1][k] = arrLL[1][k].split(':')[0] + ":";
                        arrLL[2][k] = arrLL[2][k].split(':')[0] + ":Gjithmone";
                        arrLL[3][k] = arrLL[3][k].split(':')[0] + ":Pozitive";
                        arrLL[4][k] = arrLL[4][k].split(':')[0] + ":";
                        arrLL[5][k] = arrLL[5][k].split(':')[0] + ":PASH";
                        arrLL[6][k] = arrLL[6][k].split(':')[0] + ":";
                    }
                }
            }
            ruajllogari();
            ruajpash();
        }
        //perdoret per te marre indexin ku ndodhet rreshti qe po modifikohet nqs eksiston ose indexin e ri nqs nuk eksiston

        function eksistonLL(lloj, prind, id, tip) {

            var newid = arrLL[0].length;
            for (i = 0; i < arrLL[0].length; i++) {
                if (arrLL[5][i].split(':')[1] == lloj & arrLL[4][i].split(':')[1] == prind & arrLL[6][i].split(':')[1] == tip)
                    if (arrLL[0][i].split(':')[0] == id) {
                        newid = i;

                    }
            }
            return newid;
        }
        //perdoret per te kontrolluar nese eksiston e njejta llogari tek ky ze dhe me te njejten gjendje me zerat e tjere
        function eksistonLLogNeKetePrind(lloj, prind, index, kod, tipi, lastsel, gjendje) {

            PlatinumWeb.wsfunc.eksitonLLogNeKetePrind(arrLL, lloj, prind, index, kod, tipi, lastsel, gjendje, SucceededCallbackPrindi);

        }
        //funksioni qe kthen pergjigjen e web serverit
        function SucceededCallbackPrindi(result) {
            window.parent.SessionTimeout.sendKeepAlive();
            var eksiton = result.split(';')[0];
            newid = result.split(';')[1];
            if (eksiton == "true") {
                var index2 = parseInt(result.split(';')[2]);
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                be = "<input id='butonFshi" + lastselsubgrid4 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index2 + ")'/>";
                var mydata2 = { txtFshi: be, cmbLloji: result.split(';')[3], txtKodi: "", txtPershkrimiLlogaria: "", cmbGjendja: "", cmbShenja: "" };
                var su = $("table[id$='" + lastselsubgrid4 + "']").setRowData(parseInt(result.split(';')[2]), mydata2, '');
                if (arrLL[0][newid] != undefined) {
                    arrLL[0][newid] = arrLL[0][newid].split(':')[0] + ":";
                    arrLL[1][newid] = arrLL[1][newid].split(':')[0] + ":";
                    arrLL[2][newid] = arrLL[2][newid].split(':')[0] + ":Gjithmone";
                    arrLL[3][newid] = arrLL[3][newid].split(':')[0] + ":Pozitive";
                    arrLL[4][newid] = arrLL[4][newid].split(':')[0] + ":";
                    arrLL[5][newid] = arrLL[5][newid].split(':')[0] + ":PASH";
                    arrLL[6][newid] = arrLL[6][newid].split(':')[0] + ":";
                }
                ruajllogari();
                if (index2 != lastselsub4)
                    fshiClickedS(lastselsubgrid4, index2);
                $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4); alert(result.split(';')[4]);
            }
        }

        //ne hiddenfield gridaZerat ruhen te gjithe rreshtat e grides pash per tu perdorur tek ruajtja
        function ruajpash() {
            hfgridaZerat = document.getElementById("gridaZerat");
            var str = "";
            for (i = 0; i < arr.length; i++) {
                str = str + arr[i] + ';';
            }
            hfgridaZerat.value = str;

        }
        //ne hiddenfield gridaLlogarite ruhen te gjithe rreshtat e grides llogari per tu perdorur tek ruajtja
        function ruajllogari() {
            hfgridaLLogarite = document.getElementById("gridaLlogarite");
            var str = "";
            for (j = 0; j < arrLL[0].length; j++) {
                for (i = 0; i < arrLL.length; i++) {
                    str = str + arrLL[i][j] + ';';
                }
                str += '*';
            }
            hfgridaLLogarite.value = str;

        }
        function inicializoGride() {
            $("table[id$='rowed5']").jqGrid
        (
            {
                datatype: "local",
                colNames: [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3]],
                colModel: [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0], hidden: arrayVisibleKolonaGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon, custom_value: myvalueFshi} },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1], hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo, custom_value: myvalueCombo} },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2], hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi, custom_value: myvalueNormal} },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3], hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal, custom_value: myvalueCheck} }
                    ],
                caption: "Niveli 1",
                sortable: false,
                cellsubmit: 'clientArray',
                height: 'auto',
                subGrid: true,
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subgrid_table_id;
                    subgrid_table_id = subgrid_id + "_t";
                    var pershkrimi = "";
                    if ($("table[id$='rowed5']").getRowData(row_id).txtPershkrimi.search('txtPershkrimi') != -1)
                        pershkrimi = jQuery('#txtPershkrimi' + lastsel2)[0].value;
                    else pershkrimi = $("table[id$='rowed5']").getRowData(row_id).txtPershkrimi
                    lastprindi = pershkrimi;
                    $("div[id$='" + subgrid_id.replace('ASPxPageControl1_', '') + "']").html("<table id='" + subgrid_table_id + "' class='scroll'></table>");
                    inicializoGride2(subgrid_table_id.replace("ASPxPageControl1_", ""));
                    mbushGrideNgaHiddenFieldi2(pershkrimi, subgrid_table_id.replace("ASPxPageControl1_", ""));
                },
                onCellSelect: function (id, icol) {
                    $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                    lastselsub = 0;
                    lastselsub2 = 0;
                    lastselsub3 = 0;
                    lastselsub4 = 0;
                    lastsel2 = id;
                    indexpasheditim = id;
                    $("table[id$='rowed5']").editRow(id);
                    var idkontrolli = "#" + $("table[id$='rowed5']").getGridParam('colModel')[icol].index + lastsel2;
                    if (jQuery(idkontrolli)[0] != undefined) {
                        if (jQuery(idkontrolli)[0].isDisabled == false)
                            jQuery(idkontrolli)[0].focus();
                    } keyPressPershkrimi();
                }
            }
        );
            be = "<input id='butonFshi" + lastsel2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel2 + ")' onmouseout='ndryshoImazhin(0," + lastsel2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + 1 + ")'/>";
            el3check = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox' onchange='changeCheck(" + lastsel2 + ")'  checked='checked' style='width: 100%'  />   ";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check
            }
            ];

            for (var i = 0; i < mydata2.length; i++) {
                $("table[id$='rowed5']").addRowData(1, mydata2[i]);

            }

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit

        function myElemButon() {
            el = "<input id='butonFshi" + lastsel2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel2 + ")' onmouseout='ndryshoImazhin(0," + lastsel2 + ")' src='images/square-icon.png' onclick='fshiClicked(" + lastsel2 + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim
        function myvalueFshi(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastsel2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel2 + ")' onmouseout='ndryshoImazhin(0," + lastsel2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + lastsel2 + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat 
        function fshiClicked(index) {
            if ($("table[id$='rowed5']").getGridParam('reccount') > 1) {
                newid = eksiston(1, "", index);

                $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                if (arr[2][newid] != undefined) {
                    fshibij(2, arr[2][newid].split(':')[1], "PASH");
                    arr[2][newid] = arr[2][newid].split(':')[0] + ":";
                    arr[3][newid] = arr[3][newid].split(':')[0] + ":True";
                }
                ruajpash();
                indexpasheditim = 0;
                $("table[id$='rowed5']").collapseSubGridRow(index);
                $("table[id$='rowed5']").delRowData(index);
            }
            else {
                $("table[id$='rowed5']").GridUnload("#" + $("table[id$='rowed5']")[0].id);

                for (i = 0; i < 4; i++) {
                    arr[i] = new Array();

                }
                for (i = 0; i < 7; i++) {
                    arrLL[i] = new Array();
                }
                hfgridaLLogarite = document.getElementById("gridaLlogarite");
                hfgridaLLogarite.value = "";
                ruajpash();
                ruajllogari();
                indexglobalpash = -1;
                indexgloballlogari = -1; inicializoGride();
            }

        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemCombo(value) {
            var el3 = document.createElement("div");
            var temp;

            temp = "<select  id='cmbLloji" + lastsel2 + "'  ><OPTION value=1 selected='selected'>Zeri</OPTION>";
            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueCombo(elem) {
            return jQuery('#cmbLloji' + lastsel2)[0][jQuery('#cmbLloji' + lastsel2)[0].selectedIndex].text;
        }
        //krijon elementin textbox per pershkrimin kur rreshti eshte ne editim
        function myElemPershkrimi(value) {
            var el = document.createElement("div");
            el.innerHTML = "<input  id ='txtPershkrimi" + lastsel2 + "'  type ='text' onchange='changePershkrimi()' onkeypress='keyPressPershkrimi()' value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //funksioni kur shtypet nje karakter tek textboxi i pershkrimit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit

        function keyPressPershkrimi() {
            if ($("table[id$='rowed5']").getInd(lastsel2) == $("table[id$='rowed5']").getDataIDs().length - 1) {
                var index2 = parseInt(lastsel2) + 1;
                be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";

                el3check = "<input  id ='cbTotali" + index2 + "'  type ='checkbox'  onchange='changeCheck(" + index2 + ")' checked='checked'  style='width: 100%'   />";
                var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                var su = $("table[id$='rowed5']").addRowData(parseInt(lastsel2) + 1, mydata2);

            }
            else if ($("table[id$='rowed5']").getDataIDs()[$("table[id$='rowed5']").getDataIDs().length - 1] == "" & $("table[id$='rowed5']").getInd(lastsel2) == $("table[id$='rowed5']").getDataIDs().length - 2) {
                var index2 = parseInt(lastsel2) + 1;
                be = "<input id='butonFshi" + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + index2 + ")' onmouseout='ndryshoImazhin(0," + index2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + index2 + ")'/>";

                el3check = "<input  id ='cbTotali" + index2 + "'  type ='checkbox'  onchange='changeCheck(" + index2 + ")' checked='checked'  style='width: 100%'   />";
                var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                var su = $("table[id$='rowed5']").addRowData(parseInt(lastsel2) + 1, mydata2);

            }


        }
        //ruan te dhenat tek array nqs ndryshon pershkrimi
        function changePershkrimi() {
            var o = $(document.activeElement).attr('id');
            newid = eksiston(1, "", lastsel2);
            $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');

            arr[0][newid] = lastsel2 + ":1"
            arr[1][newid] = lastsel2 + ":";
            arr[2][newid] = lastsel2 + ":" + $("table[id$='rowed5']").getRowData(lastsel2).txtPershkrimi;
            if ($("table[id$='rowed5']").getRowData(lastsel2).cbTotali.search('CHECKED') != -1)
                arr[3][newid] = lastsel2 + ":True";
            else arr[3][newid] = lastsel2 + ":False";
            $("table[id$='rowed5']").editRow(lastsel2);
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

            ruajpash();
        }

        //krijon elementin checkbox total nqs rreshti eshte ne editim
        function myelemTotal(value, options) {
            var el3 = document.createElement("div");
            el3.innerHTML = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox' onchange='changePershkrimi()'  onBlur='lostFocusKoloneFundit()' style='width: 100%' > ";
            if (value == 'false') el3.firstChild.checked = false;
            else if (value == 'true') el3.firstChild.checked = true;
            else if (value.toString().search('CHECKED') == -1) el3.firstChild.checked = false;
            else if (value.toString().search('CHECKED') != -1) el3.firstChild.checked = true;

            el3.value = value;
            return el3;
        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFundit() {

            $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
            lastsel2 = parseInt(lastsel2) + 1;
            indexpasheditim = lastsel2;
            $("table[id$='rowed5']").editRow(lastsel2);

        }
        //krijon elementin myvalueCheck kur rreshti nuk eshte ne editim
        function myvalueCheck(elem) {
            if (elem[0].firstChild.checked == true)
                el3check = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox'  onchange='changeCheck(" + lastsel2 + ")' checked='checked' style='width: 100%'   ";
            else el3check = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox' onchange='changeCheck(" + lastsel2 + ")'   style='width: 100%'   ";

            el3check += ">";
            return el3check;
        }
        //perdoret per te ruajtur te dhenat ne array kur ndryshon checkim i totalit kur rreshti nuk eshte ne editim
        function changeCheck(id) {
            newid = eksiston(1, "", id);

            arr[0][newid] = id + ":1"
            arr[1][newid] = id + ":";
            arr[2][newid] = id + ":" + $("table[id$='rowed5']").getRowData(id).txtPershkrimi;
            if (arr[3][newid] == id + ":False")
                arr[3][newid] = id + ":True";
            else arr[3][newid] = id + ":False";


            ruajpash();
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi te zerit te nivelit te pare
        function mbushGrideNgaHiddenFieldi() {
            var hf5 = document.getElementById("gridaZerat");

            if (hf5.value != "") {
                var aktivet = hf5.value.split(';');
                var niveli = aktivet[0].split(',');
                var prindi = aktivet[1].split(',');
                var pershkrimi = aktivet[2].split(',');
                var totali = aktivet[3].split(',');

                lastsel2 = 1;

                $("table[id$='rowed5']").delRowData(1);

                for (var i = 0; i < niveli.length; i++) {
                    be = "<input id='butonFshi" + lastsel2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel2 + ")' onmouseout='ndryshoImazhin(0," + lastsel2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + lastsel2 + ")'/>";
                    el3check = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox' onchange='changeCheck(" + lastsel2 + ")'  checked='checked' style='width: 100%'  />   ";
                    el3check2 = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox'  onchange='changeCheck(" + lastsel2 + ")'  style='width: 100%'  />   ";
                    if (niveli[i].split(':')[1] != 1)
                        continue;
                    else {
                        lloji = 'Zeri';
                        emertimi = (pershkrimi[i].split(':')[1] == undefined) ? "" : pershkrimi[i].split(':')[1];
                        tot = (totali[i].split(':')[1] == undefined) ? el3check : ((totali[i].split(':')[1] == "True") ? el3check : el3check2);

                        var datarow = { txtFshi: be, cmbLloji: lloji, txtPershkrimi: emertimi, cbTotali: tot };
                        var su;
                        if (emertimi != "") {
                            su = $("table[id$='rowed5']").addRowData(parseInt(lastsel2), datarow);

                            ;

                            lastsel2 = lastsel2 + 1;
                        }
                    }
                }
                be = "<input id='butonFshi" + lastsel2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + lastsel2 + ")' onmouseout='ndryshoImazhin(0," + lastsel2 + ")'  src='images/square-icon.png' onclick='fshiClicked(" + lastsel2 + ")'/>";
                el3check = "<input  id ='cbTotali" + lastsel2 + "'  type ='checkbox'  onchange='changeCheck(" + lastsel2 + ")'' checked='checked' style='width: 100%'  />   ";

                var datarow = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                var su;
                su = $("table[id$='rowed5']").addRowData(parseInt(lastsel2), datarow);
            }


        }

        function inicializoGride2(subgrid_table_id) {
            $("table[id$='" + subgrid_table_id + "']").jqGrid
        (
            {
                datatype: "local",
                colNames: [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3]],
                colModel: [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0] - 5, hidden: arrayVisibleKolonaGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon2, custom_value: myvalueFshi2} },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1] - 5, hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo2, custom_value: myvalueCombo2} },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2] - 14, hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi2, custom_value: myvalueNormal} },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3] - 5, hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal2, custom_value: myvalueCheck2} }
                    ],
                caption: "Niveli 2",
                sortable: false,
                cellsubmit: 'clientArray',
                height: 'auto',
                subGrid: true,
                subGridRowExpanded: function (subgrid_id, row_id) {

                    var subgrid_table_id1;
                    subgrid_table_id1 = subgrid_id + "_t";
                    var pershkrimi = "";
                    if ($("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi.search('txtPershkrimi') != -1)
                        pershkrimi = jQuery('#txtPershkrimi' + lastselsubgrid + lastselsub)[0].value;
                    else pershkrimi = $("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi
                    lastprindi = pershkrimi;
                    $("div[id$='" + subgrid_id.replace('ASPxPageControl1_', '') + "']").html("<table id='" + subgrid_table_id1 + "' class='scroll'></table>");
                    inicializoGride3(subgrid_table_id1.replace("ASPxPageControl1_", ""));
                    mbushGrideNgaHiddenFieldi3(pershkrimi, subgrid_table_id1.replace("ASPxPageControl1_", ""));
                    //  mbushSubGridenRreshtit(row_id);
                },
                onCellSelect: function (id, icol) {
                    $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                    $("table[id$='" + subgrid_table_id + "']").saveRow(lastselsub, false, 'clientArray');
                    $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                    lastsel2 = 0;
                    lastselsub2 = 0;
                    lastselsub3 = 0;
                    lastselsub4 = 0;
                    lastselsub = id;
                    indexpasheditim = subgrid_table_id + lastselsub;
                    lastselsubgrid = subgrid_table_id;
                    $("table[id$='" + subgrid_table_id + "']").editRow(id);
                    var idkontrolli = "#" + $("table[id$='" + subgrid_table_id + "']").getGridParam('colModel')[icol].index + lastselsubgrid + lastselsub;
                    if (jQuery(idkontrolli)[0] != undefined) {
                        if (jQuery(idkontrolli)[0].isDisabled == false)
                            jQuery(idkontrolli)[0].focus();
                    }
                    keyPressPershkrimi2();
                }
            }
        );

            be = "<input id='butonFshi" + subgrid_table_id + lastselsub + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + subgrid_table_id + lastselsub + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + subgrid_table_id + lastselsub + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + subgrid_table_id + "&quot;" + "," + lastselsub + ")'/>";
            el3check = "<input  id ='cbTotali" + subgrid_table_id + lastselsub + "'  type ='checkbox'  onchange='changeCheck2(" + lastselsub + "," + "&quot;" + subgrid_table_id + "&quot;" + ")' checked='checked' style='width: 100%'  />   ";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check
            }
            ];
            for (var i = 0; i < mydata2.length; i++) {
                $("table[id$='" + subgrid_table_id + "']").addRowData(1, mydata2[i]);

            }

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit

        function myElemButon2() {
            el = "<input id='butonFshi" + lastselsubgrid + lastselsub + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid + lastselsub + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid + lastselsub + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + lastselsubgrid + "&quot;" + "," + lastselsub + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim
        function myvalueFshi2(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastselsubgrid + lastselsub + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid + lastselsub + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid + lastselsub + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + lastselsubgrid + "&quot;" + "," + lastselsub + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat dhe mbyll griden nqs eshte rreshti i fundit
        function fshiClicked2(gridid, index) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            lastprindi = pershkrimi;
            newid = eksiston(2, lastprindi, index);
            $("table[id$='" + gridid + "']").saveRow(lastselsub, false, 'clientArray');
            if (arr[2][newid] != undefined) {
                fshibij(3, arr[2][newid].split(':')[1], "PASH");
                arr[2][newid] = arr[2][newid].split(':')[0] + ":";
                arr[3][newid] = arr[3][newid].split(':')[0] + ":True";
            }
            ruajpash();
            indexpasheditim = 0;
            if ($("table[id$='" + gridid + "']").getGridParam('reccount') > 1) {
                $("table[id$='" + gridid + "']").collapseSubGridRow(index);
                $("table[id$='" + gridid + "']").delRowData(index);
            }
            else $("table[id$='rowed5']").collapseSubGridRow(gridid.replace("rowed5_", "").replace("_t", ""));

        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemCombo2(value) {
            var el3 = document.createElement("div");
            var temp;
            if (value == "Llogari")
                temp = "<select  id='cmbLloji" + lastselsubgrid + lastselsub + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid + "&quot;" + ",2,2,&quot;" + lastselsub + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 selected='selected'>Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            else if (value == "Llogari standarte")
                temp = "<select  id='cmbLloji" + lastselsubgrid + lastselsub + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid + "&quot;" + ",2,2,&quot;" + lastselsub + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 selected='selected'>Llogari standarte</OPTION>";
            else temp = "<select  id='cmbLloji" + lastselsubgrid + lastselsub + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid + "&quot;" + ",2,2,&quot;" + lastselsub + "&quot;)' ><OPTION value=1 selected='selected'>Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";

            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueCombo2(elem) {
            return jQuery('#cmbLloji' + lastselsubgrid + lastselsub)[0][jQuery('#cmbLloji' + lastselsubgrid + lastselsub)[0].selectedIndex].text;
        }
        //krijon elementin textbox per pershkrimin kur rreshti eshte ne editim
        function myElemPershkrimi2(value) {
            var el = document.createElement("div");
            el.innerHTML = "<input  id ='txtPershkrimi" + lastselsubgrid + lastselsub + "'  type ='text' onchange='changePershkrimi2()' onkeypress='keyPressPershkrimi2()' value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //funksioni kur shtypet nje karakter tek textboxi i pershkrimit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit
        //kontrollon nese per kete gride eksiston elementi prind
        function keyPressPershkrimi2() {
            var prind = lastselsubgrid.split('_');
            if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                if ($("table[id$='" + lastselsubgrid + "']").getInd(lastselsub) == $("table[id$='" + lastselsubgrid + "']").getDataIDs().length - 1) {
                    var index2 = parseInt(lastselsub) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + lastselsubgrid + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid + index2 + "'  type ='checkbox' onchange='changeCheck2(" + index2 + "," + "&quot;" + lastselsubgrid + "&quot;" + ")' checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid + "']").addRowData(parseInt(lastselsub) + 1, mydata2);


                }
                else if ($("table[id$='" + lastselsubgrid + "']").getDataIDs()[$("table[id$='" + lastselsubgrid + "']").getDataIDs().length - 1] == "" & $("table[id$='" + lastselsubgrid + "']").getInd(lastselsub) == $("table[id$='" + lastselsubgrid + "']").getDataIDs().length - 2) {
                    var index2 = parseInt(lastselsub) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + lastselsubgrid + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid + index2 + "'  type ='checkbox' onchange='changeCheck2(" + index2 + "," + "&quot;" + lastselsubgrid + "&quot;" + ")'  checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid + "']").addRowData(parseInt(lastselsub) + 1, mydata2);


                }
            }
        }
        //ruan te dhenat tek array nqs ndryshon pershkrimi
        function changePershkrimi2() {
            var o = $(document.activeElement).attr('id');
            newid = eksiston(2, lastprindi, lastselsub);
            $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');

            arr[0][newid] = lastselsub + ":2"
            arr[1][newid] = lastselsub + ":" + lastprindi;
            arr[2][newid] = lastselsub + ":" + $("table[id$='" + lastselsubgrid + "']").getRowData(lastselsub).txtPershkrimi;
            if ($("table[id$='" + lastselsubgrid + "']").getRowData(lastselsub).cbTotali.search('CHECKED') != -1)
                arr[3][newid] = lastselsub + ":True";
            else arr[3][newid] = lastselsub + ":False";
            $("table[id$='" + lastselsubgrid + "']").editRow(lastselsub);
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

            ruajpash();

        }
        //krijon elementin checkbox total nqs rreshti eshte ne editim
        function myelemTotal2(value, options) {
            var el3 = document.createElement("div");
            el3.innerHTML = "<input  id ='cbTotali" + lastselsubgrid + lastselsub + "' onchange='changePershkrimi2()'  type ='checkbox'  onBlur='lostFocusKoloneFundit2()' style='width: 100%' > ";
            if (value == 'false') el3.firstChild.checked = false;
            else if (value == 'true') el3.firstChild.checked = true;
            else if (value.toString().search('CHECKED') == -1) el3.firstChild.checked = false;
            else if (value.toString().search('CHECKED') != -1) el3.firstChild.checked = true;

            el3.value = value;
            return el3;
        }
        //perdoret per te ruajtur te dhenat ne array kur ndryshon checkim i totalit kur rreshti nuk eshte ne editim
        function changeCheck2(id, gridid) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;

            newid = eksiston(2, pershkrimi, id);

            arr[0][newid] = id + ":2"
            arr[1][newid] = id + ":" + pershkrimi;
            arr[2][newid] = id + ":" + $("table[id$='" + gridid + "']").getRowData(id).txtPershkrimi;
            if (arr[3][newid] == id + ":False")
                arr[3][newid] = id + ":True";
            else arr[3][newid] = id + ":False";

            ruajpash();

        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFundit2() {

            $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
            lastselsub = parseInt(lastselsub) + 1;
            indexpasheditim = lastselsubgrid + lastselsub;
            $("table[id$='" + lastselsubgrid + "']").editRow(lastselsub);

        }

        //krijon elementin myvalueCheck kur rreshti nuk eshte ne editim

        function myvalueCheck2(elem) {
            if (elem[0].firstChild.checked == true)
                el3check = "<input  id ='cbTotali" + lastselsubgrid + lastselsub + "' onchange='changeCheck2(" + lastselsub + "," + "&quot;" + lastselsubgrid + "&quot;" + ")' type ='checkbox' checked='checked' style='width: 100%'   ";
            else el3check = "<input  id ='cbTotali" + lastselsubgrid + lastselsub + "' onchange='changeCheck2(" + lastselsub + "," + "&quot;" + lastselsubgrid + "&quot;" + ")' type ='checkbox'    style='width: 100%'   ";

            el3check += ">";
            return el3check;
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi te zerit te nivelit te dyte

        function mbushGrideNgaHiddenFieldi2(pershk, gridid) {
            var hf5 = document.getElementById("gridaZerat");


            if (hf5.value != "") {
                var aktivet = hf5.value.split(';');
                var niveli = aktivet[0].split(',');
                var prindi = aktivet[1].split(',');
                var pershkrimi = aktivet[2].split(',');
                var totali = aktivet[3].split(',');
                lastselsub = 1;

                $("table[id$='" + gridid + "']").delRowData(1);
                for (var i = 0; i < niveli.length; i++) {
                    be = "<input id='butonFshi" + gridid + lastselsub + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + gridid + "&quot;" + "," + lastselsub + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub + "'  type ='checkbox' onchange='changeCheck2(" + lastselsub + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
                    el3check2 = "<input  id ='cbTotali" + gridid + lastselsub + "'  type ='checkbox' onchange='changeCheck2(" + lastselsub + "," + "&quot;" + gridid + "&quot;" + ")'   style='width: 100%'  />   ";
                    if (niveli[i].split(':')[1] != 2)
                        continue;
                    else if (prindi[i].split(':')[1] == pershk) {
                        lloji = 'Zeri';
                        emertimi = (pershkrimi[i].split(':')[1] == undefined) ? "" : pershkrimi[i].split(':')[1];
                        tot = (totali[i].split(':')[1] == undefined) ? el3check : ((totali[i].split(':')[1] == "True") ? el3check : el3check2);
                        var datarow = { txtFshi: be, cmbLloji: lloji, txtPershkrimi: emertimi, cbTotali: tot };
                        var su;
                        if (emertimi != "") {
                            su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub), datarow);

                            ;

                            lastselsub = lastselsub + 1;
                        }
                    }
                }
                if (lastselsub == 1) {
                    $("table[id$='" + gridid + "']").GridUnload("#" + $("table[id$='" + gridid + "']")[0].id);
                    inicializoSubGride(gridid);
                    mbushSubGrideNgaHiddenFieldi(pershk, gridid);
                }
                else {
                    be = "<input id='butonFshi" + gridid + lastselsub + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked2(" + "&quot;" + gridid + "&quot;" + "," + lastselsub + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub + "'  type ='checkbox' onchange='changeCheck2(" + lastselsub + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";

                    var datarow = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su;
                    su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub), datarow);

                }
            }
        }

        function inicializoGride3(subgrid_table_id) {
            $("table[id$='" + subgrid_table_id + "']").jqGrid
        (
            {
                datatype: "local",
                colNames: [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3]],
                colModel: [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0] - 10, hidden: arrayVisibleKolonaGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon3, custom_value: myvalueFshi3} },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1] - 10, hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo3, custom_value: myvalueCombo3} },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2] - 28, hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi3, custom_value: myvalueNormal} },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3] - 10, hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal3, custom_value: myvalueCheck3} }
                    ],
                caption: "Niveli 3",
                sortable: false,
                cellsubmit: 'clientArray',
                height: 'auto',
                subGrid: true,
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subgrid_table_id2;
                    subgrid_table_id2 = subgrid_id + "_t";
                    var pershkrimi = "";
                    if ($("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi.search('txtPershkrimi') != -1)
                        pershkrimi = jQuery('#txtPershkrimi' + lastselsubgrid2 + lastselsub2)[0].value;
                    else pershkrimi = $("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi
                    lastprindi = pershkrimi;
                    $("div[id$='" + subgrid_id.replace('ASPxPageControl1_', '') + "']").html("<table id='" + subgrid_table_id2 + "' class='scroll'></table>");
                    inicializoGride4(subgrid_table_id2.replace("ASPxPageControl1_", ""));
                    mbushGrideNgaHiddenFieldi4(pershkrimi, subgrid_table_id2.replace("ASPxPageControl1_", ""));

                },
                onCellSelect: function (id, icol) {
                    $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='" + subgrid_table_id + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                    lastsel2 = 0;
                    lastselsub = 0;
                    lastselsub3 = 0;
                    lastselsub4 = 0;
                    lastselsub2 = id;
                    indexpasheditim = subgrid_table_id + lastselsub2;
                    lastselsubgrid2 = subgrid_table_id;
                    $("table[id$='" + subgrid_table_id + "']").editRow(id);
                    var idkontrolli = "#" + $("table[id$='" + subgrid_table_id + "']").getGridParam('colModel')[icol].index + lastselsubgrid2 + lastselsub2;
                    if (jQuery(idkontrolli)[0] != undefined) {
                        if (jQuery(idkontrolli)[0].isDisabled == false)
                            jQuery(idkontrolli)[0].focus();
                    }
                    keyPressPershkrimi3();
                }
            }
        );
            be = "<input id='butonFshi" + subgrid_table_id + lastselsub2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + subgrid_table_id + lastselsub2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + subgrid_table_id + lastselsub2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + subgrid_table_id + "&quot;" + "," + lastselsub2 + ")'/>";
            el3check = "<input  id ='cbTotali" + subgrid_table_id + lastselsub2 + "'  type ='checkbox'   onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + subgrid_table_id + "&quot;" + ")' checked='checked' style='width: 100%'  />   ";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check
            }
            ];
            for (var i = 0; i < mydata2.length; i++) {
                $("table[id$='" + subgrid_table_id + "']").addRowData(1, mydata2[i]);

            }

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit

        function myElemButon3() {
            el = "<input id='butonFshi" + lastselsubgrid2 + lastselsub2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid2 + lastselsub2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid2 + lastselsub2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + lastselsubgrid2 + "&quot;" + "," + lastselsub2 + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim
        function myvalueFshi3(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastselsubgrid2 + lastselsub2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid2 + lastselsub2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid2 + lastselsub2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + lastselsubgrid2 + "&quot;" + "," + lastselsub2 + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat dhe mbyll griden nqs eshte rreshti i fundit
        function fshiClicked3(gridid, index) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            lastprindi = pershkrimi;
            newid = eksiston(3, lastprindi, index);
            $("table[id$='" + gridid + "']").saveRow(lastselsub2, false, 'clientArray');
            if (arr[2][newid] != undefined) {
                fshibij(4, arr[2][newid].split(':')[1], "PASH");
                arr[2][newid] = arr[2][newid].split(':')[0] + ":";
                arr[3][newid] = arr[3][newid].split(':')[0] + ":True";
            }
            ruajpash();
            indexpasheditim = 0;
            if ($("table[id$='" + gridid + "']").getGridParam('reccount') > 1) {
                $("table[id$='" + gridid + "']").collapseSubGridRow(index);
                $("table[id$='" + gridid + "']").delRowData(index);
            }
            else $("table[id$='" + gridid.substring(0, gridid.search("t") + 1) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + 1) + "_", "").replace("_t", ""));

        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemCombo3(value) {
            var el3 = document.createElement("div");
            var temp;
            if (value == "Llogari")
                temp = "<select  id='cmbLloji" + lastselsubgrid2 + lastselsub2 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid2 + "&quot;" + ",2,3,&quot;" + lastselsub2 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 selected='selected'>Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            else if (value == "Llogari standarte")
                temp = "<select  id='cmbLloji" + lastselsubgrid2 + lastselsub2 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid2 + "&quot;" + ",2,3,&quot;" + lastselsub2 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 selected='selected'>Llogari standarte</OPTION>";
            else temp = "<select  id='cmbLloji" + lastselsubgrid2 + lastselsub2 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid2 + "&quot;" + ",2,3,&quot;" + lastselsub2 + "&quot;)' ><OPTION value=1 selected='selected'>Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";

            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueCombo3(elem) {
            return jQuery('#cmbLloji' + lastselsubgrid2 + lastselsub2)[0][jQuery('#cmbLloji' + lastselsubgrid2 + lastselsub2)[0].selectedIndex].text;
        }
        //krijon elementin textbox per pershkrimin kur rreshti eshte ne editim
        function myElemPershkrimi3(value) {
            var el = document.createElement("div");
            el.innerHTML = "<input  id ='txtPershkrimi" + lastselsubgrid2 + lastselsub2 + "'  type ='text' onchange='changePershkrimi3()' onkeypress='keyPressPershkrimi3()' value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //funksioni kur shtypet nje karakter tek textboxi i pershkrimit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit
        //kontrollon nese per kete gride eksiston elementi prind
        function keyPressPershkrimi3() {
            var prind = lastselsubgrid2.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                if ($("table[id$='" + lastselsubgrid2 + "']").getInd(lastselsub2) == $("table[id$='" + lastselsubgrid2 + "']").getDataIDs().length - 1) {
                    var index2 = parseInt(lastselsub2) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid2 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid2 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid2 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + lastselsubgrid2 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid2 + index2 + "'  type ='checkbox' onchange='changeCheck3(" + index2 + "," + "&quot;" + lastselsubgrid2 + "&quot;" + ")' checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid2 + "']").addRowData(parseInt(lastselsub2) + 1, mydata2);

                }
                else if ($("table[id$='" + lastselsubgrid2 + "']").getDataIDs()[$("table[id$='" + lastselsubgrid2 + "']").getDataIDs().length - 1] == "" & $("table[id$='" + lastselsubgrid2 + "']").getInd(lastselsub2) == $("table[id$='" + lastselsubgrid2 + "']").getDataIDs().length - 2) {
                    var index2 = parseInt(lastselsub2) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid2 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid2 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid2 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + lastselsubgrid2 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid2 + index2 + "'  type ='checkbox' onchange='changeCheck3(" + index2 + "," + "&quot;" + lastselsubgrid2 + "&quot;" + ")'  checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid2 + "']").addRowData(parseInt(lastselsub2) + 1, mydata2);

                }
            }
        }
        //ruan te dhenat tek array nqs ndryshon pershkrimi
        function changePershkrimi3() {
            var o = $(document.activeElement).attr('id');
            newid = eksiston(3, lastprindi, lastselsub2);
            $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');

            arr[0][newid] = lastselsub2 + ":3"
            arr[1][newid] = lastselsub2 + ":" + lastprindi;
            arr[2][newid] = lastselsub2 + ":" + $("table[id$='" + lastselsubgrid2 + "']").getRowData(lastselsub2).txtPershkrimi;
            if ($("table[id$='" + lastselsubgrid2 + "']").getRowData(lastselsub2).cbTotali.search('CHECKED') != -1)
                arr[3][newid] = lastselsub2 + ":True";
            else arr[3][newid] = lastselsub2 + ":False";
            $("table[id$='" + lastselsubgrid2 + "']").editRow(lastselsub2);
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

            ruajpash();

        }
        //krijon elementin checkbox total nqs rreshti eshte ne editim
        function myelemTotal3(value, options) {
            var el3 = document.createElement("div");
            el3.innerHTML = "<input  id ='cbTotali" + lastselsubgrid2 + lastselsub2 + "' onchange='changePershkrimi3()'  type ='checkbox'  onBlur='lostFocusKoloneFundit3()' style='width: 100%' > ";
            if (value == 'false') el3.firstChild.checked = false;
            else if (value == 'true') el3.firstChild.checked = true;
            else if (value.toString().search('CHECKED') == -1) el3.firstChild.checked = false;
            else if (value.toString().search('CHECKED') != -1) el3.firstChild.checked = true;

            el3.value = value;
            return el3;
        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFundit3() {

            $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
            lastselsub2 = parseInt(lastselsub2) + 1;
            indexpasheditim = lastselsubgrid2 + lastselsub2;
            $("table[id$='" + lastselsubgrid2 + "']").editRow(lastselsub2);

        }

        //perdoret per te ruajtur te dhenat ne array kur ndryshon checkim i totalit kur rreshti nuk eshte ne editim
        function changeCheck3(id, gridid) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            newid = eksiston(3, pershkrimi, id);

            arr[0][newid] = id + ":3"
            arr[1][newid] = id + ":" + pershkrimi;
            arr[2][newid] = id + ":" + $("table[id$='" + gridid + "']").getRowData(id).txtPershkrimi;
            if (arr[3][newid] == id + ":False")
                arr[3][newid] = id + ":True";
            else arr[3][newid] = id + ":False";

            ruajpash();

        }
        //krijon elementin myvalueCheck kur rreshti nuk eshte ne editim
        function myvalueCheck3(elem) {
            if (elem[0].firstChild.checked == true)
                el3check = "<input  id ='cbTotali" + lastselsubgrid2 + lastselsub2 + "'  onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + lastselsubgrid2 + "&quot;" + ")' type ='checkbox' checked='checked' style='width: 100%'   ";
            else el3check = "<input  id ='cbTotali" + lastselsubgrid2 + lastselsub2 + "'  onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + lastselsubgrid2 + "&quot;" + ")' type ='checkbox'    style='width: 100%'   ";

            el3check += ">";
            return el3check;
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi te zerit te nivelit te trete
        function mbushGrideNgaHiddenFieldi3(pershk, gridid) {
            var hf5 = document.getElementById("gridaZerat");

            if (hf5.value != "") {
                var aktivet = hf5.value.split(';');
                var niveli = aktivet[0].split(',');
                var prindi = aktivet[1].split(',');
                var pershkrimi = aktivet[2].split(',');
                var totali = aktivet[3].split(',');

                lastselsub2 = 1;

                $("table[id$='" + gridid + "']").delRowData(1);
                for (var i = 0; i < niveli.length; i++) {
                    be = "<input id='butonFshi" + gridid + lastselsub2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + gridid + "&quot;" + "," + lastselsub2 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub2 + "'  type ='checkbox'  onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
                    el3check2 = "<input  id ='cbTotali" + gridid + lastselsub2 + "'  type ='checkbox'   onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + gridid + "&quot;" + ")'  style='width: 100%'  />   ";
                    if (niveli[i].split(':')[1] != 3)
                        continue;
                    else if (prindi[i].split(':')[1] == pershk) {
                        lloji = 'Zeri';
                        emertimi = (pershkrimi[i].split(':')[1] == undefined) ? "" : pershkrimi[i].split(':')[1];
                        tot = (totali[i].split(':')[1] == undefined) ? el3check : ((totali[i].split(':')[1] == "True") ? el3check : el3check2);
                        var datarow = { txtFshi: be, cmbLloji: lloji, txtPershkrimi: emertimi, cbTotali: tot };
                        var su;
                        if (emertimi != "") {
                            su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub2), datarow);

                            ;

                            lastselsub2 = lastselsub2 + 1;
                        }
                    }
                }
                if (lastselsub2 == 1) {
                    $("table[id$='" + gridid + "']").GridUnload("#" + $("table[id$='" + gridid + "']")[0].id);
                    inicializoSubGride(gridid);
                    mbushSubGrideNgaHiddenFieldi(pershk, gridid);
                }
                else {
                    be = "<input id='butonFshi" + gridid + lastselsub2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked3(" + "&quot;" + gridid + "&quot;" + "," + lastselsub2 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub2 + "'  type ='checkbox'  onchange='changeCheck3(" + lastselsub2 + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";

                    var datarow = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su;
                    su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub2), datarow);

                }
            }
        }
        function inicializoGride4(subgrid_table_id) {
            $("table[id$='" + subgrid_table_id + "']").jqGrid
        (
            {
                datatype: "local",
                colNames: [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3]],
                colModel: [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0] - 15, hidden: arrayVisibleKolonaGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon4, custom_value: myvalueFshi4} },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1] - 15, hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo4, custom_value: myvalueCombo4} },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2] - 41, hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi4, custom_value: myvalueNormal} },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3] - 15, hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal4, custom_value: myvalueCheck4} }
                    ],
                caption: "Niveli 4",
                sortable: false,
                cellsubmit: 'clientArray',
                height: 'auto',
                subGrid: true,
                subGridRowExpanded: function (subgrid_id, row_id) {
                    var subgrid_table_id3;
                    subgrid_table_id3 = subgrid_id + "_t";
                    var pershkrimi = "";
                    if ($("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi.search('txtPershkrimi') != -1)
                        pershkrimi = jQuery('#txtPershkrimi' + lastselsubgrid3 + lastselsub3)[0].value;
                    else pershkrimi = $("table[id$='" + subgrid_table_id + "']").getRowData(row_id).txtPershkrimi
                    lastprindi = pershkrimi;
                    $("div[id$='" + subgrid_id.replace('ASPxPageControl1_', '') + "']").html("<table id='" + subgrid_table_id3 + "' class='scroll'></table>");
                    inicializoGride5(subgrid_table_id3.replace("ASPxPageControl1_", ""));
                    mbushGrideNgaHiddenFieldi5(pershkrimi, subgrid_table_id3.replace("ASPxPageControl1_", ""));

                },
                onCellSelect: function (id, icol) {
                    $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='" + subgrid_table_id + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                    lastsel2 = 0;
                    lastselsub2 = 0;
                    lastselsub = 0;
                    lastselsub4 = 0;
                    lastselsub3 = id;
                    indexpasheditim = subgrid_table_id + lastselsub3;
                    lastselsubgrid3 = subgrid_table_id;
                    $("table[id$='" + subgrid_table_id + "']").editRow(id);
                    var idkontrolli = "#" + $("table[id$='" + subgrid_table_id + "']").getGridParam('colModel')[icol].index + lastselsubgrid3 + lastselsub3;
                    if (jQuery(idkontrolli)[0] != undefined) {
                        if (jQuery(idkontrolli)[0].isDisabled == false)
                            jQuery(idkontrolli)[0].focus();
                    }
                    keyPressPershkrimi4();
                }
            }
        );
            be = "<input id='butonFshi" + subgrid_table_id + lastselsub3 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + subgrid_table_id + lastselsub3 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + subgrid_table_id + lastselsub3 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + subgrid_table_id + "&quot;" + "," + lastselsub3 + ")'/>";
            el3check = "<input  id ='cbTotali" + subgrid_table_id + lastselsub3 + "'  type ='checkbox'  onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + subgrid_table_id + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check
            }
            ];
            for (var i = 0; i < mydata2.length; i++) {
                $("table[id$='" + subgrid_table_id + "']").addRowData(1, mydata2[i]);

            }

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit

        function myElemButon4() {
            el = "<input id='butonFshi" + lastselsubgrid3 + lastselsub3 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid3 + lastselsub3 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid3 + lastselsub3 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + lastselsubgrid3 + "&quot;" + "," + lastselsub3 + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim
        function myvalueFshi4(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastselsubgrid3 + lastselsub3 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid3 + lastselsub3 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid3 + lastselsub3 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + lastselsubgrid3 + "&quot;" + "," + lastselsub3 + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat dhe mbyll griden nqs eshte rreshti i fundit
        function fshiClicked4(gridid, index) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            lastprindi = pershkrimi;
            newid = eksiston(4, lastprindi, index);
            $("table[id$='" + gridid + "']").saveRow(lastselsub3, false, 'clientArray');
            if (arr[2][newid] != undefined) {
                fshibij(5, arr[2][newid].split(':')[1], "PASH");
                arr[2][newid] = arr[2][newid].split(':')[0] + ":";
                arr[3][newid] = arr[3][newid].split(':')[0] + ":True";

            }
            ruajpash();
            indexpasheditim = 0;
            if ($("table[id$='" + gridid + "']").getGridParam('reccount') > 1) {
                $("table[id$='" + gridid + "']").collapseSubGridRow(index);
                $("table[id$='" + gridid + "']").delRowData(index);
            }
            else $("table[id$='" + gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2) + "_", "").replace("_t", ""));

        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemCombo4(value) {
            var el3 = document.createElement("div");
            var temp;
            if (value == "Llogari")
                temp = "<select  id='cmbLloji" + lastselsubgrid3 + lastselsub3 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid3 + "&quot;" + ",2,4,&quot;" + lastselsub3 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 selected='selected'>Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            else if (value == "Llogari standarte")
                temp = "<select  id='cmbLloji" + lastselsubgrid3 + lastselsub3 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid3 + "&quot;" + ",2,4,&quot;" + lastselsub3 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 selected='selected'>Llogari standarte</OPTION>";
            else temp = "<select  id='cmbLloji" + lastselsubgrid3 + lastselsub3 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid3 + "&quot;" + ",2,4,&quot;" + lastselsub3 + "&quot;)' ><OPTION value=1 selected='selected'>Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";

            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueCombo4(elem) {
            return jQuery('#cmbLloji' + lastselsubgrid3 + lastselsub3)[0][jQuery('#cmbLloji' + lastselsubgrid3 + lastselsub3)[0].selectedIndex].text;
        }
        //krijon elementin textbox per pershkrimin kur rreshti eshte ne editim
        function myElemPershkrimi4(value) {
            var el = document.createElement("div");
            el.innerHTML = "<input  id ='txtPershkrimi" + lastselsubgrid3 + lastselsub3 + "'  type ='text'onchange='changePershkrimi4()' onkeypress='keyPressPershkrimi4()' value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //funksioni kur shtypet nje karakter tek textboxi i pershkrimit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit
        //kontrollon nese per kete gride eksiston elementi prind
        function keyPressPershkrimi4() {
            var prind = lastselsubgrid3.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                if ($("table[id$='" + lastselsubgrid3 + "']").getInd(lastselsub3) == $("table[id$='" + lastselsubgrid3 + "']").getDataIDs().length - 1) {
                    var index2 = parseInt(lastselsub3) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid3 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid3 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid3 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + lastselsubgrid3 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid3 + index2 + "'  type ='checkbox' onchange='changeCheck4(" + index2 + "," + "&quot;" + lastselsubgrid3 + "&quot;" + ")'  checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid3 + "']").addRowData(parseInt(lastselsub3) + 1, mydata2);

                }
                else if ($("table[id$='" + lastselsubgrid3 + "']").getDataIDs()[$("table[id$='" + lastselsubgrid3 + "']").getDataIDs().length - 1] == "" & $("table[id$='" + lastselsubgrid3 + "']").getInd(lastselsub3) == $("table[id$='" + lastselsubgrid3 + "']").getDataIDs().length - 2) {
                    var index2 = parseInt(lastselsub3) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid3 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid3 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid3 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + lastselsubgrid3 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid3 + index2 + "'  type ='checkbox' onchange='changeCheck4(" + index2 + "," + "&quot;" + lastselsubgrid3 + "&quot;" + ")'  checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid3 + "']").addRowData(parseInt(lastselsub3) + 1, mydata2);

                }
            }
        }
        //ruan te dhenat tek array nqs ndryshon pershkrimi
        function changePershkrimi4() {
            var o = $(document.activeElement).attr('id');
            newid = eksiston(4, lastprindi, lastselsub3);
            $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');

            arr[0][newid] = lastselsub3 + ":4"
            arr[1][newid] = lastselsub3 + ":" + lastprindi;
            arr[2][newid] = lastselsub3 + ":" + $("table[id$='" + lastselsubgrid3 + "']").getRowData(lastselsub3).txtPershkrimi;
            if ($("table[id$='" + lastselsubgrid3 + "']").getRowData(lastselsub3).cbTotali.search('CHECKED') != -1)
                arr[3][newid] = lastselsub3 + ":True";
            else arr[3][newid] = lastselsub3 + ":False";
            $("table[id$='" + lastselsubgrid3 + "']").editRow(lastselsub3);
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

            ruajpash();

        }
        //krijon elementin checkbox total nqs rreshti eshte ne editim
        function myelemTotal4(value, options) {
            var el3 = document.createElement("div");
            el3.innerHTML = "<input  id ='cbTotali" + lastselsubgrid3 + lastselsub3 + "'  type ='checkbox' onchange='changePershkrimi4()' onBlur='lostFocusKoloneFundit4()' style='width: 100%' > ";
            if (value == 'false') el3.firstChild.checked = false;
            else if (value == 'true') el3.firstChild.checked = true;
            else if (value.toString().search('CHECKED') == -1) el3.firstChild.checked = false;
            else if (value.toString().search('CHECKED') != -1) el3.firstChild.checked = true;

            el3.value = value;
            return el3;
        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFundit4() {

            $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
            lastselsub3 = parseInt(lastselsub3) + 1;
            indexpasheditim = lastselsubgrid3 + lastselsub3;
            $("table[id$='" + lastselsubgrid3 + "']").editRow(lastselsub3);

        }

        //perdoret per te ruajtur te dhenat ne array kur ndryshon checkim i totalit kur rreshti nuk eshte ne editim
        function changeCheck4(id, gridid) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            newid = eksiston(4, pershkrimi, id);

            arr[0][newid] = id + ":4"
            arr[1][newid] = id + ":" + pershkrimi;
            arr[2][newid] = id + ":" + $("table[id$='" + gridid + "']").getRowData(id).txtPershkrimi;
            if (arr[3][newid] == id + ":False")
                arr[3][newid] = id + ":True";
            else arr[3][newid] = id + ":False";

            ruajpash();

        }
        //krijon elementin myvalueCheck kur rreshti nuk eshte ne editim
        function myvalueCheck4(elem) {
            if (elem[0].firstChild.checked == true)
                el3check = "<input  id ='cbTotali" + lastselsubgrid3 + lastselsub3 + "'  type ='checkbox' onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + lastselsubgrid3 + "&quot;" + ")' checked='checked' style='width: 100%'   ";
            else el3check = "<input  id ='cbTotali" + lastselsubgrid3 + lastselsub3 + "'  type ='checkbox' onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + lastselsubgrid3 + "&quot;" + ")'   style='width: 100%'   ";

            el3check += ">";
            return el3check;
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi te zerit te nivelit te katert
        function mbushGrideNgaHiddenFieldi4(pershk, gridid) {
            var hf5 = document.getElementById("gridaZerat");

            if (hf5.value != "") {
                var aktivet = hf5.value.split(';');
                var niveli = aktivet[0].split(',');
                var prindi = aktivet[1].split(',');
                var pershkrimi = aktivet[2].split(',');
                var totali = aktivet[3].split(',');
                lastselsub3 = 1;

                $("table[id$='" + gridid + "']").delRowData(1);

                for (var i = 0; i < niveli.length; i++) {
                    be = "<input id='butonFshi" + gridid + lastselsub3 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub3 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub3 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + gridid + "&quot;" + "," + lastselsub3 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub3 + "'  type ='checkbox' onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
                    el3check2 = "<input  id ='cbTotali" + gridid + lastselsub3 + "'  type ='checkbox' onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + gridid + "&quot;" + ")'   style='width: 100%'  />   ";
                    if (niveli[i].split(':')[1] != 4)
                        continue;
                    else if (prindi[i].split(':')[1] == pershk) {
                        lloji = 'Zeri';
                        emertimi = (pershkrimi[i].split(':')[1] == undefined) ? "" : pershkrimi[i].split(':')[1];
                        tot = (totali[i].split(':')[1] == undefined) ? el3check : ((totali[i].split(':')[1] == "True") ? el3check : el3check2);
                        var datarow = { txtFshi: be, cmbLloji: lloji, txtPershkrimi: emertimi, cbTotali: tot };
                        var su;
                        if (emertimi != "") {
                            su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub3), datarow);

                            ;

                            lastselsub3 = lastselsub3 + 1;
                        }
                    }
                }
                if (lastselsub3 == 1) {
                    $("table[id$='" + gridid + "']").GridUnload("#" + $("table[id$='" + gridid + "']")[0].id);
                    inicializoSubGride(gridid);
                    mbushSubGrideNgaHiddenFieldi(pershk, gridid);
                }
                else {
                    be = "<input id='butonFshi" + gridid + lastselsub3 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub3 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub3 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked4(" + "&quot;" + gridid + "&quot;" + "," + lastselsub3 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub3 + "'  type ='checkbox' onchange='changeCheck4(" + lastselsub3 + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";

                    var datarow = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su;
                    su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub3), datarow);

                }
            }
        }
        function inicializoGride5(subgrid_table_id) {
            $("table[id$='" + subgrid_table_id + "']").jqGrid
        (
            {
                datatype: "local",
                colNames: [arrayPershkrimiKolonaGrides[0], arrayPershkrimiKolonaGrides[1], arrayPershkrimiKolonaGrides[2], arrayPershkrimiKolonaGrides[3]],
                colModel: [
                    { name: arrayIdKolonaGrides[0], index: arrayIdKolonaGrides[0], width: arrayWidthKolonaGrides[0] - 15, hidden: arrayVisibleKolonaGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButon5, custom_value: myvalueFshi5} },
                    { name: arrayIdKolonaGrides[1], index: arrayIdKolonaGrides[1], width: arrayWidthKolonaGrides[1] - 15, hidden: arrayVisibleKolonaGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemCombo5, custom_value: myvalueCombo5} },
                    { name: arrayIdKolonaGrides[2], index: arrayIdKolonaGrides[2], width: arrayWidthKolonaGrides[2] - 44, hidden: arrayVisibleKolonaGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimi5, custom_value: myvalueNormal} },
                    { name: arrayIdKolonaGrides[3], index: arrayIdKolonaGrides[3], width: arrayWidthKolonaGrides[3] - 20, hidden: arrayVisibleKolonaGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myelemTotal5, custom_value: myvalueCheck5} }
                    ],
                caption: "Niveli 5",
                sortable: false,
                cellsubmit: 'clientArray',
                height: 'auto',

                onCellSelect: function (id, icol) {
                    $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                    $("table[id$='" + subgrid_table_id + "']").saveRow(lastselsub4, false, 'clientArray');
                    $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
                    $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
                    lastsel2 = 0;
                    lastselsub2 = 0;
                    lastselsub3 = 0;
                    lastselsub = 0;
                    lastselsub4 = id;
                    lastselsubgrid4 = subgrid_table_id;
                    indexpasheditim = subgrid_table_id + lastselsub4;
                    $("table[id$='" + subgrid_table_id + "']").editRow(id);
                    var idkontrolli = "#" + $("table[id$='" + subgrid_table_id + "']").getGridParam('colModel')[icol].index + lastselsubgrid4 + lastselsub4;
                    if (jQuery(idkontrolli)[0] != undefined) {
                        if (jQuery(idkontrolli)[0].isDisabled == false)
                            jQuery(idkontrolli)[0].focus();
                    } keyPressPershkrimi5();
                }
            }
        );
            be = "<input id='butonFshi" + subgrid_table_id + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + subgrid_table_id + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + subgrid_table_id + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + subgrid_table_id + "&quot;" + "," + lastselsub4 + ")'/>";
            el3check = "<input  id ='cbTotali" + subgrid_table_id + lastselsub4 + "'  type ='checkbox' onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + subgrid_table_id + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check
            }
            ];
            for (var i = 0; i < mydata2.length; i++) {
                $("table[id$='" + subgrid_table_id + "']").addRowData(1, mydata2[i]);

            }

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit

        function myElemButon5() {
            el = "<input id='butonFshi" + lastselsubgrid4 + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + lastselsub4 + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim

        function myvalueFshi5(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastselsubgrid4 + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + lastselsub4 + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat dhe mbyll griden nqs eshte rreshti i fundit
        function fshiClicked5(gridid, index) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            lastprindi = pershkrimi;
            newid = eksiston(5, lastprindi, index);
            $("table[id$='" + gridid + "']").saveRow(lastselsub4, false, 'clientArray');
            if (arr[2][newid] != undefined) {
                arr[2][newid] = arr[2][newid].split(':')[0] + ":";
                arr[3][newid] = arr[3][newid].split(':')[0] + ":True";
            }
            ruajpash();
            indexpasheditim = 0;
            if ($("table[id$='" + gridid + "']").getGridParam('reccount') > 1) {
                $("table[id$='" + gridid + "']").collapseSubGridRow(index);
                $("table[id$='" + gridid + "']").delRowData(index);
            }
            else $("table[id$='" + gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + gridid.substring(gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2).search("t") + 3) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + gridid.substring(gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2).search("t") + 3) + "_", "").replace("_t", ""));

        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemCombo5(value) {
            var el3 = document.createElement("div");
            var temp;
            if (value == "Llogari")
                temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",2,5,&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 selected='selected'>Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            else if (value == "Llogari standarte")
                temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",2,5,&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 selected='selected'>Llogari standarte</OPTION>";
            else temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",2,5,&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 selected='selected'>Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueCombo5(elem) {
            return jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0][jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].selectedIndex].text;
        }
        //krijon elementin textbox per pershkrimin kur rreshti eshte ne editim
        function myElemPershkrimi5(value) {
            var el = document.createElement("div");
            el.innerHTML = "<input  id ='txtPershkrimi" + lastselsubgrid4 + lastselsub4 + "' onchange='changePershkrimi5()' type ='text' onkeypress='keyPressPershkrimi5()' value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //funksioni kur shtypet nje karakter tek textboxi i pershkrimit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit
        //kontrollon nese per kete gride eksiston elementi prind
        function keyPressPershkrimi5() {
            var prind = lastselsubgrid4.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                if ($("table[id$='" + lastselsubgrid4 + "']").getInd(lastselsub4) == $("table[id$='" + lastselsubgrid4 + "']").getDataIDs().length - 1) {
                    var index2 = parseInt(lastselsub4) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid4 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid4 + index2 + "'  type ='checkbox' onchange='changeCheck5(" + index2 + "," + "&quot;" + lastselsubgrid4 + "&quot;" + ")''  checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid4 + "']").addRowData(parseInt(lastselsub4) + 1, mydata2);

                }
                else if ($("table[id$='" + lastselsubgrid4 + "']").getDataIDs()[$("table[id$='" + lastselsubgrid4 + "']").getDataIDs().length - 1] == "" & $("table[id$='" + lastselsubgrid4 + "']").getInd(lastselsub4) == $("table[id$='" + lastselsubgrid4 + "']").getDataIDs().length - 2) {
                    var index2 = parseInt(lastselsub4) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid4 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index2 + ")'/>";

                    el3check = "<input  id ='cbTotali" + lastselsubgrid4 + index2 + "'  type ='checkbox' onchange='changeCheck5(" + index2 + "," + "&quot;" + lastselsubgrid4 + "&quot;" + ")'   checked='checked'  style='width: 100%'   />";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su = $("table[id$='" + lastselsubgrid4 + "']").addRowData(parseInt(lastselsub4) + 1, mydata2);

                }
            }
        }
        //ruan te dhenat tek array nqs ndryshon pershkrimi
        function changePershkrimi5() {
            var o = $(document.activeElement).attr('id');
            newid = eksiston(5, lastprindi, lastselsub4);
            $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');

            arr[0][newid] = lastselsub4 + ":5"
            arr[1][newid] = lastselsub4 + ":" + lastprindi;
            arr[2][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtPershkrimi;
            if ($("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cbTotali.search('CHECKED') != -1)
                arr[3][newid] = lastselsub4 + ":True";
            else arr[3][newid] = lastselsub4 + ":False";
            $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4);
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

            ruajpash();

        }
        //krijon elementin checkbox total nqs rreshti eshte ne editim
        function myelemTotal5(value, options) {
            var el3 = document.createElement("div");
            el3.innerHTML = "<input  id ='cbTotali" + lastselsubgrid4 + lastselsub4 + "'  onchange='changePershkrimi5()'  type ='checkbox'  onBlur='lostFocusKoloneFundit5()' style='width: 100%' > ";
            if (value == 'false') el3.firstChild.checked = false;
            else if (value == 'true') el3.firstChild.checked = true;
            else if (value.toString().search('CHECKED') == -1) el3.firstChild.checked = false;
            else if (value.toString().search('CHECKED') != -1) el3.firstChild.checked = true;

            el3.value = value;
            return el3;
        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFundit5() {

            $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
            lastselsub4 = parseInt(lastselsub4) + 1;
            indexpasheditim = lastselsubgrid4 + lastselsub4;
            $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4);

        }
        //perdoret per te ruajtur te dhenat ne array kur ndryshon checkim i totalit kur rreshti nuk eshte ne editim
        function changeCheck5(id, gridid) {
            var prind = gridid.split('_');
            if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
            else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            newid = eksiston(5, pershkrimi, id);

            arr[0][newid] = id + ":5"
            arr[1][newid] = id + ":" + pershkrimi;
            arr[2][newid] = id + ":" + $("table[id$='" + gridid + "']").getRowData(id).txtPershkrimi;
            if (arr[3][newid] == id + ":False")
                arr[3][newid] = id + ":True";
            else arr[3][newid] = id + ":False";

            ruajpash();

        }
        //krijon elementin myvalueCheck kur rreshti nuk eshte ne editim
        function myvalueCheck5(elem) {
            if (elem[0].firstChild.checked == true)
                el3check = "<input  id ='cbTotali" + lastselsubgrid4 + lastselsub4 + "' onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + lastselsubgrid4 + "&quot;" + ")'  type ='checkbox' checked='checked' style='width: 100%'   ";
            else el3check = "<input  id ='cbTotali" + lastselsubgrid4 + lastselsub4 + "' onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + lastselsubgrid4 + "&quot;" + ")'  type ='checkbox'    style='width: 100%'   ";

            el3check += ">";
            return el3check;
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi te zerit te nivelit te peste
        function mbushGrideNgaHiddenFieldi5(pershk, gridid) {
            var hf5 = document.getElementById("gridaZerat");

            if (hf5.value != "") {
                var aktivet = hf5.value.split(';');
                var niveli = aktivet[0].split(',');
                var prindi = aktivet[1].split(',');
                var pershkrimi = aktivet[2].split(',');
                var totali = aktivet[3].split(',');

                lastselsub4 = 1;

                $("table[id$='" + gridid + "']").delRowData(1);
                for (var i = 0; i < niveli.length; i++) {
                    be = "<input id='butonFshi" + gridid + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + gridid + "&quot;" + "," + lastselsub4 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub4 + "'  type ='checkbox' onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + gridid + "&quot;" + ")'  checked='checked' style='width: 100%'  />   ";
                    el3check2 = "<input  id ='cbTotali" + gridid + lastselsub4 + "'  type ='checkbox'   onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + gridid + "&quot;" + ")' style='width: 100%'  />   ";
                    if (niveli[i].split(':')[1] != 5)
                        continue;
                    else if (prindi[i].split(':')[1] == pershk) {
                        lloji = 'Zeri';
                        emertimi = (pershkrimi[i].split(':')[1] == undefined) ? "" : pershkrimi[i].split(':')[1];
                        tot = (totali[i].split(':')[1] == undefined) ? el3check : ((totali[i].split(':')[1] == "True") ? el3check : el3check2);
                        var datarow = { txtFshi: be, cmbLloji: lloji, txtPershkrimi: emertimi, cbTotali: tot };
                        var su;
                        if (emertimi != "") {
                            su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub4), datarow);

                            ;

                            lastselsub4 = lastselsub4 + 1;
                        }
                    }
                }
                if (lastselsub4 == 1) {
                    $("table[id$='" + gridid + "']").GridUnload("#" + $("table[id$='" + gridid + "']")[0].id);
                    inicializoSubGride(gridid);
                    mbushSubGrideNgaHiddenFieldi(pershk, gridid);
                }
                else {
                    be = "<input id='butonFshi" + gridid + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClicked5(" + "&quot;" + gridid + "&quot;" + "," + lastselsub4 + ")'/>";
                    el3check = "<input  id ='cbTotali" + gridid + lastselsub4 + "'  type ='checkbox' onchange='changeCheck5(" + lastselsub4 + "," + "&quot;" + gridid + "&quot;" + ")'   checked='checked' style='width: 100%'  />   ";

                    var datarow = { txtFshi: be, cmbLloji: "", txtPershkrimi: "", cbTotali: el3check };
                    var su;
                    su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub4), datarow);

                }
            }
        }
        //formon array per konfigurimin e grides
        function formoArrayKolonaGrides() {
            var arr = $("input[id$='hfKolonaGride']")[0].value.split('||');
            var tempArray;
            var rendit = new Array();
            for (var i = 0; i < arr.length - 1; i++) {
                tempArray = arr[i].split('|');
                arrayIdKolonaGrides[i] = tempArray[0];
                arrayPershkrimiKolonaGrides[i] = tempArray[1];
                if (tempArray[2] == 'True') {
                    arrayVisibleKolonaGrides[i] = false;
                }
                else {
                    arrayVisibleKolonaGrides[i] = true;
                }
                arrayReadOnlyKolonaGrides[i] = tempArray[3];
                arrayWidthKolonaGrides[i] = tempArray[4];
                rendit[i] = tempArray[5];
            }
            var j = 0;
            while (j < 4) {
                for (k = 0; k < rendit.length; k++) {
                    if (rendit[k] == j) {
                        arrayRenditjeKolonaGrides[j] = k;
                    }
                }
                j++;
            }
        } //perdoret per te zvogeluar madhesine e grides se llogarive sipas nivelit
        function gjatesia(gridid) {
            var i = gridid.split("t").length - 1;

            var l = 0;
            if (i == 2)
                l = 6;
            else if (i == 3)
                l = 12;
            else if (i == 4)
                l = 18;
            return l;
        }
        //inicializon griden sipas te dhenave te ruajtura ne database per llogarite dhe ka funksione  per editimin e reshtave

        function inicializoSubGride(subgrid_table_id) {
            var l = gjatesia("'" + subgrid_table_id + "'");
            $("table[id$='" + subgrid_table_id + "']").jqGrid
    ({
        datatype: "local",
        colNames: [arrayPershkrimiKolonaSubGrides[0], arrayPershkrimiKolonaSubGrides[1], arrayPershkrimiKolonaSubGrides[2], arrayPershkrimiKolonaSubGrides[3], arrayPershkrimiKolonaSubGrides[4], arrayPershkrimiKolonaSubGrides[5]],
        colModel: [
            { name: arrayIdKolonaSubGrides[0], index: arrayIdKolonaSubGrides[0], width: arrayWidthKolonaSubGrides[0], hidden: arrayVisibleKolonaSubGrides[0], editable: true, sorttype: "int", edittype: 'custom', editoptions: { custom_element: myElemButonS, custom_value: myvalueFshiS} },
            { name: arrayIdKolonaSubGrides[1], index: arrayIdKolonaSubGrides[1], width: arrayWidthKolonaSubGrides[1] - l, hidden: arrayVisibleKolonaSubGrides[1], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboS, custom_value: myvalueComboS} },
            { name: arrayIdKolonaSubGrides[2], index: arrayIdKolonaSubGrides[2], width: arrayWidthKolonaSubGrides[2] - l, hidden: arrayVisibleKolonaSubGrides[2], editable: true, edittype: 'custom', editoptions: { custom_element: myelemKodi, custom_value: myvalue} },
            { name: arrayIdKolonaSubGrides[3], index: arrayIdKolonaSubGrides[3], width: arrayWidthKolonaSubGrides[3] - l, hidden: arrayVisibleKolonaSubGrides[3], editable: true, edittype: 'custom', editoptions: { custom_element: myElemPershkrimiLlogaria, custom_value: myvalueNormal} },
            { name: arrayIdKolonaSubGrides[4], index: arrayIdKolonaSubGrides[4], width: arrayWidthKolonaSubGrides[4] - l, hidden: arrayVisibleKolonaSubGrides[4], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboGjendja, custom_value: myvalueComboGj} },
            { name: arrayIdKolonaSubGrides[5], index: arrayIdKolonaSubGrides[5], width: arrayWidthKolonaSubGrides[5] - l, hidden: arrayVisibleKolonaSubGrides[5], editable: true, edittype: 'custom', editoptions: { custom_element: myelemComboShenja, custom_value: myvalueComboSh} }
            ],
        caption: "Llogarite",
        sortable: true,
        cellsubmit: 'clientArray',
        height: 'auto',
        onCellSelect: function (id, icol) {
            $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
            $("table[id$='" + subgrid_table_id + "']").saveRow(lastselsub4, false, 'clientArray');
            $("table[id$='rowed5']").saveRow(lastsel2, false, 'clientArray');
            $("table[id$='" + lastselsubgrid2 + "']").saveRow(lastselsub2, false, 'clientArray');
            $("table[id$='" + lastselsubgrid3 + "']").saveRow(lastselsub3, false, 'clientArray');
            $("table[id$='" + lastselsubgrid + "']").saveRow(lastselsub, false, 'clientArray');
            lastsel2 = 0;
            lastselsub2 = 0;
            lastselsub3 = 0;
            lastselsub = 0;
            lastselsub4 = id;
            lastselsubgrid4 = subgrid_table_id;
            indexpasheditim = 0;
            $("table[id$='" + subgrid_table_id + "']").editRow(id);
            var idkontrolli = "#" + $("table[id$='" + subgrid_table_id + "']").getGridParam('colModel')[icol].index + lastselsubgrid4 + lastselsub4;
            if (jQuery(idkontrolli)[0] != undefined) {
                if (jQuery(idkontrolli)[0].isDisabled == false)
                    jQuery(idkontrolli)[0].focus();
            } keyPressKodi();
        }
    }

       );
            be = "<input id='butonFshi" + subgrid_table_id + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + subgrid_table_id + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + subgrid_table_id + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + subgrid_table_id + "&quot;" + "," + lastselsub4 + ")'/>";
            var mydata2 = [{ txtFshi: be, cmbLloji: "", txtKodi: "", txtPershkrimiLlogaria: "", cmbGjendja: "", cmbShenja: ""
            }
            ];
            for (var i = 0; i < mydata2.length; i++)
                $("table[id$='" + subgrid_table_id + "']").addRowData(1, mydata2[i]);

        }
        //krijon elementin buton per fshirjen gjate editimit te reshtit
        function myElemButonS() {
            el = "<input id='butonFshi" + lastselsubgrid4 + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + lastselsub4 + ")'/>";
            return el;
        }
        //krijon elementin buton per fshirjen qe shfaqet kur rreshti nuk eshte ne editim
        function myvalueFshiS(elem) {
            var index = parseInt(lastsel2);

            be = "<input id='butonFshi" + lastselsubgrid4 + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + lastselsub4 + ")'/>";
            return be;
        }
        //funksionin per fshirjen e rreshtit. Ben pastrim e reshtit edhe nga array ku ruhen te dhenat dhe mbyll griden nqs eshte rreshti i fundit
        function fshiClickedS(gridid, index) {
            var pershkrimi;
            var prind = gridid.split('_');
            if (prind.length == 3) {
                if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            }
            else if (prind.length == 5) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            }
            else if (prind.length == 7) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            }
            else if (prind.length == 9) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            }
            lastprindi = pershkrimi;
            $("table[id$='" + gridid + "']").saveRow(index, false, 'clientArray');
            var lloji = $("table[id$='" + gridid + "']").getRowData(index).cmbLloji;
            newid = eksistonLL("PASH", lastprindi, index, lloji);
            if (arrLL[0][newid] != undefined) {
                arrLL[0][newid] = arrLL[0][newid].split(':')[0] + ":";
                arrLL[1][newid] = arrLL[1][newid].split(':')[0] + ":";
                arrLL[2][newid] = arrLL[2][newid].split(':')[0] + ":Gjithmone";
                arrLL[3][newid] = arrLL[3][newid].split(':')[0] + ":Pozitive";
                arrLL[4][newid] = arrLL[4][newid].split(':')[0] + ":";
                arrLL[5][newid] = arrLL[5][newid].split(':')[0] + ":PASH";
                arrLL[6][newid] = arrLL[6][newid].split(':')[0] + ":";
            }
            ruajllogari();
            indexpasheditim = 0;
            if ($("table[id$='" + gridid + "']").getGridParam('reccount') > 1) {
                $("table[id$='" + gridid + "']").collapseSubGridRow(index);
                $("table[id$='" + gridid + "']").delRowData(index);
            }

            else {
                var i = gridid.split("t").length - 1;
                if (i == 1)
                    $("table[id$='rowed5']").collapseSubGridRow(gridid.replace("rowed5_", "").replace("_t", ""));
                else if (i == 2)
                    $("table[id$='" + gridid.substring(0, gridid.search("t") + 1) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + 1) + "_", "").replace("_t", ""));
                else if (i == 3)
                    $("table[id$='" + gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2) + "_", "").replace("_t", ""));
                else if (i == 4)
                    $("table[id$='" + gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + gridid.substring(gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2).search("t") + 3) + "']").collapseSubGridRow(gridid.replace(gridid.substring(0, gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + gridid.substring(gridid.search("t") + gridid.substring(gridid.search("t") + 1).search("t") + 2).search("t") + 3) + "_", "").replace("_t", ""));
            }
        }
        //krijon elementin combo per llojin kur reshti editohet
        function myelemComboS(value) {
            var el3 = document.createElement("div");
            var temp;
            if (value == "Zeri")
                temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",1," + lastselsubgrid4.split('t').length + ",&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 selected='selected'>Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";
            else if (value == "Llogari standarte")
                temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",1," + lastselsubgrid4.split('t').length + ",&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 >Llogari</OPTION><OPTION value=3 selected='selected'>Llogari standarte</OPTION>";
            else temp = "<select  id='cmbLloji" + lastselsubgrid4 + lastselsub4 + "' onchange='changeGrid(" + "&quot;" + lastselsubgrid4 + "&quot;" + ",1," + lastselsubgrid4.split('t').length + ",&quot;" + lastselsub4 + "&quot;)' ><OPTION value=1 >Zeri</OPTION><OPTION value=2 selected='selected'>Llogari</OPTION><OPTION value=3 >Llogari standarte</OPTION>";


            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;
        }
        //krijon elementin value combo per llojin kur reshti nuk eshte ne editim
        function myvalueComboS(elem) {
            return jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0][jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].selectedIndex].text;
        }
        //krijon elementin text per kodin kur reshti editohet
        function myelemKodi(value, options) {
            var el3 = document.createElement("div");

            el3.innerHTML = "<input  id ='txtKodi" + lastselsubgrid4 + lastselsub4 + "'  type ='text' onBlur ='pershkrimiLLog()'  onchange='changeKodi()'  onkeypress='keyPressKodi()' value='" + value + "' style='width: 80%'><INPUT type='button' value='...' name='button1' onclick ='ButtonClickKodi()' style='width: 20%' > ";
            el3.value = value;

            return el3;
        }
        var llojllog;
        //funksioni per hapjen e popupit te llogarive
        function ButtonClickKodi() {
            llojllog = "aktive";
            editorGlobal = $('#txtKodi' + lastselsubgrid4 + lastselsub4);
            editorPershkrimillogaria = $('#txtPershkrimiLlogaria' + lastselsubgrid4 + lastselsub4);
            var prind = lastselsubgrid4.split('_');
            if (prind.length == 3) {
                if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            }
            else if (prind.length == 5) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            }
            else if (prind.length == 7) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            }
            else if (prind.length == 9) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            }
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                var lloji = $('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].value;
                if (lloji == 2) {
                    popupUniversal.SetHeaderText('Zgjidh llogarine');
                    document.getElementById('<%= container.ClientID %>').src = 'LupaLlogaria.aspx';
                    popupUniversal.Show();
                }
                else if (lloji == 3) {
                    popupUniversal.SetHeaderText('Zgjidh llogarine standarte');
                    document.getElementById('<%= container.ClientID %>').src = 'LupaKpf.aspx?id=1';

                    popupUniversal.Show();
                }

            }

        }
        //funksioni per ruajtjen e ndryshimeve te llogarive ne array
        function changeKodi() {
            var o = $(document.activeElement).attr('id');

            var prind = lastselsubgrid4.split('_');
            if (prind.length == 3) {
                if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            }
            else if (prind.length == 5) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            }
            else if (prind.length == 7) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            }
            else if (prind.length == 9) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            }
            lastprindi = pershkrimi;
            var lloji = jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0][jQuery('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].selectedIndex].text;

            var kodet = $('#txtKodi' + lastselsubgrid4 + lastselsub4)[0].value.split(',');
            var pershkrimet = $('#txtPershkrimiLlogaria' + lastselsubgrid4 + lastselsub4)[0].value.split(',');
            if (kodet.length > 1) {
                $('#txtKodi' + lastselsubgrid4 + lastselsub4)[0].value = kodet[0];
                $('#txtPershkrimiLlogaria' + lastselsubgrid4 + lastselsub4)[0].value = pershkrimet[0];


                var rreshtaTeGrides = $("table[id$='" + lastselsubgrid4 + "']").getRowData();
                var indexe = $("table[id$='" + lastselsubgrid4 + "']").getDataIDs();
                var ind = 0;
                for (i = 0; i < rreshtaTeGrides.length; i++) {
                    if (rreshtaTeGrides[i].txtKodi.toString().search('value') != -1)
                        ind = i;
                }
                for (m = ind + 1; m < indexe.length; m++) {

                    fshiClickedS(lastselsubgrid4, indexe[m]);
                }
                var index = parseInt(lastselsub4) + 1;
                for (r = 1; r < kodet.length; r++) {

                    newid = eksistonLL("PASH", lastprindi, index, lloji);

                    arrLL[0][newid] = index + ":" + kodet[r];
                    arrLL[1][newid] = index + ":" + pershkrimet[r];
                    arrLL[2][newid] = index + ":" + "Gjithmone";
                    arrLL[3][newid] = index + ":" + "Pozitive";
                    arrLL[4][newid] = index + ":" + lastprindi;
                    arrLL[5][newid] = index + ":PASH";
                    arrLL[6][newid] = index + ":" + lloji;
                    ruajllogari();


                    be = "<input id='butonFshi" + lastselsubgrid4 + index + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index + ")'/>";
                    var mydata2 = { txtFshi: be, cmbLloji: lloji, txtKodi: kodet[r], txtPershkrimiLlogaria: pershkrimet[r], cmbGjendja: "Gjithmone", cmbShenja: "Pozitive" };
                    var su = $("table[id$='" + lastselsubgrid4 + "']").addRowData(parseInt(index), mydata2, '');
                    index++;

                }
                for (m = ind + 1; m < rreshtaTeGrides.length; m++) {
                    newid = eksistonLL("PASH", lastprindi, index, rreshtaTeGrides[m].cmbLloji);

                    arrLL[0][newid] = index + ":" + rreshtaTeGrides[m].txtKodi;
                    arrLL[1][newid] = index + ":" + rreshtaTeGrides[m].txtPershkrimiLlogaria;
                    arrLL[2][newid] = index + ":" + rreshtaTeGrides[m].cmbGjendja;
                    arrLL[3][newid] = index + ":" + rreshtaTeGrides[m].cmbShenja;
                    arrLL[4][newid] = index + ":" + lastprindi;
                    arrLL[5][newid] = index + ":PASH";
                    arrLL[6][newid] = index + ":" + rreshtaTeGrides[m].cmbLloji;
                    ruajllogari();

                    be = "<input id='butonFshi" + lastselsubgrid4 + index + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index + ")'/>";
                    var mydata2 = { txtFshi: be, cmbLloji: rreshtaTeGrides[m].cmbLloji, txtKodi: rreshtaTeGrides[m].txtKodi, txtPershkrimiLlogaria: rreshtaTeGrides[m].txtPershkrimiLlogaria, cmbGjendja: rreshtaTeGrides[m].cmbGjendja, cmbShenja: rreshtaTeGrides[m].cmbShenja };
                    var su = $("table[id$='" + lastselsubgrid4 + "']").addRowData(parseInt(index), mydata2, '');
                    index += 1;

                }

                newid = eksistonLL("PASH", lastprindi, lastselsub4, lloji);
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                eksistonLLogNeKetePrind("PASH", lastprindi, newid, $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtKodi, lloji, lastselsub4, $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbGjendja);

                arrLL[0][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtKodi;
                arrLL[1][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtPershkrimiLlogaria;
                arrLL[2][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbGjendja;
                arrLL[3][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbShenja;
                arrLL[4][newid] = lastselsub4 + ":" + lastprindi;
                arrLL[5][newid] = lastselsub4 + ":PASH";
                arrLL[6][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbLloji;
                ruajllogari(); $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4);
                var index3 = parseInt(lastselsub4) + 1;
                for (r = 1; r < kodet.length; r++) {

                    newid = eksistonLL("PASH", lastprindi, index3, lloji);
                    eksistonLLogNeKetePrind("PASH", lastprindi, newid, kodet[r], lloji, index3, "Gjithmone");
                    index3++;
                }


            }
            else {
                newid = eksistonLL("PASH", lastprindi, lastselsub4, lloji);
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                eksistonLLogNeKetePrind("PASH", lastprindi, newid, $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtKodi, lloji, lastselsub4, $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbGjendja);

                arrLL[0][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtKodi;
                arrLL[1][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).txtPershkrimiLlogaria;
                arrLL[2][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbGjendja;
                arrLL[3][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbShenja;
                arrLL[4][newid] = lastselsub4 + ":" + lastprindi;
                arrLL[5][newid] = lastselsub4 + ":PASH";
                arrLL[6][newid] = lastselsub4 + ":" + $("table[id$='" + lastselsubgrid4 + "']").getRowData(lastselsub4).cmbLloji;
                ruajllogari(); $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4);

            }
            try {
                $('#' + o).focus();
                jQuery("#" + o).focus();
            }
            catch (Err) {
            }

        }
        //funksioni kur shtypet nje karakter tek textboxi i kodit
        // krijon rreshtin e ri nqs jemi tek reshti i fundit
        //kontrollon nese per kete gride eksiston elementi prind
        function keyPressKodi() {
            var prind = lastselsubgrid4.split('_');
            if (prind.length == 3) {
                if ($("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[1])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "']").getRowData(prind[1]).txtPershkrimi;
            }
            else if (prind.length == 5) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t" + prind[3])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t" + "']").getRowData(prind[3]).txtPershkrimi;
            }
            else if (prind.length == 7) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + prind[5])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t" + "']").getRowData(prind[5]).txtPershkrimi;
            }
            else if (prind.length == 9) {
                if ($("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi.search('txtPershkrimi') != -1)
                    pershkrimi = jQuery('#txtPershkrimi' + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + prind[7])[0].value;
                else pershkrimi = $("table[id$='" + prind[0] + "_" + prind[1] + "_t_" + prind[3] + "_t_" + prind[5] + "_t" + "']").getRowData(prind[7]).txtPershkrimi;
            }
            lastprindi = pershkrimi;
            if (pershkrimi == "") {
                alert('Ju lutem jepni emertimin e zerit prind');
                $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
                indexpasheditim = 0;
            }
            else {
                if ($("table[id$='" + lastselsubgrid4 + "']").getInd(lastselsub4) == $("table[id$='" + lastselsubgrid4 + "']").getDataIDs().length - 1) {
                    var index2 = parseInt(lastselsub4) + 1;
                    be = "<input id='butonFshi" + lastselsubgrid4 + index2 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + lastselsubgrid4 + index2 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + lastselsubgrid4 + "&quot;" + "," + index2 + ")'/>";
                    var mydata2 = { txtFshi: be, cmbLloji: "", txtKodi: "", txtPershkrimiLlogaria: "", cmbGjendja: "", cmbShenja: "" };
                    var su = $("table[id$='" + lastselsubgrid4 + "']").addRowData(parseInt(lastselsub4) + 1, mydata2);
                }
            }
            callWebserviceKodi();
        } var lengthshkrojtur;
        //funksioni qe therret web service per te marre nr e llogarise
        function callWebserviceKodi() {
            var vlera = $('#txtKodi' + lastselsubgrid4 + lastselsub4)[0].value;
            var tekstSelektuar = document.selection.createRange().text;
            var vleraShkrojtur = vlera.substr(0, vlera.length - tekstSelektuar.length) + ((event.keyCode != 0) ? String.fromCharCode(event.keyCode) : "");
            lengthshkrojtur = vleraShkrojtur.length;
            var lloji = $('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].value;
            if (lloji == 2)

                PlatinumWeb.wsfunc.ktheListeLlogarish(vleraShkrojtur, SucceededCallbackKodi, myWS.webServiceFail);
            else if (lloji == 3)
                PlatinumWeb.wsfunc.ktheListeLlogarishstandarte(vleraShkrojtur, SucceededCallbackKodi);
        }
        //funksioni i pergjigjes se web service
        function SucceededCallbackKodi(result) {
            window.parent.SessionTimeout.sendKeepAlive();
            //            var vlera = $('#txtKodi' + lastselsubgrid4 + lastselsub4)[0].value;
            //            var tekstSelektuar = document.selection.createRange().text;
            //            var vleraShkrojtur = vlera.substr(0, vlera.length - tekstSelektuar.length) + String.fromCharCode(event.keyCode);
            $('#txtKodi' + lastselsubgrid4 + lastselsub4)[0].value = result;
            var vleraGjetur = result;
            setSelectionRange($('#txtKodi' + lastselsubgrid4 + lastselsub4)[0], lengthshkrojtur, vleraGjetur.length);
        }
        function setSelectionRange(input, selectionStart, selectionEnd) {
            if (input.setSelectionRange) {
                input.focus();
                input.setSelectionRange(selectionStart, selectionStart);
            }
            else if (input.createTextRange) {
                var range = input.createTextRange();
                range.collapse(true);
                range.moveEnd('character', selectionEnd);
                range.moveStart('character', selectionStart);
                range.select();
            }
        }
        //funksioni qe therret web service per te marre pershkrimin e llogarise
        function pershkrimiLLog() {
            var kodi = "#txtKodi" + lastselsubgrid4 + lastselsub4;
            var name = jQuery(kodi)[0].value;
            if (name != "") {
                var lloji = $('#cmbLloji' + lastselsubgrid4 + lastselsub4)[0].value;
                if (lloji == 2)
                    PlatinumWeb.wsfunc.ktheEmerLlogarie(name, SucceededCallbackEmertimLlogarie);
                else if (lloji == 3)
                    PlatinumWeb.wsfunc.ktheEmerLlogariestandarte(name, SucceededCallbackEmertimLlogarie);
            }
        }
        //funksioni i pergjigjes se web service
        function SucceededCallbackEmertimLlogarie(result) {
            window.parent.SessionTimeout.sendKeepAlive();
            var kodi = "#txtKodi" + lastselsubgrid4 + lastselsub4;
            if (result != '') {
                var idKontrolli = "#txtPershkrimiLlogaria" + lastselsubgrid4 + lastselsub4;
                jQuery(idKontrolli)[0].value = result;
                jQuery(idKontrolli)[0].focus();
            }
            changeKodi();
        }
        //krijon elementin per textboxet kur rreshti nuk eshte ne editim
        function myvalue(elem) {
            return elem[0].firstChild.value;
        }
        //krijon elementin textbox per pershkrimin  llogaria kur rreshti eshte ne editim
        function myElemPershkrimiLlogaria(value) {
            var el = document.createElement("div");

            el.innerHTML = "<input  id ='txtPershkrimiLlogaria" + lastselsubgrid4 + lastselsub4 + "'  type ='text'  value='" + value + "' style='width: 100%'>";
            el.value = value;
            return el;
        }
        //krijon elementin per textboxet kur rreshti nuk eshte ne editim
        function myvalueNormal(elem) {
            return elem[0].firstChild.value;
        }
        //krijon elementin combo per gjendjen kur reshti editohet
        function myelemComboGjendja(value) {
            var el3 = document.createElement("div");

            var temp;
            if (value == "Debi")
                temp = "<select  id='cmbGjendja" + lastselsubgrid4 + lastselsub4 + "'  onchange='changeKodi()'  ><OPTION value=1 >Gjithmone</OPTION><OPTION value=2 selected='selected'>Debi</OPTION><OPTION value=3 >Kredi</OPTION>";
            else if (value == "Kredi")
                temp = "<select  id='cmbGjendja" + lastselsubgrid4 + lastselsub4 + "' onchange='changeKodi()'   ><OPTION value=1 >Gjithmone</OPTION><OPTION value=2 >Debi</OPTION><OPTION value=3 selected='selected'>Kredi</OPTION>";
            else
                temp = "<select  id='cmbGjendja" + lastselsubgrid4 + lastselsub4 + "'  onchange='changeKodi()'  ><OPTION value=1 selected='selected'>Gjithmone</OPTION><OPTION value=2 >Debi</OPTION><OPTION value=3 >Kredi</OPTION>";

            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;

        }
        //krijon elementin value combo per gjendjen kur reshti nuk eshte ne editim
        function myvalueComboGj(elem) {
            return jQuery('#cmbGjendja' + lastselsubgrid4 + lastselsub4)[0][jQuery('#cmbGjendja' + lastselsubgrid4 + lastselsub4)[0].selectedIndex].text;
        }
        //krijon elementin combo per shenjen kur reshti editohet
        function myelemComboShenja(value) {
            var el3 = document.createElement("div");

            var temp;
            if (value == "Negative")
                temp = "<select  id='cmbShenja" + lastselsubgrid4 + lastselsub4 + "'  onchange='changeKodi()' onBlur='lostFocusKoloneFunditS()' ><OPTION value=1 >Pozitive</OPTION><OPTION value=2 selected='selected'>Negative</OPTION>";
            else
                temp = "<select  id='cmbShenja" + lastselsubgrid4 + lastselsub4 + "' onchange='changeKodi()'  onBlur='lostFocusKoloneFunditS()'  ><OPTION value=1 selected='selected'>Pozitive</OPTION><OPTION value=2 >Negative</OPTION>";

            temp += "</select>";
            el3.innerHTML = temp;
            el3.select;
            el3.style.width = "100%";
            return el3;

        }
        //krijon elementin value combo per shenjen kur reshti nuk eshte ne editim
        function myvalueComboSh(elem) {
            return jQuery('#cmbShenja' + lastselsubgrid4 + lastselsub4)[0][jQuery('#cmbShenja' + lastselsubgrid4 + lastselsub4)[0].selectedIndex].text;
        }
        //perdoret per te kaluar tabin ne reshtin tjeter
        function lostFocusKoloneFunditS() {

            $("table[id$='" + lastselsubgrid4 + "']").saveRow(lastselsub4, false, 'clientArray');
            lastselsub4 = parseInt(lastselsub4) + 1;
            indexpasheditim = 0;
            $("table[id$='" + lastselsubgrid4 + "']").editRow(lastselsub4);

        }
        function renditKolonatGrides() {
            jQuery("#rowed5").remapColumns(arrayRenditjeKolonaGrides);
        }
        var el3check = "<input  id ='cbTVSH" + lastsel2 + "'  type ='checkbox'    style='width: 100%'> ";
        //formon array per konfigurimin e sub grides
        function formoArrayKolonaSubGrides() {
            var arr = $("input[id$='hfKolonaSubGride']")[0].value.split('||');
            var tempArray;
            var rendit = new Array();
            for (var i = 0; i < arr.length - 1; i++) {
                tempArray = arr[i].split('|');
                arrayIdKolonaSubGrides[i] = tempArray[0];
                arrayPershkrimiKolonaSubGrides[i] = tempArray[1];
                if (tempArray[2] == 'True') {
                    arrayVisibleKolonaSubGrides[i] = false;
                }
                else {
                    arrayVisibleKolonaSubGrides[i] = true;
                }
                arrayReadOnlyKolonaSubGrides[i] = tempArray[3];
                arrayWidthKolonaSubGrides[i] = tempArray[4];
                rendit[i] = tempArray[5];
            }
            var j = 0;
            while (j < 6) {
                for (k = 0; k < rendit.length; k++) {
                    if (rendit[k] == j) {
                        arrayRenditjeKolonaSubGrides[j] = k;

                    }
                }
                j++;
            }
        }


        function renditKolonatSubGrides() {
            jQuery("#rowed5").remapColumns(arrayRenditjeKolonaSubGrides);
        }
        //perdoret per te mbushur griden me te dhenat e ruajtura tek hidden fieldi per llogarite

        function mbushSubGrideNgaHiddenFieldi(pershk, gridid) {
            var hf5 = document.getElementById("gridaLlogarite");
            if (hf5.value != "") {
                var llogarite = hf5.value.split('*');


                lastselsub4 = 1;

                $("table[id$='" + gridid + "']").delRowData(1);

                for (var i = 0; i < llogarite.length - 1; i++) {

                    be = "<input id='butonFshi" + gridid + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + gridid + "&quot;" + "," + lastselsub4 + ")'/>";
                    if (llogarite[i].split(';')[5].split(':')[1] != 'PASH')
                        continue;
                    else if (llogarite[i].split(';')[4].split(':')[1] == pershk) {
                        lloj = (llogarite[i].split(';')[6].split(':')[1] == undefined) ? "" : llogarite[i].split(';')[6].split(':')[1];
                        kod = (llogarite[i].split(';')[0].split(':')[1] == undefined) ? "" : llogarite[i].split(';')[0].split(':')[1];
                        emertimi = (llogarite[i].split(';')[1].split(':')[1] == undefined) ? "" : llogarite[i].split(';')[1].split(':')[1];
                        gjen = (llogarite[i].split(';')[2].split(':')[1] == undefined) ? "" : llogarite[i].split(';')[2].split(':')[1];
                        shenj = (llogarite[i].split(';')[3].split(':')[1] == undefined) ? "" : llogarite[i].split(';')[3].split(':')[1];
                        var datarow = { txtFshi: be, cmbLloji: lloj, txtKodi: kod, txtPershkrimiLlogaria: emertimi, cmbGjendja: gjen, cmbShenja: shenj };
                        var su;
                        if (kod != "") {
                            su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub4), datarow);

                            ;

                            lastselsub4 = lastselsub4 + 1;
                        }
                    }
                }
                be = "<input id='butonFshi" + gridid + lastselsub4 + "' type='image' value='Fshi' onmouseover='ndryshoImazhin(1," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")' onmouseout='ndryshoImazhin(0," + "&quot;" + gridid + lastselsub4 + "&quot;" + ")'  src='images/square-icon.png' onclick='fshiClickedS(" + "&quot;" + gridid + "&quot;" + "," + lastselsub4 + ")'/>";

                var datarow = { txtFshi: be, cmbLloji: "", txtKodi: "", txtPershkrimiLlogaria: "", cmbGjendja: "", cmbShenja: "" };
                var su;
                su = $("table[id$='" + gridid + "']").addRowData(parseInt(lastselsub4), datarow);

            }
        }

    </script>
    <script type="text/javascript">
        //perdoret per te thirrur web service tek faqja prind per te marre emrin e faqes qe do te shfaqet si dhe per te ruajtur ne cookie faqen ku ndodhemi
        function changeName() {
            myFaqeCelje.shtoHandlerSession();
            window.parent.callWebservice('KonfigurimPASH.aspx');
            ////            callWebservicePerUserLabel(emrimenuse);
            myCookies.createCookie('adresa', 'KonfigurimPASH.aspx', 1);
        }
        var editorValues = new Object();
        var arr = new Array();
        var arrLL = new Array();
        var editorGlobal;
        var indeksi = -1;
        var editorGjendja;
        var editorShenja;
        var editorNrllogaria;
        var editorPershkrimillogaria;
        var indexCounter;
        //perdoret per te validuar nese jane plotesuar te gjitha  fushat e detyrueshme

        function valido(s, e) {
            var activeTabIndex = ASPxPageControl1.GetActiveTab().index;
            var tabPageCount = ASPxPageControl1.GetTabCount();

            for (var i = 0; i < tabPageCount - 1; i++) {
                ASPxPageControl1.SetActiveTab(ASPxPageControl1.GetTab(i));
                isvalid = ASPxClientEdit.ValidateGroup("entries");
                if (isvalid == false) {
                    LoadingPanel.Hide();
                    e.processOnServer = false;
                    ASPxPageControl1.SetActiveTab(ASPxPageControl1.GetTab(i));
                    break;
                }
                else
                    ASPxPageControl1.SetActiveTab(ASPxPageControl1.GetTab(activeTabIndex));
            }
        }
        //perdoet per te marre kodit e karakterit te shtypur nga tastjera
        function _getKeyCode(evt) {
            return (typeof (evt.keyCode) != "undefined" && evt.keyCode != 0) ?
												            evt.keyCode : evt.charCode;
        }
        // inicializon array me vlera boshe dhe merr te dhenat e ruajtura ne hidden fieldet qe jane marre nga databaza per te plotesuar gridat me te dhenat ne modifikim

        function Init() {
            changeName();
            for (i = 0; i < 4; i++) {
                arr[i] = new Array();
            }
            hf3 = document.getElementById("hfShtimModifikim");
            if (hf3.value == "modifikim")
                kodi_ASPxTextBox.SetEnabled(false);
            hf5 = document.getElementById("gridaZerat");

            if (hf5.value != "") {
                var arrakt = hf5.value.split(';');
                for (i = 0; i < arrakt[0].split(',').length; i++) {
                    arr[0][i] = arrakt[0].split(',')[i];
                    arr[1][i] = arrakt[1].split(',')[i];
                    arr[2][i] = arrakt[2].split(',')[i];
                    arr[3][i] = arrakt[3].split(',')[i];
                }
                indexglobalpash = arrakt[0].split(',').length - 2;
            }

            for (i = 0; i < 7; i++) {
                arrLL[i] = new Array();
            }
            hfgridaLLogarite = document.getElementById("gridaLlogarite");
            var rresht = hfgridaLLogarite.value.split('*');
            for (l = 0; l < rresht.length - 1; l++) {

                arrLL[0][l] = rresht[l].split(';')[0];
                arrLL[1][l] = rresht[l].split(';')[1];
                arrLL[2][l] = rresht[l].split(';')[2];
                arrLL[3][l] = rresht[l].split(';')[3];
                arrLL[4][l] = rresht[l].split(';')[4];
                arrLL[5][l] = rresht[l].split(';')[5];
                arrLL[6][l] = rresht[l].split(';')[6];

            }
            indexgloballlogari = rresht.length - 2;
            hf1 = document.getElementById("hfBuxheti1");
            hf2 = document.getElementById("hfBuxheti2");
            arrBuxhet = hf1.value.split(',');
            arrBuxhet2 = hf2.value.split(',');
            counter = arrBuxhet.length;
            counter2 = arrBuxhet2.length;

            jQuery(document).ready(function () {
                formoArrayKolonaGrides();
                formoArrayKolonaSubGrides();
                inicializoGride();
                mbushGrideNgaHiddenFieldi();

            });
            indexCounter = l;
        }
        //pastron array dhe fushat e faqes
        function pastro() {
            for (i = 0; i < 4; i++) {
                arr[i] = new Array();

            }
            kodi_ASPxTextBox.SetEnabled(true);
            for (i = 0; i < 7; i++) {
                arrLL[i] = new Array();
            }
            indexglobalpash = -1;

            indexgloballlogari = -1;
            arrBuxhet = new Array();
            arrBuxhet2 = new Array();
            kodi_ASPxTextBox.SetText();
            emertimi_ASPxTextBox.SetText();

            zeri_buxheti_ASPxLabel.SetText();
            hf1 = document.getElementById("hfBuxheti1");
            hf2 = document.getElementById("hfBuxheti2");
            hf3 = document.getElementById("hfShtimModifikim");
            // hf4 = document.getElementById("HiddenField1");
            hf5 = document.getElementById("HiddenFieldZerat");
            hf8 = document.getElementById("gridaZerat");
            ASPxPageControl1.SetActiveTabIndex(0);
            hf1.value = "";
            hf2.value = "";
            hf3.value = "shtim";
            // hf4.value = "";
            hf5.value = "";

            hf8.value = "";

            $("table[id$='rowed5']").GridUnload("#" + $("table[id$='rowed5']")[0].id);
            inicializoGride();
            mbushGrideNgaHiddenFieldi();




            indexCounter = 0;
            counter = 0;
            counter2 = 0;

            grid_buxhetet.PerformCallback('');

        }


        //FUNKSIONET PER BUXHETET

        var arrBuxhet = new Array();
        var counter = 0;
        var arrBuxhet2 = new Array();
        var counter2 = 0;
        //kontrollon kur kalohet tek tabi i buxheteve nese eshte zgjedhur nje ze per kete buxhet apo jo 

        function GetRowValuesZeratBuxheti(values) {

            pershkrimi = "";

            if (indexpasheditim == 0) {
                ASPxPageControl1.SetActiveTab(ASPxPageControl1.GetTabByName('Zerat'));
                alert("Zgjidhni njerin nga zerat");
            }
            else {
                pershkrimi = jQuery('#txtPershkrimi' + indexpasheditim)[0].value;
                zeri_buxheti_ASPxLabel.SetText(pershkrimi);
                grid_buxhetet.PerformCallback(pershkrimi);
            }


        }
        // perdoret per te ruajtur vleren e shtuar ne kolonen e buxhetit te pare dhe shton vleren tek totali

        function ShtoBuxhet1(editor, edgjendja, eddiff, key) {
            if (isNaN(editor.GetText())) {
                editor.SetFocus();
                alert('Vlerat e buxhetit duhet te jene numerike');
            }
            else if (editor.GetText() == '') {
                editor.SetFocus();
                alert('Jepni vleren e buxhetit');
            }
            else {
                var shuma = 0;
                var hidField = document.getElementById("hfBuxheti1"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
                arrBuxhet[counter] = key.toString() + ":" + editor.GetText() + ":" + zeri_buxheti_ASPxLabel.GetText();
                counter += 1;
                hidField.value = arrBuxhet;
                eddiff.SetText(editor.GetText() - edgjendja.GetText());
                shuma = parseFloat(textboxBuxh11.GetText()) + parseFloat(textboxBuxh112.GetText()) + parseFloat(textboxBuxh12.GetText()) + parseFloat(textboxBuxh13.GetText()) + parseFloat(textboxBuxh14.GetText()) + parseFloat(textboxBuxh15.GetText()) + parseFloat(textboxBuxh16.GetText()) + parseFloat(textboxBuxh17.GetText()) + parseFloat(textboxBuxh18.GetText()) + parseFloat(textboxBuxh19.GetText()) + parseFloat(textboxBuxh110.GetText()) + parseFloat(textboxBuxh111.GetText());
                textboxBuxh10.SetText(shuma);
                labelDiff10.SetText(textboxBuxh10.GetText() - labelGjendja0.GetText());

            }
        }
        // perdoret per te ruajtur vleren e shtuar ne kolonen e buxhetit te dyte dhe shton vleren tek totali

        function ShtoBuxhet2(editor, edgjendja, eddiff, key) {
            if (isNaN(editor.GetText())) {
                editor.SetFocus();
                alert('Vlerat e buxhetit duhet te jene numerike');
            }
            else if (editor.GetText() == '') {
                editor.SetFocus();
                alert('Jepni vleren e buxhetit');
            }
            else {
                var shuma = 0;
                var hidField2 = document.getElementById("hfBuxheti2"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
                arrBuxhet2[counter2] = key.toString() + ":" + editor.GetText() + ":" + zeri_buxheti_ASPxLabel.GetText();
                counter2 += 1;
                hidField2.value = arrBuxhet2;
                eddiff.SetText(editor.GetText() - edgjendja.GetText());
                shuma = parseFloat(textboxBuxh21.GetText()) + parseFloat(textboxBuxh212.GetText()) + parseFloat(textboxBuxh22.GetText()) + parseFloat(textboxBuxh23.GetText()) + parseFloat(textboxBuxh24.GetText()) + parseFloat(textboxBuxh25.GetText()) + parseFloat(textboxBuxh26.GetText()) + parseFloat(textboxBuxh27.GetText()) + parseFloat(textboxBuxh28.GetText()) + parseFloat(textboxBuxh29.GetText()) + parseFloat(textboxBuxh210.GetText()) + parseFloat(textboxBuxh211.GetText());
                textboxBuxh20.SetText(shuma);
                labelDiff20.SetText(textboxBuxh20.GetText() - labelGjendja0.GetText());
            }
        }
        //perdoret per te ruajtur vleren e totalit tek buxheti i pare dhe llogarit vleren e gjithe kolonave te tjera total/12

        function ShtoTotal1(editor, edgjendja, eddiff) {

            if (isNaN(editor.GetText())) {
                editor.SetFocus();
                alert('Vlera e totalit te buxhetit duhet te jene numerike');
            }
            else if (editor.GetText() == '') {
                editor.SetFocus();
                alert('Jepni vleren e totalit te buxhetit');
            }
            else {
                var i;
                var hidField = document.getElementById("hfBuxheti1"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
                var s = parseFloat(editor.GetText()) / 12;
                hidField.value = '';

                for (i = 1; i < 13; i++) {
                    arrBuxhet[counter] = i + ":" + s + ":" + zeri_buxheti_ASPxLabel.GetText();
                    counter += 1;
                }
                hidField.value = arrBuxhet;
                eddiff.SetText(editor.GetText() - edgjendja.GetText());
                textboxBuxh11.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh12.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh13.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh14.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh15.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh16.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh17.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh18.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh19.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh110.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh111.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh112.SetText(parseFloat(editor.GetText()) / 12);
                labelDiff11.SetText(textboxBuxh11.GetText() - labelGjendja1.GetText());
                labelDiff12.SetText(textboxBuxh12.GetText() - labelGjendja2.GetText());
                labelDiff13.SetText(textboxBuxh13.GetText() - labelGjendja3.GetText());
                labelDiff14.SetText(textboxBuxh14.GetText() - labelGjendja4.GetText());
                labelDiff15.SetText(textboxBuxh15.GetText() - labelGjendja5.GetText());
                labelDiff16.SetText(textboxBuxh16.GetText() - labelGjendja6.GetText());
                labelDiff17.SetText(textboxBuxh17.GetText() - labelGjendja7.GetText());
                labelDiff18.SetText(textboxBuxh18.GetText() - labelGjendja8.GetText());
                labelDiff19.SetText(textboxBuxh19.GetText() - labelGjendja9.GetText());
                labelDiff110.SetText(textboxBuxh110.GetText() - labelGjendja10.GetText());
                labelDiff111.SetText(textboxBuxh111.GetText() - labelGjendja11.GetText());
                labelDiff112.SetText(textboxBuxh112.GetText() - labelGjendja12.GetText());
            }
        }
        //perdoret per te ruajtur vleren e totalit tek buxheti i dyte dhe llogarit vleren e gjithe kolonave te tjera total/12

        function ShtoTotal2(editor, edgjendja, eddiff) {
            if (isNaN(editor.GetText())) {
                editor.SetFocus();
                alert('Vlera e totalit te buxhetit duhet te jene numerike');
            }
            else if (editor.GetText() == '') {
                editor.SetFocus();
                alert('Jepni vleren e totalit te buxhetit');
            }
            else {
                var i;
                var hidField2 = document.getElementById("hfBuxheti2"); //shton vleren tek hidden field e cila perdoret me vone nga kodi
                var s = parseFloat(editor.GetText()) / 12;
                hidField2.value = '';

                for (i = 1; i < 13; i++) {
                    arrBuxhet2[counter2] = i + ":" + s + ":" + zeri_buxheti_ASPxLabel.GetText();
                    counter2 += 1;
                }
                hidField2.value = arrBuxhet2;
                eddiff.SetText(editor.GetText() - edgjendja.GetText());
                textboxBuxh21.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh22.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh23.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh24.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh25.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh26.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh27.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh28.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh29.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh210.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh211.SetText(parseFloat(editor.GetText()) / 12);
                textboxBuxh212.SetText(parseFloat(editor.GetText()) / 12);
                labelDiff21.SetText(textboxBuxh21.GetText() - labelGjendja1.GetText());
                labelDiff22.SetText(textboxBuxh22.GetText() - labelGjendja2.GetText());
                labelDiff23.SetText(textboxBuxh23.GetText() - labelGjendja3.GetText());
                labelDiff24.SetText(textboxBuxh24.GetText() - labelGjendja4.GetText());
                labelDiff25.SetText(textboxBuxh25.GetText() - labelGjendja5.GetText());
                labelDiff26.SetText(textboxBuxh26.GetText() - labelGjendja6.GetText());
                labelDiff27.SetText(textboxBuxh27.GetText() - labelGjendja7.GetText());
                labelDiff28.SetText(textboxBuxh28.GetText() - labelGjendja8.GetText());
                labelDiff29.SetText(textboxBuxh29.GetText() - labelGjendja9.GetText());
                labelDiff210.SetText(textboxBuxh210.GetText() - labelGjendja10.GetText());
                labelDiff211.SetText(textboxBuxh211.GetText() - labelGjendja11.GetText());
                labelDiff212.SetText(textboxBuxh212.GetText() - labelGjendja12.GetText());
            }
        }
        
    </script>
    <form id="form1" runat="server" style="width: 100%">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server"
            ClientInstanceName="LoadingPanel" Font-Size="9pt"
            CssFilePath="~/App_Themes/Aqua/{0}/styles.css" 
            CssPostfix="Aqua" Modal="True" ImagePosition="Top">
            <Image Url="~/App_Themes/Aqua/Web/Loading.gif" >
            </Image>
            <LoadingDivStyle Opacity="30"></LoadingDivStyle>
        </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        <Services>
            <asp:ServiceReference Path="wsfunc.asmx" />
        </Services>
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />
    </dx:ASPxGlobalEvents>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ClientInstanceName="ASPxPageControl1"
                    ActiveTabIndex="1" Width="100%" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                    CssPostfix="Aqua" ImageFolder="~/App_Themes/Aqua/{0}/" SettingsLoadingPanel-Text="" 
                    TabSpacing="3px">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Text="Kartela">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="kodi_ASPxLabel" runat="server" Text="Kodi:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="kodi_ASPxTextBox" ClientInstanceName="kodi_ASPxTextBox" runat="server"
                                                    Width="170px" ValidationSettings-CausesValidation="True" ValidationSettings-SetFocusOnError="True"
                                                    ValidationSettings-ValidationGroup="entries">
                                                    <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dxe:ASPxLabel ID="emertimi_ASPxLabel" runat="server" Text="Emertimi:">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td>
                                                <dxe:ASPxTextBox ID="emertimi_ASPxTextBox" ClientInstanceName="emertimi_ASPxTextBox"
                                                    runat="server" Width="170px" ValidationSettings-CausesValidation="True" ValidationSettings-SetFocusOnError="True"
                                                    ValidationSettings-ValidationGroup="entries">
                                                    <ValidationSettings CausesValidation="True" SetFocusOnError="True" ValidationGroup="entries">
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dxe:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Text="Zerat" Name="Zerat">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="rowed5" runat="server" style="width: 100%">
                                    </table>
                                    <%-- <dxwgv:ASPxGridView ID="grid_zerat" runat="server" ClientInstanceName="grid_zerat"
                                        CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass"
                                        OnAfterPerformCallback="grid_zerat_AfterPerformCallback" OnHtmlRowCreated="grid_zerat_HtmlRowCreated"
                                        OnCustomJSProperties="grid_zerat_CustomJSProperties" OnCustomCallback="grid_zerat_CustomCallback"
                                        OnHtmlFooterCellPrepared="grid_zerat_HtmlFooterCellPrepared" OnDataBound="grid_zerat_DataBound"
                                        Width="60%">
                                        <ClientSideEvents RowDblClick="function(s, e) { }" />
                                        <Images ImageFolder="~/App_Themes/BlackGlass/{0}/">
                                            <FilterRowButton Height="13px" Width="13px" />
                                        </Images>
                                        <Styles CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass">
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dxwgv:ASPxGridView>--%>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 70%" class="style1">
                                                <dxe:ASPxLabel ID="pergjigja" runat="server" Text="">
                                                </dxe:ASPxLabel>
                                            </td>
                                            <td align="right" class="style2">
                                                <dxe:ASPxButton ID="ASPxButton1" runat="server" Text="Ruaj" OnClick="ruaj_Click"
                                                    CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" Width="100%"
                                                    ValidationGroup="entries">
                                                    <ClientSideEvents Click="function(s, e) {
	LoadingPanel.Show();
    valido(s,e);
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right" class="style2">
                                                <dxe:ASPxButton ID="ASPxButton2" runat="server" Text="Pastro" AutoPostBack="false"
                                                    CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
	pastro();
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right" class="style2">
                                                <dxe:ASPxButton ID="ASPxButton3" runat="server" Text="Anullo" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                                    PostBackUrl="~/KonfigPASH.aspx" CssPostfix="Aqua" Width="100%">
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <%-- <dxtc:TabPage Text="Llogarite">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl6" runat="server">
                                    <dxe:ASPxLabel ID="ASPxLabel1" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                        Text="Zgjidhni llogarite per zerin: ">
                                    </dxe:ASPxLabel>
                                    <dxe:ASPxLabel ID="zeri_ASPxLabel" runat="server" Font-Names="Calibri" ClientInstanceName="zeri_ASPxLabel"
                                        Font-Size="Medium">
                                    </dxe:ASPxLabel>
                                    <div style="width: 100%; height: 5px">
                                    </div>
                                    <dxwgv:ASPxGridView ID="grid_llogarite" runat="server" ClientInstanceName="grid_llogarite"
                                        CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass"
                                        OnHtmlRowCreated="grid_llogarite_HtmlRowCreated" OnCustomCallback="grid_llogarite_CustomCallback"
                                        OnCustomJSProperties="grid_llogarite_CustomJSProperties" OnAfterPerformCallback="grid_llogarite_AfterPerformCallback"
                                        Width="60%">
                                        <Images ImageFolder="~/App_Themes/BlackGlass/{0}/">
                                            <FilterRowButton Height="13px" Width="13px" />
                                        </Images>
                                        <Styles CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass">
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dxwgv:ASPxGridView>
                                 
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>--%>
                        <dxtc:TabPage Text="Buxhetet">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl7" runat="server">
                                    <dxe:ASPxLabel ID="ASPxLabel2" runat="server" Font-Names="Calibri" Font-Size="Medium"
                                        Text="Plotesoni buxhetin per zerin: ">
                                    </dxe:ASPxLabel>
                                    <dxe:ASPxLabel ID="zeri_buxheti_ASPxLabel" runat="server" Font-Names="Calibri" ClientInstanceName="zeri_buxheti_ASPxLabel"
                                        Font-Size="Medium">
                                    </dxe:ASPxLabel>
                                    <div style="width: 100%; height: 5px">
                                    </div>
                                    <dxwgv:ASPxGridView ID="grid_buxhetet" runat="server" ClientInstanceName="grid_buxhetet"
                                        CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass"
                                        OnHtmlRowCreated="grid_buxhetet_HtmlRowCreated" OnCustomCallback="grid_buxhetet_CustomCallback">
                                    <Images SpriteCssFilePath="~/App_Themes/BlackGlass/{0}/sprite.css">
                                    <LoadingPanelOnStatusBar Url="~/App_Themes/BlackGlass/GridView/gvLoadingOnStatusBar.gif">
                                    </LoadingPanelOnStatusBar>
                                    <LoadingPanel Url="~/App_Themes/BlackGlass/GridView/Loading.gif">
                                    </LoadingPanel>
                                </Images>
                                <ImagesFilterControl>
                                    <LoadingPanel Url="~/App_Themes/BlackGlass/Editors/Loading.gif">
                                    </LoadingPanel>
                                </ImagesFilterControl>
                                        <Styles CssFilePath="~/App_Themes/BlackGlass/{0}/styles.css" CssPostfix="BlackGlass">
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dxwgv:ASPxGridView>
                                    <table width="100%">
                                        <tr>
                                            <td style="width: 70%">
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dxe:ASPxButton ID="ASPxButton4" runat="server" Text="Ruaj" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                                    CssPostfix="Aqua" Width="100%" OnClick="ruaj_Click" ValidationGroup="entries">
                                                    <ClientSideEvents Click="function(s, e) {
	LoadingPanel.Show();
    valido(s,e);
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dxe:ASPxButton ID="ASPxButton5" runat="server" Text="Pastro" AutoPostBack="false"
                                                    CssFilePath="~/App_Themes/Aqua/{0}/styles.css" CssPostfix="Aqua" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
	pastro();
}" />
                                                </dxe:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dxe:ASPxButton ID="ASPxButton6" runat="server" Text="Anullo" CssFilePath="~/App_Themes/Aqua/{0}/styles.css"
                                                    PostBackUrl="~/KonfigPASH.aspx" CssPostfix="Aqua" Width="100%">
                                                </dxe:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                    <ClientSideEvents ActiveTabChanged="function(s, e) {
                                                if (ASPxPageControl1.GetActiveTab().index == 2)
                                                             GetRowValuesZeratBuxheti();   
                                                                                                               
                                                }" />
                </dxtc:ASPxPageControl>
                <asp:HiddenField ID="HiddenFieldZerat" runat="server" />
                <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                <asp:HiddenField ID="gridaZerat" runat="server" />
                <asp:HiddenField ID="gridaLlogarite" runat="server" />
                <asp:HiddenField ID="hfBuxheti1" runat="server" />
                <asp:HiddenField ID="hfBuxheti2" runat="server" />
                <asp:HiddenField ID="hfKolonaGride" runat="server" />
                <asp:HiddenField ID="hfKolonaSubGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
        <ContentTemplate>
            <dxpc:ASPxPopupControl ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh komponenten prind"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dxpc:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <iframe id="container" name="containerPasqyreFinanciare" frameborder="0" runat="server">
                        </iframe>
                    </dxpc:PopupControlContentControl>
                </ContentCollection>
            </dxpc:ASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
