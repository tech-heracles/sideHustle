<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shto_KushtDergimi.aspx.cs"
    Inherits="PlatinumWeb.Shto_KushtDergimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/Shto_KushtDergimi.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body onload="changeName()">
    <form id="form1" runat="server">
         
    <div>
        <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
            <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
        </dx:ASPxGlobalEvents>
        <dxtc:ASPxPageControl EnableHierarchyRecreation="false" ID="ASPxPageControl1" runat="server"   TabSpacing="3px"
            Width="35%" ActiveTabIndex="0">
            <ContentStyle>
                <Border BorderColor="#AECAF0" BorderStyle="Solid" BorderWidth="1px" />
            </ContentStyle>
            <TabPages>
                <dxtc:TabPage Name="Te pergjithshme" Text="Te pergjithshme">
                    <ContentCollection>
                        <dxw:ContentControl>
                            <dx:ASPxGridView Width="94%" ID="grid_KushteDergimi" runat="server" OnAfterPerformCallback="grid_KushteDergimi_AfterPerformCallback"
                                OnRowInserting="grid_KushteDergimi_RowInserting" OnRowValidating="grid_KushteDergimi_RowValidating"
                                OnStartRowEditing="grid_KushteDergimi_StartRowEditing">
                                <Styles>
                                    <Header ImageSpacing="5px" SortingImageSpacing="5px">
                                    </Header>
                                </Styles>
                                <StylesEditors>
                                    <ProgressBar Height="25px">
                                    </ProgressBar>
                                </StylesEditors>
                            </dx:ASPxGridView>
                            <table width="94%">
                                <tr>
                                    <td>
                                        <dx:ASPxButton ID="Aspxbutton2" runat="server" Text="Ruaj" Width="100%" OnClick="ruaj_Button_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="Aspxbutton3" runat="server" Text="Pastro" Width="100%" OnClick="pastro_ASPxButton_Click">
                                        </dx:ASPxButton>
                                    </td>
                                    <td>
                                        <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Anullo" PostBackUrl="~/KushteDergimi.aspx"
                                            Width="100%">
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <dx:ASPxLabel ID="pergjigja" runat="server" Text="" ForeColor="Red">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </dxw:ContentControl>
                    </ContentCollection>
                </dxtc:TabPage>
            </TabPages>
        </dxtc:ASPxPageControl >
    </div>
    </form>
</body>
</html>