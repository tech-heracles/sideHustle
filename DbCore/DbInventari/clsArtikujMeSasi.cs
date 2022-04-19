using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class clsArtikujMeSasi
    {
        public int IdSeti { get; set; }

        public int IdArtikulli { get; set; }

        public float Sasi { get; set; }

        public string Mag { get; set; }

        public static List<clsArtikujMeSasi> Grupo(List<clsArtikujMeSasi> artikujt)
        {
            return artikujt?.GroupBy(x => new {x.IdSeti, x.IdArtikulli, x.Mag }).Select(x => new clsArtikujMeSasi {IdSeti = x.Key.IdSeti, IdArtikulli = x.Key.IdArtikulli, Mag = x.Key.Mag, Sasi = x.Sum(s => s.Sasi) }).ToList();
        }
    }
}
