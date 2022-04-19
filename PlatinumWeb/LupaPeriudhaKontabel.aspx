<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaPeriudhaKontabel.aspx.cs"
    Inherits="PlatinumWeb.LupaPeriudhaKontabel" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcb" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaPeriudhaKontabel.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="Init()" style="background-color: #EDF3F4">
    <form id="form1" runat="server">
           
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="JsGlobal.js"></script>
    <div>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.  window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
            ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <dx:ASPxLabel ID="lblViti" runat="server" Text="Viti aktual" ClientInstanceName="lblViti">
                    </dx:ASPxLabel>
                    <dx:ASPxComboBox ID="cmbVitiAktual" runat="server" ShowShadow="False" ClientInstanceName="cmbVitiAktual"
                        OnSelectedIndexChanged="cmbVitiAktual_OnSelectedIndexChanged" SettingsLoadingPanel-ImagePosition="Top">
                        <LoadingPanelImage>
                        </LoadingPanelImage>
                        <DropDownButton>
                            <Image>
                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                            </Image>
                        </DropDownButton>
                        <ValidationSettings>
                            <ErrorFrameStyle ImageSpacing="4px">
                                <ErrorTextPaddings PaddingLeft="4px" />
                                <ErrorTextPaddings PaddingLeft="4px"></ErrorTextPaddings>
                            </ErrorFrameStyle>
                        </ValidationSettings>
                    </dx:ASPxComboBox>
                    <table width="100%">
                        <tr>
                            <td>
                                <dx:ASPxGridView ID="gvLupaPerKont" runat="server" ClientInstanceName="gvLupaPerKont"
                                    OnDataBound="gvLupaPerKont_DataBound" OnAfterPerformCallback="gvLupaPerKont_AfterPerformCallback"
                                    Width="100%" EnableCallBacks="False" OnHtmlRowCreated="gvLupaPerKont_HtmlRowCreated">
                                    <ClientSideEvents RowDblClick="function(s, e) {
                                         OnGridSelectionChanged(s,e);                                      
                                         window.parent.popupUniversal.Hide();
                                        }"></ClientSideEvents>
                                    <ClientSideEvents RowDblClick="function(s, e) {
                                         OnGridSelectionChanged();                                      
                                        }" />
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
                        <tr>
                            <td align="center" class="style1">
                                <table>
                                    <tr>
                                        <td>
                                            <dx:ASPxButton ID="btnOk" runat="server" Text="Ok" AutoPostBack="false" AllowFocus="true"
                                                ClientInstanceName="btnOk">
                                                <ClientSideEvents Click="function(s, e) {
                                                             OnGridSelectionChanged(); 
                                                             window.parent.popupUniversal.Hide();                                   
                                    }"></ClientSideEvents>
                                            </dx:ASPxButton>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
</html>
