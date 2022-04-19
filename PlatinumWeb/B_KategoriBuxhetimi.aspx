<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_KategoriBuxhetimi.aspx.cs" Inherits="PlatinumWeb.BKategoriBuxhetimi" %>
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
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/B_KategoriBuxhetimi.aspx-IMB.7.1.js&v76"
        type="text/javascript"></script>

</head>
<body>
    <form id="form1" runat="server" novalidate>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivStyle-Opacity="30"/>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu" OnFilterSave="Ruaj_ASPxButton_Click" OnFilterDelete="FshiFilter_ASPxButton_Click"/>
         <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="PageControl1" ClientInstanceName="PageControl1" Width="100%" Height="600px"  runat="server" ActiveTabIndex="0" 
                                    ClientSideEvents-ActiveTabChanged="function(s,e){onActiveTabChanged(s,e);}">
                    <TabPages>
                        <dx:TabPage  Name="Te pergjithshme" Text="Te pergjithshme">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl1" runat="server">
                                    <table class="renditKontrolle">
                                        <tbody>
                                            <tr>
                                                <td class="renditKontrolleCaption">
                                                    <dx:ASPxLabel ID="lblModeli" AssociatedControlID="cmbKonfigurimi" runat="server" Text="Modeli:"/>
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxComboBox ID="cmbKonfigurimi" Width="100%" runat="server" ClientInstanceName="cmbKonfigurimi" 
                                                                        ClientSideEvents-SelectedIndexChanged="function(s,e){ndryshoKonfigurimin();}" />
                                                </td>
                                                <td class="renditKontrolleCellMeWidth33">
                                                    <dx:ASPxLabel ID="lblKonfigurimi" runat="server" ClientInstanceName="lblKonfigurimi" class="klasePerLblKonfigurimi"></dx:ASPxLabel>
                                                </td>
                                                <td class="renditKontrolleLabelMeWidth33"></td>
                                            </tr>
                                            <tr>
                                                <dx:ASPxGridView    runat="server" ID="gvKategoriBuxhetimi" ClientInstanceName="gvKategoriBuxhetimi"  
                                                                    OnCustomJSProperties="gvKategoriBuxhetimi_CustomJSProperties"
                                                                    OnAfterPerformCallback="gvKategoriBuxhetimi_AfterPerformCallback"   
                                                                    OnCustomCallback="gvKategoriBuxhetimi_CustomCallback" 
                                                                    OnDataBound="gvKategoriBuxhetimi_DataBound" 
                                                                    OnHeaderFilterFillItems="gvKategoriBuxhetimi_HeaderFilterFillItems">
                                                <ClientSideEvents   RowDblClick="function(s, e) {OnGridDoubleClick();   kaloTab=true; }"
                                                                    FocusedRowChanged="function(s, e) {mbush=true;}"
                                                    BeginCallback="function(s, e) {	BeginCallback(s,e); }" />
                                                </dx:ASPxGridView>
                                            </tr>
                                        </tbody>
                                    </table>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                        <dx:TabPage Name="Kategori Buxhetimi" Text="Artikuj Buxhetimi">
                            <ContentCollection>
                                <dx:ContentControl ID="ContentControl2" runat="server">
                                    <table id="tblInformacion" class="renditKontrolle"><tbody></tbody></table>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblKodi" ClientInstanceName="lblKodi" AssociatedControlID="txtKodi" Text="Kodi:" />
                                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtKodi" ClientInstanceName="txtKodi">
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True" RegularExpression-ValidationExpression="^[\s\S]{0,50}$"
                                            RegularExpression-ErrorText="Kodi nuk duhet te jete me shume se 50 karaktere">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblEmertimi" ClientInstanceName="lblEmertimi" AssociatedControlID="txtEmertimi" Text="Emertimi:" />
                                    <dx:ASPxMemo Width="100%" Rows="3" runat="server" ID="txtEmertimi" ClientInstanceName="txtEmertimi" >
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="true"
                                            ValidationGroup="entries" SetFocusOnError="True">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblEmertimi2" ClientInstanceName="lblEmertimi2" AssociatedControlID="txtEmertimi2" Text="Emertimi 2:" />
                                    <dx:ASPxMemo Width="100%" runat="server" ID="txtEmertimi2" ClientInstanceName="txtEmertimi2" >
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxMemo>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblPrindi" ClientInstanceName="lblPrindi" AssociatedControlID="btnePrindi" Text="Prindi:" />
                                    <dx:ASPxComboBox Width="100%" runat="server" ID="btnePrindi" ClientInstanceName="btnePrindi" EnableCallbackMode="true" OnItemRequestedByValue="btnePrindi_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btnePrindi_ItemsRequestedByFilterCondition">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickPrindi();}" SelectedIndexChanged="function(s,e){SelectedIndexChangedPrindi();}" LostFocus="function(s,e){btnePrindiLostFocus(s,e);}"/>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblBuxheti" ClientInstanceName="lblBuxheti" AssociatedControlID="cmbBuxheti" Text="Buxheti:" />
                                    <div><select id="cmbBuxheti"></select></div>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblNiveli" ClientInstanceName="lblNiveli" AssociatedControlID="txtNiveli" Text="Niveli:" />
                                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtNiveli" ClientInstanceName="txtNiveli" >
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblLlogaria" ClientInstanceName="lblLlogaria" AssociatedControlID="btneLlogaria" Text="Llogaria:" />
                                    <dx:ASPxComboBox Width="100%" runat="server" ID="btneLlogaria" ClientInstanceName="btneLlogaria" EnableCallbackMode="true" OnItemRequestedByValue="btneLlogaria_ItemRequestedByValue" OnItemsRequestedByFilterCondition="btneLlogaria_ItemsRequestedByFilterCondition">
                                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickLlogaria();}" />
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblKoeficenti" ClientInstanceName="lblKoeficenti" AssociatedControlID="txtKoeficenti" Text="Koeficenti:" />
                                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtKoeficenti" ClientInstanceName="txtKoeficenti" >
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxTextBox>
                                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblAktive" ClientInstanceName="lblAktive" AssociatedControlID="cbAktive" Text="Aktive:" />
                                    <dx:ASPxCheckBox Width="100%" runat="server" ID="cbAktive" ClientInstanceName="cbAktive" >
                                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                                            ValidationGroup="entries1" ValidateOnLeave="false">
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxCheckBox>
                                </dx:ContentControl>
                            </ContentCollection>
                        </dx:TabPage>
                    </TabPages>
                </dx:ASPxPageControl>
        <ucPopUp:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUp:popUpUniversal>
        <asp:UpdatePanel runat="server" id="hiddenFields">
            <ContentTemplate>
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"/>
                    <asp:HiddenField ID="hfShtimModifikim" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="cmbBuxhetiHf" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfLupaBuxheti" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfId" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
