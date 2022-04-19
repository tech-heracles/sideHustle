<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LupaArkiva.aspx.cs" Inherits="PlatinumWeb.LupaArkiva" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>





<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <title>Alpha Web</title>
   
    <link href="js/css/le-frog/jquery-ui.css" rel="stylesheet" type="text/css" runat="server"
          id="themeJQuery"/>
    <link rel="stylesheet" type="text/css" media="screen" href="js/jqGrid445/css/ui.jqgrid.css"/>
     <%--<link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" />--%>
    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" />
	<meta name="viewport" content="width=device-width,initial-scale=1.0" />
     <link href="AlphaWeb.css" type="text/css" rel="stylesheet"/>
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/bootstrap-3.3.6-dist/js/bootstrap.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/jquery-ui-1.10.2.custom.min.js;~/js/myMesazh-IMB.2.1.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/myNrAuto-IMB.2.1.js;~/JsGlobal.js;~/js/myCookies-IMB.2.1.js;~/js/json2.js;~/js/arkiva.js;~/js/aspx.js/LupaArkiva.aspx-IMB.3.3.js&v58"
            type="text/javascript"></script>
</head>
<body >
<form id="form1" runat="server">
       
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                         Font-Size="9pt" Modal="True" ImagePosition="Top">
        <LoadingDivStyle Opacity="30">
        </LoadingDivStyle>
    </dx:ASPxLoadingPanel>
    <asp:ScriptManager ID="ScriptManager1" runat="server"> 

    </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ try{ window.parent.SessionTimeout.sendKeepAlive(); } catch(e){console.log(e)}}"/>--%>
    </dx:ASPxGlobalEvents>



    <dx:ASPxFileManager ID="fileManager" ClientInstanceName="fileManager" Width="100%" runat="server"
                        OnItemDeleting="fileManager_ItemDeleting" OnFileUploading="fileManager_FileUploading" OnItemRenaming="fileManager_ItemRenaming" OnItemCopying="fileManager_ItemCopying" OnItemMoving="fileManager_ItemMoving"
                        OnCustomCallback="fileManager_CustomCallback" OnPreRender="fileManager_PreRender">
          
        <SettingsToolbar ShowDownloadButton="true" ShowRefreshButton="true" ShowCreateButton="true" ShowFilterBox="true" ShowDeleteButton="true" ShowPath="true" ShowMoveButton="true" ShowRenameButton="true"/>
        <ClientSideEvents Init="Arkiva.fileManager_Init"/>
        <ClientSideEvents CustomCommand="Arkiva.OnCustomCommand" SelectedFileOpened="Arkiva.doubClickFile" ></ClientSideEvents>
        <Settings AllowedFileExtensions=".xls,.xlsx,.doc,.docx,.jpg,.jpeg,.gif,.rtf,.txt,.avi,.png,.mp3,.xml,.doc,.pdf,.ods,.odt" EnableMultiSelect="true" ThumbnailFolder="~/Arkiva/Thumbnails"/>
        <SettingsEditing AllowCreate="true" AllowDelete="true" AllowDownload="true" AllowCopy="false" AllowRename="true" AllowMove="true"/>

        <SettingsUpload UseAdvancedUploadMode="true" Enabled="true">
            <AdvancedModeSettings EnableMultiSelect="true"/>
        </SettingsUpload>

<SettingsDropbox TeamMemberID=""></SettingsDropbox>

        <SettingsPermissions>
            <AccessRules>
                <dx:FileManagerFolderAccessRule Browse="Deny" Path="TeFshire"/>
            </AccessRules>
        </SettingsPermissions>
    </dx:ASPxFileManager>


    <asp:HiddenField ID="HfKonfAmb" runat="server"/>
    <asp:HiddenField ID="hfKonffillestar" runat="server"/>
    <asp:HiddenField ID="hfShtimModifikim" runat="server"/>
    <dx:ASPxHiddenField ID="hfTeDrejta" runat="server" ClientInstanceName="hfTeDrejta"></dx:ASPxHiddenField>
    <asp:HiddenField ID="hfStatusi" runat="server"/>
    <asp:HiddenField ID="hfIdDok" runat="server"/>
    <asp:HiddenField ID="hfVeprimi" runat="server"/>
    <asp:HiddenField ID="hfVjenNga" runat="server"/>
    <dx:ASPxHiddenField ID="hfMyArkiva" runat="server" ClientInstanceName="hfMyArkiva"></dx:ASPxHiddenField>
    <dx:ASPxHiddenField ID="hfIdPerdorues" runat="server" ClientInstanceName="hfIdPerdorues"></dx:ASPxHiddenField>

</form>
</body>
</html>