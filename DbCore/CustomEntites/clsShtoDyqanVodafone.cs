using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore
{
    public class clsShtoDyqanVodafone
    {
        #region Atribute

        private String kodi;
        private String pershkrimi;
        private String adresa;
        private DateTime dataAktivizimit;
        private String ndermarrja;
        private int status;
        private String[] administrator;

        #endregion

        #region Properties

        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        public String Adresa
        {
            get { return adresa; }
            set { adresa = value; }
        }

        public DateTime DataAktivizimit
        {
            get { return dataAktivizimit; }
            set { dataAktivizimit = value; }
        }

        public String Ndermarrja
        {
            get { return ndermarrja; }
            set { ndermarrja = value; }
        }

        public int Status
        {
            get { return status; }
            set { status = value; }
        }

        public String[] Administrator
        {
            get { return administrator; }
            set { administrator = value; }
        }

        #endregion

        #region Konstruktori

        public clsShtoDyqanVodafone()
        {

        }

        #endregion
    }
}
