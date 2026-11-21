<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuLeviz.ascx.cs" Inherits="PlatinumWeb.MenuLeviz" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
      
      <style type="text/css">
          .style1
          {
              font-weight: bold;
          }
      </style>

        <table style="height: 100%">
        <tr>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td style = "text-align:right">         
            <dx:ASPxImage ID="imgPara" runat="server" ImageUrl = "~/images/new/navigate_down11.png">
            </dx:ASPxImage>
        </td>
        <td style = "text-align:left">
            <dx:ASPxHyperLink ID="lnkPara" runat="server" Text="Para" ClientInstanceName = "lnkPara"
                ClientIDMode="AutoID"   
                 >
            </dx:ASPxHyperLink>  
            
        </td> 
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>      
        <td style = "text-align:right"> 
            <dx:ASPxImage ID="imgPas" runat="server" 
                ImageUrl = "~/images/new/navigate_up11.png" Height="19px" Width="19px">
            </dx:ASPxImage>
        </td>
         <td style = "text-align:left">
          <dx:ASPxHyperLink ID="lnkPas" runat="server" Text="Pas" ClientInstanceName = "lnkPas"
                 ClientIDMode="AutoID"   
                   >
            </dx:ASPxHyperLink>
        </td>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
         <tr>
         <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
         <td style = "text-align:right">   
            <dx:ASPxImage ID="imgFillim" runat="server" ImageUrl = "~/images/new/navigate_beginning11.png">
            </dx:ASPxImage>
         </td>
        <td style = "text-align:left">
         <dx:ASPxHyperLink ID="lnkFillim" runat="server" Text="Fillim" ClientInstanceName = "lnkFillim"
                ClientIDMode="AutoID"   
                  >                
         </dx:ASPxHyperLink>
        </td>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
        <td style = "text-align:right">
            <dx:ASPxImage ID="imgFund" runat="server" ImageUrl = "~/images/new/navigate_end11.png">
            </dx:ASPxImage>
        </td>
         <td style = "text-align:left">
          <dx:ASPxHyperLink ID="lnkFund" runat="server" Text="Fund" ClientInstanceName = "lnkFund"
                 ClientIDMode="AutoID"   
                   >
            </dx:ASPxHyperLink>
        </td>
        <td> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        </table>
        


