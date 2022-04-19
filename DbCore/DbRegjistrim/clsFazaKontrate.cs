
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbKontabiliteti;
using System.Data;
using System.Globalization;
using DbCore.DbAdmin;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kartat e klientit
    ///  (Te dhenat  merren nga tabela : T_Karta)
    /// </summary>
    public class clsFazaKontrate
    {
        #region Atribute

        private int idFaza;
        private int idKontrata;
        private string pershkrimi;
        private int vlera;
        private DateTime data;
        private int dite;
     
        private int idStatusDok;
        private int idKrijuesi;
        private int idNdermarrje;    
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;       
      
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdFaza
        {
            get { return idFaza; }
            set { idFaza = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin e kartes.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos email.
        /// </summary>
        public int IdKontrata
        {
            get { return idKontrata; }
            set { idKontrata = value; }
        }

        /// <summary>
        /// Kthen/Vendos emrin e personit.
        /// </summary>
        public int Vlera
        {
            get { return vlera; }
            set { vlera = value; }
        }

        /// <summary>
        /// Kthen/Vendos kontaktin e personit.
        /// </summary>
        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public int Dite
        {
            get { return dite; }
            set { dite = value; }
        }

        /// <summary>
        /// Kthen/Vendos idStatusDok
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        
        /// <summary>
        /// Kthen/Vendos ID-ne e krijuesit.
        /// </summary>
        public int IdKrijues
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes.
        /// </summary>
        public int IdNdermarrje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

      

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
            set
            {
                dtKrijimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
            set
            {
                dtModifikimi = value;
            }
        }
        

        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsFazaKontrate()
        { }

        /// <summary>
        /// konstrukotr me 1 parameter
        /// </summary>
        /// <param name="id">id e kartes</param>
        public clsFazaKontrate(int idFaza)
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            mbushFaza(data.merrFazaSipasId(idFaza));
            data.Dispose();
        }


        public clsFazaKontrate(int idFaza,int idKontrata,  string pershkrimi, int vlera,DateTime datefaturimi,int ditedif, int idStatusDok, int idKrijues, int idNdermarrje,   bool shtim )
        {
            try
            {
                this.idFaza = idFaza;
                this.idKontrata = idKontrata;
                this.pershkrimi = pershkrimi;
                this.vlera = vlera;
                this.data = datefaturimi;
                this.dite = ditedif;
                this.idKrijuesi = idKrijues;
                this.idNdermarrje = idNdermarrje;
            


                clsMesazh mesazh = this.kontrolloFaza(shtim);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public clsFazaKontrate(DataRow rreshti)
        {
            
            mbushFaza(rreshti);
        }

        #endregion

        #region Metoda Publike

        private clsMesazh kontrolloFaza(bool shtim)
        {
            //if (kodi == "")  return new clsMesazh(false, "Plotesoni kodin e kartes!");
            //clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
         

            //if (shtim && db.ekzistonKarta(kodi, idNdermarrje))
            //{
            //    db.Dispose();
            //    return new clsMesazh(false, "Ekziston nje karte me kete kod!");
            //}


            return new clsMesazh(true, "Kontrollet e artikullit u kaluan me sukses");
        }




        
        public bool mbushFaze(int idFaze)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            bool sukses = mbushFaza(db.merrFazaSipasId(idFaze));
            db.Dispose();
            return sukses;
        }


        /// <summary>
        /// kthe nje datarow me karten e marre nga db sipas id.
        /// </summary>
        /// <param name="idAutomjet"></param>
        /// <returns>datarow</returns>
        public static DataRow ktheFazeSipasId(int idFaze)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataRow dr = db.merrFazaSipasId(idFaze);
            return dr;
        }

        /// <summary>
        /// kthe nje datatable me karten e marre nga db sipas id.
        /// </summary>
        /// <param name="idKarta"></param>
        /// <returns>datatable</returns>
        public static DataTable ktheFazeSipasIdDt(int idFaze)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            DataTable dt = db.merrFazaSipasIdDt(idFaze);
            return dt;
        }


        public static bool EkzistonFaza(int idFaza, int idNdermarrje, int idKontrata)
        {
            clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
        return    db.EkzistonFazaPerKontraten(idFaza, idNdermarrje, idKontrata);
        }

        public clsMesazh ruaj(clsDatabaseRegjistrim db)
        {
          //  clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            clsMesazh mesazh = new clsMesazh();
            //idFaze = 0;
         
            try
            {
             //   db.beginTransaksion();

                mesazh = db.ruajFaza(out this.idFaza, this.idKontrata, this.pershkrimi, this.vlera, this.data, this.dite, this.idStatusDok, this.idNdermarrje, this.idKrijuesi);
                //this.idFaza = idFaze;
                if (!mesazh.Status)
                {
                 //   db.rollbackTransaksion();
                    return mesazh;
                }

               
              //  db.commitTransaksion();
                return mesazh;
            }
            catch
            {
               // db.rollbackTransaksion();
                return mesazh;
            }
        }

        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim db  = new clsDatabaseRegjistrim();
            clsMesazh mesazh = new clsMesazh();
 
           
            try
            {
                db.beginTransaksion();
                mesazh = db.modifikoFaza( this.idFaza, this.idKontrata, this.pershkrimi, this.vlera, this.data, this.dite, this.idStatusDok, this.idNdermarrje, this.idKrijuesi);
                if (!mesazh.Status)
                {
                    db.rollbackTransaksion();
                    return mesazh;
                }

           
                db.commitTransaksion();
                return mesazh;
            }
            catch
            {
                db.rollbackTransaksion();
                return mesazh;
            }
        }


        public static clsMesazh fshi(clsDatabaseRegjistrim db, int idKontrata,int idPerdoruesi)
        {
           // clsDatabaseRegjistrim db = new clsDatabaseRegjistrim();
            clsMesazh mesazh = new clsMesazh();
            //idFaze = 0;

            try
            {
              //  db.beginTransaksion();

                mesazh = db.fshiFazatMeStatusDok (idKontrata, idPerdoruesi);
                //this.idFaza = idFaze;
                if (!mesazh.Status)
                {
                   // db.rollbackTransaksion();
                    return mesazh;
                }


               // db.commitTransaksion();
                return mesazh;
            }
            catch
            {
              //  db.rollbackTransaksion();
                return mesazh;
            }
        }
       
        #endregion

        #region Metoda Internal

        /// <summary>
        /// Metode per mbushjen e automjetit nga databaza. Thirret nga metoda mbushAutomjetet e colAutomjete.cs
        /// </summary>
        /// <param name="dbDataRowAutomjet">Si parameter merr nje DataRow.</param>
        /// <returns>Kthen true nese mbushja ndodh me sukses. Ne te kundert false.</returns>
        /// <param name="dbinventari"></param>
        internal bool mbushFaza(DataRow dbDataRowKarte)
        {
            if (dbDataRowKarte == null)
                return false;
            try
            {
                int.TryParse(dbDataRowKarte["IDFAZA"].ToString(), out idFaza);
                int.TryParse(dbDataRowKarte["IDKONTRATA"].ToString(), out idKontrata);
                pershkrimi = dbDataRowKarte["PERSHKRIMI"].ToString();
                int.TryParse(dbDataRowKarte["VLERA"].ToString(), out vlera);
                DateTime.TryParse(dbDataRowKarte["DATA"].ToString(), out data);
                int.TryParse(dbDataRowKarte["DITE"].ToString(), out dite);               
                int.TryParse(dbDataRowKarte["IDSTATUSDOK"].ToString(), out idStatusDok);
                int.TryParse(dbDataRowKarte["IDNDERMARRJE"].ToString(), out idNdermarrje);
                int.TryParse(dbDataRowKarte["IdKrijuesi"].ToString(), out idKrijuesi);    
                DateTime.TryParse(dbDataRowKarte["DTKRIJIMI"].ToString(), out dtKrijimi);
                DateTime.TryParse(dbDataRowKarte["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                return true;
            }
            catch (InvalidCastException)
            {
                throw new Exception("ERROR: Gabim gjate marrjes se automjetit nga db-ja");
            }
        }

        #endregion


    }
}
