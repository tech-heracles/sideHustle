; if (typeof myMesazh == 'undefined') {
    myMesazh = {
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
                    myMesazh.ShtoMesazh(myArrayofMsg[i]);
                $(id).val("");
            }
        },
        shtoMesazhNeseKa: function () {
            myMesazh.lexoMesazhNgaHf("#mesazhFromServer");
            myMesazh.lexoMesazhNgaHf("#menu_msg_Frame_mesazhFromServer");
            return;
        },
        ShtoMesazhSesioni: function (mesazhSessioni) {
            if (window.parent && window.parent.SessionTimeout)
                window.parent.SessionTimeout.sendKeepAlive();
            if (mesazhSessioni === null)
                return;
            switch (mesazhSessioni.Tipi) {
                case 0: myMesazh.ShtoMesazhGabimi(mesazhSessioni.PershkrimMesazhi);
                    break;
                case 1: myMesazh.ShtoMesazhSuksesi(mesazhSessioni.PershkrimMesazhi);
                    break;
                case 2: myMesazh.ShtoMesazhInformues(mesazhSessioni.PershkrimMesazhi);
                    break;
            }
        },
        
        shtoHandler: function () {
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(myMesazh.EndRequestTimer);
        },
        EndRequestTimer: function (sender, args) {
            if(!myMesazh.pyetjeEHapur)
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
            var myMesazhContext = this;
            var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha : 0, UseCancelButton: true }; //duhet hequr kur te behet me konstruktor
            msgJson = $.extend({}, defaults, msgJson);
            if (typeof LoadingPanel !== "undefined")
                Utils.hiqLoadingGif();
            if (msgJson.type == "confirm") {
                this.blloko();
            }
            this.shtoNeSessionStorage(msgJson);

            if (msgJson.type == "prompt") {
                msgJson.template = '<div class="noty_message"><div class="input-group"><span id="notyTextMessage" class="noty_text input-group-addon"></span><input class="' + myMesazh.myNotyTextButtonClass + ' form-control" aria-describedby="notyTextMessage" type="text"/></div></div>';
                msgJson.type = "confirm";
                msgJson.fromPrompt = true;
            }
            if (msgJson.type == "confirm") {
                this.pyetjeEHapur = true;
                msgJson.buttons = [
                        {
                            addClass: 'btn btn-primary', text: (msgJson.idGjuha == 1 ? "Ok" : 'Po'), onClick: function ($noty) {
                                myMesazhContext.pyetjeEHapur = false;
                                // this = button element
                                // $noty = $noty element
                                if (msgJson.okClick)
                                    msgJson.okClick(myMesazh.myNotyTextButtonClass);
                                else
                                    myMesazh.Po(msgJson.serverSide);
                                myMesazhContext.zhblloko();
                                $noty.close();
                                //noty({ text: 'You clicked "Ok" button', type: 'success' });
                            }
                        }];
                if (msgJson.UseCancelButton) {
                    msgJson.buttons.push(
                    {
                        addClass: 'btn btn-danger', text: (msgJson.idGjuha == 1 ? "Cancel" : 'Jo'), onClick: function ($noty) {
                            myMesazhContext.pyetjeEHapur = false;
                            if (msgJson.cancelClick) {
                                msgJson.cancelClick();
                                myMesazhContext.zhblloko();
                                $noty.close();
                                return;
                            }
                            if (msgJson.fromPrompt) {
                                myMesazhContext.zhblloko();
                                $noty.close();
                                return;
                            }
                            myMesazh.Jo(msgJson.serverSide);
                            myMesazhContext.zhblloko();
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
        ShtoMesazhStatusiEinvoice: function (msgJson,eic,selected) {
            var myMesazhContext = this;
            var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha : 0, UseCancelButton: true }; //duhet hequr kur te behet me konstruktor
            msgJson = $.extend({}, defaults, msgJson);
            if (typeof LoadingPanel !== "undefined")
                Utils.hiqLoadingGif();
            if (msgJson.type == "confirm") {
                this.blloko();
            }
            this.shtoNeSessionStorage(msgJson);

            if (msgJson.type == "prompt") {
                msgJson.template = '<div class="noty_message"><div class="input-group"><span id="notyTextMessage" class="noty_text input-group-addon"></span><input class="' + myMesazh.myNotyTextButtonClass + ' form-control" aria-describedby="notyTextMessage" type="text"/></div></div>';
                msgJson.type = "confirm";
                msgJson.fromPrompt = true;
            }
            if (msgJson.type == "confirm") {
                this.pyetjeEHapur = true;
                msgJson.buttons = [
                    {
                        addClass: 'btn btn-primary', text: (msgJson.idGjuha == 1 ? "Ok" : 'Po'), onClick: function ($noty) {
                            myMesazhContext.pyetjeEHapur = false;
                            // this = button element
                            myMesazhContext.zhblloko();
                            $noty.close();
                            changeEinvoiceStatus(eic, selected);

                            $noty.close();

                            //noty({ text: 'You clicked "Ok" button', type: 'success' });
                        }
                    }];
                if (msgJson.UseCancelButton) {
                    msgJson.buttons.push(
                        {
                            addClass: 'btn btn-danger', text: (msgJson.idGjuha == 1 ? "Cancel" : 'Jo'), onClick: function ($noty) {
                                myMesazhContext.pyetjeEHapur = false;
                                if (msgJson.cancelClick) {
                                    msgJson.cancelClick();
                                    myMesazhContext.zhblloko();
                                    $noty.close();
                                    return;
                                }
                                if (msgJson.fromPrompt) {
                                    myMesazhContext.zhblloko();
                                    $noty.close();
                                    return;
                                }
                                myMesazhContext.zhblloko();
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
            if (!this.ShtoMesazh({ type: "error", text: mesazh })) {
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_error3.ico");
                mesazhList.SetSelectedIndex(0);
                clearTimeout(timeout);
                timeout = setTimeout(function () {
                    mesazhList.SetSelectedIndex(-1);
                }, 10000);
                this.shtoNeSessionStorage(msgToInsert);
                Utils.hiqLoadingGif();;
            }
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
        ShtoPyetjeStatusi: function (mesazh,eic,selected) {
            if (this.kaPyetjeTeHapur()) return;
            var currentTime = new Date();
            if (!this.ShtoMesazhStatusiEinvoice({ type: "confirm", text: mesazh }, eic, selected)) {
                var msgToInsert = appendStringOfTime(currentTime, mesazh);
                mesazhList.InsertItem(0, msgToInsert, getIntOfDate(currentTime), "images/info_pyetje.ico");
                mesazhList.SetSelectedIndex(0);
                this.shtoNeSessionStorage(msgToInsert);
                btnPo.SetVisible(true);
                btnJo.SetVisible(true);
                btnPo.Focus();
             
            }
        },
        ShtoMesazhNgaGrida: function(grida){
            var mesazhi = Utils.MerrMesazhNgaGrida(grida);
            if (mesazhi.Kodi !== 1000) {
                if (mesazhi.Status) {
                    myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
                } else {
                    myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
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
            //myMesazh.shtoNeSessionStorage(msgToInsert);
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
            myMesazh.PoClick();
        },
        Jo: function (serverSide) {
            var currentTime = new Date();
            var msgToInsert = appendStringOfTime(currentTime, "U zgjodh 'JO' per pyetjen me siper");
            myMesazh.shtoNeSessionStorage(msgToInsert);
            if (serverSide) {
                btnJo.DoClick();
                return;
            }
            //mesazhList.SetSelectedIndex(-1);
            //btnPo.SetVisible(false);
            //btnJo.SetVisible(false);
            //hlClose.SetVisible(false);
            //e.processOnServer = false;
            myMesazh.JoClick();
        },
        JoStatusi: function(){
            btnJo.DoClick();
        },
        JoClick: function (s, e) {
            JoClick(s, e);
        },
        PoClick: function (s, e) {
            PoClick(s, e);
        },
        ndertoMesazhet: function (options) {
            var defaults = { idGjuha: typeof pageState != 'undefined' && typeof pageState.idGjuha != 'undefined' ? pageState.idGjuha : 0 }; //duhet hequr kur te behet me konstruktor
            options = $.extend({}, defaults, options);
            var opsionMbyllje = hfState.Get("MenuItemMbyll");
            var opsionRuajtje = hfState.Get("labelRuajNdryshimet");
            var titulliMesazhModal = hfState.Get("labelMesazhModal");
            var popUpOptions = { prependSelector: "body", dialogClass: "dialog-mesazhet", contentClass: "tabele-mesazhe", titulli: titulliMesazhModal, text:{mbyll: opsionMbyllje, ruaj: opsionRuajtje}};
            
            var myPopup = Utils.ndertoPopup(popUpOptions);
            myPopup.modal("show");
            $("." + popUpOptions.contentClass).html("");
            var arrHeader = options.idGjuha == 0 ? ['Menu', 'Ora', 'Mesazhi'] : ['Menu', 'Time', 'Message'];
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
            $.each(arrBody, function (index, item) {
                $("." + popUpOptions.contentClass + " > table > tbody").append("<tr class='alert-" + getBootStrapClassPart(item.type) + "'><td>" + (item.ambienti ? item.ambienti : "N/A") + "</td><td>" + stringOfTime(item.time) + "</td><td>" + item.text + "</td></tr>");
            });
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
                    "search": "Kërko:",
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
            if (window.parent && window.parent.lblFaqja) //e perkoheshme duhet hequr kur te behet me konstruktor new myMesazh(idGjuha, ambienti)
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
                nukKaMesazhe.text = idgjuha == 0 ? "Nuk ka mesazhe ne kete sesion" : "No messages yet";
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
        myMesazh.shtoMesazhNeseKa();
        if (typeof (isPostBack) == "undefined" && typeof (Sys) != "undefined") {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(myMesazh.shtoMesazhNeseKa);
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

function afishoNjoftime(itemsnjoftime, idPerdoruesi)
{

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
            onClosed: function() { RuajNjoftime(itemBrenda.NJOFTIMEID , idPerdoruesi) } 
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

