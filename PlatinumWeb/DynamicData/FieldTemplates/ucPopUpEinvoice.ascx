<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopUpEinvoice.ascx.cs" Inherits="PlatinumWeb.ucPopUpEinvoice" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<script src="DX.ashx?jsfileset=~/js/ucPopUpEinvoice.js&v76" type="text/javascript"></script>
<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpEinvoice" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popUpEinvoice" CloseAction="CloseButton" CssPostfix="Glass"
                    EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="400px">
    <HeaderStyle>
        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControlEinvoice" runat="server">
            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="600px">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContentEinvoice" runat="server" SupportsDisabledAttribute="True">
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblKonfig" runat="server" ClientIDMode="AutoID" Text="Statusi:" Wrap="False">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cmbKonfigEinvoice" runat="server" ClientInstanceName="cmbKonfigEinvoice">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnEinvoice" runat="server" CausesValidation="True" ClientInstanceName="btnEinvoice" Text="Ndrysho">
                                    <ClientSideEvents Click="function(s, e) { popUpEinvoiceFunctions.ndryshoStatus(s, e); }" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>