using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
   public  class clsDiteLeje
    {
        #region Atributet

        private int id;
        private DateTime dateFillimi;
        private DateTime dateMbarimi;
        
        private int idKrijuesi;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;

        private int idPunonjesi;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private decimal nrDitesh;
        private System.Data.DataRow rreshti;
      


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
        /// id e simbolit te list pagese
        /// </summary>
        public decimal NrDitesh
        {
            get
            {
                return nrDitesh;
            }
            set
            {
                nrDitesh = value;
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
        public DateTime DateFillimi
        {
            get { return dateFillimi; }
            set { dateFillimi = value; }
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

       
        public DateTime DateMbarimi
        {
            get
            {
                return dateMbarimi;
            }
            set
            {
                dateMbarimi = value;
            }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsDiteLeje()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="id"> id ritese e kokes</param>
        /// <param name="dtfillimi">orefillimi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="koeficient"> koeficineti</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsDiteLeje(int id, int idpunonjesi, DateTime dtfillimi, DateTime dtmbarimi,  decimal nrDitesh, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.id = id;
            this.idPunonjesi = idpunonjesi;
            this.nrDitesh = nrDitesh;
            this.dateFillimi = dtfillimi;
            this.dateMbarimi = dtmbarimi;
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
        public clsDiteLeje(int idpunonjesi, DateTime dtfillimi, DateTime dtmbarimi, decimal nrDitesh, int idkrijuesi, int idPerdoruesi, int idNdermarje, int idStatusDok)
        {
            this.idPunonjesi = idpunonjesi;
            this.nrDitesh = nrDitesh;
            this.dateFillimi = dtfillimi;
            this.dateMbarimi = dtmbarimi;
            this.idKrijuesi = idkrijuesi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            

        }

        /// <summary>
        /// konstruktor me parameter id 
        /// </summary>
        /// <param name="id">id e grupit</param>
        public clsDiteLeje(int id)
        {
            clsDatabazeListPagesa dbKont = new clsDatabazeListPagesa();
            mbushDiteLeje(dbKont.merrDiteLeje(id));
            dbKont.Dispose();
        }

        public clsDiteLeje(System.Data.DataRow rreshti)
        {
            
            mbushDiteLeje(rreshti);
        }




        #endregion

        #region Metoda Publike
        public clsDiteLeje krijoPerImport(string kodi, string emer, string mbiemer, DateTime dtfillimi, DateTime dtmbarimi,  decimal nrditesh, int idkrijuesi, int idperdoruesi, int idndermarje, int idstatusdok, int vitinderm)
        {
            try
            {
                clsPunonjes pun = new clsPunonjes(kodi, idndermarje);
                idPunonjesi = pun.IdPunonjes;
                if (pun.IdPunonjes <= 0)
                    throw new Exception("Punonjesi nuk ekziston!");
                if (emer != "" && pun.Emer != emer)
                    throw new Exception("Emri i punonjesit nuk eshte i sakte!");
                if (mbiemer != "" && pun.Mbiemer != mbiemer)
                    throw new Exception("Mbiemri i punonjesit nuk eshte i sakte!");
                if ( (dtmbarimi -dtfillimi).TotalDays+1 != double.Parse(nrditesh.ToString()))
                    throw new Exception("Diferenca e diteve nuk eshte e njejte me nr e diteve");
                if (dtfillimi.Year != vitinderm)
                    throw new Exception("Viti i listpageses duhet ti perkase vitit ushtrimor!");

                if (dtfillimi.Month != dtmbarimi.Month)
                    throw new Exception("Data e fillimit dhe e mbarimit te lejes nuk i perkasin te njejtes periudhe");
                string dataEkzistuese = clsDiteLeje.KaPrerjeDatashDitetELejes(dtfillimi, dtmbarimi, pun.IdPunonjes);
                if (!string.IsNullOrEmpty(dataEkzistuese))
                    throw new Exception($"Per punonjesin {pun.NrPersonal} jane importuar dite leje per datat {dataEkzistuese}!");
                
                return new clsDiteLeje(0, idPunonjesi, dtfillimi, dtmbarimi,  nrditesh, idkrijuesi, idperdoruesi, idndermarje, idstatusdok);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        public static string KaPrerjeDatashDitetELejes(DateTime dtFillimi, DateTime dtMbarimi, int idPunonjes)
        {
            using (var db = new clsDatabazeListPagesa())
                return db.kaPrerjeDatashDitetELejes(dtFillimi, dtMbarimi, idPunonjes);
        }
        
        /// <summary>
        /// Ruan objektin grupin dokumenti ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="ruajGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            using (var scope = new MyTransactionScope())
            {
                clsDatabazeListPagesa data = new clsDatabazeListPagesa();
                int id;
                clsMesazh u_ruajt = data.ruajDiteLeje(out id, this.idPunonjesi, this.DateFillimi, this.dateMbarimi, this.nrDitesh, this.IdPerdoruesi, this.idKrijuesi, this.IdNdermarje, this.idStatusDok);
                if (u_ruajt.Status)
                {
                    scope.Complete();
                }
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

            clsMesazh u_modifikua = data.modifikoDiteLeje(this.Id, this.idPunonjesi, this.DateFillimi, this.dateMbarimi, this.nrDitesh, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
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
            clsMesazh u_fshi = data.fshiDiteLeje(this.Id, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }

            data.commitTransaksion();
            return u_fshi;
        }



        //public static bool ekzistonOreShtese(DateTime date, int idpunonjesi)
        //{
        //    clsDatabazeListPagesa data = new clsDatabazeListPagesa();
        //    bool sukses = data.ekzistonOreShtese(date, idpunonjesi);
        //    data.Dispose();
        //    return sukses;
        //}
        //public static bool ekzistonOreShtese(DateTime date, int idpunonjesi, clsDatabazeListPagesa data)
        //{

        //    bool sukses = data.ekzistonOreShtese(date, idpunonjesi);

        //    return sukses;
        //}
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush grupimet nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupe">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDiteLeje(System.Data.DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupe["ID"].ToString(), out id);
                    DateTime.TryParse(dbDataRowGrupe["DATEFILLIMI"].ToString(), out dateFillimi);
                    DateTime.TryParse(dbDataRowGrupe["DATEMBARIMI"].ToString(), out dateMbarimi);
         
                    int.TryParse(dbDataRowGrupe["IDPUNONJESI"].ToString(), out  idPunonjesi);
                    decimal.TryParse(dbDataRowGrupe["NRDITESH"].ToString(), out nrDitesh);
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
                    throw new Exception("ERROR: Gabim gjate marrjes se dite leje nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
