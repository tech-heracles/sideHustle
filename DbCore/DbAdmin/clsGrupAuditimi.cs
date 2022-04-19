using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne grupet e auditimit. Grupet e auditimit sherbejne per te krijuar
    ///  grupime te ndryshme te fushave qe do auditohen. Qellimi i tyre eshte te perdoren si filtra neper raportet
    ///  e auditimit(Te dhenat  merren nga tabela : T_GRUPAUDITIMI)
    /// </summary>
    public class clsGrupAuditimi
    {
        #region Atribute

        private int idGrupi;
        private int nrGrupi;
        private String pershkrimiGrupi;
        private int idPerdoruesi;

        private colGrupAuditimiTrupi oColTrupi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupAuditimi(int idgrupi, int nrgrupi, String pershkrimigrupi, int idperdoruesi)
        {
            idGrupi = idgrupi;
            nrGrupi = nrgrupi;
            pershkrimiGrupi = pershkrimigrupi;
            idPerdoruesi=idperdoruesi;
            oColTrupi = new colGrupAuditimiTrupi();

        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupAuditimi(int nrgrupi, String pershkrimigrupi, int idperdoruesi)
        {
            nrGrupi = nrgrupi;
            pershkrimiGrupi = pershkrimigrupi;
            idPerdoruesi=idperdoruesi;
            oColTrupi = new colGrupAuditimiTrupi();

        }
        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGrupAuditimi()
        {
        }

        public clsGrupAuditimi(DataRow rreshti)
        {
            
            mbushGrupAuditim(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupi
        {
            get { return idGrupi; }
            set { idGrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e grupit. Fushe qe sherben si identifikues llogjik.
        /// </summary>
        public int NrGrupi
        {
            get { return nrGrupi; }
            set { nrGrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e grupit te auditimit.
        /// </summary>
        public String PershkrimiGrupi
        {
            get { return pershkrimiGrupi; }
            set { pershkrimiGrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <c>clsGrupAuditimiTrupi</c>. Cdo objekt i tipit
        /// <c>clsGrupAuditimi</c> permban nje collection te tille.
        /// </summary>
        public colGrupAuditimiTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e eprdoruesit qe e ka krijuar kete grup auditimi.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e objektin <c>clsGrupAuditimi</c> ne tabelen perkatese ne databaze
        /// sebashku edhe me collectionin me objekte te tipit  <c>clsGrupAuditimiTrupi</c>
        /// </summary>
        public clsMesazh ruaj()
        {
            clsMesazh u_ruajt = ruajGrupAuditimiDheTrupin(this);
            return u_ruajt;
        }

        //public bool modifiko()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    bool u_modifikua = data.modifikoGrupAuditimi(this);
        //    return u_modifikua;
        //}

        //public bool fshi()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    bool u_fshi = data.fshiGrupAuditimi(this);
        //    return u_fshi;
        //}

        public clsMesazh ruajGrupAuditimiDheTrupin(clsGrupAuditimi gr)
        {
            clsMesazh mesazh;
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            
            dbAdmin.beginTransaksion();
            try
            {
                int idGA;
                mesazh = dbAdmin.ruajGrupAuditimi(out idGA, gr.NrGrupi, gr.PershkrimiGrupi, gr.IdPerdoruesi);
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                gr.IdGrupi = idGA;
                foreach (clsGrupAuditimiTrupi o in gr.OColTrupi)
                {
                    o.IdGrupi = gr.IdGrupi;
                    mesazh = dbAdmin.ruajTrupin(o.IdGrupi, o.IdTabele, o.IdKolone);
                    if (!mesazh.Status)
                    {
                        dbAdmin.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbAdmin.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushGrupAuditim(DataRow dbDataRowGrupAuditim)
        {
            if (dbDataRowGrupAuditim != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupAuditim["IDGRUPAUDIT"].ToString(), out idGrupi);
                    int.TryParse(dbDataRowGrupAuditim["NRGRUPAUDIT"].ToString(), out nrGrupi);
                    pershkrimiGrupi = dbDataRowGrupAuditim["PERSHKGRUPAUDIT"].ToString();
                    int.TryParse(dbDataRowGrupAuditim["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    colGrupeAuditimi col = new colGrupeAuditimi();
                    oColTrupi = col.merrTrupinGrupitAuditimit(this);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grup auditimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}