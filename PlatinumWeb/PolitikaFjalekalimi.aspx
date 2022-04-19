<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PolitikaFjalekalimi.aspx.cs" Inherits="PlatinumWeb.PolitikaFjalekalimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/PolitikaFjalekalimi.aspx-IMB.2.1.js&v76""
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
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick">
                                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                <ClientSideEvents  Init="function(s) {s.SetClientVisible(true);}" />
                                <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="pnlKonfigurimi" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <br />
                <dx:ASPxFormLayout ID="formLayout" runat="server" AlignItemCaptionsInAllGroups="True" Width="70%">
                    <Items>
                        <dx:LayoutGroup Caption="Historiku i Fjalëkalimit" ColCount="3" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="32%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbRuajHistorikun" runat="server" ClientInstanceName="cbRuajHistorikun"
                                                TextSpacing="2px" Text="Ruaj historikun e fjalëkalimit">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                                <ClientSideEvents CheckedChanged="function(s, e) { ndryshoHistorikun(s, e); }" />
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="70px" HorizontalAlign="Right" Caption="Ruaj historikun për">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="spinDiteHistoriku" ClientInstanceName="spinDiteHistoriku" Width="50px" MinValue="1" MaxValue="9999" runat="server" Height="21px"></dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblHeretEFundit1" runat="server" AssociatedControlID="cbRuajHistorikun"
                                                ClientInstanceName="lblHeretEFundit" Text="fjalëkalimet e fundit">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup Caption="Politikat e fjalëkalimit" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" HelpText="Fjalëkalimi duhet të përmbajë numra, karaktere speciale, shkronjë të vogël, shkronjë të madhe.">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbPassKompleks" runat="server" ClientInstanceName="cbPassKompleks"
                                                TextSpacing="2px" Width="100%" Text="Fjalëkalim kompleks">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbResetPass" runat="server" ClientInstanceName="cbResetPass"
                                                TextSpacing="2px" Text="Lejo përdoruesin të resetojë fjalëkalimin" Height="18px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                  <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cb2fact" runat="server" ClientInstanceName="cb2fact"
                                                TextSpacing="2px" Text="2-factor auth" Height="18px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup ShowCaption="False" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" HelpText="Përdoruesi duhet të ndryshojë fjalëkalimin në logimin e radhës">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbPassDetyrueshem" runat="server" ClientInstanceName="cbResetPass"
                                                TextSpacing="2px" Text="Ndryshim fjalëkalimi i detyrueshëm" Height="18px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup ShowCaption="False" ColCount="4" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="53%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbPassSkadon" runat="server" ClientInstanceName="cbPassSkadon"
                                                TextSpacing="2px" Text="Skado fjalëkalimin" Width="300px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                                <ClientSideEvents CheckedChanged="function(s, e) { ndryshoSkadiminPass(s, e); }" />

                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="70px" HorizontalAlign="Right" Caption="Fjalëkalimi skadon pas">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="sePassSkadonPas" ClientInstanceName="sePassSkadonPas" MinValue="1" MaxValue="9999" runat="server" Width="50px" Height="21px" Number="45"></dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Width="300px" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" HorizontalAlign="Left" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblDiteSkadence" runat="server"
                                                ClientInstanceName="lblDiteSkadence" Text="ditësh.">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup ShowCaption="False" ColCount="4" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="230px" HorizontalAlign="Right" Caption="Gjatësia minimale e fjalëkalimit">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seGjatesiMin" MinValue="1" MaxValue="1000" runat="server" Width="50px" Height="21px">
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Width="300px" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblKaraktere" runat="server"
                                                ClientInstanceName="lblKaraktere" HorizontalAlign="Left" Text="karaktere.">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                         <dx:LayoutGroup ShowCaption="False" ColCount="4" GroupBoxDecoration="None">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="230px" HorizontalAlign="Right" Caption="Numri i karaktereve speciale">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seNrKaraktereSpeciale" MinValue="1" MaxValue="1000" runat="server" Width="50px" Height="21px">
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                 <dx:LayoutItem ShowCaption="False" Width="300px" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>                      
                          <dx:LayoutGroup ShowCaption="False" ColCount="4" GroupBoxDecoration="None">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="230px" HorizontalAlign="Right" Caption="Numri i karaktereve kapitale">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seNrShkronjaveKapitale" MinValue="1" MaxValue="1000" runat="server" Width="50px" Height="21px">
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                 <dx:LayoutItem ShowCaption="False" Width="300px" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                        <dx:LayoutGroup ShowCaption="False" ColCount="4" GroupBoxDecoration="None">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="100%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="230px" HorizontalAlign="Right" Caption="Numri i karaktereve numra">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seNrKaraktereveNumra" MinValue="1" MaxValue="1000" runat="server" Width="50px" Height="21px">
                                            </dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                 <dx:LayoutItem ShowCaption="False" Width="300px" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                 
                               <dx:LayoutGroup Caption="Bllokimi i përdoruesit" ColCount="4" RowSpan="3" Width="45%" GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutItem ShowCaption="False" Width="45%">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbBllokoUser" runat="server" ClientInstanceName="cbBllokoUser"
                                                TextSpacing="2px" Text="Blloko përdorues">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                                <ClientSideEvents CheckedChanged="function(s, e) { ndryshoBllokiminUserit(s, e); }" />
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Caption="Blloko pas" Width="50px" HorizontalAlign="Right">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seBllokoPas" ClientInstanceName="seBllokoPas" MinValue="1" MaxValue="1000" runat="server" Width="50px" Height="21px"></dx:ASPxSpinEdit>
                                            <br />
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblTentativa" runat="server"
                                                ClientInstanceName="lblTentativa" Text="tentativash për 1 sesion">
                                            </dx:ASPxLabel>
                                            <br />
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" HorizontalAlign="Left">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbGjeneroPassword" runat="server" ClientInstanceName="cbGjeneroPassword"
                                                TextSpacing="2px" Text="Gjenero fjalekalim automatik" Width="230px" Height="18px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem Width="170px" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Width="50px" HorizontalAlign="Right">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxSpinEdit ID="seMaxSession" ClientInstanceName="seMaxSession" Style="margin: 0 auto" MinValue="1" MaxValue="500" runat="server" Width="50px" Height="21px"></dx:ASPxSpinEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel Wrap="False" ID="lblNrSession" runat="server"
                                                ClientInstanceName="lblNrSession" Text="sesione">
                                            </dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                 <dx:LayoutItem Width="170px" ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" HorizontalAlign="Left" Width="10%" >
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                            <dx:ASPxCheckBox ID="cbBllokoLogimin" runat="server" ClientInstanceName="cbBllokoLogimin"
                                                TextSpacing="2px" Text="Blloko logimin më shumë se një herë" Width="230px" Height="18px">
                                                <DisabledStyle Font-Bold="False">
                                                </DisabledStyle>
                                            </dx:ASPxCheckBox>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:LayoutGroup>
                    </Items>
                </dx:ASPxFormLayout>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
