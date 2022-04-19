<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMLupaHistoriku.aspx.cs" Inherits="PlatinumWeb.CRMLupaHistoriku" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>




<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
	 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/myButtonClickLupa-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/CRMLupaHistoriku.aspx-IMB.4.8.js&v49"
        type="text/javascript">
    </script>
</head>
<body>
    <form id="form1" runat="server">          
    <dx:ASPxHiddenField ID="hfState" runat="server"></dx:ASPxHiddenField>
        <dx:ASPxImageGallery ID="galeria" EnableViewState="false" Width="100%"  runat="server">
            <SettingsTableLayout ColumnCount="3" RowsPerPage="5"  />
            <PagerSettings EndlessPagingMode="OnScroll">
                
            </PagerSettings>
        </dx:ASPxImageGallery>
    
    
    </form>
</body>
</html>
