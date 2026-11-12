<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RivleresimMagazine.aspx.cs"
    Inherits="PlatinumWeb.RivleresimMagazine" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>AVEC Accounting</title>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/aspx.js/RivleresimMagazine.aspx-IMB.2.1.js&v77"
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
        </asp:ScriptManager>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true">
        </dx:ASPxHiddenField>
        <div>
            <asp:UpdatePanel runat="server" ID="pnl">
                <ContentTemplate>
                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false" ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                        OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                        <ClientSideEvents Init="function(s, e) { s.SetClientVisible(true); menuInit(s,e);}" ItemClick="function(s, e) { menuClick(s,e);}" />
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <eo:ProgressBar ID="ProgressBar1" runat="server" Width="100%" OnRunTask="ProgressBar1_RunTask"
                ClientSideOnError="onTaskError" ClientSideOnValueChanged="onTaskRunning" ClientSideOnTaskDone="onTaskDone" BorderColor="Black"
                BorderStyle="Solid" BorderWidth="1px" ControlSkinID="None" IndicatorColor="LightBlue"
                ShowPercentage="True" Height="18px">
            </eo:ProgressBar>
            <dx:ASPxLabel ID="lblProgresi" ClientInstanceName="lblProgresi" runat="server" Text="" Wrap="True">
            </dx:ASPxLabel>

            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" runat="server" ID="ASPxRoundPanel1" ClientIDMode="AutoID"
                HeaderText="Rivlerësimi i inventarit" Width="100%">
                <ContentPaddings Padding="14px" />
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <table class="renditKontrolle">
                            <tr>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbMagazina" ID="lblMagazina" runat="server" Text="Magazina">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="renditKontrolleCellMeWidth33">
                                    <dx:ASPxComboBox ID="cmbMagazina" runat="server" ShowShadow="False" Width="100%"
                                        ClientInstanceName="cmbMagazina" IncrementalFilteringMode="Contains">
                                        <ClientSideEvents SelectedIndexChanged="function(s,e){ filtriNdryshoi(); }" />
                                    </dx:ASPxComboBox>
                                </td>
                                <td class="renditKontrolleCaption">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaNga" ID="lblPeriudha" runat="server" Text="Periudha">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="renditKontrolleCellMeWidth33">
                                    <dx:ASPxDateEdit ID="dtePeriudhaNga" ClientInstanceName="dtePeriudhaNga" runat="server" ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="function (s,e){dtePeriudhaChanged(s,e);}" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="padding: 2px 7px 2px 7px;">
                                    <dx:ASPxLabel Wrap="False" AssociatedControlID="dtePeriudhaDeri" ID="lblDeri" runat="server" Text=" - ">
                                    </dx:ASPxLabel>
                                </td>
                                <td class="renditKontrolleCellMeWidth33">
                                    <dx:ASPxDateEdit ID="dtePeriudhaDeri" ClientInstanceName="dtePeriudhaDeri" runat="server" ShowShadow="False" Width="100%">
                                        <ClientSideEvents DateChanged="function (s,e){dtePeriudhaChanged(s,e);}" />
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="padding-left: 10px;">
                                    <dx:ASPxButton ID="btnShfaq" runat="server" Text="Shfaq artikujt" AutoPostBack="False" CausesValidation="False" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { shfaqArtikujt(); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="btnZgjidhGjitha" runat="server" ToolTip="Zgjidh të gjithë artikujt"
                                        Image-Url="images/checks.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { gvRivleresim.SelectRows(); }" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnHiqZgjedhjen" runat="server" ToolTip="Hiq zgjedhjen"
                                        Image-Url="images/uncheck2.png" Image-Height="16px" AutoPostBack="False" CausesValidation="False" UseSubmitBehavior="false">
                                        <ClientSideEvents Click="function(s, e) { gvRivleresim.UnselectRows(); }" />
                                    </dx:ASPxButton>
                                </td>
                                <td style="padding-left: 10px;">
                                    <dx:ASPxLabel Wrap="False" ID="lblArtikujtPerRivleresim" runat="server"
                                        Text="Artikujt me lëvizje në periudhë" ClientInstanceName="lblArtikujtPerRivleresim">
                                    </dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                        <dx:ASPxGridView ID="gvRivleresim" runat="server" ClientInstanceName="gvRivleresim" Width="100%"
                            KeyFieldName="IdArtikulli" AutoGenerateColumns="False"
                            OnCustomCallback="gvRivleresim_CustomCallback" OnCustomJSProperties="gvRivleresim_CustomJSProperties">
                            <SettingsBehavior AllowSelectByRowClick="True" AllowFocusedRow="True" />
                            <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowFilterBar="Visible" />
                            <SettingsPager PageSize="20" />
                            <SettingsLoadingPanel ImagePosition="Top" />
                            <ClientSideEvents EndCallback="function(s, e) { gvRivleresimEndCallback(s, e); }" />
                            <Columns>
                                <dx:GridViewCommandColumn ShowSelectCheckbox="True" SelectAllCheckboxMode="AllPages" VisibleIndex="0" Width="40px" />
                                <dx:GridViewDataTextColumn FieldName="Kodi" Caption="Kodi" VisibleIndex="1">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="Pershkrimi" Caption="Përshkrimi" VisibleIndex="2">
                                    <Settings AutoFilterCondition="Contains" />
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="NrMagazinash" Caption="Magazina" VisibleIndex="3" Width="90px" />
                                <dx:GridViewDataDateColumn FieldName="DataEPare" Caption="Lëvizja e parë" VisibleIndex="4" Width="120px">
                                    <PropertiesDateEdit DisplayFormatString="dd/MM/yyyy" />
                                </dx:GridViewDataDateColumn>
                                <dx:GridViewDataTextColumn FieldName="NrDitesh" Caption="Ditë me lëvizje" VisibleIndex="5" Width="120px" />
                            </Columns>
                        </dx:ASPxGridView>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>
        </div>
    </form>
</body>
</html>
