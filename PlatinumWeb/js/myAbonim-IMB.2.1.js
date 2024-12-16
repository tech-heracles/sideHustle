; if (typeof myAbonim == 'undefined') {
    myAbonim = {
        timeout: 0,
        client: false,
        myNotyTextButtonClass: "myNotyTextButton",
        pyetjeEHapur: false,
        eshteLupe: false,
        kaPyetjeTeHapur: function () { return this.pyetjeEHapur; },
        lexoMesazhNgaHf: function (id) {
            var msg = $(id).val();
            if (msg) {
                var myArrayofMsg = JSON.parse(msg);
                for (var i = 0; i < myArrayofMsg.length; i++)
                    myAbonim.ShtoMesazh(myArrayofMsg[i]);
                $(id).val("");
            }
        },
        shtoMesazhNeseKa: function () {
            myAbonim.lexoMesazhNgaHf("#mesazhFromServer");
            myAbonim.lexoMesazhNgaHf("#menu_msg_Frame_mesazhFromServer");
            return;
        },
        ShtoMesazhSesioni: function (mesazhSessioni) {
            if (window.parent && window.parent.SessionTimeout)
                window.parent.SessionTimeout.sendKeepAlive();
            if (mesazhSessioni === null)
                return;
            switch (mesazhSessioni.Tipi) {
                case 0: myAbonim.ShtoMesazhGabimi(mesazhSessioni.PershkrimMesazhi);
                    break;
                case 1: myAbonim.ShtoMesazhSuksesi(mesazhSessioni.PershkrimMesazhi);
                    break;
                case 2: myAbonim.ShtoMesazhInformues(mesazhSessioni.PershkrimMesazhi);
                    break;
            }
        },

        shtoHandler: function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(myAbonim.EndRequestTimer);
        },
        EndRequestTimer: function (sender, args) {
            if (!myAbonim.pyetjeEHapur)
                Utils.hiqLoadingGif();
            clearTimeout(this.timeout);
            if ((typeof (mesazhList) !== "undefined") && mesazhList.GetText() != "" && mesazhList.GetText().split('?').length == 1) {
                this.timeout = setTimeout(function () {
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
            }
        },
        InicializoTimer: function () {
            clearTimeout(this.timeout);
            if ((typeof (mesazhList) !== "undefined") && mesazhList.GetText() != "" && mesazhList.GetText().split('?').length == 1) {
                this.timeout = setTimeout(function () {
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
            }
        },
        ShtoMesazhSuksesi: function (mesazh) {
            //    mesazhPage.SetText(mesazh);
            var currentTime = new Date();
            var msgToInsert = appendStringOfTime(currentTime, mesazh);
            if (!this.ShtoMesazh({ type: "success", text: mesazh })) {
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_sukses.ico");
                mesazhList.SetSelectedIndex(0);


                //    $("#mesazhPage").css({ color: 'green' });
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    //        mesazhPage.SetText('');
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
                Utils.hiqLoadingGif();;
            }
        },
        ShtoMesazh: function (msgJson) {
            var myAbonimContext = this;
            var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha : 0, UseCancelButton: true }; //duhet hequr kur te behet me konstruktor
            msgJson = $.extend({}, defaults, msgJson);
            if (typeof LoadingPanel !== "undefined")
                Utils.hiqLoadingGif();
            if (msgJson.type == "confirm") {
                this.blloko();
            }
            this.shtoNeSessionStorage(msgJson);

            if (msgJson.type == "prompt") {
                msgJson.template = '<div class="noty_message"><div class="input-group"><span id="notyTextMessage" class="noty_text input-group-addon"></span><input class="' + myAbonim.myNotyTextButtonClass + ' form-control" aria-describedby="notyTextMessage" type="text"/></div></div>';
                msgJson.type = "confirm";
                msgJson.fromPrompt = true;
            }
            if (msgJson.type == "confirm") {
                this.pyetjeEHapur = true;
                msgJson.buttons = [
                    {
                        addClass: 'btn btn-primary', text: (msgJson.idGjuha == 1 ? "Ok" : 'Po'), onClick: function ($noty) {
                            myAbonimContext.pyetjeEHapur = false;
                            // this = button element
                            // $noty = $noty element
                            if (msgJson.okClick)
                                msgJson.okClick(myAbonim.myNotyTextButtonClass);
                            else
                                myAbonim.Po(msgJson.serverSide);
                            myAbonimContext.zhblloko();
                            $noty.close();
                            //noty({ text: 'You clicked "Ok" button', type: 'success' });
                        }
                    }];
                if (msgJson.UseCancelButton) {
                    msgJson.buttons.push(
                        {
                            addClass: 'btn btn-danger', text: (msgJson.idGjuha == 1 ? "Cancel" : 'Jo'), onClick: function ($noty) {
                                myAbonimContext.pyetjeEHapur = false;
                                if (msgJson.cancelClick) {
                                    msgJson.cancelClick();
                                    myAbonimContext.zhblloko();
                                    $noty.close();
                                    return;
                                }
                                if (msgJson.fromPrompt) {
                                    myAbonimContext.zhblloko();
                                    $noty.close();
                                    return;
                                }
                                myAbonim.Jo(msgJson.serverSide);
                                myAbonimContext.zhblloko();
                                $noty.close();
                            }
                        });
                }
            }
            var myNoty = this._noty(msgJson);
            if (myNoty && typeof mesazhList !== "undefined" && mesazhList.GetVisible())
                mesazhList.SetVisible(false);
            return myNoty;
        },
        ShtoMesazhGabimi: function (mesazh) {
            var currentTime = new Date();
            var msgToInsert = appendStringOfTime(currentTime, mesazh);
            
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_error3.ico");
                mesazhList.SetSelectedIndex(0);
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
                this.shtoNeSessionStorage(msgToInsert);
                Utils.hiqLoadingGif();;
           
        },
        ShtoMesazhInformues: function (mesazh) {
            var currentTime = new Date();
            var msgToInsert = appendStringOfTime(currentTime, mesazh);
            if (!this.ShtoMesazh({ type: "information", text: mesazh })) {
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_info.ico");
                mesazhList.SetSelectedIndex(0);
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
                this.shtoNeSessionStorage(msgToInsert);
                Utils.hiqLoadingGif();;
            }
        },
        ShtoPyetje: function (mesazh, setTimer) {
            if (this.kaPyetjeTeHapur()) return;
            var currentTime = new Date();
            if (!this.ShtoMesazh({ type: "confirm", text: mesazh })) {
                var msgToInsert = appendStringOfTime(currentTime, mesazh);
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_pyetje.ico");
                mesazhList.SetSelectedIndex(0);
                this.shtoNeSessionStorage(msgToInsert);
                btnPo.SetVisible(true);
                btnJo.SetVisible(true);
                if (setTimer) {
                    // timer.SetEnabled(true);
                    clearTimeout(timeout);
                    timeout = setTimeout(function () {
                        //            mesazhPage.SetText('');
                        mesazhList.SetSelectedIndex(-1);
                        btnPo.SetVisible(false);
                        btnJo.SetVisible(false);
                    }, 10000);
                    hlClose.SetVisible(true);
                }
                btnPo.Focus();
            }
        },
        ShtoMesazhNgaGrida: function (grida) {
            var mesazhi = Utils.MerrMesazhNgaGrida(grida);
            if (mesazhi.Kodi !== 1000) {
                if (mesazhi.Status) {
                    myAbonim.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
                } else {
                    myAbonim.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
                }
            }
        },
        _noty: function (msg) {
            if (window.parent && window.parent.noty) {
                return window.parent.noty(msg);
            }
            if (typeof noty != "undefined") {
                return noty(msg);
            }
            return false;
        },
        vendosClient: function () {
            this.client = true;
        },
        vendosServer: function () {
            this.client = false;
        },
        Po: function (serverSide) {
            //var currentTime = new Date();
            //var msgToInsert = appendStringOfTime(currentTime, "U zgjodh 'PO' per pyetjen me siper");
            //myAbonim.shtoNeSessionStorage(msgToInsert);
            if (serverSide) {
                btnPo.DoClick();
                Utils.shfaqLoadingGif();;
                return;
                //mesazhList.SetSelectedIndex(-1);
                //btnPo.SetVisible(false);
                //btnJo.SetVisible(false);
                //hlClose.SetVisible(false);
                //if (!this.client)
                //Utils.shfaqLoadingGif();;
                //return;
            }
            myAbonim.PoClick();
        },
        Jo: function (serverSide) {
            var currentTime = new Date();
            var msgToInsert = appendStringOfTime(currentTime, "U zgjodh 'JO' per pyetjen me siper");
            myAbonim.shtoNeSessionStorage(msgToInsert);
            if (serverSide) {
                btnJo.DoClick();
                return;
            }
            //mesazhList.SetSelectedIndex(-1);
            //btnPo.SetVisible(false);
            //btnJo.SetVisible(false);
            //hlClose.SetVisible(false);
            //e.processOnServer = false;
            myAbonim.JoClick();
        },
        JoClick: function (s, e) {
            JoClick(s, e);
        },
        PoClick: function (s, e) {
            PoClick(s, e);
        },
        ndertoAbonim: function (options) {
            var idNdermarrje = hfState.Get("idNdermarrje");
            var idPerdoruesi = hfState.Get("idPerdoruesi");
            var linkAbonim = "";
            $.ajax({
                async: false,
                url: Utils.getServerApiUrl("Autorizime", "KtheLicence"),
                data: JSON.stringify({ idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi }),
                success: function (licenca) {
                    linkAbonim = "https://calculate.alpha.al/?paymentMethod=1&packageId=" + licenca.llojlicenca + "&numberOfUsers=" + licenca.nrperdorues + "&numberOfCompanies=" + licenca.nrndermarrje + "&niptCheck=false&promo=&onlineAssistantHours=0&powerBI=false&numberOfSalesUsers=0&fiscalization=false#package-container";
                }
            });
            var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha : 0 }; //duhet hequr kur te behet me konstruktor
            options = $.extend({}, defaults, options);
            var opsionLlogaritRiAbonim = hfState.Get("MenuLlogaritAbonim");
            var opsionMbyllje = hfState.Get("MenuItemMbyll");
            var opsionRuajtje = hfState.Get("labelRuajNdryshimet");
            var titulliAbonimModal = hfState.Get("labelAbonimModal");
            var popUpOptions = { prependSelector: "body", dialogClass: "dialog-abonim", contentClass: "tabele-abonim", titulli: titulliAbonimModal, text: { mbyll: opsionMbyllje, ruaj: opsionRuajtje, llogarit: opsionLlogaritRiAbonim }, linkAbonim: linkAbonim };
            var myPopup = Utils.ndertoPopupAbonimi(popUpOptions);
            myPopup.modal("show");
            $("." + popUpOptions.contentClass).html("");
            var arrHeader = options.idGjuha == 0 ? ['Licenca', 'Afati i mbarimit', 'Lloji i licences', 'Numri i perdoruesve', 'Numri i ndermarrjeve'] : ['Licence', 'Expiration date', 'Type of licence', 'Number of users', 'Number of enterprises'];
            var arrBody = this.merrNgaSessionStorage(options.idGjuha);
            //table-striped
            //<div class="alert alert-success" role="alert">...</div>
            //<div class="alert alert-info" role="alert">...</div>
            //<div class="alert alert-warning" role="alert">...</div>
            //<div class="alert alert-danger" role="alert">...</div>
            $("." + popUpOptions.contentClass).append($("<table class='table table-bordered'><thead><tr><th>" + arrHeader.join("</th><th>") + "</th></tr></thead>"
                //hiq komentin nese do footer
                //+ "<tfoot><tr><th>" + arrHeader.join("</th><th>") + "</th></tr></tfoot>" 
                + "<tbody></tbody></table>"));
            //$.each(arrBody, function () {
            //    $("." + popUpOptions.contentClass + " > table > tbody").append("<tr><td>" +  + "</td><td>" +  + "</td><td>" +  + "</td><td>" +  + "</td><td>" +  + "</td></tr>");
            //});
            $.ajax({
                async: false,
                url: Utils.getServerApiUrl("Autorizime", "KtheLicence"),
                data: JSON.stringify({ idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi }),
                success: function (licenca) {
                    $("." + popUpOptions.contentClass + " > table > tbody").append("<tr class='alert-" + "'><td>" + licenca.kodlicenca + "</td><td>" + licenca.datelicenca + "</td><td>" + licenca.llojlicenca + "</td><td>" + licenca.nrperdorues + "</td><td>" + licenca.nrndermarrje + "</td></tr>");
                }
            });
            //mesazhList.InsertItem(licenca.kodlicenca, licenca.datelicenca, licenca.llojlicenca, licenca.nrperdorues, licenca.nrndermarrje);

            var myDtOptions = {
                scrollY: '50vh',
                scrollCollapse: true,
                paging: false,
                "ordering": false
            };
            if (options.idGjuha == 0) {
                myDtOptions.language = {
                    "decimal": "",
                    "emptyTable": "No data available in table",
                    "info": "Duke treguar _START_ nga _END_ te _TOTAL_ mesazheve gjithesej",
                    "infoEmpty": "Showing 0 to 0 of 0 entries",
                    "infoFiltered": "(filtered from _MAX_ total entries)",
                    "infoPostFix": "",
                    "thousands": ",",
                    "lengthMenu": "Show _MENU_ entries",
                    "loadingRecords": "Loading...",
                    "processing": "Processing...",
                    "zeroRecords": "No matching records found",
                    "paginate": {
                        "first": "Fillim",
                        "last": "Fund",
                        "next": "Pas",
                        "previous": "Para"
                    },
                    "aria": {
                        "sortAscending": ": activate to sort column ascending",
                        "sortDescending": ": activate to sort column descending"
                    }
                }
            }
            $("." + popUpOptions.contentClass + " table").DataTable(myDtOptions);
        },
        ndertoNjoftime: function (options) {

            var arrBody = options.mesazhet;
            var idPerdoruesi = options.idPerdoruesi;
            $.each(arrBody, function (index, item) {
                setTimeout(function afishomesazhe() {
                    afishoNjoftime(item, idPerdoruesi);
                }, index * 1000);
            });
        },
        shtoNeSessionStorage: function (msgToInsert) {
            var msgKey = "msg1";
            var storedMessages = { type: msgToInsert.type, text: msgToInsert.text, time: new Date() };
            if (window.parent && window.parent.lblFaqja) //e perkoheshme duhet hequr kur te behet me konstruktor new myAbonim(idGjuha, ambienti)
                storedMessages.ambienti = window.parent.lblFaqja.GetText();
            var msgToInsertString = JSON.stringify(storedMessages);
            if (console)
                console.log(msgToInsertString);
            if (!sessionStorage)
                return;
            var msgList = sessionStorage.getItem(msgKey);
            if (!msgList)
                msgList = "[]";
            msgList = JSON.parse(msgList);
            msgList.unshift(storedMessages);
            sessionStorage.setItem(msgKey, JSON.stringify(msgList));
        },
        merrNgaSessionStorage: function (idgjuha) {
            var nukKaMesazhe = { type: "info", time: new Date(), ambienti: (idgjuha == 0 ? "Mesazhet" : "Messages") };
            if (!sessionStorage) {
                nukKaMesazhe.text = "Nuk ka session storage"
                return [nukKaMesazhe];
            }
            var msgList = sessionStorage.getItem("msg1");
            if (!msgList) {
                nukKaMesazhe.text = idgjuha == 0 ? "Nuk ka ne kete sesion" : "No messages yet";
                return [nukKaMesazhe];
            }
            msgList = JSON.parse(msgList);
            return msgList;
        },
        zhblloko: function () {
            if (!this.kaPyetjeTeHapur()) {
                if (!this.eshteLupe)
                    Utils.zhbllokoFaqe();
                else
                    Utils.zhblloko();
            }
        },
        blloko: function () {
            if (!this.eshteLupe)
                Utils.bllokoFaqe();
            else
                Utils.blloko();
        },
        closeAll: function () {
            window.parent.$.noty.closeAll();
        }
    };
    $(window).on("load", function () {
        myAbonim.shtoMesazhNeseKa();
        if (typeof (isPostBack) == "undefined" && typeof (Sys) != "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(myAbonim.shtoMesazhNeseKa);
        }
    });
};
function getBootStrapClassPart(msgType) {
    switch (msgType) {
        case "error": return "danger";
        case "information": return "info";
        default: return msgType;
    }
}

function getIntOfDate(date) {
    return date.getDate() * Math.pow(10, 6) + date.getHours() * Math.pow(10, 4) + date.getMinutes() * Math.pow(10, 2) + date.getSeconds();
};

function appendStringOfTime(date, mesazh) {
    return stringOfTime(date) + ' - ' + mesazh;
};
function stringOfTime(date) {
    if (typeof date == "string")
        date = new Date(date);
    return date.getHours() + ':' + date.getMinutes() + ':' + date.getSeconds();
}

function afishoNjoftime(itemsnjoftime, idPerdoruesi) {

    var itemBrenda = itemsnjoftime;
    $.notify({
        title: itemBrenda.MESAZH_TITULLI,
        message: itemBrenda.MESAZH_PERMBAJTJA,
        target: '_blank',
    }, {
        type: 'pastel-info',
        allow_dismiss: true,
        delay: 0,
        template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
            '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
            '<button type="button" aria-hidden="true" class="close" data-notify="minmax"> <i class="fa fa-window-maximize" style="font-size:13px;margin-top:5px;margin-right:5px"> </i></button>' +
            '<span data-notify="title">{1}</span>' +
            '<span data-notify="message" >{2}</span>' +
            '</div>',
        onClosed: function () { RuajNjoftime(itemBrenda.NJOFTIMEID, idPerdoruesi) }
    })
}

function RuajNjoftime(njoftimeid, idperdoruesi) {
    $.ajax({
        url: Utils.getServerApiUrl("NodeApi", "ruajmesazheperdoeus"),
        data: JSON.stringify({ Njoftimeid: njoftimeid, IdPerdoruesi: idperdoruesi })
    }).done(function (result) {
        console.log(result);
    });
}

    //mesazhList.InsertItem(licenca.kodlicenca, licenca.datelicenca, licenca.llojlicenca, licenca.nrperdorues, licenca.nrndermarrje);
   
