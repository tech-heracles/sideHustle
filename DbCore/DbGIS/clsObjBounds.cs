using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbGIS
{
    public class clsObjBounds
    {
        public clsObjPoint min { get; set; }//majtas poshte
        public clsObjPoint max { get; set; }//djathtas lart
        public clsObjBounds(clsObjPoint min, clsObjPoint max)
        {
            this.min = min;
            this.max = max;

        }
    }
}