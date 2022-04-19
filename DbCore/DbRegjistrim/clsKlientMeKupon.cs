using DbCore.DbAdmin;
using DbCore.DbInventari;
using System;
using System.Data;
using System.Linq;
using DbCore.IMBUtils.Security;

namespace DbCore.DbRegjistrim
{
    public class clsKlientMeKupon
    {
        private string aparati;
        private DateTime dateSkadimi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idKlientKupon;
        private int idKrijuesi;
        private int idModifikuesi;

        private int idNdermarrje;
        private int idStatusDok;
        private string kodKuponi;
        private string msisdn;
        private bool statusi;
        private decimal vleraEZbritjes;

        public string Aparati
        {
            get { return aparati; }

            set { aparati = value; }
        }

        public DateTime DateSkadimi
        {
            get { return dateSkadimi; }

            set { dateSkadimi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

            set { dtModifikimi = value; }
        }

        public int IdKlientKupon
        {
            get { return idKlientKupon; }

            set { idKlientKupon = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }

            set { idKrijuesi = value; }
        }

        public int IdModifikuesi
        {
            get { return idModifikuesi; }

            set { idModifikuesi = value; }
        }

        public int IdNdermarrje
        {
            get { return idNdermarrje; }

            set { idNdermarrje = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }

            set { idStatusDok = value; }
        }

        public string KodKuponi
        {
            get { return kodKuponi; }

            set { kodKuponi = value; }
        }

        public string Msisdn
        {
            get { return GetValidMsisdn(msisdn); }

            set { msisdn = value; }
        }

        public bool Statusi
        {
            get { return statusi; }

            set { statusi = value; }
        }

        public decimal VleraEZbritjes
        {
            get { return vleraEZbritjes; }

            set { vleraEZbritjes = value; }
        }

        public clsKlientMeKupon()
        {
         idNdermarrje=   clsNdermarrje.ktheIdNdermarrjeMeme();
        }

        public clsKlientMeKupon(string kodKuponi)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
           
            mbushKlientMeKupon(db.MerrKlientMeKuponSipasNdermarrjesRootDheKodit(RijndaelSimple.EncryptDDString(kodKuponi), clsNdermarrje.ktheIdNdermarrjeMeme()));
            db.Dispose();
        }


        public clsKlientMeKupon(int idKlientKupon, string kodKuponi, string aparati, string msisdn, decimal vleraEZbritjes, bool statusi, DateTime dateSkadimi, int idNdermarrje, int idKrijuesi, int idModifikuesi, DateTime dtKrijimi, DateTime dtModifikimi, int idStatusDok)
        {
            IdKlientKupon = idKlientKupon;
            KodKuponi = kodKuponi.ToUpper();
            Aparati = aparati;
            Msisdn = msisdn;
            VleraEZbritjes = vleraEZbritjes;
            Statusi = statusi;
            DateSkadimi = dateSkadimi;
            IdKrijuesi = idKrijuesi;
            IdModifikuesi = idModifikuesi;
            DtKrijimi = dtKrijimi;
            DtModifikimi = dtModifikimi;
            IdStatusDok = idStatusDok;
            IdNdermarrje = idNdermarrje;
            clsMesazh mesazh = KontrolloKlientKupon();
            if (!mesazh)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        private bool EkzistonNjeKuponMeKeteKod()
        {
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.EkzistonNjeKlientKuponMeKeteKod(RijndaelSimple.EncryptDDString(kodKuponi), idNdermarrje);
        }

        private bool EkzistonNjeKuponMeKeteMsisdn()
        {
            if (string.IsNullOrWhiteSpace(msisdn))
                return false;
            //kontrollo vetem nese ka vlere
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
                return dbRegj.EkzistonNjeKlientKuponMeKeteMsisdn(msisdn, idNdermarrje);
        }

        private bool EkzistonNjeArtikullMeKeteKodAparati()
        {
            return clsArtikulli.ekziston(aparati, idNdermarrje);
        }

        private void mbushKlientMeKupon(DataRow dataRow)
        {
            idKlientKupon = int.Parse(dataRow["IDKLIENTKUPON"].ToString());
            msisdn = dataRow["MSISDN"].ToString();
            kodKuponi =RijndaelSimple.DecryptDDString(dataRow["KODKUPONI"].ToString());
            aparati = dataRow["APARATI"].ToString();
            decimal.TryParse(dataRow["VLERAEZBRITJES"].ToString(), out vleraEZbritjes);
            DateTime.TryParse(dataRow["DATESKADIMI"].ToString(), out dateSkadimi);

            statusi = bool.Parse(dataRow["STATUSI"].ToString());
            IdStatusDok = int.Parse(dataRow["IDSTATUSDOK"].ToString());
            idKrijuesi = int.Parse(dataRow["IDKRIJUESI"].ToString());
            int.TryParse(dataRow["IDMODIFIKUESI"].ToString(), out idModifikuesi);
            idNdermarrje = int.Parse(dataRow["IDNDERMARRJE"].ToString());
            dtKrijimi = DateTime.Parse(dataRow["DTKRIJIMI"].ToString());
            DateTime.TryParse(dataRow["DTMODIFIKIMI"].ToString(), out dtModifikimi);
        }

        /// <summary>
        /// validon objektin klientKupon
        /// </summary>
        /// <returns></returns>
        public clsMesazh KontrolloKlientKupon()
        {
            if (EkzistonNjeKuponMeKeteKod())
                return new clsMesazh(false, "Ekziston nje kupon me kodin : " + kodKuponi);
            if (EkzistonNjeKuponMeKeteMsisdn())
                return new clsMesazh(false, "Ekziston nje kupon me msisdn : " + Msisdn);
            if (!EkzistonNjeArtikullMeKeteKodAparati())
                return new clsMesazh(false, string.Format("Nuk ekziston nje artikull me kodin : {0} ", aparati));

            return new clsMesazh(true);
        }

        public clsKlientMeKupon KrijoKlientKuponPerImport(string kodKuponi, string aparati, string msisdn, decimal vleraEZbritjes, bool statusi, DateTime dateSkadimi, int idKrijuesi)
        {
            idKlientKupon = -1;
            idStatusDok = 1;
            statusi = false;//kupon i pa perdorur
            DtModifikimi = default(DateTime);
            IdModifikuesi = default(int);
            dtKrijimi = DateTime.Now;
            IdNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            if (!Integrime.KonfigurimeStatikeIntegrimi.FakeResponse)
                kodKuponi = RijndaelSimple.DecryptDDString(kodKuponi);//meqe do na e sjellin te enkriptuar
            return new clsKlientMeKupon(idKlientKupon, kodKuponi, aparati, GetValidMsisdn(msisdn), vleraEZbritjes, statusi, dateSkadimi, idNdermarrje, idKrijuesi, idModifikuesi, dtKrijimi, dtModifikimi, idStatusDok);
        }

        public static string GetValidMsisdn(string msisdn)
        {

            if (!msisdn.StartsWith("3556"))
                msisdn = "3556" + msisdn;
            return msisdn;
        }

        public clsMesazh Ruaj()
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            try
            {
                clsMesazh mesazh = new clsMesazh();
               // mesazh = KontrolloKlientKupon();
               // if (!mesazh)
                 //   return mesazh;

                db.beginTransaksion();

                mesazh = db.RuajKuponPerKlient(out idKlientKupon,string.IsNullOrEmpty(kodKuponi)?"": RijndaelSimple.EncryptDDString(kodKuponi), aparati, Msisdn, vleraEZbritjes, statusi, dateSkadimi, IdNdermarrje, idKrijuesi, dtKrijimi, idStatusDok);
                if (mesazh)
                    db.commitTransaksion();
                else
                    db.rollbackTransaksion();
                return mesazh;
            }
            catch (Exception e)
            {
                db.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se kuponit per klient per kuponin me kodin " + kodKuponi + "\n" + e.Message);
            }
        }

        /// <summary>
        /// vendos statusin perdorur per kuponin me kodin e caktuar
        /// </summary>
        /// <param name="kodKuponi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static clsMesazh UpdateStatusPerdorur(clsDatabaseRegjistrim dbRegj, string kodKuponi,string msisdn, int idKokaShitje, int idPerdoruesi)
        {
            return dbRegj.UpdateStatusPerdorur(RijndaelSimple.EncryptDDString(kodKuponi),msisdn, clsNdermarrje.ktheIdNdermarrjeMeme(), idKokaShitje, idPerdoruesi);
        }

        /// <summary>
        /// vendos statusin perdorur per kuponin me kodin e caktuar per tenuren e dt 21 mars
        /// </summary>
        /// <param name="kodKuponi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        public static clsMesazh UpdateStatusPerdorur(clsDatabaseRegjistrim dbRegj,  string msisdn, int idKokaShitje, int idPerdoruesi)
        {
            return dbRegj.UpdateStatusPerdorur(msisdn, clsNdermarrje.ktheIdNdermarrjeMeme(), idKokaShitje, idPerdoruesi);
        }
 

        /// <summary>
        /// kontrollon nese kuponi ekziston dhe eshte i pa perdorur;
        /// mesazhet qe vijne
        /// 1-kodi eshte perdorur
        /// 2-kodi nuk ekziston
        /// 3-ok
        /// 4-gabim i cmendur :P
        /// </summary>
        /// <param name="kodKuponi"></param>
        /// <returns></returns>
        public static clsMesazh ValidoKuponKlienti(string kodKuponi)
        {
            int idNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            clsMesazh is_valid = new clsMesazh(true);

            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                is_valid = dbRegj.ValidoKuponKlienti(RijndaelSimple.EncryptDDString(kodKuponi), idNdermarrje);
            }
            return is_valid;


        }
        public static clsMesazh ValidoMsidnKlienti(string msisdn,out string kodAparati,out string kodAparatiEsales )
        {
            int idNdermarrje = clsNdermarrje.ktheIdNdermarrjeMeme();
            clsMesazh is_valid;
            
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                is_valid = dbRegj.ValidoMsisdnKlientiTenure(GetValidMsisdn(msisdn), idNdermarrje,out kodAparati,out kodAparatiEsales);
            }
            return is_valid;


        }

    }
}