using System;
using DbCore.DbInventari;
using DbCore;

namespace DbCore
{
    public struct ListeVleraArtikulliMeNjesiKodbaresh
    {
        public int idRreshti;
        public clsArtikulli artikulli;
        public double gjendjeTot;
        public ComboListTvsh[] listeTvsh;
        public clsDetajimArtikulli detajimiPare;
        public clsDetajimArtikulli detajimiDyte;
        public string kodbari;
        public int njesia;
        public clsMesazh detajimFundit;
        public object magazinatTrupi;
        public bool kerkoMeKodbar;
    }
}
