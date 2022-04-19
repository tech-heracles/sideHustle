using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using DbCore.DbAdmin;
using DbCore.DbQendraKosto;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje element te trupit te fletes kontabel
    ///  (Te dhenat  merren nga tabela : T_TRUPIFLETEKONTABEL)
    ///</remarks>
    public class clsTrupiFleteKontabel
    {
        #region Atribute

        private DataRow _rreshti;

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se fletes kontabel te ciles i perket trupi
        /// </summary>
        public int IdKokaFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise
        /// </summary>
        public int IdLlogari { get; set; }

        /// <summary>
        /// Kthen/Vendos emrin e llogarise
        /// </summary>
        public string EmerLlogari { get; set; }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise
        /// </summary>
        public string NrLlogari { get; set; }

        /// <summary>
        /// Kthen/Vendos pershkrimin e trupit
        /// </summary>
        public string PershkrimTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes
        /// </summary>
        public int IdMonedha { get; set; }

        /// <summary>
        /// Kthen/Vendos kusin e monedhes
        /// </summary>
        public double Kursi { get; set; }

        /// <summary>
        /// Kthen/Vendos nese llogaria preket ne debi apo ne kredi
        /// </summary>
        public string DK { get; set; }

        /// <summary>
        /// Kthen/Vendos vleften me te cilen preket llogaria ne debi
        /// </summary>
        public double VleftaDebiTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos vleften me te cilen preket llogaria ne kredi
        /// </summary>
        public double VleftaKrediTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos vleren ne monedhe baze me te cilen preket llogaria ne debi
        /// </summary>
        public double VleftaDebiMonBazeTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos vleren ne monedhe baze me te cilen preket llogaria ne kredi
        /// </summary>
        public double VleftaKrediMonBazeTrupiFleteKontabel { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e monedhes
        /// </summary>
        public string KodMonedha { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e skemes kontabel
        /// </summary>
        public string KodiSkemaKontabel { get; set; }

        #endregion

        #region Kontruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTrupiFleteKontabel(int idtrupifletekontabel,
            int idkokafletekontabel,
            int idllogari,
            string pershkrimtrupifletekontabel,
            int idmonedha,
            double kurs,
            double vleftadebitrupifletekontabel,
            double vleftakreditrupifletekontabel,
            string kodmonedha,
            string kodiskemakontabel,
            double vleftadebimonbazetrupifletakontabel,
            double vleftakredimonbazetrupifletakontabel)
        {
            IdTrupiFleteKontabel = idtrupifletekontabel;
            IdKokaFleteKontabel = idkokafletekontabel;
            IdLlogari = idllogari;
            PershkrimTrupiFleteKontabel = pershkrimtrupifletekontabel;
            IdMonedha = idmonedha;
            Kursi = kurs;
            VleftaDebiTrupiFleteKontabel = vleftadebitrupifletekontabel;
            VleftaKrediTrupiFleteKontabel = vleftakreditrupifletekontabel;
            KodMonedha = kodmonedha;
            KodiSkemaKontabel = kodiskemakontabel;
            VleftaDebiMonBazeTrupiFleteKontabel = vleftadebimonbazetrupifletakontabel;
            VleftaKrediMonBazeTrupiFleteKontabel = vleftakredimonbazetrupifletakontabel;
        }

        public clsTrupiFleteKontabel(int idNdermarrje,
            bool azhornim,
            int idmonnderm,
            string nrLlog,
            string emerllog,
            string pershkrimi,
            string monedha,
            string kursi,
            string debi,
            string kredi,
            string debimon,
            string kredimon,
            DateTime dtDok,
            int llojKursi = 0)
        {
            var llog = new clsLlogari();
            if (nrLlog != null && nrLlog != "null" && nrLlog != "")
            {
                llog = new clsLlogari(nrLlog, idNdermarrje);
                IdLlogari = llog.IdLlogari;
                if (IdLlogari <= 0)
                    throw new Exception("Nje nga llogarite nuk ekziston!");
                if (!llog.Aktiv)
                    throw new Exception("Nje nga llogarite nuk eshte aktive!");
                EmerLlogari = emerllog;
                if (pershkrimi != "null")
                    PershkrimTrupiFleteKontabel = pershkrimi;
                ValidoMonedhen(monedha, idNdermarrje, llog);

                if (kursi != "null" && kursi != "")
                    if (kursi == "0")
                        Kursi = llog.IdMonedha != idmonnderm
                            ? clsKurset.merrKursinEFundit(llog.IdMonedha, llojKursi)
                            : 1;
                    else
                        Kursi = double.Parse(kursi);

                if (debi != "null" && debi != "")
                {
                    if (azhornim && llog.IdMonedha != idmonnderm)
                        VleftaDebiTrupiFleteKontabel = 0;
                    else if (debi == "0" && debimon != "0")
                        VleftaDebiTrupiFleteKontabel = double.Parse(debimon) / Kursi;
                    else
                        VleftaDebiTrupiFleteKontabel = double.Parse(debi);
                }

                //if (debi != "null" && debi != "")
                //    if (debi == "0" && debimon != "0")
                //        VleftaDebiTrupiFleteKontabel = azhornim && llog.IdMonedha == idmonnderm
                //            ? double.Parse(debimon)
                //            : double.Parse(debimon) / Kursi;
                //    else
                //        VleftaDebiTrupiFleteKontabel = azhornim && llog.IdMonedha == idmonnderm
                //            ? double.Parse(debimon)
                //            : double.Parse(debi);

                if (kredi != "null" && kredi != "")
                {
                    if (azhornim && llog.IdMonedha != idmonnderm)
                        VleftaKrediTrupiFleteKontabel = 0;
                    else if (kredi == "0" && kredimon != "0")
                        VleftaKrediTrupiFleteKontabel = double.Parse(kredimon) / Kursi;
                    else
                        VleftaKrediTrupiFleteKontabel = double.Parse(kredi);
                }

                //if (kredi != "null" && kredi != "")
                //if (kredi == "0" && kredimon != "0")
                //    VleftaKrediTrupiFleteKontabel = azhornim && llog.IdMonedha == idmonnderm
                //        ? double.Parse(kredimon)
                //        : double.Parse(kredimon) / Kursi;
                //else
                //    VleftaKrediTrupiFleteKontabel = azhornim && llog.IdMonedha == idmonnderm
                //        ? double.Parse(kredimon)
                //        : double.Parse(kredi);

                if (debimon != "null" && debimon != "")
                    if (debimon == "0" && debi != "0")
                        VleftaDebiMonBazeTrupiFleteKontabel = double.Parse(debi) * Kursi;
                    else
                        VleftaDebiMonBazeTrupiFleteKontabel = double.Parse(debimon);

                if (kredimon != "null" && kredimon != "")
                    if (kredimon == "0" && kredi != "0")
                        VleftaKrediMonBazeTrupiFleteKontabel = double.Parse(kredi) * Kursi;
                    else
                        VleftaKrediMonBazeTrupiFleteKontabel = double.Parse(kredimon);

                KodiSkemaKontabel = "";
            }
            else
                IdLlogari = -1;
            if (IdLlogari != -1)
                clsKokaFleteKontabel.ShtoTeDhenaPerObjektivat(dtDok, new colObjektivaKosto(), new List<double>(), new List<double>(), new List<int>(), llog, this, new clsObjektivaKosto(llog.IdObjektivaKosto));
        }

        //private void ValidoLlogarine(string llog)
        //{
        //    if (debimon != "null" && debimon != "")
        //        IdLlogari = llog.IdLlogari;
        //    if (IdLlogari <= 0)
        //        throw new Exception("Nje nga llogarite nuk ekziston!");
        //    if (!llog.Aktiv)
        //        throw new Exception("Nje nga llogarite nuk eshte aktive!");
        //}

        private void ValidoMonedhen(string monedha, int idNdermarrje, clsLlogari llog)
        {
            if (monedha == "null" || monedha == "")
                IdMonedha = llog.IdMonedha;
            else
            {
                var mon = new clsMonedha();
                mon.mbushMonedhen(monedha, idNdermarrje);
                if (mon.IdMonedha == 0)
                    throw new Exception("Nje nga monedhat nuk ekziston!");
                IdMonedha = mon.IdMonedha;
            }
        }

        public clsTrupiFleteKontabel(int idkoka, int idllogari)
        {
            using (var dbTrupFletKont = new clsDatabaseKontabilitet())
                MbushTrupFletKont(dbTrupFletKont.ktheTrupatFleteKontabelSipasKokesDheLlogarisePerQK(idkoka, idllogari));
        }

        /// <summary>
        /// Konstruktori pa parametra
        /// </summary>
        public clsTrupiFleteKontabel()
        {
        }

        public clsTrupiFleteKontabel Clone() => (clsTrupiFleteKontabel)MemberwiseClone();

        public clsTrupiFleteKontabel(int debikredi, double vlera, clsLlogari oLlogari, bool azhornim, double? kursi, int? idMonedha, bool eshteAzhornim, string pershkrimi, colKurset kurseDate, clsMonedha monedheNdermarrje, clsDatabaseAdmin dbA, bool llogaritKurs = true)
        {
            NrLlogari = oLlogari.NrLlogari;
            IdLlogari = oLlogari.IdLlogari;
            EmerLlogari = oLlogari.EmerLlogari1;
            var monKf = (azhornim ? monedheNdermarrje : new clsMonedha(oLlogari.IdMonedha, dbA)) ?? new clsMonedha();
            Kursi = llogaritKurs ? LlogaritKursin(kursi, idMonedha, azhornim, monKf, kurseDate) : Convert.ToDouble(kursi);
            IdMonedha = monKf.IdMonedha;
            KodMonedha = monKf.KodiMonedha;
            KodiSkemaKontabel = "";
            PershkrimTrupiFleteKontabel = pershkrimi;
            switch (debikredi)
            {
                case 1:
                    if (azhornim && monKf.IdMonedha != oLlogari.IdMonedha)
                        VleftaDebiTrupiFleteKontabel = 0;
                    else if (eshteAzhornim)
                        VleftaDebiTrupiFleteKontabel = Math.Abs(vlera / Kursi);
                    else
                        VleftaDebiTrupiFleteKontabel = vlera / Kursi;

                    VleftaKrediTrupiFleteKontabel = 0;
                    VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    VleftaDebiMonBazeTrupiFleteKontabel = eshteAzhornim ? Math.Abs(vlera) : vlera;
                    DK = "D";
                    break;
                case 2:
                    VleftaDebiTrupiFleteKontabel = 0;
                    if (azhornim && monKf.IdMonedha != oLlogari.IdMonedha)
                        VleftaKrediTrupiFleteKontabel = 0;
                    else if (eshteAzhornim)
                        VleftaKrediTrupiFleteKontabel = Math.Abs(vlera / Kursi);
                    else
                        VleftaKrediTrupiFleteKontabel = vlera / Kursi;
                    VleftaKrediMonBazeTrupiFleteKontabel = eshteAzhornim ? Math.Abs(vlera) : vlera;
                    VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    DK = "K";
                    break;
            }
        }

        public clsTrupiFleteKontabel(clsDatabaseAdmin dbAdmin, int debikredi, double vlera, clsLlogari oLlogari, bool azhornim, int idndermarje, double? kursi, int? idMonedha, DateTime data, string pershkrimi, bool eshteAzhornim)
        {
            NrLlogari = oLlogari.NrLlogari;
            IdLlogari = oLlogari.IdLlogari;
            EmerLlogari = oLlogari.EmerLlogari1;
            int idMonKf = oLlogari.IdMonedha;
            if (azhornim)
            {
                clsMonedha monedhaNderm = new clsMonedha();
                monedhaNderm.mbushMonedhenENdermarrjes(idndermarje, dbAdmin);
                idMonKf = monedhaNderm.IdMonedha;
                IdMonedha = idMonKf;
                KodMonedha = monedhaNderm.KodiMonedha;
                Kursi = 1;
            }
            else
            {
                IdMonedha = idMonKf;
                KodMonedha = (new clsMonedha(idMonKf, dbAdmin)).KodiMonedha;
                if (idMonedha.HasValue && IdMonedha == idMonedha)
                    Kursi = kursi.Value;
                else
                {
                    var kurs = clsKurset.getKursSipasIdMonedhaFromCache(IdMonedha, data, dbAdmin);
                    Kursi = kurs.VleraKursi > 0 ? kurs.VleraKursi : 1;
                }
            }

            KodiSkemaKontabel = "";
            PershkrimTrupiFleteKontabel = pershkrimi;
            switch (debikredi)
            {
                case 1:
                    if (azhornim && idMonKf != oLlogari.IdMonedha)
                        VleftaDebiTrupiFleteKontabel = 0;
                    else
                    if (eshteAzhornim)
                        VleftaDebiTrupiFleteKontabel = Math.Abs(vlera / (Kursi == 0 ? 1 : Kursi));
                    else VleftaDebiTrupiFleteKontabel = vlera / (Kursi == 0 ? 1 : Kursi);

                    VleftaKrediTrupiFleteKontabel = 0;
                    VleftaKrediMonBazeTrupiFleteKontabel = 0;
                    VleftaDebiMonBazeTrupiFleteKontabel = eshteAzhornim ? Math.Abs(vlera) : vlera;
                    DK = "D";
                    break;
                case 2:
                    VleftaDebiTrupiFleteKontabel = 0;
                    if (azhornim && idMonKf != oLlogari.IdMonedha)
                        VleftaKrediTrupiFleteKontabel = 0;
                    else
                    if (eshteAzhornim)
                        VleftaKrediTrupiFleteKontabel = Math.Abs(vlera / (Kursi == 0 ? 1 : Kursi));
                    else
                        VleftaKrediTrupiFleteKontabel = vlera / (Kursi == 0 ? 1 : Kursi);
                    VleftaKrediMonBazeTrupiFleteKontabel = eshteAzhornim ? Math.Abs(vlera) : vlera;
                    VleftaDebiMonBazeTrupiFleteKontabel = 0;
                    DK = "K";
                    break;
            }
        }

        public clsTrupiFleteKontabel(DataRow rreshti)
        {
            MbushTrupFletKont(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Metoda qe krijon trupin e fletes kontabel nga importi
        /// </summary>
        /// <param name="nrllog"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="kodmon"></param>
        /// <param name="kursi"></param>
        /// <param name="vleftaDebiTrupiFleteKontabel"></param>
        /// <param name="vleftaKrediTrupiFleteKontabel"></param>
        /// <param name="vleftaDebiMonBazeTrupiFleteKontabel"></param>
        /// <param name="vleftaKrediMonBazeTrupiFleteKontabel"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="dtDok"></param>
        /// <returns></returns>
        public clsMesazh KrijoTrupFleteKontabelImport(string nrllog, string pershkrimi, string kodmon, double kursi, double vleftaDebiTrupiFleteKontabel, double vleftaKrediTrupiFleteKontabel, double vleftaDebiMonBazeTrupiFleteKontabel, double vleftaKrediMonBazeTrupiFleteKontabel, int idNdermarrje, DateTime dtDok)
        {
            int idLlog;
            string emerllog;
            int idMon;
            if (nrllog == "")
                return new clsMesazh(false, "Llogaria nuk mund te jete bosh!");
            {
                var llog = new clsLlogari(nrllog, idNdermarrje);
                if (llog.IdLlogari <= 0)
                    return new clsMesazh(false, "Llogaria " + nrllog + " nuk ekziston!");
                if (!llog.Aktiv)
                    return new clsMesazh(false, "Llogaria " + nrllog + " nuk eshte aktive!");

                idLlog = llog.IdLlogari;
                emerllog = llog.EmerLlogari1;
                idMon = llog.IdMonedha;
            }

            var dk = vleftaDebiMonBazeTrupiFleteKontabel - vleftaKrediMonBazeTrupiFleteKontabel > 0 ? "D" : "K";

            NrLlogari = nrllog;
            IdLlogari = idLlog;
            EmerLlogari = emerllog;
            IdMonedha = idMon;
            KodMonedha = kodmon;
            Kursi = kursi;
            PershkrimTrupiFleteKontabel = pershkrimi;
            DK = dk;
            VleftaDebiMonBazeTrupiFleteKontabel = vleftaDebiMonBazeTrupiFleteKontabel;
            VleftaKrediMonBazeTrupiFleteKontabel = vleftaKrediMonBazeTrupiFleteKontabel;
            VleftaDebiTrupiFleteKontabel = vleftaDebiTrupiFleteKontabel;
            VleftaKrediTrupiFleteKontabel = vleftaKrediTrupiFleteKontabel;

            if (IdLlogari == -1) return new clsMesazh(true, "Trupi i fletes kontabel u krijua me sukses!");
            {
                var llog = new clsLlogari(IdLlogari);
                if (llog.IdObjektivaKosto != 0 && llog.IdObjektivaKosto != -1)
                    clsKokaFleteKontabel.ShtoTeDhenaPerObjektivat(dtDok, new colObjektivaKosto(), new List<double>(), new List<double>(), new List<int>(), llog, this, new clsObjektivaKosto(llog.IdObjektivaKosto));
            }

            return new clsMesazh(true, "Trupi i fletes kontabel u krijua me sukses!");
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// Metoda qe llogarit kursin
        /// </summary>
        /// <param name="kursi"></param>
        /// <param name="idMonedha"></param>
        /// <param name="azhornim"></param>
        /// <param name="monedha"></param>
        /// <param name="kurseDate"></param>
        /// <returns></returns>
        private static double LlogaritKursin(double? kursi, int? idMonedha, bool azhornim, clsMonedha monedha, colKurset kurseDate)
        {
            double myKursi;
            if (azhornim)
                myKursi = 1;
            else if (idMonedha.HasValue && monedha.IdMonedha == idMonedha.Value)
                myKursi = kursi.Value;
            else
            {
                var kurs = kurseDate.FirstOrDefault(x => x.IdMonedha == monedha.IdMonedha);
                if (kurs == null)
                    myKursi = 1;
                else
                    myKursi = kurs.VleraKursi > 0 ? kurs.VleraKursi : 1;
            }
            return myKursi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e fletes kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupFletKont">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool MbushTrupFletKont(DataRow dbDataRowTrupFletKont)
        {
            if (dbDataRowTrupFletKont == null) return false;
            try
            {
                IdTrupiFleteKontabel = int.Parse(dbDataRowTrupFletKont["IDTRUPIFLETEKONTABEL"].ToString());
                IdKokaFleteKontabel = int.Parse(dbDataRowTrupFletKont["IDKOKAFLETEKONTABEL"].ToString());
                IdLlogari = int.Parse(dbDataRowTrupFletKont["IDLLOGARI"].ToString());
                PershkrimTrupiFleteKontabel = dbDataRowTrupFletKont["PERSHKRIMITRUPIFLETEKONTABEL"].ToString();
                IdMonedha = int.Parse(dbDataRowTrupFletKont["IDMODEDHA"].ToString());
                Kursi = double.Parse(dbDataRowTrupFletKont["KURSI"].ToString());
                VleftaDebiTrupiFleteKontabel = double.Parse(dbDataRowTrupFletKont["VLEFTADEBITRUPIFLETEKONTABEL"].ToString());
                VleftaKrediTrupiFleteKontabel = double.Parse(dbDataRowTrupFletKont["VLEFTAKREDITRUPIFLETEKONTABEL"].ToString());
                NrLlogari = dbDataRowTrupFletKont["nrllogari"].ToString();
                EmerLlogari = dbDataRowTrupFletKont["EmerLlogari"].ToString();
                KodMonedha = dbDataRowTrupFletKont["monedhakod"].ToString();
                VleftaDebiMonBazeTrupiFleteKontabel = double.Parse(dbDataRowTrupFletKont["VLEFTADEBIMONBAZETRUPIFLETEKONTABEL"].ToString());
                VleftaKrediMonBazeTrupiFleteKontabel = double.Parse(dbDataRowTrupFletKont["VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL"].ToString());
                DK = double.Parse(dbDataRowTrupFletKont["VLEFTADEBIMONBAZETRUPIFLETEKONTABEL"].ToString()) - double.Parse(dbDataRowTrupFletKont["VLEFTAKREDIMONBAZETRUPIFLETEKONTABEL"].ToString()) > 0 ? "D" : "K";

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se llogarive nga db-ja");
            }
        }

        #endregion
    }
}