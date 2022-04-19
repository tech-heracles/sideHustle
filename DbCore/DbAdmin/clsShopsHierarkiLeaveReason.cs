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
    ///  (Te dhenat  merren nga tabela : T_SHOPS_HIERARKI_LEAVEREASON)
    /// </summary>
    public class clsShopsHierarkiLeaveReason : IDataBaseReader
    {
        #region Attribute

        private int idLeaveReason;
        private string pershkrimLeaveReason;
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
        public int IdLeaveReason
        {
            get { return idLeaveReason; }
            set { idLeaveReason = value; }
        }

        public string PershkrimLeaveReason
        {
            get { return pershkrimLeaveReason; }
            set { pershkrimLeaveReason = value; }
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
        /// Konstruktor i plote

        public clsShopsHierarkiLeaveReason(int idLeaveReason, string pershkrimLeaveReason, int idkrijuesi, int idperdorues, bool aktive)
        {
            this.idLeaveReason = idLeaveReason;
            this.pershkrimLeaveReason = pershkrimLeaveReason;
            this.idKrijuesi = idkrijuesi;
            this.idPerdorues = idperdorues;
            this.aktiv = aktive;

        }
        public clsShopsHierarkiLeaveReason() {
        }
        public clsShopsHierarkiLeaveReason(IDataRecord record)
        {
            Mbush(record);
        }

        public clsShopsHierarkiLeaveReason(string pershkrimLeaveReason)
        {
            if (pershkrimLeaveReason == "")
                return;
            using (clsDatabaseAdmin dbShopsHierarkiLeaveReason = new clsDatabaseAdmin())
            {
                dbShopsHierarkiLeaveReason.merrShopsHierarkiLeaveReasonSipasPershkrimi(pershkrimLeaveReason, this);
            }
        }

        public clsShopsHierarkiLeaveReason(int idLeaveReason)
        {
            if (idLeaveReason == 0)
                return;
            using (clsDatabaseAdmin dbShopsHierarkiLeaveReason = new clsDatabaseAdmin())
            {
                dbShopsHierarkiLeaveReason.merrShopsHierarkiLeaveReason(idLeaveReason, this);
            }
        }

        #endregion

        #region Metoda Internal
        public void Mbush(IDataRecord dbDataRowShopsHierarkiLeaveReason)
        {
           
            int.TryParse(dbDataRowShopsHierarkiLeaveReason["IDLEAVEREASON"]?.ToString(), out idLeaveReason);
            pershkrimLeaveReason = dbDataRowShopsHierarkiLeaveReason["PERSHKRIMLEAVEREASON"]?.ToString();
            DateTime.TryParse(dbDataRowShopsHierarkiLeaveReason["DTKRIJIMI"]?.ToString(), out dtKrijimi);
            DateTime.TryParse(dbDataRowShopsHierarkiLeaveReason["DTMODIFIKIMI"]?.ToString(), out dtModifikimi);
            int.TryParse(dbDataRowShopsHierarkiLeaveReason["IDPERDORUES"]?.ToString(), out idPerdorues);
            int.TryParse(dbDataRowShopsHierarkiLeaveReason["IDKRIJUESI"]?.ToString(), out idKrijuesi);
            bool.TryParse(dbDataRowShopsHierarkiLeaveReason["AKTIV"]?.ToString(), out aktiv);
           
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// shkruan te drejten ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh modifikoShopsHierarkiLeaveReason()
        {
            using (var data = new clsDatabaseAdmin())
            return data.modifikoShopsHierarkiLeaveReason(this.idLeaveReason, this.pershkrimLeaveReason, this.idPerdorues, this.aktiv);
        }
        
        public static bool ekzistonLeaveReasonMeKetePershkrim(string pershkrimLeaveReason)
        {
            clsDatabaseAdmin dbShopsHierarkiLeaveReason = new clsDatabaseAdmin();
            bool ekziston = dbShopsHierarkiLeaveReason.ekzistonLeaveReasonMeKetePershkrim(pershkrimLeaveReason);
            dbShopsHierarkiLeaveReason.Dispose();
            return ekziston;
        }

        public clsMesazh ruajShopsHierarkiLeaveReason()
        {
            using (var data = new clsDatabaseAdmin())
            return data.ruajShopsHierarkiLeaveReason(pershkrimLeaveReason, idKrijuesi, aktiv);
        }
    }
}

#endregion