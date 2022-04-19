<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaPunonjes.aspx.cs" Inherits="PlatinumWeb.LupaPunonjes" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1 {
            height: 54px;
        }
    </style>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/ListPagesaUtils.js;~/js/aspx.js/LupaPunonjes.aspx-IMB.2.1.js&v76"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
         
        <dx:ASPxLoadingPanel ID="LoadingPanel" ClientInstanceName="LoadingPanel" runat="server"></dx:ASPxLoadingPanel>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.  window.parent.  SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
        <div id='div1' style="display: none">
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
                ShowHeader="False">
                <PanelCollection>
                    <dx:PanelContent>
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                        ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                       >
                                        <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                                        <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                                        <ClientSideEvents ItemClick="LupaPunonjes.handlers.menuClick" Init="function(s) {s.SetClientVisible(true);}" />
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
                        <asp:UpdatePanel ID="pnlKryesor" runat="server">
                            <ContentTemplate>
                                <div id="div" style="display:inline-block">
                                    
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen"
                                                    AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) { gvLupaPunonjes.SelectAllRowsOnPage(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td>
                                                <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe"
                                                    AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) { gvLupaPunonjes.SelectRows(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="padding-right: 10px">
                                                <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen"
                                                    AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
                                                    <ClientSideEvents Click="function(s, e) { gvLupaPunonjes.UnselectRows(); }" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td style="padding-right: 10px">
                                                <dx:ASPxCheckBox ID="cbApliko" runat="server" CheckState="Unchecked" ClientInstanceName="cbApliko"
                                                    TextSpacing="2px">
                                                    <ClientSideEvents CheckedChanged="LupaPunonjes.handlers.cbAplikoCheckedChanged" />
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td>
                                                <dx:ASPxLabel ID="lblApliko" runat="server" ClientInstanceName="lblApliko" Text="Apliko te njejtin nr ditesh per te selektuarit"
                                                    Width="150px" Wrap="True">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 70px">
                                                <dx:ASPxLabel ID="lblNrDitesh" runat="server" ClientInstanceName="lblNrDitesh" Text="Nr. Ditesh">
                                                </dx:ASPxLabel>
                                            </td>
                                            <td style="width: 170px">
                                                <dx:ASPxTextBox ID="txtNrDitesh" runat="server" ClientEnabled="False" ClientInstanceName="txtNrDitesh"
                                                    Width="170px">
                                                    <ValidationSettings>
                                                        <ErrorFrameStyle ImageSpacing="4px">
                                                            <ErrorTextPaddings PaddingLeft="4px" />
                                                        </ErrorFrameStyle>
                                                        <RegularExpression ErrorText="Nr i diteve duhet te jete numer!" ValidationExpression="[0-9.,]*" />
                                                    </ValidationSettings>
                                                    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
                                                    </DisabledStyle>
                                                </dx:ASPxTextBox>
                                            </td>
                                          </div>
                                        </tr>
                                    </table>
                                </div>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvLupaPunonjes" runat="server" ClientInstanceName="gvLupaPunonjes"
                                                OnDataBound="gvLupaPunonjes_DataBound" OnAfterPerformCallback="gvLupaPunonjes_AfterPerformCallback"
                                                Width="100%" OnCustomCallback="gvLupaPunonjes_CustomCallback">
                                                <ClientSideEvents Init="function(s, e){s.SetHeight(320); }" RowDblClick="LupaPunonjes.handlers.onGridSelectionChanged" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                                <Styles>
                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                    </Header>
                                                </Styles>
                                                <StylesEditors>
                                                    <ProgressBar Height="25px">
                                                    </ProgressBar>
                                                </StylesEditors>
                                                <SettingsPager></SettingsPager>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                           <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                           </dx:ASPxComboBox>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel >
            <asp:HiddenField ID="hfKompFill" runat="server" />
        </div>
    </form>
</body>
</html>
