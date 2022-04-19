


//var hfState = new ASPxClientHiddenField();

function saveReport(s, e, saveMode) {
    hfState.Set("saveMode", saveMode);
    s.save();
}
function reportDesigner_filerAction(e, name) { return e.Actions.filter(function (x) { return x.text === name })[0];}

function reportDesigner_CustomizeMenuActions(s, e) {
    var newDesign = hfState.Get("newDesign");
    var saveAction = reportDesigner_filerAction(e, "Save");
    var saveAsAction = reportDesigner_filerAction(e, "Save As");
    var exitAction = reportDesigner_filerAction(e, "Exit");
    var loadCustomLayoutAction = reportDesigner_filerAction(e, "Load Custom Layout");
    if (exitAction && saveAsAction && saveAction && loadCustomLayoutAction) {
        saveAction.clickAction = function (s, e) { saveReport(s, e, "Save") };
        saveAsAction.clickAction = function (s, e) {
            if (!newDesign)
                var myMsg = myMesazh.ShtoMesazh({
                    type: "prompt", text: "Vendos nje emer per raportin:", okClick: function (myNotyTextButton) {
                        hfState.Set("designName", $("." + myNotyTextButton).val());
                        saveReport(s, e, "SaveAs");
                    },
                    idGjuha: hfState.Get("idGjuha")
                });
        };
        loadCustomLayoutAction.clickAction = function (s, e) {
            var myMsg = myMesazh.ShtoMesazh({
                type: "prompt", text: "Jep emrin e plote te disajnit qe deshiron te perdoresh", okClick: function (myNotyTextButton) {
                    CallbackPanel.PerformCallback($("." + myNotyTextButton).val())
                },
                idGjuha: hfState.Get("idGjuha")
            });
        }



        exitAction.clickAction = function (s, e) { window.close(); };
        e.Actions[e.Actions.length - 3] = saveAsAction;
        e.Actions[e.Actions.length - 2] = loadCustomLayoutAction;
        e.Actions[e.Actions.length - 1] = exitAction;
    }
}

function reportDesigner_endCallback(s, e) {

    if (s["cpLayoutSaved"]) {
        hfState.Remove("saveMode")
        delete s["cpLayoutSaved"];
    }
    var mesazhi=s["cpMesazhi"];
    if (mesazhi) {
        mesazhi = JSON.parse(mesazhi);
        if (mesazhi.Status)
            myMesazh.ShtoMesazhSuksesi(mesazhi.PershkrimMesazhi);
        else
            myMesazh.ShtoMesazhGabimi(mesazhi.PershkrimMesazhi);
        delete s["cpMesazhi"];
    }
    

}