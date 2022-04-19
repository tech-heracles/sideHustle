using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Types;

namespace DbCore.DbAdmin
{
    public class colLidhjetAutorizim : System.Collections.Generic.List<clsLidhjeAutorizim>
    {
        #region Konstruktoret

        public colLidhjetAutorizim()
        {
        }
        public colLidhjetAutorizim(IEnumerable<clsLidhjeAutorizim> lidhjet) : base(lidhjet) { }

        public colLidhjetAutorizim(int id, int idlloj, clsDatabaseAdmin data)
        {
            mbushLidhjetAutorizim(data.ktheLidhjeAutorizimSipasIdLidheseIdLloji(id, idlloj));
        }
        public colLidhjetAutorizim(int id, string kodLlojBuxheti, clsDatabaseAdmin data)
        {
            mbushLidhjetAutorizim(data.ktheLidhjeAutorizimSipasIdLidheseKodLloji(id, kodLlojBuxheti));
        }
        public colLidhjetAutorizim(string[] kodeAutorizimesh, int idLloji, int idLidhese)
        {
            if (kodeAutorizimesh == null) return;
            for (int i = 0; i < kodeAutorizimesh.Length; i++)
            {
                var idAutorizimi = clsAutorizimKoka.ktheIDAutorizim(kodeAutorizimesh[i]);
                if (idAutorizimi == 0) throw new MyException($"Autorizimi me kodin {kodeAutorizimesh[i]} nuk u ekziston"); ;
                Add(new clsLidhjeAutorizim()
                {
                    IdAutorizimeKoka = idAutorizimi,
                    IdLloji = idLloji,
                    IdLidhese = idLidhese
                });
               
            }
        }
        public colLidhjetAutorizim(int id, string kodLlojBuxheti)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                mbushLidhjetAutorizim(data.ktheLidhjeAutorizimSipasIdLidheseKodLloji(id, kodLlojBuxheti));
            }
        }

        public colLidhjetAutorizim(int id, int idlloj)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushLidhjetAutorizim(data.ktheLidhjeAutorizimSipasIdLidheseIdLloji(id, idlloj));
            data.Dispose();
        }

        public DataTable ktheLidhjetAutorizimSipasKokesAutorizim(int idAutorizimKoka, int idGjuha)
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
            {
                return data.ktheLidhjeAutorizimSipasIdKokesAutorizim(idAutorizimKoka, idGjuha);
            }
        }
        #endregion

        #region Metoda Publike

        public new clsLidhjeAutorizim this[int index]
        {
            get { return ((clsLidhjeAutorizim)base[index]); }
        }

        public clsMesazh Ruaj()
        {
            clsMesazh mesazh;
            foreach (var lidhje in this)
            {
                mesazh = lidhje.ruaj();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true, "autorizimet u ruajten ne rregull");
        }
        public clsMesazh Fshi()
        {
            clsMesazh mesazh;
            foreach (var lidhje in this)
            {
                mesazh = lidhje.fshi();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true,$"lidhje me autorizimet u fshine me sukses");
        }
        public static clsMesazh Modifiko(colLidhjetAutorizim lidhjeEkzistuese, colLidhjetAutorizim lidhjeTeReja)
        {
            clsMesazh mesazh = null;
            colLidhjetAutorizim perTuFshire = null;
            //duhen fshire te gjitha
            if ((lidhjeTeReja == null || lidhjeTeReja.Count == 0))
                perTuFshire = lidhjeEkzistuese;
            else
            {
                perTuFshire = new colLidhjetAutorizim(lidhjeEkzistuese.Except(lidhjeTeReja, new FuncEqualityComparer<clsLidhjeAutorizim>((l1, l2) => l1.IdAutorizimeKoka == l2.IdAutorizimeKoka)));
            }
            //fshihen
            foreach (var lidhje in perTuFshire)
            {
                mesazh = lidhje.fshi();
                if (!mesazh) return mesazh;
            }

            var perShtim = new colLidhjetAutorizim(lidhjeTeReja.Except(lidhjeEkzistuese, new FuncEqualityComparer<clsLidhjeAutorizim>((l1, l2) => l1.IdAutorizimeKoka == l2.IdAutorizimeKoka)));

            if (perShtim.Count > 0)
            {
                mesazh = perShtim.Ruaj();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true,"Modifikimi u krye me sukses!");
        }

        public clsMesazh fshiLidhjeAutorizim(clsDatabaseKontabilitet dbKont)
        {
            clsMesazh mesazh = new clsMesazh(true, "Fshirja perfundoi me sukses");
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbKont))
            {
                try
                {

                    foreach (clsLidhjeAutorizim o in this)
                    {
                        
                       dbAdmin.fshiLidhjeAutorizim(o.IdLidhjeAutorizim);
                       
                    }
                }catch(Exception ex)
                {
                    mesazh = new clsMesazh(false, ex.Message);
                }
            }
            return mesazh;
        }

        public clsMesazh ruajLidhjeAutorizim(int idLlog, clsDatabaseKontabilitet dbKont)
        {
            clsMesazh mesazh = new clsMesazh(true, "Ruajtja përfundoi me sukses");
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbKont))
            {
                foreach (clsLidhjeAutorizim o in this)
                {
                    o.IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Llogari", dbKont);
                    o.IdLidhese = idLlog;
                    mesazh = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!mesazh.Status)
                    {
                        break;
                    }
                }
            }
            return mesazh;
        }

        public static clsMesazh FshiLidhjeAutorizim(colLidhjetAutorizim col , int idperdoruesi, clsDatabaseAdmin dbAdmin)
        {
            colAutorizimetKoka autorizimPerd = new colAutorizimetKoka(idperdoruesi, dbAdmin);
            clsMesazh mesazh = new clsMesazh(true, "Fshirja përfundoi me sukses");
            for (int j = 0; j < col.Count; j++)
            {
                int idAutorizimKoka = col[j].IdAutorizimeKoka;
                if (idAutorizimKoka == -1)
                    continue;
                clsAutorizimKoka perdo = autorizimPerd.Find(x => x.IdAutorizimKoka == idAutorizimKoka);
                if (perdo != null)
                    mesazh = dbAdmin.fshiLidhjeAutorizim(col[j].IdLidhjeAutorizim);
            }
            return mesazh;
        }
        #endregion

        #region Metoda Private

        private bool mbushLidhjetAutorizim(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsLidhjeAutorizim(rreshti));
            }
            return true;
        }

        #endregion

    }
}

