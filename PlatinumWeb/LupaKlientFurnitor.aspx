<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKlientFurnitor.aspx.cs"
    Inherits="PlatinumWeb.LupaKlientFurnitor" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            font-weight: bold;
            color: #666666;
        }
    </style>
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />

    <!-- DevExtreme themes -->
    <link rel="stylesheet" type="text/css" href="Content/dx.common.css" />
    <link rel="stylesheet" type="text/css" href="Content/dx.generic.alphaweb-compact.css" />
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <!-- A DevExtreme library -->
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/Scripts/dx.viz-web.js;~/js/localization/DevExtreme.Perkthime.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myDxDataGrid.js;~/js/aspx.js/LupaKlientFurnitor.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body >
    <form id="form1" runat="server">
           
        <asp:ScriptManager ID="ScriptManager1" runat="server">
           
        </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        </dx:ASPxGlobalEvents>
        <div id='div' style="display: none">           
            <table width="98%">
                <tr>
                    <td>
                        <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                            ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                           >
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
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            
                <ContentTemplate>
                    <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
                    </dx:ASPxHiddenField>
                                    <dx:ASPxGridView ID="gvLupaKlientFurnitor" runat="server" SettingsBehavior-ColumnResizeMode="NextColumn" ClientInstanceName="gvLupaKlientFurnitor"
                                        OnDataBound="gvLupaKlientFurnitor_DataBound" OnPreRender="gvLupaKlientFurnitor_PreRender"
                                        OnAfterPerformCallback="gvLupaKlientFurnitor_AfterPerformCallback" Width="98%" EnableCallbackCompression="true"
                                        ClientIDMode="AutoID" OnCustomCallback="gvLupaKlientFurnitor_CustomCallback" SettingsDataSecurity-AllowReadUnlistedFieldsFromClientApi="True">
                                        <ClientSideEvents Init="function(s, e){s.SetHeight(415); }" RowDblClick="function(s, e) { OnGridSelectionChanged(); }"
                                            BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                        <SettingsPager></SettingsPager>
                                        <Styles>
                                            <Header ImageSpacing="5px" Wrap="True" SortingImageSpacing="5px">
                                            </Header>
                                        </Styles>
                                        <StylesEditors>
                                            <CalendarHeader Spacing="1px">
                                            </CalendarHeader>
                                            <ProgressBar Height="25px">
                                            </ProgressBar>
                                        </StylesEditors>
                                    </dx:ASPxGridView>
                            
                <asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>   
                 <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                  ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                 </dx:ASPxComboBox>       
        </div>
    </form>
</body>
</html>
