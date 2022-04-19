using DbCore.DbRegjistrim;
using DbCore.DbShare;
using System;

namespace DbCore
{
    public struct ListeDokKontrolloKonvertuar
    {
        public colNivelRegjistrimi nivelet;
        public string mesazh;
        public int[] ids;
        public colKonfigurimAmbjenti colKonfig;
    }
}