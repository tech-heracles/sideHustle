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
    ///  (Te dhenat  merren nga tabela : T_SHOPS_HIERARKI_UNIFORM)
    /// </summary>
    public class clsShopsHierarkiUniform : IDataBaseReader
    {
        #region Attribute

        private int idUniform;
        private string pershkrimUniform;
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
        public int IdUniform
        {
            get { return idUniform; }
            set { idUniform = value; }
        }

        public string PershkrimUniform
        {
            get { return pershkrimUniform; }
            set { pershkrimUniform = value; }
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
        public clsShopsHierarkiUniform()
        {
        }

        public clsShopsHierarkiUniform(IDataRecord record)
        {
            Mbush(record);
        }

        /// <summary>
        /// Konstruktor i plote
        /// </summary>
        /// <param name="idRolPerdorues"></param>
        /// <param name="idRoli"></param>
        /// <param name="idPerdorues"></param>
        public clsShopsHierarkiUniform(int idUniform, string pershkrimUniform, int idkrijuesi, int idperdorues, bool aktive)
        {
            this.idUniform = idUniform;
            this.pershkrimUniform = pershkrimUniform;
            this.idKrijuesi = idkrijuesi;
            this.idPerdorues = idperdorues;
            this.aktiv = aktive;

        }
        
        public clsShopsHierarkiUniform(int idUniform)
        {
            if (idUniform == 0)
                return;
            using (clsDatabaseAdmin dbShopsHierarkiUniform = new clsDatabaseAdmin())
            {
                dbShopsHierarkiUniform.merrShopsHierarkiUniform(idUniform, this);
            }
        }

        public clsShopsHierarkiUniform(string pershkrimUniform)
        {
            if (pershkrimUniform == "")
                return;
            using (clsDatabaseAdmin dbShopsHierarkiUniform = new clsDatabaseAdmin())
            {
                dbShopsHierarkiUniform.merrShopsHierarkiUniformSipasPershkrimi(pershkrimUniform, this);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public void Mbush(IDataRecord dbDataRowShopsHierarkiUniform)
        {

            int.TryParse(dbDataRowShopsHierarkiUniform["IDUNIFORM"]?.ToString(), out idUniform);
            pershkrimUniform = dbDataRowShopsHierarkiUniform["PERSHKRIMUNIFORM"]?.ToString();
            DateTime.TryParse(dbDataRowShopsHierarkiUniform["DTKRIJIMI"]?.ToString(), out dtKrijimi);
            DateTime.TryParse(dbDataRowShopsHierarkiUniform["DTMODIFIKIMI"]?.ToString(), out dtModifikimi);
            int.TryParse(dbDataRowShopsHierarkiUniform["IDPERDORUES"]?.ToString(), out idPerdorues);
            int.TryParse(dbDataRowShopsHierarkiUniform["IDKRIJUESI"]?.ToString(), out idKrijuesi);
            bool.TryParse(dbDataRowShopsHierarkiUniform["AKTIV"]?.ToString(), out aktiv);

        }
        
        #endregion

        #region Metoda Publike

        public clsMesazh modifikoShopsHierarkiUniform()
        {
            using (var data = new clsDatabaseAdmin())
            return data.modifikoShopsHierarkiUniform(this.idUniform, this.pershkrimUniform, this.idPerdorues, this.aktiv);
        }

        public static bool ekzistonUniformMeKetePershkrim(string pershkrimUniform)
        {
            clsDatabaseAdmin dbShopsHierarkiUniform = new clsDatabaseAdmin();
            bool ekziston = dbShopsHierarkiUniform.ekzistonUniformMeKetePershkrim(pershkrimUniform);
            dbShopsHierarkiUniform.Dispose();
            return ekziston;
        }

        public clsMesazh ruajShopsHierarkiUniform()
        {
           using (var data = new clsDatabaseAdmin())
           return data.ruajShopsHierarkiUniform(pershkrimUniform, idKrijuesi, aktiv);
            
        }

        #endregion
    }
}
