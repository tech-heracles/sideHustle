using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DbCore.DbAdmin;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Resources;
using PlatinumWeb.ApplicationUtils.Pages;


namespace PlatinumWeb
{
    public partial class Restore : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //if (CacheLayer.GlobalCacheManager.MySessionCache["LoggedIn"].Equals("No"))
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            int idPerd = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            //if (CacheLayer.GlobalCacheManager.MySessionCache["KodiNdermarrjes"] == null)
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerd);
            }
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        
            if (!IsPostBack)
            {
                EmrateTabeve();


            }
            // clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            //clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "", pnlMesazhi);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            ASPxPageControl1.TabPages[0].Text = rm.GetString("MenuItemRestore", cultinf);
            ASPxLabel1.Text = rm.GetString("TabItemRestore", cultinf);
        }

          /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            clsToolbarConfig.percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), idViti, idPerdorues, idNdermarrje, aSPxMenu1, "Restore.aspx", this, MenuInfo, true, false, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session));
        }
     
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(ASPxMenu1, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
        
        }
        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "Restore")
            {
                Page.Validate();

            }

        }


        protected void ucFile_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {
            //    DevExpress.Web.UploadedFile f = ucFile.UploadedFiles[0];
            //    DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true);

            //    clsNdermarrje nderm = new clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    mesazh = nderm.restore(f.FileContent, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), nderm.NdermarrjeKodi, nderm.IdLicenca);
            ///nestila prova per importin e artikujve
            //////System.Data.DataTable table = new System.Data.DataTable();
            //////if (f.FileName.EndsWith(".txt"))
            //////{
            //////    StreamReader sr = new StreamReader(f.FileContent);
            //////    string[] emrat;
            //////    if (!sr.EndOfStream)
            //////    {
            //////        emrat = sr.ReadLine().Split('\t');

            //////        foreach (string em in emrat)
            //////            table.Columns.Add(em);
            //////        while (!sr.EndOfStream)
            //////        {
            //////            string line = sr.ReadLine();
            //////            string[] arr = line.Split('\t');
            //////            table.Rows.Add(arr);

            //////        }
            //////    }
            //////    sr.Close();
            //////    sr.Dispose();
            //////}
            //////else
            //////{
            //////    IExcelDataReader excelReader;
            //////    if (f.FileName.EndsWith(".xls"))
            //////        //1. Reading from a binary Excel file ('97-2003 format; *.xls)
            //////        excelReader = ExcelReaderFactory.CreateBinaryReader(f.FileContent);
            //////    //...
            //////    //2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
            //////    else excelReader = ExcelReaderFactory.CreateOpenXmlReader(f.FileContent);

            //////    excelReader.IsFirstRowAsColumnNames = true;
            //////    DataSet result = excelReader.AsDataSet();
            //////    table = result.Tables[0];
            //////    excelReader.Close();
            //////}
            //////DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            //////konf.mbushKonfigAmbjSipasKod("ART", DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));

            //////foreach (DataRow dr in table.Rows)
            //////{
            //////    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
            //////    decimal koeficenti = 0, peshabruto = 0, peshaneto = 0, minimum = 0, maksimum = 0;
            //////    decimal.TryParse(dr["KOEFICENTARTIKULLI"].ToString(), out koeficenti);
            //////    decimal.TryParse(dr["PESHABRUTOARTIKULLI"].ToString(), out peshabruto);
            //////    decimal.TryParse(dr["PESHANETOARTIKULLI"].ToString(), out peshaneto);
            //////    decimal.TryParse(dr["MINIMUMARTIKULLI"].ToString(), out minimum);
            //////    decimal.TryParse(dr["MAXIMUMARTIKULLI"].ToString(), out maksimum);
            //////    DbCore.DbRegjistrim.clsTaksa taksa = new DbCore.DbRegjistrim.clsTaksa(dr["IDTVSH"].ToString(), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //////    try
            //////    {
            //////        art = art.krijoArtikullPerImport(dr["KODARTIKULLI"].ToString(), dr["PERSHKRIMARTIKULLI"].ToString(), dr["PERSHKRIMIANGARTIKULLI"].ToString(), dr["KODIDOGANORARTIKULLI"].ToString(), dr["VENDODHJEARTIKULLI"].ToString(), dr["KODIFIKIMI1ARTIKULLI"].ToString(), dr["KODIFIKIMI2ARTIKULLI"].ToString(), dr["ORIGJINEARTIKULLI"].ToString(), dr["NJESI1ARTIKULLI"].ToString(), dr["NJESI2ARTIKULLI"].ToString(), koeficenti, dr["IDFURNITORIKRYESOR"].ToString(), peshabruto, peshaneto, bool.Parse(dr["DETAJIMARTIKULLI"].ToString()), dr["KLASA"].ToString(), dr["IDSKEMAKONTABILITETIARTIKULLI"].ToString(), dr["IDLLOGARIINVENTARI"].ToString(), dr["IDLLOGARIBLERJE"].ToString(), dr["IDLLOGARISHITJE"].ToString(), dr["IDLLOGARITETRETE"].ToString(), dr["IDLLOGARISHPENZIME"].ToString(), dr["IDLLOGARIAMORTIZIMI"].ToString(), minimum, maksimum, dr["METODEKOSTOJEARTIKULLI"].ToString(), int.Parse(dr["LLOGARITJAKMSHARTIKULLI"].ToString()), int.Parse(dr["ZEVENDESIMAUTOMATIKARTIKULLI"].ToString()), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), bool.Parse(dr["AKTIV"].ToString()), bool.Parse(dr["KONTROLLGJENDJEARTIKULLI"].ToString()), bool.Parse(dr["KONTROLLGJENDJE"].ToString()), bool.Parse(dr["KONTROLLCMIMI"].ToString()), null, new DbCore.DbInventari.colFurnitoreArtikujsh(), new DbCore.DbInventari.colArtikujtZevendesues(), new colVleraFushaShtese(), new DbCore.DbKontabiliteti.colBuxhetet(), new DbCore.DbInventari.colDetajimePerArt(), false, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), dr["KODBARI"].ToString(), taksa.IdTaksa, konf.IdKonfigAmbjente, "", true);
            //////        DbCore.clsMesazh mesazhinv = art.ruajArtikull(art.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, art.PershkrimiAngArtikulli, art.KodiDoganorArtikulli,
            //////                     art.VendodhjeArtikulli, art.Kodifikimi1Artikulli, art.Kodifikimi2Artikulli, art.OrigjineArtikulli, art.Njesi1Artikulli, art.Njesi2Artikulli,
            //////                     art.KoeficientArtikulli, art.IdFurnitoriKryesor, art.PeshaBrutoArtikulli, art.PeshaNetoArtikulli, art.DetajimArtikulli, art.Klasa,
            //////                     art.IdSkemaKontabilitetiArtikulli, art.IdLlogariInventari, art.IdLlogariBlerje, art.IdLlogariShitje, art.IdLlogariTeTrete, art.IdLlogariShpenzime, art.IdLlogariAmortizimi,
            //////                     art.MinimumArtikulli, art.MaximumArtikulli, art.MetodeKostojeArtikulli, art.LlogaritjaKMSHArtikulli, art.ZevendesimAutomatikArtikulli, art.IdPerdoruesi,
            //////                     art.Aktiv, art.ColArtikujPerberes, 0, "", "", art.OColFurnitoreArtikujsh, art.OColArtikujtZevendesues, art.OColVleraFushaShtese,
            //////                     art.OColBuxhetet, art.OColDetajime, art.IdStatusDok, art.LlojiArt, new DbCore.DbInventari.colCmimeArtikujsh());
            //////        if (!mesazhinv.StatusMesazhi)
            //////        {
            //////            e.CallbackData += "Art " + dr["KODARTIKULLI"].ToString() + " :" + mesazhinv.PershkrimMesazhi;
            //////        }
            //////    }
            //////    catch (Exception ex)
            //////    {
            //////        e.CallbackData += "Art " + dr["KODARTIKULLI"].ToString() + " :" + ex.Message;
            //////    }

            //////}




            //if (mesazh.Status)
            //{
            //    e.IsValid = true;
            //    e.CallbackData = "Restori perfundoi me sukses!";
            //}
            //else
            //{
            //    e.IsValid = false;
            //    e.CallbackData = mesazh.PershkrimMesazhi;
            //}

        }
    }
}