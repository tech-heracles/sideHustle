<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreviewThemeAmbjente.aspx.cs"
    Inherits="PlatinumWeb.PreviewThemeAmbjente" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>







<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>



<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Preview</title>
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
        id="themeJQuery" />
  <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/jqGrid445/grid.locale-en.js;~/js/jqGrid445/jquery.jqGrid.min.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/PreviewThemeAmbjente.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
    <div id="backDiv" runat="server">
        <dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter1" runat="server" Height="700px" Width="950px" BackColor="Transparent">
            <Panes>
                <dx:SplitterPane Size="10%" MinSize="200px" ShowCollapseBackwardButton="True">
                    <PaneStyle BackColor="Transparent">
                    </PaneStyle>
                    <Separators Size="10px">
                    </Separators>
                    <ContentCollection>
                        <dx:SplitterContentControl ID="SplitterContentControl2" runat="server">
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 100%; height: 100%;
                                vertical-align: top;">
                                <tr>
                                    <td align="center" valign="top">
                                        <dx:ASPxNavBar ID="ASPxNavBar1" runat="server" ClientInstanceName="navbar" GroupSpacing="0px"
                                            Width="100%" ClientIDMode="AutoID" AutoCollapse="True" EnableAnimation="True"
                                             SyncSelectionMode="CurrentPath" AutoPostBack="False"
                                            EnableClientSideAPI="True" AllowSelectItem="True">
                                            <Groups>
                                                <dx:NavBarGroup Text="Administrimi">
                                                    <Items>
                                                        <dx:NavBarItem Text="Ndërmarrjet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Degët Administrative">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Përdoruesit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Ndryshim fjalëkalimi">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Rolet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Backup">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Restore">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Hyrjet/daljet në program">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Autorizimet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Fushat Shtesë">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Motivet">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Kontabiliteti" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Strukturat e llogarive">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Llogaritë">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Ambjenti i kontabilizimit">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Inventari" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Njësitë matëse" Name="NjesiArtikulli.aspx" Visible="false">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Grupet e artikujve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Detajime">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Artikujt">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Artikujt afatgjatë">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Magazinat">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Njesi Vartese">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Dokumentat e hyrjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimet e hyrjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Dokumentat e daljeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimet e daljeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Rivlerësimi i inventarit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Shpërndarja e shpenzimeve" Visible="false">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Blerjet dhe shitjet" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Çmimet e Shitjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Afatet e Maturimit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kategoritë e zbritjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Llojet e transportit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kusht  dergimi">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kusht  pagese">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Grupet e klientëve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Grupet e furnitorëve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Klientët">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Furnitorët">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Pikat e shitjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Pikat e furnizimit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Çelje agjent shitje">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Faturat e blerjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimet e blerjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Faturat e shitjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimet e shitjeve">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Arka dhe banka" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Çelja e arkave">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Çelja e bankave">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Derdhjet bankare">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimi i derdhjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Tërheqjet bankare">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimi i tërheqjeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Arkëtimet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimi i arkëtimeve">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Pagesat">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Regjistrimi i pagesave">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Lidhja e dokumentave">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Grupe">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Tituj">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kapituj">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Urdhër pagesa">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Urdhër pagese e re">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Burimet Njerezore" Name="" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Struktura Administrative">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Komponente page">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Komponente listëpagese">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Sigurimet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Tatime mbi pagën">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kategori Page">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Shtesa Page">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Sigurime Suplementare">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Punonjës">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="List pagesa">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="List pagese e re">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Raportet" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Kontabiliteti">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Blerjet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Shitjet">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Inventar">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Klientët dhe furnitorët">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Arka dhe Banka">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Burimet Njerëzore">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Kubi">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                                <dx:NavBarGroup Text="Help" Expanded="False">
                                                    <Items>
                                                        <dx:NavBarItem Text="Manuali Perdoruesit">
                                                        </dx:NavBarItem>
                                                        <dx:NavBarItem Text="Remote Support">
                                                        </dx:NavBarItem>
                                                    </Items>
                                                </dx:NavBarGroup>
                                            </Groups>
                                            <GroupContentStyle ItemSpacing="1px">
                                            </GroupContentStyle>
                                            <CollapseImage Height="0px" Width="0px">
                                            </CollapseImage>
                                            <ExpandImage Height="0px" Width="0px">
                                            </ExpandImage>
                                        </dx:ASPxNavBar>
                                    </td>
                                </tr>
                            </table>
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
                <dx:SplitterPane>
                    <Panes>
                        <dx:SplitterPane Name="paneKryesor" ScrollBars="Vertical">
                            <PaneStyle BackColor="White">
                            </PaneStyle>
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl3" runat="server">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" ItemImagePosition="Top"
                                                    Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                                    Theme="Default">
                                                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                                    <ClientSideEvents Init="function(s) {s.SetClientVisible(true);}" />
                                                    <ItemImage Height="32px" Width="32px">
                                                    </ItemImage>
                                                    <SubMenuItemImage Height="16px" Width="16px">
                                                    </SubMenuItemImage>
                                                    <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
                                                        <Paddings PaddingBottom="1px" PaddingTop="9px" />
                                                    </ItemStyle>
                                                    <SubMenuItemStyle Width="32px">
                                                    </SubMenuItemStyle>
                                                    <Items>
                                                        <dx:MenuItem Text="Shto">
                                                            <Image Url="~/images/new/add.png">
                                                            </Image>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Ruaj">
                                                            <Image Url="~/images/new/disk_blue.png">
                                                            </Image>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Modifiko">
                                                            <Image Url="~/images/new/ndrysho.png">
                                                            </Image>
                                                        </dx:MenuItem>
                                                        <dx:MenuItem Text="Fshi">
                                                            <Image Url="~/images/new/stop.png">
                                                            </Image>
                                                        </dx:MenuItem>
                                                    </Items>
                                                </dx:ASPxMenu>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl2" runat="server" Width="100%" Height="100%"
                                                    TabSpacing="3px">
                                                    <TabPages>
                                                        <dx:TabPage Name="Ambjenti Kryesor" Text="Ambjenti Kryesor">
                                                            <ContentCollection>
                                                                <dxw:ContentControl ID="content" runat="server">
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <dx:ASPxGridView runat="server" ID="grida" Width="100%">
                                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True" />
                                                                                    <SettingsBehavior AllowFocusedRow="True" AllowSelectByRowClick="True"></SettingsBehavior>
                                                                                    <SettingsPager AlwaysShowPager="True">
                                                                                    </SettingsPager>
                                                                                    <Settings ShowFilterRow="true" ShowFilterBar="Visible" ShowFilterRowMenu="True" ShowGroupPanel="True"
                                                                                        ShowHeaderFilterButton="True" />
                                                                                    <Settings ShowFilterRow="True" ShowFilterRowMenu="True" ShowHeaderFilterButton="True"
                                                                                        ShowGroupPanel="True" ShowFilterBar="Visible"></Settings>
                                                                                    <Styles>
                                                                                        <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                                                        </Header>
                                                                                    </Styles>
                                                                                    <StylesEditors>
                                                                                        <CalendarHeader Spacing="1px">
                                                                                        </CalendarHeader>
                                                                                        <ProgressBar Height="25px">
                                                                                        </ProgressBar>
                                                                                    </StylesEditors>
                                                                                </dx:ASPxGridView>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                        <dx:TabPage Name="Gridat e Regjistrimeve" Text="Gridat e Regjistrimeve">
                                                            <ContentCollection>
                                                                <dxw:ContentControl>
                                                                    <div id="divgride2">
                                                                        <table id="rowed5">
                                                                        </table>
                                                                    </div>
                                                                </dxw:ContentControl>
                                                            </ContentCollection>
                                                        </dx:TabPage>
                                                    </TabPages>
                                                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
                                                    <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px"></Paddings>
                                                    <ContentStyle>
                                                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px"></Border>
                                                    </ContentStyle>
                                                </dx:ASPxPageControl >
                                            </td>
                                        </tr>
                                    </table>
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                    <ContentCollection>
                        <dx:SplitterContentControl runat="server" SupportsDisabledAttribute="True">
                        </dx:SplitterContentControl>
                    </ContentCollection>
                </dx:SplitterPane>
            </Panes>
        </dx:ASPxSplitter >
    </div>
    </form>
</body>
</html>
