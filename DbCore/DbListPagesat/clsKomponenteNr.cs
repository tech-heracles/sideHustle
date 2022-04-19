using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbListPagesat
{
    public class clsKomponenteNr
    {

        #region Atributet

        private int id;

        private int idKrijuesi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;

        private int idPunonjesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private decimal vlera;
        private int idKomponente;
        private int muaji;
        private int viti;
        private string muajiLP;
        private int vitiLP;
        private string _kodiKomponentes;
        private string nrPersonalPunonjesi;


        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        /// <summary>
        /// id e komponentes
        /// </summary>
        public int IdKomponente
        {
            get
            {
                return idKomponente;
            }
            set
            {
                idKomponente = value;
            }
        }
        /// <summary>
        /// muaji kur ka ndodhur
        /// </summary>
        public int Muaji
        {
            get
            {
                return muaji;
            }
            set
            {
                muaji = value;
            }
        }
        /// <summary>
        /// muaji kur do paguhet
        /// </summary>
        public string MuajiLP
        {
            get
            {
                return muajiLP;
            }
            set
            {
                muajiLP = value;
            }
        }
        /// <summary>
        /// id e simbolit te list pagese
        /// </summary>
        public decimal Vlera
        {
            get
            {
                return vlera;
            }
            set
            {
                vlera = value;
            }
        }
        /// <summary>
        /// kthen vendos id e krijuesit
        /// </summary>
        public int IdKrijuesi
        {
            get
            {
                return idKrijuesi;
            }
            set
            {
                idKrijuesi = value;
            }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// kthen vendos idpunonjesit
        /// </summary>
        public int IdPunonjesi
        {
            get
            {
                return idPunonjesi;
            }
            set
            {
                idPunonjesi = value;
            }
        }


        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin .
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }


        /// <summary>
        /// viti kur ka ndodhur
        /// </summary>
        public int Viti
        {
            get
            {
                return viti;
            }
            set
            {
                viti = value;
            }
        }

        /// <summary>
        /// viti kur do paguhet
        /// </summary>
        public int VitiLP
        {
            get
            {
                return vitiLP;
            }
            set
            {
                vitiLP = value;
            }
        }

        public string KodiKomponentes
        {
            get { return _kodiKomponentes; }
            set { _kodiKomponentes = value; }
        }

        public string NrPersonalPunonjesi => nrPersonalPunonjesi;


        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsKomponenteNr()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="id"> id ritese e kokes</param>
        /// <param name="data">orefillimi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="koeficient"> koeficineti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsKomponenteNr(int id, int idpunonjesi, decimal totali, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {
            this.id = id;
            this.idPunonjesi = idpunonjesi;
            this.vlera = totali;

            this.idKrijuesi = idkrijuesi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.idKomponente = idkomponente;
            this.muaji = muaji;
            this.viti = viti;
            this.muajiLP = muajilp;
            this.vitiLP = vitilp;

        }
        /// <summary>
        /// konstruktor me parameter id 
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsKomponenteNr(int id)
        {
            using (clsDatabazeListPagesa dbKont = new clsDatabazeListPagesa())
            {
                dbKont.merrKomponenteNr(id, this);
            }
        }

        public clsKomponenteNr(DataRow rreshti)
        {

            // mbushKomponenteNr(rreshti);
        }




        #endregion

        #region Metoda Publike
        public clsKomponenteNr krijoPerImport(string kodi, string emer, string mbiemer, decimal totali, int idperdoruesi, int idndermarje, int idstatusdok, string komponente, string muaji, int viti, string muajilp, int vitilp, int vitinderm, bool kontrolloEkzistence)
        {

            clsPunonjes pun = new clsPunonjes(kodi, idndermarje);
            idPunonjesi = pun.IdPunonjes;
            if (pun.IdPunonjes <= 0)
                throw new MyException("Punonjesi nuk ekziston!");
            if (emer != "" && pun.Emer != emer)
                throw new MyException("Emri i punonjesit nuk eshte i sakte!");
            if (mbiemer != "" && pun.Mbiemer != mbiemer)
                throw new MyException("Mbiemri i punonjesit nuk eshte i sakte!");
            if (vitilp != vitinderm)
                throw new MyException("Viti i listpageses duhet ti perkase vitit ushtrimor!");
            int mlp = 0;
            if (!int.TryParse(muajilp, out mlp))
                throw new MyException("Muaji i listpageses duhet te jete nr!");
            if (mlp < 1 || mlp > 12)
                throw new MyException("Muaji i listpageses duhet te jete nr midis 01 dhe 12");
            muajilp = mlp.ToString();
            if (muaji != "")
            {
                int m = 0;
                if (!int.TryParse(muaji, out m))
                    throw new MyException("Muaji duhet te jete nr!");
                if (m < 1 || m > 12)
                    throw new MyException("Muaji duhet te jete nr midis 01 dhe 12");

                muaji = m.ToString();
            }
            else
            {
                muaji = muajilp;

            }
            if (vitilp > 9999 || vitilp < 0)
                throw new MyException("Viti i listpageses nuk eshte i sakte");
            if (viti > 9999 || viti < 0)
                throw new MyException("Viti nuk eshte i sakte");
            else if (viti == 0)
                viti = vitilp;
            clsKomponentePage komp = new clsKomponentePage(komponente, idndermarje, new DateTime(vitilp, mlp, 1));
            if (komp.IdKomponentePage <= 0)
                throw new MyException("Komponentja nuk ekziston!");

            if (!clsKomponenteListPagesePunonjesi.MerrEDukshmePerKomponente(komp.Kodi, idPunonjesi, new DateTime(vitilp, mlp, 1)))
            {
                throw new MyException($"Komponentja {komp.Kodi} nuk mund te importohet per punonjesin {pun.NrPersonal} pasi ajo nuk eshte e dukshme ");
            }

            if (kontrolloEkzistence && ekzistonKomponenteNr(komp.IdKomponentePage, idPunonjesi, muaji, viti, muajilp, vitilp))
                throw new MyException($"Ekziston nje rekord per punonjesin {pun.NrPersonal} per komponenten {komp.Kodi} per muajin {muaji} dhe muaj listpagese {muajilp}", new Dictionary<string, object> { { "EkzistonRekordi", true } });

            var per = new DbAdmin.clsPeriudhaKontabel(new DateTime(vitilp, int.Parse(muajilp), 1), idndermarje);
            if (per.Ekycur)
                throw new MyException("Periudha e listpageses " + muajilp + " eshte e kycur");
            if (!clsKokaListPagese.ekzistonListePagesaPerPunonjes(pun.NrPersonal, idndermarje, vitilp, mlp))
                throw new Exception(IMBUtils.Messages.MessagesResource.Messages["msgNukKaLpPerPunonjesin"]
                    .Replace("#xxx", pun.NrPersonal)
                    .Replace("#mmm", clsFunksione.merrMuaj(mlp))
                    .Replace("#vvv", vitilp.ToString())
                    );
            return new clsKomponenteNr(0, idPunonjesi, totali, idperdoruesi, idperdoruesi, idndermarje, idstatusdok, komp.IdKomponentePage, int.Parse(muaji), viti, muajilp, vitilp)
            {
                _kodiKomponentes = komp.Kodi,
                nrPersonalPunonjesi = kodi
            };


        }

        /// <summary>
        /// Ruan objektin grupin dokumenti ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="ruajGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        
        public clsMesazh Ruaj()
        {
            using (var data = new clsDatabazeListPagesa())
            {
                int id;
                clsMesazh u_ruajt = data.ruajKomponenteNr(out id, this.idPunonjesi, this.vlera, this.IdPerdoruesi, this.idKrijuesi, this.IdNdermarje, this.idStatusDok, this.idKomponente, this.muaji, this.viti, this.muajiLP, this.vitiLP);
                if (!u_ruajt) return u_ruajt;
                u_ruajt = data.ruajLogPunonjes(out id, idPunonjesi, IdKrijuesi, 1, $"import komponente numer per punonjesin me numer personal {NrPersonalPunonjesi}", $"Komponente :{KodiKomponentes}, Vlera :{this.Vlera}, Muaji :{this.Muaji},Viti :{this.Viti},Muajip :{MuajiLP},VitiLp :{VitiLP}");
                return u_ruajt;
            }
        }

        /// <summary>
        /// Modifikon objektin grup ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="modifikoGrup"/> 
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>

        public clsMesazh modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            data.beginTransaksion();

            clsMesazh u_modifikua = data.modifikoKomponenteNr(this.Id, this.idPunonjesi, this.vlera, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.idKomponente, this.muaji, this.viti, this.muajiLP, this.vitiLP);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }

            data.commitTransaksion();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin grup ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="fshiGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>

        public clsMesazh fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            data.beginTransaksion();
            clsMesazh u_fshi = data.fshiKomponenteNr(this.Id, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }

            data.commitTransaksion();
            return u_fshi;
        }



        public static bool ekzistonKomponenteNr(int idkomponente, int idpunonjesi, string muaji, int viti, string muajilp, int vitilp)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool sukses = data.ekzistonKomponenteNr(idkomponente, idpunonjesi, muaji, viti, muajilp, vitilp);
            data.Dispose();
            return sukses;
        }
        //public static bool ekzistonOreShtese(DateTime date, int idpunonjesi, clsDatabazeListPagesa data)
        //{

        //    bool sukses = data.ekzistonOreShtese(date, idpunonjesi);

        //    return sukses;
        //}

        public static clsKomponenteNr Krijo(IDataRecord record)
        {
            clsKomponenteNr kompNr = new clsKomponenteNr();
            kompNr.mbushKomponenteNr(record);
            return kompNr;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush grupimet nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupe">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void mbushKomponenteNr(IDataRecord record)
        {

            try
            {
                Converter.ParseExact(record["ID"].ToString(), out id, "id");
                Converter.Parse(record["IDKRIJUESI"].ToString(), out idKrijuesi, "idkrijuesi");
                Converter.Parse(record["IDPERDORUESI"].ToString(), out idPerdoruesi, "idperdoruesi");
                Converter.Parse(record["IDNDERMARJE"].ToString(), out idNdermarje, "idNdermarrje");
                Converter.Parse(record["IDSTATUSDOK"].ToString(), out idStatusDok, "idStatusDok");
                Converter.Parse(record["DTKRIJIMI"].ToString(), out dtKrijimi, "dtKrijimi");
                Converter.Parse(record["DTMODIFIKIMI"].ToString(), out dtModifikimi, "dtModifikimi");
                Converter.Parse(record["IDKOMPONENTE"].ToString(), out idKomponente, "idKomponente");
                muajiLP = record["MUAJILP"].ToString();
                Converter.Parse(record["VITILP"].ToString(), out vitiLP, "vitiLP");
                Converter.ParseExact(record["IDPUNONJESI"].ToString(), out idPunonjesi, "idPunonjesi");
                Converter.ParseExact(record["Vlera"].ToString(), out vlera, "vlera");
                int.TryParse(record["MUAJI"].ToString(), out muaji);
                int.TryParse(record["VITI"].ToString(), out viti);

            }
            catch (MyWarnException warn)
            {
                ImbLogger.Warn($"gabim ne mbushjen e komponenteve te Nr per:{id} {warn.ToString()}");
            }
         

        }


        internal void mbushKomponenteNrBasic(IDataRecord record)
        {
            Converter.ParseExact(record["IDPUNONJESI"].ToString(), out idPunonjesi, "idPunonjesi");
            Converter.ParseExact(record["Vlera"].ToString(), out vlera, "vlera");
            int.TryParse(record["MUAJI"].ToString(), out muaji);
            int.TryParse(record["VITI"].ToString(), out viti);
            _kodiKomponentes = record["Kodi"].ToString();
        }

        public static clsKomponenteNr KrijoBasic(IDataRecord record)
        {
            var komp = new clsKomponenteNr();
            komp.mbushKomponenteNrBasic(record);
            return komp;
        }
        #endregion
    }
}
