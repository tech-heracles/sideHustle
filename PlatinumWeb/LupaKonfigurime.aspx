<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaKonfigurime.aspx.cs"
    Inherits="PlatinumWeb.LupaKonfigurime" %>
<%@ Register TagPrefix="avec" Namespace="PlatinumWeb" Assembly="PlatinumWeb" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<!DOCTYPE html>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxtc" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxw" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<head id="Head1" runat="server">
    <title>AVEC Accounting</title>
    <style type="text/css">
        .style1
        {
            height: 54px;
        }
    </style>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/LupaKonfigurime.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    
</head>
<body onload="Init()">
    <form id="form1" runat="server">
         
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
    </dx:ASPxGlobalEvents>
    <div id='div' style="display: none">
        <dx:ASPxRoundPanel EnableHierarchyRecreation="false" ID="ASPxRoundPanel1" runat="server" HeaderText="Zgjidh Llogarine"
            ClientInstanceName="panel" Width="100%" ShowHeader="False">
            <PanelCollection>
                <dx:PanelContent>
                    <table width="100%">
                        <tr>
                            <td>
                                <avec:AvecToolbar ID="ASPxMenu1" runat="server" OnDataBound="ASPxMenu1_DataBound" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="pnlMesazhi" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dx:ASPxMenu ID="MenuInfo" runat="server" ClientInstanceName="MenuInfo" Width="100%"
                                            BorderBetweenItemAndSubMenu="HideRootOnly" ClientIDMode="AutoID" ShowPopOutImages="True">
                                            <ItemSubMenuOffset FirstItemX="2" LastItemX="2" X="2" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <SubMenuStyle GutterWidth="17px" />
                                        </dx:ASPxMenu>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                    <asp:UpdatePanel ID="pnlKryesor" runat="server">
                        <ContentTemplate>                     
                            <table width="100%">
	                            <br />
	                            <tr>
		                            <td>
			                            <templates>
				                            <TitlePanel>
					                            <div runat="server" id ="gridaSelectButtons" visible ="false">
						                            <table>
							                            <tr>
								                            <td>
									                            <dx:ASPxButton ID="gridaSelectFaqe" runat="server" ToolTip="Zgjidh te gjithe faqen" AutoPostBack="false" Image-Url="images/check2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
										                            <ClientSideEvents Click="function(s, e) { gvLupaKonfig.SelectAllRowsOnPage(); }" />
									                            </dx:ASPxButton>
								                            </td>
								                            <td>
									                            <dx:ASPxButton ID="gridaSelectTeGjitha" runat="server" ToolTip="Zgjidh te gjithe" AutoPostBack="false" Image-Url="images/checks.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
										                            <ClientSideEvents Click="function(s, e) { gvLupaKonfig.SelectRows(); }" />
									                            </dx:ASPxButton>
								                            </td>
								                            <td>
									                            <dx:ASPxButton ID="gridaUnSelectTeGjitha" runat="server" ToolTip="Fshi Zgjedhjen" AutoPostBack="false" Image-Url="images/uncheck2.png" Image-Height="16px" Font-Size="8" UseSubmitBehavior="false">
										                            <ClientSideEvents Click="function(s, e) { gvLupaKonfig.UnselectRows(); }" />
									                            </dx:ASPxButton>
								                            </td>
							                            </tr>
						                            </table>
					                            </div>
				                            </TitlePanel>
			                            </templates>
		                            </td>
	                            </tr>
	                            <tr>
		                            <td>
			                            <dx:ASPxGridView ID="gvLupaKonfig" runat="server" ClientInstanceName="gvLupaKonfig"
                                                                        OnDataBound="gvLupaKonfig_DataBound" OnAfterPerformCallback="gvLupaKonfig_AfterPerformCallback"
                                                                        Width="100%" OnCustomCallback="gvLupaKonfig_CustomCallback">
				                            <ClientSideEvents RowDblClick="function(s, e) { OnGridSelectionChanged(); }" BeginCallback="function(s, e) { BeginCallback(s,e); }" />
				                            <Styles>
					                            <Header ImageSpacing="5px" SortingImageSpacing="5px"></Header>
				                            </Styles>
				                            <StylesEditors>
					                            <ProgressBar Height="25px"></ProgressBar>
				                            </StylesEditors>
                                            <SettingsPager></SettingsPager>
			                            </dx:ASPxGridView>
		                            </td>
	                            </tr>
	                            <tr></tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                        <dx:ASPxComboBox ID="cmbKonfigurimi" runat="server" ClientInstanceName="cmbKonfigurimi" AnimationType="None"
                            ShowShadow="False" Width="100%" ValueType="System.String" SettingsLoadingPanel-ImagePosition="Top" ClientVisible ="false">
                        </dx:ASPxComboBox>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel >
    </div>
    </form>
</body>
