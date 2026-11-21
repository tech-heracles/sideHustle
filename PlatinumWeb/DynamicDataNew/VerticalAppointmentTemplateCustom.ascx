<%@ Control Language="C#" AutoEventWireup="true" Inherits="VerticalAppointmentTemplateCustom" Codebehind="VerticalAppointmentTemplateCustom.ascx.cs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dxe" %>
<div id="appointmentDiv" runat="server" class='<%#((VerticalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>'>
    <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %> style="width: 100%">
        <tr <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, null, "top") %> style="vertical-align: top">
            <td runat="server" id="statusContainer">    
            </td>
            <td style="width:100%">
                <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 1, 0) %> style="width: 100%">
                    <tr <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, null, "top") %> style="vertical-align: top">
                        <td class="dxscCellWithPadding">
                            <table id="imageContainer" runat="server" style="text-align: center">
                                <tr><td class="dxscCellWithPadding"></td></tr>
                            </table>
                        </td>
                        <td class="dxscCellWithPadding" style="width:100%">                    
                            <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 1, 0) %> style="width: 100%">                        
                                <tr>
                                    <td class="dxscCellWithPadding">
                                        <dxe:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblStartTime" Text='<%#((VerticalAppointmentTemplateContainer)Container).Items.StartTimeText.Text%>' Visible='<%#((VerticalAppointmentTemplateContainer)Container).Items.StartTimeText.Visible%>'></dxe:ASPxLabel>
                                        <dxe:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" style="margin-left: -4px;" ID="lblEndTime" Text='<%#((VerticalAppointmentTemplateContainer)Container).Items.EndTimeText.Text%>' Visible='<%#((VerticalAppointmentTemplateContainer)Container).Items.EndTimeText.Visible%>'></dxe:ASPxLabel>        
                                        <dxe:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((VerticalAppointmentTemplateContainer)Container).Items.Title.Text%>'></dxe:ASPxLabel>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dxscCellWithPadding">
                                        <div runat="server" id="horizontalSeparator" class='<%#((VerticalAppointmentTemplateContainer)Container).Items.HorizontalSeparator.Style.CssClass %>' visible='<%#((VerticalAppointmentTemplateContainer)Container).Items.HorizontalSeparator.Visible%>'></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="dxscCellWithPadding">
                                        <dxe:ASPxLabel runat="server" Visible="true" EnableViewState="false"  EncodeHtml="true" ID="lblDescription" Text='<%#((VerticalAppointmentTemplateContainer)Container).Items.Description.Text%>'></dxe:ASPxLabel>
                                    </td>        
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
