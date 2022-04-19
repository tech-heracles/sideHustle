using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Resources;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.IMBUtils.Messages;
using static System.Convert;

namespace DbCore.DbRegjistrim
{
    public class clsAzhornimKFKoka
    {
        #region Konstruktor

        public clsAzhornimKFKoka()
        {

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes</param>
        public clsAzhornimKFKoka(int id)
        {
            using (var dbAzhornimKfKoka = new clsDatabaseRegjistrim())
                MbushAzhornimFkKoka(dbAzhornimKfKoka.ktheAzhornimKokaKFSipasId(id));
        }

        public clsAzhornimKFKoka(int id, clsDatabaseRegjistrim dbAzhornimKfKoka)
        {
            MbushAzhornimFkKoka(dbAzhornimKfKoka.ktheAzhornimKokaKFSipasId(id));
        }
        public clsAzhornimKFKoka(int id, DateTime datDok, clsDatabaseRegjistrim dbAzhornimKFKoka)
        {
            MbushAzhornimFkKoka(dbAzhornimKFKoka.ktheAzhornimKokaKFSipasIdKfDheDatDok(id, datDok));
        }
        public clsAzhornimKFKoka(DataRow rreshti)
        {
            MbushAzhornimFkKoka(rreshti);
        }

        #endregion

        #region Properties

        public int IdAzhornimKfKoka { get; set; }

        public string NrDok { get; set; }

        public DateTime DateDok { get; set; }

        public colGjendjeKlientFurnitor OColGjendjeKlientFurnitor { get; set; }

        public DateTime DateRegjistrimi { get; set; }

        public int IdLlogariDebi { get; set; }

        public int IdLlogariKredi { get; set; }

        public string NrLlogariKredi { get; set; }

        public string NrLlogariDebi { get; set; }

        public int IdKlientFurnitor { get; set; }

        public string KodKlientFurnitor { get; set; }

        public string Pershkrimi { get; set; }

        public double Kursi { get; set; }

        public int IdNdermarrje { get; set; }

        public int IdNderViti { get; set; }

        public int IdPerdorues { get; set; }

        public int IdNivel { get; set; }

        public int IdKonfigAmbjente { get; set; }

        public int IdDokNga { get; set; }

        public int IdNivelGjenerues { get; set; }

        public int IdKonfigGjenerues { get; set; }

        public int IdGjenerues { get; set; }

        public int IdStatusDok { get; set; }

        public DateTime DtKrijimi { get; private set; }

        public DateTime DtModifikimi { get; private set; }

        public colAzhornimKFTrupi OColAzhornimKfTrupi { get; set; }

        public clsKokaFleteKontabel OKokaFleteKontabel { get; set; }

        #endregion

        #region Metoda Publike

        public clsMesazh KrijoAzhornim(string nrdok, DateTime dtdok, DateTime dtregj, int idllogdeb, int idllogkred, string nrllogdeb, string nrllogkred, string pershkrim, double kurs, int idnderm, int idndervit, int idperdorues, int idnivel, int idkonf, int iddoknga, int idnivelgjen, int idkonfgjen, int idgjen, int idstatus, colAzhornimKFTrupi trupi, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupe = "jo";
            NrDok = nrdok;
            DateDok = dtdok;
            DateRegjistrimi = dtregj;
            IdLlogariDebi = idllogdeb;
            IdLlogariKredi = idllogkred;
            NrLlogariDebi = nrllogdeb;
            NrLlogariKredi = nrllogkred;
            Pershkrimi = pershkrim;
            Kursi = kurs;
            IdNdermarrje = idnderm;
            IdNderViti = idndervit;
            IdPerdorues = idperdorues;
            IdNivel = idnivel;
            IdKonfigAmbjente = idkonf;
            IdDokNga = iddoknga;
            IdNivelGjenerues = idnivelgjen;
            IdKonfigGjenerues = idkonfgjen;
            IdGjenerues = idgjen;
            IdStatusDok = idstatus;
            OColAzhornimKfTrupi = trupi;
            var mesazh = Kontrollo();
            if (!mesazh.Status)
                return mesazh;
            if (mekontabilizim)
            {
                var pershk = Pershkrimi != string.Empty ? Pershkrimi : "Nga azhornimi klient/furnitor";

                const int idLlojDok = 21;
                const int idkategoria = 11;

                //gjenerimi i fletes kontabel per dokumentin 
                try
                {
                    List<string> rreshtakf;
                    List<int> emratkf;
                    List<int> idllogobj;
                    List<double> vleratobjektiva;
                    List<double> vleratobjektivamonbaze;
                    colTrupatFletetKontabel trupatperGjendjekf;
                    colObjektivaKosto objektivat;
                    OKokaFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimAzhornimKF(IdAzhornimKfKoka, IdNivel,
                        IdKonfigAmbjente, DateDok, NrDok, IdNdermarrje, IdNderViti, IdPerdorues, DateRegjistrimi, trupi,
                        pershk, IdDokNga, idLlojDok, idperiudha, idkategoria, out emratkf, out rreshtakf,
                        out trupatperGjendjekf, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze,
                        out idllogobj, 0, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra, idGjuha, rm, ci);
                    // gjenerimi i gjendjes kf
                    OColGjendjeKlientFurnitor = colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emratkf, IdNivel, NrDok, DateDok, DateRegjistrimi, trupatperGjendjekf, rreshtakf);
                }
                catch (Exception ex)
                {
                    return new clsMesazh(false, ex.Message);
                }
            }
            else
            {
                OKokaFleteKontabel = new clsKokaFleteKontabel();
                OColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor();
            }

            return new clsMesazh(true, "Dokumenti u krijua me sukses!");
        }

        public void MerrAzhornimSipasKlientitMeIFundit(int idkf)
        {
            using (var data = new clsDatabaseRegjistrim())
                MbushAzhornimFkKoka(data.ktheAzhornimKokaKFSipasKFmeteFundit(idkf));
        }

        /// <summary>
        /// shkruan  ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh Modifiko(bool lidhur)
        {
            clsMesazh mesazh;
            var data = new clsDatabaseRegjistrim();
            if (!lidhur)
            {
                try
                {
                    data.beginTransaksion();
                    mesazh = ModifikoAzhornimKf(data);
                    if (mesazh.Status)
                        data.commitTransaksion();
                    else
                        data.rollbackTransaksion();
                }
                catch (Exception)
                {
                    data.rollbackTransaksion();
                    throw;
                }
            }
            else
            {
                mesazh = data.modifikoKokaAzhornimKF(IdAzhornimKfKoka, NrDok, DateDok, DateRegjistrimi, IdLlogariDebi, IdLlogariKredi, Pershkrimi, Kursi, IdNdermarrje, IdNderViti, IdPerdorues, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusDok);
                data.Dispose();
            }

            return mesazh;
        }

        public clsMesazh Ruaj()
        {
            clsMesazh ruaj;
            var data = new clsDatabaseRegjistrim();
            try
            {
                data.beginTransaksion();
                ruaj = RuajAzhornimKlientFurnitori(data);
                if (ruaj.Status)
                    data.commitTransaksion();
                else
                    data.rollbackTransaksion();
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                throw;
            }

            return ruaj;
        }

        /// <summary>
        /// fshin nje objekt dokument veprimekf sebashku me te trupin, gjendjenkf dhe kontabilitetin perkates
        /// Nje objekt dokument veprimekf ka nje koleksion me trupin, gjendjenkf dhe kontabilitetin , 
        /// fshirja e nje dokumenti veprimekf imponon fshirjen edhe te nje colection-i me trupin, gjendjenkf dhe kontabilitetin
        /// Mqs cdo rresht i ri qe fshihet ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i veprimitkf bashke me trupin, gjendjenkf dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon fshirjen e rregullt te nje te nje dokumenti sebashku me trupin,gjendjen kf dhe kontabilitetin e tij
        ///1. merr kontabilitetet eksistuese te dokumentit te shitjes dhe magazines dhe i stornon
        ///3. ben fshirjen e trupit dhe me pas te kokes se dokumentit te shitjes
        /// </summary>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e fshirjes se te dhenave ne DB</returns>
        public clsMesazh Fshi()
        {
            var data = new clsDatabaseRegjistrim();
            try
            {
                data.beginTransaksion();
                var dbkontab = new clsDatabaseKontabilitet(data);

                var kokaEkzistuese = new clsAzhornimKFKoka(IdAzhornimKfKoka);
                kokaEkzistuese.OKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.OColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdAzhornimKfKoka, kokaEkzistuese.IdNivel, data);

                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdAzhornimKfKoka, 11, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OKokaFleteKontabel = newclsKokaFleteKontabel;
                }

                clsMesazh mesazh;
                foreach (var gj in kokaEkzistuese.OColGjendjeKlientFurnitor)
                {
                    if (gj.IdGjendjeKf != 0)
                    {
                        gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                        mesazh = gj.Modifiko(data);
                        if (!mesazh.Status)
                        {
                            data.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                }

                if (kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.OKokaFleteKontabel.fshiupd(dbkontab);
                    if (!mesazh.Status)
                        return mesazh;
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                mesazh = kokaEkzistuese.Modifiko(true);

                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }

                data.commitTransaksion();

                return new clsMesazh(true, MessagesResource.Messages["msgFshirjaPerfundoiMeSukses"]);
            }
            catch (Exception ce)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        public static bool KaAutorizime(int idkoka, int idperdoruesi)
        {
            using (var dbKokaMagazina = new clsDatabaseRegjistrim())
                return dbKokaMagazina.kaAutorizimAzhornimKF(idkoka, idperdoruesi);
        }

        #endregion

        #region Metoda Private

        private clsMesazh Kontrollo()
        {
            if (NrDok == "")
                return new clsMesazh(false, "Numri i dokumentit nuk mund te jete bosh");

            if (DateDok == null || DateDok.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e dokumentit!");

            if (DateRegjistrimi == null || DateRegjistrimi.ToShortDateString() == "01/01/0100")
                return new clsMesazh(false, "Zgjidhni daten e regjistrimit!");

            if (NrLlogariDebi == "")
                return new clsMesazh(false, "Jepni llogarine e debise!");

            if (NrLlogariKredi == "")
                return new clsMesazh(false, "Jepni llogarine e kredise!");
            if (!clsLlogari.ekzistonLlogari(NrLlogariDebi, IdNdermarrje))
                return new clsMesazh(false, "Llogaria debi nuk ekziston!");

            if (!clsLlogari.ekzistonLlogari(NrLlogariKredi, IdNdermarrje))
                return new clsMesazh(false, "Llogaria kredi nuk ekziston!");

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }
        
        /// <summary>
        /// Modifikon nje objekt dokument veprimekf sebashku me te trupin, gjendjakf dhe kontabilitetin
        /// Nje objekt dokument veprimekf ka nje koleksion me trupin, gjendjakf dhe kontabilitetin perkates , 
        /// modifikimi e nje dokumenti imponon modifikimin edhe te nje colection-i me trupin,gjendjakf dhe kontabilitetin
        /// Mqs cdo rresht i ri qe modifikon ne DB kerkon thirrjen e nje SP-je me parametra dhe dokumenti i veprimekf bashke me trupin, gjendjenkf dhe kontabilitetin konsiderohet si nje regjistrim,
        /// perdoret nje transaksion qe imponon modifikimin e rregullt te nje te nje dokumenti veprimekf sebashku me trupin, gjendjenkf dhe kontabilitetin
        /// 1. merret dokumenti eksistues i veprimekf  kalohen ne gjendjen 20 te modifikimit
        /// 2. ruhet dokumenti i ri i veprimekf se bashku me trupin,gjendjen kf dhe kontabilitetin
        /// 3. stornohen kontabiliteti i dokumentave eksistues 
        /// </summary>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        private clsMesazh ModifikoAzhornimKf(clsDatabaseRegjistrim data)
        {
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                var kokaEkzistuese = new clsAzhornimKFKoka(IdAzhornimKfKoka);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                IdDokNga = kokaEkzistuese.IdAzhornimKfKoka;//dokumentit te ri do i ruajme id e dokumentit nga u krijua
                kokaEkzistuese.OKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.OColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdAzhornimKfKoka, kokaEkzistuese.IdNivel, data);

                var dbqendra = new clsDatabaseQendraKosto(data);
                var mesazh = kokaEkzistuese.Modifiko(true);
                if (!mesazh.Status)
                    return mesazh;

                foreach (var gj in kokaEkzistuese.OColGjendjeKlientFurnitor)
                {
                    if (gj.IdGjendjeKf != 0)
                    {
                        gj.IdStatusGjendjeKf = 2; //rasti kur ndrysheohet statusi per treguar qe dokumenti eshte i modifikuar dhe nuk duhet marre parasysh
                        mesazh = gj.Modifiko(data);
                    }
                    if (!mesazh.Status)
                        return mesazh;
                }

                var dbkontab = new clsDatabaseKontabilitet(data);
                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdAzhornimKfKoka, 11, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OKokaFleteKontabel = newclsKokaFleteKontabel;
                    var kokaqendra = new clsKokaQendraKosto();
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);
                    if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                    {
                        kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = kokaqendra;
                    }
                    else
                        kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = new clsKokaQendraKosto();
                }

                OKokaFleteKontabel.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel;
                OKokaFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto.IdKoka;

                if (data.ekzistonAzhornim(NrDok, IdNivel, IdNdermarrje, DateDok))
                    return new clsMesazh(false, MessagesResource.Messages["msgEkziston1RegjistrimMeKeteNumerDokumenti"]);

                mesazh = RuajAzhornimKlientFurnitori(data);
                if (!mesazh.Status)
                    return mesazh;

                if (kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel != 0)
                {
                    mesazh = kokaEkzistuese.OKokaFleteKontabel.ModifikoFleteKontabel(true, dbkontab);
                    if (!mesazh.Status)
                        return mesazh;
                }
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        private clsMesazh RuajAzhornimKlientFurnitori(clsDatabaseRegjistrim dbRegj)
        {
            try
            {
                int idAzhornimKfKoka;
                var mesazh = dbRegj.ruajKokaAzhornimKF(out idAzhornimKfKoka, NrDok, DateDok, DateRegjistrimi, IdLlogariDebi, IdLlogariKredi, Pershkrimi, Kursi, IdNdermarrje, IdNderViti, IdPerdorues, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusDok);
                if (!mesazh.Status)
                    return mesazh;

                IdAzhornimKfKoka = idAzhornimKfKoka;

                foreach (var o in OColAzhornimKfTrupi)
                {
                    o.IdAzhornimKfKoka = IdAzhornimKfKoka;
                    mesazh = dbRegj.ruajTrupiAzhornimKF(o.IdKlientFurnitor, o.Pershkrimi, o.DebiKredi, o.Vlefta, o.IdLlogariKp, o.IdAzhornimKfKoka, o.Kursi);
                    if (!mesazh.Status)
                        return mesazh;
                }

                if (OKokaFleteKontabel.VleftaFleteKontabel == 0)
                {
                    return new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                }

                OKokaFleteKontabel.IdGjenerues = IdAzhornimKfKoka;

                var dbKont = new clsDatabaseKontabilitet(dbRegj);
                mesazh = OKokaFleteKontabel.Ruaj(dbKont);
                if (!mesazh.Status)
                    return mesazh;

                foreach (var gj in OColGjendjeKlientFurnitor)
                {
                    gj.IdDok = IdAzhornimKfKoka;
                    if (gj.IdKlientGjendjeKf != 0)
                    {
                        int idG;
                        mesazh = dbRegj.ruajGjendjeKF(out idG, IdAzhornimKfKoka, gj.NrDok, gj.DateDok, gj.VlMinus, gj.VlPlus, gj.NivelDok, gj.IdMonedhaDok, gj.KursiDok, gj.DateRegj, gj.VlMinusMonedheBaze, gj.VlPlusMonedheBaze, gj.IdKlientGjendjeKf);
                    }
                    if (!mesazh.Status)
                        return mesazh;
                }

                mesazh = new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                return new clsMesazh(false, ce.Message);
            }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e azhornim te FK nga databaza
        /// </summary>
        /// <param name="dbDataRowAzhornimFkKoka">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushAzhornimFkKoka(DataRow dbDataRowAzhornimFkKoka)
        {
            if (dbDataRowAzhornimFkKoka != null)
            {
                try
                {
                    DateDok = !IsDBNull(dbDataRowAzhornimFkKoka["DTDOK"])
                        ? ToDateTime(dbDataRowAzhornimFkKoka["DTDOK"])
                        : DateTime.MinValue;
                    DateRegjistrimi = !IsDBNull(dbDataRowAzhornimFkKoka["DTREGJ"])
                        ? ToDateTime(dbDataRowAzhornimFkKoka["DTREGJ"])
                        : DateTime.MinValue;
                    IdAzhornimKfKoka = !IsDBNull(dbDataRowAzhornimFkKoka["IDAZHORNIMKFKOKA"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDAZHORNIMKFKOKA"])
                        : 0;
                    IdLlogariDebi = !IsDBNull(dbDataRowAzhornimFkKoka["LLOGDEBI"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["LLOGDEBI"])
                        : 0;
                    IdLlogariKredi = !IsDBNull(dbDataRowAzhornimFkKoka["LLOGKREDI"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["LLOGKREDI"])
                        : 0;
                    IdNdermarrje = !IsDBNull(dbDataRowAzhornimFkKoka["IDNDERMARRJE"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDNDERMARRJE"])
                        : 0;
                    IdNderViti = !IsDBNull(dbDataRowAzhornimFkKoka["IDNDERVITI"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDNDERVITI"])
                        : 0;
                    IdPerdorues = !IsDBNull(dbDataRowAzhornimFkKoka["IDPERDORUESI"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDPERDORUESI"])
                        : 0;
                    NrDok = dbDataRowAzhornimFkKoka["NRDOK"].ToString();
                    NrLlogariDebi = dbDataRowAzhornimFkKoka["NRLLOGARIDEBI"].ToString();
                    NrLlogariKredi = dbDataRowAzhornimFkKoka["NRLLOGARIKredi"].ToString();
                    Pershkrimi = dbDataRowAzhornimFkKoka["PERSHKRIMI"].ToString();
                    IdNivel = !IsDBNull(dbDataRowAzhornimFkKoka["IDNIVEL"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDNIVEL"])
                        : 0;
                    IdKonfigAmbjente = !IsDBNull(dbDataRowAzhornimFkKoka["IDKONFIGAMBJENTE"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDKONFIGAMBJENTE"])
                        : 0;
                    IdDokNga = !IsDBNull(dbDataRowAzhornimFkKoka["IDDOKNGA"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDDOKNGA"])
                        : 0;
                    IdNivelGjenerues = !IsDBNull(dbDataRowAzhornimFkKoka["IDNIVELGJENERUES"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDNIVELGJENERUES"])
                        : 0;
                    IdKonfigGjenerues = !IsDBNull(dbDataRowAzhornimFkKoka["IDKONFIGGJENERUES"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDKONFIGGJENERUES"])
                        : 0;
                    IdGjenerues = !IsDBNull(dbDataRowAzhornimFkKoka["IDGJENERUES"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDGJENERUES"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRowAzhornimFkKoka["IDSTATUSDOK"])
                        ? ToInt32(dbDataRowAzhornimFkKoka["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dbDataRowAzhornimFkKoka["DTKRIJIMI"])
                        ? ToDateTime(dbDataRowAzhornimFkKoka["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRowAzhornimFkKoka["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRowAzhornimFkKoka["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    Kursi = !IsDBNull(dbDataRowAzhornimFkKoka["KURSI"])
                        ? ToDouble(dbDataRowAzhornimFkKoka["KURSI"])
                        : 0;
                    OColAzhornimKfTrupi = new colAzhornimKFTrupi(IdAzhornimKfKoka);
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se azhornimit te kokes se FK nga db-ja");
                }
            }
        }

        #endregion
    }
}
