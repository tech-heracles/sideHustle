<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopUpUniversal.ascx.cs" Inherits="PlatinumWeb.ucPopUpUniversal" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
    <ContentTemplate>
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
            CloseAction="CloseButton" EnableAnimation="False" HeaderText="Zgjidh Llogarine"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            AllowResize="True" AppearAfter="10" ClientIDMode="AutoID">
            <ClientSideEvents Closing="function(s, e) {popupUniversal.SetContentUrl('');}" />
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </ContentTemplate>
</asp:UpdatePanel>