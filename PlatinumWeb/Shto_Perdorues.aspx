<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Shto_Perdorues.aspx.cs"
    Inherits="PlatinumWeb.Shto_Perdorues" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
  
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myBuxhet-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/aspx.js/Shto_Perdorues.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
    <style>
        #ASPxPageControl1_txtShenime{
            visibility: hidden !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            </asp:ScriptManager>
             
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                 <ClientSideEvents EndCallback="function(s,e){ try{window.parent.SessionTimeout.sendKeepAlive();}catch(e){
                    //SessionTimeout.sendKeepAlive();
                    }}" />
            </dx:ASPxGlobalEvents>
            <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                    OnItemClick="ASPxMenu1_ItemClick">
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
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                     
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpConfirm" runat="server" AllowDragging="True" ClientInstanceName="popUpConfirm"
                    CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Konfirmo"
                    Modal="true" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    ShowHeader="true" Width="300px" Enabled="True" >
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel3" runat="server" Width="200px">
                                <PanelCollection>
                                    <dx:PanelContent>
                                        <dx:ASPxLabel ID="lblMsgConfirm" runat="server" ClientInstanceName="lblMsgConfirm" Text="Jeni i sigurt per ndryshimin e rolit?">
                                        </dx:ASPxLabel>
                                        <br />
                                        <br />
                                        <div style="text-align: right;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnKonfirmo" runat="server" Text="Ok" ClientInstanceName ="btnKonfirmo" ValidationGroup="entries" OnClick="btnKonfirmo_Click">
                                                            <ClientSideEvents Click="function(s, e) { popUpConfirm.Hide(); Utils.shfaqLoadingGif(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxButton ID="btnCancel" runat="server" Text="Anullo" AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s, e) { popUpConfirm.Hide(); }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </dx:PanelContent>
                                </PanelCollection>
                            </dx:ASPxPanel >
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientInstanceName="popFshi"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                        Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
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
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); Utils.shfaqLoadingGif(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvPerdorues" style="display: none">
                <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                      TabSpacing="3px" Width="100%" ActiveTabIndex="4" Height="520px">
                    <ContentStyle>
                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dx:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <table class="renditKontrolle">
                                                <tr>
                                                    <td class="renditKontrolleCaption">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                            runat="server" Text="Modeli:" Style="font-size: large">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33">
                                                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                                                            ShowShadow="False" Width="100%" Height="24px" Style="font-size: medium" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="renditKontrolleLabelMeWidth33">
                                                        <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi"
                                                            ClientInstanceName="lblKonfigurimi">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33"></td>
                                                </tr>
                                            </table>
                                            <dx:ASPxGridView ID="grid_ListPerdoruesit" ClientInstanceName="grid_ListPerdoruesit"
                                                runat="server" Width="100%" OnDataBound="grid_ListPerdoruesit_DataBound" OnAfterPerformCallback="grid_ListPerdoruesit_AfterPerformCallback"
                                                OnHeaderFilterFillItems="grid_ListPerdoruesit_HeaderFilterFillItems" OnProcessColumnAutoFilter="grid_ListPerdoruesit_ProcessColumnAutoFilter"
                                                OnCustomCallback="grid_ListPerdoruesit_CustomCallback" OnCustomJSProperties="grid_ListPerdoruesit_CustomJSProperties"
                                                OnAutoFilterCellEditorInitialize="grid_ListPerdoruesit_AutoFilterCellEditorInitialize">
                                                <Styles>
                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                    </Header>
                                                </Styles>
                                               
                                                <ClientSideEvents RowDblClick="function(s, e) { OnGridDoubleClick(e.visibleIndex); kaloTab=true; }"
                                                    FocusedRowChanged="function(s, e) { mbush=true; }"
                                                    BeginCallback="function(s, e) { BeginCallback(s,e); }" EndCallback="function (s,e){PageControl.AdjustSize();}" />
                                                <SettingsPager PageSize="15">
                                                </SettingsPager>
                                                <StylesEditors>
                                                    <ProgressBar Height="25px">
                                                    </ProgressBar>
                                                </StylesEditors>
                                            </dx:ASPxGridView>
                                            <br />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Perdorues" Text="Perdorues">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <table id="tblPerdoruesi" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="emri_TextBox" ID="lblEmri" runat="server"
                                        Text="Emri:" ClientInstanceName="lblEmri">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="emri_TextBox" runat="server" TabIndex="1" ClientInstanceName="emri_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	txtEmri2.SetText(emri_TextBox.GetText());
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Emri duhet te permbaje vetem shkronja dhe numra!" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="mbiemri_TextBox" ID="lblMbiemri"
                                        runat="server" Text="Mbiemri:" ClientInstanceName="lblMbiemri">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="mbiemri_TextBox" runat="server" TabIndex="2" ClientInstanceName="mbiemri_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	txtMbiemri2.SetText(mbiemri_TextBox.GetText());
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Mbiemri duhet te permbaje vetem shkronja dhe numra!" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodiDyqanit" ID="lblKodiDyqanit" runat="server"
                                        Text="Autorizimi:" ClientInstanceName="lblKodiDyqanit">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtKodiDyqanit" runat="server" ClientInstanceName="txtKodiDyqanit"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                      <%--<div id="dvlbltxtShopName">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ShopName" ID="lblShopName" runat="server"
                                        Text="Emri i dyqanit:" ClientInstanceName="lblShopName">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtShopName" runat="server" ClientInstanceName="txtShopName"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                             <%--<div id="dvlbltxtDealer">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="Dealer" ID="lblDealer" runat="server"
                                        Text="Emri i Dealer:" ClientInstanceName="lblDealer">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtDealer" runat="server" ClientInstanceName="txtDealer"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    <%--<div id="dvlblUserCRM">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblUserCRM" ID="lblUserCRM" runat="server"
                                        Text="Useri CRM:" ClientInstanceName="lblUserCRM">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtUserCRM" runat="server" ClientInstanceName="txtUserCRM"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>


                                      <%--<div id="dvlblUserCRM">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblUserEtopUP" ID="lblUserEtopUP" runat="server"
                                        Text="Useri i ETopUp:" ClientInstanceName="lblUserEtopUP">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtUserEtopUP" runat="server" ClientInstanceName="txtUserEtopUP"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    
                                      <%--<div id="dvlbllblIDETopUp">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblIDETopUp" ID="lblIDETopUp" runat="server"
                                        Text="ID e ETopUp:" ClientInstanceName="lblIDETopUp">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtIDETopUp" runat="server" ClientInstanceName="txtIDETopUp"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    
                                      <%--<div id="dvlblTypeDeviceSalesRep">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblTypeDeviceSalesRep" ID="lblTypeDeviceSalesRep" runat="server"
                                        Text="Tipi I Aparatit qe perdor:" ClientInstanceName="lblTypeDeviceSalesRep">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtTypeDeviceSalesRep" runat="server" ClientInstanceName="txtTypeDeviceSalesRep"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    
                                      <%--<div id="dvlblSalesRepMobile">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblSalesRepMobile" ID="lblSalesRepMobile" runat="server"
                                        Text="Numri I telefonit te perfaqesuesit te Shitjes':" ClientInstanceName="lblSalesRepMobile">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtSalesRepMobile" runat="server" ClientInstanceName="txtSalesRepMobile"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                    
                                      <%--<div id="dvlblSalesRepMPesaMSISDN">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="lblSalesRepMPesaMSISDN" ID="lblSalesRepMPesaMSISDN" runat="server"
                                        Text="Revoke Eforms:" ClientInstanceName="lblSalesRepMPesaMSISDN">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtSalesRepMPesaMSISDN" runat="server" ClientInstanceName="txtSalesRepMPesaMSISDN"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                           
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>

                                      <%--<div id="dteSalesRepTrainingDate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteSalesRepTrainingDate" ID="lblSalesRepTrainingDate"
                                        runat="server" Text="Date fillimi Trajnimi:" ClientInstanceName="lblSalesRepTrainingDate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dteSalesRepTrainingDate">--%>
                                    <dx:ASPxDateEdit ID="dteSalesRepTrainingDate" runat="server" ClientInstanceName="dteSalesRepTrainingDate"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>

                                    <%--<div id="dteSalesRepStartDateShop">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteSalesRepStartDateShop" ID="lblSalesRepStartDateShop"
                                        runat="server" Text="Date Fillimi ne dyqan:" ClientInstanceName="lblSalesRepStartDateShop">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dteSalesRepTrainingDate">--%>
                                    <dx:ASPxDateEdit ID="dteSalesRepStartDateShop" runat="server" ClientInstanceName="dteSalesRepStartDateShop"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>

                                    <%--<div id="dteSalesRepStartDateShop">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteSalesRepStartDate" ID="lblSalesRepStartDate"
                                        runat="server" Text="Date Fillimi ne Vodafone:" ClientInstanceName="lblSalesRepStartDate">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dteSalesRepTrainingDate">--%>
                                    <dx:ASPxDateEdit ID="dteSalesRepStartDate" runat="server" ClientInstanceName="dteSalesRepStartDate"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>


                                     <%--<div id="dteSalesRepStartMaternityLeave">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteSalesRepStartMaternityLeave" ID="lblSalesRepStartMaternityLeave"
                                        runat="server" Text="Date Fillimi te lejes se lindjes:" ClientInstanceName="lblSalesRepStartMaternityLeave">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dteSalesRepTrainingDate">--%>
                                    <dx:ASPxDateEdit ID="dteSalesRepStartMaternityLeave" runat="server" ClientInstanceName="dteSalesRepStartMaternityLeave"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>


                                       <%--<div id="dteLeaveDateVodafoneVod">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteSalesRepStartMaternityLeave" ID="lblLeaveDateVodafoneVod"
                                        runat="server" Text="Date largimi nga VF:" ClientInstanceName="lblLeaveDateVodafoneVod">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteLeaveDateVodafoneVod" runat="server" ClientInstanceName="dteLeaveDateVodafoneVod"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>


                                    
                                       <%--<div id="dteLeaveDateShop">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteLeaveDateShop" ID="lblLeaveDateShop"
                                        runat="server" Text="Date largimi nga dyqani:" ClientInstanceName="lblLeaveDateShop">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteLeaveDateShop" runat="server" ClientInstanceName="dteLeaveDateShop"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>

                                     <%--<div id="dteMaternityLeaveEndDate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteMaternityLeaveEndDate" ID="lblMaternityLeaveEndDate"
                                        runat="server" Text="Date mbarimi te lejes se lindjes:" ClientInstanceName="lblMaternityLeaveEndDate">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteMaternityLeaveEndDate" runat="server" ClientInstanceName="dteMaternityLeaveEndDate"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>

                                     <%--<div id="dteTrainingEndDate">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dteTrainingEndDate" ID="lblTrainingEndDate"
                                        runat="server" Text="Date mbarimi Trajnimi:" ClientInstanceName="lblTrainingEndDate">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteTrainingEndDate" runat="server" ClientInstanceName="dteTrainingEndDate"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>


                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGender" ID="lblGender"
                                        runat="server" Text="Gjinia:" ClientInstanceName="lblGender">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="cmbGender" ClientInstanceName="cmbGender"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False">
                                        <ClientSideEvents TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                 

                                       <%--<div id="txtComRetSal">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtComRetSal" ID="lblComRetSal" runat="server"
                                        Text="Komente nga Retail Sales:" ClientInstanceName="lblComRetSal">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                     <dx:ASPxMemo ID="txtComRetSal" runat="server" Width="100%" ClientInstanceName="txtComRetSal"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                   
                                    <%--</div>--%> 

                                        <%--<div id="txtAccountExecutive">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtAccountExecutive" ID="lblAccountExecutive" runat="server"
                                        Text="Opened Date:" ClientInstanceName="lblAccountExecutive">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtAccountExecutive" runat="server" ClientInstanceName="txtAccountExecutive"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%> 

                                      <%--<div id="txtIDNumber">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtIDNumber" ID="lblIDNumber" runat="server"
                                        Text="Numer personal:" ClientInstanceName="lblIDNumber">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemri_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtIDNumber" runat="server" ClientInstanceName="txtIDNumber"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                 }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                             <RegularExpression ValidationExpression="^[A-Za-z][A-Za-z0-9]*(?:_[A-Za-z0-9]+)*$" ErrorText="Te jete 10 karaktere, te filloj dhe te mbaroje me shkronje!" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%> 

                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbIsInsured" ID="lblIsInsured"
                                        runat="server" Text="I Siguruar (Po/Jo):" ClientInstanceName="lblIsInsured">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="cmbIsInsured" ClientInstanceName="cmbIsInsured"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" >
                                        <ClientSideEvents TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>


                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtComROS" ID="lblComROS"
                                        runat="server" Text="Komente nga Retail Operations Specialist:" ClientInstanceName="lblComROS">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxMemo ID="txtComROS" runat="server" Width="100%" ClientInstanceName="txtComROS"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>                                   
                                  
                                    <%--<div id="txtRegSup">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtRegSup" ID="lblRegSup"
                                        runat="server" Text="Regional Supervisor:" ClientInstanceName="lblRegSup">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtRegSup" runat="server" ClientInstanceName="txtRegSup"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                     <%--<div id="txtRetailSAE">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtRetailSAE" ID="lblRetailSAE"
                                        runat="server" Text="Retail Sales Account Executive:" ClientInstanceName="lblRetailSAE">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtRetailSAE" runat="server" ClientInstanceName="txtRetailSAE"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                       <%--<div id="txtRetailSAE">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtRetailSAM" ID="lblRetailSAM"
                                        runat="server" Text="Retail Sales Area Manager:" ClientInstanceName="lblRetailSAM">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtRetailSAM" runat="server" ClientInstanceName="txtRetailSAM"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>


                                              <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSiteCode" ID="lblSiteCode"
                                        runat="server" Text="Site Code:" ClientInstanceName="lblSiteCode">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtSiteCode" runat="server" ClientInstanceName="txtSiteCode"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="dteBirthday" ID="lblBirthday"
                                        runat="server" Text="Datelindja:" ClientInstanceName="lblBirthday">
                                    </dx:ASPxLabel>
                                    <dx:ASPxDateEdit ID="dteBirthday" runat="server" ClientInstanceName="dteBirthday"
                                        ShowShadow="False" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxDateEdit>


                                    
                                              <dx:ASPxLabel Wrap="False" AssociatedControlID="txtDistrict" ID="lblDistrict"
                                        runat="server" Text="District:" ClientInstanceName="lblDistrict">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtDistrict" runat="server" ClientInstanceName="txtDistrict"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>


                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShopMainCode" ID="lblShopMainCode"
                                        runat="server" Text="Shop Main Code:" ClientInstanceName="lblShopMainCode">
                                    </dx:ASPxLabel>
                                 
                                    <dx:ASPxTextBox ID="txtShopMainCode" runat="server" ClientInstanceName="txtShopMainCode"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>


                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLongitude" ID="lblLongitude"
                                        runat="server" Text="Longitude:" ClientInstanceName="lblLongitude">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtLongitude" runat="server" ClientInstanceName="txtLongitude"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    
                                     <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLatitude" ID="lblLatitude"
                                        runat="server" Text="Latitude:" ClientInstanceName="lblLatitude">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="txtComROS">--%>
                                    <dx:ASPxTextBox ID="txtLatitude" runat="server" ClientInstanceName="txtLatitude"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {	
	                                                  }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>

                                    
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStatus" ID="lblStatus"
                                        runat="server" Text="Status:" ClientInstanceName="lblStatus">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="cmbStatus" ClientInstanceName="cmbStatus" 
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" >
                                        <ClientSideEvents ButtonClick="myButtonClickLupaShopsStatus" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                      
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbUniform" ID="lblUniform"
                                        runat="server" Text="Uniform:" ClientInstanceName="lblUniform">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="cmbUniform" ClientInstanceName="cmbUniform"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" >
                                         <ClientSideEvents ButtonClick="myButtonClickLupaShopsUniform" TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                         
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLeaveReason" ID="lblLeaveReason"
                                        runat="server" Text="Leave Reason:" ClientInstanceName="lblLeaveReason">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="cmbLeaveReason" ClientInstanceName="cmbLeaveReason"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False">
                                       <ClientSideEvents ButtonClick="myButtonClickLupaShopsHierarkiLeaveReason" TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>


                                     <%--<div id="txtRegSup">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime"
                                        runat="server" Text="Shenime:" ClientInstanceName="lblShenime" ClientEnabled="false">
                                    </dx:ASPxLabel>

                                     <dx:ASPxMemo ID="txtShenime" runat="server" Width="100%" ClientInstanceName="txtShenime"
                                        Rows="3" ClientEnabled="false">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>

                                          
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbStatusAprovimi" ID="lblStatusAprovimi"
                                        runat="server" Text="Status Aprovimi:" ClientInstanceName="lblStatusAprovimi">
                                    </dx:ASPxLabel>
                                   
                                    <dx:ASPxComboBox ID="cmbStatusAprovimi" ClientInstanceName="cmbStatusAprovimi"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" >
                                       <ClientSideEvents ButtonClick="myButtonClickLupaShopsHierarkiLeaveReason" TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>

                                      <dx:ASPxLabel Wrap="False" AssociatedControlID="cbNjoftimEmailAprovim" ID="lblNjoftimEmailAprovim"
                                        runat="server" Text="Njoftim me email per aprovim:" ClientInstanceName="lblNjoftimEmailAprovim">
                                    </dx:ASPxLabel>
                                      <dx:ASPxCheckBox ID="cbNjoftimEmailAprovim" runat="server" ClientInstanceName="cbNjoftimEmailAprovim">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="username_TextBox" ID="lblUsername"
                                        runat="server" Text="Usernameii:" ClientInstanceName="lblUsername">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="username_TextBox" runat="server" TabIndex="3" ClientInstanceName="username_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Username lejohet të ketë vetëm shkronja, numra, vizë poshtë, pikë(.), dhe simboli et(@) " ValidationExpression="^([a-zA-Z])[a-zA-Z._@]*[\w-]*[\S]$|^([a-zA-Z])[0-9._@]*[\S]$|^[a-zA-Z]*[\S]$" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPasswordieksistues" ID="lblPasswordieksistues"
                                        runat="server" Text="Passwordi ekzistues:" ClientInstanceName="lblPasswordieksistues">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="txtPasswordieksistues" runat="server" TabIndex="4" Password="True" Width="100%"
                                        ClientInstanceName="txtPasswordieksistues">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="password_TextBox" ID="lblPassword"
                                        runat="server" Text="Password:" ClientInstanceName="lblPassword">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="password_TextBox" runat="server" TabIndex="5" ClientInstanceName="password_TextBox"
                                        Password="True" Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { kontrolloPassword(s, e); }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="konfirmo_Textbox" ID="lblKonfirmoPassword"
                                        runat="server" Text="Konfirmo password:" ClientInstanceName="lblKonfirmoPassword">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="konfirmo_Textbox" runat="server" TabIndex="6" ClientInstanceName="konfirmo_Textbox"
                                        Password="True" Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="aktiv_CheckBox" ID="lblPerdoruesAktiv"
                                        runat="server" Text="Perdorues aktiv:" ClientInstanceName="lblPerdoruesAktiv">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="aktiv_CheckBox" runat="server" TabIndex="11" ClientInstanceName="aktiv_CheckBox">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kontrolloPassword_CheckBox" ID="lblKontrolloPassword"
                                        runat="server" Text="Kontrollo password:" ClientInstanceName="lblKontrolloPassword">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="kontrolloPassword_CheckBox" runat="server" TabIndex="10" ClientInstanceName="kontrolloPassword_CheckBox">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="shfaqPerdoruesMenu_CheckBox" ID="lblShfaqPerdoruesMenu"
                                        runat="server" Text="Shfaq perdoruesin ne  menu:" ClientInstanceName="lblShfaqPerdoruesMenu">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="shfaqPerdoruesMenu_CheckBox" runat="server" TabIndex="10" ClientInstanceName="shfaqPerdoruesMenu_CheckBox">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="false" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPassPerkohshem" ID="lblPassPerkohshem"
                                        runat="server" Text="Fjalëkalim i përkohshëm:" ClientInstanceName="lblPassPerkohshem">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbPassPerkohshem" runat="server" TabIndex="8" ClientInstanceName="cbPassPerkohshem">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <ClientSideEvents CheckedChanged="function(s,e){ disablePassEkzistues(s, e); }" />
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKycur" ID="lblPerdoruesIKycur"
                                        runat="server" Text="I kyçur:" ClientInstanceName="lblPerdoruesIKycur">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbKycur" runat="server" TabIndex="9" ClientInstanceName="cbKycur">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGjuha" ID="lblGjuha"
                                        runat="server" Text="Gjuha:" ClientInstanceName="lblGjuha">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbGjuha" TabIndex="7" ClientInstanceName="cmbGjuha"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dtPrintimi_CheckBox" ID="lblDtPrintimi"
                                        runat="server" Text="Shfaq date dhe ore printimi" ClientInstanceName="lblDtPrintimi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="dtPrintimi_CheckBox" runat="server" TabIndex="10" ClientInstanceName="dtPrintimi_CheckBox">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbKycur" ID="lblPerdoruesIKycurMobile"
                                        runat="server" Text="I kyçur Mobile:" ClientInstanceName="lblPerdoruesIKycurMobile">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbKycurMobile" runat="server" TabIndex="9" ClientInstanceName="cbKycurMobile">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbShfaqNjoftime" ID="lblPerdoruesShfaqNjoftime"
                                        runat="server" Text="Shfaq njoftime:" ClientInstanceName="lblPerdoruesShfaqNjoftime">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbShfaqNjoftime" runat="server" TabIndex="11" ClientInstanceName="cbShfaqNjoftime">
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>

                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="btnResetOtpToken" ID="lblResetOtpToken"
                                        runat="server" Text="Reseto Tokenin OTP:" ClientInstanceName="lblResetOtpToken">
                                    </dx:ASPxLabel>
                                    <%--<input id="btnResetOtpToken" runat="server" type="button" onserverclick="PastroOTP_Button_Click" value="Reset Token"/>--%>
                                    <dx:ASPxButton ID="btnResetOtpToken" runat="server" AutoPostBack="False" Text="Reset Token" OnClick="PastroOTP_Button_Click"
                                                                                    ClientInstanceName="btnResetOtpToken" Width="100px">
                                                                                </dx:ASPxButton>
                                    <div class="OTP success">
                                        <asp:label ID="otpSuccess" ForeColor="Green" runat="server"></asp:label>
                                    </div>

                                    <dx:ASPxLabel Wrap="False" ID="lblKonfigKase" AssociatedControlID="cmbKonfigKase" runat="server"
                                        Text="Kasa:" ClientInstanceName="lblKonfigKase">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbKonfigKase" Width="100%" runat="server" ClientInstanceName="cmbKonfigKase"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents ButtonClick="function(s,e){}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidateOnLeave="false"
                                            ValidationGroup="entries1">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                            <RequiredField IsRequired="True"></RequiredField>
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Kontakt" Text="Kontakt">
                            <ContentCollection>
                                <dx:ContentControl runat="server">
                                    <table id="tblKontakti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="email_TextBox" ID="lblEmail" runat="server"
                                        Text="E-mail:" ClientInstanceName="lblEmail">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="email_TextBox" TabIndex="14" ClientInstanceName="email_TextBox" runat="server"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                            <RequiredField IsRequired="True" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="adresa_TextBox" ID="lblAdresa" runat="server"
                                        Text="Adresa:" ClientInstanceName="lblAdresa">
                                    </dx:ASPxLabel>
                                    <dx:ASPxMemo ID="adresa_TextBox" TabIndex="13" runat="server" Width="100%" ClientInstanceName="adresa_TextBox"
                                        Rows="3">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="qyteti_ASPxComboBox" ID="lblQyteti"
                                        runat="server" Text="Gjuha:" ClientInstanceName="lblQyteti">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="qyteti_ASPxComboBox" TabIndex="12" ClientInstanceName="qyteti_ASPxComboBox"
                                        runat="server" Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) {}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                            ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="tel_TextBox" ID="lblTel" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTel">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="tel_TextBox" runat="server" Width="100%" TabIndex="16" ClientInstanceName="tel_TextBox">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="^\s*\+?\s*([0-9 \(\)][\s-]*){9,}$" ErrorText="Formati nuk eshte i sakte!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="fax_TextBox" ID="lblFax" runat="server"
                                        Text="Fax:" ClientInstanceName="lblFax">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox ID="fax_TextBox" runat="server" TabIndex="15" Width="100%" ClientInstanceName="fax_TextBox">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Rolet" Text="Rolet">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl6" runat="server">
                                      <table style="width: 100%">
                                        <tr>
                                            <td>
                                            <table id="tblRolet"  class="CustomRenditKontrolleNje" style="grid-flow: columns;">
                                                <tbody>
                                                </tbody>
                                            </table>
                                            
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtEmri2" ID="lblEmri2" runat="server"
                                                            Text="Emri:" ClientInstanceName="lblEmri2">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxTextBox ID="txtEmri2" runat="server" TabIndex="1" ClientInstanceName="txtEmri2"
                                                            Width="100%">
                                                            <ClientSideEvents  TextChanged="function(s, e) {	
	                                                                        }" />
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                                ValidationGroup="entries" SetFocusOnError="True">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Emri duhet te permbaje vetem shkronja dhe numra!" />
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtMbiemri2" ID="lblMbiemri2"
                                                            runat="server" Text="Mbiemri:" ClientInstanceName="lblMbiemri2">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxTextBox ID="txtMbiemri2" runat="server" TabIndex="2" ClientInstanceName="txtMbiemri2"
                                                            Width="100%">
                                                            <ClientSideEvents TextChanged="function(s, e) {	
	                                                                        }" />
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                                ValidationGroup="entries" SetFocusOnError="True">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ValidationExpression="[a-z,A-Z,0-9]*" ErrorText="Mbiemri duhet te permbaje vetem shkronja dhe numra!" />
                                                                <RequiredField IsRequired="True" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAmbjenti" ID="lblAmbjenti"
                                                            runat="server" Text="Ambjenti i punes Web:" ClientInstanceName="lblAmbjenti">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxComboBox ID="cmbAmbjenti" ClientInstanceName="cmbAmbjenti"
                                                            runat="server" Width="100%" ValueType="System.String" ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents TextChanged="function(s, e) { ShowHideAmbjentMobile(); }"/>
                                                            <DropDownButton>
                                                                <Image> 
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                                ValidateOnLeave="false">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbAmbjentiMobile" ID="lblAmbjentiMobile"
                                                            runat="server" Text="Ambjenti i punes Mobile:" ClientInstanceName="lblAmbjentiMobile">
                                                        </dx:ASPxLabel>
                                                        <dx:ASPxComboBox ID="cmbAmbjentiMobile" ClientInstanceName="cmbAmbjentiMobile"
                                                            runat="server" Width="100%" ValueType="System.String" 
                                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents  TextChanged="function(s, e) {}" />
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" 
                                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1"
                                                                ValidateOnLeave="false">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                            </td>
                                    </tr>
                                    </table>
                                    <br />
                                    <br />
                                    
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxGridView ID="gvRolet" ClientInstanceName="gvRolet" runat="server" Width="100%"
                                                    OnDataBound="gvRolet_DataBound" OnAutoFilterCellEditorInitialize="gvRolet_AutoFilterCellEditorInitialize"
                                                    OnAfterPerformCallback="gvRolet_AfterPerformCallback" OnCustomCallback="gvRolet_CustomCallback">
                                                    <Styles>
                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                        </Header>
                                                    </Styles>
       <%--<ClientSideEvents  SelectionChanged=" function(s, e) { 
             gvRolet.GetSelectedFieldValues('AktivRoli;IdRoli',SucceedCallbackRoli);
}" />--%>
                                                     <ClientSideEvents BeginCallback="OnBeginCallback" SelectionChanged=" function(s, e) { 
             gvRolet.GetSelectedFieldValues('AktivRoli;IdRoli',SucceedCallbackRoli);
}" EndCallback="OnEndCallback"/>
                                                    
                                                    <SettingsPager PageSize="15">
                                                    </SettingsPager>
                                                    <StylesEditors>
                                                        <ProgressBar Height="25px">
                                                        </ProgressBar>
                                                    </StylesEditors>
                                                </dx:ASPxGridView>
                                            </td>
                                        </tr>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Autorizimet" Text="Autorizimet" ClientVisible="false">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl5" runat="server">
                                    <dx:ASPxGridView ID="ASPxGridView_Autorizimet" runat="server" ClientInstanceName="ASPxGridView_Autorizimet"
                                        Width="50%" OnCustomCallback="ASPxGridView_Autorizimet_CustomCallback">
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                    <br />
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="Active_TabChanged" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dx:ASPxPageControl>
            </div>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" Value="false" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                     <asp:HiddenField ID="hfIndexId" runat="server" />
                    <asp:HiddenField ID="hfRedirect" runat="server" />
                    <asp:HiddenField ID="hfNgaEmailKerkeseAprovimi" runat="server" />
                    <asp:HiddenField ID="hfKontrolletNrAutom" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <dx:ASPxHiddenField ID="hfVleraNrAutom" runat="server" ClientInstanceName="hfVleraNrAutom">
                    </dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfVleratFushaAutomatike" runat="server" ClientInstanceName="hfVleratFushaAutomatike">
                    </dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfNdryshuarPass" runat="server" ClientInstanceName="hfNdryshuarPass">
                    </dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfMinGjatesiPassword" runat="server" ClientInstanceName="hfMinGjatesiPassword">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
                        <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
