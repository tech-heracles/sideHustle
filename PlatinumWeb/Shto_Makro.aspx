<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_Makro.aspx.cs" Inherits="PlatinumWeb.Shto_Makro" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <style type="text/css">
        .AutoCompleteExtender_CompletionList
        {
            background-color: window;
            color: windowtext;
            padding: 1px;
            font-size: small;
            background-color: Gray; /*creates border with
            autocomplete_completionListElement
            background-color*/
        }
        
        /*AutoComplete flyout */
        .AutoCompleteExtender_CompletionListItem
        {
            text-align: left;
            background-color: White;
        }
        
        /* AutoComplete highlighted item */
        .AutoCompleteExtender_HighlightedItem
        {
            background-color: Silver;
            color: blue;
            font-weight: bold;
            font-size: small;
        }
    </style>
    <%--    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/aspx.js/Shto_Makro.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.10.2.min.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/aspx.js/Shto_Makro.aspx-IMB.2.1.js&v36"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Services>
                <asp:ServiceReference Path="wsfunc.asmx" />
            </Services>
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <dxtc:ASPxPageControl ID="ASPxPageControl1" runat="server" ClientInstanceName="PageControl"
                    SettingsLoadingPanel-Text="" TabSpacing="3px" Width="100%" OnActiveTabChanged="ASPxPageControl1_ActiveTabChanged"
                    ActiveTabIndex="5">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <dxwgv:ASPxGridView ID="gvMakro" runat="server" ClientInstanceName="gvMakro" Width="60%"
                                        OnRowInserting="gvMakro_RowInserting" OnAfterPerformCallback="gvMakro_AfterPerformCallback"
                                        OnRowValidating="gvMakro_RowValidating" OnStartRowEditing="gvMakro_StartRowEditing"
                                        OnHtmlRowCreated="gvMakro_HtmlRowCreated" OnCellEditorInitialize="gvMakro_CellEditorInitialize"
                                        OnInitNewRow="gvMakro_InitNewRow">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dxwgv:ASPxGridView>
                                    <br />
                                    <table class="butonat">
                                        <tr>
                                            <td style="width: 70%">
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="overview_Button" runat="server" Text="Ruaj" Width="100%" OnClick="overview_Button_Click">
                                                    <ClientSideEvents Click="function(s, e) {
	pastro();
	   merrTeDhena();
	   if(valid==false)
{alert('Ju lutem plotesoni trupin');

  e.processOnServer = false;
}   
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="overview_pastro_ASPxButton" runat="server" Text="Pastro" Width="100%"
                                                    OnClick="overview_pastro_ASPxButton_Click" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	pastro();
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="overviewAnullo" runat="server" Text="Anullo" PostBackUrl="~/Makro.aspx"
                                                    Width="100%" CausesValidation="False">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Produktet" Text="Produktet" TabStyle-Height="150px">
                            <TabStyle Height="150px">
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl4" runat="server">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxLabel ID="ASPxLabel38" runat="server" Text="Kodi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtKodi2" runat="server" Width="170px" AutoPostBack="false"
                                                    ClientInstanceName="txtKodi2">
                                                    <ClientSideEvents TextChanged="function(s, e) {

	 KodiKokaMakro.SetText(txtKodi2.GetText());
  ProcessTextCahnged('KodiKokaMakro', txtKodi2.GetText()) ;
}" />
                                                    <ValidationSettings RequiredField-IsRequired="True" ValidationGroup="entries">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                            <td nowrap="nowrap">
                                                <dx:ASPxLabel ID="ASPxLabel39" runat="server" Text="Pershkrimi:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td>
                                                <dx:ASPxTextBox ID="txtPershkrimi2" runat="server" Width="170px" AutoPostBack="false"
                                                    ClientInstanceName="txtPershkr2">
                                                    <ClientSideEvents TextChanged="function(s, e) {
	                                  
	                              	 PershkrimiKokaMakro.SetText(txtPershkr2.GetText());
  ProcessTextCahnged('PershkrimiKokaMakro', txtPershkr2.GetText()) ;               
                                            }" />
                                                    <ValidationSettings RequiredField-IsRequired="True" ValidationGroup="entries">
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RequiredField IsRequired="True" />
                                                    </ValidationSettings>
                                                </dx:ASPxTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <dxwgv:ASPxGridView ID="gvTrupi" runat="server" ClientInstanceName="gvTrupi" OnAfterPerformCallback="gvTrupi_AfterPerformCallback"
                                                    OnHtmlRowCreated="gvTrupi_HtmlRowCreated" OnCustomCallback="gvTrupi_CustomCallback"
                                                    OnCustomJSProperties="gvTrupi_CustomJSProperties" Width="800px" OnDataBound="gvTrupi_DataBound"
                                                    EnableCallBacks="False">
                                                    <Images>
                                                        <LoadingPanelOnStatusBar>
                                                        </LoadingPanelOnStatusBar>
                                                        <LoadingPanel>
                                                        </LoadingPanel>
                                                    </Images>
                                                    <ImagesFilterControl>
                                                        <LoadingPanel>
                                                        </LoadingPanel>
                                                    </ImagesFilterControl>
                                                    <ClientSideEvents BeginCallback="function(s, e) {
	
             merrTeDhena();
             
}" EndCallback="function(s,e){ enable();}" Init="function(s,e){ enable();}" />
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
                                                    <StylesEditors>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dxwgv:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <br />
                                    <table class="butonat">
                                        <tr>
                                            <td style="width: 70%">
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj" OnClick="ruaj_Button_Click"
                                                    ValidationGroup="entries" Width="100%">
                                                    <ClientSideEvents Click="function(s, e) {
	         valido(s,e);
             merrTeDhena();
             pastro();
if(valid==false)
{alert('Ju lutem plotesoni trupin');

  e.processOnServer = false;
}         
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="ASPxButton4" runat="server" Text="Pastro" OnClick="pastro_Button_Click"
                                                    Width="100%" CausesValidation="False">
                                                    <ClientSideEvents Click="function(s, e) {
	pastro();
	
}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="width: 10%">
                                                <dx:ASPxButton ID="ASPxButton5" runat="server" Text="Anullo" PostBackUrl="~/Makro.aspx"
                                                    Width="100%" CausesValidation="False">
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) {	                    
	                   var hf = document.getElementById('HiddenField1');
                        var listeFushash = hf.value.split(';');
	                    if (e.tab.index == 1 )
                         {      
                            
                            if (listeFushash[0]!='' && listeFushash[0]!=undefined && listeFushash[0]!='undefined')                                             
                            {
                               
                                 txtKodi2.SetText (listeFushash[0]);
                                 
                            }
                             if (listeFushash[1]!='' && listeFushash[1]!=undefined && listeFushash[1]!='undefined')                                             
                            {
                           
                              txtPershkr2.SetText (listeFushash[1]);
                              
                            }
                            }
                    }" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfLloji" runat="server" />
                <asp:HiddenField ID="hfAutorizime" runat="server" />
                <asp:HiddenField ID="hfProdukti" runat="server" />
                <asp:HiddenField ID="hfPershkrimi" runat="server" />
                <asp:HiddenField ID="hfFunksioni" runat="server" />
                <asp:HiddenField ID="hfVlera" runat="server" />
                <asp:HiddenField ID="hfRenditja" runat="server" />
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="">
                </dx:ASPxLabel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Zgjidh Makron"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            <iframe id="container" name="container" frameborder="0" runat="server"></iframe>
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
