<%@ Control Language="C#" AutoEventWireup="true" Inherits="HorizontalSameDayAppointmentTemplate" Codebehind="HorizontalSameDayAppointmentTemplate.ascx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<div id="appointmentDiv" runat="server" class='<%#((HorizontalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>'>
    <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %> style="width: 100%">
        <tr>
            <td runat="server" id="statusContainer" style="vertical-align: top">    
            </td>
        </tr>
        <tr>
            <td>
                <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 1, 0) %> style="width: 100%">
                    <tr class="dx-al" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "left", "middle") %> style="vertical-align: middle;">
                        <td runat="server" id="startTimeClockContainer" class="dxscCellWithPadding"> 
                        </td>
                        <td class="dxscCellWithPadding">
                            <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblStartTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Visible%>'></dx:ASPxLabel>            
                        </td>
                        <td runat="server" id="endTimeClockContainer" class="dxscCellWithPadding">
                        </td>
                        <td class="dxscCellWithPadding">
                            <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblEndTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Visible%>'></dx:ASPxLabel>
                        </td>
                        <td class="dxscCellWithPadding">
                            <table id="imageContainer" runat="server" style="vertical-align: middle;">                            
                            </table>
                        </td>
                        <td class="dxscCellWithPadding" style="width: 100%">
                            <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.Title.Text%>'> </dx:ASPxLabel>            
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>