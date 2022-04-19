using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class AktivizoAlphaWeb : MyPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
           // lblIdGjeneruar.Text = new Kycim.ReadKF(Kycim.Produkti.AlphaWeb).GetNumrinEForte().ToString();
        }

        protected void btnInstalo_Click(object sender, EventArgs e)
        {
            string strKey = txtKey1.Text.Trim() + txtKey2.Text.Trim() + txtKey3.Text.Trim() + txtKey4.Text.Trim() + txtKey5.Text.Trim();
            //if (new Kycim.ReadKF(Kycim.Produkti.AlphaWeb).instalo(strKey))
            //{
            //    Application["validInstall"] = true;
            //    lblInfoInstalimi.Text = "Instalimi i kopjes se miratuar u krye me sukses!";
            //    Response.Redirect("Login_Ndermarrje.aspx", false);
            //}
            //else
            //    lblInfoInstalimi.Text = "Instalimi i kopjes se miratuar nuk u krye me sukses! Kontaktoni administratorin ose suportin IMB!";

            clsMesazh mesazhi = new clsMesazh();// new Kycim.ReadKF(Kycim.Produkti.AlphaWeb).instaloMeMesazh(strKey, Server.MapPath("~/dlls/icon.dat")); //-komento per momentin
            if (mesazhi.Status)
            {
                Application["validInstall"] = true;
                lblInfoInstalimi.Text = "Instalimi i kopjes se miratuar u krye me sukses!";
                Response.Redirect("Login_Ndermarrje.aspx", false);
            }
            else
                lblInfoInstalimi.Text = mesazhi.PershkrimMesazhi;
        }

        

     

    }
}