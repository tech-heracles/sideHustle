<%@ Control Language="C#" AutoEventWireup="true" Inherits="RecurrentAppointmentDeleteForm" Codebehind="RecurrentAppointmentDeleteForm.ascx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<table class="dxscBorderSpacing" style="width:100%; height:100%">
    <tr>
        <td class="dxscCellWithPadding" rowspan="2"  style="vertical-align:top;">
            <dx:ASPxImage id="imgWarning" runat="server" EnableViewState="False">
            </dx:ASPxImage>
        </td>
        <td class="dxscCellWithPadding" style="width:100%;">
            <dx:ASPxLabel ID="lblConfirm" runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="dxscCellWithPadding" style="width:100%;">
            <dx:ASPxRadioButtonList ID="rbAction" runat="server" ValueType="System.Int32">
                                <Border BorderStyle="None" />
                                <Items>
                                    <dx:ListEditItem Value="1" />
                                    <dx:ListEditItem Value="2" />
                                </Items>
                            </dx:ASPxRadioButtonList>
        </td>
    </tr>
    <tr>
        <td class="dx-ac dxscCellWithPadding" <%= DevExpress.Web.Internal.RenderUtils.GetAlignAttributes(this, "center", null) %> style="width:100%" colspan="2">
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
