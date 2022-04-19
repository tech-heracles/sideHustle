using System;
using System.Collections.Generic;
using System.Data;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsVeprimeKFTrupi
    {
        #region Properties

        /// <summary>
        /// get set id e trupit te dokumentit
        /// </summary>
        public int IdVeprimeKFTrupi { get; set; }

        /// <summary>
        /// get set id e kokes te dokumentit
        /// </summary>
        public int IdVeprimeKFKoka { get; set; }

        /// <summary>
        /// get set id e klient furnitorit
        /// </summary>
        public int IdKF { get; set; }

        public int IdKfKunderParti { get; set; }

        public string KodKF { get; set; }

        /// <summary>
        /// get set pershkrimin
        /// </summary>
        public string Pershkrimi { get; set; }

        /// <summary>
        /// get set daten
        /// </summary>
        public DateTime Data { get; set; }

        /// <summary>
        /// get set debi/kredi
        /// </summary>
        /// <example>1-debi, 2 -kredi</example>
        public int DebiKredi { get; set; }

        /// <summary>
        /// get set vleften
        /// </summary>
        public double Vlefta { get; set; }

        /// <summary>
        /// get set id e monedhes
        /// </summary>
        public int IdMonedha { get; set; }

        public string KodMonedha { get; set; }

        /// <summary>
        /// get set kursin
        /// </summary>
        public double Kursi { get; set; }

        /// <summary>
        /// get set vleften ne monedhen baze
        /// </summary>
        public double VleftaMonBaze { get; set; }

        /// <summary>
        /// get set id e llogarise kunderparti
        /// </summary>
        public int IdLlogKunderParti { get; set; }

        /// <summary>
        /// get set id e fatures
        /// </summary>
        public int IdFatura { get; set; }

        /// <summary>
        /// get set id e llogarise kunderparti
        /// </summary>
        public int IdNivelFatura { get; set; }

        public string NrLlogKunderParti { get; set; }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsVeprimeKFTrupi()
        {
        }

        public clsVeprimeKFTrupi(Dictionary<string, object> rreshtDokuKlient, object nivele, int idndermarje, object id, int idperdoruesi, bool meklient, string monbaze)
        {
            var kodi = rreshtDokuKlient["txtKodi"].ToString();
            var pershkrimi = rreshtDokuKlient["txtPershkrim"].ToString();
            var data = rreshtDokuKlient["dteData"].ToString();
            var dk = rreshtDokuKlient["cmbDebiKredi"].ToString();
            var vlefta = rreshtDokuKlient["txtVlefta"].ToString();
            var monedha = rreshtDokuKlient["txtMonedha"].ToString();
            var kursi = rreshtDokuKlient["txtKursi"].ToString();
            var vleftamon = rreshtDokuKlient["txtVleftaMon"].ToString();
            var nrfat = rreshtDokuKlient["txtNrFature"].ToString();
            var llog = rreshtDokuKlient["txtLlogKunderparti"].ToString();

            if (kodi != null && kodi != "null" && kodi != "")
            {
                if (kodi != "" && !clsKlientFurnitor.EkzistonKlientFurnitor(kodi, idndermarje))
                    throw new Exception("Klient/Furnitori nuk ekziston!");

                var kf = new clsKlientFurnitor();
                kf.mbushKlientFurnitorSipasKodit(kodi, idndermarje, idperdoruesi);
                if (kf.IdKlientFurnitor < 1)
                    throw new Exception("Ju nuk keni autorizime per kete klient/furnitor!");

                if (kodi != "" && !kf.AktivKF)
                    throw new Exception("Klient/Furnitori nuk është aktiv!");

                IdKF = kf.IdKlientFurnitor;
                KodKF = kodi;

                if (pershkrimi != null && pershkrimi != "null")
                    Pershkrimi = pershkrimi;

                if (data != null && data != "null" && data != "")
                    Data = DateTime.Parse(data);

                if (dk != null && dk != "null" && dk != "")
                    DebiKredi = dk == "Debi" ? 1 : 2;

                if (vlefta != null && vlefta != "null" && vlefta != "")
                    Vlefta = double.Parse(vlefta);

                if (monedha != null && monedha != "null" && monedha != "")
                {
                    KodMonedha = monedha;
                    var mon = new clsMonedha();
                    mon.mbushMonedhen(monedha, idndermarje);
                    IdMonedha = mon.IdMonedha;
                }

                if (kursi != null && kursi != "null" && kursi != "")
                    Kursi = double.Parse(kursi);

                if (vleftamon != null && vleftamon != "null" && vleftamon != "")
                    VleftaMonBaze = double.Parse(vleftamon);

                if (llog != null && llog != "null" && llog != "")
                {
                    if (meklient)
                    {
                        var klient = new clsKlientFurnitor();
                        klient.mbushKlientFurnitorSipasKodit(llog, idndermarje);
                        if (klient.IdKlientFurnitor <= 0)
                            throw new Exception("Klient/Furnitori kunderparti nuk ekziston!");

                        if (klient.IdKlientFurnitor > 0 && klient.AktivKF)
                        {
                            if (klient.Monedha != kf.Monedha && klient.Monedha != monbaze && kf.Monedha != monbaze)
                            {
                                throw new Exception("Klient/furnitoret jane ne monedha te ndryshme dhe te ndryshme nga monedha baze!");
                            }

                            IdKfKunderParti = klient.IdKlientFurnitor;
                        }
                        else
                            throw new Exception("Klient/Furnitori kunderparti nuk eshte aktiv!");
                    }
                    else
                    {
                        var llogari = new clsLlogari(llog, idndermarje);
                        if (llogari.IdLlogari <= 0)
                            throw new Exception("Llogaria kunderparti nuk ekziston!");

                        if (llogari.IdLlogari > 0 && llogari.Aktiv)
                            IdLlogKunderParti = llogari.IdLlogari;
                        else
                            throw new Exception("Llogaria kunderparti nuk eshte aktiv!");
                    }
                }
                else if (meklient)
                    throw new Exception("Plotesoni klient/furnitorin kunderparti");
                else
                    throw new Exception("Plotesoni llogarine kunderparti");

                if (nrfat != null && nrfat != "null" && nrfat != "")
                {
                    IdFatura = int.Parse(id.ToString());
                    IdNivelFatura = int.Parse(nivele.ToString());
                }
            }
            else
                IdKF = -1;
        }

        public clsVeprimeKFTrupi(DataRow rreshti)
        {
            MbushVeprimeKfTrupi(rreshti);
        }

        #endregion

        #region Metoda Internal
        
        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool MbushVeprimeKfTrupi(DataRow rreshti)
        {
            try
            {
                IdVeprimeKFTrupi = ToInt32(rreshti["IDVEPRIMEKFTRUPI"]);
                IdVeprimeKFKoka = ToInt32(rreshti["IDVEPRIMEKFKOKA"]);
                IdKF = ToInt32(rreshti["IDKF"]);
                Pershkrimi = Convert.ToString(rreshti["PERSHKRIMI"]);
                Data = ToDateTime(rreshti["DATA"]);
                DebiKredi = ToInt32(rreshti["DEBIKREDI"]);
                Vlefta = double.Parse(rreshti["VLEFTA"].ToString());
                IdMonedha = ToInt32(rreshti["MONEDHA"]);
                Kursi = double.Parse(rreshti["KURSI"].ToString());
                VleftaMonBaze = double.Parse(rreshti["VLEFTAMONBAZE"].ToString());

                IdLlogKunderParti = !IsDBNull(rreshti["IDLLOGKUNDERPARTI"])
                    ? ToInt32(rreshti["IDLLOGKUNDERPARTI"])
                    : 0;
                IdKfKunderParti = !IsDBNull(rreshti["IDKFKUNDERPARTI"])
                    ? ToInt32(rreshti["IDKFKUNDERPARTI"])
                    : 0;
                IdFatura = !IsDBNull(rreshti["IDFATURA"])
                    ? ToInt32(rreshti["IDFATURA"])
                    : 0;
                IdNivelFatura = !IsDBNull(rreshti["IDNIVELFATURA"])
                    ? ToInt32(rreshti["IDNIVELFATURA"])
                    : 0;
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Metoda Publike

        public clsMesazh KrijoTrupVeprimeKfImport(DbData dbData, string kodKf, string monedha, string llogKunderparti, string debiKredi, double vlefta, double vleftaMonBaze, int idNdermarrje, DateTime dtdok, int llojKursi, double kursi, clsMonedha monedhaNdermarrjes, string kfKunderparti, bool kushtNDKF)
        {
            clsDatabaseKontabilitet dbKontabilitet = new clsDatabaseKontabilitet(dbData);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
            if (string.IsNullOrEmpty(kodKf))
                return new clsMesazh(false, "Plotesoni kodin e klient/furnitorit!");

            var klfurnKunderparti = new clsKlientFurnitor();
            clsLlogari llogariKunderparti = new clsLlogari();

            var kf = new clsKlientFurnitor(kodKf, idNdermarrje, dbKontabilitet);
            if (kf.IdKlientFurnitor == 0)
                return new clsMesazh(false, $"Klient/furnitori {kodKf} nuk ekziston!");

            if (!kf.AktivKF)
                return new clsMesazh(false, $"Klient/furnitori {kodKf} nuk eshte aktiv!");

            if (!kushtNDKF)
            {
                kfKunderparti = string.Empty;

                if (string.IsNullOrEmpty(llogKunderparti))
                    return new clsMesazh(false, "Plotesoni llogarine kunderparti!");

                llogariKunderparti = new clsLlogari(llogKunderparti, idNdermarrje, dbKontabilitet);
                if (llogariKunderparti.IdLlogari == 0)
                    return new clsMesazh(false, $"Llogaria kunderparti {llogKunderparti} nuk ekziston!");

                if (!llogariKunderparti.Aktiv)
                    return new clsMesazh(false, $"Llogaria kunderparti {llogKunderparti} nuk eshte aktive!");
            }
            else
            {
                llogKunderparti = string.Empty;

                if (string.IsNullOrEmpty(kfKunderparti))
                    return new clsMesazh(false, "Plotesoni klient/furnitorin kunderparti!");

                if (kfKunderparti.ToLower() == kodKf.ToLower())
                    return new clsMesazh(false, "Nuk lejohet i njejti klient/furnitor ne te dyja fushat!");

                klfurnKunderparti = new clsKlientFurnitor(kfKunderparti, idNdermarrje, dbKontabilitet);
                if (klfurnKunderparti.IdKlientFurnitor == 0)
                    return new clsMesazh(false, $"Klient/furnitori kunderparti {kfKunderparti} nuk ekziston!");

                if (!klfurnKunderparti.AktivKF)
                    return new clsMesazh(false, $"Klient/furnitori kunderparti {kfKunderparti} nuk eshte aktiv!");


                if (klfurnKunderparti.Monedha != kf.Monedha && klfurnKunderparti.Monedha != monedhaNdermarrjes.PershkrimiMonedha && kf.Monedha != monedhaNdermarrjes.PershkrimiMonedha)
                    return new clsMesazh(false, $"Klient/furnitoret {kodKf} dhe {kfKunderparti} jane ne monedha te ndryshme dhe te ndryshme nga monedha baze!");
            }

            
            IdKF = kf.IdKlientFurnitor;
            KodKF = kodKf;
            Pershkrimi = string.Empty;
            Data = dtdok; //DateTime.Now;
            switch (debiKredi.ToLower())
            {
                case "debi":
                    DebiKredi = 1;
                    break;
                case "kredi":
                    DebiKredi = 2;
                    break;
                default:
                    return new clsMesazh(false, "Fusha Debi/Kredi nuk ka vlere te sakte!");
            }

            var mon = new clsMonedha();
            mon.mbushMonedhenEKlientit(kodKf, idNdermarrje);

            IdMonedha = mon.IdMonedha;
            KodMonedha = mon.KodiMonedha;
            IdLlogKunderParti = llogariKunderparti.IdLlogari;
            IdKfKunderParti = klfurnKunderparti.IdKlientFurnitor;
            IdFatura = 0;
            IdNivelFatura = 0;
            NrLlogKunderParti = llogKunderparti;

            if (kursi != 0)
            {
                if (mon.IdMonedha == monedhaNdermarrjes.IdMonedha && kursi != 1)
                    return new clsMesazh(false, string.Format("Kursi nuk mund te jete ndryshe nga 1 per klientin {0}!", KodKF));
                Kursi = kursi;
            }
            else
                Kursi = clsKurset.getKursSipasIdMonedhaDheLlojiFromCache(mon.IdMonedha, dtdok, llojKursi, dbAdmin).VleraKursi;

            Vlefta = vlefta;
            if (vleftaMonBaze != 0 && vlefta * Kursi != vleftaMonBaze)
                return new clsMesazh(false, "Vlefta ne monedhe baze nuk eshte e barabarte me prodhimin e vleftes me kursin!");

            VleftaMonBaze = vlefta * Kursi;
            return new clsMesazh(true, "Trupi i veprime klient/furnitor u krijua me sukses!");
        }
        
        /// <summary>
        /// krijon objektin
        /// </summary>
        /// <param name="data">data </param>
        /// <param name="debiKredi">debi kredi 1-debi, 2-kredi</param>
        /// <param name="idKF">id e klient furnitorit</param>
        /// <param name="idLlogKunderParti">id e llogarise kunderparti</param>
        /// <param name="idMonedha">id e monedhes</param>
        /// <param name="idVeprimeKFKoka">id e kokes se dokumentit</param>
        /// <param name="idVeprimeKFTrupi">id ritese e trupit te dokumentit</param>
        /// <param name="kursi">kursi</param>
        /// <param name="pershkrimi">pershkrimi</param>
        /// <param name="vlefta"> vlefta</param>
        /// <param name="vleftaMonBaze"> vlefta ne monedhen baze</param>
        /// <returns>kthen id-ne e rolit te krijuar ose zero nese krijimi deshtoi</returns>
        public clsMesazh Ruaj(clsDatabaseRegjistrim datab)
        {
            int idVeprimeKfTrupi = datab.ruajVeprimeKFTrupi(out idVeprimeKfTrupi, IdVeprimeKFKoka, IdKF, Pershkrimi, Data, DebiKredi, Vlefta, IdMonedha, Kursi,
                VleftaMonBaze, IdLlogKunderParti, IdFatura, IdNivelFatura, IdKfKunderParti);

            var mesazh = idVeprimeKfTrupi <= 0 
                ? new clsMesazh(false, MessagesResource.Messages["msgNukURuajtTrupiIDokumentit"])
                : new clsMesazh(true);

            return mesazh;
        }

        #endregion
    }
}
