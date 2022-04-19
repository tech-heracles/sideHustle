using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsKonfigurimeGIS
    {
        #region Atribute
        
        private string varFusha;
        private string varPershkrimi;

        #endregion

        #region Konstruktoret

        public clsKonfigurimeGIS()
        {
        }

        public clsKonfigurimeGIS(DataRow db)
        {
            mbushKonfigurime(db);
        }

        public clsKonfigurimeGIS(string fusha, string pershkrimi)
        {
            varFusha = fusha;
            varPershkrimi = pershkrimi;
        }

        #endregion

        #region Properties

        public string Fusha
        {
            get { return varFusha; }
            set { varFusha = value; }
        }
        public string Pershkrimi
        {
            get { return varPershkrimi; }
            set { varPershkrimi = value; }
        }
        #endregion

        #region Metoda Publike

        public clsKonfigurimeGIS findPershkrimByFusha(colKonfigurimeGIS konfig, string fusha)
        {
            if (konfig.Exists(x => (x.Fusha == fusha)))
            {
                return konfig.Find(x => (x.Fusha == fusha));
            }
            else
            {
                clsKonfigurimeGIS tempKonfig = new clsKonfigurimeGIS();
                tempKonfig.Fusha = fusha;
                tempKonfig.Pershkrimi = fusha;
                return tempKonfig;
            }

        }

        #endregion

        #region Metoda Internal
        internal bool mbushKonfigurime(DataRow db)
        {
            if (db != null)
            {
                try
                {
                    Fusha = db["FUSHA"].ToString();
                    Pershkrimi = db["PERSHKRIMI"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se konfigurimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}