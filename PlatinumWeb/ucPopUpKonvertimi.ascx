<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucPopUpKonvertimi.ascx.cs" Inherits="PlatinumWeb.ucPopUpKonvertimi" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popKonvertim" runat="server" AllowDragging="True" ClientIDMode="AutoID"
                    ClientInstanceName="popKonvertim" CloseAction="CloseButton" CssPostfix="Glass"
                    EnableAnimation="False" EnableViewState="False" Font-Bold="true" HeaderText="Kujdes"
                    Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                    Width="400px">
                    <HeaderStyle>
                        <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
                    </HeaderStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl14" runat="server">
                            <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel11" runat="server" ClientIDMode="AutoID" Width="600px">
                                <PanelCollection>
                                    <dx:PanelContent ID="PanelContent11" runat="server" SupportsDisabledAttribute="True">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <dx:ASPxLabel ID="lblKonvertoNe" runat="server" ClientIDMode="AutoID" Text="Konverto Ne:"
                                                        Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cmbKonverto" runat="server" ClientInstanceName="cmbKonverto">
                                                        <ClientSideEvents SelectedIndexChanged="function(s, e) {
	ndryshoNiveli(s,e);
}" />
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxLabel ID="lblKonfig" runat="server" ClientIDMode="AutoID" Text="Lloji:" Wrap="False">
                                                    </dx:ASPxLabel>
                                                </td>
                                                <td>
                                                    <dx:ASPxComboBox ID="cmbKonf" runat="server" ClientInstanceName="cmbKonf">
                                                    </dx:ASPxComboBox>
                                                </td>
                                                <td>
                                                    <dx:ASPxButton ID="ButtonOk2" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk2"
                                                        Text="Konverto">
                                                        <ClientSideEvents Click="function(s, e) {   konverto();
	popKonvertim.Hide();

}" />
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