using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class clsTrupiAnketa
    {
        #region Atribute

        private int idTrupiAnketa;
        private int idKokaAnketa;
        private int idOpsionAnketa;
        private bool detyrueshme;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifkimi;
        private string opsioni;

        #endregion

        #region Properties
        public int IdTrupiAnketa
        {
            get { return idTrupiAnketa; }
            set { idTrupiAnketa = value; }
        }

        public int IdKokaAnketa
        {
            get { return idKokaAnketa; }
            set { idKokaAnketa = value; }
        }

        public int IdOpsionAnketa
        {
            get { return idOpsionAnketa; }
            set { idOpsionAnketa = value; }
        }

        public bool Detyrueshme
        {
            get { return detyrueshme; }
            set { detyrueshme = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifkimi
        {
            get { return dtModifkimi; }
            set { dtModifkimi = value; }
        }

        public string Opsioni
        {
            get
            {
                return opsioni;
            }
        }
        #endregion

        #region Konstruktoret

        public clsTrupiAnketa()
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
            clsMesazh u_ruajt = data.ruajTrupiAnketa(out id, this.idKokaAnketa, this.idOpsionAnketa, this.detyrueshme, this.idStatusDok);
            this.idKokaAnketa = id;
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
            clsMesazh u_modifikua = data.modifikoTrupiAnkete(this.idTrupiAnketa, this.idKokaAnketa, this.idOpsionAnketa, this.detyrueshme, this.idStatusDok);
            return u_modifikua;
        }

        public static clsMesazh fshi(int idTrupi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiTrupAnkete(idTrupi);
            data.Dispose();
            return u_fshi;
        }

        public bool mbushKokeAnkete(int idTrupi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool sukses = mbushTrupiAnketa(data.merrTrupAnketeSipasId(idTrupi));
            data.Dispose();
            return sukses;
        }

        public DataRow ktheKokeAnketeDt(int idTrupi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrTrupAnketeSipasId(idTrupi);
            data.Dispose();
            return dr;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTrupiAnketa(DataRow dbDataRowTrupiAnketa)
        {
            if (dbDataRowTrupiAnketa != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupiAnketa["IDTRUPIANKETA"].ToString(), out idTrupiAnketa);
                    int.TryParse(dbDataRowTrupiAnketa["IDKOKAANKETA"].ToString(), out idKokaAnketa);
                    int.TryParse(dbDataRowTrupiAnketa["IDOPSIONIANKETA"].ToString(), out idOpsionAnketa);
                    bool.TryParse(dbDataRowTrupiAnketa["DETYRUESHME"].ToString(), out detyrueshme);
                    int.TryParse(dbDataRowTrupiAnketa["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowTrupiAnketa["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowTrupiAnketa["DTMODIFIKIMI"].ToString(), out dtModifkimi);
                    opsioni = dbDataRowTrupiAnketa["Opsioni"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te trupit te anketes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}