<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_LlojeBuxheti.aspx.cs" Inherits="PlatinumWeb.B_LlojeBuxheti" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUp" TagName="popUpUniversal" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/B_LlojeBuxheti.aspx-IMB.7.1.js&v76"
        type="text/javascript"></script>
</head>
<body>
 <form id="form1" runat="server" novalidate>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivStyle-Opacity="30"/>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <asp:UpdatePanel runat="server" ID="panelKryesor" UpdateMode="Conditional">
            <ContentTemplate>
                <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click"/>
                <dx:ASPxGridView    runat="server" ID="gvLlojBuxheti" EnableCallBacks="true" 
                                                                    ClientInstanceName="gvLlojBuxheti"  
                                                                    OnCustomJSProperties="gvLlojBuxheti_CustomJSProperties"
                                                                    OnAfterPerformCallback="gvLlojBuxheti_AfterPerformCallback"   
                                                                    OnCustomCallback="gvLlojBuxheti_CustomCallback" 
                                                                    OnDataBound="gvLlojBuxheti_DataBound" 
                                                                    OnHeaderFilterFillItems="gvLlojBuxheti_HeaderFilterFillItems"

                                                                    OnRowInserting="gvLlojBuxheti_RowInserting"
                                                                    OnRowUpdating="gvLlojBuxheti_RowUpdating"
                                                                    OnHtmlRowCreated="gvLlojBuxheti_HtmlRowCreated">
                                                                     
                    <SettingsEditing Mode="EditFormAndDisplayRow" />
                    
                    <ClientSideEvents RowDblClick="Row_DblClick" BeginCallback="function(s, e) {BeginCallback(s,e);}" EndCallback="function(s, e) { EndCallbackGrida(s, e); }" />
                    <Templates>
                        <EditForm>
                                <table style="width:100%">
                                    <tr>
                                        <td style="width: 20%; text-align: center">
                                            Kodi:
                                        </td>
                                        <td style="width: 30%; text-align: right">
                                            <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" ClientInstanceName="txtKodi">
                                            </dx:ASPxTextBox>
                                        </td>
                                        <td style="width: 20%; text-align: center">
                                            Pershkrimi:
                                        </td>
                                        <td style="width: 30%; text-align: right">
                                            <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="100%">
                                            </dx:ASPxTextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 30%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 30%;  text-align: right;">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 70%;">
                                                        <dx:ASPxGridViewTemplateReplacement ID="upButton" runat="server" ReplacementType="EditFormUpdateButton">
                                                        </dx:ASPxGridViewTemplateReplacement>
                                                    </td>
                                                    <td style="width: 30%;">
                                                        <dx:ASPxGridViewTemplateReplacement ID="cButton" runat="server" ReplacementType="EditFormCancelButton">
                                                        </dx:ASPxGridViewTemplateReplacement>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </EditForm>
                        </Templates>
                </dx:ASPxGridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ucPopUp:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUp:popUpUniversal>
        <asp:UpdatePanel runat="server" id="hiddenFields">
            <ContentTemplate>
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"/>
                    <asp:HiddenField ID="hfKonffillestar" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfRuaj" ClientIDMode="Static" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
