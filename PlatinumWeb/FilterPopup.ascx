<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilterPopup.ascx.cs"
    Inherits="PlatinumWeb.FilterPopup" %>
	<%@ Register Assembly="DevExpress.Web.v18.2, Version=18.2.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>


<style type="text/css">
    img
    {
        border-width: 0;
    }
</style>
<script type="text/javascript">
  //todo nedjan title panel per select on page,poshte te jete vetem ok dhe cancel,buttonat qe kane te bejne me selectin te jene siper

    var FilterPopup = {
        pivotGrida: undefined,
        getPivotGrid:function(){    
                if (Utils.IsNullOrEmpty(DrillDownWindow.cpPivotGridName)) {
                    alert("vendos emrin e pivotgrides ne popup");
                    return;
                }
                return  window[DrillDownWindow.cpPivotGridName];
        },
        selectAllClick : function (s, e) {
            if (s.GetText() == 'Show All') {
                GridView.PerformCallback('ShowAll');
                s.SetText('Hide All')
            }
            else {
                GridView.PerformCallback('HideAll');
                s.SetText('Show All')
            }
        },
        ClosePopupWindow: function() {
            DrillDownWindow.HideWindow();
            GridView.PerformCallback("ClearGrid");
        },
        filterOkClick: function () {
            GridView.GetSelectedFieldValues("FilterValue", function (values) {
                FilterPopup.getPivotGrid().PerformCallback(values);
            });
            FilterPopup.ClosePopupWindow();
        },
        InvertFilter: function (s, e) {
            GridView.PerformCallback('InvertFilter');
        }


 };
    
</script>

<dx:ASPxPopupControl EnableHierarchyRecreation="false" ID="ASPxPopupControl1" Modal="true" runat="server" Height="1px" AllowResize="false"
    AllowDragging="True" ClientInstanceName="DrillDownWindow" Width="300px"  CloseAction="CloseButton"  Border-BorderWidth="0">
    <Border BorderWidth="0px"></Border>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <table width="100%">
                <tr>
                    <td>
                        <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" ClientInstanceName="GridView"
                            OnCustomCallback="ASPxGridView1_CustomCallback" KeyFieldName="FilterValue" Width="100%">
                            <Columns>
                                <dx:GridViewCommandColumn ShowInCustomizationForm="True" Width="20px" ShowSelectCheckbox="True"
                                    VisibleIndex="0">
                                </dx:GridViewCommandColumn>
                                <dx:GridViewDataTextColumn FieldName="FilterValue" VisibleIndex="2">
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <SettingsBehavior AllowDragDrop="False" AllowGroup="False" 
                                AllowSelectByRowClick="True" />
                            <SettingsPager  Mode="EndlessPaging">
                                
                            </SettingsPager>
                            <Settings GridLines="None" ShowColumnHeaders="False" ShowFilterRow="True" />
                            <Styles>
                                <SelectedRow BackColor="White" Font-Bold="True" ForeColor="#003300">
                                </SelectedRow>
                            </Styles>
                        </dx:ASPxGridView>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <dx:ASPxButton ID="buttonSelectAll" runat="server" AutoPostBack="False" EnableClientSideAPI="True"
                                        Text="Hide All">
                                        <ClientSideEvents Click="FilterPopup.selectAllClick" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="buttonFilterInver" runat="server" AutoPostBack="False" EnableClientSideAPI="True"
                                        Text="Invert">
                                        <ClientSideEvents Click="FilterPopup.InvertFilter" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="buttonFilterOk" runat="server" AutoPostBack="False" EnableClientSideAPI="True"
                                        Text="Ok">
                                        <ClientSideEvents Click="FilterPopup.filterOkClick" />
                                    </dx:ASPxButton>
                                </td>
                                <td>
                                    <dx:ASPxButton ID="buttonFilterCancel" runat="server" AutoPostBack="False" EnableClientSideAPI="True"
                                        Text="Cancel">
                                        <ClientSideEvents Click="FilterPopup.ClosePopupWindow" />
                                    </dx:ASPxButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl >
