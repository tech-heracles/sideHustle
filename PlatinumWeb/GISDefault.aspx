<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GISDefault.aspx.cs" Inherits="PlatinumWeb.GISDefault" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="TimeoutControl.ascx" TagName="TimeoutControl" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha GIS</title>

    <link rel="shortcut icon" href="images/GIS/faviconGIS.ico" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/ext-3.4.0/resources/css/ext-all.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/ext-3.4.0/resources/css/xtheme-gray.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/GeoExt/css/geoext-all-debug.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/GeoExt/css/popup.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/ext-3.4.0/ux/gridfilters/css/GridFilters.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/ext-3.4.0/ux/gridfilters/css/RangeMenu.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/blueimp-gallery.min.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/jquery-ui-1.11.1.css" id="theme" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/print.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/printpreview.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/stile.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/butonat.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/fileuploadfield.css" />
    <link rel="stylesheet" type="text/css" href="js/srcGIS/css/Stile/GisPageStil.css" />
    <%--<script type="text/javascript">var gisGlobalColor = "Purple"</script>--%>
<meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
    <script src="js/OpenLayers-2.13.1/OpenLayers.js"></script>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/Utils-IMB.2.1.js;~/js/srcGIS/varKonfig.js;~/js/srcGIS/jsInherit/XYZ_A_T.js;~/js/proj4js-combined.js;~/js/srcGIS/jquery-ui-1.11.1.min.js;~/js/srcGIS/jquery.blueimp-gallery.min.js;~/js/srcGIS/jquery.image-gallery.min.js;~/js/srcGIS/ext-3.4.0/ext-base.js;~/js/srcGIS/ext-3.4.0/ext-all.js;~/js/srcGIS/GeoExt/GeoExt.js;~/public/modernizr.js;~/js/srcGIS/Exporter-all.js;~/js/srcGIS/jsInherit/RegularPolygonDR.js;~/js/srcGIS/jsClasses/convertTypeDB_GEO.js;~/js/srcGIS/jsInherit/GetFeatureT.js;~/js/srcGIS/jsInherit/OpenLayers_Protocol_HTTP_T.js;~/js/srcGIS/jsClasses/sprintf.js;~/js/srcGIS/jsInherit/GraticuleXYT.js;~/js/srcGIS/jsInherit/PrintProviderT.js;~/js/srcGIS/jsClasses/SimplePrint.js;~/js/srcGIS/jsInherit/SimplePrintT.js;~/js/srcGIS/jsInherit/PrintExtentT.js;~/js/srcGIS/jsClasses/MultiPagePrint.js;~/js/srcGIS/jsInherit/MultiPagePrintT.js;~/js/srcGIS/jsFunc/funksionePerTeDhenaTeJashtme.js;~/js/srcGIS/jsClasses/Harte.js;~/js/srcGIS/jsClasses/HarteUI.js;~/js/srcGIS/jsClasses/LayerAppClass.js;~/js/srcGIS/jsInherit/ControlClickT.js;~/js/srcGIS/jsInherit/ControlClickTT.js;~/js/srcGIS/jsClasses/UndoRedo.js;~/js/srcGIS/jsClasses/BaseLayerClass.js;~/js/srcGIS/jsClasses/FileUploadField.js;~/js/srcGIS/jsClasses/VTypesTe.js;~/js/srcGIS/jsInherit/PropertyGridT.js;~/js/srcGIS/jsInherit/ModifyFeatureT.js;~/js/srcGIS/jsInherit/ArrayT.js;~/js/srcGIS/jsFunc/funksioneNdihmese.js;~/js/srcGIS/jsInherit/ExportMapTe.js;~/js/srcGIS/jsClasses/LoadingPanel.js;~/js/srcGIS/jsClasses/Ext.ux.PanelCollapsedTitle.js;~/js/srcGIS/ext-3.4.0/ux/ux-all.js;~/js/OpenLayers-2.13.1/lib/OpenLayers/WPSProcess.js;~/js/OpenLayers-2.13.1/lib/OpenLayers/WPSClient.js;~/js/srcGIS/jsClasses/stream.js;~/js/srcGIS/jsClasses/shapefile.js;~/js/srcGIS/jsClasses/geoextUX/LayerTreeBuilder.js;~/js/srcGIS/jsFunc/funksioneGisPage.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/srcGIS/jsFunc/canceBackSpace.js;~/js/srcGIS/jsFunc/funksioneReadyGisPage.js&v76"
        type="text/javascript"></script>
	<%--<script src='http://maps.google.com/maps/api/js?v=3.22&amp;'></script>--%>
    <script src="//maps.googleapis.com/maps/api/js?key=AIzaSyACrrRhtdTbsjW6rvdFyl-7mtFWeJL60R0" async="" defer="defer" type="text/javascript"></script>
    <script src="GISProxyGEO.ashx?url=printCapabilitiesInfo"> </script>
    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src="https://www.googletagmanager.com/gtag/js?id=UA-121798081-2"></script>
</head>

<body>
    <div id="map"></div>
    <div id="printdiv"></div>
    <div id="legendpaneldivPrintim" style="display: none"></div>
    <div id="gpxpaneldiv"></div>
    <div id="editAttrs"></div>
    <div id="insertAtribute"></div>
    <div id="editWindow"></div>
    <div id="atribute">
        <form name="formatribute">
            <table id="tabeleAtribute" style="background-color: #BBDCFC;"></table>
        </form>
    </div>
    <div id="BaseLayerWindow"></div>
    <div id="BaseLayerPanelDiv"></div>
    <div id="rightClickMenuDiv"></div>
    <div id="DokumentimUploadField"></div>
    <div id="njoftimeRezultate" class="njoftimeRezultateCss"></div>    
    <div class="container marketing">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-offset-2 col-md-10">
                    <div id="blueimp-gallery" class="blueimp-gallery blueimp-gallery-controls">
                        <div class="slides"></div>
                        <h3 class="title"></h3>
                        <a class="prev">‹</a>
                        <a class="next">›</a>
                        <a class="close">×</a>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <form id="form1" runat="server">
          
        <asp:ScriptManager ID="ScriptManagerGWSId" runat="server">
         </asp:ScriptManager>
        <asp:HiddenField ID="folderatShfaqurHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="gjuhaHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="projeksioneHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GISLayersTypeHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GISLayersHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GISNdermarrjeHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GISAllUsersHF" ClientIDMode="Static" runat="server" />
        <asp:HiddenField ID="GISTheme" ClientIDMode="Static" runat="server" />
        <uc1:TimeoutControl ID="SessionTimeout" runat="server" />

        <asp:UpdatePanel ID="updateraporti" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
            <ContentTemplate>
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal"
                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="True" PopupHorizontalAlign="WindowCenter"
                    EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True"
                    AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                    <ClientSideEvents Closing="function(s, e) { popupUniversal.SetContentUrl('');  }" CloseUp="PopupCloseUp" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
                <dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="popupUniversal2" runat="server" AllowDragging="True" ClientInstanceName="popupUniversal2"
                    CloseAction="CloseButton" HeaderText="Zgjidh Llogarine" Modal="False" PopupHorizontalAlign="WindowCenter"
                    EnableAnimation="False" PopupVerticalAlign="WindowCenter" AllowResize="True" ShowCollapseButton="true"
                    AppearAfter="10" ClientIDMode="AutoID" Height="400px">
                    <ClientSideEvents Closing="function(s, e) { popupUniversal2.SetContentUrl('');  }" CloseUp="PopupCloseUp2" />
                    <ContentStyle>
                        <Paddings Padding="1px" PaddingBottom="1px" PaddingLeft="1px" PaddingRight="1px"
                            PaddingTop="1px" />
                    </ContentStyle>
                    <ContentCollection>
                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl >
            </ContentTemplate>
        </asp:UpdatePanel>
        <dx:ASPxHiddenField ID="hfState" ClientInstanceName="hfState" runat="server">
        </dx:ASPxHiddenField>
    </form>
</body>
</html>
