<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuFrame.ascx.cs" Inherits="PlatinumWeb.MenuFrame" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .menuFrame td {
        height: 30px;
    }
</style>

<script type="text/javascript">
    function expandAll(s, e) {

        if (Utils.getUrlVar("vjenNga") == 'CRM' || Utils.getUrlVar("vjenNga") == 'epayslip') {
            return;
        }
        var splitpane0 = window.parent.splitter.GetPane(0);
        var splitpane1 = window.parent.splitter.GetPane(1);
        var splitpane4 = window.parent.splitter.GetPane(2);
        var splitpane2 = splitpane1.GetPane(0);
        var splitpane3 = splitpane1.GetPane(1);
        if (splitpane0 == null || splitpane1 == null || splitpane2 == null || splitpane3 == null || splitpane4 == null) return;
        splitpane0.Collapse(splitpane1);
        splitpane4.Collapse(splitpane1);
        splitpane2.Collapse(splitpane3);
    }

    function collapseAll(s, e) {
        if (top.location.href != window.location.href) {
            var splitpane0 = window.parent.splitter.GetPane(0);
            var splitpane1 = window.parent.splitter.GetPane(1);
            var splitpane4 = window.parent.splitter.GetPane(2);
            var splitpane2 = splitpane1.GetPane(0);
            var splitpane3 = splitpane1.GetPane(1);
            if (splitpane0 == null || splitpane1 == null || splitpane2 == null || splitpane3 == null || splitpane4 == null) return;
            splitpane0.Expand(splitpane1);
            splitpane4.Expand(splitpane1);
            splitpane2.Expand(splitpane3);
        }
        else {
            if (Utils.getUrlVar("vjenNga") == 'CRM') {
                window.location.href = "CRMDefault.aspx";
                return;
            }
            window.location.href = "FaqeKryesore.aspx";
        }
    }
    function openHelpWindow(s, e, helpUrl) {
        window.open(helpUrl, "_blank");
    }

</script>
<dx:ASPxHiddenField ID="hfIdGjuha" runat="server"></dx:ASPxHiddenField>

<table class="menuFrame" style="float: right">
    <tr>
        <td>
            <dx:ASPxButton ID="btnHelp" runat="server" Text="" ClientInstanceName="btnHelp" Width="40"
                Height="20px" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                ImagePosition="Bottom" HorizontalAlign="Center" ToolTip="Ndihme"
                VerticalAlign="Middle">
                <Image Url="images/new/help_14.png" UrlHottracked="images/new/help_14_W.png">
                </Image>
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="" ClientInstanceName="btnCollapseAll"
                Width="40" Height="20px" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True" ToolTip="Minimizo">
                <Image Url="images/new/reg_screen.png" UrlHottracked="images/new/reg_screen_W.png">
                </Image>
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="" ClientInstanceName="btnExpandAll"
                Width="40" Height="20px" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True" ToolTip="Maksimizo">
                <Image Url="images/new/full_screen.png" UrlHottracked="images/new/full_screen_W.png">
                </Image>
            </dx:ASPxButton>
        </td>
    </tr>
    <tr>
        <td></td>
        <td>
            <dx:ASPxButton ID="btnPara" runat="server" Text="<" Enabled="false" Visible="false" ClientInstanceName="btnPara"
                Width="40" Height="20px" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True">
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnMbrapa" runat="server" Text=">" Enabled="false" Visible="false" ClientInstanceName="btnMbrapa"
                Width="40" Height="20px" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True">
            </dx:ASPxButton>
        </td>
    </tr>
</table>
