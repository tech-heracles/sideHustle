
<%@ Control Language="C#" AutoEventWireup="true" Inherits="InplaceEditor" Codebehind="InplaceEditor.ascx.cs" %>

<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler" TagPrefix="dxwschs" %>
<%@ Register Assembly="DevExpress.Web.ASPxScheduler.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxScheduler.Controls" TagPrefix="dxsc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<table runat="server" id="mainContainer" style="width:100%; height:100%">
    <tr>
        <td class="dx-p2" style="width:100%">
            <dx:ASPxMemo ClientInstanceName="_dx" ID="memSubject" runat="server" Width="100%" Rows="5" Text='<%# ((AppointmentInplaceEditorTemplateContainer)Container).Appointment.Subject %>'>
            </dx:ASPxMemo>
        </td>
        <td runat="server" id="buttonContainer" class="dx-p2" style="vertical-align: top">

        <div>
			<dxsc:NoBorderButton runat="server" ClientInstanceName="_dx" ID="btnSave" Width="19px" Height="19px" /> 
        </div>
        
        <div style="padding-top:1px;">
			<dxsc:NoBorderButton runat="server" ClientInstanceName="_dx" ID="btnCancel" Width="19px" Height="19px" /> 
        </div>

        <div style="padding-top:6px;">
			<dxsc:NoBorderButton runat="server" ClientInstanceName="_dx" ID="btnEditForm" Width="19px" Height="19px" />
        </div>
        </td>
    </tr>
</table>