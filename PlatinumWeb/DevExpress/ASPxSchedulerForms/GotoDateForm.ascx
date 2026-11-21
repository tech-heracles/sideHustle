
<%@ Control Language="C#" AutoEventWireup="true" Inherits="GotoDateForm" Codebehind="GotoDateForm.ascx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
    
<table class="dxscBorderSpacing" style="width:100%; height:100%">
    <tr>
        <td class="dxscCellWithPadding">
            <dx:ASPxLabel ID="lblDate" runat="server" AssociatedControlID="edtDate" />
        </td>
        <td class="dxscCellWithPadding" style="width:100%">
            <dx:ASPxDateEdit ClientInstanceName="_dx" ID="edtDate" runat="server" Width="100%" Date='<%#((GotoDateFormTemplateContainer)Container).Date %>' />
        </td> 
    </tr>
    <tr>
        <td class="dxscCellWithPadding">
            <span style="white-space: nowrap;">
            <dx:ASPxLabel ID="lblView" runat="server" AssociatedControlID="cbView"></dx:ASPxLabel>
            </span>
        </td>
        <td class="dxscCellWithPadding" style="width:100%">
            <dx:ASPxComboBox ClientInstanceName="_dx" ID="cbView" runat="server" Width="100%" DataSource='<%#((GotoDateFormTemplateContainer)Container).ViewsDataSource %>'></dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <td class="dx-ac dxscCellWithPadding" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "center", null) %> colspan="2" style="width: 100%">
            <table class="dxscButtonTable">
                <tr>
                    <td class="dxscCellWithPadding">
                        <dx:ASPxButton ID="btnOk" ClientInstanceName="_dx" runat="server" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" Width="91px" />
                    </td>
                    <td class="dxscCellWithPadding">
                        <dx:ASPxButton ID="btnCancel" ClientInstanceName="_dx" runat="server" UseSubmitBehavior="false" AutoPostBack="false" EnableViewState="false" 
                            Width="91px" CausesValidation="False" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>    


