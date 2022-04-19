<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaporteGrida.aspx.cs" Inherits="PlatinumWeb.RaporteGrida" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RaporteGrida.aspx-IMB.4.3.js&v76"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){try{ window.parent.    SessionTimeout.sendKeepAlive();} catch(e){}}" />--%>
        </dx:ASPxGlobalEvents>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hfKonffillestar" runat="server" />
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                <table width="100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }"
                                    Init="function(s) {s.SetClientVisible(true);}" />
                                <ItemImage Height="32px" Width="32px">
                                </ItemImage>
                                <SubMenuItemImage Height="16px" Width="16px">
                                </SubMenuItemImage>
                                <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                    <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                </ItemStyle>
                                <SubMenuItemStyle Width="32px">
                                </SubMenuItemStyle>
                            </dx:ASPxMenu>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="dvMenu" style="display: none">
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </td>
                    </tr>
                </table>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="pnlKryesor" runat="server">
            <ContentTemplate>
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green">
                </dx:ASPxLabel>
                <br />

                <dx:ASPxGridView ID="gvRaporti" ClientInstanceName="gvRaporti" runat="server"
                    Width="100%" OnDataBound="gvRaporti_DataBound" OnAfterPerformCallback="gvRaporti_AfterPerformCallback"
                    OnCustomCallback="gvRaporti_CustomCallback" OnCustomJSProperties="gvRaporti_CustomJSProperties"
                    OnProcessColumnAutoFilter="gvRaporti_ProcessColumnAutoFilter" OnHeaderFilterFillItems="gvRaporti_HeaderFilterFillItems">
                    <Templates>
                        <TitlePanel>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton2" runat="server" ToolTip="Zgjidh kolonat" AutoPostBack="false"
                                            ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                            <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,gvRaporti)}"
                                                Init="myFaqeCelje.InitTeDrejtaKonf" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <asp:UpdatePanel ID="pnlruaj" runat="server">
                                            <ContentTemplate>
                                                <dx:ASPxButton ID="ASPxButton3" runat="server" ToolTip="Ruaj kolonat" AutoPostBack="true"
                                                    ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                    OnClick="RuajKolona_Click">
                                                    <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                </dx:ASPxButton>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                            AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) { gvRaporti.SelectAllRowsOnPage(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                            AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) { gvRaporti.SelectRows(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                            AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                            <ClientSideEvents Click="function(s, e) { gvRaporti.UnselectRows(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnXlsxExport" runat="server" ToolTip="Export to Xlsx" Image-Height="16px" Image-Url="images/xlsx24.png"
                                            Font-Size="8">
                                            <ClientSideEvents Click="function(s, e) { btnXlsxExportHidden.DoClick(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="btnPdfExport" runat="server" ToolTip="Export to Pdf" Image-Height="16px" Image-Url="images/pdf_icon.png"
                                            Font-Size="8">
                                            <ClientSideEvents Click="function(s, e) { btnPdfExportHidden.DoClick(); }" />
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                            </table>
                            <table id="filtra" style="display: none">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblPeriudha2" runat="server" Text="Data e faturimit" ClientInstanceName="lblPeriudha2">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="radDtDok11" ClientInstanceName="radDtDok11" Font-Size="12px"
                                            Font-Bold="true" ForeColor="#0072c6" runat="server" RepeatColumns="4" CssClass="Glass"
                                            CssPostfix="Glass" Height="16px" EnableClientSideAPI="true" Border-BorderStyle="None">
                                            <ClientSideEvents ValueChanged="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}"
                                                Init="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}" SelectedIndexChanged="function (s,e){ gvRaporti.PerformCallback('filtro');}" />
                                            <Items>
                                                <dx:ListEditItem Text="Aktuale" Value="Aktuale" Selected="true" />
                                                <dx:ListEditItem Text="Periudha" Value="Periudha" />
                                                <dx:ListEditItem Text="Viti Ushtrimor" Value="VitiUshtrimor" />
                                                <dx:ListEditItem Text="Gjithe Vitet" Value="GjitheVitet" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>

                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblNgaDok" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtNgaDok" ClientEnabled="false" runat="server" TabIndex="10"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2015-01-01" ValidationSettings-CausesValidation="True"
                                            ClientInstanceName="txtNgaDok" CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents DateChanged="function (s,e){ngaDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblDeriDok" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtDeriDok" ClientEnabled="false" runat="server" TabIndex="11"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2015-1-31" ClientInstanceName="txtDeriDok"
                                            CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents DateChanged="function (s,e){deriDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                            <table id="filtra1" style="display: none">
                                <tr>
                                    <td>
                                        <dx:ASPxLabel ID="lblPeriudha1" runat="server" Text="Data e ekzekutimit">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <dx:ASPxRadioButtonList ID="radDtDok5" ClientInstanceName="radDtDok5" Font-Size="12px"
                                            Font-Bold="true" ForeColor="#0072c6" runat="server" RepeatColumns="4" CssClass="Glass"
                                            CssPostfix="Glass" Height="16px" EnableClientSideAPI="true" Border-BorderStyle="None">
                                            <ClientSideEvents ValueChanged="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok1,txtDeriDok1);}"
                                                Init="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok1,txtDeriDok1);}" SelectedIndexChanged="function (s,e){ gvRaporti.PerformCallback('filtro');}" />
                                            <Items>
                                                <dx:ListEditItem Text="Aktuale" Value="Aktuale" Selected="true" />
                                                <dx:ListEditItem Text="Periudha" Value="Periudha" />
                                                <dx:ListEditItem Text="Viti Ushtrimor" Value="VitiUshtrimor" />
                                                <dx:ListEditItem Text="Gjithe Vitet" Value="GjitheVitet" />
                                            </Items>
                                        </dx:ASPxRadioButtonList>
                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblNgaDok1" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtNgaDok1" ClientEnabled="false" runat="server" TabIndex="10"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2015-01-01" ValidationSettings-CausesValidation="True"
                                            ClientInstanceName="txtNgaDok1" CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents DateChanged="function (s,e){ngaDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                    <td class="tdStyle9">
                                        <dx:ASPxLabel ID="lblDeriDok1" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td class="tdStyle7">
                                        <dx:ASPxDateEdit ID="txtDeriDok1" ClientEnabled="false" runat="server" TabIndex="11"
                                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                            EditFormatString="dd/MM/yyyy" Date="2015-01-31" ClientInstanceName="txtDeriDok1"
                                            CssPostfix="Glass" Height="16px">
                                            <ClientSideEvents DateChanged="function (s,e){deriDokDateChanged(s,e);}" />
                                            <CalendarProperties ShowClearButton="False" ShowTodayButton="False">
                                                <HeaderStyle Spacing="1px" />
                                                <FooterStyle Spacing="4px" />
                                            </CalendarProperties>
                                            <ButtonStyle Width="13px">
                                            </ButtonStyle>
                                            <ValidationSettings CausesValidation="True">
                                                <ErrorImage Height="14px" Width="14px" />
                                                <ErrorFrameStyle ImageSpacing="4px">
                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                </ErrorFrameStyle>
                                            </ValidationSettings>
                                        </dx:ASPxDateEdit>
                                    </td>
                                </tr>
                            </table>
                        </TitlePanel>
                    </Templates>
                    <Styles>
                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                        </Header>
                    </Styles>
                    <SettingsPager PageSize="15">
                    </SettingsPager>
                    <ClientSideEvents
                        BeginCallback="function(s, e) { BeginCallback(s,e); }"
                        EndCallback="function(s, e) { endCallback(s,e); }"
                        ColumnMoving="function(s, e) { AllowMoving(s,e); }" />
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvRaporti"
                    ExportedRowType="Selected" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxButton ID="btnPdfExportHidden" ClientVisible="False" ClientInstanceName="btnPdfExportHidden"
            runat="server" ToolTip="Export to Pdf" Text="Export to Pdf" Font-Size="8pt" UseSubmitBehavior="False"
            OnClick="btnPdfExport_Click">
            <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
        </dx:ASPxButton>

        <dx:ASPxButton ID="btnXlsxExportHidden" ClientVisible="False" ClientInstanceName="btnXlsxExportHidden"
            runat="server" ToolTip="Export to Xlsx" Text="Export to Xlsx" Font-Size="8" UseSubmitBehavior="false"
            OnClick="btnXlsxExport_Click">
            <ClientSideEvents Click="function(s, e) {
              clickExport(e) 
}" />
        </dx:ASPxButton>

    </form>
</body>
</html>
