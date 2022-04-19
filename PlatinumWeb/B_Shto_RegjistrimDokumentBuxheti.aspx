<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="B_Shto_RegjistrimDokumentBuxheti.aspx.cs" Inherits="PlatinumWeb.B_Shto_RegjistrimDokumentBuxheti" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="~/ucMenuAndMsgFrame.ascx" TagPrefix="ucMenu" TagName="ucMenuAndMsgFrame" %>
<%@ Register Src="~/ucPopUpUniversal.ascx" TagPrefix="ucPopUpUniversal" TagName="popUpUniversal" %>
<%@ Register Src="~/ucPopUpKonvertimi.ascx" TagPrefix="ucPopUpKonvertimi" TagName="popUpKonvertimi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    
    <link href="js/css/le-frog/jquery-ui.css" media="screen" rel="stylesheet" type="text/css" runat="server" id="themeJQuery" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
        <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <!-- A DevExtreme library -->

    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/myMesazh-IMB.2.1.js;~/js/async.min.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/js/myFushaShtese-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/multiOpenAccordion-IMB.2.1.js;~/js/toolbar.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/js/myDxDataGrid.js;~/js/memoryObject.js;~/js/aspx.js/B_Shto_RegjistrimDokumentBuxheti.aspx-IMB.7.1.js&v76"
        type="text/javascript"></script>    
</head>
<body>
    <form id="form1" runat="server" novalidate>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Font-Size="9pt" Modal="True" ImagePosition="Top" LoadingDivStyle-Opacity="30"/>

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
        <ucMenu:ucMenuAndMsgFrame ID="menu_msg_Frame" runat="server" OnMenuClick="Menu_ItemClick" OnMenuTemplate="PercaktoTemplateMenu"/>   
        <asp:UpdatePanel runat="server" ID="updatePanel1">
            <ContentTemplate> 
                <ucPopUpKonvertimi:popUpKonvertimi ID="popUpKonvertimi" runat="server" /> 
                <div id="accordition">
                    <div style="font-size:12px;">
                        <h3 id="kokeKonfigurimi">Koke Dokumenti:</h3>
                        <div>
                            <table id="tblKonfigurimi" runat="server">
                            </table>
                            <table id="tblFillim" class="renditKontrolle">
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                    <div class="trup" style="font-size:12px;">
                        <h3 id="trupKonfigurimi">Trup Dokumenti</h3>
                        <div id="divgride1" style="min-height:200px;">
                            <!------Grida e trupit duke perdorur DevExtreme---------->
                            <div class="dx-viewport demo-container">
                                <div id="data-grid-buxheti">
                                    <div id="dxDataGrid_trupPerfitimi" class ="noUndoGrida"></div>
                                </div>
                            </div>
                            <!------------------------------------------------------->
                        </div>
                    </div>
                    <div class="fund" style="font-size:12px;">
                        <h3 id="fundKonfigurimi"><span id="fundKonfigurimidiv" class="ui-not-accordion-header-text">Fund Dokumenti</span></h3>
                        <div>
                            <table id="tblFund" class="renditKontrolle" align="left">
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
        
                <div id="dvFillim" class="">
                    <dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" BackColor="white" ClientInstanceName="lblKonfigurimi" Text="" AssociatedControlID="gauge"></dx:ASPxLabel>
                    <dx:ASPxLabel ID="lblNiveli" ClientInstanceName="lblNiveli" AssociatedControlID="cmbNiveli" runat="server" Text="Niveli:"/>
                    <dx:ASPxComboBox ID="cmbNiveli" Width="100%" runat="server" ClientInstanceName="cmbNiveli" ClientSideEvents-SelectedIndexChanged="function(s,e){TextChangedNiveli();}" />
                    <dx:ASPxLabel ID="lblModeli" ClientInstanceName="lblModeli" AssociatedControlID="cmbModeli" runat="server" Text="Modeli:"/>
                    <dx:ASPxComboBox ID="cmbModeli" Width="100%" runat="server" ClientInstanceName="cmbModeli" ClientSideEvents-SelectedIndexChanged="function(s,e){ndryshoKonfigurimin();}" />
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblNrDok" ClientInstanceName="lblNrDok" AssociatedControlID="txtNrDok" Text="Nr. Dokumenti:" />
                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtNrDok" ClientInstanceName="txtNrDok">
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                            RequiredField-ErrorText="Numri i dokumentit është i domosdoshëm">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="True"></RequiredField>
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel ID="lblLlojVeprimi" ClientInstanceName="lblLlojVeprimi" AssociatedControlID="cmbLlojVeprimi" runat="server" Text="Lloji i veprimit:"/>
                    <dx:ASPxComboBox ID="cmbLlojVeprimi" Width="100%" runat="server" ClientInstanceName="cmbLlojVeprimi" ClientSideEvents-SelectedIndexChanged="function(s,e){cmbLlojVeprimiChanged(s,e);}">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                    <dx:ASPxLabel ID="lblBurimi" ClientInstanceName="lblBurimi" AssociatedControlID="cmbBurimi" runat="server" Text="Burimi i perfitimit:" ClientVisible="false"/>
                    <dx:ASPxComboBox ID="cmbBurimi" Width="100%" runat="server" ClientInstanceName="cmbBurimi" ClientVisible="false">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                    <dx:ASPxLabel Wrap="False" ID="lblEntiteti" AssociatedControlID="cmbEntiteti" runat="server" Text="Entiteti" ClientInstanceName="lblEntiteti" />
                    <dx:ASPxComboBox ID="cmbEntiteti" Width="100%" runat="server" ClientInstanceName="cmbEntiteti"
                        ShowShadow="False" ValueType="System.Int64" EnableClientSideAPI="True" IncrementalFilteringMode="Contains"
                        EnableCallbackMode="True" CallbackPageSize="10" EnableSynchronization="True"
                        OnItemRequestedByValue="cmbEntiteti_ItemRequestedByValue" OnItemsRequestedByFilterCondition="cmbEntiteti_ItemsRequestedByFilterCondition"
                        SettingsLoadingPanel-ImagePosition="Top">
                        <ClientSideEvents ButtonClick="function(s,e){ButtonClickEntiteti();}" SelectedIndexChanged="function(s,e){valueChangedEntiteti(s,e);}"/>
                        <DropDownButton>
                            <Image>
                                <SpriteProperties HottrackedCssClass="dxditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                            </Image>
                        </DropDownButton>
                        <ValidationSettings ErrorText="Ploteso Entitetin" ErrorDisplayMode="ImageWithTooltip"
                            Display="Dynamic" ValidateOnLeave="false" ValidationGroup="entries1">
                            <RequiredField IsRequired="True" />
                        </ValidationSettings>
                        <DisabledStyle Font-Bold="False">
                        </DisabledStyle>
                    </dx:ASPxComboBox>
                    <dx:ASPxLabel ID="lblDoktori" ClientInstanceName="lblDoktori" AssociatedControlID="btnDoktori" runat="server" Text="Doktori:" ClientVisible="false"/>
                    <dx:ASPxComboBox ID="btnDoktori" Width="100%" runat="server" ClientInstanceName="btnDoktori"  ShowShadow="False" ValueType="System.Int32"                         ClientVisible="false" AllowNull="true"
                                            EnableClientSideAPI="True"
                                            IncrementalFilteringMode="Contains" EnableSynchronization="True" EnableCallbackMode="True"
                                            DropDownRows="3" CallbackPageSize="3" OnItemRequestedByValue="btnDoktori_ItemRequestedByValue"
                                            OnItemsRequestedByFilterCondition="btnDoktori_ItemsRequestedByFilterCondition"
                                            SettingsLoadingPanel-ImagePosition="Top">
                        <ClientSideEvents ButtonClick="function(s, e) { ButtonClickDoktori(); }"/>
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
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblDate" ClientInstanceName="lblDate" AssociatedControlID="dteDate" Text="Date Dokumenti:" />
                    <dx:ASPxDateEdit ID="dteDate" Width="100%" runat="server" ClientInstanceName="dteDate"
                                    ShowShadow="False">
                        <ClientSideEvents DateChanged="function(s, e){}" GotFocus="function(s, e){}" />
                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries">
                            <RequiredField IsRequired="True" />
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                            </ErrorFrameStyle>
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

                    <dx:ASPxLabel ID="lblFormatiPrintimit" ClientInstanceName="lblFormatiPrintimit" AssociatedControlID="cmbFormatiPrintimit" runat="server" Text="Formati i Printimit:"/>
                    <dx:ASPxComboBox ID="cmbFormatiPrintimit" Width="100%" runat="server" ClientInstanceName="cmbFormatiPrintimit" />
                    
                    <dx:ASPxLabel ID="lblStatusApr" ClientInstanceName="lblStatusApr" runat="server" AssociatedControlID="lblStatusAprovimi" Text=""/>
                    <dx:ASPxLabel ID="lblStatusAprovimi" ClientInstanceName="lblStatusAprovimi" runat="server" Text=""/>

                    <dx:ASPxHiddenField ID="hfNrAutoShitje" runat="server" ClientInstanceName="hfNrAutoShitje"/>
                    <dx:ASPxHiddenField ID="hfNrAuto" runat="server" ClientInstanceName="hfNrAuto" />
                </div>
                <div id="dvFundi" style="display: none;">
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblTotaliPaTvsh" ClientInstanceName="lblTotaliPaTvsh" AssociatedControlID="txtTotaliPaTvsh" Text="Totali pa tvsh:" />
                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtTotaliPaTvsh" ClientInstanceName="txtTotaliPaTvsh">
                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                            LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="True"></RequiredField>
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblTotali" ClientInstanceName="lblTotali" AssociatedControlID="txtTotali" Text="Totali:" />
                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtTotali" ClientInstanceName="txtTotali">
                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                            LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="True"></RequiredField>
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblTvsh" ClientInstanceName="lblTvsh" AssociatedControlID="txtTvsh" Text="Vlera e Tvsh:" />
                    <dx:ASPxTextBox Width="100%" runat="server" ID="txtTvsh" ClientInstanceName="txtTvsh">
                        <ClientSideEvents Init="function(s,e){Utils.initTxtNumber(s,e)}" GotFocus="function(s,e){Utils.gotFocusTxtNumer(s,e);}"
                            LostFocus="function(s,e){Utils.lostFocusTxtNumer(s,e);}" />
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                            <RequiredField IsRequired="True"></RequiredField>
                        </ValidationSettings>
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel Wrap="False" runat="server" ID="lblShenime" ClientInstanceName="lblShenime" AssociatedControlID="txtShenime" Text="Shenime:" />
                    <dx:ASPxMemo Width="100%" runat="server" ID="txtShenime"  ClientInstanceName="txtShenime" Rows="8">
                        <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" CausesValidation="false"
                            ValidationGroup="entries1" ValidateOnLeave="false" RegularExpression-ValidationExpression="^[\s\S]{0,1000}$" RegularExpression-ErrorText="Fusha Shenime lejon deri ne 1000 karaktere.">
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dx:ASPxMemo>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <ucPopUpUniversal:popUpUniversal runat="server" ID="ucPopUpUniversal"></ucPopUpUniversal:popUpUniversal>
        <asp:UpdatePanel runat="server" id="hiddenFields">
            <ContentTemplate>
                    <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
                    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"/>
                    <asp:HiddenField ID="hfStatusi" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfKonffillestar" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfShtimModifikim" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfKontrollet" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfId" ClientIDMode="Static" runat="server" />
                    <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
