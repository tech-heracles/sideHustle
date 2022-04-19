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
    public class clsKokaMbylljeKF
    {
        #region Konstruktor

        public clsKokaMbylljeKF()
        {

        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes</param>
        public clsKokaMbylljeKF(int id)
        {
            using (var db = new clsDatabaseRegjistrim())
                MbushMbylljeFkKoka(db.ktheMbylljeKokaKFSipasId(id));
        }

        public clsKokaMbylljeKF(DataRow rreshti)
        {
            MbushMbylljeFkKoka(rreshti);
        }

        #endregion

        #region Properties

        public int IdKoka { get; set; }

        public string NrDok { get; set; }

        public DateTime DateDok { get; set; }

        public colGjendjeKlientFurnitor OColGjendjeKlientFurnitor { get; set; }

        public DateTime DateRegjistrimi { get; set; }

        public int IdLlogDebi { get; set; }

        public int IdLlogKredi { get; set; }

        public string NrLlogariKredi { get; set; }

        public string NrLlogariDebi { get; set; }

        public string Pershkrimi { get; set; }

        public int IdNdermarje { get; set; }

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

        public colTrupiMbylljeKF OColTrupi { get; set; }

        public clsKokaFleteKontabel OKokaFleteKontabel { get; set; }

        #endregion

        #region Metoda Publike

        public clsMesazh KrijoMbyllje(string nrdok, DateTime dtdok, DateTime dtregj, int idllogdeb, int idllogkred, string nrllogdeb, string nrllogkred, string pershkrim, int idnderm, int idndervit, int idperdorues, int idnivel, int idkonf, int iddoknga, int idnivelgjen, int idkonfgjen, int idgjen, int idstatus, colTrupiMbylljeKF trupi, int idperiudha, bool mekontabilizim, out string shfaqmesazhapolupe, colTrupiQendraKosto trupivjeterqendra, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            shfaqmesazhapolupe = "jo";
            NrDok = nrdok;
            DateDok = dtdok;
            DateRegjistrimi = dtregj;
            IdLlogDebi = idllogdeb;
            IdLlogKredi = idllogkred;
            NrLlogariDebi = nrllogdeb;
            NrLlogariKredi = nrllogkred;

            Pershkrimi = pershkrim;
            IdNdermarje = idnderm;
            IdNderViti = idndervit;
            IdPerdorues = idperdorues;
            IdNivel = idnivel;
            IdKonfigAmbjente = idkonf;
            IdDokNga = iddoknga;
            IdNivelGjenerues = idnivelgjen;
            IdKonfigGjenerues = idkonfgjen;
            IdGjenerues = idgjen;
            IdStatusDok = idstatus;
            OColTrupi = trupi;

            var mesazh = Kontrollo();
            if (!mesazh.Status)
                return mesazh;

            if (mekontabilizim)
            {
                var pershk = Pershkrimi != string.Empty ? Pershkrimi : "Nga mbyllja klient/furnitor";

                const int idLlojDok = 64;
                const int idkategoria = 64;

                //gjenerimi i fletes kontabel per dokumentin 
                try
                {
                    List<int> emratkf;
                    List<string> rreshtakf;
                    List<int> idllogobj;
                    List<double> vleratobjektiva;
                    List<double> vleratobjektivamonbaze;
                    colTrupatFletetKontabel trupatperGjendjekf;
                    colObjektivaKosto objektivat;
                    OKokaFleteKontabel = clsKokaFleteKontabel.gjeneroKontabilizimMbylljeKF(IdKoka, IdNivel,
                        IdKonfigAmbjente, DateDok, NrDok, IdNdermarje, IdNderViti, IdPerdorues, DateRegjistrimi, trupi,
                        pershk, IdDokNga, idLlojDok, idperiudha, idkategoria, out emratkf, out rreshtakf,
                        out trupatperGjendjekf, out objektivat, out vleratobjektiva, out vleratobjektivamonbaze,
                        out idllogobj, 0, 0, 0, out shfaqmesazhapolupe, trupivjeterqendra, idGjuha, rm, ci);
                    // gjenerimi i gjendjes kf
                    OColGjendjeKlientFurnitor = colGjendjeKlientFurnitor.KrijoGjendjetKlientFurnitor(emratkf, IdNivel,
                        NrDok, DateDok, DateRegjistrimi, trupatperGjendjekf, rreshtakf);
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
                    mesazh = ModifikoMbylljeKf(data);
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
                mesazh = data.modifikoKokaMbylljeKF(IdKoka, NrDok, DateDok, DateRegjistrimi, IdLlogDebi, IdLlogKredi, Pershkrimi, IdNdermarje, IdNderViti, IdPerdorues, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusDok);
                data.Dispose();
            }
            return mesazh;
        }
        
        public clsMesazh Ruaj(ResourceManager rm, CultureInfo ci)
        {
            clsMesazh ruaj;
            var data = new clsDatabaseRegjistrim();
            try
            {
                data.beginTransaksion();
                ruaj = RuajMbylljeKlientFurnitori(data);
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
                var kokaEkzistuese = new clsKokaMbylljeKF(IdKoka);
                kokaEkzistuese.OKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.OColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, data);

                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 64, dbkontab);
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
                return new clsMesazh(true, MessagesResource.Messages["regjMagFshirjaPerfundoiMeSukses"]);
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
                return dbKokaMagazina.kaAutorizimMbylljeKF(idkoka, idperdoruesi);
        }

        #endregion

        #region MetodaPrivate

        private clsMesazh RuajMbylljeKlientFurnitori(clsDatabaseRegjistrim dbRegj)
        {
            try
            {
                int idKoka;
                var mesazh = dbRegj.ruajKokaMbylljeKF(out idKoka, NrDok, DateDok, DateRegjistrimi, IdLlogDebi, IdLlogKredi, Pershkrimi, IdNdermarje, IdNderViti, IdPerdorues, IdNivel, IdKonfigAmbjente, IdDokNga, IdNivelGjenerues, IdKonfigGjenerues, IdGjenerues, IdStatusDok);
                if (!mesazh.Status)
                    return mesazh;
                IdKoka = idKoka;
                foreach (var o in OColTrupi)
                {
                    o.IdKoka = IdKoka;
                    mesazh = dbRegj.ruajTrupiMbylljeKF(o.IdKf, o.Pershkrimi, o.DebiKredi, o.GjendjaLlog, o.GjendjaMonBaze, o.IdLlogariKp, o.IdKoka, o.Kursi);
                    if (!mesazh.Status)
                        return mesazh;
                }

                if (OKokaFleteKontabel.VleftaFleteKontabel == 0)
                {
                    return new clsMesazh(true, MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]);
                }

                OKokaFleteKontabel.IdGjenerues = IdKoka;

                var dbKont = new clsDatabaseKontabilitet(dbRegj);
                mesazh = OKokaFleteKontabel.Ruaj(dbKont);
                if (!mesazh.Status)
                    return mesazh;

                foreach (var gj in OColGjendjeKlientFurnitor)
                {
                    gj.IdDok = IdKoka;
                    if (gj.IdKlientGjendjeKf != 0)
                    {
                        int idG;
                        mesazh = dbRegj.ruajGjendjeKF(out idG, IdKoka, gj.NrDok, gj.DateDok, gj.VlMinus, gj.VlPlus, gj.NivelDok, gj.IdMonedhaDok, gj.KursiDok, gj.DateRegj, gj.VlMinusMonedheBaze, gj.VlPlusMonedheBaze, gj.IdKlientGjendjeKf);
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
        private clsMesazh ModifikoMbylljeKf(clsDatabaseRegjistrim data)
        {
            try
            {   //obj koka qe i kalohet si parameter eshte si duhet te modifikohet
                var kokaEkzistuese = new clsKokaMbylljeKF(IdKoka);
                if (string.IsNullOrEmpty(kokaEkzistuese.NrDok) || kokaEkzistuese.IdStatusDok == 2)
                {
                    return new clsMesazh(false, MessagesResource.Messages["msgDokumentiKaNdryshuarHapeniPerseri"]);
                }

                kokaEkzistuese.IdStatusDok = 2;  //duhet vendosur nje status i pershtatshem per kete modifikim
                IdDokNga = kokaEkzistuese.IdKoka;//dokumentit te ri do i ruajme id e dokumentit nga u krijua
                kokaEkzistuese.OKokaFleteKontabel = new clsKokaFleteKontabel();
                kokaEkzistuese.OColGjendjeKlientFurnitor = new colGjendjeKlientFurnitor(kokaEkzistuese.IdKoka, kokaEkzistuese.IdNivel, data);

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
                var newclsKokaFleteKontabel = new clsKokaFleteKontabel(kokaEkzistuese.IdKoka, 64, dbkontab);
                if (newclsKokaFleteKontabel.NrDukumentiKokaFleteKontabel != null)
                {
                    kokaEkzistuese.OKokaFleteKontabel = newclsKokaFleteKontabel;
                    var kokaqendra = new clsKokaQendraKosto();
                    kokaqendra.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(newclsKokaFleteKontabel.IdKokaFleteKontabel, newclsKokaFleteKontabel.IdKonfigAmbjente, dbqendra);

                    if (kokaqendra.IdKoka != 0 && kokaqendra.IdKoka != -1)
                        kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = kokaqendra;
                    else
                        kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto = new clsKokaQendraKosto();
                }

                OKokaFleteKontabel.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.IdKokaFleteKontabel;
                OKokaFleteKontabel.KokaQendraKosto.IdDokNga = kokaEkzistuese.OKokaFleteKontabel.KokaQendraKosto.IdKoka;

                if (data.ekzistonAzhornim(NrDok, IdNivel, IdNdermarje, DateDok))
                    return new clsMesazh(false, MessagesResource.Messages["msgEkziston1RegjistrimMeKeteNumerDokumenti"]);

                mesazh = RuajMbylljeKlientFurnitori(data);
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

            if (!clsLlogari.ekzistonLlogari(NrLlogariDebi, IdNdermarje))
                return new clsMesazh(false, "Llogaria debi nuk ekziston!");

            if (!clsLlogari.ekzistonLlogari(NrLlogariKredi, IdNdermarje))
                return new clsMesazh(false, "Llogaria kredi nuk ekziston!");

            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e mbyllje te FK nga databaza
        /// </summary>
        /// <param name="dbDataRow">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void MbushMbylljeFkKoka(DataRow dbDataRow)
        {
            if (dbDataRow != null)
            {
                try
                {
                    DateDok = !IsDBNull(dbDataRow["DTDOK"])
                        ? ToDateTime(dbDataRow["DTDOK"])
                        : DateTime.MinValue;
                    DateRegjistrimi = !IsDBNull(dbDataRow["DTREGJ"])
                        ? ToDateTime(dbDataRow["DTREGJ"])
                        : DateTime.MinValue;
                    IdKoka = !IsDBNull(dbDataRow["IDKOKA"])
                        ? ToInt32(dbDataRow["IDKOKA"])
                        : 0;
                    IdLlogDebi = !IsDBNull(dbDataRow["idLLOGDEBI"])
                        ? ToInt32(dbDataRow["idLLOGDEBI"])
                        : 0;
                    IdLlogKredi = !IsDBNull(dbDataRow["idLLOGKREDI"])
                        ? ToInt32(dbDataRow["idLLOGKREDI"])
                        : 0;
                    IdNdermarje = !IsDBNull(dbDataRow["IDNDERMARJE"])
                        ? ToInt32(dbDataRow["IDNDERMARJE"])
                        : 0;
                    IdNderViti = !IsDBNull(dbDataRow["IDNDERVITI"])
                        ? ToInt32(dbDataRow["IDNDERVITI"])
                        : 0;
                    IdPerdorues = !IsDBNull(dbDataRow["IDPERDORUESI"])
                        ? ToInt32(dbDataRow["IDPERDORUESI"])
                        : 0;
                    NrDok = dbDataRow["NRDOK"].ToString();
                    NrLlogariDebi = dbDataRow["NRLLOGARIDEBI"].ToString();
                    NrLlogariKredi = dbDataRow["NRLLOGARIKredi"].ToString();
                    Pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                    IdNivel = !IsDBNull(dbDataRow["IDNIVEL"])
                        ? ToInt32(dbDataRow["IDNIVEL"])
                        : 0;
                    IdKonfigAmbjente = !IsDBNull(dbDataRow["IDKONFIGAMBJENTE"])
                        ? ToInt32(dbDataRow["IDKONFIGAMBJENTE"])
                        : 0;
                    IdDokNga = !IsDBNull(dbDataRow["IDDOKNGA"])
                        ? ToInt32(dbDataRow["IDDOKNGA"])
                        : 0;
                    IdNivelGjenerues = !IsDBNull(dbDataRow["IDNIVELGJENERUES"])
                        ? ToInt32(dbDataRow["IDNIVELGJENERUES"])
                        : 0;
                    IdKonfigGjenerues = !IsDBNull(dbDataRow["IDKONFIGGJENERUES"])
                        ? ToInt32(dbDataRow["IDKONFIGGJENERUES"])
                        : 0;
                    IdGjenerues = !IsDBNull(dbDataRow["IDGJENERUES"])
                        ? ToInt32(dbDataRow["IDGJENERUES"])
                        : 0;
                    IdStatusDok = !IsDBNull(dbDataRow["IDSTATUSDOK"])
                        ? ToInt32(dbDataRow["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dbDataRow["DTKRIJIMI"])
                        ? ToDateTime(dbDataRow["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dbDataRow["DTMODIFIKIMI"])
                        ? ToDateTime(dbDataRow["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    OColTrupi = new colTrupiMbylljeKF(IdKoka);
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se mbylljes te kokes se kf nga db-ja");
                }
            }
        }

        #endregion
    }
}
