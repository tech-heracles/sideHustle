<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaShfaqGrupimPerberes.aspx.cs" Inherits="PlatinumWeb.LupaShfaqGrupimPerberes"
    EnableEventValidation="false" ValidateRequest="false" ViewStateEncryptionMode="Never" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>




<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alpha Web</title>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaShfaqGrupimPerberes.aspx-IMB.5.7.js;~/js/myButtonClickLupa-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
        <script language="javascript" type="text/javascript">       
        </script>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
     </asp:ScriptManager>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){
          if (window.parent.identikuesPerPopupArtikulli == 'ArtikullPerberes')   
              window.parent.window.parent.window.parent.SessionTimeout.sendKeepAlive();
              else
                window.parent.window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dx:ASPxHiddenField ID="hfState" runat="server">
        </dx:ASPxHiddenField>

        <div id="dvArtikulli" style="display: none">
            <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" HeaderText=""  >
                <PanelCollection>
                    <dx:PanelContent>
                        <table id="tblInformacion" class="CustomRenditKontrolleDy">
                            <tbody>
                            </tbody>
                        </table>
         
                        <asp:UpdatePanel ID="pnlKryesor" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <dx:ASPxGridView ID="gvLupaGrupimPerberes" runat="server" ClientInstanceName="gvLupaGrupimPerberes"
                                                OnDataBound="gvLupaGrupimPerberes_DataBound" 
                                                EnableCallbackCompression="True" EnableCallBacks="true"
                                                Width="100%"  >
                                                <SettingsPager ></SettingsPager>
                                                <StylesPager Summary-Width="100%" PageNumber-Width="100%">
                                                </StylesPager>
                                                <ClientSideEvents 
                                                    BeginCallback="function(s, e) { BeginCallback(s,e); }" />
                                                <Styles>
                                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                                    </Header>
                                                </Styles>
                                                <StylesEditors>
                                                    <ProgressBar Height="100%">
                                                    </ProgressBar>
                                                </StylesEditors>
                                                <SettingsPager></SettingsPager>
                                            </dx:ASPxGridView>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel >
        </div>
        <asp:HiddenField ID="hfLidhur" runat="server" />
        <asp:HiddenField ID="hfKontrollet" runat="server" />
        <asp:HiddenField ID="hfShtimModifikim" runat="server" />
        <asp:HiddenField ID="hfLupaMagazina" runat="server" />
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
            AutoUpdatePosition="True" Font-Bold="False">
            <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl(''); }" />
            <ContentStyle>
                <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                    PaddingTop="1px" />
            </ContentStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
    </form>
</body>
</html>
