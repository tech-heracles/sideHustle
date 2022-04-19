using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Threading;
using System.Text;
using DevExpress.Web;
using System.Globalization;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaEksportim : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspxWebControlUtils.konfiguroMenuRuajPerLupaPerEksport(ASPxMenu1, this);
                //clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
                DbCore.DbAdmin.clsKokaFormatImporti kokaFormatImporti = new DbCore.DbAdmin.clsKokaFormatImporti(int.Parse(Request.QueryString["id"]));
                txtEmerSkedari.Text = kokaFormatImporti.Kodi;
                txtEmerSheet.Text = kokaFormatImporti.Kodi;
            }
        }

        private void krijoFile(int id)
        {
            DbCore.DbAdmin.clsKokaFormatImporti koka = new DbCore.DbAdmin.clsKokaFormatImporti(id);
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            DbCore.DbAdmin.colTrupiFormatImporti coltrup = DbCore.DbAdmin.colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(id, true);
         
            foreach (DbCore.DbAdmin.clsTrupiFormatImporti trup in coltrup)
            {
                if (koka.IdKategori == 98 && trup.KodKontrolli == "Fillim periudhe")//kur kemi listorare krijojme kolona per cdo date midis periudhave
                {
                    DateTime datefillimi, datembarimi;
                    DateTime.TryParse(trup.VleraDefault, out datefillimi);
                    DateTime.TryParse(coltrup.Find(x => x.KodKontrolli == "Mbarim periudhe").VleraDefault, out datembarimi);
                    for (DateTime i = datefillimi; i <= datembarimi; i = i.AddDays(1))
                    {
                        GridViewDataColumn gvs = new GridViewDataColumn();
                        gvs.Name = i.ToShortDateString();
                        gvs.Caption = i.ToString("dddd, d MMMM yyyy", cultinf);
                        ASPxGridView1.Columns.Add(gvs);
                    }
                    continue;
                }
                if (koka.IdKategori == 98 && trup.KodKontrolli == "Mbarim periudhe")
                    continue;
                if (trup.Shfaq)
                {
                    GridViewDataColumn gvs = new GridViewDataColumn();
                    gvs.Name = trup.EmerImporti;
                    gvs.Caption = trup.EmerImporti;

                    ASPxGridView1.Columns.Add(gvs);
                }
            }
            if (rbTipi.Value.ToString() == "CSV")
            {
                string simbolndares = txtSimboliNdares.Text;
                if (cbSimboliNdares.Checked)
                    simbolndares = "\t";

                try
                {
                    DevExpress.XtraPrinting.CsvExportOptionsEx o = new DevExpress.XtraPrinting.CsvExportOptionsEx();
                    o.Separator = simbolndares;
                    o.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text;
                    ASPxGridViewExporter1.WriteCsvToResponse(txtEmerSkedari.Text, o);
                }
                catch (Exception err)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim gjate krijimit te dokumentit", pnlMesazhi);
                }
            }
            else if (rbTipi.Value.ToString() == "XLSX")
            {
                try
                {
                    DevExpress.XtraPrinting.XlsxExportOptionsEx o = new DevExpress.XtraPrinting.XlsxExportOptionsEx();
                    if (txtEmerSheet.Text != "") o.SheetName = txtEmerSheet.Text;
                    ASPxGridViewExporter1.WriteXlsxToResponse(txtEmerSkedari.Text, o);
                }
                catch (Exception err)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim gjate krijimit te dokumentit", pnlMesazhi);
                }
            }
            else
            {
                try
                {
                    DevExpress.XtraPrinting.XlsExportOptionsEx o = new DevExpress.XtraPrinting.XlsExportOptionsEx();
                    if (txtEmerSheet.Text != "") o.SheetName = txtEmerSheet.Text;
                    ASPxGridViewExporter1.WriteXlsToResponse(txtEmerSkedari.Text, o);
                }
                catch (Exception err)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim gjate krijimit te dokumentit", pnlMesazhi);
                }
            }
        }

        protected void ExportoGride(object sender, EventArgs e)
        {
                int id = int.Parse(Request.QueryString["id"]);
                krijoFile(id);
        }
    }
}