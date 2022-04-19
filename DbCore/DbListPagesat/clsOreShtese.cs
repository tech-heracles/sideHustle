using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;

namespace DbCore.DbListPagesat
{
    /// <summary>
    /// kjo klase perdoret per importin e te gjitha komponenteve te listpageses te tipit formule, ku importohen te dhenat e parametrit si psh oret jashte orarit, call up, call out ect
    /// </summary>
    public class clsOreShtese
    {
        #region Atributet

        private int id;
        private DateTime data;
        private DateTime ngaOra;
        private DateTime neOra;
        private int idKrijuesi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;

        private int idPunonjesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private decimal totali;
        private int idKomponente;
        private string kodiKomponentes;
        private int muaji;
        private int viti;
        private string muajiLP;
        private int vitiLP;
        private DataRow rreshti;
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
        public decimal Totali
        {
            get
            {
                return totali;
            }
            set
            {
                totali = value;
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
        /// Kthen/Vendos daten
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
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

        public DateTime NeOra
        {
            get
            {
                return neOra;
            }
            set
            {
                neOra = value;
            }
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
        public DateTime NgaOra
        {
            get
            {
                return ngaOra;
            }
            set
            {
                ngaOra = value;
            }
        }

        public string KodiKomponentes
        {
            get
            {
                return kodiKomponentes;
            }

            set
            {
                kodiKomponentes = value;
            }
        }

        public string NrPersonalPunonjesi => nrPersonalPunonjesi;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsOreShtese()
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
        public clsOreShtese(int id, int idpunonjesi, DateTime data, DateTime ngadata, DateTime nedata, decimal totali, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {
            this.id = id;
            this.idPunonjesi = idpunonjesi;
            this.totali = totali;
            this.data = data;
            this.ngaOra = ngadata;
            this.neOra = nedata;
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
        /// konstruktor me parametra pa id 
        /// </summary>
        /// <param name="orefillimi">orefillimi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="koeficient"> koeficineti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsOreShtese(int idpunonjesi, DateTime data, DateTime ngadata, DateTime nedata, decimal totali, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok, int idkomponente, int muaji, int viti, string muajilp, int vitilp)
        {
            this.idPunonjesi = idpunonjesi;
            this.totali = totali;
            this.data = data;
            this.ngaOra = ngadata;
            this.neOra = nedata;
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
        public clsOreShtese(int id)
        {
            clsDatabazeListPagesa dbKont = new clsDatabazeListPagesa();
            mbushOreShtese(dbKont.merrOreShtese(id));
            dbKont.Dispose();
        }

        public clsOreShtese(DataRow rreshti)
        {
            
            mbushOreShtese(rreshti);
        }




        #endregion

        #region Metoda Publike
        public clsOreShtese krijoPerImport(string kodi, string emer, string mbiemer, DateTime data, DateTime ngaora, DateTime neore, decimal totali, int idkrijuesi, int idperdoruesi, int idndermarje, int idstatusdok, string komponente, string muaji, int viti, string muajilp, int vitilp, int vitinderm, bool kontrolloEkzistence)
        {

            clsPunonjes pun = new clsPunonjes(kodi, idndermarje);
            idPunonjesi = pun.IdPunonjes;
            if (pun.IdPunonjes <= 0)
                throw new MyException("Punonjesi nuk ekziston!");
            if (emer != "" && pun.Emer != emer)
                throw new MyException("Emri i punonjesit nuk eshte i sakte!");
            if (mbiemer != "" && pun.Mbiemer != mbiemer)
                throw new MyException("Mbiemri i punonjesit nuk eshte i sakte!");
            if ((neore - ngaora).TotalHours != 0 && (neore - ngaora).TotalHours != double.Parse(totali.ToString()))
                throw new MyException("Diferenca e oreve nuk eshte e njejte me totalin");
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
                throw new Exception("Viti i listpageses nuk eshte i sakte");
            if (viti > 9999 || viti < 0)
                throw new Exception("Viti nuk eshte i sakte");
            else if (viti == 0)
                viti = vitilp;
            clsKomponentePage komp = new clsKomponentePage(komponente, idndermarje, new DateTime(vitilp, mlp, 1));
            if (komp.IdKomponentePage <= 0)
                throw new MyException("Komponentja nuk ekziston!");

            if (!clsKomponenteListPagesePunonjesi.MerrEDukshmePerKomponente(komp.Kodi, idPunonjesi, new DateTime(vitilp, mlp, 1)))
            {
                throw new MyException($"Komponentja {komp.Kodi} nuk mund te importohet per punonjesin {pun.NrPersonal} pasi ajo nuk eshte e dukshme ");
            }

            if (kontrolloEkzistence && ekzistonOreShtese(komp.IdKomponentePage, idPunonjesi, muaji, viti, muajilp, vitilp))
            {
                throw new MyException($"Ekziston nje rekord per punonjesin {pun.NrPersonal} per komponenten {komp.Kodi} per muajin {muaji} dhe muaj listpagese {muajilp}", new Dictionary<string, object> { { "EkzistonRekordi", true } });
            }
               var per = new DbAdmin.clsPeriudhaKontabel(new DateTime(vitilp, int.Parse(muajilp), 1), idndermarje);
                if (per.Ekycur)
                    throw new MyException("Periudha e listpageses " + muajilp + " eshte e kycur");
                if (!clsKokaListPagese.ekzistonListePagesaPerPunonjes(pun.NrPersonal, idndermarje, vitilp, mlp))
                    throw new MyException(IMBUtils.Messages.MessagesResource.Messages["msgNukKaLpPerPunonjesin"]
                        .Replace("#xxx", pun.NrPersonal)
                        .Replace("#mmm", clsFunksione.merrMuaj(mlp))
                        .Replace("#vvv", vitilp.ToString())
                        );
                return new clsOreShtese(0, idPunonjesi, data, ngaora, neore, totali, idkrijuesi, idperdoruesi, idndermarje, idstatusdok, komp.IdKomponentePage,int.Parse(muaji), viti, muajilp, vitilp)
  {
                KodiKomponentes = komp.Kodi,
                nrPersonalPunonjesi = kodi
            };
        }
        /// <summary>
        /// Ruan objektin grupin dokumenti ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="ruajGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            using (var data = new clsDatabazeListPagesa())
            {
                int id;
                clsMesazh u_ruajt = data.ruajOreShtese(out id, this.idPunonjesi, this.Data, this.ngaOra, this.neOra, this.totali, this.IdPerdoruesi, this.idKrijuesi, this.IdNdermarje, this.idStatusDok, this.idKomponente, this.muaji, this.viti, this.muajiLP, this.vitiLP);
                if (!u_ruajt) return u_ruajt;

                u_ruajt = data.ruajLogPunonjes(out id, idPunonjesi, IdKrijuesi, 1, $"import komponente formule per punonjesin me numer personal {NrPersonalPunonjesi}", $"Komponente :{KodiKomponentes}, Vlera :{this.Totali}, Muaji :{this.Muaji},Viti :{this.Viti},Muajip :{MuajiLP},VitiLp :{VitiLP}");
                if (!u_ruajt) return u_ruajt;
                    
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

            clsMesazh u_modifikua = data.modifikoOreShtese(this.Id, this.idPunonjesi, this.Data, this.ngaOra, this.neOra, this.totali, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.idKomponente, this.muaji, this.viti, this.muajiLP, this.vitiLP);
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
            clsMesazh u_fshi = data.fshiOreShtese(this.Id, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }

            data.commitTransaksion();
            return u_fshi;
        }



        public static bool ekzistonOreShtese(int idkomponente, int idpunonjesi, string muaji, int viti, string muajilp, int vitilp)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool sukses = data.ekzistonOreShtese(idkomponente, idpunonjesi, muaji, viti, muajilp, vitilp);
            data.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush grupimet nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupe">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushOreShtese(DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupe["ID"].ToString(), out id);
                    DateTime.TryParse(dbDataRowGrupe["DATA"].ToString(), out data);
                    DateTime.TryParse(dbDataRowGrupe["NGAORA"].ToString(), out ngaOra);
                    DateTime.TryParse(dbDataRowGrupe["NEORA"].ToString(), out neOra);
                    int.TryParse(dbDataRowGrupe["IDPUNONJESI"].ToString(), out idPunonjesi);
                    decimal.TryParse(dbDataRowGrupe["TOTALI"].ToString(), out totali);
                    int.TryParse(dbDataRowGrupe["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowGrupe["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrupe["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupe["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupe["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupe["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowGrupe["IDKOMPONENTE"].ToString(), out idKomponente);
                    int.TryParse(dbDataRowGrupe["MUAJI"].ToString(), out muaji);
                    int.TryParse(dbDataRowGrupe["VITI"].ToString(), out viti);
                    muajiLP = dbDataRowGrupe["MUAJILP"].ToString();
                    int.TryParse(dbDataRowGrupe["VITILP"].ToString(), out vitiLP);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se oreve shtese nga db-ja");
                }
            }
            else
                return false;
        }

        internal void mbushOreShteseBasic(IDataRecord dbDataRowGrupe)
        {
            int.TryParse(dbDataRowGrupe["IDPUNONJESI"].ToString(), out idPunonjesi);
            decimal.TryParse(dbDataRowGrupe["TOTALI"].ToString(), out totali);
            int.TryParse(dbDataRowGrupe["MUAJI"].ToString(), out muaji);
            int.TryParse(dbDataRowGrupe["VITI"].ToString(), out viti);
            kodiKomponentes = dbDataRowGrupe["kodi"].ToString();
        }

        public static clsOreShtese KrijoBasic(IDataRecord record)
        {
            var oreShtese = new clsOreShtese();
            oreShtese.mbushOreShteseBasic(record);
            return oreShtese;
        }

        #endregion
    }
}
