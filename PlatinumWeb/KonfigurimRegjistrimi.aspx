<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimRegjistrimi.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimRegjistrimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
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
        

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KonfigurimRegjistrimi.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
         <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
        </dx:ASPxHiddenField>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    </dx:ASPxGlobalEvents>   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                            ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" SeparatorWidth="1px"
                            ShowPopOutImages="True" Width="100%" OnItemClick="ASPxMenu1_ItemClick1">
                            <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                            <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                            <ClientSideEvents ItemClick="function(s, e) {
	                        menu_click(s,e);
                            }" Init="function(s) {s.SetClientVisible(true);}" />
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
                        <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                    BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                    <ClientSideEvents Init="function(s,e){myMesazh.InicializoTimer();
                                    }" />
                                    <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                    <ItemStyle HorizontalAlign="Left" />
                                    <SubMenuStyle GutterWidth="17px" />
                                </dx:ASPxMenu>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>            
            <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                PopupVerticalAlign="WindowCenter" Width="300px">
                <HeaderStyle>
                    <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                </HeaderStyle>
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                        <dxp:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                            <PanelCollection>
                                <dxp:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                    <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?">
                                    </dx:ASPxLabel>
                                    <br />
                                    <br />
                                    <div style="text-align: right;">
                                        <table>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                        OnClick="ButtonOk_Click2" Text="Ok">
                                                        <ClientSideEvents Click="function(s, e) {
	popFshi.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                    </dx:ASPxButton>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                        <ClientSideEvents Click="function(s, e) {
		popFshi.Hide();
}" />
                                                    </dx:ASPxButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </dxp:PanelContent>
                            </PanelCollection>
                        </dxp:ASPxPanel >
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl >
            <div style="visibility: hidden">
                <dx:ASPxButton ID="ASPxButton1" runat="server" ClientInstanceName="btn" Text="ASPxButton"
                    Height="0px">
                </dx:ASPxButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="pnlKryesor" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--<dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Green">
            </dx:ASPxLabel>--%>
            <dx:ASPxGridView ID="gvKategoriDok" ClientInstanceName="gvKategoriDok" runat="server"
                Width="100%" OnRowInserting="gvKategoriDok_RowInserting" OnDataBound="gvKategoriDok_DataBound"
                OnAfterPerformCallback="gvKategoriDok_AfterPerformCallback" OnRowValidating="gvKategoriDok_RowValidating"
                OnStartRowEditing="gvKategoriDok_StartRowEditing" OnHtmlRowCreated="gvKategoriDok_HtmlRowCreated"
                OnCellEditorInitialize="gvKategoriDok_CellEditorInitialize" OnHeaderFilterFillItems="gvKategoriDok_HeaderFilterFillItems"
                OnRowUpdating="gvKategoriDok_RowUpdating" OnAutoFilterCellEditorInitialize="gvKategoriDok_AutoFilterCellEditorInitialize"
                OnInitNewRow="gvKategoriDok_InitNewRow" OnProcessColumnAutoFilter="gvKategoriDok_ProcessColumnAutoFilter"
                OnCustomCallback="gvKategoriDok_CustomCallback">
                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <ClientSideEvents RowDblClick="Row_DblClick" BeginCallback="function(s, e) {BeginCallback(s,e);}" EndCallback="function(s, e) { EndCallbackGrida(s, e); }" />
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
                <Templates>
                    <EditForm>
                        <table style="width:100%">
                            <tr>
                                <td style="width: 15%; text-align: right">
                                    Kategoria:
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cmbKategoria" AutoPostBack="false" runat="server" Width="100%"
                                        ClientInstanceName="cmbKategoria">
                                        <ClientSideEvents SelectedIndexChanged="gvKategoriDokSelectedIndexChanged"
                 Init="gvKategoriDok_Init" />
                                    </dx:ASPxComboBox>
                                </td>
                                <td style="width: 15%; text-align: right">
                                    Kodi:
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%" ClientInstanceName="txtKodi">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 15%; text-align: right">
                                    Pershkrimi:
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtPershkrimi" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; text-align: right">
                                    Radha:
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="txtRadha" runat="server" Width="100%">
                                    </dx:ASPxTextBox>
                                </td>
                                <td style="width: 15%; text-align: right">
                                    Aktiv:
                                </td>
                                <td>
                                    <dx:ASPxCheckBox ID="chkAktiv" runat="server">
                                    </dx:ASPxCheckBox>
                                </td>
                                <td style="width: 15%; text-align: right">
                                    Konvertimi:
                                </td>
                                <td>
                                    <dx:ASPxDropDownEdit ClientInstanceName="checkComboBox" ID="ASPxDropDownEdit1" SkinID="CheckComboBox"
                                        Width="210px" runat="server" EnableAnimation="False">
                                        <DropDownWindowTemplate>
                                            <dx:ASPxListBox Width="100%" ID="listBox" ClientInstanceName="checkListBox" SelectionMode="CheckColumn"
                                                runat="server" SkinID="CheckComboBoxListBox">
                                                <Items>
                                                    <dx:ListEditItem Text="(Te gjitha)" />
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                            </dx:ASPxListBox>
                                            <table style="width: 100%">
                                                <tr>
                                                    <td align="right">
                                                        <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Mbyll">
                                                            <ClientSideEvents Click="function(s, e){ checkComboBox.HideDropDown(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </DropDownWindowTemplate>
                                        <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                    </dx:ASPxDropDownEdit>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 15%; text-align: right">
                                   Nr Serial Unik:
                                </td>
                                <td style="width: 15%">
                                   <dx:ASPxCheckBox ID="cbNrSerialUnik" runat="server">
                                    </dx:ASPxCheckBox>
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                                <td style="width: 15%">
                                    &nbsp;
                                </td>
                                <td style="width: 20%; text-align: right">
                                    <table>
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="btnRuaj" AutoPostBack="true" runat="server" Text="Ruaj" Width="100%"
                                                    OnClick="ruaj_Button_Click" Visible="false">
                                                    <ClientSideEvents Click="function(s, e){ InitRuaj(); e.processOnServer=true;}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxGridViewTemplateReplacement ID="upButton" runat="server" ReplacementType="EditFormUpdateButton">
                                                </dx:ASPxGridViewTemplateReplacement>
                                            </td>
                                            <td>
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
    <asp:HiddenField ID="hfRuaj" runat="server" />
       <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
        </dx:ASPxHiddenField>
    <asp:HiddenField ID="hfVeprimi" runat="server" />
    </form>
</body>
</html>
