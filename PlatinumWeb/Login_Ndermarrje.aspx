<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login_Ndermarrje.aspx.cs"
    Inherits="PlatinumWeb.Login_Ndermarrje" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxoc" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Alpha Web</title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <style type="text/css">

        body *{
            margin:0;
            padding:0;
            outline:0;
            border:0;
        }
        body{
            margin: 8px !important;
        }

        body.kesh{
            overflow:auto;
            margin: 0!important;
        }

        .main {
            text-align: center;
            position:relative;
            padding:0;
            margin:0;
        }
        .kesh .main {
            text-align: center;            
            padding:0;
            margin:0;
            position: inherit;
        }

        .center {
            display:inline-block;
            /*padding-top:20px;*/
        }

        .content {
            width:700px;
            padding-top:100px;
        }

        .kesh .content{
            width:700px;
            padding-top:100px;
			padding-left: 32%;
            display:block;
        }
        
        .grid {
            /*vendos tekstin brenda grides majtas*/
            text-align:left;
        }
        
        .right {
            width: auto;
            height: 16px;
            background-color: #0072c6;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

        .right:hover {
            background-color: #208bda;
        }

        .kesh .right{
            width: 100%;
            height: 40px;
            background-color: #1478B4;
            display: inline-flex;
            border-radius: 0px;
            padding: 0;
            float: none;
        }

        .kesh .right:hover{
            background-color: none;
        }

        .klient .right {
            width: auto;
            height: 16px;
            background-color: #5D9AD3;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

        .vodafone .right {
            width: auto;
            height: 16px;
            background-color: #ed1b24;
            border-radius: 4px;
            padding: 9px 11px 9px 11px;
            float: right;
            display: inline-flex;
        }

        .klient .right:hover {
            background-color: #75afe5;
        }

        .vodafone .right:hover {
            background-color: #f13941;
        }

        .emri {
            padding: 0;
            margin: 0;
            line-height: 16px;
        }

        .kesh .emri2 {
            padding: 0;
            margin: 0;
            line-height: 16px;
			float: right;			
			border: 1px solid white;
			height: 20px;
			border-radius:5px;
        } 
		
		.kesh .emriKesh {	
			border: 1px solid white;
			height: 30px;
			border-radius:5px;
			margin-top:5px;
			display: inline-flex;
			background-color: #3375a8;
			margin-right: 10px;
        }

        .dalje {
            margin-left: 3px;
        }

        .zgjidh{
            margin-top:20px;
        }

        .kesh .footer {
			position: absolute;
			right: 0;
			bottom: 0;
			left: 0;
			padding: 0.6rem;
			background-color: #1478B4;
			text-align: center;
		}
    
    @media all and (max-width:700px) {
        .content {
            padding-top:20px;
        }
    }
    </style>
    <link href="FaqeKryesore.css" rel="stylesheet" />
    <%--   <script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>
    <script src="js/jquery.cookie.js" type="text/javascript"></script>
    <script src="js/aspx.js/Login_Ndermarrje.aspx-IMB.2.1.js?versioni22" type="text/javascript"></script> --%>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery.cookie.js;~/js/aspx.js/Login_Ndermarrje.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
    <meta name="description" content="Login-i i ndermarrjes" />
     <meta name="viewport" content="width=device-width,initial-scale=1.0" />
</head>
<body onload="load()" class="">
    <form id="form2" runat="server" style="width: 100%; padding: 0; margin: 0">
        <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popNdermarrjePaPeriudha" runat="server" AllowDragging="True" ClientIDMode="AutoID"
            ClientInstanceName="popNdermarrjePaPeriudha" CloseAction="CloseButton" CssPostfix="Glass" EnableAnimation="False"
            EnableViewState="False" Font-Bold="true" HeaderText="Kujdes" Modal="True" PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter" Width="300px">
            <HeaderStyle>
                <Paddings PaddingLeft="10px" PaddingRight="6px" PaddingTop="1px" />
            </HeaderStyle>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                    <dx:ASPxPanel EnableHierarchyRecreation="false" ID="ASPxPanel1" runat="server" ClientIDMode="AutoID" Width="271px">
                        <PanelCollection>
                            <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxLabel ID="lblNukEkzistojnePeriudhat" runat="server" ClientIDMode="AutoID" Text="Kujdes! Nuk ekzistojne periudhat!">
                                </dx:ASPxLabel>
                                <br />
                                <br />
                                <div style="text-align: right;">
                                    <table>
                                        <tr>
                                            <td style="width: 57%"></td>
                                            <td style="align-content: center">
                                                <dx:ASPxButton ID="ButtonOk" Width="70px" runat="server" CausesValidation="False" ClientInstanceName="ButtonOk"
                                                    Text="Ok" AutoPostBack="False">
                                                    <ClientSideEvents Click="function(s, e) {popNdermarrjePaPeriudha.Hide();}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="visibility: hidden">
                                </div>
                            </dx:PanelContent>
                        </PanelCollection>
                    </dx:ASPxPanel >
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl >
        <section class="main">
            <header class="right header">
                <div class="emri">
                <dx:ASPxLabel ID="lblUserEmri" ClientInstanceName="lblUserEmri" runat="server" Text=""
                    Font-Size="11" ForeColor="white" Font-Names="Calibri">
                </dx:ASPxLabel>
                </div>
                <div class="dalje">
                <dx:ASPxHyperLink CssClass="dalje" ID="ASPxHyperLink2" runat="server" EnableViewState="false" ViewStateMode="Disabled"
                    NavigateUrl="<% DbCore.IMBUtils.Paths.defaultLoginPath%>" ImageUrl="images/dalje.png" ForeColor="Black" Font-Underline="false" Text="Dalje" Font-Size="11"
                    Font-Names="Calibri">
                </dx:ASPxHyperLink>
                </div>
            </header>
            <header class="right headerKesh" style="display: none;">
				<div id="btnHome" class="logoHome" style="position:absolute; margin-left:10px; background-image:url('images/KeshSlogan2.png'); width:500px;">                                        
					<dx:ASPxHyperLink ID="ASPxHyperLink1" runat="server" Width="100%" Height="32px">
					</dx:ASPxHyperLink>
				</div>
				<div id="faqja" class="faqja" style="width:100%">
					<dx:ASPxLabel runat="server" ClientInstanceName="lblFaqja" CssClass="style15" 
						Font-Size="Large" ForeColor="#ffffff" ClientIDMode="AutoID" ID="ASPxLabel1">
					</dx:ASPxLabel>
				</div>
                <div>
					<div class="emriKesh">				
						<dx:ASPxLabel ID="lblUserEmriKESH" ClientInstanceName="lblUserEmriKESH" runat="server" Text=""
							Font-Size="11" ForeColor="white" Font-Names="Calibri" style="padding-left:6px; padding-bottom:5px; padding-top:5px;">
						</dx:ASPxLabel>
						<dx:ASPxHyperLink CssClass="dalje" ID="ASPxHyperLink3" runat="server" EnableViewState="false" ViewStateMode="Disabled"
							NavigateUrl="<% DbCore.IMBUtils.Paths.defaultLoginPath%>" ImageUrl="images/logoutShigjeta.png" ForeColor="Black" Font-Underline="false" style="padding-right:5px; padding-bottom:5px;">
						</dx:ASPxHyperLink> 
					</div>
				</div>
            </header>
            <section class="content center">
                <dx:ASPxGridView ID="grid_ListLoginNdermarrje" Width="100%" CssClass="grid" runat="server" ClientInstanceName="grid"
                    OnDataBound="grid_ListLoginNdermarrje_DataBound" SettingsBehavior-AllowFocusedRow="true" Theme="Default">
                    <ClientSideEvents RowDblClick="OnGridDoubleClick"
                        EndCallback="setSize" Init="setSize" BeginCallback="setSize" />
                    <Styles>
                        <LoadingPanel ImageSpacing="8px">
                        </LoadingPanel>
                    </Styles>
                    <Settings ShowFilterRow="true" ShowFilterBar="Visible"  />
                    <Styles  Cell-Wrap="True"></Styles>
                    <SettingsLoadingPanel ImagePosition="Top" />
                    <StylesEditors>
                        <CalendarHeader Spacing="1px">
                        </CalendarHeader>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <ImagesEditors>
                        <DropDownEditDropDown>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                        </DropDownEditDropDown>
                        <SpinEditIncrement>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditIncrementImageHover_Aqua"
                                PressedCssClass="dxEditors_edtSpinEditIncrementImagePressed_Aqua" />
                        </SpinEditIncrement>
                        <SpinEditDecrement>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditDecrementImageHover_Aqua"
                                PressedCssClass="dxEditors_edtSpinEditDecrementImagePressed_Aqua" />
                        </SpinEditDecrement>
                        <SpinEditLargeIncrement>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeIncImageHover_Aqua"
                                PressedCssClass="dxEditors_edtSpinEditLargeIncImagePressed_Aqua" />
                        </SpinEditLargeIncrement>
                        <SpinEditLargeDecrement>
                            <SpriteProperties HottrackedCssClass="dxEditors_edtSpinEditLargeDecImageHover_Aqua"
                                PressedCssClass="dxEditors_edtSpinEditLargeDecImagePressed_Aqua" />
                        </SpinEditLargeDecrement>
                    </ImagesEditors>
              
                </dx:ASPxGridView>
                         <footer class="zgjidh">
                <dx:ASPxButton ID="ok_ASPxButton" CssClass="center" runat="server" Text="OK" ClientInstanceName="btnOk"
                    OnClick="ok_ASPxButton_Click"  Width="100px" Theme="Default">
                    <ClientSideEvents Click="function(s, e) {
	            SetSplitterPaneContentUrl('Footer','FooterPanelInfo.aspx');
            }" />
                </dx:ASPxButton>
            </footer>
            </section>
            
        <!-- Place this tag where you want the Live Helper Plugin to render. -->
        <div id="lhc_status_container_page" ></div>
               
        </section>
        <div class="footer" style="display: none;">
			<div style="background:url('images/emrikesh_white.png') no-repeat; height:14px; margin-left:37%">
			</div>
		</div>
        
            <dx:ASPxHiddenField ID="hfState" runat="server" ClientInstanceName="hfState">
            </dx:ASPxHiddenField>

    </form>

    <script>
		
		function load() {
            
			if (document.getElementsByTagName('body')[0].className == 'kesh'){
				document.querySelector('.header').style.display='none';
				document.querySelector('.headerKesh').style.display = 'inline-flex';
				document.querySelector('.footer').style.display = 'block';
			}
        };
    </script>
</body>
</html>
