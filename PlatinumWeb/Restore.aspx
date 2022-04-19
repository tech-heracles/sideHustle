<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Restore.aspx.cs" Inherits="PlatinumWeb.Restore" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>










<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
<%--    <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myButtonClickLupa-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/myFaqeCelje-IMB.2.1.js?versioni22" type="text/javascript"></script>
    <script src="js/aspx.js/Restore.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script>--%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Restore.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.    SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
    <div>
        <asp:UpdatePanel runat="server" ID="pnl">
            <ContentTemplate>
                <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                    ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                    OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
                    <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                    <SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
                    <ClientSideEvents ItemClick="function(s, e) {
                    Utils.shfaqLoadingGif();;
	                   ucFile.Upload();  
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
                  
                <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" ClientInstanceName="LoadingPanel"
                    Font-Size="9pt" Modal="True" ImagePosition="Top">
                    <LoadingDivStyle Opacity="30">
                    </LoadingDivStyle>
                </dx:ASPxLoadingPanel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hfStatus" runat="server" />
        <dx:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" ClientInstanceName="PageControl" runat="server"
              TabSpacing="3px" Width="100%" ActiveTabIndex="0" Height="520px">
            <Paddings Padding="2px" PaddingLeft="5px" PaddingRight="5px" />
            <TabPages>
                <dx:TabPage Name="Restore" Text="Restore">
                    <ContentCollection>
                        <dx:ContentControl ID="ContentControl1" runat="server">
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Zgjidhni skedarin e backupit per te ngarkuar"
                                class="klasePerLblKonfigurimi" Font-Bold="True" Font-Size="Medium">
                            </dx:ASPxLabel>
                            <br />
                            <br />
                            <table width="33%">
                                <tr>
                                    <td>
                                        <dx:ASPxUploadControl ID="ucFile" runat="server" ClientInstanceName="ucFile" Width="100%" OnFileUploadComplete="ucFile_FileUploadComplete">
                                            <ClientSideEvents FileUploadComplete="function(s, e) { kot(s,e);
                                            Utils.hiqLoadingGif();;
	
}" />
                                        </dx:ASPxUploadControl>
                                    </td>
                                </tr>
                            </table>
                        </dx:ContentControl>
                    </ContentCollection>
                </dx:TabPage>
            </TabPages>
        </dx:ASPxPageControl >
    </div>
    </form>
</body>
</html>
