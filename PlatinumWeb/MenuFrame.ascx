<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuFrame.ascx.cs" Inherits="PlatinumWeb.MenuFrame" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .menuFrame td {
        height: 30px;
    }
</style>

<script type="text/javascript">
    // FaqeKryesore ka vetem dy panele (shiriti lart dhe faqja); footer-i dhe paneli majtas nuk ekzistojne me
    function panelet() {
        var splitter = window.parent && window.parent.splitter;
        if (!splitter) return null;
        var lart = splitter.GetPane(0), faqja = splitter.GetPane(1);
        return lart && faqja ? { lart: lart, faqja: faqja } : null;
    }

    function expandAll(s, e) {
        if (Utils.getUrlVar("vjenNga") == 'epayslip')
            return;
        var p = panelet();
        if (p) p.lart.Collapse(p.faqja);
    }

    function collapseAll(s, e) {
        if (top.location.href != window.location.href) {
            var p = panelet();
            if (p) p.lart.Expand(p.faqja);
        }
        else {
            window.location.href = "FaqeKryesore.aspx";
        }
    }

</script>
<dx:ASPxHiddenField ID="hfIdGjuha" runat="server"></dx:ASPxHiddenField>

<table class="menuFrame" style="float: right">
    <tr>
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
