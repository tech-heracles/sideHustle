using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class clsOpsioneAnkete
    {
        #region Atribute

        private int idOpsioni;
        private string emertimi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKrijuesi;
        private int idModifikuesi;
        private int idNdermarrje;

        #endregion

        #region Properties

        public int IdOpsioni
        {
            get { return idOpsioni; }
            set { idOpsioni = value; }
        }

        public string Emertimi
        {
            get { return emertimi; }
            set { emertimi = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        public int IdModifikuesi
        {
            get { return idModifikuesi; }
            set { idModifikuesi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        #endregion

        #region Konstruktore

        public clsOpsioneAnkete()
        {

        }
        public clsOpsioneAnkete(int idOpsioni)
        {

        }
        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_ruajt = ruaj(data);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseCRM data)
        {
            int id;
            clsMesazh u_ruajt = data.ruajOpsionAnkete(out id, this.emertimi, this.idStatusDok, this.idKrijuesi, this.idNdermarrje);
            this.idOpsioni = id;
            return u_ruajt;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_modifikua = modifiko(data);
            data.Dispose();
            return u_modifikua;
        }

        public clsMesazh modifiko(clsDatabaseCRM data)
        {
            clsMesazh u_modifikua = data.modifikoOpsionAnkete(this.idOpsioni, this.emertimi, this.idStatusDok, this.idModifikuesi, this.idNdermarrje);
            return u_modifikua;
        }

        public static clsMesazh fshi(int idOpsion, int idPerdorues)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiOpsionAnkete(idOpsion, idPerdorues);
            data.Dispose();           
            return u_fshi;
        }

        public bool mbushOpsionAnkete(int idOpsion)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool sukses = mbushOpsioneAnkete(data.merrOpsionAnketeSipasId(idOpsion));
            data.Dispose();
            return sukses;
        }

        public DataRow ktheOpsionAnketeDt(int idOpsion)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrOpsionAnketeSipasId(idOpsion);
            data.Dispose();
            return dr;
        }
        public static bool ekzistonFushaAnketeMeEmertim(int idopsioni,string emertimi, int idnderm) 
        {
            clsDatabaseCRM dbOpsione = new clsDatabaseCRM();
            bool sukses = dbOpsione.ekzistonOpsionAnketa(idopsioni,emertimi, idnderm);
            dbOpsione.Dispose();
            return sukses;
        }
        

        #endregion

        #region Metoda Internal

        internal bool mbushOpsioneAnkete(DataRow dbDataRowOpsionAnkete)
        {
            if (dbDataRowOpsionAnkete != null)
            {
                try
                {
                    int.TryParse(dbDataRowOpsionAnkete["IDOPSIONI"].ToString(), out idOpsioni);
                    emertimi = dbDataRowOpsionAnkete["EMERTIMI"].ToString();
                    int.TryParse(dbDataRowOpsionAnkete["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowOpsionAnkete["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowOpsionAnkete["IDMODIFIKUESI"].ToString(), out idModifikuesi);
                    DateTime.TryParse(dbDataRowOpsionAnkete["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowOpsionAnkete["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowOpsionAnkete["IDNDERMARJE"].ToString(), out idNdermarrje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te anketes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}