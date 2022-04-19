using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public struct clsPerkthimElement
    {

        private string celesi, fjalaPerkthyer;
        public string Celesi
        {
            set { celesi = value; }
            get { return celesi; }

        }
        public string FjalaPerkthyer
        {
            set { fjalaPerkthyer = value; }
            get { return fjalaPerkthyer; }

        }
        public clsPerkthimElement(string cel, string fjala)
        {
            celesi = cel;
            fjalaPerkthyer = fjala;
        }
    }
}