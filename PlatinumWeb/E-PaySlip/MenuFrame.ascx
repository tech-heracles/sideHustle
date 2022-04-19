<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuFrame.ascx.cs" Inherits="PlatinumWeb.E_PaySlip.MenuFrame" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .style1 {
        height: 30px;
    }

    .style2 {
        height: 22px;
    }
</style>

<script type="text/javascript">
    function expandAll(s, e) {

        if (Utils.getUrlVar("vjenNga") == 'CRM') {
            return;
        }
        var splitpane0 = window.parent.splitter.GetPane(0);
        var splitpane1 = window.parent.splitter.GetPane(1);
        var splitpane4 = window.parent.splitter.GetPane(2);
        var splitpane2 = splitpane1.GetPane(0);
        var splitpane3 = splitpane1.GetPane(1);
        splitpane0.Collapse(splitpane1);
        splitpane4.Collapse(splitpane1);
        splitpane2.Collapse(splitpane3);
    }

    function collapseAll(s, e)
    {
        if (top.location.href != window.location.href)
        {
            var splitpane0 = window.parent.splitter.GetPane(0);
            var splitpane1 = window.parent.splitter.GetPane(1);
            var splitpane4 = window.parent.splitter.GetPane(2);
            var splitpane2 = splitpane1.GetPane(0);
            var splitpane3 = splitpane1.GetPane(1);
            splitpane0.Expand(splitpane1);
            splitpane4.Expand(splitpane1);
            splitpane2.Expand(splitpane3);
        }
        else
        {
            if (Utils.getUrlVar("vjenNga") == 'CRM')
            {
                window.location.href = "CRMDefault.aspx";
                return;
            }
            if (Utils.getUrlVar("vjenNga") == 'epayslip')
            {
                window.location.href = "ListaRaporte.aspx?vjenNga=Pini";
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
<table align="right">
    <tr>
        <td class="style2">
            <dx:ASPxButton ID="btnHelp" runat="server" Text="" ClientInstanceName="btnHelp" Width="40"
                Height="20" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                ImagePosition="Bottom" HorizontalAlign="Center" ToolTip="Ndihme"
                VerticalAlign="Middle">
                <Image Url="~/images/new/help_14.png">
                </Image>
            </dx:ASPxButton>
        </td>
        <td class="style2">
            <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="-" ClientInstanceName="btnCollapseAll"
                Width="40" Height="20" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True">
            </dx:ASPxButton>
        </td>
        <td class="style2">
            <dx:ASPxButton ID="btnExpandAll" runat="server" Text="+" ClientInstanceName="btnExpandAll"
                Width="40" Height="20" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                Font-Bold="True">
            </dx:ASPxButton>
        </td>
    </tr>
    <tr>
        <td class="style1" colspan="3"></td>
    </tr>
</table>