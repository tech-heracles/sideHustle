<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FormatePrintimi.aspx.cs" Inherits="PlatinumWeb.FormatePrintimi" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxcp" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxp" %>
<!DOCTYPE html>
<%@ Register Src="~/ImbReportToolbar.ascx" TagPrefix="uc1" TagName="ImbReportToolbar" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
    <link href="AlphaWeb.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
    <link rel="Stylesheet" type="text/css" href="Stylesheet1.css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="css/selectize.css" type="text/css" rel="stylesheet" />
       <script type="text/javascript" src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/ui.multiselect.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/Utils-IMB.2.1.js;~/js/Menu_IMB.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/selectize.min.js;~/js/multiSelect.js;~/js/customCombobox.js;~/DataTables-1.10.12/media/js/jquery.dataTables.min.js;~/DataTables-1.10.12/media/js/dataTables.bootstrap.min.js;~/js/jquery.blockUI.js;~/js/bootstrap-notify.js;~/js/menu.js;~/js/aspx.js/FormatePrintimi.aspx-IMB.7.1.js&v76"></script>
    <style>
        .table-fixed thead{
              width: 580px;
            }
            .table-fixed tbody {
              height: 475px;
              overflow-y: auto;
              width: 580px;
            }
            .table-fixed td{
                width:280px;
            }
            .table-fixed th{
                width:280px;
				border-bottom: #ddd 1px solid;
            }
            .table-fixed tbody, .table-fixed thead > tr> th {
              float: left;
            }   
    </style>
</head>
<body>
    <form id="form1" runat="server">
     
        <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="360000"></asp:ScriptManager>
		<dx:ASPxSplitter EnableHierarchyRecreation="false" ID="ASPxSplitter10" Height="100%" runat="server" Orientation="Vertical" SeparatorVisible="false"
                ClientInstanceName="splitterFormatePrintimi" BackColor="Transparent" ClientIDMode="AutoID"  FullscreenMode="True">
                <Panes>
		<dx:SplitterPane Size="60px" Name="Top" ShowCollapseBackwardButton="True"
                        PaneStyle-BackColor="Transparent" Separators-Size="2px">
                        <PaneStyle BackColor="Transparent"></PaneStyle>
               <ContentCollection>
                            <dx:SplitterContentControl EnableViewState="false" CssClass="topSpliter" ID="SplitterContentControl1" runat="server">
                                <div style="position:absolute;z-index:10;width:100%" >
                                    <div class="col-lg-4" >
					                    <div class="col-md-2">
						                    <label class="" style="margin-top: 17%;">Raporte:</label>
					                    </div>
					                    <div class="col-md-10">
                                            <select  id="cmbRaporte" >                                               
                                            </select>
					                    </div>
                                    </div>
                                    <div class="col-lg-4"  >
					                    <div class="col-md-2">
						                    <label class="" style="margin-top: 17%;">Formate:</label>
					                    </div>
					                    <div class="col-md-10">
						                    <select id="cmbFormate" >
						                    </select>
					                    </div>
                                    </div>
                                    <div class="col-lg-4">
					                    <div class="col-md-3">
						                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
							                    <ContentTemplate>
								                    <button class="btn btn-primary" type="button" id="btnPrintPreview" name="btnPrintPreview" onclick="btnPrintPreview_Clicked();">Print Preview</button>
							                    </ContentTemplate>
						                    </asp:UpdatePanel>
					                    </div>
					                    <div class="col-md-9">
						                    <button  type="button" class="btn btn-primary" id="btnLidhMeNdermarrjen" data-toggle="modal" data-target="#PopUpNdermarrje" onclick="MerrNdermarrjetEPalidhura();">Lidh me ndermarrjen</button>
					                    </div>
                                    </div>
                                 </div>
                            <!-- PopUp-->
                            <div class="container">

                                    <!-- Modal -->
                                    <div class="modal fade" id="PopUpNdermarrje" role="dialog">
                                        <div class="modal-dialog">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                                    <h4 class="modal-title">Ndermarrje te palidhura</h4>
                                                </div>
                                                <div class="modal-body" style="height:600px">
                                                       <input type="text" class="form-control" placeholder="Search" id="searchnder"/>
                                                       <table id="TabeleNdermarrje" class="table table-bordered table-fixed" style="height:430px;margin-top:30px">
                                                           <thead style="display:block">
                                                              <tr>
                                                                <th style="width:30px;height:35px"></th>
                                                                <th style="width: 265px">Kodi</th>
                                                                <th style="width:265px">Ndermarrja</th>
                                                              </tr>
                                                           </thead>
                                                           <tbody>
                                                           </tbody>
                                                         </table>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-basic" style="margin-right:240px" onclick="ToogleTeGjithaNdermarrjet()" id="btnZgjidhTeGjithaNdermarrjet">Zgjidh te gjitha ndermarjet</button>
                                                    <button type="button" class="btn btn-default" data-dismiss="modal">Mbyll</button>
                                                    <button type="button" id="btnRuaj" class="btn btn-primary" onclick="DergoNdermarrjeNeServer();">Ruaj</button>

                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
			             </dx:SplitterContentControl>
			       </ContentCollection>
			</dx:SplitterPane>
			<dx:SplitterPane Name="paneViewFatura" ContentUrl="javascript:false" ContentUrlIFrameName="paneViewFatura"
				ScrollBars="Auto" AutoWidth="true" ShowCollapseBackwardButton="True"
                PaneStyle-BackColor="Transparent">
				<PaneStyle BackColor="Transparent">
				</PaneStyle>
				<ContentCollection>
					<dx:SplitterContentControl ID="SplitterContentControl13" runat="server">
					</dx:SplitterContentControl>
				</ContentCollection>
			</dx:SplitterPane>
			</Panes>
            </dx:ASPxSplitter>
        <dx:ASPxHiddenField ID="HfState" runat="server">
        </dx:ASPxHiddenField>
        
    </form>
    </body>
    </html>





