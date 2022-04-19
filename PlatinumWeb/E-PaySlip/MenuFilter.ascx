<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuFilter.ascx.cs"
    Inherits="PlatinumWeb.E_PaySlip.MenuFilter" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>



<%--<script src="js/jquery-1.10.2.min.js" type="text/javascript"></script>--%>
  <script type="text/javascript">
      function ruajf(s,e) {
          Utils.shfaqLoadingGif();;
         
          if ($("input[id$='hfRuajFiltra']").val() == 'po') 
           ruajFiltra(s,e); 
      }
  </script>
<table align="center">
    <tr>
        <td>
            <dx:ASPxComboBox ID="btnFiltra" runat="server" ClientInstanceName="btnFiltra"  
                    AutoPostBack="False"
                CallbackPageSize="50" OnItemRequestedByValue="btnFiltra_TextBox_ItemRequestedByValue">
                <LoadingPanelImage  >
                </LoadingPanelImage>
                <DropDownButton>
                    <Image>
                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                    </Image>
                </DropDownButton>
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="Button1" runat="server" Text="Ruaj" ClientInstanceName="btnRuaj"
                CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="True"  
                   >
                <ClientSideEvents Click="function(s, e) { ruajf(s,e)}" />
            </dx:ASPxButton>
        </td>
        <td>
            <dx:ASPxButton ID="btnFshi" runat="server" ClientInstanceName="btnFshi" CausesValidation="False"
                ClientIDMode="AutoID" AutoPostBack="True"  
                    ImagePosition="Bottom"
                HorizontalAlign="Center" ToolTip="Fshi filter te grides" VerticalAlign="Middle">
                <ClientSideEvents Click="function(s, e) { Utils.shfaqLoadingGif();; }" />
                <Image Url="../images/new/button_cancel-32.png">
                </Image>
            </dx:ASPxButton>
        </td>
    </tr>
</table>
<asp:HiddenField ID="hfRuajFiltra" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server"
                     ClientInstanceName="LoadingPanel" Font-Size="9pt"
                       
                       Modal="True" ImagePosition="Top" >
                    
                     <LoadingDivStyle Opacity="30"></LoadingDivStyle>
    </dx:ASPxLoadingPanel>
