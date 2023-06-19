using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DbCore;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Security;

namespace PlatinumWeb.WebServiceLicencat
{
    /// <summary>
    /// Summary description for shto_ndermarrje
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class shto_ndermarrje : System.Web.Services.WebService
    {
        public clsMesazh mesazh;

        [WebMethod(EnableSession = true)]
        public clsMesazh shtoNdermarrje(int idLicenca, string kodiNdermarrje, string kodiNdermDefault, string pershrkimiNder, int monedha, string viti, int llojLicence, string konfigurimiDefault)
        {
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            // string decryptedtranID = DbCore.RijndaelSimple.DecryptImbString(tranid);
            dbAdmin.beginTransaksion();
            try
            {
                int idPerdoruesAdminLicenca = dbAdmin.merrPerdoruesAdminlicence();
                DbCore.DbAdmin.clsNdermarrje ndermarrje = celNdermarrje(idLicenca, idPerdoruesAdminLicenca, kodiNdermarrje, kodiNdermDefault, pershrkimiNder, monedha, viti, llojLicence, konfigurimiDefault);
                mesazh = shtoRoleDefault(ndermarrje, llojLicence, idPerdoruesAdminLicenca, idLicenca);
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ex.Message);
            }
        }
        [WebMethod(EnableSession = true)]
        private DbCore.DbAdmin.clsNdermarrje celNdermarrje(int idLicenca, int idPerdoruesAdminLicenca, string kodiNdermarrje, string kodiNdermDefault, string pershrkimiNder, int monedha, string viti, int llojLicence, string konfigurimiDefault)
        {
            Shto_Ndermarrje nder = new Shto_Ndermarrje();
             DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
            vit.mbushVitetMet(viti, -1);
            DbCore.DbAdmin.clsNdermarrje ndermarrjaERe = new DbCore.DbAdmin.clsNdermarrje();
            ndermarrjaERe = nder.krijoNdermarrje(idLicenca, idPerdoruesAdminLicenca, "", "", kodiNdermarrje, "", "", monedha, "", 20000.00, pershrkimiNder, 1, "", "", "", "", 0, -1, "", false, false, false, "", "", "", vit.IdViti, null, 1, 1,false,"","",false);
            DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(kodiNdermDefault);
            int idnderm = ndermarrja.IdNdermarrje;
            ndermarrjaERe.Lloji = ndermarrja.Lloji;
            ndermarrja.Dispose();
            mesazh = ndermarrjaERe.ruaj(konfigurimiDefault, idnderm,false);
            return ndermarrjaERe;
        }

        [WebMethod]
        private clsMesazh shtoRoleDefault(DbCore.DbAdmin.clsNdermarrje ndermarrje, int llojLicence, int idPerdoruesAdminLicenca, int idlicenca)
        {
            DbCore.DbAdmin.clsRoli roli = new DbCore.DbAdmin.clsRoli();
            return roli.ruajRoleDefaultMeTeDrejtaRaportesh(ndermarrje.IdNdermarrje, ndermarrje.IdViti, idPerdoruesAdminLicenca, llojLicence, idlicenca);
        }


        /// <summary>
        /// krijon perdoruesin qe do te ruhet
        /// </summary>
        /// <returns>kthen clsPerdorues me perdoruesin qe do te ruhet</returns>
        /// <param name="idPerdoruesi"></param>
        [WebMethod]
        private DbCore.DbAdmin.clsPerdorues shtoPerdorues(int idPerdoruesi, String emri, String username, int idRoli, string email)
        {
            Shto_Perdorues shto_perdorues = new Shto_Perdorues();
            return shto_perdorues.krijoPerdorues(emri, "", username, "", "", email, "", -1, true, true, PasswordHelper.GjeneroPassword(10), idPerdoruesi, 1, 2, false, false, 0, 0, true, false, true, false,false, DbCore.DbAdmin.clsPerdorues.ruajRole(idRoli), 0, "", "", "", "", "", "", "", "", "", 0, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, "", "", "", 0, "", "", "", "", DateTime.Now, "", "", "", "", "", 0, 0, 0, "", 0, false,0);
        }
    }
}
