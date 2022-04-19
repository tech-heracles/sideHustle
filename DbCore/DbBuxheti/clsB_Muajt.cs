using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbBuxheti
{
    public class ClsBMuajt
    {
        public decimal Janar { get; set; }
        public decimal Shkurt { get; set; }
        public decimal Mars { get; set; }
        public decimal Prill { get; set; }
        public decimal Maj { get; set; }
        public decimal Qershor { get; set; }
        public decimal Korrik { get; set; }
        public decimal Gusht { get; set; }
        public decimal Shtator { get; set; }
        public decimal Tetor { get; set; }
        public decimal Nentor { get; set; }
        public decimal Dhjetor { get; set; }

        private static Dictionary<int, string> muaj = new Dictionary<int, string>{
                { 1 , "Janar"},
                { 2 , "Shkurt"},
                { 3 , "Mars"},
                { 4 , "Prill"},
                { 5 , "Maj"},
                { 6 , "Qershor"},
                { 7 , "Korrik"},
                { 8 , "Gusht"},
                { 9 , "Shtator"},
                { 10 , "Tetor"},
                { 11 , "Nentor"},
                { 12 , "Dhjetor" }
            };

        public static string Muaji(int muajiNr){
            return muaj[muajiNr];
        }

        public static int MuajiNr(string muaji ) => muaj.FirstOrDefault(x => x.Value == muaji).Key;

        public ClsBMuajt() { }
    }

    
}
