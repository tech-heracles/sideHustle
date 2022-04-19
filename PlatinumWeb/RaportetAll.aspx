<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaportetAll.aspx.cs" Inherits="PlatinumWeb.RaportetAll" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Alpha Web</title>
 <link href="bootstrap-3.3.6-dist/css/bootstrap-iso.css" rel="stylesheet" />
 <meta name="viewport" content="width=device-width,initial-scale=1.0" />
    <script src="DX.ashx?jsfileset=~/js/jquery-1.11.3.min.js;~/js/noty/jquery.noty.packaged.imb.js;~/js/noty/bootstrap.js;~/js/noty/relax.js;~/js/noty.defaults.js;~/js/myFaqeCelje-IMB.2.1.js;~/js/jquery.blockUI.js;~/js/Utils-IMB.2.1.js;~/js/aspx.js/RaportetAll.aspx-IMB.2.1.js&v76""
        type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
         
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
     </asp:ScriptManager>
    <dx:ASPxGlobalEvents ID="ASPxGlobalEvents1" runat="server">
        <%--<ClientSideEvents EndCallback="function(s,e){ window.parent.SessionTimeout.sendKeepAlive();}" />--%>
    </dx:ASPxGlobalEvents>
<%--    <asp:UpdatePanel ID="updreports" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
        <ContentTemplate>--%>
            <asp:SqlDataSource ID="SqlDataSourceRaportet" runat="server">
               <%-- <SelectParameters>
                    <asp:ControlParameter ControlID="cmbmodules" PropertyName="Value" DbType="Int32"
                        Name="IDMODULI" DefaultValue="7" />
                </SelectParameters>--%>
            </asp:SqlDataSource>
            <table style="border: 1px solid #999999; background-image: url('images/grad_bg_image.gif');
                background-repeat: repeat-x; height: 85px; width: 100%;">
                <tr>
                    <td style="width: 15%; padding-left: 20px;">
                        <dx:ASPxLabel ID="listaRaporteve" runat="server" Text="Lista e raporteve" Font-Bold="false"
                            Font-Italic="False" Font-Size="16pt" ForeColor="Gray">
                        </dx:ASPxLabel>
                    </td>
                    <td align="right">
                        <table width="100%">
                            <tr>
                                <td align="right">
                                    <dx:ASPxLabel ID="lbld" runat="server" Text="Datë dokumenti" Style="color: #003366;
                                        font-size: 10pt; font-family: Tahoma; color: #666666;" Width="100px">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxRadioButtonList ID="ASPxRadioButtonList1" runat="server" RepeatColumns="3"
                                        ClientIDMode="AutoID" ClientInstanceName="btnradPeriudha" SelectedIndex="0" Width="100%"
                                        TextSpacing="2px" Border-BorderWidth="0px">
                                        <ClientSideEvents ValueChanged="function(s,e){RaportetAll.klickselectedvaluedok(s,e);}" Init="function(s,e){RaportetAll.klickselectedvaluedok(s,e);}" />
                                        <Items>
                                            <dx:ListEditItem Text="Aktuale" Value="0" Selected="True" />
                                            <dx:ListEditItem Text="Periudha" Value="1" />
                                            <dx:ListEditItem Text="Viti ushtrimor" Value="2" />
                                        </Items>
                                    </dx:ASPxRadioButtonList>
                                </td>
                                <td align="center">
                                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Nga" Width="30px" Style="color: #666666">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtdoknga" runat="server" ClientIDMode="AutoID" AutoPostBack="false"
                                        AllowNull="false" DateOnError="Today" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Date="2009-05-06" ValidationSettings-CausesValidation="True" ShowShadow="False"
                                        ClientInstanceName="dtdoknga" Width="100px">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td align="center">
                                    <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Deri" Width="30px" Style="color: #666666">
                                    </dx:ASPxLabel>
                                </td>
                                <td>
                                    <dx:ASPxDateEdit ID="dtdokderi" runat="server" ClientIDMode="AutoID" AutoPostBack="false"
                                        AllowNull="false" DateOnError="Today" EditFormat="Custom" EditFormatString="dd/MM/yyyy"
                                        Date="2009-05-06" ValidationSettings-CausesValidation="True" ShowShadow="False"
                                        ClientInstanceName="dtdokderi" Width="100px">
                                        <CalendarProperties>
                                            <HeaderStyle Spacing="1px" />
                                            <FooterStyle Spacing="17px" />
                                        </CalendarProperties>
                                        <DropDownButton>
                                            <Image>
                                                <SpriteProperties HottrackedCssClass="dxEditors_edtDropDownHover_Aqua" PressedCssClass="dxEditors_edtDropDownPressed_Aqua" />
                                            </Image>
                                        </DropDownButton>
                                        <ValidationSettings>
                                            <ErrorFrameStyle ImageSpacing="4px">
                                                <ErrorTextPaddings PaddingLeft="4px" />
                                            </ErrorFrameStyle>
                                        </ValidationSettings>
                                    </dx:ASPxDateEdit>
                                </td>
                                <td style="width: 2px">
                                </td>
                                <td valign="top" align="center" style="width: 18%; padding-bottom: 15px;">
                                    <dx:ASPxLabel ID="lblpersonalizuara" runat="server" Text="Raportet e personalizuara"
                                        Style="color: #003366; font-size: 10pt; font-family: Tahoma; color: #666666;">
                                    </dx:ASPxLabel>
                                    <dx:ASPxComboBox ID="cmbmodules" DataSourceID="SqlDataSourceModules" runat="server"
                                        TextField="MODULIPERSHK" ValueField="IDMODULI" AutoPostBack="True">
                                    </dx:ASPxComboBox>
                                    <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSourceRaportet">
                                        <ItemTemplate>
                                            <br />
                                            <img src="images/document_edit.png" alt="" />
                                            <dx:ASPxHyperLink runat="server" Font-Bold="false" Font-Size="10" NavigateUrl="javascript:void('0')"
                                                ID="HPL1" Text='<%# Eval("RAPEMRI") %>'>
                                            </dx:ASPxHyperLink>
                                            <dx:ASPxButton ID="filtroButton" runat="server" EnableTheming="False" 
                                                AutoPostBack="False" EnableViewState="False" NavigateUrl="javascript:void('0')" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                                <td>
                                    <table align="right" width="15%">
                                        <tr style="width: 15%">
                                            <td align="right" style="padding-bottom: 2px;">
                                                <dx:ASPxButton ID="btnHelp" runat="server" Text="" ClientInstanceName="btnHelp" Width="40"
                                                    Height="20" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                                    ImagePosition="Bottom" HorizontalAlign="Center" ToolTip="Ndihme" OnInit="btnHelp_Init">
                                                    <Image Url="~/images/new/help_14.png">
                                                    </Image>
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="padding-bottom: 2px;">
                                                <dx:ASPxButton ID="btnCollapseAll" runat="server" Text="-" ClientInstanceName="btnCollapseAll"
                                                    Width="40" Height="25" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                                    Font-Bold="True">
                                                    <ClientSideEvents Click="function(s,e){RaportetAll.collapseAll(s,e);}" />
                                                </dx:ASPxButton>
                                            </td>
                                            <td align="right" style="padding-bottom: 2px;">
                                                <dx:ASPxButton ID="btnExpandAll" runat="server" Text="+" ClientInstanceName="btnExpandAll"
                                                    Width="40" Height="25" CausesValidation="False" ClientIDMode="AutoID" AutoPostBack="false"
                                                    Font-Bold="True">
                                                    <ClientSideEvents Click="function(s,e){RaportetAll.expandAll(s,e);}" />
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table width="100%" style="border-style: none solid solid solid; border-width: 1px;
                border-color: #999999; background-color: #FCFCFC;">
                <tr>
                    <td style="width: 100%; padding-left: 0px; padding: 0px;">
                        <dx:ASPxSiteMapControl ID="stmapmodules" runat="server" ClientIDMode="AutoID" MaximumDisplayLevels="5"
                            Width="100%">
                            <DefaultLevelProperties NodeSpacing="8px" ImageSpacing="10px" Font-Italic="False"
                                Font-Underline="False" ForeColor="#666666" Border-BorderStyle="NotSet">
                                <Font Italic="False" Underline="False"></Font>
                                <ChildNodesPaddings PaddingBottom="5px" PaddingLeft="0px" PaddingTop="5px" />
                                <NodePaddings PaddingRight="50px" />
                                <CurrentNodeStyle Font-Bold="True" Font-Underline="False" />
                            </DefaultLevelProperties>
                            <LevelProperties>
                                <dx:LevelProperties NodeSpacing="20px" Font-Italic="False" Image-Url="images/t7.gif"
                                    Image-Width="21" Image-Height="21" Font-Size="Medium" ForeColor="#FF9900" Font-Bold="True">
                                    <BorderBottom BorderWidth="0px" />
                                    <Font Underline="false" Bold="False"></Font>
                                    <Image Height="21px" Url="images/t7.gif" Width="21px">
                                    </Image>
                                    <ChildNodesPaddings PaddingBottom="5px" PaddingLeft="12px" PaddingTop="17px" />
                                    <NodePaddings PaddingBottom="1px" PaddingLeft="7px" />
                                </dx:LevelProperties>
                                <dx:LevelProperties>
                                    <BorderBottom BorderColor="#EBEBEB" BorderStyle="Solid" BorderWidth="1px" />
                                    <Image>
                                        <SpriteProperties CssClass="dxWeb_smLevelBullet_Aqua" />
                                    </Image>
                                    <NodePaddings PaddingBottom="4px" PaddingTop="3px" />
                                    <NodeTemplate>
                                        <table border="0" cellpadding="0" cellspacing="0" style="">
                                            <tr>
                                                <td style="white-space: nowrap; padding-right: 7px;">
                                                  
                                                     <dx:ASPxHyperLink ForeColor="#666666" ID="Label1" runat="server" Text='<%# Eval("Title") %>'
                                                        NavigateUrl='<%# Eval("Url") %>' ImageUrl="~/images/filter2.png" EnableTheming="false" />
                                                </td>
                                                <td style="white-space: nowrap; padding: 3px;">
                                                    
                                                </td>
                                                <td>
                                                
                                            </td>

                                            </tr>
                                        </table>
                                    </NodeTemplate>
                                </dx:LevelProperties>
                                <dx:LevelProperties>
                                    <Image>
                                        <SpriteProperties CssClass="dxWeb_smLevelBullet_Aqua" />
                                    </Image>
                                </dx:LevelProperties>
                                <dx:LevelProperties>
                                    <Image>
                                        <SpriteProperties CssClass="dxWeb_smLevelBullet_Aqua" />
                                    </Image>
                                </dx:LevelProperties>
                                <dx:LevelProperties>
                                    <Image>
                                        <SpriteProperties CssClass="dxWeb_smLevelBullet_Aqua" />
                                    </Image>
                                </dx:LevelProperties>
                            </LevelProperties>
                            <Columns>
                                <dx:SiteMapColumn Width="25%">
                                </dx:SiteMapColumn>
                                <dx:SiteMapColumn Width="25%">
                                </dx:SiteMapColumn>
                                <dx:SiteMapColumn Width="25%">
                                </dx:SiteMapColumn>
                            </Columns>
                            <ColumnSeparatorStyle Width="20px">
                                <Paddings Padding="0px" />
                            </ColumnSeparatorStyle>
                            <LinkStyle HoverColor="#F99006" VisitedColor="#F99006">
                            </LinkStyle>
                            <Border BorderWidth="0px" />
                        </dx:ASPxSiteMapControl>
                    </td>
                </tr>
            </table>
<%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server">
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSourceModules" runat="server">
    </asp:SqlDataSource>
    </form>
</body>
</html>
