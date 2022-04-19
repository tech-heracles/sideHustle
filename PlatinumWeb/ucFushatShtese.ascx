<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ucFushatShtese.ascx.cs" Inherits="PlatinumWeb.ucFushatShtese" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<script src="js/ucFushatShtese.js">


</script>
<table id="tblFushatShtese" class="renditKontrolleCellMeWidth33">
    <tbody>
        <tr>
            <td>
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbModeli" ID="lblModeli" runat="server"
                    Text="Modeli:" ClientInstanceName="lblModeli">
                </dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbModeli" runat="server" SelectedIndex="0"
                    ClientInstanceName="cmbModeli" ShowShadow="False">
                    <ClientSideEvents SelectedIndexChanged="SelectedModelIndexChanged" />
                </dx:ASPxComboBox>
            </td>
            <td style="padding-left: 40px">
                <dx:ASPxLabel Wrap="False" AssociatedControlID="dtAktivizimi" ID="lblDtAktivizimi" runat="server"
                    Text="Date Aktivizimi:" ClientInstanceName="lblDtAktivizimi" ClientVisible="false" >
                </dx:ASPxLabel>
                <dx:ASPxDateEdit runat="server" ID="dtAktivizimi" ClientInstanceName="dtAktivizimi" ClientVisible="false" >
                </dx:ASPxDateEdit>
            </td>
            <td style="padding-left: 20px">
                <dx:ASPxLabel Wrap="False" AssociatedControlID="cmbDtNdryshimi" ID="lblDtNdryshimi" runat="server"
                    Text="Historiku:" ClientInstanceName="lblDtNdryshimi" ClientVisible="false" >
                </dx:ASPxLabel>
                <dx:ASPxComboBox ID="cmbDtNdryshimi" runat="server" SelectedIndex="0" ClientVisible="false" 
                    ClientInstanceName="cmbDtNdryshimi" ShowShadow="False" EnableSynchronization="True">
                    <ClientSideEvents SelectedIndexChanged="DtNdryshimiChanged" />
                </dx:ASPxComboBox>
            </td>
        </tr>
    </tbody>
</table>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi5" ClientVisible="false" ID="lblKodi5" runat="server"
    Text="Kodi:" ClientInstanceName="lblKodi5">
</dx:ASPxLabel>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtKodi6" ClientVisible="false" ID="lblKodi6" runat="server"
    Text="Kodi:" ClientInstanceName="lblKodi6">
</dx:ASPxLabel>
<dx:ASPxTextBox ID="txtKodi5" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi5">
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px" />
        </ErrorFrameStyle>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
</dx:ASPxTextBox>
<dx:ASPxTextBox ID="txtKodi6" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false" ClientInstanceName="txtKodi6">
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px" />
        </ErrorFrameStyle>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
</dx:ASPxTextBox>
<dx:ASPxLabel Wrap="False" ClientVisible="false" AssociatedControlID="txtPershkrimi5" ID="lblPershkrimi5"
    runat="server" Text="Pershkrimi:" ClientInstanceName="lblPershkrimi5">
</dx:ASPxLabel>
<dx:ASPxTextBox ID="txtPershkrimi5" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false"
    ClientInstanceName="txtPershkrimi5">
</dx:ASPxTextBox>
<dx:ASPxLabel Wrap="False" AssociatedControlID="txtPershkrimi6" ID="lblEmertimi6" ClientVisible="false"
    runat="server" Text="Pershkrimi:" ClientInstanceName="lblEmertimi6">
</dx:ASPxLabel>
<dx:ASPxTextBox ID="txtPershkrimi6" ClientVisible="false" runat="server" Width="100%" AutoPostBack="false"
    ClientInstanceName="txtEmertimi6">
    <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
        <ErrorFrameStyle ImageSpacing="4px">
            <ErrorTextPaddings PaddingLeft="4px" />
        </ErrorFrameStyle>
    </ValidationSettings>
    <DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black">
    </DisabledStyle>
</dx:ASPxTextBox>
<br />
<dx:ASPxGridView ID="gvFushat" runat="server" Width='60%' KeyboardSupport="true" ClientInstanceName="gvFushat"
    OnAfterPerformCallback="gvFushat_AfterPerformCallback" OnHtmlRowCreated="gvFushat_HtmlRowCreated" OnCustomJSProperties="gvFushat_CustomJSProperties"
    OnCustomCallback="gvFushat_CustomCallback" OnDataBound="gvFushat_DataBound">
    <ClientSideEvents EndCallback="gvFushatEndCallback"  BeginCallback="gvFushatBeginCallback"/>
    <Styles>
        <Header ImageSpacing="5px" SortingImageSpacing="5px">
        </Header>
    </Styles>
    <StylesEditors>
        <ProgressBar Height="25px">
        </ProgressBar>
    </StylesEditors>
    <SettingsLoadingPanel  Mode="Disabled"/>
</dx:ASPxGridView>

<br />
<dx:ASPxHiddenField ID="HfFushaShtese" ClientInstanceName="HfFushaShtese" runat="server"></dx:ASPxHiddenField>
