using System;
using DbCore.DbInventari;
using DbCore;

namespace DbCore
{
    public struct ListeVleraTePlotaArtikulli
    {
        public int idRreshti;
        public clsArtikulli artikulli;
        public double gjendjeTot;
        public double gjendjeMag;
        public ComboListTvsh[] listeTvsh;
        public clsDetajimArtikulli detajimiPare;
        public clsDetajimArtikulli detajimiDyte;
        public string kodbari;
        public int njesia;
        public string koefArtPerbere;
        public string pershkrimMag;
        public clsZbritjeAnalitike zbritjeAnalitike;
        public object[] cmimArtikulliResult;
        public colGjendjeArtikulli colGjendjeArtikulli;
        public clsMesazh detajimFundit;
        public object magazinatTrupi;
        public bool kerkoMeKodbar;
        public int counterWsKodi;
    }
}