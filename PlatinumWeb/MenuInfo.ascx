<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuInfo.ascx.cs" Inherits="PlatinumWeb.MenuInfo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<script src="js/myMesazh-IMB.2.1.js?versioni22" type="text/javascript"></script>
<%-- --%>
<script src="js/aspx.js/MenuInfo.ascx-IMB.2.1.js?versioni22" type="text/javascript"></script>
<table style="width:100%; background-color: #F3F3F3">
    <tr>
        <%--        <td>
            <dx:ASPxImage ID="ASPxImage1" runat="server" ImageUrl="~/images/new/about.png" ClientIDMode="Static">
            </dx:ASPxImage>
         
        </td>--%>
        <%--<dx:ASPxLabel ID="mesazhPage" runat="server" Text="" ClientInstanceName="mesazhPage"
                Font-Size="Medium" ClientIDMode="Static" Wrap="False">
            </dx:ASPxLabel> --%>
        <td id="mesazhi" style="width: auto;">
            <dx:ASPxComboBox ID="mesazhList" ClientVisible="false" ClientEnabled="true" ShowImageInEditBox="true"
                DropDownButton-Visible="false" DropDownButton-Image-UrlHottracked="~/images/new/about.png"
                ItemImage-Height="22px" ItemImage-Width="22px" DropDownButton-Image-UrlPressed="~/images/new/about.png"
                DropDownButton-Image-Url="~/images/new/about.png" Width="100%" BackColor="transparent"
                ClientInstanceName="mesazhList" runat="server" EnableClientSideAPI="True" ItemStyle-Wrap="False"
                ValueType="System.Int32" Border-BorderStyle="None" DropDownButton-Position="Left"
                DisabledStyle-ForeColor="Black" Style="margin-bottom: 0px" 
                oniteminserted="mesazhList_ItemInserted">
                <ClientSideEvents SelectedIndexChanged="function(s, e){mesazhListSelectedIndexChanged(s, e); mesazhList.ShowDropDown();}" />
                <ItemImage Height="22px" Width="22px">
                </ItemImage>
                <ItemStyle Wrap="False"></ItemStyle>
                <DropDownButton Position="Left" Visible="False">
                    <Image UrlHottracked="~/images/new/about.png" UrlPressed="~/images/new/about.png"
                        Url="~/images/new/about.png">
                    </Image>
                </DropDownButton>
                <Border BorderStyle="None"></Border>
                <DisabledStyle ForeColor="Black" BackColor="Transparent">
                    <Border BorderStyle="None" />
                </DisabledStyle>
            </dx:ASPxComboBox>
        </td>
        <td align="left">
            <table>
                <tr>
                    <td style="width: auto;">
                        <dx:ASPxButton ID="btnPo" ClientVisible="false" runat="server" CausesValidation="False" ClientInstanceName="btnPo"
                            Text="Po"      
                            ClientIDMode="AutoID" Width="30px">
                            <%--<ClientSideEvents Click="function(s, e) { poBtnClick(s, e); }" Init="function(s, e) { poBtnInit(s, e); }" />--%>
                        </dx:ASPxButton>
                    </td>
                    <td style="width:5px;">
                    </td>
                    <td style="width: auto;">
                        <dx:ASPxButton ID="btnJo" ClientVisible="false" runat="server" CausesValidation="False" ClientInstanceName="btnJo"
                            Text="Jo"      
                            ClientIDMode="AutoID" Width="30px">
                            <%--<ClientSideEvents Click="function(s, e) {joBtnClick(s, e);}" Init="function(s, e) { joBtnInit(s, e); }" />--%>
                        </dx:ASPxButton>
                    </td>
                </tr>
            </table>
        </td>
        <td align="right" valign="top" style="width: auto;">
            <dx:ASPxHyperLink ID="hlClose"  runat="server" Text="x" Cursor="pointer" ClientInstanceName="hlClose"
                ClientVisible="False" ClientIDMode="AutoID" ToolTip="Anullo">
                <ClientSideEvents Click="function(s, e) { hlCloseClick(s, e); }" />
            </dx:ASPxHyperLink>
        </td>
    </tr>
</table>
<%--timer.SetEnabled(false);--%>
<%--<dx:ASPxTimer ID="timer" runat="server" ClientInstanceName="timer" Interval="10000">
    <ClientSideEvents Tick="   function (s,e){mesazhPage.SetText(); timer.SetEnabled(false); btnPo.SetVisible(false); btnJo.SetVisible(false);hlClose.SetVisible(false);}"
        Init="function (s,e){timer.SetEnabled(false);}" />
</dx:ASPxTimer>--%>
