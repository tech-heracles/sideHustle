;
var RaporteUtils = {
    IsEditable: false,
    Parameter: null,
    Viewer: null,
    OldViewer: null,
    PageNumber: 0,
    ClickTimeOut: null
};

RaporteUtils.InitReportViewer = function (s, e) {
    RaporteUtils.Viewer = s;
    //s.previewModel.reportPreview.zoom(1.05);
    var rapEmriReal = hfState.Get("RaportiEmerReal");
    if (rapEmriReal == "shpenzimeParapaguaraProcredit" || rapEmriReal == "shitjesipasgrupeartikjvestatistikor")
        s.previewModel.reportPreview.zoom(1.10);
    else
        s.previewModel.reportPreview.zoom(1.05);
    //$('div[title="Export Options"]').remove();
    if ($('div[title="Search"]')[1]) {
        $('div[title="Search"]')[1].click();
        $('div[title="Collapse"]').click();
    }
    if (RaporteUtils.PageNumber > 0)
        RaporteUtils.ReportViewerGoToPage();

    RaporteUtils.SetDefaultExportOptions();
};

RaporteUtils.IsReportOpened = function () {
    return (hfState.Get("oldViewer") == true || RaporteUtils.Viewer.GetReportPreview().reportId != null);
};

RaporteUtils.SetDefaultExportOptions = function () {
    var exportOptions = RaporteUtils.Viewer.previewModel.reportPreview.exportOptionsModel();
    if (exportOptions && hfState.Get("isMultiDesignReport") !== undefined && hfState.Get("isMultiDesignReport") === false) {
        exportOptions["docx"].docxExportMode("SingleFile");
        exportOptions["rtf"].rtfExportMode("SingleFile");
    }
};

RaporteUtils.IsForCustomExport = function () {
    var rapEmriReal = hfState.Get("RaportiEmerReal");
    switch (rapEmriReal) {
        case "hyrjeVodafone":
        case "gjendjeArtikujtVodafone":
        case "veprimeTeAnulluaraVodafone":
        case "gjendjaEProdukteveLoan":
        case "porosiDealerVodafone":
        case "shitjeAnalitikeVodafone":
        case "hyrjeVodafoneNdermarrjeBije":
        case "veprimeTeAnulluaraVodafoneNdermarrjeBije":
        case "gjendjaMagazinesSipasDetajimeve":
        case "logePerKartelePunonjesi":
        case "RptKartelaLlogariveFormat2":
        case "labourOfficeReport":
        case "lejeVjetore":
        case "punonjesQenderKosto":
        case "listeArketimeAnullimePostpaid":
            return true;
        default:
            return false;
    }
};

RaporteUtils.ExportReport = function (exportFormat, actionExportToClick, arg) {
    switch (exportFormat) {
        case "xlsx":
        case "xls":
        case "csv":
            if (!RaporteUtils.IsForCustomExport())
                break;
            $('#exportOption').val("CustomExport_" + exportFormat);
            exportButton.DoClick();
            return;
    }
    actionExportToClick(arg);
};

RaporteUtils.ModifyExportOptions = function (s, e) {
    var actionExportTo = e.GetById(DevExpress.Report.Preview.ActionId.ExportTo);
    var actionExportToClick = actionExportTo.clickAction;
    actionExportTo.clickAction = function (arg) {
        switch (arg.itemData.text) {
            case "Raw XLSX":
            case "Raw XLS":
                RaporteUtils.Viewer.GetPreviewModel().reportPreview.exportOptionsModel()[arg.itemData.format].rawDataMode(true);
                actionExportToClick(arg);
                RaporteUtils.Viewer.GetPreviewModel().reportPreview.exportOptionsModel()[arg.itemData.format].rawDataMode(false);
                break;
            default:
                RaporteUtils.ExportReport(arg.itemData.format, actionExportToClick, arg);
                break;
        }
    };
};

RaporteUtils.ModifyExportOptionsOldViewer = function (e) {
    var actionExportTo = e.GetById(DevExpress.Report.Preview.ActionId.ExportTo);
    actionExportTo.clickAction = function (arg) {
        var exportFormat = arg.itemData.format;
        if (!exportFormat)
            return;
        switch (arg.itemData.text) {
            case "Raw XLSX":
            case "Raw XLS":
                $('#exportOption').val("CustomExport_" + exportFormat + "_raw");
                exportButton.DoClick();
                break;
            case "xlsx":
            case "xls":
            case "csv":
                if (!RaporteUtils.IsForCustomExport())
                    RaporteUtils.OldViewer.SaveToDisk(exportFormat);
                $('#exportOption').val("CustomExport_" + exportFormat);
                exportButton.DoClick();
                break;
            default:
                RaporteUtils.OldViewer.SaveToDisk(exportFormat);
                break;
        }
    };
};

RaporteUtils.ruajStyleRaportiSuccess = function (result) {
    try {
        window.parent.SessionTimeout.sendKeepAlive();
    }
    catch (e) {
    }

    if (result)
        if (myMesazh != undefined)
            myMesazh.ShtoMesazhSuksesi(hfState.Get("ReportToolbarMsgStiliZgjedhurURuajt")); 
        else
            alert(hfState.Get("ReportToolbarMsgStiliZgjedhurURuajt"));
    else
        if (myMesazh != undefined)
            myMesazh.ShtoMesazhGabimi(hfState.Get("ReportToolbarMsgErrorGjateRuajtjesSeStilit")); 
        else
            alert(hfState.Get("ReportToolbarMsgErrorGjateRuajtjesSeStilit"));
};

RaporteUtils.RuajStilRaporti = function () {
    var reportStyle = hfState.Get('ReportStyle');
    var idDesign = hfState.Get("IdReportDesign");

    $.ajax({
        type: "POST",
        processData: false,
        contentType: "application/json",
        dataType: "JSON",
        url: Utils.getServerApiUrl("Konfigurime", "ruajStyleRaporti"),
        data: JSON.stringify({ styleName: reportStyle, zoomFactor: 0, exportFormat: 0, exportMode: 0, idDesign: idDesign })
    }).done(RaporteUtils.ruajStyleRaportiSuccess);
};

RaporteUtils.AddRawExportOption = function (e) {
    if (RaporteUtils.IsForCustomExport())
        return;
    var actionExportTo = e.GetById(DevExpress.Report.Preview.ActionId.ExportTo);

    actionExportTo.items()[0].items.push({
        format: "xlsx",
        text: "Raw XLSX"
    });
    actionExportTo.items()[0].items.push({
        format: "xls",
        text: "Raw XLS"
    });
};


function PerktheToolbar(s, e) {
    var arrayToolbar = e.Actions;
    for (var i = 0; i < arrayToolbar.length; i++) {
        switch (arrayToolbar[i].id) {
            case "dxxrp-first-page":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonFirstPage");
                break;
            case "dxxrp-prev-page":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonPreviousPage");
                break;
            case "dxxrp-pagination":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonPageCount");
                break;
            case "dxxrp-next-page":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonNextPage");
                break;
            case "dxxrp-last-page":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonLastPage");
                break;
            case "dxxrp-multipage-toggle":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonShumeFaqe");
                break;
            case "dxxrp-zoom-out":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonZoomOut");
                break;
            case "dxxrp-zoom-selector":
                arrayToolbar[i].text = hfState.Get(" ReportToolbarButtonZoomToWholePage");
                break;
            case "dxxrp-zoom-in":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonZoomIn");
                break;
            case "dxxrp-highlight-editing-fields":
                arrayToolbar[i].text = hfState.Get("ReportToolbarEdito");
                break;
            case "dxxrp-print":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonPrint");
                break;
            case "dxxrp-print-page":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonPrintPage");
                break;
            case "dxxrp-export-menu":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonEksportTo");
                break;
            case "dxxrp-search":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonSearch");
                break;
            case "dxxrp-design":
                arrayToolbar[i].text = hfState.Get("ReportToolbarButtonDesigner");
                break;
        }                
    }
}

RaporteUtils.ChangePage = function (itemId) {
    var lastIndex = (hfState.Get("reportPageCount") - 1);
    var index = RaporteUtils.Paginator.option('value');
    var value = -1;

    if (index > 0 && itemId == "dxxrp-first-page")
        value = 0;
    else if (index > 0 && itemId == "dxxrp-prev-page")
        value = index - 1;
    else if (index < lastIndex && itemId == "dxxrp-next-page")
        value = index + 1;
    else if (index < lastIndex && itemId == "dxxrp-last-page")
        value = lastIndex;

    if (value > -1)
        RaporteUtils.Paginator.option('value', value);
};

RaporteUtils.Pages = function () {
    var numberArray = [];
    for (var i = 0; i < hfState.Get("reportPageCount"); i++) {
        numberArray.push({ index: i, text: (i + 1) });
    }
    return new DevExpress.data.DataSource({
        pageSize: 100,
        paginate: true,
        store: numberArray
    });
};

RaporteUtils.EnabledOldViewerItems = function (e) {
    var actions = e.Actions;
    actions.map(function (item) {
        switch (item.id) {
            case "dxxrp-first-page":
            case "dxxrp-prev-page":
            case "dxxrp-next-page":
            case "dxxrp-last-page":
                item.clickAction = function () { RaporteUtils.ChangePage(item.id); };
                item.disabled = false;
                break;
            case "dxxrp-search":
                item.clickAction = function () { RaporteUtils.OldViewer.Search(); };
                item.disabled = false;
                break;
            case "dxxrp-print":
                item.clickAction = function () { RaporteUtils.OldViewer.Print(); };
                item.disabled = false;
                break;
            case "dxxrp-print-page":
                item.clickAction = function () { RaporteUtils.OldViewer.Print(RaporteUtils.Paginator.option('value')); };
                item.disabled = false;
                break;
            case "dxxrp-pagination":
                item.disabled = false;
                break;
            case "dxxrp-multipage-toggle":
            case "dxxrp-zoom-out":
            case "dxxrp-zoom-selector":
            case "dxxrp-zoom-in":
            case "dxxrp-highlight-editing-fields":
                item.visible = false;
                break;
            default:
                item.disabled = function () { return false; };
        }
        return item;
    });

    var paginator = {
        text: hfState.Get("ReportToolbarComboBoxStiliTooltip"),
        imageClassName: "dxrd-image-zoom",
        disabled: false,
        id: "dxxrp-pagination",
        visible: true,
        onValueChanged: function (data) {
            RaporteUtils.Parameter = "ChangePage";
            if (RaporteUtils.ClickTimeOut)
                clearTimeout(RaporteUtils.ClickTimeOut);

            RaporteUtils.ClickTimeOut = setTimeout(function () {
                RaporteUtils.OldViewer.GotoPage(data.value);
            }, 350);

            data.component._refresh();
        },
        onInitialized: function (data) {
            RaporteUtils.Paginator = data.component;
        },
        templateName: "custom-toolbarItem-template-paginator",
        currentValue: 0,
        valueExpr: "index",
        displayExpr: "text",
        searchEnabled: true,
        searchMode: 'startswith',
        fieldTemplate: function (data, container) {
            var textField = $("<div class='product-name'></div>");
            textField.dxTextBox({
                value: (!data ? "" : (data.text + " of " + hfState.Get("reportPageCount")))
            });
            container.append(textField);
        }
    };
    paginator.dataSource = RaporteUtils.Pages();
    actions[actions.findIndex(function (item) { return item.id == "dxxrp-pagination"; })] = paginator;


    var actionExportTo = e.GetById(DevExpress.Report.Preview.ActionId.ExportTo);

    actionExportTo.items()[0].items.push({ format: "pdf", text: "PDF" });
    actionExportTo.items()[0].items.push({ format: "xls", text: "XLS" });
    actionExportTo.items()[0].items.push({ format: "xlsx", text: "XLSX" });
    actionExportTo.items()[0].items.push({ format: "rtf", text: "RTF" });
    actionExportTo.items()[0].items.push({ format: "mht", text: "MHT" });
    actionExportTo.items()[0].items.push({ format: "html", text: "HTML" });
    actionExportTo.items()[0].items.push({ format: "text", text: "Text" });
    actionExportTo.items()[0].items.push({ format: "csv", text: "CSV" });
    actionExportTo.items()[0].items.push({ format: "image", text: "Image" });

    RaporteUtils.ModifyExportOptionsOldViewer(e);
};

RaporteUtils.CustomizeMenuActions = function (s, e) {
    RaporteUtils.MenuEvent = e;
    RaporteUtils.MenuSender = s;
    RaporteUtils.ModifyExportOptions(s, e);
    var actions = e.Actions;
    if (hfState.Get("oldViewer") == true)
        RaporteUtils.EnabledOldViewerItems(e);

    PerktheToolbar(s, e);
    var designsCol = (hfState.Get("ReportDesigns")) ? JSON.parse(hfState.Get("ReportDesigns")) : null;
    var idDesignZgjedhur = hfState.Get("IdReportDesign");
    this.IsEditable = designsCol != null && designsCol.filter(function (item) { return item.IdRaportDesign == idDesignZgjedhur; })[0].IsEditable;
    RaporteUtils.AddRawExportOption(e);

    if (designsCol) {
        actions.unshift({ 
            text: hfState.Get("ReportToolbarButtonZgjidhDizajnin") ,
            imageClassName: "dxrd-image-zoom",
            disabled: false,
            visible: true,
            onValueChanged: function (data) {
                hfState.Set("IdReportDesign", data.value);
                RaporteUtils.Parameter = "changeDesign";
                ASPxCallbackPanel1.PerformCallback("changeDesign");
            },
            templateName: "custom-designerSelector-template",
            items: JSON.parse(hfState.Get("ReportDesigns")),//["Design 1", "Design 2", "Design 3"],
            currentValue: ko.observable(idDesignZgjedhur),
            valueExpr: "IdRaportDesign",
            displayExpr: "Pershkrim",
            itemTemplate: function (data) {
                return "<div title='" + data.Pershkrim + "'>" + data.Pershkrim + "</div>";
            }
        });
    }

    if (hfState.Get("ShfaqButonEksportoPerTatime")) {
        actions.push({
            text: (hfState.Get("exportTatimeTooltip") ? hfState.Get("exportTatimeTooltip") : hfState.Get("ReportToolbarButtonEksportoPerTatimet")), 
            hasSeparator: false, 
            disabled: ko.observable(false),
            visible: true,
            clickAction: function () {
                if (!RaporteUtils.IsReportOpened()) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgShtypShikoEksport"));
                    return;
                }
                $('#exportOption').val("SaveToDiskTatime");
                exportButton.DoClick();
            },
            imageClassName: "custom-button-export-tatime"
        });
    }

    if (hfState.Get("ShfaqButonEksportoVeprimtariDitore")) {
        actions.push({
            text: hfState.Get("ReportToolbarButtonEksportoVepDitore"),
            hasSeparator: false,
            disabled: ko.observable(false),
            visible: true,
            clickAction: function () {
                if (!RaporteUtils.IsReportOpened()) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgShtypShikoEksport"));
                    return;
                }
                $('#exportOption').val("saveToDiskVeprimtariaDitore");
                saveVeprimtariaDitore.DoClick();
            },
            imageClassName: "custom-button-export-veprimtari-ditore"
        });
    }

    if (hfState.Get("ShfaqButonEksportoBirthdayCard")) {
        actions.push({
            text: hfState.Get("ReportToolbarButtonExpKartolinaDitl") ,
            hasSeparator: false,
            disabled: ko.observable(false),
            visible: true,
            clickAction: function () {
                if (!RaporteUtils.IsReportOpened()) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgShtypShikoEksport")); 
                    return;
                }
                $('#exportOption').val("saveToDiskBirthdayCard");
                exportButton.DoClick();
            },
            imageClassName: "custom-button-export-birthday-card"
        });
    }

    if (hfState.Get("EnableStyle")) {
        actions.push({
            text: hfState.Get("ReportToolbarComboBoxStiliTooltip"), 
            imageClassName: "dxrd-image-zoom",
            disabled: false,
            visible: true,
            onValueChanged: function (data) {
                hfState.Set("ReportStyle", data.value);
                RaporteUtils.Parameter = "changeStyle";
                ASPxCallbackPanel1.PerformCallback("changeStyle");
            },
            templateName: "custom-toolbarItem-template",
            items: JSON.parse(hfState.Get("ReportStyles")),
            currentValue: ko.observable(hfState.Get("ReportStyle")),
            valueExpr: "FileName",
            displayExpr: "EmerStili"
        });
    }

    actions.push({
        text: hfState.Get("ReportToolbarButtonZgjidhOrientimin"), 
        imageClassName: "dxrd-image-zoom",
        disabled: false,
        visible: true,
        onValueChanged: function (data) {
            hfState.Set("ReportOrientation", data.value);
            RaporteUtils.Parameter = "changeOrientation";
            ASPxCallbackPanel1.PerformCallback("changeOrientation");
        },
        templateName: "custom-toolbarItem-template",
        items: JSON.parse(hfState.Get("ReportOrientations")),
        currentValue: ko.observable(hfState.Get("ReportOrientation"))
    });

    actions.push({
        text: hfState.Get("ReportToolbarButtonRuajStil"),
        hasSeparator: false,
        disabled: ko.observable(false),
        visible: true,
        onClick: RaporteUtils.RuajStilRaporti,
        templateName: "custom-toolbarItem-template-button"
    });

    if (this.IsEditable) {
        actions.push({
            text: hfState.Get("ReportToolbarMsgModifiko"),
            hasSeparator: false,
            disabled: ko.observable(false),
            visible: true,
            onClick: function () {
                if (!RaporteUtils.IsReportOpened()) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgShtypShikoEdit"));
                    return;
                }
                if (hfState.Get("isMultiDesignReport")) {
                    myMesazh.ShtoMesazhInformues(hfState.Get("msgSingleDesignEdit"));
                    return;
                }
                var urlHost = Utils.getServerUrlHost();
                var queryString = {
                    key: hfState.Get("guidString"),
                    subRaport: Utils.getServerUrl().indexOf("RaportiShpejte") > -1,
                    raportdyte: Utils.getUrlVar("raportdyte"),
                    idRaportDesign: hfState.Get("IdReportDesign"),
                    urlReferuesi: location.href,
                    orientimi: hfState.Get("ReportOrientation"),
                    scopeID: Utils.getUrlVar("scopeID")
                };
                window.open(urlHost + "/Designer.aspx?" + Utils.KonvertoObjectQueryString(queryString));
            },
            templateName: "custom-toolbarItem-template-button"
        });
    }

 };

RaporteUtils.DeactivateViewer = function () {
    RaporteUtils.PageNumber = 0;
    if (RaporteUtils.Parameter)
        RaporteUtils.Viewer.Close();

    RaporteUtils.Parameter = null;
};

RaporteUtils.ReportViewerGoToPage = function () {
    RaporteUtils.Viewer.previewModel.reportPreview.pages.subscribe(function () {
        if (RaporteUtils.PageNumber > 0)
            RaporteUtils.Viewer.previewModel.GoToPage(RaporteUtils.PageNumber);
    });
};