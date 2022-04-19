//;$(document).ready(function (e) {
//    myFaqeCelje.krijoMenuPerCRM(true);
//});

//function merrTeDhenaPerMenu() {
//}

//function SucceededCallbackMenu(result) {
//    var menute = JSON.parse(result);
//    var html = htmlHomePage = "";
//    var $ul = $('#ulMenu');
//    var $ulHomePage = $('#myHomePage');
//    for (var i = 0; i < menute.length; i++) {
//        if (menute[i].KOMPONEMRI == "CRMDefault.aspx") //Eshte vet Home dhe e shtojme vetem te menuja anash dhe jo te faqja default sepse te ajo jemi 
//        {
//            html += "<li><a href=" + menute[i].KOMPONEMRI + ">" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";
//        }
//        else {
//            if ((menute[i].KOMPONEMRI).indexOf('Raport_PivotGrid.aspx') != -1 || (menute[i].KOMPONEMRI).indexOf("Shto_KlientFurnitor.aspx") != -1 || (menute[i].KOMPONEMRI).indexOf("Raportet.aspx?idmod=24.aspx") != -1)
//            {
//                html += "<li><a href=" + menute[i].KOMPONEMRI + "&vjenNga=CRM>" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";
//                htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=" + menute[i].KOMPONEMRI + "&vjenNga=CRM>" + "<img class='ui-li-thumb' src='images/CRM/" + menute[i].IMAGEURL + ".png'>" + "<h2>" + menute[i].PERSHKRIMKOMPONENTE + "</h2></a></li>";
//            }
//            else {
//                html += "<li><a href=" + menute[i].KOMPONEMRI + ">" + "<span style='vertical-align:middle' class='fa " + menute[i].IMAGEURL + " fa-2x'></span>" + "&nbsp; " + menute[i].PERSHKRIMKOMPONENTE + "</a></li>";
//                htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=" + menute[i].KOMPONEMRI + ">" + "<img class='ui-li-thumb' src='images/CRM/" + menute[i].IMAGEURL + ".png'>" + "<h2>" + menute[i].PERSHKRIMKOMPONENTE + "</h2></a></li>";
//            }
//        }
//    }
//    html += "<li><a href=Login_Ndermarrje.aspx><span style='vertical-align:middle' class='fa fa-building-o fa-2x'></span>&nbsp; Ndermarrjet </a></li>";
//    html += "<li><a target='_blank' href=FaqeKryesore.aspx?vjenNga=CRM><img style='vertical-align:middle' class='ui-li-thumb' width='28px' height='28px' src='images/CRM/AlphaWebMenu.png'>&nbsp; Alpha Web </a></li>";
//    htmlHomePage += "<li class='ui-li-has-thumb'><a class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=Login_Ndermarrje.aspx><img class='ui-li-thumb' src='images/CRM/fa-building-o.png'><h2>Ndermarrjet</h2></a></li>";
//    htmlHomePage += "<li class='ui-li-has-thumb'><a target='_blank' class='ui-btn ui-btn-icon-right ui-icon-carat-r' href=FaqeKryesore.aspx?vjenNga=CRM><img class='ui-li-thumb' src='images/CRM/AlphaWeb.png'><h2>Alpha Web</h2></a></li>";
//    $ul.html(html);
   
//    $ulHomePage.html(htmlHomePage);
//}