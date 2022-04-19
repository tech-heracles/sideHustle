<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="KontabilizimDokumenti.aspx.cs"
    Inherits="PlatinumWeb.KontabilizimDokumenti" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxPivotGrid.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPivotGrid" TagPrefix="dx" %>


<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>

<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html
	xmlns="http://www.w3.org/1999/xhtml">
	<head id="Head1" runat="server">
		<title>Alpha Web</title>
		<link href="DX.ashx?cssfile=~/AlphaWeb.css" rel="stylesheet" />
		<link href="fine-uploader/fine-uploader-new.css" rel="stylesheet"/>
		<link href="js/jqGrid445/plugins/ui.multiselect.css" rel="stylesheet" type="text/css" />
		<link href="js/jqGrid445/css/ui.jqgrid.css" rel="stylesheet" type="text/css" />
		<meta name="viewport" content="width=device-width,initial-scale=1.0" />
		<link href="AlphaWeb.css" rel="stylesheet" />
		<link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
		<script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myCookies-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/KontabilizimDokumenti.aspx-IMB.7.4.js"
        type="text/javascript"></script>
	</head>
	<body>
		<form id="form1" runat="server">
			<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Font-Size="9pt" Modal="True" ImagePosition="Top">
				<LoadingDivStyle Opacity="30"></LoadingDivStyle>
			</dx:ASPxLoadingPanel>
			<asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="3600"></asp:ScriptManager>
			<dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server"></dx:ASPxGlobalEvents>
			<dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server" SyncWithServer="true" ViewStateMode="Enabled"></dx:ASPxHiddenField>
			<asp:UpdatePanel ID="UpdatePanel1" runat="server">
				<ContentTemplate>
					<asp:HiddenField ID="hfKonffillestar" runat="server" />
					<asp:HiddenField ID="hfTeDrejtaKonfGride" runat="server" />
					<asp:HiddenField ID="hfTeDrejtaGjitheDok" runat="server" />
					<table width="100%">
						<tr>
							<td>
								<dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  ClientInstanceName="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound"
                                ItemImagePosition="Top" Width="100%" AutoPostBack="true" ShowPopOutImages="True"
                                OnItemClick="ASPxMenu1_ItemClick" SeparatorWidth="1px">
									<RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
									<SubMenuStyle GutterWidth="0px" HorizontalAlign="Justify" />
									<ClientSideEvents ItemClick="function(s, e) { menu_click(s,e); }" Init="function(s) {s.SetClientVisible(true);}" />
									<ItemImage Height="32px" Width="32px"></ItemImage>
									<SubMenuItemImage Height="16px" Width="16px"></SubMenuItemImage>
									<ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" VerticalAlign="Middle">
										<Paddings PaddingBottom="1px" PaddingTop="9px" />
									</ItemStyle>
									<SubMenuItemStyle Width="32px"></SubMenuItemStyle>
								</dx:ASPxMenu>
							</td>
						</tr>
						<tr>
							<td>
								<div id="dvMenu" style="display: none">
									<asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
										<ContentTemplate>
											<dx:ASPxMenu ID="MenuInfo" BackColor="transparent" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
												<ClientSideEvents Init="function(s,e){$('#dvMenu').show();myMesazh.InicializoTimer();}" />
												<ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
												<SubMenuStyle GutterWidth="17px" />
											</dx:ASPxMenu>
										</ContentTemplate>
									</asp:UpdatePanel>
								</div>
							</td>
						</tr>
					</table>
				</ContentTemplate>
			</asp:UpdatePanel>
			<table>
				<tr>
					<td class="renditKontrolleCaption">
						<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbKonfigurimi" ID="konfigurimi_Label" runat="server"  Text="Modeli:"></dx:ASPxLabel>
					</td>
					<td class="renditKontrolleCellMeWidth33">
						<dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi"
                                                ShowShadow="False" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top"
                                                Style="font-size: small" Height="24px" Width="100%" AnimationType="None">
							<ClientSideEvents SelectedIndexChanged="function(s,e){ndryshoKonfiguriminInit()}" />
							<DropDownButton>
								<Image>
									<SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
								</Image>
							</DropDownButton>
							<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
								<ErrorFrameStyle ImageSpacing="4px">
									<ErrorTextPaddings PaddingLeft="4px" />
								</ErrorFrameStyle>
							</ValidationSettings>
						</dx:ASPxComboBox>
					</td>
					<td class="renditKontrolleLabelMeWidth33">
						<dx:ASPxLabel Wrap="False" ID="lblKonfigurimi" runat="server" class="klasePerLblKonfigurimi" ClientInstanceName="lblKonfigurimi"></dx:ASPxLabel>
					</td>
				</tr>
			</table>
			<div id="dvAktiviteti" style="width: 100%">
				<div>
					<table id="tblFiltra" class="renditKontrolle" style="border-collapse: separate; border-spacing: 0 0.5em;" runat="server">
						<tbody>
							<tr>
								<td class="tdStyle6">
									<dx:ASPxLabel Wrap="False" AssociatedControlID="radDtDok" ID="lblPeriudha" runat="server" CssClass="style13"
                                    Text="Date Dokumenti " ClientInstanceName="lblPeriudha"></dx:ASPxLabel>
								</td>
								<td class="tdStyle6">
									<dx:ASPxRadioButtonList ID="radDtDok" ClientInstanceName="radDtDok" Font-Size="12px" RepeatDirection="Horizontal"
                                                Font-Bold="true" ForeColor="#0072c6" runat="server" RepeatColumns="4" CssClass="Glass"
                                                CssPostfix="Glass" Height="16px" EnableClientSideAPI="true" Border-BorderStyle="None">
										<ClientSideEvents ValueChanged="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}"
                                                    Init="function(s,e){Utils.toggleKontrolletPeriudha(s,txtNgaDok,txtDeriDok);}" />
										<Items>
											<dx:ListEditItem Text="Aktuale" Value="Aktuale" Selected="true" />
											<dx:ListEditItem Text="Viti Ushtrimor" Value="VitiUshtrimor" />
											<dx:ListEditItem Text="Periudha" Value="Periudha" />
										</Items>
									</dx:ASPxRadioButtonList>
								</td>
								<td style="padding-left: 20px;" >
									<dx:ASPxLabel ID="lblNgaDok" runat="server" Text="Nga" Style="font-weight: 700; color: #0072c6"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxDateEdit ID="txtNgaDok" ClientEnabled="false" runat="server" TabIndex="10"
                                                AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                                EditFormatString="dd/MM/yyyy" Date="2017-01-01" ValidationSettings-CausesValidation="True"
                                                ClientInstanceName="txtNgaDok" CssPostfix="Glass" Height="16px">
										<ClientSideEvents Init="function (s,e){initNgaDok(s,e);}" DateChanged="function (s,e){ngaDokDateChanged(s,e);}" />
										<CalendarProperties ShowClearButton="False" ShowTodayButton="True">
											<HeaderStyle Spacing="1px" />
											<FooterStyle Spacing="4px" />
										</CalendarProperties>
										<ButtonStyle Width="13px"></ButtonStyle>
										<ValidationSettings CausesValidation="True">
											<ErrorImage Height="14px" Width="14px" />
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
									</dx:ASPxDateEdit>
								</td>
								<td>
									<dx:ASPxLabel ID="lblDeriDok" runat="server" Text="Deri" Style="font-weight: 700; color: #0072c6"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxDateEdit ID="txtDeriDok" ClientEnabled="false" runat="server" TabIndex="11"
                                                AutoPostBack="false" AllowNull="false" DateOnError="Today" EditFormat="Custom"
                                                EditFormatString="dd/MM/yyyy" Date="2017-12-31" ClientInstanceName="txtDeriDok"
                                                CssPostfix="Glass" Height="16px">
										<ClientSideEvents DateChanged="function (s,e){deriDokDateChanged(s,e);}" />
										<CalendarProperties ShowClearButton="False" ShowTodayButton="True">
											<HeaderStyle Spacing="1px" />
											<FooterStyle Spacing="4px" />
										</CalendarProperties>
										<ButtonStyle Width="13px"></ButtonStyle>
										<ValidationSettings CausesValidation="True">
											<ErrorImage Height="14px" Width="14px" />
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
									</dx:ASPxDateEdit>
								</td>
							</tr>
							<tr>
								<td >
									<dx:ASPxLabel Wrap="False" AssociatedControlID="cmbGrupKontabilizimi" ID="lblGrupKontabilizimi" runat="server" CssClass="style13"
                                            Text="Grup Kontabilizimi" ClientInstanceName="lblGrupKontabilizimi"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxComboBox ID="cmbGrupKontabilizimi" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="cmbGrupKontabilizimi">
										<ClientSideEvents ButtonClick="function(s,e){ButtonClickHapComboKontabilizimi(s);}" TextChanged="function(s, e){VendosVlereKontabilizimi();}"/>
										<ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
									</dx:ASPxComboBox>
								</td>
								<td style="padding-left: 20px;" >
									<dx:ASPxLabel Wrap="False" ID="ASPxLabel1" AssociatedControlID="txtNumer" runat="server"
                                            Text="Numer Dokumenti " ClientInstanceName="lblNumer"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxTextBox ID="txtNumer" Width="100%" runat="server" ClientInstanceName="txtNumer">
										<ValidationSettings ErrorDisplayMode="ImageWithTooltip" Display="Dynamic" ValidationGroup="entries"
                                                RequiredField-ErrorText="Numri i dokumentit është i domosdoshëm"></ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
									</dx:ASPxTextBox>
								</td>
							</tr>
							<tr>
								<td>
									<dx:ASPxLabel Wrap="False" ID="lblRuajNeGrup" runat="server" Text="Gjeneruar nga "  ClientInstanceName="lblRuajNeGrup" AssociatedControlID="txtNrGrupKontabilizimi"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxComboBox ID="txtNrGrupKontabilizimi" runat="server" ClientInstanceName="txtNrGrupKontabilizimi" SettingsLoadingPanel-ImagePosition="Top" Width="100%" ShowShadow="False"
                                                                                 DropDownStyle="DropDown" IncrementalFilteringMode ="StartsWith" EnableIncrementalFiltering="false">
										<ClientSideEvents ButtonClick="function(s,e) {ButtonClickedLlojDok(s);}"/>
										<LoadingPanelImage></LoadingPanelImage>
										<DropDownButton>
											<Image>
												<SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
											</Image>
										</DropDownButton>
										<ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ValidationGroup="entries1" ValidateOnLeave="false">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
										<DisabledStyle Font-Bold="False"></DisabledStyle>
									</dx:ASPxComboBox>
								</td>
								<td style="padding-left: 20px;">
									<dx:ASPxLabel ID="lblPerdorues" Width="80%" runat="server" CssClass="style13" Text="Perdoruesi" ClientInstanceName="lblPerdorues"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxComboBox ID="btnPerdorues" runat="server"  EnableSynchronization="True" Width="100%" AutoPostBack="false" ClientInstanceName="">
										<ClientSideEvents ButtonClick=" 
                                                    function(s,e) {ButtonClickedBtnePerdoruesi(s);
                                                        }" />
										<ValidationSettings CausesValidation="true" ValidationGroup="entries" SetFocusOnError="true"
                                                Display="Dynamic" ErrorDisplayMode="ImageWithTooltip">
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
										<DisabledStyle BackColor="#EEEEEE" Font-Bold="False" ForeColor="Black"></DisabledStyle>
									</dx:ASPxComboBox>
								</td>
								<td></td>
								<td>
									<dx:ASPxButton ID="btnKerko1" runat="server" Text="Kerko" AutoPostBack="false" 
                                                                CausesValidation="False" ClientInstanceName="btnKerko1" Width="170px">
										<ClientSideEvents Click="function(s, e) { kerko() }" />
									</dx:ASPxButton>
								</td>
							</tr>
                            <tr>
                                <td >
									<dx:ASPxLabel ID="lblDateRegjistrimi" runat="server" Text="Date Regjistrimi" Style="font-weight: 700; color: #0072c6"></dx:ASPxLabel>
								</td>
								<td>
									<dx:ASPxDateEdit ID="txtDateRegjistrimi" runat="server" ClientInstanceName="txtDateRegjistrimi" ShowShadow="False">
										<ClientSideEvents Init="function (s,e){ initDateRegjistrimi(s,e); }" />
										<CalendarProperties ShowClearButton="False" ShowTodayButton="True">
											<HeaderStyle Spacing="1px" />
											<FooterStyle Spacing="4px" />
										</CalendarProperties>
										<ButtonStyle Width="13px"></ButtonStyle>
										<ValidationSettings CausesValidation="True">
											<ErrorImage Height="14px" Width="14px" />
											<ErrorFrameStyle ImageSpacing="4px">
												<ErrorTextPaddings PaddingLeft="4px" />
											</ErrorFrameStyle>
										</ValidationSettings>
									</dx:ASPxDateEdit>
								</td>
							</tr>
						</tbody>
					</table>
				</div>
			</div>
			<asp:UpdatePanel ID="pnlKryesor" runat="server">
				<ContentTemplate>
					<div style="visibility: hidden">
						<dx:ASPxLabel ID="pergjigja" runat="server" Text="" EncodeHtml="False" ForeColor="Green"
                        ClientVisible="false"></dx:ASPxLabel>
					</div>
					<asp:HiddenField ID="hfVeprimi" runat="server" />
					<br />
					<dx:ASPxGridView ID="gvKontabilizimDokumenti" runat="server" 
                       ClientInstanceName="gvKontabilizimDokumenti"
                       onDataBound="gvKontabilizimDokumenti_DataBound"
                        OnAfterPerformCallback="gvKontabilizimDokumenti_AfterPerformCallback"
                        OnCustomJSProperties="gvKontabilizimDokumenti_CustomJSProperties"
                        OnHeaderFilterFillItems="gvKontabilizimDokumenti_HeaderFilterFillItems"
                        Width="100%" OnCustomCallback="gvKontabilizimDokumenti_CustomCallback">
						<ClientSideEvents RowDblClick="function(s, e) {  }" BeginCallback="function(s, e) { BeginCallback(s,e); }" EndCallback="function(s, e) { EndCallback(s,e); }" />
						<Settings ShowFilterRow="true" />
						<Styles>
							<Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
						</Styles>
						<StylesEditors>
							<ProgressBar Height="25px"></ProgressBar>
						</StylesEditors>
					</dx:ASPxGridView>
				</ContentTemplate>
			</asp:UpdatePanel>
			<div>
				<asp:UpdatePanel ID="UpdatePanel9" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
					<ContentTemplate>
						<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                        CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                        PopupVerticalAlign="WindowCenter" AllowResize="True" AppearAfter="10" ClientIDMode="AutoID"
                        AutoUpdatePosition="True" Font-Bold="False">
							<ClientSideEvents CloseUp="function(s, e) {  }" />
							<ContentStyle>
								<Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                                PaddingTop="1px" />
							</ContentStyle>
							<ContentCollection>
								<dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server"></dx:PopupControlContentControl>
							</ContentCollection>
						</dx:ASPxPopupControl >
					</ContentTemplate>
				</asp:UpdatePanel>
			</div>
		</form>
	</body>
</html>