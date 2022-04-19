using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace DbCore.DbListPagesat
{
   public class clsLegjendaListOrareve
    {  
       #region Atributet

        private int id;
        private string oreFillimi;
        private string oreMbarimi;
        private string pershkrimi;
        private decimal koeficienti; 
        private int idKrijuesi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
      
        private string kodi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
       private int idKomponente;
       private DataRow rreshti;


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
       /// id e kompoentes te list pagese
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
        /// Kthen/Vendos oren e fillimit 
        /// </summary>
        public string OreFillimi
        {
            get { return oreFillimi; }
            set { oreFillimi = value; }
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
        /// kthen vendos ditet  e javes per kete konfigurim
        /// </summary>
        public string Kodi
        {
            get
            {
                return kodi;
            }
            set
            {
                kodi = value;
            }
        }
        /// <summary>
        /// kthen vendos oren e mbarimit
        /// </summary>
        public string OreMbarimi
        {
            get
            {
                return oreMbarimi;
            }
            set
            {
                oreMbarimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne pershkrimin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos koeficienti
        /// </summary>
        public decimal Koeficienti
        {
            get { return koeficienti; }
            set { koeficienti = value; }
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


        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsLegjendaListOrareve()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="id"> id ritese e kokes</param>
        /// <param name="orefillimi">orefillimi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="koeficient"> koeficineti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsLegjendaListOrareve(int id,string kodi, string orefillimi, string orembarimi, String pershkrimi, decimal koeficient, int idkomponente,int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.id = id;
            this.kodi=kodi;
            this.idKomponente=idkomponente;
            this.oreFillimi = orefillimi;
            this.oreMbarimi = orembarimi;
            this.pershkrimi = pershkrimi;
            this.koeficienti = koeficient;
            this.idKrijuesi = idkrijuesi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;

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
        public clsLegjendaListOrareve(string kodi,string orefillimi, string orembarimi, String pershkrimi, decimal koeficient, int idkomponente,int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.kodi=kodi;
            this.idKomponente=idkomponente;
            this.oreFillimi = orefillimi;
            this.oreMbarimi = orembarimi;
            this.pershkrimi = pershkrimi;
            this.koeficienti = koeficient;
            this.idKrijuesi = idkrijuesi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;

        }

        /// <summary>
        /// konstruktor me parameter id 
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsLegjendaListOrareve(int id)
        {
            clsDatabazeListPagesa dbKont = new  clsDatabazeListPagesa ();
            mbushLegjendeListOrari(dbKont.merrLegjendaListOrari(id));
            dbKont.Dispose();
        }

        /// <summary>
        /// konstruktor me 3 parametra (kodin  dhe id e ndermarrjes dhe grupin)
        /// </summary>
        /// <param name="kodi">kod</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="grupi">grupi</param>
        public clsLegjendaListOrareve(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa dbKodifikimArtikujsh = new clsDatabazeListPagesa();
            mbushLegjendeListOrari(dbKodifikimArtikujsh.merrLegjendaListOrariSipasKod(kodi, idndermarje));
            dbKodifikimArtikujsh.Dispose();
        } public clsLegjendaListOrareve(string kodi, int idndermarje, clsDatabazeListPagesa dbKodifikimArtikujsh)
        {
          
            mbushLegjendeListOrari(dbKodifikimArtikujsh.merrLegjendaListOrariSipasKod(kodi, idndermarje));
            
        }

        public clsLegjendaListOrareve(DataRow rreshti)
        {
            
            mbushLegjendeListOrari(rreshti);
        }


        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin grupin dokumenti ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="ruajGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            int id;
            data.beginTransaksion();

            clsMesazh u_ruajt = data.ruajLegjendeListOrari(out id,this.kodi, this.OreFillimi,this.oreMbarimi, this.Pershkrimi, this.Koeficienti, this.idKomponente,this.IdPerdoruesi,this.idKrijuesi, this.IdNdermarje, this.idStatusDok);
            if (!u_ruajt.Status)
            {
                data.rollbackTransaksion();

                return u_ruajt;
            }
           
            data.commitTransaksion();

            return u_ruajt;
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

            clsMesazh u_modifikua = data.modifikoLegjendeListOrari(this.Id,this.kodi, this.OreFillimi,this.oreMbarimi, this.Pershkrimi, this.Koeficienti,this.idKomponente, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
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
            clsMesazh u_fshi = data.fshiLegjendeListOrari(this.Id, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }
           
            data.commitTransaksion();
            return u_fshi;
        }



        public static bool ekzistonLegjenda(string kodi, int idndermarje)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool sukses = data.ekzistonLegjendeListOrari(kodi, idndermarje);
            data.Dispose();
            return sukses;
        }
        public static bool kaVeprimeSimboli(int id)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool sukses = data.kaVeprimeSimboli(id);
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
        internal bool mbushLegjendeListOrari(DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupe["ID"].ToString(), out id);
                 oreFillimi=  dbDataRowGrupe["OREFILLIMI"].ToString().Substring(0,5);
                 oreMbarimi = dbDataRowGrupe["OREMBARIMI"].ToString().Substring(0, 5);
                       kodi = dbDataRowGrupe["KODI"].ToString();
                    pershkrimi = dbDataRowGrupe["PERSHKRIMI"].ToString();
        int.TryParse(dbDataRowGrupe["IDKOMPONENTELISTPAGESE"].ToString(), out idKomponente);
                    decimal.TryParse(dbDataRowGrupe["KOEFICIENTI"].ToString(), out koeficienti);
                    int.TryParse(dbDataRowGrupe["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowGrupe["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrupe["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupe["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupe["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupe["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se legjendes te list orarit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
