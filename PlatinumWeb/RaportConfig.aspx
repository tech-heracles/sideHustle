<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RaportConfig.aspx.cs" Inherits="PlatinumWeb.RaportConfig" %>

<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxm" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxwgv" %>
<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dxe" %>
    <html>
<head runat="server">
     <title> Alpha Web</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding-left: 20%">
        <dxm:ASPxMenu ID="menuKryesore" ClientInstanceName="menuKryesore" runat="server"
            OnItemClick="menuKryesore_ItemClick">
        </dxm:ASPxMenu>
    </div>
    <table style="width: 100%">
        <tr>
            <td style="height: 100%; width: 20%; vertical-align: top;" rowspan="2">
                <dxwgv:ASPxGridView ID="grid_rapConfig" runat="server" ClientInstanceName="grid_rapConfig"
                       >
                    <Styles    >
                    </Styles>
                    <SettingsLoadingPanel Text="" />
                    <SettingsPager>
                        <AllButton>
                            <Image Height="19px" Width="27px" />
                        </AllButton>
                        <FirstPageButton>
                            <Image Height="19px" Width="23px" />
                        </FirstPageButton>
                        <LastPageButton>
                            <Image Height="19px" Width="23px" />
                        </LastPageButton>
                        <NextPageButton>
                            <Image Height="19px" Width="19px" />
                        </NextPageButton>
                        <PrevPageButton>
                            <Image Height="19px" Width="19px" />
                        </PrevPageButton>
                    </SettingsPager>
                    <Images  >
                        <CollapsedButton Height="15px"
                            Width="15px" />
                        <ExpandedButton Height="15px" 
                            Width="15px" />
                        <DetailCollapsedButton Height="15px" 
                            Width="15px" />
                        <DetailExpandedButton Height="15px" 
                            Width="15px" />
                        <HeaderFilter Height="19px"  Width="19px" />
                        <HeaderActiveFilter Height="19px" 
                            Width="19px" />
                        <HeaderSortDown Height="5px" 
                            Width="7px" />
                        <HeaderSortUp Height="5px"  Width="7px" />
                        <FilterRowButton Height="13px" Width="13px" />
                        <WindowResizer Height="13px"  Width="13px" />
                    </Images>
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <ImagesEditors>
                        <CalendarFastNavPrevYear Height="19px" 
                            Width="19px" />
                        <CalendarFastNavNextYear Height="19px" 
                            Width="19px" />
                        <DropDownEditDropDown Height="7px" 
                           
                            Width="9px" />
                        <SpinEditIncrement Height="6px" 
                           
                           
                            Width="7px" />
                        <SpinEditDecrement Height="7px"
                           
                            Width="7px" />
                        <SpinEditLargeIncrement Height="9px"
                           
                            Width="7px" />
                        <SpinEditLargeDecrement Height="9px" 
                           
                            Width="7px" />
                    </ImagesEditors>
                </dxwgv:ASPxGridView>
            </td>
            <td style="height: 10%; vertical-align: top;">
                <dxwgv:ASPxGridView ID="grid_previewRap" ClientInstanceName="grid_previewRap" runat="server"
                    Width="100%" Styles-Header-Font-Bold="true"  
                     >
                    <Styles    >
                    </Styles>
                    <SettingsLoadingPanel Text="" />
                    <SettingsPager>
                        <AllButton>
                            <Image Height="19px" Width="27px" />
                        </AllButton>
                        <FirstPageButton>
                            <Image Height="19px" Width="23px" />
                        </FirstPageButton>
                        <LastPageButton>
                            <Image Height="19px" Width="23px" />
                        </LastPageButton>
                        <NextPageButton>
                            <Image Height="19px" Width="19px" />
                        </NextPageButton>
                        <PrevPageButton>
                            <Image Height="19px" Width="19px" />
                        </PrevPageButton>
                    </SettingsPager>
                    <Images  >
                        <CollapsedButton Height="15px" 
                            Width="15px" />
                        <ExpandedButton Height="15px" 
                            Width="15px" />
                        <DetailCollapsedButton Height="15px" 
                            Width="15px" />
                        <DetailExpandedButton Height="15px" 
                            Width="15px" />
                        <HeaderFilter Height="19px"  Width="19px" />
                        <HeaderActiveFilter Height="19px" 
                            Width="19px" />
                        <HeaderSortDown Height="5px" 
                            Width="7px" />
                        <HeaderSortUp Height="5px"  Width="7px" />
                        <FilterRowButton Height="13px" Width="13px" />
                        <WindowResizer Height="13px"  Width="13px" />
                    </Images>
                    <StylesEditors>
                        <ProgressBar Height="25px">
                        </ProgressBar>
                    </StylesEditors>
                    <ImagesEditors>
                        <CalendarFastNavPrevYear Height="19px"
                            Width="19px" />
                        <CalendarFastNavNextYear Height="19px" 
                            Width="19px" />
                        <DropDownEditDropDown Height="7px" 
                            
                            Width="9px" />
                        <SpinEditIncrement Height="6px" 
                            
                            
                          
                            Width="7px" />
                        <SpinEditDecrement Height="7px"
                          
                            Width="7px" />
                        <SpinEditLargeIncrement Height="9px" 
                           
                            Width="7px" />
                        <SpinEditLargeDecrement Height="9px" 
                           
                            Width="7px" />
                    </ImagesEditors>
                </dxwgv:ASPxGridView>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>