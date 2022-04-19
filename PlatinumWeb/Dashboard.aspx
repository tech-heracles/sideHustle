<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="PlatinumWeb.Dashboard.Dashboard" %>

<%@ Register Assembly="DevExpress.Dashboard.v18.2.Web.WebForms, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.DashboardWeb" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="../AlphaWeb.css" rel="stylesheet" />
    <link href="../bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="DX.ashx?jsfileset=~/js/aspx.js/Dashboard-IMB.8.8.js&v76" type="text/javascript"></script>

    <style>
        .toolsIcon {
            background-image: url(images/Dashboard/toolsIcon.svg);
            background-repeat: no-repeat;
        }

        .refreshIcon {
            background-image: url(images/Dashboard/refreshIcon.svg);
            background-repeat: no-repeat;
        }

        .itemsIcon {
            background-image: url(images/Dashboard/itemsIcon.svg);
            background-repeat: no-repeat;
        }

        .viewerIcon {
            background-image: url(images/Dashboard/viewerIcon.svg);
            background-repeat: no-repeat;
        }
    </style>
</head>
<body>
    <script type="text/html" id="dx-save-as-form">
        <div><span data-bind="text: saveAsName"></span></div>
        <div style="margin: 10px 0" data-bind="dxTextBox: { value: newName }"></div>
        <div data-bind="dxButton: { text: saveTitle, onClick: saveAs }"></div>
    </script>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <dx:ASPxTimer ID="Timer" runat="server" Interval="1800000" ClientInstanceName="Timer">
                <ClientSideEvents Tick="ReloadData"></ClientSideEvents>
            </dx:ASPxTimer>
            <dx:ASPxDashboard ID="ASPxDashboard1" runat="server" WorkingMode="Viewer" ClientInstanceName="ASPxDashboard1" ColorScheme="light.compact" AllowExportDashboard="false"  AllowExportDashboardItems="true" OnCustomJSProperties="ASPxDashboard1_CustomJSProperties">
                <ClientSideEvents 
                    DashboardChanged ="onDashboardChanged"
                    BeforeRender="onBeforeRender" 
                    DashboardTitleToolbarUpdated="onDashboardTitleToolbarUpdated" 
                    EndCallback="onDashboardEndCallback"
                    CallbackError="onDashboardCallbackError"
                    BeginCallback="onDashboardBeginCallback"/>
            </dx:ASPxDashboard>
            <dx:ASPxHiddenField ID="hfState" runat="server" />
        </div>

        <!-- Modal Dashboard Manager-->
        <div class="modal fade bs-example-modal-lg" id="dashboardManagerModal" style="z-index:2 !important;" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle">
          <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title" id="exampleModalLongTitle">Dashboard Manager</h4>
              </div>
              <div class="modal-body">
                <!------Grida e dashboards duke perdorur DevExtreme---------->
                <div id="data-grid-dashboard">                
                    <div id="gvDashboard" class ="noUndoGrida"></div>
                </div>
                <!------------------------------------------------------->
              </div>
              <div class="modal-footer">
                <input type="button" class="btn btn-primary btn-sm saveBtn" onclick="SaveDashboardsChanges();" value="Save changes"/>
                <input type="button" class="btn btn-secondary btn-sm cancelBtn" data-dismiss="modal" value="Cancel"/>
              </div>
            </div>
          </div>
        </div>

        <!-- Modal Confirm Delete-->
        <div class="modal fade" id="dashboardDeleteModal" tabindex="-1"  style="z-index:4 !important;" aria-labelledby="exampleModalCenterTitle">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title" id="exampleModalDeleteTitle">Confirm Deleting.</h4>
              </div>
              <div class="modal-body" style="text-align: center" id="modalDeleteBody">
                <p>Do you want to delete this dashboard?</p>
              </div>
              <div class="modal-footer">
                <input type="button" class="btn btn-primary btn-sm deleteBtn" data-dismiss="modal" onclick="DeleteDashboard()" value="Delete"/>
                <input type="button" class="btn btn-secondary btn-sm cancelBtn" data-dismiss="modal" value="Cancel"/>
              </div>
            </div>
          </div>
        </div>

        <!-- Modal Confirm Save-->
        <div class="modal fade" id="dashboardSaveModal" tabindex="-1" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title" id="exampleModalSaveTitle">Confirm Saving</h4>
              </div>
              <div class="modal-body" style="text-align: center" id="modalSaveBody">
                <p>If you leave before saving, your changes will be lost.<br /> Do you want to save changes?</p>
              </div>
              <div class="modal-footer">
                <input type="button" class="btn btn-primary btn-sm saveBtn" data-dismiss="modal" onclick="SaveDashboard()" value="Save"/>
                <input type="button" class="btn btn-secondary btn-sm cancelBtn" data-dismiss="modal" onclick="DoNotSaveDashboard()" value="Don't Save"/>
              </div>
            </div>
          </div>
        </div>

        <!-- Modal User Sharing-->
        <div class="modal fade" id="dashboardSharingModal" tabindex="-1"  style="z-index:4 !important;" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
                <h4 class="modal-title" id="exampleModalSharingTitle">Dashboard Sharing</h4>
              </div>
              <div class="modal-body">
                <!------Grida e perdoruesve duke perdorur DevExtreme---------->
                <div id="data-grid-users">
                    <div id="gvUsers" class ="noUndoGrida"></div>
                </div>
                <!------------------------------------------------------->
              </div>
              <div class="modal-footer">
                <input type="button" class="btn btn-primary btn-sm shareBtn" onclick="ShareDashboard();" value="Share dashboard"/>
                <input type="button" class="btn btn-secondary btn-sm cancelBtn" data-dismiss="modal" value="Cancel"/>
              </div>
            </div>
          </div>
        </div>
    </form>
    <script src="DX.ashx?jsfileset=~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/Dashboard/online-map-extension.js;~/js/Dashboard/SaveAsExtension.js;~/js/Dashboard/SaveExtension.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui.min.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myCookies-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myDxDataGrid.js&v76" type="text/javascript"></script>
</body>
</html>
