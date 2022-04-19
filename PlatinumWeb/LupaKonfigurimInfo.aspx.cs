using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DbCore.DbAdmin;
using DevExpress.Web;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class LupaKonfigurimInfo : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AspxWebControlUtils.konfiguroMenuRuajPerLupaPerInfo(ASPxMenu1);
                int id = int.Parse(Request.QueryString["id"]);
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                mbushPopUpListeNgaDB(id, true, idNdermarrje);
            }
        }

        private void mbushPopUpListeNgaDB(int id, bool def, int idNdermarrje)
        {//mbush griden e popupit me te dhena     
            colInfoTrupi trupatvis, trupatinvis;
            if (def)
            {
                //trupatvis = colInfoTrupi.merrInfoSipasIdKokaDheVisible(id, true, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                //trupatinvis = colInfoTrupi.merrInfoSipasIdKokaDheVisible(id, false, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
                trupatvis = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(id, true, idNdermarrje);
                trupatinvis = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(id, false, idNdermarrje);
            }
            else
            {
                trupatvis = DbCore.mySessionObjects.merrInfoVisibleNgaSesioni(Session);
                trupatinvis = DbCore.mySessionObjects.merrInfoInVisibleNgaSesioni(Session);
            }
            lbxZgjedhur.DataSource = trupatvis;
            lbxZgjedhur.ValueField = "EmerKolone";
            lbxZgjedhur.TextField = "PershkrimKolone";
            lbxFushat.ValueField = "EmerKolone";
            lbxFushat.TextField = "PershkrimKolone";
            lbxFushat.DataSource = trupatinvis;
            lbxZgjedhur.DataBind();
            lbxFushat.DataBind();
        }

        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            if (e.Item.Name == "OK")
            {
                int id = int.Parse(Request.QueryString["id"]);
                ruajNeSesion(id);
                if (Request.QueryString["ruaj"] != null && Request.QueryString["ruaj"] == "po" && id != -1 && id != -2&& id != -3)
                    ruajnedb(id);
            }
            if (e.Item.Name == "Default")
            {
                clsInfoKoka inf = new clsInfoKoka(int.Parse(Request.QueryString["id"]));
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (inf.Lloji == 1)
                    mbushPopUpListeNgaDB(-1, true, idNdermarrje);
                else if (inf.Lloji == 2)
                    mbushPopUpListeNgaDB(-2, true, idNdermarrje);
                else mbushPopUpListeNgaDB(-3, true, idNdermarrje);
            }
        }

        private void ruajnedb(int id)
        {
            clsInfoKoka koka = new clsInfoKoka(id);
            colInfoTrupi trupat = new colInfoTrupi();
            trupat.AddRange(DbCore.mySessionObjects.merrInfoVisibleNgaSesioni(Session));
            trupat.AddRange(DbCore.mySessionObjects.merrInfoInVisibleNgaSesioni(Session));
            koka.InfoTrupi = trupat;

            DbCore.clsMesazh mesazh = koka.modifikoInfo();
        }

        private void ruajNeSesion(int id)
        {
            colInfoTrupi colvis = new colInfoTrupi();
            colInfoTrupi colinvis = new colInfoTrupi();
            int i = 0;
            foreach (ListEditItem a in lbxZgjedhur.Items)
            {
                clsInfoTrupi inf = new clsInfoTrupi(0, id, a.Value.ToString(), a.Text, true, i);
                i++;
                colvis.Add(inf);
            }
            foreach (ListEditItem a in lbxFushat.Items)
            {
                clsInfoTrupi inf = new clsInfoTrupi(0, id, a.Value.ToString(), a.Text, false, i);
                i++;
                colinvis.Add(inf);
            }
            DbCore.mySessionObjects.ruajInfoVisibleNeSession(Session, colvis);
            DbCore.mySessionObjects.ruajInfoInVisibleNeSession(Session, colinvis);
        }
    }
}