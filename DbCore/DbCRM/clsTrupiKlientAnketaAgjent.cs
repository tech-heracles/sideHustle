using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbCRM
{
   public class clsTrupiKlientAnketaAgjent
      {
        #region Atribute
        private int idTrupiKlientAnketa;
        private int idKokaKlientAnketa;
        private int idTrupiAnketa;
        private int statusveprimi;
        private string shenimetrupi;
      

        #endregion

        #region Properties
        public int IidTrupiKlientAnketa
        {
            get { return idTrupiKlientAnketa; }
            set { idTrupiKlientAnketa = value; }
        }

        public int IdKokaKlientAnketa
        {
            get { return idKokaKlientAnketa; }
            set { idKokaKlientAnketa = value; }
        }

        public int IdTrupiAnketa
        {
            get { return idTrupiAnketa; }
            set { idTrupiAnketa = value; }
        }

        public int Statusveprimi
        {
            get { return statusveprimi; }
            set { statusveprimi = value; }
        }

        public string Shenimetrupi
        {
            get { return shenimetrupi; }
            set { shenimetrupi = value; }
        }

       

        #endregion

        #region Konstruktoret

        public clsTrupiKlientAnketaAgjent()
        {

        }

        public clsTrupiKlientAnketaAgjent(int idTrupiKlientAnketa, int idKokaKlientAnketa, int idTrupiAnketa, int statusveprimi, string shenimetrupi)
        {
            this.idTrupiKlientAnketa = idTrupiKlientAnketa;
            this.idKokaKlientAnketa = idKokaKlientAnketa;
            this.idTrupiAnketa = idTrupiAnketa;
            this.statusveprimi = statusveprimi;
            this.shenimetrupi = shenimetrupi;

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
            clsMesazh u_ruajt = data.ruajTrupiKlientAnketaAgjent(out id, this.idKokaKlientAnketa, this.idTrupiAnketa, this.statusveprimi, this.shenimetrupi);
            this.idTrupiKlientAnketa = id;
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
            clsMesazh u_modifikua = data.modifikoTrupiKlientAnketeAgjent(this.idTrupiKlientAnketa, this.idKokaKlientAnketa, this.idTrupiAnketa, this.statusveprimi, this.shenimetrupi);
            return u_modifikua;
        }



        public static bool kaShenimeTrupi(int key)
        {
            clsDatabaseCRM dbCRM = new clsDatabaseCRM();
            bool kaShenime=dbCRM.kaShenimeTrupiKlientAnketaAgjent(key);
            dbCRM.Dispose();
            return kaShenime;
        }

        public bool mbushTrupiKlientAnketeAgjent(int idTrupi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool sukses = mbushTrupiKlientAnketaAgjent(data.merrTrupiKlientAnketeAgjentSipasId(idTrupi));
            data.Dispose();
            return sukses;
        }

        public DataRow ktheTrupiKlienteAnketeAgjentDt(int idTrupi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrTrupiKlientAnketeAgjentSipasId(idTrupi);
            data.Dispose();
            return dr;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushTrupiKlientAnketaAgjent(DataRow dbDataRowTrupiKlientAnketaAgjent)
        {
            if (dbDataRowTrupiKlientAnketaAgjent != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupiKlientAnketaAgjent["IDTRUPIKLIENTANKETE"].ToString(), out idTrupiKlientAnketa);
                    int.TryParse(dbDataRowTrupiKlientAnketaAgjent["IDKOKAKLIENTANKETE"].ToString(), out idKokaKlientAnketa);
                    int.TryParse(dbDataRowTrupiKlientAnketaAgjent["IDTRUPIANKETA"].ToString(), out idTrupiAnketa);
                    int.TryParse(dbDataRowTrupiKlientAnketaAgjent["STATUSVEPRIMI"].ToString(), out statusveprimi);
                    shenimetrupi = dbDataRowTrupiKlientAnketaAgjent["SHENIMETRUPI"].ToString();
                       return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te trupit te klient ankete agjent nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion


        
      }
    }

