<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopUpKlonimi.ascx.cs" Inherits="PlatinumWeb.ucPopUpKlonimi" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<script src="DX.ashx?jsfileset=~/js/ucPopUpKlonimi.js&v76" type="text/javascript"></script>
<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popUpKlonimi" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popUpKlonimi" CloseAction="CloseButton" CssPostfix="Glass"
                    EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="400px">
    <HeaderStyle>
        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
    </HeaderStyle>
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControlKlono" runat="server">
            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="600px">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContentKlono" runat="server" SupportsDisabledAttribute="True">
                        <table width="100%">
                            <tr>
                                <td>
                                    <dx:ASPxLabel ID="lblKlonoNe" runat="server" ClientIDMode="AutoID" Text="Klono në:"
                                        Wrap="False">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cmbNiveliKlono" runat="server" ClientInstanceName="cmbNiveliKlono">
                                        <ClientSideEvents SelectedIndexChanged="function(s, e) { popUpKlonimiFunctions.changedNiveli(s,e); }" />
                                        <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                            <RequiredField IsRequired="true" />
                                        </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxLabel ID="lblKonfig" runat="server" ClientIDMode="AutoID" Text="Lloji:" Wrap="False">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxComboBox ID="cmbKonfigKlono" runat="server" ClientInstanceName="cmbKonfigKlono">
                                    <ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic">
                                        <RequiredField IsRequired="true" />
                                    </ValidationSettings>
                                    </dx:ASPxComboBox>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="btnKlono" runat="server" CausesValidation="True" ClientInstanceName="btnKlono" Text="Klono">
                                        <ClientSideEvents Click="function(s, e) { popUpKlonimiFunctions.klonoDokument(s, e); }" />
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