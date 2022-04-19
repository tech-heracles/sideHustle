using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbGIS
{
    public class clsObjPoint
    {
        public decimal lon { get; set; }
        public decimal lat { get; set; }

        public clsObjPoint(decimal longtitude, decimal latitude)
        {
            lon = longtitude;
            lat = latitude;
        }
    }
}