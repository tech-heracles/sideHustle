<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuLeviz.ascx.cs" Inherits="PlatinumWeb.E_PaySlip.MenuLeviz" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
      
      <style type="text/css">
          .style1
          {
              font-weight: bold;
          }
      </style>
      <%-- <table width = "100%">
        <tr >            
        <td style = "width:50%">
            <dx:ASPxMenu ID="ASPxMenu1" ClientVisible="false"  runat="server" Orientation = "Vertical" Border-BorderWidth="0px"
                AutoSeparators="RootOnly" ClientIDMode="AutoID" Width = "100%"
                    ItemAutoWidth = "false"
                ImageSpacing="3px" ShowPopOutImages="True"  ItemStyle-Height = "1.5px"
                  Font-Size="12px">
            <Items>
            <dx:MenuItem Text="Poshtë" Name = "Poshtë" BeginGroup = "True">
               <Image Url="~/images/new/navigate_down1.png" ></Image>
            </dx:MenuItem>
            <dx:MenuItem Text = "Lart" Name ="Lart" BeginGroup = "True">
               <Image Url="~/images/new/navigate_up1.png"></Image>
            </dx:MenuItem>
           
            </Items>
                <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                </LoadingPanelImage>
                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" 
                    VerticalAlign="Middle" />
                <SubMenuStyle GutterWidth="0px" />
            </dx:ASPxMenu>
        </td>
        <td style = "width:50%">
        <dx:ASPxMenu ID="ASPxMenu2" runat="server" Orientation = "Vertical" Border-BorderWidth="0px"
                AutoSeparators="RootOnly" ClientIDMode="AutoID" Width = "100%"
                    ItemAutoWidth = "false"
                ImageSpacing="3px" ShowPopOutImages="True"  ItemStyle-Height = "1.5px"
                  Font-Size="12px">
            <Items>
            <dx:MenuItem Text="Fillim" Name = "Fillim" BeginGroup = "True">
                <Image Url="~/images/new/navigate_beginning1.png"></Image>
            </dx:MenuItem>
            <dx:MenuItem Text = "Fund" Name ="Fund" BeginGroup = "True">
                <Image Url="~/images/new/navigate_end1.png"></Image>
            </dx:MenuItem>
           
            </Items>
                <LoadingPanelImage Url="~/App_Themes/Aqua/Web/Loading.gif">
                </LoadingPanelImage>
                <RootItemSubMenuOffset FirstItemX="-1" FirstItemY="-1" X="-1" Y="-1" />
                <ItemStyle DropDownButtonSpacing="12px" PopOutImageSpacing="18px" 
                    VerticalAlign="Middle" />
                <SubMenuStyle GutterWidth="0px" />
            </dx:ASPxMenu>
      
        </td>
        </tr>
        </table>--%>
      <%--  <table>
        <tr>           
        <td>
         <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Para" ClientIDMode="AutoID" 
                    
                 
                Image-Url = "~/images/new/navigate_down11.png" ImagePosition ="Left" HorizontalAlign="Left"
                >
             <Image Url="~/images/new/navigate_down11.png">
             </Image>
            </dx:ASPxButton>
         </td>
        <td>
        <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Pas" ClientIDMode="AutoID" 
                    
                 
                Image-Url = "~/images/new/navigate_up11.png" ImagePosition ="Left" HorizontalAlign="Left"
                >
            <Image Url="~/images/new/navigate_up11.png">
            </Image>
            </dx:ASPxButton>
        </td>
        <td>
        <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Fillim" ClientIDMode="AutoID" 
                    
                 
                Image-Url = "~/images/new/navigate_beginning1.png" ImagePosition ="Left" 
                HorizontalAlign="Left">
            <Image Url="~/images/new/navigate_beginning1.png">
            </Image>
            </dx:ASPxButton>
        </td>
        <td>
         <dx:ASPxButton ID="ASPxButton4" runat="server" Text="Fund" ClientIDMode="AutoID" 
                    
                 
                Image-Url = "~/images/new/navigate_end1.png" ImagePosition ="Left" 
                HorizontalAlign="Left">
             <Image Url="~/images/new/navigate_end1.png">
             </Image>
            </dx:ASPxButton>
        </td>
       
        </tr>
        </table>--%>
      <%-- <table>
        <tr>           
        <td>
         <dx:ASPxButton ID="ASPxButton5" runat="server" Text="Para" ClientIDMode="AutoID" 
                    
                  Width = "100%"
                Image-Url = "~/images/new/navigate_down11.png"
                >
             <Image Url="~/images/new/navigate_down11.png">
             </Image>
            </dx:ASPxButton>
         </td>
        <td>
        <dx:ASPxButton ID="ASPxButton6" runat="server" Text="Pas" ClientIDMode="AutoID" 
                    
                  Width = "100%"
                Image-Url = "~/images/new/navigate_up11.png"
                >
            <Image Url="~/images/new/navigate_up11.png">
            </Image>
            </dx:ASPxButton>
        </td>
        </tr>
        <tr>
        
       
        <td>
        <dx:ASPxButton ID="ASPxButton7" runat="server" Text="Fillim" ClientIDMode="AutoID" 
                    
                  Width = "100%"
                Image-Url = "~/images/new/navigate_beginning1.png">
            <Image Url="~/images/new/navigate_beginning1.png">
            </Image>
            </dx:ASPxButton>
        </td>
        <td>
         <dx:ASPxButton ID="ASPxButton8" runat="server" Text="Fund" ClientIDMode="AutoID" 
                    
                  Width = "100%"
                Image-Url = "~/images/new/navigate_end1.png">
             <Image Url="~/images/new/navigate_end1.png">
             </Image>
            </dx:ASPxButton>
        </td>
       </tr>       
        </table>--%>
      <%--<table style = "height:28px">
        <tr>
        <td>
         <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Para" ClientIDMode="AutoID" 
                    
                  Width = "100%" Font-Size = "10px"              
                >
             
            </dx:ASPxButton>
        </td>
        </tr>
        <tr>
         <td>
         <dx:ASPxButton ID="ASPxButton2" runat="server" Text="Pas" ClientIDMode="AutoID" 
                    
                  Width = "100%" Font-Size = "10px"  
               
                >
            
            </dx:ASPxButton>
        </td>
        </tr>
         <tr>
        <td>
         <dx:ASPxButton ID="ASPxButton3" runat="server" Text="Fillim" ClientIDMode="AutoID" 
                    
                  Width = "100%" Font-Size = "10px"  
               >
           
            </dx:ASPxButton>
        </td>
        </tr>
        <tr style = "height:4px">
         <td>
         <dx:ASPxButton ID="ASPxButton4" runat="server" Text="Fund" ClientIDMode="AutoID" 
                    
                  Width = "78%" 
                 Font-Size = "10px" Height="16px">             
         </dx:ASPxButton>
        </td>
        </tr>
        </table>--%>

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
            
            <%--<asp:LinkButton ID="lnkPara" runat="server"   
                >Para</asp:LinkButton>   --%>       
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
        


