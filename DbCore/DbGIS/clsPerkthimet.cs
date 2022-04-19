using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class clsPerkthimet
    {
        private Dictionary <string,string> elementetPerkthyer;

        public clsPerkthimet()
        {

            elementetPerkthyer = new Dictionary<string,string>();
        }
        public void shtoElement(clsPerkthimElement el)
        {
            elementetPerkthyer.Add(el.Celesi,el.FjalaPerkthyer);

        }
        public void shtoElementCF(string Celesi, string fjala)
        {
            elementetPerkthyer.Add(Celesi, fjala);

        }
        public Dictionary<string, string> ElementetPerkthyer
        {
            get { return elementetPerkthyer; }
        }
    }
}