<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GjeneroFaturePermbledhese.aspx.cs" Inherits="PlatinumWeb.GjeneroFaturePermbledhese" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


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
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link href="DX.ashx?cssfile=~/AlphaWeb.css" rel="stylesheet" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/GjeneroFaturePermbledhese.aspx-IMB.4.2.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="86400000">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){try{ window.parent.SessionTimeout.sendKeepAlive();} catch(e){}}" />--%>
    </dx:ASPxGlobalEvents>
    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled">
    </dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hfArkiva" runat="server" ClientInstanceName="hfArkiva">
    </dx:ASPxHiddenField>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hfKonffillestar" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            <asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                            ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                            OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
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
                        <div id="dvMenu" style="display: none">
                            <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                        BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                        <ClientSideEvents Init="Init_MenuInfo" />
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
    <table class="renditKontrolle">
        <tbody>
            <tr>
                <td class="renditKontrolleCaption">
                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                        runat="server" ClientIDMode="AutoID" Text="Modeli:">
                    </dx:ASPxLabel>
                </td>
                <td class="renditKontrolleCellMeWidth33">
                    <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                        ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" OnSelectedIndexChanged="mbushKonfigurimAmbjenti" >
                        <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                        <LoadingPanelImage>
                        </LoadingPanelImage>
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
                    <dx:ASPxLabel ID="lblKonfigurimi" runat="server" Text="" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi">
                    </dx:ASPxLabel>
                </td>
                <td class="renditKontrolleCellMeWidth33">
                </td>
            </tr>
        </tbody>
    </table>
    <asp:UpdatePanel ID="pnlKryesor" runat="server">
        <ContentTemplate>
            <div style="visibility: hidden">
                <dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green"
                    ClientVisible="false">
                </dx:ASPxLabel>
            </div>
            <asp:HiddenField ID="hfVeprimi" runat="server" />     <asp:HiddenField ID="status1" runat="server" />
            <br />
            <table style="width:100%">
                <tr>
                    <td  style="width:5%">
                        <dx:ASPxLabel ID="lblPeriudha" runat="server" Text="Periudha">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdStyle9" style="width:3%">
                        <dx:ASPxLabel ID="lblNgaDok" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdStyle7" style="width:5%">
                        <dx:ASPxDateEdit ID="txtNgaDok" runat="server" TabIndex="10"
                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom" UseMaskBehavior="true"
                            EditFormatString="dd/MM/yyyy" Date="1900-01-01" ValidationSettings-CausesValidation="True"
                            ClientInstanceName="txtNgaDok" CssPostfix="Glass" Height="16px">
                            <ClientSideEvents DateChanged="Date_Changed" />
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
                    <td  style="width:2%"></td>
                    <td class="tdStyle9"  style="width:3%">
                        <dx:ASPxLabel ID="lblDeriDok" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6">
                        </dx:ASPxLabel>
                    </td>
                    <td class="tdStyle7"  style="width:4%">
                        <dx:ASPxDateEdit ID="txtDeriDok" runat="server" TabIndex="11" UseMaskBehavior="true"
                            AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                            EditFormatString="dd/MM/yyyy" Date="9999-12-30" ClientInstanceName="txtDeriDok"
                            CssPostfix="Glass" Height="16px">
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
                    <td  style="width:78%"></td>
                </tr>
            </table>
            <dx:ASPxGridView ID="grid_RegDok" ClientInstanceName="grid_RegDok" runat="server" 
                Width="100%" OnDataBound="grid_RegDok_DataBound" OnAfterPerformCallback="grid_RegDok_AfterPerformCallback"
                OnCustomCallback="grid_RegDok_CustomCallback" OnCustomJSProperties="grid_RegDok_CustomJSProperties"
                OnProcessColumnAutoFilter="grid_RegDok_ProcessColumnAutoFilter" OnHeaderFilterFillItems="grid_RegDok_HeaderFilterFillItems" SettingsPager-PageSize="20">
         
                <Styles>
                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                    </Header>
                </Styles>
                <SettingsPager PageSize="15">
                </SettingsPager>
                <ClientSideEvents  BeginCallback="function(s, e) {
	BeginCallback(s,e);
}" />
                <StylesEditors>
                    <ProgressBar Height="25px">
                    </ProgressBar>
                </StylesEditors>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="grid_RegDok"
                ExportedRowType="Selected" />
            <iframe id="Container" runat="server" frameborder="0" name="Container" height="0"
                width="0"></iframe>
             <iframe id="Container1" runat="server" frameborder="0" name="Container1" height="0"
                width="0"></iframe>
        </ContentTemplate>
    </asp:UpdatePanel>
        <div>
        <asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                    AutoUpdatePosition="True" Font-Bold="False">
                    <ClientSideEvents CloseUp="function(s,
    e) { 
    
   
     }" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
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
