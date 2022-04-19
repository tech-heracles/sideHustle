<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ImbReportToolbar.ascx.cs" Inherits="PlatinumWeb.ImbReportToolbar" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<script type="text/javascript">
    // <![CDATA[
    CustomToolbarClientControl = {
        PageIndex: 0,
        PageCount: 0,
        GetToolbarClientObject: function (shortName) {
            return this.GetClientObject(userControlPrefixes + shortName);
        },
        GetClientObject: function (shortName) {
            return ASPxClientControl.GetControlCollection().Get(shortName);
        },
        GetClientObjectFromPostFixId: function (shortName) {
            return ASPxClientControl.GetControlCollection().Get($("[id$='" + shortName + "']").attr('id'));
        },
        GetReportViewer: function () {
            return window[this.GetToolbarClientObject('HfState').Get('ReportViewerClientID')];
        },
        OnPageLoad: function (e) {
            this.PageIndex = e.PageIndex;
            this.PageCount = e.PageCount;
            this.UpdateView(e.IsFirstPage(), e.IsLastPage());
            if (this.GetToolbarClientObject('HfState').Get('arsyeReload') == "SaveToDisk100")
                this.SaveToDisk();
            if (this.GetToolbarClientObject('HfState').Get('arsyeReload') == "Print")
                this.GetReportViewer().Print();
            this.SetWindowWidth();
        },
        UpdateView: function (isFirstPage, isLastPage) {
            this.SetPageIndexItems(this.GetToolbarClientObject('ASPxComboBox_PageIndex'));
            this.SetPageIndexItems(this.GetClientObjectFromPostFixId('ASPxComboBox_PageIndex_B'));
            this.GetToolbarClientObject('ASPxTextBox_PageCount').SetValue(this.PageCount);
            this.GetClientObjectFromPostFixId('ASPxTextBox_PageCount_B').SetValue(this.PageCount);
            this.SetEnabled(!isFirstPage, !isLastPage);
        },
        SetPageIndexItems: function (comboBox_PageIndex) {
            if (comboBox_PageIndex.GetItemCount() != this.PageCount) {
                comboBox_PageIndex.BeginUpdate();
                comboBox_PageIndex.ClearItems();
                for (i = 0; i < this.PageCount; i++)
                    comboBox_PageIndex.AddItem((i + 1).toString());
                comboBox_PageIndex.EndUpdate();
            }
            comboBox_PageIndex.SetSelectedIndex(this.PageIndex);
        },
        SetEnabled: function (hasPrevPage, hasNextPage) {
            this.GetToolbarClientObject('ASPxButton_FirstPage').SetEnabled(hasPrevPage);
            this.GetToolbarClientObject('ASPxButton_PrevPage').SetEnabled(hasPrevPage);
            this.GetToolbarClientObject('ASPxButton_NextPage').SetEnabled(hasNextPage);
            this.GetToolbarClientObject('ASPxButton_LastPage').SetEnabled(hasNextPage);
            this.GetToolbarClientObject('ASPxButton_Search').SetEnabled(true);
            this.GetClientObjectFromPostFixId('ASPxButton_FirstPage_B').SetEnabled(hasPrevPage);
            this.GetClientObjectFromPostFixId('ASPxButton_PrevPage_B').SetEnabled(hasPrevPage);
            this.GetClientObjectFromPostFixId('ASPxButton_NextPage_B').SetEnabled(hasNextPage);
            this.GetClientObjectFromPostFixId('ASPxButton_LastPage_B').SetEnabled(hasNextPage);
            this.GetClientObjectFromPostFixId('ASPxButton_Search_B').SetEnabled(true);
            //this.GetToolbarClientObject('ASPxButton_Search').SetEnabled(this.GetReportViewer().IsSearchAllowed());
        },
        Search: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "Search");
            this.GetReportViewer().Search();
        },
        Print: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "Print");
            this.GetReportViewer().Print();

        },
        PrintPage: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "PrintPage");
            this.GetReportViewer().Print(this.PageIndex);
        },
        GotoFirstPage: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoFirstPage");
            this.GetReportViewer().GotoPage(0);
        },
        GotoPrevPage: function () {
            var gotoPage = this.PageIndex - 1;
            if (gotoPage == 0)
                this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoFirstPage");
            else
                this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoPrevPage");
            this.GetReportViewer().GotoPage(gotoPage);
        },
        GotoSelectedPage: function (sender) {
            var gotoPage = sender.GetSelectedIndex();
            if (gotoPage == 0)
                this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoFirstPage");
            else
                this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoSelectedPage");
            this.GetReportViewer().GotoPage(gotoPage);
        },
        GotoNextPage: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoNextPage");
            this.GetReportViewer().GotoPage(this.PageIndex + 1);
        },
        GotoLastPage: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "GotoLastPage");
            this.GetReportViewer().GotoPage(this.PageCount - 1);
        },
        SaveToDisk: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "SaveToDisk");
            this.GetToolbarClientObject('ASPxCheckBox_Raw').SetChecked(false);
            this.GetClientObjectFromPostFixId('ASPxCheckBox_Raw_B').SetChecked(false);
            this.GetReportViewer().SaveToDisk(this.GetExportFormat());
        },
        SaveToDiskPerTatime: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "SaveToDiskTatime");
            //var checkBoxRaw = this.GetToolbarClientObject('ASPxCheckBox_Raw');
            //checkBoxRaw.SetChecked(false);
            this.GetReportViewer().SaveToDisk(this.GetExportFormat());
        },
        SaveToDiskVeprimtariaDitore: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "SaveToDiskVeprimtariaDitore");
            this.GetReportViewer().Refresh();
        },
        SaveToDiskRaw: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "SaveToDisk");
            this.GetToolbarClientObject('ASPxCheckBox_Raw').SetChecked(true);
            this.GetClientObjectFromPostFixId('ASPxCheckBox_Raw_B').SetChecked(true);
            this.GetReportViewer().SaveToDisk(this.GetExportFormat());
        },
        SaveToWindow: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "SaveToWindow");
            this.GetToolbarClientObject('ASPxCheckBox_Raw').SetChecked(false);
            this.GetClientObjectFromPostFixId('ASPxCheckBox_Raw_B').SetChecked(false);
            this.GetReportViewer().SaveToWindow(this.GetExportFormat());
        },
        GetExportFormat: function () {
            var formatComboSelected = this.GetToolbarClientObject('ASPxComboBox_ExportFormat').GetSelectedItem();
            return formatComboSelected.value.replace(/\d/g, ''); //i heqim numrat qe mund te kete
        },
        ChangeStyle: function (s, e) {
            this.GetToolbarClientObject('HfState').Set('reportStyle', s.GetValue());
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "changeStyle");
            this.GetReportViewer().Refresh();
        },
        ChangeExportMode: function (s, e) {
            var mySelectedItem = s.GetSelectedItem();
            this.GetToolbarClientObject('ASPxComboBox_ExportFormat').SetSelectedItem(mySelectedItem);
            this.GetClientObjectFromPostFixId('ASPxComboBox_ExportFormat_B').SetSelectedItem(mySelectedItem);
            this.GetToolbarClientObject('HfState').Set('reportExportMode', mySelectedItem.index);
        },
        GetStyle: function () {
            if (this.GetToolbarClientObject('ASPxComboBox_Style') != undefined) {
                var stili = this.GetToolbarClientObject('ASPxComboBox_Style').GetSelectedItem();
                return stili.value;
            }
        },
        ChangeZoom: function (s, e) {
            var mySelectedItem = s.GetSelectedItem();
            this.GetToolbarClientObject('ASPxComboBox_ZoomFactor').SetSelectedItem(mySelectedItem);
            this.GetClientObjectFromPostFixId('ASPxComboBox_ZoomFactor_B').SetSelectedItem(mySelectedItem);
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "ChangeZoom");
            return this.ChangeZoomExternal(s);
        },
        InitZoom: function (s, e) {
            
        },
        ChangeZoomExternal: function (zoomFactorKontrol) {
            var zoomFactor = zoomFactorKontrol.GetValue();
            this.GetToolbarClientObject('HfState').Set('zoomFactor', zoomFactor);
            
            this.SetWindowWidth();
            var hfRilodo = $('#' + this.GetToolbarClientObject('HfState').Get('HfRilodo'));
            if (hfRilodo != undefined)
                hfRilodo.val(true);
            this.GetReportViewer().Refresh();
            return this;
        },
        SetWindowWidth: function () {
            this.GetToolbarClientObject('HfState').Set('windowWidth', $(window).width());
        },
        ChangeOrientation: function (s, e) {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "ChangeOrientation");
            this.GetToolbarClientObject('HfState').Set('orientimi', s.GetValue());
            var hfRilodo = $('#' + this.GetToolbarClientObject('HfState').Get('HfRilodo'));
            if (hfRilodo != undefined)
                hfRilodo.val(true);
            this.GetReportViewer().Refresh();
        },
        GetOrientation: function () {
            var orientimi = this.GetToolbarClientObject('ASPxComboBox_Orientimi').GetSelectedItem();
            return orientimi.value;
        },
        GetArsyeReload: function () {
            return this.GetToolbarClientObject('HfState').Get('arsyeReload');
        },
        SetArsyeReload: function (arsyeReload) {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', arsyeReload);
        },
        ResetArsyeReload: function () {
            this.GetToolbarClientObject('HfState').Set('arsyeReload', '');
        },
        UHapRaporti: function () {
            this.GetToolbarClientObject('HfState').Set('eshteHapurRaporti', true);
        },
        Init: function (s, e) {
            var ClientInstanceNameString = this.GetToolbarClientObject('HfState').Get('ClientInstanceName');
            if (ClientInstanceNameString !== undefined && ClientInstanceNameString !== '')
                window[ClientInstanceNameString] = this;
            if (this.GetToolbarClientObject('HfState').Get("BottomToolbarVisible") == 'True') {
                $('#botomImbReportToolbar').show();
                $(this.GetReportViewer().GetMainElement()).after($('#botomImbReportToolbar'));
            }
            if(Utils.getServerUrl().indexOf("RaportiShpejte") != -1)
                this.GetToolbarClientObject('HfState').Set('eshteHapurRaporti', true);

        },
        SaveStyle: function (s, e) {
            var myHfState = this.GetToolbarClientObject('HfState');
            var zoomFactor = myHfState.Get('zoomFactor');
            var reportStyle = myHfState.Get('reportStyle');
            var exportFormat = myHfState.Get('reportExportFormat');
            var exportMode = myHfState.Get('reportExportMode');
            var hfRilodo = $('#' + myHfState.Get('HfRilodo'));
            var idDesign=myHfState.Get("idRaportDesign");
            var idNdermarrje = myHfState.Get("idNdermarrje");
            var idPerdoruesi = myHfState.Get("idPerdoruesi");
            if (hfRilodo != undefined)
                hfRilodo.val(false);
            $.ajax({
                pritPergjigje: false,
                url: Utils.getServerApiUrl("Konfigurime", "ruajStyleRaporti"),
                data: JSON.stringify({ styleName: reportStyle, zoomFactor: zoomFactor, exportFormat: exportFormat, exportMode: exportMode, idDesign: idDesign, idNdermarrje: idNdermarrje, idPerdoruesi: idPerdoruesi })
            }).done(this.ruajStyleRaportiSuccess);
            e.processOnServer = false;
        },
        ruajStyleRaportiSuccess: function (result) {
            try { window.parent.SessionTimeout.sendKeepAlive(); } catch (e) { };
            if (result)
                if (myMesazh != undefined)
                    myMesazh.ShtoMesazhSuksesi("Stili i zgjedhur u ruajt me sukses!");
                else
                    alert("Stili i zgjedhur u ruajt me sukses!");
            else
                if (myMesazh != undefined)
                    myMesazh.ShtoMesazhGabimi("Gabim: gjate ruajtjes se stilit te zgjedhur!");
                else
                    alert("Gabim: gjate ruajtjes se stilit te zgjedhur!");
        },
        ChangeExportMode: function (comboExportMode) {
            var saveToDiskRaw = $('.saveRawTd');
            switch (this.GetExportFormat()) {
                case "xlsx":
                    comboExportMode.SetVisible(true);
                    comboExportMode.ClearItems();
                    var njeFaqe = this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeSingleFile');
                    comboExportMode.AddItem(njeFaqe, 0);
                    comboExportMode.AddItem(this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeSingleFilePageByPage'), 1);
                    comboExportMode.SetSelectedIndex(njeFaqe);
                    comboExportMode.SetSelectedIndex(0);
                    //comboExportMode.AddItem(this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeDifferentFiles'), 2);
                    //checkBoxRaw.SetVisible(true);
                    saveToDiskRaw.show();
                    break;
                case "xls":
                    comboExportMode.SetVisible(true);
                    comboExportMode.ClearItems();
                    var njeFaqe = this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeSingleFile');
                    comboExportMode.AddItem(njeFaqe, 0);
                    comboExportMode.SetSelectedIndex(njeFaqe);
                    comboExportMode.SetSelectedIndex(0);
                    //comboExportMode.AddItem(this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeDifferentFiles'), 1);
                    //checkBoxRaw.SetVisible(true);
                    saveToDiskRaw.show();
                    break;
                case "rtf":
                    comboExportMode.SetVisible(true);
                    comboExportMode.ClearItems();
                    var njeFaqe = this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeSingleFile');
                    comboExportMode.AddItem(njeFaqe, 0);
                    comboExportMode.AddItem(this.GetToolbarClientObject('HfState').Get('ReportToolbarExportModeSingleFilePageByPage'), 1);
                    comboExportMode.SetSelectedIndex(njeFaqe);
                    comboExportMode.SetSelectedIndex(0);
                    //checkBoxRaw.SetVisible(false);
                    saveToDiskRaw.hide();
                    break;
                case "pdf":
                case "mht":
                case "html":
                case "text":
                case "csv":
                case "image":
                    saveToDiskRaw.hide();
                    break;
                default:
                    comboExportMode.SetVisible(false);
                    comboExportMode.ClearItems();
                    //checkBoxRaw.SetVisible(false);
                    saveToDiskRaw.hide();
                    break;
            }
        },
        ChangedFormat: function (s, e) {
            //var checkBoxRaw = this.GetToolbarClientObject('ASPxCheckBox_Raw');
            this.GetToolbarClientObject('HfState').Set('reportExportFormat', s.GetSelectedItem().index);
            this.GetToolbarClientObject('HfState').Set('arsyeReload', "changeExportFormat");
            var selectedItem = s.GetSelectedItem();
            this.GetToolbarClientObject('ASPxComboBox_ExportFormat').SetSelectedItem(selectedItem);
            this.GetClientObjectFromPostFixId('ASPxComboBox_ExportFormat_B').SetSelectedItem(selectedItem);
            this.ChangeExportMode(this.GetToolbarClientObject('ASPxComboBox_ExportMode'));
            this.ChangeExportMode(this.GetClientObjectFromPostFixId('ASPxComboBox_ExportMode_B'));
        },
        ChangeDesign: function (s, e) {
            var selectedDesign = s.GetSelectedItem();

            cmbDesign_A.SetSelectedItem(selectedDesign);
            cmbDesign_B.SetSelectedItem(selectedDesign);
            HfState.Set("idRaportDesign", selectedDesign.value);
            HfState.Set("arsyeReload", "ChangeDesign");
            $('#' + HfState.Get('HfRilodo')).val(true);
            this.GetReportViewer().Refresh();
            
        },
        HapRaportNeDesign: function (s, e) {
            if (!this.GetToolbarClientObject('HfState').Get('eshteHapurRaporti')) {
                myMesazh.ShtoMesazhInformues("Raporti duhet te hapet qe te behet i mundur editimi i tij!");
                return;
            }
            var urlHost = Utils.getServerUrlHost();
            var queryString = {
                key: HfState.Get("guidString"),
                subRaport: Utils.getServerUrl().indexOf("RaportiShpejte") > -1,
                raportdyte: Utils.getUrlVar("raportdyte"),
                idRaportDesign: HfState.Get("idRaportDesign"),
                urlReferuesi: location.href,
                orientimi: HfState.Get("orientimi"),
                scopeID: Utils.getUrlVar("scopeID")
            }
            window.open(urlHost + "/Designer.aspx?" + Utils.KonvertoObjectQueryString(queryString));
            e.processOnServer = false;
        }
    }
    $(document).ready(function () {
        CustomToolbarClientControl.Init();
    });
    // ]]>
</script>

<dx:ASPxRoundPanel ID="ASPxPanel" ShowHeader="False" runat="server" EnableTheming="True" EnableViewState="False" Width="100%" BackColor="White">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <dx:ASPxHiddenField ID="HfState" ClientInstanceName="HfState" runat="server"></dx:ASPxHiddenField>
            <table  border="0" style="height: 27px; border-collapse: separate;">
                <tbody>
                    <tr>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" Width="100px" EnableTheming="true" EnableClientSideAPI="True" ToolTip="Zgjidh dizajnin e ketij raporti" EnableViewState="False" ClientVisible="true" ValueType="System.Int32"
                                ID="cmbDesign_A" ClientInstanceName="cmbDesign_A">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeDesign(s, e)}" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                                Width="24px" AutoPostBack="False" ToolTip="Display the search window" EnableTheming="True"
                                EnableViewState="False" ID="ASPxButton_Search" ImageSpacing="0px">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.Search(); }"></ClientSideEvents>
                                <HoverStyle BackColor="Transparent">
                                </HoverStyle>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSearch.png" UrlDisabled="~/images/CustomToolbar/BtnSearchDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px"></td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                                ToolTip="Print the report" EnableViewState="False" ID="ASPxButton_Print">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.Print(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrint.png" UrlDisabled="~/images/CustomToolbar/BtnPrintDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                                ToolTip="Print the current page" EnableViewState="False" ID="ASPxButton_PrintPage">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.PrintPage(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrintPage.png"
                                    UrlDisabled="~/images/CustomToolbar/BtnPrintPageDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px"></td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                                Width="24px" AutoPostBack="False" ToolTip="First Page" EnableViewState="False"
                                ID="ASPxButton_FirstPage">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoFirstPage(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnFirstPage.png"
                                    UrlDisabled="~/images/CustomToolbar/BtnFirstPageDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                                Width="24px" AutoPostBack="False" ToolTip="Previous Page" EnableViewState="False"
                                ID="ASPxButton_PrevPage">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoPrevPage(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrevPage.png" UrlDisabled="~/images/CustomToolbar/BtnPrevPageDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelPage" EnableViewState="False" Font-Size="Small"
                                Text="Page">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" ToolTip="Select page number" EnableViewState="False"
                                EnableClientSideAPI="True" Width="46px" ID="ASPxComboBox_PageIndex">
                                <ClientSideEvents ValueChanged="function(s, e) { CustomToolbarClientControl.GotoSelectedPage(s); }"></ClientSideEvents>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelOf" EnableViewState="False" Font-Size="Small"
                                Text="of">
                            </dx:ASPxLabel>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxTextBox runat="server" Height="24px" EnableClientSideAPI="True" Width="46px"
                                ToolTip="Total page count" EnableViewState="False" ReadOnly="True" ID="ASPxTextBox_PageCount">
                            </dx:ASPxTextBox>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                                Width="24px" AutoPostBack="False" ToolTip="Next Page" EnableViewState="False"
                                ID="ASPxButton_NextPage">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoNextPage(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnNextPage.png" UrlDisabled="~/images/CustomToolbar/BtnNextPageDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                                Width="24px" AutoPostBack="False" ToolTip="Last Page" EnableViewState="False"
                                ID="ASPxButton_LastPage">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoLastPage(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnLastPage.png" UrlDisabled="~/images/CustomToolbar/BtnLastPageDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px"></td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                                ToolTip="Export a report and save it to the disk" EnableViewState="False" ID="ASPxButton_Save">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDisk(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSave.png" UrlDisabled="~/images/CustomToolbar/BtnSaveDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False" ClientVisible="false"
                                ToolTip="Export a report and save it to the disk Tatimore" EnableViewState="False" ID="btn_SaveRapPerTatime">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDiskPerTatime(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveTatimore.png" UrlDisabled="~/images/CustomToolbar/BtnSaveDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                                ToolTip="Export a report and show it in a new window" EnableViewState="False"
                                ID="ASPxButton_SaveWindow">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToWindow(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveWindow.png"
                                    UrlDisabled="~/images/CustomToolbar/BtnSaveWindowDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td id="SaveRawTd_B" class="saveRawTd" style="width: 24px; height: 24px">
                            <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                                ToolTip="Export a report and show it in a new window" EnableViewState="False"
                                ID="ASPxButton_SaveRaw">
                                <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDiskRaw(); }"></ClientSideEvents>
                                <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveBlackandWhite.png"
                                    UrlDisabled="~/images/CustomToolbar/BtnSaveWindowDisabled.png">
                                </Image>
                                <Paddings Padding="0px"></Paddings>
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" ToolTip="Select export format" EnableViewState="False"
                                ID="ASPxComboBox_ExportFormat" Width="120px" SelectedIndex="0">
                                <Items>
                                    <dx:ListEditItem Value="pdf" Text="Pdf"></dx:ListEditItem>
                                    <dx:ListEditItem Value="xls" Text="Xls"></dx:ListEditItem>
                                    <dx:ListEditItem Value="xlsx" Text="Xlsx"></dx:ListEditItem>
                                    <dx:ListEditItem Value="rtf" Text="Rtf"></dx:ListEditItem>
                                    <dx:ListEditItem Value="mht" Text="Mht"></dx:ListEditItem>
                                    <dx:ListEditItem Value="html" Text="Html"></dx:ListEditItem>
                                    <dx:ListEditItem Value="txt" Text="Text"></dx:ListEditItem>
                                    <dx:ListEditItem Value="csv" Text="Csv"></dx:ListEditItem>
                                    <dx:ListEditItem Value="png" Text="Image"></dx:ListEditItem>
                                </Items>
                                <ClientSideEvents Init="function(s,e){CustomToolbarClientControl.ChangedFormat(s,e);}"
                                    SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangedFormat(s,e);}" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxCheckBox runat="server" Height="24px" ClientVisible="false" ToolTip="Zgjidh per excel pa formatim" EnableViewState="False"
                                ID="ASPxCheckBox_Raw" Width="24px">
                            </dx:ASPxCheckBox>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" ValueType="System.Int32" ClientVisible="false" ToolTip="Zgjidh menyren e exportit" EnableViewState="False"
                                ID="ASPxComboBox_ExportMode" Width="120px">
                                <Items>
                                    <dx:ListEditItem Value="0" Text="Skedar i vetem"></dx:ListEditItem>
                                    <dx:ListEditItem Value="1" Text="Shume faqe"></dx:ListEditItem>
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeExportMode(s,e);}" />
                            </dx:ASPxComboBox>
                        </td>
                        <%-- <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelZoomFactor" AssociatedControlID="ASPxComboBox_ZoomFactor" EnableViewState="False" Font-Size="Small"
                                Text="Zoom Factor in %">
                            </dx:ASPxLabel>
                        </td>--%>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Width="90px" Height="24px" ToolTip="Select zoom factor" EnableViewState="False" ClientVisible="false"
                                ID="ASPxComboBox_ZoomFactor" SelectedIndex="0" ValueType="System.Int32">
                                <Items>
                                    <dx:ListEditItem Text="Page Width" Value="0" />
                                    <dx:ListEditItem Text="100" Value="100" />
                                    <dx:ListEditItem Text="115" Value="115" />
                                    <dx:ListEditItem Text="130" Value="130" />
                                    <dx:ListEditItem Text="145" Value="145" />
                                    <dx:ListEditItem Text="160" Value="160" />
                                    <dx:ListEditItem Text="185" Value="185" />
                                </Items>
                                <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeZoom(s,e);}" Init="function(s,e){CustomToolbarClientControl.InitZoom(s,e);}" />
                            </dx:ASPxComboBox>
                        </td>
                        <%--<td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelStili" EnableTheming="true" AssociatedControlID="ASPxComboBox_Style" EnableViewState="False" Font-Size="Small"
                                Text="Style">
                            </dx:ASPxLabel>
                        </td>--%>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" Width="88px" EnableTheming="true" ToolTip="Select report style" EnableViewState="False"
                                ID="ASPxComboBox_Style">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeStyle(s,e);}" />
                            </dx:ASPxComboBox>
                        </td>
                        <%--                        <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelOrientimi" EnableTheming="true" AssociatedControlID="ASPxComboBox_Orientimi" EnableViewState="False" Font-Size="Small"
                                Text="Orientation">
                            </dx:ASPxLabel>
                        </td>--%>
                        <td style="height: 24px">
                            <dx:ASPxComboBox runat="server" Height="24px" Width="100px" EnableTheming="true" ToolTip="Select report Orientation" EnableViewState="False"
                                ID="ASPxComboBox_Orientimi">
                                <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeOrientation(s,e);}" />
                            </dx:ASPxComboBox>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxButton ID="ASPxButton_SaveStyle" runat="server" Height="24px" EnableViewState="false" EnableTheming="true" Text="Save Style">
                                <ClientSideEvents Click="function(s,e){CustomToolbarClientControl.SaveStyle(s,e);}" />
                            </dx:ASPxButton>
                        </td>
                        
                        <td style="height: 24px">
                            <dx:ASPxButton ID="btnExportVeprimtariaDitore" runat="server" Height="24px" EnableViewState="false" AutoPostBack="false" EnableTheming="true" Text="Eksporto">
                                <ClientSideEvents Click="function(s,e){CustomToolbarClientControl.SaveToDiskVeprimtariaDitore()}" />
                            </dx:ASPxButton>
                        </td>
                        <td style="height: 24px">
                            <dx:ASPxButton ID="btnDesigner" runat="server" Height="24px" EnableViewState="false" AutoPostBack="false" EnableTheming="true" Text="Edit">
                                <ClientSideEvents Click="function(s, e){CustomToolbarClientControl.HapRaportNeDesign(s, e);}" />
                            </dx:ASPxButton>
                        </td>
                    </tr>
                </tbody>
            </table>
        </dx:PanelContent>
    </PanelCollection>
    <Border BorderWidth="0px" />
    <HeaderStyle BackColor="#DEDEDE">
        <BorderLeft BorderStyle="None" />
        <BorderRight BorderStyle="None" />
        <BorderBottom BorderStyle="None" />
    </HeaderStyle>
    <ClientSideEvents Init="function(s,e){CustomToolbarClientControl.Init(s,e);}" />
</dx:ASPxRoundPanel>
<div id="botomImbReportToolbar" class="ImbReportToolBar" style="padding-left: 12px;">
    <table border="0" style="height: 27px; border-collapse: separate;">
        <tbody>
            <tr>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" Width="100px" EnableTheming="true" ToolTip="Zgjidh dizajnin e ketij raporti" EnableViewState="False"
                        ID="cmbDesign_B" ClientInstanceName="cmbDesign_B" ValueType="System.Int32" >
                        <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeDesign(s, e)}" />
                    </dx:ASPxComboBox>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                        Width="24px" AutoPostBack="False" ToolTip="Display the search window" EnableTheming="True"
                        EnableViewState="False" ID="ASPxButton_Search_B" ImageSpacing="0px">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.Search(); }"></ClientSideEvents>
                        <HoverStyle BackColor="Transparent">
                        </HoverStyle>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSearch.png" UrlDisabled="~/images/CustomToolbar/BtnSearchDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="height: 24px"></td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                        ToolTip="Print the report" EnableViewState="False" ID="ASPxButton_Print_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.Print(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrint.png" UrlDisabled="~/images/CustomToolbar/BtnPrintDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                        ToolTip="Print the current page" EnableViewState="False" ID="ASPxButton_PrintPage_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.PrintPage(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrintPage.png"
                            UrlDisabled="~/images/CustomToolbar/BtnPrintPageDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="height: 24px"></td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                        Width="24px" AutoPostBack="False" ToolTip="First Page" EnableViewState="False"
                        ID="ASPxButton_FirstPage_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoFirstPage(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnFirstPage.png"
                            UrlDisabled="~/images/CustomToolbar/BtnFirstPageDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                        Width="24px" AutoPostBack="False" ToolTip="Previous Page" EnableViewState="False"
                        ID="ASPxButton_PrevPage_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoPrevPage(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnPrevPage.png" UrlDisabled="~/images/CustomToolbar/BtnPrevPageDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="height: 24px">
                    <dx:ASPxLabel runat="server" ID="labelPage_B" EnableViewState="False" Font-Size="Small"
                        Text="Page">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" ToolTip="Select page number" EnableViewState="False"
                        EnableClientSideAPI="True" Width="46px" ID="ASPxComboBox_PageIndex_B">
                        <ClientSideEvents ValueChanged="function(s, e) { CustomToolbarClientControl.GotoSelectedPage(s); }"></ClientSideEvents>
                    </dx:ASPxComboBox>
                </td>
                <td style="height: 24px">
                    <dx:ASPxLabel runat="server" ID="labelOf_B" EnableViewState="False" Font-Size="Small"
                        Text="of">
                    </dx:ASPxLabel>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxTextBox runat="server" Height="24px" EnableClientSideAPI="True" Width="46px"
                        ToolTip="Total page count" EnableViewState="False" ReadOnly="True" ID="ASPxTextBox_PageCount_B">
                    </dx:ASPxTextBox>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                        Width="24px" AutoPostBack="False" ToolTip="Next Page" EnableViewState="False"
                        ID="ASPxButton_NextPage_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoNextPage(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnNextPage.png" UrlDisabled="~/images/CustomToolbar/BtnNextPageDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" EnableClientSideAPI="True"
                        Width="24px" AutoPostBack="False" ToolTip="Last Page" EnableViewState="False"
                        ID="ASPxButton_LastPage_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.GotoLastPage(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnLastPage.png" UrlDisabled="~/images/CustomToolbar/BtnLastPageDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="height: 24px"></td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                        ToolTip="Export a report and save it to the disk" EnableViewState="False" ID="ASPxButton_Save_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDisk(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSave.png" UrlDisabled="~/images/CustomToolbar/BtnSaveDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td>
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False" ClientVisible="false"
                        ToolTip="Export a report and save it to the disk Tatimore" EnableViewState="False" ID="btn_SaveRapPerTatime_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDiskPerTatime(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveTatimore.png" UrlDisabled="~/images/CustomToolbar/BtnSaveDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                        ToolTip="Export a report and show it in a new window" EnableViewState="False"
                        ID="ASPxButton_SaveWindow_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToWindow(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveWindow.png"
                            UrlDisabled="~/images/CustomToolbar/BtnSaveWindowDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td id="SaveRawTd" class="saveRawTd" style="width: 24px; height: 24px">
                    <dx:ASPxButton runat="server" Height="24px" CssClass="ctButton" Width="24px" AutoPostBack="False"
                        ToolTip="Export a report and show it in a new window" EnableViewState="False"
                        ID="ASPxButton_SaveRaw_B">
                        <ClientSideEvents Click="function(s, e) { CustomToolbarClientControl.SaveToDiskRaw(); }"></ClientSideEvents>
                        <Image Height="16px" Width="16px" Url="~/images/CustomToolbar/BtnSaveBlackandWhite.png"
                            UrlDisabled="~/images/CustomToolbar/BtnSaveWindowDisabled.png">
                        </Image>
                        <Paddings Padding="0px"></Paddings>
                    </dx:ASPxButton>
                </td>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" ToolTip="Select export format" EnableViewState="False"
                        ID="ASPxComboBox_ExportFormat_B" Width="120px" SelectedIndex="0">
                        <Items>
                            <dx:ListEditItem Value="pdf" Text="Pdf"></dx:ListEditItem>
                            <dx:ListEditItem Value="xls" Text="Xls"></dx:ListEditItem>
                            <dx:ListEditItem Value="xlsx" Text="Xlsx"></dx:ListEditItem>
                            <dx:ListEditItem Value="rtf" Text="Rtf"></dx:ListEditItem>
                            <dx:ListEditItem Value="mht" Text="Mht"></dx:ListEditItem>
                            <dx:ListEditItem Value="html" Text="Html"></dx:ListEditItem>
                            <dx:ListEditItem Value="txt" Text="Text"></dx:ListEditItem>
                            <dx:ListEditItem Value="csv" Text="Csv"></dx:ListEditItem>
                            <dx:ListEditItem Value="png" Text="Image"></dx:ListEditItem>
                        </Items>
                        <ClientSideEvents Init="function(s,e){CustomToolbarClientControl.ChangedFormat(s,e);}"
                            SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangedFormat(s,e);}" />
                    </dx:ASPxComboBox>
                </td>
                <td style="height: 24px">
                    <dx:ASPxCheckBox runat="server" Height="24px" ClientVisible="false" ToolTip="Zgjidh per excel pa formatim" EnableViewState="False"
                        ID="ASPxCheckBox_Raw_B" Width="24px">
                    </dx:ASPxCheckBox>
                </td>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" ValueType="System.Int32" ClientVisible="false" ToolTip="Zgjidh menyren e exportit" EnableViewState="False"
                        ID="ASPxComboBox_ExportMode_B" Width="120px">
                        <Items>
                            <dx:ListEditItem Value="0" Text="Skedar i vetem"></dx:ListEditItem>
                            <dx:ListEditItem Value="1" Text="Shume faqe"></dx:ListEditItem>
                        </Items>
                        <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeExportMode(s,e);}" />
                    </dx:ASPxComboBox>
                </td>
                <%-- <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelZoomFactor_B" AssociatedControlID="ASPxComboBox_ZoomFactor" EnableViewState="False" Font-Size="Small"
                                Text="Zoom Factor in %">
                            </dx:ASPxLabel>
                        </td>--%>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Width="90px" Height="24px" ToolTip="Select zoom factor" EnableViewState="False" ClientVisible="false"
                        ID="ASPxComboBox_ZoomFactor_B" SelectedIndex="0" ValueType="System.Int32">
                        <Items>
                            <dx:ListEditItem Text="Page Width" Value="0" />
                            <dx:ListEditItem Text="100" Value="100" />
                            <dx:ListEditItem Text="115" Value="115" />
                            <dx:ListEditItem Text="130" Value="130" />
                            <dx:ListEditItem Text="145" Value="145" />
                            <dx:ListEditItem Text="160" Value="160" />
                            <dx:ListEditItem Text="185" Value="185" />
                        </Items>
                        <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeZoom(s,e);}" Init="function(s,e){CustomToolbarClientControl.InitZoom(s,e);}" />
                    </dx:ASPxComboBox>
                </td>
                <%--<td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelStili_B" EnableTheming="true" AssociatedControlID="ASPxComboBox_Style" EnableViewState="False" Font-Size="Small"
                                Text="Style">
                            </dx:ASPxLabel>
                        </td>--%>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" Width="88px" EnableTheming="true" ToolTip="Select report style" EnableViewState="False"
                        ID="ASPxComboBox_Style_B">
                        <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeStyle(s,e);}" />
                    </dx:ASPxComboBox>
                </td>
                <%--                        <td style="height: 24px">
                            <dx:ASPxLabel runat="server" ID="labelOrientimi_B" EnableTheming="true" AssociatedControlID="ASPxComboBox_Orientimi" EnableViewState="False" Font-Size="Small"
                                Text="Orientation">
                            </dx:ASPxLabel>
                        </td>--%>
                <td style="height: 24px">
                    <dx:ASPxComboBox runat="server" Height="24px" Width="100px" EnableTheming="true" ToolTip="Select report Orientation" EnableViewState="False"
                        ID="ASPxComboBox_Orientimi_B">
                        <ClientSideEvents SelectedIndexChanged="function(s,e){CustomToolbarClientControl.ChangeOrientation(s,e);}" />
                    </dx:ASPxComboBox>
                </td>
                <td style="height: 24px">
                    <dx:ASPxButton ID="ASPxButton_SaveStyle_B" runat="server" Height="24px" EnableViewState="false" EnableTheming="true" Text="Save Style">
                        <ClientSideEvents Click="function(s,e){CustomToolbarClientControl.SaveStyle(s,e);}" />
                    </dx:ASPxButton>
                </td>
                 <td style="height: 24px">
                            <dx:ASPxButton ID="btnExportVeprimtariaDitore2" runat="server" Height="24px" EnableViewState="false" AutoPostBack="false" EnableTheming="true" Text="Eksporto">
                                <ClientSideEvents Click="function(s,e){CustomToolbarClientControl.SaveToDiskVeprimtariaDitore()}" />
                            </dx:ASPxButton>
                        </td>
                <td style="height: 24px">
                    <dx:ASPxButton ID="btnDesigner2" runat="server" Height="24px" EnableViewState="false" AutoPostBack="false" EnableTheming="true" Text="Edit">
                        <ClientSideEvents Click="function(s, e){CustomToolbarClientControl.HapRaportNeDesign(s, e);}" />
                    </dx:ASPxButton>
                </td>
            </tr>
        </tbody>
    </table>
</div>
