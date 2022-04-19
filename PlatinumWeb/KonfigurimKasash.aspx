<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KonfigurimKasash.aspx.cs"
    Inherits="PlatinumWeb.KonfigurimKasash" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
   
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KonfigurimKasash.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">         
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
            <LoadingDivStyle Opacity="30">
            </LoadingDivStyle>
        </dx:ASPxLoadingPanel>
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
          
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            
        </dx:ASPxGlobalEvents>

        <div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td>
                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" AutoPostBack="true" ClientInstanceName="ASPxMenu1"
                                    ItemImagePosition="Top" OnDataBound="ASPxMenu1_DataBound" OnItemClick="ASPxMenu1_ItemClick"
                                    SeparatorWidth="1px" ShowPopOutImages="True" Width="100%">
                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                    <ClientSideEvents ItemClick="function(s, e) { menu_click(s,e) }" Init="function(s) {s.SetClientVisible(true);}" />
                                     
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
                    </table>

                    <table style="width: 100%">
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" BorderBetweenItemAndSubMenu="HideRootOnly"
                                            ClientIDMode="AutoID" ClientInstanceName="MenuInfo" ShowPopOutImages="True" Width="100%">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true">
                    </dx:ASPxHiddenField>
                    <asp:HiddenField ID="hfStatusi" runat="server" />
                    <asp:HiddenField ID="hfKasa" runat="server" />
                    <asp:HiddenField ID="hfKasaNew" runat="server" />
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
                                        <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?"
                                                ClientInstanceName="lblMsgbox">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                                OnClick="ButtonOk_Click2" Text="Ok">
                                                                <ClientSideEvents Click="function(s, e) { popFshi.Hide();Utils.shfaqLoadingGif(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) {popFshi.Hide();}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popFshi2" runat="server" AllowDragging="True" ClientInstanceName="popFshi2"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kujdes"
                        Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="300px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel3" runat="server" ClientIDMode="AutoID" Width="271px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent3" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel ID="lblMsgbox2" runat="server" ClientIDMode="AutoID" Text="Jeni i sigurt?"
                                                ClientInstanceName="lblMsgbox2">
                                            </dx:ASPxLabel>
                                            <br />
                                            <br />
                                            <div style="text-align: right;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2"
                                                                OnClick="btnPlu_Click" Text="Ok">
                                                                <ClientSideEvents Click="function(s, e) { popFshi2.Hide(); dergoNeKase(s, e); Utils.shfaqLoadingGif(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                        <td>
                                                            <dx:ASPxButton ID="ButtonCancel2" runat="server" ClientIDMode="AutoID" Text="Anullo">
                                                                <ClientSideEvents Click="function(s, e) {popFshi2.Hide();}" />
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
                    <dx:ASPxPopupControl EnableHierarchyRecreation="false" runat="server" ID="popupMbyllKase" AllowDragging="True" ClientInstanceName="popupMbyllKase"
                        CloseAction="CloseButton" EnableAnimation="False" EnableViewState="False" HeaderText="Kasa (Mbyll gjendjen ditore)"
                        Font-Bold="true" Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                        Width="450px" ClientIDMode="AutoID" CssPostfix="Glass">
                        <HeaderStyle>
                            <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                        </HeaderStyle>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel2" runat="server" ClientIDMode="AutoID" Width="450px">
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent2" runat="server" SupportsDisabledAttribute="True">
                                            <table class="renditKontrolle">
                                                <tr>
                                                    <td class="renditKontrolleCaption">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojMbyllje" ID="txtMbyll" runat="server"
                                                            ClientIDMode="AutoID" Text="Lloji i mbylljes s&euml; xhiros:" ClientInstanceName="lblMsgbox">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33">
                                                        <dx:ASPxComboBox ID="cmbLlojMbyllje" runat="server" ClientInstanceName="cmbLlojMbyllje"
                                                            ShowShadow="False" Width="100%" SettingsLoadingPanel-ImagePosition="Top">
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
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth33">
                                                        <dx:ASPxCheckBox ID="printoCheck" runat="server" Text="Printo" Checked="true">
                                                        </dx:ASPxCheckBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel >
                                <br />
                                <dx:ASPxPanel EnableHierarchyRecreation="false" runat="server" ID="paneliDy">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <table>
                                                <tr align="center">
                                                    <td>
                                                        <dx:ASPxButton ID="mbyllXhiroButon" runat="server" CausesValidation="False" ClientInstanceName="mbyllXhiroButon"
                                                            Text="Mbyll Xhiron" HorizontalAlign="Center" OnClick="mbyllXhiroButon_Click">
                                                            <ClientSideEvents Click="function(s, e) {
	                                                        popupMbyllKase.Hide();
                                                            Utils.shfaqLoadingGif();;
                                                        }" />
                                                        </dx:ASPxButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxPanel >
                            </dx:PopupControlContentControl>
                        </ContentCollection>
                    </dx:ASPxPopupControl >
                </ContentTemplate>
            </asp:UpdatePanel>
            <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" OnActiveTabChanging="ASPxPageControl1_ActiveTabChanging" runat="server" ActiveTabIndex="1" ClientInstanceName="PageControl"
                Height="520px"   TabSpacing="3px" Width="100%">
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                <TabPages>
                    <dx:TabPage Name="Kasat" Text="Kasat">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl1" runat="server">
                                <dx:ASPxGridView ID="gvKasat" runat="server" Width="100%" AutoGenerateColumns="true" KeyFieldName="IdKonfigurimi" OnAfterPerformCallback="gvKasat_AfterPerformCallback"
                                    OnHeaderFilterFillItems="gvKasat_HeaderFilterFillItems"
                                    ClientInstanceName="gvKasat" OnDataBound="gvKasat_DataBound" OnProcessColumnAutoFilter="gvKasat_ProcessColumnAutoFilter"
                                    OnCustomCallback="gvKasat_CustomCallback" OnCustomJSProperties="gvKasat_CustomJSProperties">
                                    <ClientSideEvents RowDblClick="function(s, e) {OnGridDoubleClick(e.visibleIndex);   kaloTab=true; }"
                                        FocusedRowChanged="function(s, e) { onNdryshimFokusi(); }" SelectionChanged="function(s, e) { }" />
                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                    <SettingsPager PageSize="15">
                                    </SettingsPager>
                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowGroupPanel="True" ShowHeaderFilterButton="True" UseFixedTableLayout="True" />
                                    <StylesEditors>
                                        <ProgressBar Height="25px">
                                        </ProgressBar>
                                    </StylesEditors>
                                </dx:ASPxGridView>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Name="Konfigurimi i kases" Text="Konfigurimi i kases">
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                                <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlLloj" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlLloj">
                                    <PanelCollection>
                                        <dx:PanelContent>
                                            <table class="CustomRenditKontrolleDy3" >
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojObjekti" ID="lblLlojObjekti"
                                                            runat="server" ClientInstanceName="lblLlojObjekti" Text="Lloji:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxComboBox ID="cmbLlojObjekti" Width="100%" runat="server" ClientInstanceName="cmbLlojObjekti"
                                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents SelectedIndexChanged="function(s, e) {
	                                                        IndexChangedLlojiKasePeshore (cmbLlojObjekti.GetValue());
                                                            }"></ClientSideEvents>
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings CausesValidation="True" SetFocusOnError="true" ValidationGroup="entries">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField ErrorText="*" IsRequired="True" />
                                                                <RequiredField IsRequired="True"></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="renditKontrolleLabelMeWidth25"></td>
                                                    <td class="renditKontrolleCellMeWidth25"></td>
                                                </tr>
                                                <tr>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbLlojiKases" ID="lblLlojiKases"
                                                            runat="server" ClientInstanceName="lblLlojiKases" Text="Lloji:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxComboBox ID="cmbLlojiKases" Width="100%" runat="server" ClientInstanceName="cmbLlojiKases"
                                                            ShowShadow="False" SettingsLoadingPanel-ImagePosition="Top">
                                                            <ClientSideEvents 
                                                                SelectedIndexChanged="function(s, e) { onSelectionChangedLlojKase(cmbLlojiKases.GetValue()); }">
                                                            </ClientSideEvents>
                                                            <DropDownButton>
                                                                <Image>
                                                                    <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua"
                                                                        PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                                                </Image>
                                                            </DropDownButton>
                                                            <ValidationSettings CausesValidation="True" SetFocusOnError="true" ValidationGroup="entries">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                                <RequiredField ErrorText="*" IsRequired="True" />
                                                                <RequiredField IsRequired="True"></RequiredField>
                                                            </ValidationSettings>
                                                            <DisabledStyle Font-Bold="False">
                                                            </DisabledStyle>
                                                        </dx:ASPxComboBox>
                                                    </td>
                                                    <td class="renditKontrolleLabelMeWidth25"></td>
                                                    <td class="renditKontrolleCellMeWidth25"></td>
                                                </tr>
                                                <tr>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi" ID="lblKodi" runat="server"
                                                            Text="Kodi:" ClientInstanceName="lblKodi">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxTextBox ID="txtKodi" runat="server" Width="100%"  AutoPostBack="false" ClientInstanceName="txtKodi">
                                                            <ClientSideEvents Init="function(s, e) { s.Focus(); }" />
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,50}$"
                                                                RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere"
                                                                    ValidationExpression="^[\s\S]{0,50}$"></RegularExpression>
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </td>

                                                    <td class="renditKontrolleLabelMeWidth25"></td>
                                                    <td class="renditKontrolleCellMeWidth25"></td>
                                                </tr>
                                                <tr>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtSkema" ID="lblSkema" runat="server"
                                                            Text="Skema e Peshores:" ClientInstanceName="lblSkema" ClientVisible="false">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxTextBox ID="txtSkema" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtSkema" ClientVisible="false">
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,50}$"
                                                                RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RegularExpression ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere" ValidationExpression="^[\s\S]{0,50}$"></RegularExpression>
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </td>

                                                    <td class="renditKontrolleLabelMeWidth25"></td>
                                                    <td class="renditKontrolleCellMeWidth25"></td>
                                                </tr>

                                                <tr>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtUrl" ID="lblUrl"
                                                            runat="server" ClientInstanceName="lblUrl" Text="Url:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxTextBox ID="txtUrl" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtUrl">
                                                            <ClientSideEvents Init="function(s, e) { s.Focus(); }" />
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                                                ValidationGroup="entries" SetFocusOnError="True">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px" />
                                                                </ErrorFrameStyle>
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                    <td class="renditKontrolleLabelMeWidth25">
                                                        <dx:ASPxLabel Wrap="False" AssociatedControlID="txtIPKase" ID="lblIPKase"
                                                            runat="server" ClientInstanceName="lblIPKase" Text="IP e Kases:">
                                                        </dx:ASPxLabel>
                                                    </td>
                                                    <td class="renditKontrolleCellMeWidth25">
                                                        <dx:ASPxTextBox ID="txtIPKase" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtIPKase">
                                                            <ClientSideEvents Init="function(s, e) { }" />
                                                            <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                <ErrorFrameStyle ImageSpacing="4px">
                                                                    <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                </ErrorFrameStyle>
                                                            </ValidationSettings>
                                                            <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                            </DisabledStyle>
                                                        </dx:ASPxTextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </dx:PanelContent>
                                    </PanelCollection>
                                    <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                </dx:ASPxPanel >
                                <br />
                                <br />
                                <br />

                                    <div style="display:inline-flex; overflow: auto;">
                                        <div>
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlBosh" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlBosh" Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table >
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <br />
                                                                    <dx:ASPxLabel Wrap="False" ID="lblZgjidhLlojin" runat="server" Text="                    Zgjidhni llojin e kases qe keni lidhur me kompjuterin tuaj.                      "
                                                                        Font-Bold="True" Font-Size="Medium">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlIVA" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlIVA" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table >
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucIVA" ID="lblSkedariKomandave"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandave" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                                    <dx:ASPxTextBox ID="ucIVA" runat="server"  ClientInstanceName="ucIVA">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td colspan="3">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblShembull" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetore" ClientInstanceName="cbKasaMeShifraDhjetore" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbKase" runat="server" Text="Kase" Checked="True" ClientInstanceName="rbKasa"
                                                                        TextSpacing="2px" Wrap="False">
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbKasa.GetChecked()==true){rbPrinter.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbPrinter" runat="server" Text="Printer" ClientInstanceName="rbPrinter"
                                                                        TextSpacing="2px" Wrap="False">
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbPrinter.GetChecked()==true){rbKasa.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrFature" runat="server" ClientInstanceName="cbPrintoNrFature" Text="Printo numer fature"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoTotalDheNeMon" runat="server" Text="Printo totalin dhe ne monedhen"
                                                                        ClientInstanceName="cbPrintoTotalDheNeMon" TextSpacing="2px" Wrap="False" >
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoTotalDheNeMon"></ClientSideEvents>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxComboBox ID="cmbMonedha" runat="server" ClientInstanceName="cmbMonedha"
                                                                        ClientEnabled="false" ShowShadow="False"  SettingsLoadingPanel-ImagePosition="Top">
                                                                        <DropDownButton>
                                                                            <Image>
                                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                                            </Image>
                                                                        </DropDownButton>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                                            SetFocusOnError="true" ValidationGroup="entries">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RequiredField ErrorText="*" IsRequired="True" />
                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle Font-Bold="False">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbKuponTatimor" runat="server" Text="Kupon tatimor" ClientInstanceName="rbKuponTatimor"
                                                                        TextSpacing="2px" Wrap="False" >
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbKuponTatimor.GetChecked()==true){rbFatureTatimore.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbFatureTatimore" runat="server" Text="Fature tatimore"
                                                                         ClientInstanceName="rbFatureTatimore" Checked="true" TextSpacing="2px"
                                                                        Wrap="False">
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbFatureTatimore.GetChecked()==true){rbKuponTatimor.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrKopjeFature" runat="server" Text="Printo kopje fature"
                                                                        ClientInstanceName="cbPrintoNrKopjeFature" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoKopjeTeKthimeve" runat="server" ClientInstanceName="cbPrintoKopjeTeKthimeve"
                                                                        Text="Printo kopje te kthimeve" TextSpacing="2px" Wrap="False" >
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoKopjeTeKthimeve"></ClientSideEvents>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxTextBox ID="txtNrKopjeKthimesh" runat="server"  Text="0" ClientInstanceName="txtNrKopjeKthimesh"
                                                                        ClientEnabled="false">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RegularExpression ErrorText="Duhet te jete numer" ValidationExpression="[0-9]*"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoManualishtNgaKasa" runat="server" Text="Printo manualisht nga kasa"
                                                                        ClientInstanceName="cbPrintoManualishtNgaKasa" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td></td>
                                                               <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNeServerIVA" runat="server" Text="Printo nga Serveri"
                                                                        ClientInstanceName="cbPrintoNeServerIVA" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td></td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoPershkrim2IVA" runat="server" Text="Printo Pershkrim 2"
                                                                        ClientInstanceName ="cbPrintoPershkrim2IVA" TextSpacing ="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlAED" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlAED" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucAED" ID="lblSkedariKomandaveAED"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandaveAED" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxTextBox ID="ucAED" runat="server" ClientInstanceName="ucAED">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td colspan="2">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblShembullAED" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPorta" ID="lblPorta" runat="server"
                                                                        Text="Porta">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxTextBox ID="txtPorta" runat="server" ClientInstanceName="txtPorta" Text="1">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbMbyllCdoFature" ClientInstanceName="cbMbyllCdoFature" runat="server" Text="Mbyll cdo fature me vete "
                                                                        Checked="true" TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoBarkod" ClientInstanceName="cbPrintoBarkod" runat="server" Text="Printo me Barkod" TextSpacing="2px"
                                                                        Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbRuajKopje" ClientInstanceName="cbRuajKopje" runat="server" Text="Ruaj Kopje" TextSpacing="2px"
                                                                        Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetoreAED" ClientInstanceName="cbKasaMeShifraDhjetoreAED" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNeServerAED" runat="server" Text="Printo nga Serveri"
                                                                        ClientInstanceName="cbPrintoNeServerAED" TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoKodArtikulli" ClientInstanceName="cbPrintoKodArtikulli" runat="server" Text="Printo kod artikulli"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrKopjeFatureAED" runat="server" Text="Printo kopje fature"
                                                                        ClientInstanceName="cbPrintoNrKopjeFatureAED" TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoPershkrim2AED" runat="server" Text="Printo Pershkrim 2"
                                                                        ClientInstanceName="cbPrintoPershkrim2AED" TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlBTN" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlBTN" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table >
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucBTN" ID="lblSkedariKomandaveBTN"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandaveBTN" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                                    <dx:ASPxTextBox ID="ucBTN" runat="server" ClientInstanceName="ucBTN" >
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td colspan="3">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblShembullBTN" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetoreBTN" ClientInstanceName="cbKasaMeShifraDhjetoreBTN" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbKaseBTN" runat="server" Text="Kase" Checked="True" ClientInstanceName="rbKasaBTN"
                                                                        TextSpacing="2px" Wrap="False" >
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbKasaBTN.GetChecked()==true){rbPrinterBTN.SetChecked(false);}}"> </ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbPrinterBTN" runat="server" Text="Printer" ClientInstanceName="rbPrinterBTN"
                                                                        TextSpacing="2px" Wrap="False" >
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbPrinterBTN.GetChecked()==true){rbKasaBTN.SetChecked(false);}	}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrFatureBTN" ClientInstanceName="cbPrintoNrFatureBTN" runat="server" Text="Printo numer fature"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <hr />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoTotalDheNeMonBTN" runat="server" Text="Printo totalin dhe ne monedhen"
                                                                        ClientInstanceName="cbPrintoTotalDheNeMonBTN" TextSpacing="2px" Wrap="False">
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoTotalDheNeMonBTN"></ClientSideEvents>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxComboBox ID="cmbMonedhaBTN" runat="server" ClientInstanceName="cmbMonedhaBTN"
                                                                        ClientEnabled="false" ShowShadow="False"  SettingsLoadingPanel-ImagePosition="Top">
                                                                        <DropDownButton>
                                                                            <Image>
                                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                                            </Image>
                                                                        </DropDownButton>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                                            SetFocusOnError="true" ValidationGroup="entries">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RequiredField ErrorText="*" IsRequired="True" />
                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle Font-Bold="False">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrKopjeFatureBTN" runat="server" Text="Printo kopje fature"
                                                                        ClientInstanceName="cbPrintoNrKopjeFatureBTN" TextSpacing="2px">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                           
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoKopjeTeKthimeveBTN" runat="server" ClientInstanceName="cbPrintoKopjeTeKthimeveBTN"
                                                                        Text="Printo kopje te kthimeve" TextSpacing="2px">
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoKopjeTeKthimeveBTN"></ClientSideEvents>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxTextBox ID="txtNrKopjeKthimeshBTN" runat="server"  Text="0"
                                                                        ClientInstanceName="txtNrKopjeKthimeshBTN" ClientEnabled="false">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RegularExpression ErrorText="Duhet te jete numer" ValidationExpression="[0-9]*"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                 <dx:ASPxCheckBox ID="cbPrintoNeServerBTN" runat="server" Text="Printo nga Serveri"
                                                                        ClientInstanceName="cbPrintoNeServerBTN" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                     </td>
                                                                     <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbKuponTatimorBTN" runat="server" Text="Kupon tatimor" ClientInstanceName="rbKuponTatimorBTN"
                                                                        TextSpacing="2px" >

                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbKuponTatimorBTN.GetChecked()==true){rbFatureTatimoreBTN.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxRadioButton ID="rbFatureTatimoreBTN" runat="server" Text="Fature tatimore"
                                                                        ClientInstanceName="rbFatureTatimoreBTN" Checked="true" TextSpacing="2px" >
                                                                        <ClientSideEvents CheckedChanged="function(s, e) {if(rbFatureTatimoreBTN.GetChecked()==true){rbKuponTatimorBTN.SetChecked(false);}}"></ClientSideEvents>
                                                                    </dx:ASPxRadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoPershkrim2BTN" runat="server" Text="Printo Pershkrim 2"
                                                                        ClientInstanceName ="cbPrintoPershkrim2BTN" TextSpacing ="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlCKVNOKI" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlCKVNOKI" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table>
                                                        <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucCKV" ID="lblSkedariKomandaveCKV"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandaveCKV" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                                    <dx:ASPxTextBox ID="ucCKV" runat="server" ClientInstanceName="ucCKV" >
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                        <tr>
                                                                <td></td>
                                                                <td colspan="3">
                                                                    <dx:ASPxLabel Wrap="False" ID="ASPxLabel2" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblPortaCom" runat="server" Text="Porta COM">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth33">
                                                                    <dx:ASPxTextBox ID="txtPortaCom" ClientInstanceName="txtPortaCom" runat="server"  Text="COM1">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetoreCKVNOKI" ClientInstanceName="cbKasaMeShifraDhjetoreCKVNOKI" Checked="true" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" ID="lblBoudRate" runat="server" Text="BoudRate">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxTextBox ID="txtBoudRate" ClientInstanceName="txtBoudRate" runat="server"  Text="6400">
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxCheckBox ID="cbPrintoNrFatureCKVNOKI" ClientInstanceName="cbPrintoNrFatureCKVNOKI" runat="server" Text="Printo numer fature"
                                                                    TextSpacing="2px" >
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxCheckBox ID="cbMeSkedar" ClientInstanceName="cbMeSkedar" runat="server" Text="Kase e konfiguruar me skedar"
                                                                    TextSpacing="2px" >
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                <dx:ASPxCheckBox ID="cbPrintoNeServerCKV" runat="server" Text="Printo nga Serveri"
                                                                    ClientInstanceName="cbPrintoNeServerCKV" TextSpacing="2px" Wrap="False" >
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxCheckBox ID="cbPrintoPershkrim2CKV" runat="server" Text="Printo Pershkrim 2"
                                                                    ClientInstanceName ="cbPrintoPershkrim2CKV" TextSpacing ="2px" Wrap="False">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlPKP" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlPKP" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table >
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucPKP" ID="lblSkedariKomandavePKP"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandavePKP" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                                    <dx:ASPxTextBox ID="ucPKP" runat="server"  ClientInstanceName="ucPKP">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td colspan="3">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblShembullPKP" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetorePKP" ClientInstanceName="cbKasaMeShifraDhjetorePKP" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrFaturePKP" ClientInstanceName="cbPrintoNrFaturePKP" runat="server" Text="Printo numer fature"
                                                                        TextSpacing="2px">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                    <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                 <dx:ASPxCheckBox ID="cbPrintoNeServerPKP" runat="server" Text="Printo nga Serveri"
                                                                        ClientInstanceName="cbPrintoNeServerPKP" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                     </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                    <dx:ASPxCheckBox ID="cbPrintoTotalDheNeMonPKP" runat="server" ClientInstanceName="cbPrintoTotalDheNeMonPKP"
                                                                        Text="Printo totalin dhe ne monedhen" TextSpacing="2px">
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoTotalDheNeMonPKP"></ClientSideEvents>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50" colspan="3">
                                                                    <dx:ASPxComboBox ID="cmbMonedhaPKP" runat="server" ClientInstanceName="cmbMonedhaPKP"
                                                                        ClientEnabled="false" ShowShadow="False"  SettingsLoadingPanel-ImagePosition="Top">
                                                                        <DropDownButton>
                                                                            <Image>
                                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                                            </Image>
                                                                        </DropDownButton>
                                                                        <ValidationSettings CausesValidation="True" SetFocusOnError="true" ValidationGroup="entries">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RequiredField ErrorText="*" IsRequired="True" />
                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle Font-Bold="False">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrKopjeFaturePKP" runat="server" ClientInstanceName="cbPrintoNrKopjeFaturePKP"
                                                                        Text="Printo kopje fature" TextSpacing="2px">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoKopjeTeKthimevePKP" runat="server" ClientInstanceName="cbPrintoKopjeTeKthimevePKP"
                                                                        Text="Printo kopje te kthimeve" TextSpacing="2px">
                                                                        <ClientSideEvents CheckedChanged="CheckedChanged_cbPrintoKopjeTeKthimevePKP"></ClientSideEvents>
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25" colspan="2">
                                                                    <dx:ASPxTextBox ID="txtNrKopjeKthimeshPKP" runat="server"  Text="0"
                                                                        ClientInstanceName="txtNrKopjeKthimeshPKP" ClientEnabled="false">
                                                                        <ValidationSettings>
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RegularExpression ErrorText="Duhet te jete numer" ValidationExpression="[0-9]*"></RegularExpression>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoPershkrim2PKP" runat="server" Text="Printo Pershkrim 2"
                                                                        ClientInstanceName ="cbPrintoPershkrim2PKP" TextSpacing ="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlGEKOS" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlGEKOS" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucGEKOS" ID="lblSkedariKomandaveGEKOS"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandaveGEKOS" Text="Skedari i Komandave:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxTextBox ID="ucGEKOS" runat="server"  ClientInstanceName="ucGEKOS">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <dx:ASPxLabel Wrap="False" ID="lblShembullGEKOS" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtPassOperator" ID="lblPassOperator"
                                                                        runat="server" Text="Pass. operatorit">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxTextBox ID="txtPassOperator" ClientInstanceName="txtPassOperator" runat="server"  Text="0000">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGjuha" ID="lblGjuha" runat="server"
                                                                        Text="Gjuha">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxComboBox ID="cmbGjuha" runat="server" ClientInstanceName="cmbGjuha" ShowShadow="False"
                                                                         SettingsLoadingPanel-ImagePosition="Top">
                                                                        <DropDownButton>
                                                                            <Image>
                                                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua"></SpriteProperties>
                                                                            </Image>
                                                                        </DropDownButton>
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="True"
                                                                            SetFocusOnError="true" ValidationGroup="entries">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                            <RequiredField ErrorText="*" IsRequired="True" />
                                                                            <RequiredField IsRequired="True"></RequiredField>
                                                                        </ValidationSettings>
                                                                        <DisabledStyle Font-Bold="False">
                                                                        </DisabledStyle>
                                                                    </dx:ASPxComboBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="txtShenime" ID="lblShenime" runat="server"
                                                                        Text="Shenime ne fature">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxTextBox ID="txtShenime" ClientInstanceName="txtShenime" runat="server"  Text="">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrKopjeFatureGEKOS" runat="server" ClientInstanceName="cbPrintoNrKopjeFatureGEKOS"
                                                                        Text="Printo kopje fature" TextSpacing="2px">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                      <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                 <dx:ASPxCheckBox ID="cbPrintoNeServerGEKOS" runat="server" Text="Printo nga Serveri"
                                                                        ClientInstanceName="cbPrintoNeServerGEKOS" TextSpacing="2px" Wrap="False" >
                                                                    </dx:ASPxCheckBox>
                                                                     </td>
                                                               
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" ID="lblKodiTVSH" runat="server" Text="Kodi i TVSH 20%">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxTextBox ID="txtKodiTVSH" ClientInstanceName="txtKodiTVSH" runat="server"  Text="2">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                            <ErrorFrameStyle ImageSpacing="4px">
                                                                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                            </ErrorFrameStyle>
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth50">
                                                                    <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetoreGEKOS" ClientInstanceName="cbKasaMeShifraDhjetoreGEKOS" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                        TextSpacing="2px"  Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoNrFatureGEKOS" ClientInstanceName="cbPrintoNrFatureGEKOS" runat="server" Text="Printo numer fature"
                                                                        TextSpacing="2px"  Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="renditKontrolleCellMeWidth25">
                                                                    <dx:ASPxCheckBox ID="cbPrintoPershkrim2GEKOS" runat="server" Text="Printo Pershkrim 2"
                                                                        ClientInstanceName ="cbPrintoPershkrim2GEKOS" TextSpacing ="2px" Wrap="False">
                                                                    </dx:ASPxCheckBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlBNTAClass" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlBNTAClass" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table>
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" ID="lblPortaComBNTAClass" runat="server" Text="Porta COM">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxTextBox ID="txtPortaComBNTAClass" ClientInstanceName="txtPortaComBNTAClass" runat="server"  Text="COM4">
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth50">
                                                                <dx:ASPxCheckBox ID="cbKasaMeShifraDhjetoreBNTAClass" ClientInstanceName="cbKasaMeShifraDhjetoreBNTAClass" runat="server" Text="Kasa e konfiguruar me shifra dhjetore"
                                                                    TextSpacing="2px" Wrap="False" CheckState="Checked">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="renditKontrolleCaption">
                                                                <dx:ASPxLabel Wrap="False" ID="lblBoudRateBNTAClass" runat="server" Text="BoudRate">
                                                                </dx:ASPxLabel>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth33">
                                                                <dx:ASPxTextBox ID="txtBoudRateBNTAClass" ClientInstanceName="txtBoudRateBNTAClass" runat="server"  Text="9600">
                                                                    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                                            <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                                                                        </ErrorFrameStyle>
                                                                    </ValidationSettings>
                                                                </dx:ASPxTextBox>
                                                            </td>
                                                            <td class="renditKontrolleCellMeWidth50" colspan="2">
                                                                <dx:ASPxCheckBox ID="cbPrintoNeServerBNTAclass" runat="server" Text="Printo nga Serveri"
                                                                    ClientInstanceName="cbPrintoNeServerBNTAclass" TextSpacing="2px" Wrap="False" >
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="renditKontrolleCellMeWidth25">
                                                                <dx:ASPxCheckBox ID="cbPrintoPershkrim2BNTAclass" runat="server" Text="Printo Pershkrim 2"
                                                                    ClientInstanceName ="cbPrintoPershkrim2BNTAclass" TextSpacing ="2px" Wrap="False">
                                                                </dx:ASPxCheckBox>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlPeshore" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlPeshore" ClientVisible="false"
                                                Height="300px">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <table >
                                                            <tr>
                                                                <td class="renditKontrolleCaption">
                                                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="ucPeshore" ID="lblPeshore"
                                                                        runat="server" ClientInstanceName="lblSkedariKomandaveAED" Text="Path file peshore:">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                                <td class="renditKontrolleCell" colspan="2">
                                                                    <dx:ASPxTextBox ID="ucPeshore" runat="server" Width="50%" ClientInstanceName="ucPeshore">
                                                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
                                                                        </ValidationSettings>
                                                                    </dx:ASPxTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td colspan="2">
                                                                    <dx:ASPxLabel Wrap="False" ID="ASPxLabel3" runat="server" Text="Shembull:  C:\Temp\skedar.inp">
                                                                    </dx:ASPxLabel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                        </div>
                                        <div style="overflow:auto; width: 900px" >
                                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="pnlgrida" runat="server" ClientIDMode="AutoID" ClientInstanceName="pnlgrida"  Height="300px"
                                                ClientVisible="false">
                                                <PanelCollection>
                                                    <dx:PanelContent>
                                                        <dx:ASPxGridView ID="gvNiveleTVSHIVA" runat="server" ClientInstanceName="gvNiveleTVSHIVA"
                                                            OnCustomCallback="gvNiveleTVSHIVA_CustomCallback" OnHtmlRowCreated="gvNiveleTVSHIVA_HtmlRowCreated"
                                                            OnCustomJSProperties="gvNiveleTVSHIVA_CustomJSProperties" >
                                                            <SettingsPager Mode="ShowAllRecords" PageSize="0">
                                                            </SettingsPager>
                                                        </dx:ASPxGridView>
                                                    </dx:PanelContent>
                                                </PanelCollection>
                                                <Border BorderColor="#99CCFF" BorderStyle="Solid" BorderWidth="1px"></Border>
                                            </dx:ASPxPanel >
                                        </div>
                                    </div>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                    <dx:TabPage Text="Raporte" Name="Raporte">

                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl2" runat="server">
                                <asp:UpdatePanel ID="pnlraporte" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <table id="tblGrupimi" >
                                            <tr>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <dx:ASPxButton ID="btnRaportiX" runat="server" Text="Raporti X" Width="200px" OnClick="btnRaportiX_Click">
                                                        <ClientSideEvents Click="Click_btnRaportiX" />
                                                    </dx:ASPxButton>
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnRaportiZ" runat="server" Text="Raporti Z" Width="200px" OnClick="btnRaportiZ_Click">
                                                        <ClientSideEvents Click="Click_btnRaportiZ" />
                                                    </dx:ASPxButton>
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <dx:ASPxButton ID="btnPlu" runat="server" Text="Fshi PLU" Width="200px" >
                                                        <ClientSideEvents Click="Click_btnPlu" />
                                                    </dx:ASPxButton>
                                                    <br />
                                                    <br />
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>
                </TabPages>
                <ClientSideEvents ActiveTabChanged="function(s, e) {tabsActiveTabChanged(s,e); }" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
            </dx:ASPxPageControl>
            <asp:HiddenField ID="hfVlerat" runat="server" />
            <asp:HiddenField ID="hfFile" runat="server" />
            <asp:HiddenField ID="hfPyetje" runat="server" />
            <asp:HiddenField ID="hfShtimModifikim" runat="server" />
            <asp:HiddenField ID="hfId" runat="server" />
            <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta">
            </dx:ASPxHiddenField>
        </div>
    </form>
</body>
</html>
