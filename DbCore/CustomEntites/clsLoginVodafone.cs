using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore
{
    public class clsLoginVodafone
    {
        private string username;
        private string ndermarrja;
        private string ipKasa;
        private string emerPrinter;
        private string dyqani;

        public static string path = "";

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Ndermarrja
        {
            get { return ndermarrja; }
            set { ndermarrja = value; }
        }

        public string IpKasa
        {
            get { return ipKasa; }
            set { ipKasa = value; }
        }

        public string EmerPrinter
        {
            get { return emerPrinter; }
            set { emerPrinter = value; }
        }

        public string Dyqani
        {
            get { return dyqani; }
            set { dyqani = value; }
        }

        public clsLoginVodafone()
        {
        }

    }
}
