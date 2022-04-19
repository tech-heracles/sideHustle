using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe bejne lidhjen e statusesve me perdoruesit
    ///  (Te dhenat  merren nga tabela : T_SHOPS_HIERARKI_STATUS)
    /// </summary>
    public class clsShopsHierarkiStatus : IDataBaseReader

    {
        #region Attribute

        private int idStatus;
        private string pershkrimStatusi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idPerdorues;
        private int idKrijuesi;
        private bool aktiv;

        #endregion

        #region Properties

        /// <summary>
        /// Get Set: IDStatus
        /// </summary>
        public int IdStatus
        {
            get { return idStatus; }
            set { idStatus = value; }
        }

        public string PershkrimStatusi
        {
            get { return pershkrimStatusi; }
            set { pershkrimStatusi = value; }
        }


        ///<summary>
        ///Kthen/Vendos Daten e krijimit qe gjenerohet automatikisht
        ///</summary>
        public DateTime DtKrijimi
        {
            get
            { return dtKrijimi; }
            set
            { dtKrijimi = value; }
        }

        ///<summary>
        ///Kthen/Vendos daten e modifikimit te statusit te punonjesit
        ///</summary>
        public DateTime DtModifikimi
        {
            get
            { return dtModifikimi; }
            set
            { dtModifikimi = value; }
        }


        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }


        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }
        public bool Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsShopsHierarkiStatus()
        {
        }

        public clsShopsHierarkiStatus(IDataRecord record)
        {
            Mbush(record);
        }

        /// <summary>
        /// Konstruktor i plote
        /// </summary>
        /// <param name="idRolPerdorues"></param>
        /// <param name="idRoli"></param>
        /// <param name="idPerdorues"></param>
        public clsShopsHierarkiStatus(int idstatus, string pershkrimstatusi, int idkrijuesi, int idperdorues, bool aktive)
        {
            this.idStatus = idstatus;
            this.pershkrimStatusi = pershkrimstatusi;
            this.idKrijuesi = idkrijuesi;
            this.idPerdorues = idperdorues;
            this.aktiv = aktive;

        }

        public clsShopsHierarkiStatus(string pershkrimStatusi)
        {
            if (pershkrimStatusi == "")
                return;
            using (clsDatabaseAdmin dbShopsHierarkiStatus = new clsDatabaseAdmin())
            {
                dbShopsHierarkiStatus.merrShopsHierarkiStatusiSipasPershkrimi(pershkrimStatusi, this);
            }
        }
        public static int merrIdStatusPerdoruesiSipasPershkrimi(string pershkrimStatusi)
        {
            clsDatabaseAdmin dbShopsHierarkiStatus = new clsDatabaseAdmin();
            {
                return dbShopsHierarkiStatus.ktheIdStatusPerdoruesiSipasPershkrimit(pershkrimStatusi);
            }
        }

        public clsShopsHierarkiStatus(int idstatus)
        {
            if (idstatus == 0)
                return;
            using (clsDatabaseAdmin dbShopsHierarkiStatus = new clsDatabaseAdmin())
            {
                dbShopsHierarkiStatus.merrShopsHierarkiStatusi(idstatus, this);
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         public void Mbush(IDataRecord dbDataRowShopsHierarkiStatus)
        {

            int.TryParse(dbDataRowShopsHierarkiStatus["IDSTATUS"]?.ToString(), out idStatus);
            pershkrimStatusi = dbDataRowShopsHierarkiStatus["PERSHKRIMSTATUSI"]?.ToString();
            DateTime.TryParse(dbDataRowShopsHierarkiStatus["DTKRIJIMI"]?.ToString(), out dtKrijimi);
            DateTime.TryParse(dbDataRowShopsHierarkiStatus["DTMODIFIKIMI"]?.ToString(), out dtModifikimi);
            int.TryParse(dbDataRowShopsHierarkiStatus["IDPERDORUES"]?.ToString(), out idPerdorues);
            int.TryParse(dbDataRowShopsHierarkiStatus["IDKRIJUESI"]?.ToString(), out idKrijuesi);
            bool.TryParse(dbDataRowShopsHierarkiStatus["AKTIV"]?.ToString(), out aktiv);

        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// shkruan te drejten ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh modifikoShopsHierarkiStatus()
        {
            using (var data = new clsDatabaseAdmin())
                return data.modifikoShopsHierarkiStatus(this.idStatus, this.pershkrimStatusi, this.idPerdorues, this.aktiv);
        }

        public static bool ekzistonStatusMeKetePershkrim(string pershkrimStatusi)
        {
            clsDatabaseAdmin dbShopsHierarkiStatus = new clsDatabaseAdmin();
            bool sukses = dbShopsHierarkiStatus.ekzistonStatusMeKetePershkrim(pershkrimStatusi);
            dbShopsHierarkiStatus.Dispose();
            return sukses;
        }

        public clsMesazh ruajShopsHierarkiStatus()
        {
            using (var data = new clsDatabaseAdmin())
                return data.ruajShopsHierarkiStatus(pershkrimStatusi, aktiv, idKrijuesi);
        }
        
        #endregion
    }
}
