<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="Shto_Ndermarrje.aspx.cs"
    ValidateRequest="false" Inherits="PlatinumWeb.Shto_Ndermarrje" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
     
    <script src="js/aspx.js/Shto_Ndermarrje.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_Ndermarrje.aspx-IMB.2.1.js&v76"
        type="text/javascript"></script>
    <style type="text/css">
        #mainContainer td.qelizeButoni {
            padding-top: 15px;
        }

        #mainContainer td.Koka {
            padding-right: 5px;
            padding-top: 4px;
            vertical-align: top;
        }

        #mainContainer td.permbajtja {
            padding-bottom: 20px;
        }

        #mainContainer td.ShfaqjeProve {
            border: solid 2px gray;
            width: 250px;
            height: 250px;
            text-align: center;
        }

        #mainContainer td.shenim {
            text-align: left;
            padding-top: 1px;
        }
        .dialog-pajisje{
            display: inline-block;
            position: absolute;
            background-color: white;
            width: 50%;
            border: 3px solid white;
            padding: 10px;
            z-index: 999;
            position: absolute;
            left: 0;
            right: 0;
            margin-left: auto;
            margin-right: auto;
            text-align: center;
            margin-top: 10%;
            border-radius: 5px;
            opacity: 1;
        }
        .bg-pajisje{
            width: 100%;
            height: 100%;
            opacity: 0.5;
            position: absolute;
            background-color:black;
            z-index: 999;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="width: 100%">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
            </asp:ScriptManager>
            <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
                <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
            </dx:ASPxGlobalEvents>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                    ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                    SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
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
                                                <ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
                                                <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                                <ItemStyle HorizontalAlign="Left" />
                                                <SubMenuStyle GutterWidth="17px" />
                                            </dx:ASPxMenu>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                     
                    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                        Font-Size="9pt" Modal="True" ImagePosition="Top">
                        <LoadingDivStyle Opacity="30">
                        </LoadingDivStyle>
                    </dx:ASPxLoadingPanel>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popFshi" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
                        EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" Width="300px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
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
                                                                    Utils.shfaqLoadingGif();
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
                                            <div style="visibility: hidden">
                                                <dx:ASPxButton ID="btnShfaqh" runat="server" CausesValidation="False" ClientInstanceName="btnShfaqImazh"
                                                    OnClick="btnShfaqImazh_Click" Text="Ok">
                                                </dx:ASPxButton>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popLlojLicence" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                        ClientInstanceName="popLlojLicence" CloseAction="CloseButton" CssPostfix="Glass"
                        EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                        Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="400px">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl14" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                            <table width="100%">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="lblLlojLicence" runat="server" ClientIDMode="AutoID" Text="Lloji i Licences:"
                                                            Wrap="False">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td>
                                                        <dx:ASPxComboBox ID="cmbLlojLicence" runat="server" ClientInstanceName="cmbLlojLicence">
                                                        </dx:ASPxComboBox>
                                                    </td>    </tr>
                                                <tr> <td >
                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLicenca" ID="lblLicenca1" ClientInstanceName="lblLicenca1"
                                                        runat="server" ClientIDMode="AutoID" Text="Licenca: ">
                                                    </dx:ASPxLabel>
                                                </td>
                                                 
                                                <td >
                                                    <dx:ASPxComboBox ID="cmbLicenca" runat="server" ClientInstanceName="cmbLicenca"
                                                        IncrementalFilteringMode="Contains" SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False"
                                                        ValueType="System.Int32" Width="100%" AutoPostBack="false">

                                                        <DropDownButton>
                                                            <Image>
                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                    PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                            </Image>
                                                        </DropDownButton>
                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                <ErrorTextPaddings PaddingLeft="4px" />
                                                            </ErrorFrameStyle>
                                                        </ValidationSettings>
                                                    </dx:ASPxComboBox>
                                                </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <br />
                                            <div style="text-align: center;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2"
                                                                OnClick="ButtonOk2_Click2" Text="Ok">
                                                                <ClientSideEvents Click="function(s, e) {   
	popLlojLicence.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk3" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk3"
                                                                OnClick="ButtonOk3_Click2" Text="Ok">
                                                                <%----%>
                                                                <ClientSideEvents Click="function(s, e) {   
	popLlojLicence.Hide();
    Utils.shfaqLoadingGif();;
}" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel>
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                    <dx:ASPxButton ID="btnVazhdoRuajtje" runat="server" CausesValidation="False" ClientInstanceName="btnVazhdoRuajtje" OnClick="btnVazhdoRuajtje_Click" ClientVisible="false">
                    </dx:ASPxButton>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div id="dvNdermarja" style="display: none">
                <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
                    ActiveTabIndex="3" TabSpacing="3px" Width="100%">
                    <ContentStyle>
                        <border bordercolor="#AECAF0" borderstyle="Solid" borderwidth="1px" />
                    </ContentStyle>
                    <TabPages>
                        <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl3" runat="server">
                                    <table class="renditKontrolle">
                                        <tr>
                                            <td class="renditKontrolleCaption">
                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label"
                                                    runat="server" Style="font-size: large" Text="Modeli:">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td class="renditKontrolleCellMeWidth33">
                                                <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                    Width="100%" Height="23px" ShowShadow="False" Style="font-size: medium" ValueType="System.String"
                                                    SettingsLoadingPanel-ImagePosition="Top" AnimationType="None">
                                                    <ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfigurimin()}" />
                                                    <DropDownButton>
                                                        <Image>
                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                        </Image>
                                                    </DropDownButton>
                                                    <ValidationSettings>
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
                                    <dx:ASPxGridView ID="ASPxGridView_Ndermarrjet" runat="server" ClientInstanceName="ASPxGridView_Ndermarrjet"
                                        Width="100%" OnAfterPerformCallback="ASPxGridView_Ndermarrjet_AfterPerformCallback"
                                        OnCustomCallback="ASPxGridView_Ndermarrjet_CustomCallback" OnPreRender="OnPreRender_ASPxGridView_Ndermarrjet"
                                        OnCustomJSProperties="ASPxGridView_Ndermarrjet_CustomJSProperties" OnDataBound="ASPxGridView_Ndermarrjet_DataBound"
                                        OnHeaderFilterFillItems="ASPxGridView_Ndermarrjet_HeaderFilterFillItems" OnProcessColumnAutoFilter="ASPxGridView_Ndermarrjet_ProcessColumnAutoFilter"
                                        Settings-ShowFilterRowMenu="True">
                                        <Templates>
                                            <TitlePanel>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Zgjidh kolonat" AutoPostBack="false"
                                                                ClientVisible="false" Image-Url="images/new/wrench.png" Font-Size="8">
                                                                <ClientSideEvents Click="function (s,e){myFaqeCelje.buttonKonfiguroClick(s,e,ASPxGridView_Ndermarrjet)}"
                                                                    Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <asp:UpdatePanel ID="pnlruaj" runat="server">
                                                                <ContentTemplate>
                                                                    <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Ruaj kolonat" AutoPostBack="true"
                                                                        ClientVisible="false" Image-Url="images/new/disk_blue (3).png" Font-Size="8"
                                                                        OnClick="RuajKolona_Click">
                                                                        <ClientSideEvents Init="myFaqeCelje.InitTeDrejtaKonf" />
                                                                    </dx:ASPxButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </TitlePanel>
                                        </Templates>
                                        <Styles>
                                            <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <ClientSideEvents FocusedRowChanged="function(s, e) {
            mbush=true;	
}"
                                            RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }" SelectionChanged="function(s, e){OnGridSelectionChanged(e);}"
                                            BeginCallback="function(s, e) {
	BeginCallback(s,e);
}"
                                            EndCallback="function (s,e){ endCallback();
}" />
                                        <SettingsPager PageSize="15">
                                        </SettingsPager>
                                        <Settings ShowFilterRowMenu="True"></Settings>
                                        <StylesEditors>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Informacion" Text="Informacion">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl1" runat="server">
                                    <table id="tblNdermarja" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblKodi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kodi_TextBox" ID="lblKodi" runat="server"
                                        Text="Kodi:" ClientInstanceName="lblKodi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkodi_TextBox">--%>
                                    <dx:ASPxTextBox ID="kodi_TextBox" runat="server" ClientInstanceName="kodi_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true" RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 20 karaktere dhe nuk duhet te permbaje thonjeza dyshe"
                                                ValidationExpression='^[^"]{0,20}$'></RegularExpression>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" ID="lblKodLicenca" AssociatedControlID="txtKodLicenca"
                                        runat="server" Text="Kodi i licences" ClientInstanceName="lblKodLicenca">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox Wrap="False" ID="txtKodLicenca" Width="100%"
                                        runat="server" ClientInstanceName="txtKodLicenca">
                                    </dx:ASPxTextBox>

                                    <%--</div>--%>
                                    <%--<div id="dvlblPershkrimi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="pershkrimi_TextBox" ID="lblPershkrimi"
                                        runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvpershkrimi_TextBox">--%>
                                    <dx:ASPxMemo ID="pershkrimi_TextBox" runat="server" ClientInstanceName="pershkrimi_TextBox"
                                        Width="100%" Rows="3">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                            <RegularExpression ErrorText="Pershkrimi nuk duhet te permbaje thonjeza dyshe" ValidationExpression='[^"]*'></RegularExpression>
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxMemo>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNipti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="nipt_TextBox" ID="lblNipti" runat="server"
                                        Text="NIPT:" ClientInstanceName="lblNipti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvnipt_TextBox">--%>
                                    <dx:ASPxTextBox ID="nipt_TextBox" runat="server" ClientInstanceName="nipt_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtLimitiShitjes" ID="lblLimitiShitjes" runat="server"
                                        Text="Limiti i shitjes me kupon:" ClientInstanceName="lblLimitiShitjes">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvnipt_TextBox">--%>
                                    <dx:ASPxTextBox ID="txtLimitiShitjes" runat="server" ClientInstanceName="txtLimitiShitjes"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblLicenca">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="licenca_TextBox" ID="lblLicenca"
                                        runat="server" Text="Licenca:" ClientInstanceName="lblLicenca">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvlicenca_TextBox">--%>
                                    <dx:ASPxTextBox ID="licenca_TextBox" runat="server" ClientInstanceName="licenca_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {
	
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblKodiFiskal">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="kodi_fiskal_TextBox" ID="lblKodiFiskal"
                                        runat="server" Text="Kodi fiskal:" ClientInstanceName="lblKodiFiskal">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvkodi_fiskal_TextBox">--%>
                                    <dx:ASPxTextBox ID="kodi_fiskal_TextBox" runat="server" ClientInstanceName="kodi_fiskal_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblMonedha">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="monedha_ASPxComboBox" ID="lblMonedha"
                                        runat="server" Text="Monedha:" ClientInstanceName="lblMonedha">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvmonedha_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="monedha_ASPxComboBox" runat="server" Width="100%" ClientInstanceName="monedha_ASPxComboBox"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblViti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtViti" ID="lblViti" runat="server"
                                        Text="Viti:" ClientInstanceName="lblViti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtViti">--%>
                                    <dx:ASPxComboBox ID="txtViti" ClientInstanceName="txtViti" Width="100%" runat="server"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <ClientSideEvents ButtonClick="function(s, e) {
                                            grida=false;
                                            Viti_Click();}"
                                            LostFocus="function(s, e) {
                                           
}" />
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblNrTvsh">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtNrTvsh" ID="lblNrTvsh" runat="server"
                                        Text="Nr Tvsh" ClientInstanceName="lblNrTvsh">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtxtNrTvsh">--%>
                                    <dx:ASPxTextBox ID="txtNrTvsh" runat="server" ClientInstanceName="txtNrTvsh" Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {

}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" ErrorText="*" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                   <dx:ASPxLabel Wrap="False" ID="lblkodbiznesi" AssociatedControlID="txtkodbiznesi"
                                        runat="server" Text="Kodi i njesise se biznesit" ClientInstanceName="lblkodbiznesi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxTextBox Wrap="False" ID="txtkodbiznesi" Width="100%"
                                        runat="server" ClientInstanceName="txtkodbiznesi">
                                    </dx:ASPxTextBox>
                                      
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucEmerSkedari" ID="lblCertifikata" 
                                    ClientInstanceName="lblCertifikata" runat="server" Text="Ngarko Certifikaten">
                                    </dx:ASPxLabel>
                                       <asp:UpdatePanel ID="uploadpnl" runat="server">
                                        <ContentTemplate>
                                    <dx:ASPxUploadControl ID="ucEmerSkedari" runat="server" ClientInstanceName="ucEmerSkedari"  Width="70%" ClientVisible="false"
                                                                                ShowProgressPanel="True" OnFileUploadComplete="ucEmerSkedari_FileUploadComplete" AdvancedModeSettings-EnableMultiSelect="false">
<%--                                                                                <ClientSideEvents FileUploadComplete="function(s, e) {shfaqPopupPerPajisjetElektronike(); nese do duhet qe kur te ngarkohet certifikata te shfaqet mesazhi per pajisjet elektronike}"--%>
                                                                                    <%--FilesUploadComplete="function(s, e) { }"--%>
                                                                                  <%--></ClientSideEvents>--%>
                                                                                <ValidationSettings MaxFileSizeErrorText="File qe keni zgjedhur eshte shume i madh!"
                                                                                    MaxFileSize="1048576" AllowedFileExtensions=".p12,.txt"   MaxFileCount="1" >
                                                                                </ValidationSettings>
                                         <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Red">
                                        </DisabledStyle>
                                    </dx:ASPxUploadControl>
                                    
                                     <dx:ASPxButton ID="upload" runat="server" AutoPostBack="False" Text="Ngarko"
                                      ClientInstanceName="upload" Width="100px" ClientEnabled="True" OnClick="upload_Clilck">
                                       <ClientSideEvents Click="function(s, e) { ucEmerSkedari.Upload() ; }" />
                                     </dx:ASPxButton>
                                     <dx:ASPxButton ID="pastro" runat="server" AutoPostBack="True" Text="Pastro Folder" ClientVisible="false"
                                      ClientInstanceName="pastro" Width="100px" ClientEnabled="True" OnClick="pastro_Clilck">
                                       <ClientSideEvents Click="function(s, e) {  }" />
                                     </dx:ASPxButton>
                                     </ContentTemplate>
                                     </asp:UpdatePanel>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtGrupi" ID="lblGrupi" runat="server"
                                        Text="Grupim ndermarrje:" ClientInstanceName="lblGrupi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>

                                    <%--  <dx:ASPxCheckBox ID="cbMonedheKF" Width="80%" runat="server" ClientInstanceName="cbMonedheKF">
                                            </dx:ASPxCheckBox>--%>


                                    <%--<div id="dvtxtGrupi">--%>
                                    <dx:ASPxComboBox ID="txtGrupi" runat="server" Width="100%" ClientInstanceName="txtGrupi"
                                        EnableCallbackMode="False" OnItemRequestedByValue="txtGrupi_ItemRequestedByValue"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
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
                                        <ClientSideEvents ButtonClick="function(s, e) {Grupi_Click();}" />
                                    </dx:ASPxComboBox>
                                    <%--<div>--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLogu" ID="lblLogu" runat="server"
                                        Text="Ruaj Loge:" ClientInstanceName="lblLogu">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div>--%>
                                    <dx:ASPxCheckBox ID="cbLogu" Width="80%" runat="server" ClientInstanceName="cbLogu">
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbfiskalizimi" ID="lblfiskalizimi" runat="server"
                                        Text="Apliko fiskalizim:" ClientInstanceName="lblfiskalizimi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div>--%>
                                    <dx:ASPxCheckBox ID="cbfiskalizimi" Width="80%" runat="server" ClientInstanceName="cbfiskalizimi">
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbMeTvsh" ID="lblMeTvsh" runat="server"
                                        ClientInstanceName="lblMeTvsh" ClientVisible="false">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div>--%>
                                    <dx:ASPxCheckBox ID="cbMeTvsh" Width="80%" runat="server" ClientInstanceName="cbMeTvsh" ClientVisible="false">
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cbLlogariBuxhetori" ID="lblLlogariBuxhetori" runat="server"
                                        ClientInstanceName="lblLlogariBuxhetori" ClientVisible="true" Text="Llogari buxhetori">
                                    </dx:ASPxLabel>
                                    <dx:ASPxCheckBox ID="cbLlogariBuxhetori" Width="80%" runat="server" ClientInstanceName="cbLlogariBuxhetori" ClientVisible="true">
                                    </dx:ASPxCheckBox>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbRaportuesi" ID="lblRaportuesi" runat="server"
                                        Text="Ndermarrje Raportimi:" ClientInstanceName="lblRaportuesi">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbRaportuesi" ClientInstanceName="cmbRaportuesi" Width="100%" runat="server"
                                        SettingsLoadingPanel-ImagePosition="Top" ShowShadow="False">
                                        <LoadingPanelImage>
                                        </LoadingPanelImage>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Kontakt" Text="Kontakt">
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblKontakti" class="renditKontrolle">
                                        <tbody>
                                        </tbody>
                                    </table>
                                    <%--<div id="dvlblEmail">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="email_TextBox" ID="lblEmail" runat="server"
                                        Text="E-mail:" ClientInstanceName="lblEmail">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvemail_TextBox">--%>
                                    <dx:ASPxTextBox ID="email_TextBox" runat="server" ClientInstanceName="email_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ErrorText="Format i gabuar e-mail!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblVendi">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="vendi_TextBox" ID="lblVendi" runat="server"
                                        Text="Vendi:" ClientInstanceName="lblVendi">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvvendi_TextBox">--%>
                                    <dx:ASPxTextBox ID="vendi_TextBox" runat="server" ClientInstanceName="vendi_TextBox"
                                        Width="100%">
                                        <ClientSideEvents TextChanged="function(s, e) {
	
}" />
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblQyteti">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="qyteti_ASPxComboBox" ID="lblQyteti"
                                        runat="server" Text="Qyteti:" ClientInstanceName="lblQyteti">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvqyteti_ASPxComboBox">--%>
                                    <dx:ASPxComboBox ID="qyteti_ASPxComboBox" runat="server" Width="100%" ClientInstanceName="qyteti_ASPxComboBox"
                                        ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                        <ClientSideEvents TextChanged="function(s, e) { }" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                        <DisabledStyle Font-Bold="False">
                                        </DisabledStyle>
                                    </dx:ASPxComboBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblTel">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="tel_TextBox" ID="lblTel" runat="server"
                                        Text="Tel:" ClientInstanceName="lblTel">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvtel_TextBox">--%>
                                    <dx:ASPxTextBox ID="tel_TextBox" runat="server" ClientInstanceName="tel_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9 \-()+]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                    <%--<div id="dvlblFax">--%>
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="fax_TextBox" ID="lblFax" runat="server"
                                        Text="Fax:" ClientInstanceName="lblFax">
                                    </dx:ASPxLabel>
                                    <%--</div>--%>
                                    <%--<div id="dvfax_TextBox">--%>
                                    <dx:ASPxTextBox ID="fax_TextBox" runat="server" ClientInstanceName="fax_TextBox"
                                        Width="100%">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="true">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RegularExpression ValidationExpression="[0-9 \-()+]*" ErrorText="Sheno vetem numra!" />
                                        </ValidationSettings>
                                        <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                        </DisabledStyle>
                                    </dx:ASPxTextBox>
                                    <%--</div>--%>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Ndermarrje Model" Text="Ndermarrje Model">
                            <TabStyle>
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl4" runat="server">
                                    <table width="100%">
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <table class="renditKontrolle">
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojNdermarje" ID="lblNdermarjaTransferuese"
                                                                    runat="server" Text="Ndermarrja transferuese">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxComboBox ID="cmbLlojNdermarje" runat="server" ValueType="System.String" ClientInstanceName="cmbLlojNdermarje"
                                                                    Width="100%">
                                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                           ASPxCallbackPanel1.PerformCallback(s.GetValue());                                                      
	                                                      }" />
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dxcp:ASPxCallbackPanel EnableHierarchyRecreation="false" ID="ASPxCallbackPanel1" runat="server" ClientInstanceName="ASPxCallbackPanel1"
                                                                    OnCallback="ASPxCallbackPanel1_Callback" Width="100%">
                                                                    <PanelCollection>
                                                                        <dx:PanelContent runat="server" SupportsDisabledAttribute="True">
                                                                            <dx:ASPxComboBox ID="cmbNdermarjet" runat="server" ValueType="System.String" ClientInstanceName="cmbNdermarjet"
                                                                                Width="100%">
                                                                            </dx:ASPxComboBox>
                                                                        </dx:PanelContent>
                                                                    </PanelCollection>
                                                                </dxcp:ASPxCallbackPanel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <table id="tblModel" class="renditKontrolle">
                                                                    <tbody>
                                                                    </tbody>
                                                                </table>
                                                                <%--<div id="dvlblEmail">--%>
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbPrind" ID="lblPrind" runat="server"
                                                                    Text="Ndermarrje meme:" ClientInstanceName="lblPrind">
                                                                </dx:ASPxLabel>
                                                                <dx:ASPxCheckBox runat="server" ID="cbPrind" ClientInstanceName="cbPrind">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	Prind_CheckedChanged(s,e);
}" />
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                        ValidationGroup="entries" SetFocusOnError="true">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField IsRequired="true" ErrorText="*" />
                                                                    </ValidationSettings>
                                                                    <DisabledStyle Font-Bold="False">
                                                                    </DisabledStyle>
                                                                </dx:ASPxCheckBox>
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cbOwnShop" ID="lblOwnShop" runat="server"
                                                                    Text="Own Shop:" ClientInstanceName="lblOwnShop">
                                                                </dx:ASPxLabel>
                                                                <dx:ASPxCheckBox runat="server" ID="cbOwnShop" ClientInstanceName="cbOwnShop">
                                                                    <ClientSideEvents CheckedChanged="function(s, e) {
	
}" />
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                        ValidationGroup="entries" SetFocusOnError="true">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField IsRequired="true" ErrorText="*" />
                                                                    </ValidationSettings>
                                                                    <DisabledStyle Font-Bold="False">
                                                                    </DisabledStyle>
                                                                </dx:ASPxCheckBox>
                                                                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMeme" ID="lblMeme" runat="server"
                                                                    Text="Ndermarrje meme:" ClientInstanceName="lblMeme">
                                                                </dx:ASPxLabel>
                                                                <dx:ASPxComboBox ID="cmbMeme" runat="server" Width="100%" ClientInstanceName="cmbMeme"
                                                                    ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top" OnItemRequestedByValue="cmbMeme_ItemRequestedByValue">
                                                                    <ClientSideEvents TextChanged="function(s, e) { }" SelectedIndexChanged="function(s, e) {
	Prind_SelectIndexChanged(s,e);
}" />
                                                                    <DropDownButton>
                                                                        <Image>
                                                                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                        </Image>
                                                                    </DropDownButton>
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                        ValidationGroup="entries2" SetFocusOnError="true" ValidateOnLeave="false">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                                        </ErrorFrameStyle>
                                                                        <RequiredField IsRequired="true" ErrorText="*" />
                                                                    </ValidationSettings>
                                                                    <DisabledStyle Font-Bold="False">
                                                                    </DisabledStyle>
                                                                </dx:ASPxComboBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="margin-left: 19%;">
                                                    <table align="left" style="border: thin solid #996633; margin-left: 19%;" width="30%">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="llog_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Plani standart i llogarive">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="grup_llog_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Grupet dhe nengrupet e llogarive">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="kpf_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Struktura e llogarive">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="monedha_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Monedha default">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="njesi_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Njesite e matjes per artikujt">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="rap_fin_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Shembull per raporte financiare te personalizuara (cash flow)">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="skema_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Skema default kontabilizimi">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="nivelTVSH_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Taksat">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="qytete_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Qytete">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel21" runat="server" Text="Konfigurime ne lidhje me klient/furnitor">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="maturimi_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Maturimi">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="men_pageses_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Menyrat e pageses">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="men_trans_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Menyrat e transportit">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="kushte_drg_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Kushtet e dergimit">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="nivel_cmimi_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Nivel cmimi">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="nivel_zbritje_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Nivel zbritje">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="kategori_zbritje_check" runat="server" Checked="True" CheckState="Checked"
                                                                    Text="Kategori zbritje">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                        <dxtc:TabPage Name="Logo Ndermarrje" Text="Logo Ndermarrje" ClientVisible="true">
                            <TabStyle>
                            </TabStyle>
                            <ContentCollection>
                                <dxw:ContentControl ID="ContentControl5" runat="server">
                                    <div id="dvZgjidhImazh">
                                        <table border="0" cellpadding="0" cellspacing="0" id="mainContainer">
                                            <tr>
                                                <td valign="top" align="center" class="permbajtja">
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                            <td align="center" style="padding-right: 20px; vertical-align: top;">
                                                                <table cellpadding="0" cellspacing="0">
                                                                    <tr>
                                                                        <td class="Koka">
                                                                            <dx:ASPxLabel ID="lblZgjidhImaxh" runat="server" Text="Zgjidh Logo:" AssociatedControlID="ngarkoImazh">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                        <td>
                                                                            <dx:ASPxUploadControl ID="ngarkoImazh" runat="server" ClientInstanceName="ngarkuesi"
                                                                                ShowProgressPanel="True" Size="35" OnFileUploadComplete="ngarkoImazh_FileUploadComplete" >
                                                                                <ClientSideEvents FileUploadComplete="function(s, e) { Ngarkuesi_NeFileNgarkimPlotesuar(e); btnShfaqImazh.DoClick();}"
                                                                                    FilesUploadComplete="function(s, e) { Ngarkuesi_NeFiletNgarkimPlotesuar(e); }"
                                                                                    FileUploadStart="function(s, e) { Ngarkuesi_NeNgarkimFillim(); }" TextChanged="function(s, e) { UpdateButoniNgarkim(); }"></ClientSideEvents>
                                                                                <ValidationSettings MaxFileSizeErrorText="Imazhi qe keni zgjedhur eshte shume i madh!"
                                                                                    MaxFileSize="1048576" AllowedFileExtensions=".jpg,.jpeg,.jpe,.gif,.bmp,.png">
                                                                                </ValidationSettings>
                                                                            </dx:ASPxUploadControl>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td></td>
                                                                        <td class="shenim">
                                                                            <dx:ASPxLabel ID="lblTipLejuar" runat="server" Text="Tipe Logosh te Lejuar: jpg, jpeg, gif, jpe, bmp, png"
                                                                                Font-Size="8pt">
                                                                            </dx:ASPxLabel>
                                                                            <br />
                                                                            <dx:ASPxLabel ID="lblMaksimumiLejuar" runat="server" Text="Maksimumi i madhesise se file-it: 1Mb"
                                                                                Font-Size="8pt">
                                                                            </dx:ASPxLabel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="2" align="center" class="qelizeButoni">
                                                                            <dx:ASPxButton ID="btnNgarko" runat="server" AutoPostBack="False" Text="Ngarko"
                                                                                ClientInstanceName="btnNgarko" Width="100px" ClientEnabled="False">
                                                                                <ClientSideEvents Click="function(s, e) { ngarkuesi.Upload(); }" />
                                                                            </dx:ASPxButton>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td align="center" class="ShfaqjeProve">
                                                                <%--<img src="images/Korniza.jpg" id="previewImage" alt="" />--%>
                                                                <%--  <dx:ASPxImage ID="previewImage" runat="server" ImageUrl="images/Korniza.bmp" ClientInstanceName="previewImage">
                                                            </dx:ASPxImage>--%>
                                                                <asp:UpdatePanel ID="pnlImazh" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <dx:ASPxBinaryImage ID="ASPxBinaryImage1" runat="server" EmptyImage-Url="images/Korniza.bmp"
                                                                            ClientInstanceName="ppp" ImageAlign="Middle">
                                                                        </dx:ASPxBinaryImage>
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </dxw:ContentControl>
                            </ContentCollection>
                        </dxtc:TabPage>
                    </TabPages>
                    <ClientSideEvents ActiveTabChanged="function(s, e) { tabsActiveTabChanged(s,e);}" />
                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                </dxtc:ASPxPageControl>
            </div>
            <asp:UpdatePanel ID="pnl" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" runat="server" />
                    <asp:HiddenField ID="hfLidhur" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" runat="server" />
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" runat="server" />
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfSuperUser" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
                    </dx:ASPxHiddenField>
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
            <dx:ASPxHiddenField ID="hfMsgShtoNdermarrje" runat="server">
            </dx:ASPxHiddenField>
            <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                <ContentTemplate>
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" AllowResize="True"
                        AppearAfter="10" ClientIDMode="AutoID" ClientInstanceName="popupUniversal" CloseAction="CloseButton"
                        EnableAnimation="False" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter">
                        <ClientSideEvents Closing="function(s, e) {
	popupUniversal.SetContentUrl('');
}" />
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    </form>
</body>
</html>
