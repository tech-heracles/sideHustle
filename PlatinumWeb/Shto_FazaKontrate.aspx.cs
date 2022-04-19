
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAnalizeBuxheti;
using DbCore.DbRegjistrim;
using DevExpress.Web;
using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_FazaKontrate : MyPageBase
    {
        private int idNdermarrje;
        private int idKonfig;
        private int idPerdoruesi;
        private int idGjuha;
        private int idViti;
        private int idKontrata;
 
        private string merrNgaSesioni;
        private const string komponente = "Shto_FazaKontrate.aspx";
        private const string idKomponente = "3031";

  

        protected void Page_Load(object sender, EventArgs e)
         {
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }

            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
            idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int.TryParse(Request.QueryString["id"], out idKontrata);
            merrNgaSesioni = Request.QueryString["merrngasesioni"];
            if (!IsPostBack)
            {
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfId.Value = idKontrata.ToString();              
                hfKonffillestar.Value = "1";

                if (merrNgaSesioni == "false")
                {
                
                    mbushGridenNgaDB(idNdermarrje, idKontrata);
                    GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvFazat", gvFazat, "LF", idKomponente, idGjuha);
                    konfiguroGride();
                } 
                else
                {
                    mbushGrideNgaSession(idNdermarrje, idKontrata);
                    GridUtil.PercaktoVisibleColumnsAndTypesGridSipasKodKonfigurimi(idNdermarrje, "gvFazat", gvFazat, "LF", idKomponente, idGjuha);
                    konfiguroGride();
                }
            }
            else
            {
                idNdermarrje = (int)hfState.Get("idNdermarrje");
                idPerdoruesi = (int)hfState.Get("idPerdoruesi");
                idGjuha = (int)hfState.Get("idGjuha");

                mbushGrideNgaSession(idNdermarrje,idKontrata);
                konfiguroGride();
            }
            percaktoTemplateMenu(rm,ci);
        }

        private void konfiguroGride()
        {


            GridUtil.ShtoButtonFshi(gvFazat);
            GridUtil.konfiguroGridaPerBatchEditing(gvFazat, true,true , true, false);
           

        }
      

        /// <summary>
        /// mbush datasourcein e grides direkt nga db
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void mbushGridenNgaDB(int idNdermarrje, int idkontrata)
        {
            colFazaKontrate col = new colFazaKontrate(idNdermarrje, idkontrata);
            gvFazat.DataSource = col;
            gvFazat.DataBind();
          
            mySessionObjects.ruajObjectNeSesion(Session, col, "fazat");
        }

        /// <summary>
        /// merr ds e grides nga sessioni
        /// </summary>
        public void mbushGrideNgaSession(int idNdermarrje, int idkontrata)
        {
            object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "fazat");
            if (tmp == null)
            {
                tmp = new colFazaKontrate(idNdermarrje, idkontrata);
            }
            gvFazat.DataSource = tmp;
            gvFazat.DataBind();
        }

        private void percaktoTemplateMenu(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            clsToolbarConfig.percaktoTemplateMenu(idGjuha, idViti, idPerdoruesi, idNdermarrje, ASPxMenu1, komponente, this, MenuInfo, Ruaj_ASPxButton_Click, FshiFilter_ASPxButton_Click, false, true, false, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), true);
            ASPxMenu1.Items.FindByName("Anullo").Text = rm.GetString("MenuItemMbyll", ci);
        }

        public void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            
        }

        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {
        }

        protected void gvFazat_BatchUpdate(object sender, DevExpress.Web.Data.ASPxDataBatchUpdateEventArgs e)
        {
            try
            {
              
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                object tmp = mySessionObjects.merrObjectNgaSesioni(Session, "fazat");
                clsMesazh mesazhi = new clsMesazh(true);

                colFazaKontrate col = (tmp as colFazaKontrate) ?? new colFazaKontrate(idNdermarrje, idKontrata);

                for (int i = 0; i < e.UpdateValues.Count; i++)
                {
                    clsFazaKontrate oldRreshti = col.Where(x => x.IdFaza == e.UpdateValues[i].MerrKeyValue<int>()).FirstOrDefault();

                    clsFazaKontrate newRreshti = e.UpdateValues[i].MerrCustomUpdatedObject<clsFazaKontrate>(oldRreshti);
                    newRreshti.IdKrijues = idPerdoruesi;
                  //  mesazhi = newRreshti.modifiko();
                }

                for (int i = 0; i < e.InsertValues.Count; i++)
                {
                    clsFazaKontrate newRreshti = new clsFazaKontrate
                    {
                        IdKrijues = idPerdoruesi,
                        IdKontrata = idKontrata,
                        IdNdermarrje = idNdermarrje,
                        IdStatusDok = 1,
                        IdFaza=col.Count+1
                    };
                   
                    newRreshti = e.InsertValues[i].MerrCustomInsertedObject(newRreshti);
                  //  int idFaze=0;
                  //  mesazhi = newRreshti.ruaj(out idFaze);
                 //   if (mesazhi.Status)
                        col.Add(newRreshti);
                }
                for (int i = 0; i < e.DeleteValues.Count; i++)
                {
                    int key = e.DeleteValues[i].MerrKeyValue<int>();
                    if (!clsFazaKontrate.EkzistonFaza(key, idNdermarrje, idKontrata))//nese nuk eshte kontrate e ruajtur ne db
                        col.RemoveAll(x => x.IdFaza == key);
                }
                ////nuk lejohet fshirja e fazave
                ////for (int i = 0; i < e.DeleteValues.Count; i++)
                ////{
                ////    int key = Convert.ToInt32(e.DeleteValues[i].Keys[0]);
                ////    if (AnalizeBuxheti.KaVeprimeRreshti(idUrdherPagesa, key))
                ////    {
                ////        throw new Exception("Me kete rresht ka veprime!");
                ////    }
                ////    mesazhi = clsRreshtaAmbjenti.Fshi(key, idUrdherPagesa);
                ////    if (mesazhi.Status) col.RemoveAll(x => x.IdFaza == key);
                ////}

                mySessionObjects.ruajObjectNeSesion(Session, col, "fazat");
                if (mesazhi.Status)
                {
                  
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ruajtja u krye me sukses!:Green");
                }
                else
                {
                    DbCore.mySessionObjects.ruajMesazhNeSesion(Session, "Ndodhi nje gabim gjate ruajtes se zerave!:Red");
                }
                gvFazat.DataSource = col;
                gvFazat.DataBind();
                e.Handled = true;
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                DbCore.mySessionObjects.ruajMesazhNeSesion(Session,  err.Message + ":Red");
            }
           
        }

        protected void gvFazat_DataBound(object sender, EventArgs e)
        {
            if (gvFazat.Columns["#"] == null)
            {
                //behet nepermjet kodit afishimi i checkboxit qe do perdoret per
                //perzgjidh
                GridViewCommandColumn check = new GridViewCommandColumn("#");
                check.ShowSelectCheckbox = true;
                check.Width = Unit.Percentage(2);
                gvFazat.Settings.ShowFilterRow = false;
                gvFazat.Settings.ShowFilterBar = GridViewStatusBarMode.Hidden;
                gvFazat.Settings.ShowFilterRowMenu = false;
                check.VisibleIndex = 0;
                gvFazat.Columns.Insert(0, check);

                gvFazat.KeyFieldName = "IdFaza";
                gvFazat.SettingsBehavior.AllowSelectByRowClick = true;
                gvFazat.SettingsBehavior.AllowFocusedRow = true;
                
                
            }
        }

        protected void gvFazat_CustomCallback(object sender, DevExpress.Web.ASPxGridViewCustomCallbackEventArgs e)
        {
            if (e.Parameters.Length > 0)
            {
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
              
                  mbushGridenNgaDB(idNdermarrje, idKontrata);
               
            }
        }

        protected void gvFazat_CustomErrorText(object sender, ASPxGridViewCustomErrorTextEventArgs e)
        {

        }

       // protected void gvFazat_RowValidating(object sender, DevExpress.Web.Data.ASPxDataValidationEventArgs e)
       // {
            //int idUrdherPagesa = 0;
            //clsMesazh mesazhi = clsRreshtaAmbjenti.EkzistonKyKod(e.NewValues["Pershkrimi"].ToString(), idUrdherPagesa, idNdermarrje);
            //if (mesazhi.Status)
            //    e.Errors[gvFazat.Columns["Pershkrimi"]] = mesazhi.PershkrimMesazhi;

       // }
    }
}