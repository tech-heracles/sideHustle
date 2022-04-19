using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Types;

namespace DbCore.DbAdmin
{
    public class colFushatShtese : List<clsFushaShtese>, IDataBase
    {


        #region Konstruktoret
        public colFushatShtese() { }
        public colFushatShtese(IEnumerable<clsFushaShtese> colleciton) : base(colleciton) { }
        public colFushatShtese(int idmodeli)
        {
            using (var dbAdmin = new clsDatabaseAdmin())
                dbAdmin.ktheFushatShteseSipasModelit(idmodeli, this);
        }
        #endregion

        #region Metoda Publike

        public new clsFushaShtese this[int index] => base[index];

        public void MbushFushatShteseSipasAtit(int ati)
        {
            using (var data = new clsDatabaseAdmin())
                data.ktheFushatShteseSipasAtit(ati, this);
        }
        public clsMesazh Ruaj()
        {
            clsMesazh mesazh = null;
            foreach (var o in this)
            {
                //behet ruajtja e fushave shtese 
                var idFushaShteseOld = o.IdFushaShtese;
                mesazh = o.Ruaj();
                if (!mesazh) return mesazh;
                if (!(o.AtiTipiFushaShtese > 0))
                {
                    mesazh = o.ruajFushePerLidhjetEkzistuese();
                    if (!mesazh) return mesazh;
                }
                //update-on id e prindit pas ruajtjes
                this.ForEach(fushaShtese =>
                  {
                      if (fushaShtese.AtiTipiFushaShtese == idFushaShteseOld)
                          fushaShtese.AtiTipiFushaShtese = o.IdFushaShtese;
                  });

            }

            return new clsMesazh(true);
        }

        public clsMesazh Modifiko()
        {
            clsMesazh mesazh = null;
            foreach (var o in this)
            {
                mesazh = o.Modifiko();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true);
        }
        public static clsMesazh Modifiko(colFushatShtese fushaTeReja, colFushatShtese fushaEkzistuese)
        {
            var fushaPerModifikim = new colFushatShtese() { Capacity = fushaEkzistuese.Count };
            var fushaPerTuFshire = new colFushatShtese(fushaEkzistuese.Except(fushaTeReja, new FuncEqualityComparer<clsFushaShtese>((f1, f2) => f1.IdFushaShtese == f2.IdFushaShtese)));
            var fushaPerShtim = new colFushatShtese() { Capacity = fushaTeReja.Count };

            fushaTeReja.ForEach(fusha =>
            {
                if (fusha.IdFushaShtese < 0) fushaPerShtim.Add(fusha);
                else fushaPerModifikim.Add(fusha);
            });

            var mesazh = fushaPerTuFshire.Fshi();
            if (!mesazh) return mesazh;

            mesazh = fushaPerShtim.Ruaj();
            if (!mesazh) return mesazh;

            mesazh = fushaPerModifikim.Modifiko();
            if (!mesazh) return mesazh;

            return new clsMesazh(true);
        }
        public clsMesazh Fshi()
        {
            foreach (var o in this)
            {
                var mesazh = o.Fshi();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true);
        }
        /// <summary>
        /// kontrollon nese nje fusheshtese  eshte prind
        /// </summary>
        /// <param name="fushatEKetijModeli"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public bool EshteFushePrind(clsFushaShtese f)
        {
            bool fushPrind;
            if (f.TipiFushaShtese == 6)
            {
                var prindi = Find(x => x.IdFushaShtese == f.AtiTipiFushaShtese);
                if (prindi != null && prindi.TipiFushaShtese == 6)
                    fushPrind = false;
                else fushPrind = true;
            }
            else fushPrind = true;
            return fushPrind;
        }
        public void Mbush(IDataRecord record)
        {
            var fushaShtese = new clsFushaShtese();
            fushaShtese.Mbush(record);
            Add(fushaShtese);
        }
        #endregion


    }
}