using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
namespace DbCore.DbAdmin
{
    public class colTrupiFormatImporti : List<clsTrupiFormatImporti>
    {
        #region Metoda Publike

        public new clsTrupiFormatImporti this[int index] => base[index];

        /// <summary>
        /// Kthen trupin e format importi sipas idkoka
        /// </summary>
        /// <param name="idKoka"></param>
        /// <returns></returns>
        public static colTrupiFormatImporti merrFormatImportiTrupiSipasIdKoka(int idKoka)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                colTrupiFormatImporti trupi = new colTrupiFormatImporti();
                trupi.mbushFormatImporti(dbAdmin.merrFormatImportiTrupiSipasIdKoka(idKoka));
                return trupi;
            }
        }

        public static colTrupiFormatImporti merrFormatImportiTrupiSipasIdKoka(int idKoka, clsDatabaseAdmin dbAdmin)
        {
            colTrupiFormatImporti trupi = new colTrupiFormatImporti();
            trupi.mbushFormatImporti(dbAdmin.merrFormatImportiTrupiSipasIdKoka(idKoka));
            return trupi;
        }

        /// <summary>
        /// merr trupin e format importi sipas idkokes dhe ne do merren kolonat e dukshme apo te padukshmet
        /// </summary>
        /// <param name="idkoka">idkoka</param>
        /// <param name="visible">visible</param>
        /// <param name="indermarje">id e ndermarjes</param>
        /// <returns>kthen koleksionin e trupit me kolonat e dukshme apo te padukshme</returns>
        public static colTrupiFormatImporti merrFormatImportiTrupiSipasIdKokaDheVisible(int idkoka, bool visible)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            colTrupiFormatImporti trupi = new colTrupiFormatImporti();
            trupi.mbushFormatImporti(dbAdmin.merrFormatImportiTrupiSipasIdKokaDheVisible(idkoka, visible));
            dbAdmin.Dispose();
            return trupi;
        }

        public static colTrupiFormatImporti merrFormatImportiTrupiSipasIdKokaDetyrueshme(int idkoka, bool detyrueshme)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            colTrupiFormatImporti trupi = new colTrupiFormatImporti();
            trupi.mbushFormatImporti(dbAdmin.merrFormatImportiTrupiSipasIdKokaDetyrueshme(idkoka, detyrueshme));
            dbAdmin.Dispose();
            return trupi;
        }

        public clsTrupiFormatImporti filtroFormatImportiSipasFushes(string fusha)
        {
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.KodKontrolli == fusha)
                    return trupi;
                continue;
            }
            return new clsTrupiFormatImporti();
        }

        public colTrupiFormatImporti filtroFormatImportiVisible()
        {
            colTrupiFormatImporti colKoka = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.Visible || trupi.FusheKokeApoTrupi == 3)
                    colKoka.Add(trupi);
            }
            return colKoka;
        }

        public colTrupiFormatImporti filtroFormatImportiSipasFushaKoke()
        {
            colTrupiFormatImporti colKoka = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3)
                    colKoka.Add(trupi);
            }
            return colKoka;
        }

        public colTrupiFormatImporti filtroFormatImportiSipasFushaTrupi()
        {
            colTrupiFormatImporti colTrupi = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.FusheKokeApoTrupi == 2 || trupi.FusheKokeApoTrupi == 3)
                    colTrupi.Add(trupi);
            }
            return colTrupi;
        }

        public colTrupiFormatImporti filtroFormatImportiSipasFushaKokeVisible()
        {
            colTrupiFormatImporti colKoka = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if ((trupi.Visible && trupi.FusheKokeApoTrupi == 1) || trupi.FusheKokeApoTrupi == 3)
                    colKoka.Add(trupi);
            }
            return colKoka;
        }

        public colTrupiFormatImporti filtroFormatImportiSipasFushaTrupiVisible()
        {
            colTrupiFormatImporti colTrupi = new colTrupiFormatImporti();
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if ((trupi.Visible && trupi.FusheKokeApoTrupi == 2) || trupi.FusheKokeApoTrupi == 3)
                    colTrupi.Add(trupi);
            }
            return colTrupi;
        }

        public clsTrupiFormatImporti filtroFormatImportiPerPrimaryKey()
        {
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.FusheKokeApoTrupi == 3)
                    return trupi;
                continue;
            }
            return new clsTrupiFormatImporti();
        }

        public clsTrupiFormatImporti filtroFormatImportiPerPrimaryKeyProduktProdhimi()
        {
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.FusheKokeApoTrupi == 5)
                    return trupi;
                continue;
            }
            return new clsTrupiFormatImporti();
        }

        public string ktheEmerImportiSipasKodKontrolli(string kodKontrolli)
        {
            foreach (clsTrupiFormatImporti trupi in this)
            {
                if (trupi.KodKontrolli.Equals(kodKontrolli))
                {
                    return trupi.EmerImporti;
                }
            }
            return string.Empty;
        }

        public int ktheNumerFushash(int kokeApoTrup, int kategoria)
        {
            int i = 0;
            foreach (clsTrupiFormatImporti tr in this)
            {
                if ((kokeApoTrup == 1 && ((tr.Visible && tr.FusheKokeApoTrupi == 1) || tr.FusheKokeApoTrupi == 3 || tr.KodKontrolli == "Kod Ndermarrje")) || (kokeApoTrup == 2 && ((tr.Visible && tr.FusheKokeApoTrupi == 2) || tr.FusheKokeApoTrupi == 3 || (kategoria == 45 && tr.FusheKokeApoTrupi == 5))) || (kokeApoTrup == 4 && ((tr.Visible && tr.FusheKokeApoTrupi == 4) || tr.FusheKokeApoTrupi == 5)))
                {
                    i++;
                }
            }
            return i;
        }

        public string MerrPrimaryKey(string kategoria)
        {
            string idprimary = string.Empty;
            switch (kategoria)
            {
                case "Shitje":
                case "Blerje":
                    idprimary = ktheEmerImportiSipasKodKontrolli("Id Shitje Koka");
                    break;
                case "Magazina":
                    idprimary = ktheEmerImportiSipasKodKontrolli("Id Koka Magazina");
                    break;
                case "Veprime Arke":
                case "Veprime Banke":
                    idprimary = ktheEmerImportiSipasKodKontrolli("Id Koka");
                    break;
                case "Perfitim Buxheti":
                    idprimary = ktheEmerImportiSipasKodKontrolli("Id Koka Buxheti");
                    break;
                case "Ekzekutim Prodhimi":
                    idprimary = ktheEmerImportiSipasKodKontrolli("IdKokaEkzekutimProdhimi");
                    break;
                case "Inventarizimi":
                case "Inventarizimi afatgjate":
                    idprimary = ktheEmerImportiSipasKodKontrolli("Id Koka");
                    break;
            }

            return idprimary;
        }

        #endregion

        #region Metoda Private

        private bool mbushFormatImporti(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsTrupiFormatImporti(rreshti));
            }

            return true;
        }

        #endregion
    }
}