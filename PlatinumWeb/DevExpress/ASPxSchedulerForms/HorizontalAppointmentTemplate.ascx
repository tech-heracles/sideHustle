<%@ Control Language="C#" AutoEventWireup="true" Inherits="HorizontalAppointmentTemplate" Codebehind="HorizontalAppointmentTemplate.ascx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<div id="appointmentDiv" runat="server" class='<%#((HorizontalAppointmentTemplateContainer)Container).Items.AppointmentStyle.CssClass %>'>
    <table style="width:100%; height:100%;" <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>>
        <tr>
            <td runat="server" id="statusContainer" style="vertical-align: top">    
            </td>
        </tr>
        <tr>
            <td>
            <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 0, 0) %>  style="width:100%; border-collapse:separate; padding-bottom:2px; padding-top:2px; padding-left:1px; padding-right:1px;">
                <tr class="dx-al" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "left", "middle") %> style="vertical-align: middle;">
                    <td>
                        <asp:Image runat="server" ID="imgStartContinueArrow" Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueArrow.Visible%>'></asp:Image>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblStartContinueText" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartContinueText.Visible%>'> </dx:ASPxLabel>
                    </td>
                    <td runat="server" id="startTimeClockContainer"> </td>
                    <td>
                       <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblStartTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.StartTimeText.Visible%>' ></dx:ASPxLabel>
                    </td>
                    <td class="dx-ac" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "center", null) %> style="width: 100%;">
                        <table <%= DevExpress.Web.Internal.RenderUtils.GetTableSpacings(this, 1, 1) %> style="border-collapse:separate; border-spacing: 1px; vertical-align: middle;">
                            <tr>
                                <td class="dxscCellWithPadding">
                                    <table id="imageContainer" runat="server" style="vertical-align: middle;">                                    
                                    </table>
                                </td>
                                <td class="dx-ac dxscCellWithPadding" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "center", null) %>>                            
                                     <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblTitle" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.Title.Text%>'></dx:ASPxLabel>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td runat="server" id="endTimeClockContainer"> 
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblEndTime" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndTimeText.Visible%>' ></dx:ASPxLabel>
                    </td>
                    <td>
                        <dx:ASPxLabel runat="server" EnableViewState="false" EncodeHtml="true" ID="lblEndContinueText" Text='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueText.Text%>' Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueText.Visible%>'></dx:ASPxLabel>
                    </td>
                    <td>
                        <asp:Image runat="server" ID="imgEndContinueArrow" Visible='<%#((HorizontalAppointmentTemplateContainer)Container).Items.EndContinueArrow.Visible%>'></asp:Image>
                    </td>
                </tr>
            </table>
            </td>
        </tr>
    </table>
</div>