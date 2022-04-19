<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MenuExport.ascx.cs" Inherits="PlatinumWeb.MenuExport" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<table align="center">        
    <tr>
        <td>
            <dx:ASPxComboBox ID="cmbExport" runat="server" ClientInstanceName="cmbExport"  
                    AutoPostBack="False"
                CallbackPageSize="50">
                <LoadingPanelImage  >
                </LoadingPanelImage>
                <DropDownButton>
                    <Image>
                        <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                    </Image>
                </DropDownButton>
                <Items>
                    <dx:ListEditItem Text = "Pdf" Value="0" Selected = "true"/>
                    <dx:ListEditItem Text = "Excel Xlsx" Value="1" />
                    <dx:ListEditItem Text = "Excel Xls" Value="2" />
                    <dx:ListEditItem Text = "Text" Value="3" />
                    <dx:ListEditItem Text = "Html" Value="4" />
                </Items>
            </dx:ASPxComboBox>
        </td>
        <td>
            <dx:ASPxButton ID="exportButton" runat="server" Text="Exporto" ClientInstanceName="exportButton"
                CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="True"  
                   >
            </dx:ASPxButton>
        </td>
    </tr>
</table>