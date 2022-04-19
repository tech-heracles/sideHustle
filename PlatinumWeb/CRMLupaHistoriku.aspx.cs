using DbCore.DbShare;
using System;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class CRMLupaHistoriku : MyPageBase
    {
        string guidString;

        protected void Page_Load(object sender, EventArgs e)
        {
            int id = 0;
            int llojDok = -1;
            if (Request.QueryString["id"] != null && Request.QueryString["id"] != "" && Request.QueryString["llojDok"] != null && Request.QueryString["llojDok"] != "")
            {
                id = int.Parse(Request.QueryString["id"]);
                llojDok = int.Parse(Request.QueryString["llojDok"]);
            }
            percaktoEmratEFushave();

            if (!IsPostBack)
            {
                guidString = Guid.NewGuid().ToString();
                hfState.Set("guidString", guidString);
                MbushNgaGalerinNgaDB(id, llojDok);
            }
            else
            {
                guidString = hfState["guidString"].ToString();
                MbushNgaSession(id, llojDok);
            }
        }

        private void percaktoEmratEFushave()
        {
            galeria.ImageUrlField = "Path";
            galeria.FullscreenViewerThumbnailUrlField = "Path";
            galeria.ThumbnailUrlField = "Path";
            galeria.TextField = "Shenime";

        }

        private void MbushNgaGalerinNgaDB(int idDok, int llojDok)
        {
            var oArkiva = new colArkiva(idDok, llojDok);

            galeria.DataSource = oArkiva;
            galeria.DataBind();
            DbCore.mySessionObjects.RuajNeSession(Session, oArkiva, guidString);
        }

        private void MbushNgaSession(int idDok, int llojDok)
        {
            colArkiva tmpObject = DbCore.mySessionObjects.MerrNgaSession<colArkiva>(Session, guidString);
            if (tmpObject == null)
                MbushNgaGalerinNgaDB(idDok, llojDok);
            else
            {
                galeria.DataSource = tmpObject;
                galeria.DataBind();
            }
        }
    }
}