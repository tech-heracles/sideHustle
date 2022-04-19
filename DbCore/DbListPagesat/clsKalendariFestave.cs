using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
   public class clsKalendariFestave
    {
         #region Atributet

        private int id;
        private DateTime data;
      
        private string pershkrimi;
        private decimal koeficienti; 
        private int idKrijuesi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
      
      
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;



        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
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
        public clsKalendariFestave()
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
        public clsKalendariFestave(int id, DateTime data,  String pershkrimi, decimal koeficient,int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.id = id;
            this.data = data;
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
        /// <param name="data">orefillimi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="koeficient"> koeficineti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsKalendariFestave(DateTime data,  String pershkrimi, decimal koeficient, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.data = data;
 
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
        public clsKalendariFestave(int id)
        {
            clsDatabazeListPagesa dbKont = new  clsDatabazeListPagesa ();
            mbushKalendarFestash(dbKont.merrKalendarFestash(id));
            dbKont.Dispose();
        }

        public clsKalendariFestave(DataRow rreshti)
        {
            
            mbushKalendarFestash(rreshti);
        }

        ///// <summary>
        ///// konstruktor me 3 parametra (kodin  dhe id e ndermarrjes dhe grupin)
        ///// </summary>
        ///// <param name="kodi">kod</param>
        ///// <param name="idndermarje">id e ndermarrjes</param>
        ///// <param name="grupi">grupi</param>
        //public clsKalendariFestave(string kodi, int idndermarje, int grupi)
        //{
        //    clsDatabazeListPagesa dbKodifikimArtikujsh = new clsDatabazeListPagesa();
        //    mbushGrup(dbKodifikimArtikujsh.merrGrupimSipasKod(kodi, idndermarje, grupi));
        //    dbKodifikimArtikujsh.Dispose();
        //}


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

            clsMesazh u_ruajt = data.ruajKalendarFestash(out id, this.Data, this.pershkrimi, this.Koeficienti, this.IdPerdoruesi,this.idKrijuesi, this.IdNdermarje, this.idStatusDok);
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

            clsMesazh u_modifikua = data.modifikoKalendarFestash(this.Id, this.Data, this.pershkrimi, this.Koeficienti, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
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
            clsMesazh u_fshi = data.fshiKalendarFestash(this.Id, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }
           
            data.commitTransaksion();
            return u_fshi;
        }



        public static bool ekzistonFeste(DateTime date, int idndermarje)
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            bool sukses = data.ekzistonKalendarFestash(date, idndermarje);
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
        internal bool mbushKalendarFestash(DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupe["ID"].ToString(), out id);
                  DateTime.TryParse( dbDataRowGrupe["DATA"].ToString(), out data);
                    pershkrimi = dbDataRowGrupe["PERSHKRIMI"].ToString();
            
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
                    throw new Exception("ERROR: Gabim gjate marrjes se kalendarit te festave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
