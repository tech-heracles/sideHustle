using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using AlphaWeb.Core.Interfaces.Data;

namespace DbCore.DbAdmin
{
    public class clsKonfigurimEmailKI : IDataBaseReader
    {
        #region Atributet

        private int id;
        private int idTemplateImporti;
        private int statusi;
        private int lloji;
        private int idDestinacion;
        private string destinacion;
        private string email;
        private DateTime dtKrijimi;
        private int idKrijuesi;
        private int idStatusDok;

        #endregion

        #region Properties
        
        public int Id { get { return id; } set { id = value; } }
        public int IdTemplateImporti { get { return idTemplateImporti; } set { idTemplateImporti = value; } }
        public int Statusi { get { return statusi; } set { statusi = value; } }
        public int Lloji { get { return lloji; } set { lloji = value; } }
        public int IdDestinacion { get { return idDestinacion; } set { idDestinacion = value; } } // mund te jete ref e clsPerdorues ose clsRoli
        public string Destinacion { get { return destinacion; } set { destinacion = value; } }
        public string Email { get { return email; } set { email = value; } }
        public DateTime DtKrijimi { get { return dtKrijimi; } set { dtKrijimi = value; } }
        public int IdKrijuesi { get { return idKrijuesi; } set { idKrijuesi = value; } }
        public int IdStatusDok { get { return idStatusDok; } set { idStatusDok = value; } }

        #endregion

        #region Konstruktoret
        public clsKonfigurimEmailKI() { }

        public clsKonfigurimEmailKI(IDataRecord record) {
            Mbush(record);
        }

        public clsKonfigurimEmailKI( int id, int idTemplateImporti, int statusi, int lloji, int idDestinacion, string email, DateTime dtKrijimi, int idKrijuesi, int idStatusDok)
        {
            this.id = id;
            this.idTemplateImporti = idTemplateImporti;
            this.statusi = statusi;
            this.lloji = lloji;
            this.idDestinacion = idDestinacion;
            this.email = email;
            this.dtKrijimi = dtKrijimi;
            this.idKrijuesi = idKrijuesi;
            this.idStatusDok = idStatusDok;
        }

        #endregion

        #region Metoda Publike
        
        public virtual void Mbush(IDataRecord record)
        {
            int.TryParse(record["ID"].ToString(), out id);
            int.TryParse(record["IDTEMPLATEIMPORTI"].ToString(), out idTemplateImporti);
            int.TryParse(record["STATUSI"].ToString(), out statusi);
            int.TryParse(record["LLOJI"].ToString(), out lloji);
            int.TryParse(record["IDDESTINACION"].ToString(), out idDestinacion);
            destinacion = record["DESTINACION"].ToString();
            email = record["EMAIL"].ToString();
            DateTime.TryParse(record["DTKRIJIMI"].ToString(), out dtKrijimi);
            int.TryParse(record["IDKRIJUESI"].ToString(), out idKrijuesi);
            int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
        }

        #endregion
        
    }
}
