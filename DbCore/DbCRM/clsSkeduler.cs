using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbCRM
{
    public class clsSkeduler
    {
        #region Atribute

        private int idAuto;
        private int idPerdoruesi;
        private int idKlienti;
        private DateTime data;
        private int idKrijuesi;
        private DateTime dtKrijimi;
        private int idModifikimi;
        private DateTime dtModifikimi;
        private int idStatusDok;
        private DateTime startDate;
        private DateTime endTime;
        private bool allDay;
        private string description;
     
        private int idNdermarje;
        private bool celurNeWeb;
        private int lloji;
        private string koordinateFillimi;
        private string koordinateMbarimi;
        private int status;
      
        #endregion

        #region Konstruktoret

        public clsSkeduler()
        {
        }

        public clsSkeduler(int idNdermarrje,int idPerdoruesi, int idKlienti, DateTime data, int idKrijuesi, DateTime dtKrijimi, int idModifikimi, DateTime dtModifikimi, int idStatusDok,  DateTime startDate, DateTime endTime, bool allDay, string description,  int idNdermarje,int lloji,string koordFillimi,string koordMbarimi,int status)
        {
            this.idPerdoruesi = idPerdoruesi;
            this.idKlienti = idKlienti;
            this.data = data;
            this.idKrijuesi = idKrijuesi;
            this.dtKrijimi = dtKrijimi;
            this.idModifikimi = idModifikimi;
            this.dtModifikimi = dtModifikimi;
            this.idStatusDok = idStatusDok;
            this.startDate = startDate;
            this.endTime = endTime;
            this.allDay = allDay;
            this.description = description;
           
            this.idNdermarje = idNdermarje;
            this.celurNeWeb = true;

            this.lloji = lloji;
            this.koordinateFillimi = koordFillimi;
            this.koordinateMbarimi = koordMbarimi;
            this.status = status;

        }

        public clsSkeduler(int idTakimi)
        {
            
            //this.idTakimi = idTakimi;
            mbushTakim(idTakimi);
        }

        #endregion

        #region Properties

        public bool CelurNeWeb
        {
            get
            {
                return celurNeWeb;
            }
            set
            {
                celurNeWeb = value;
            }
        }
        public int IdAuto
        {
            get { return idAuto; }
            set { idAuto = value; }
        }

        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        public int IdKlienti
        {
            get { return idKlienti; }
            set { idKlienti = value; }
        }

        public DateTime Data
        {
            get { return data; }
            set { data = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public int IdModifikimi
        {
            get { return idModifikimi; }
            set { idModifikimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }



        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public bool AllDay
        {
            get { return allDay; }
            set { allDay = value; }
        }




        public string Description
        {
            get { return description; }
            set { description = value; }
        }


     

        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
        public int Lloji
        {
            get { return lloji; }
            set { lloji = value; }
        }
        public string KoordinateFillimi
        {
            get { return koordinateFillimi; }
            set { koordinateFillimi = value; }
        }
        public string KoordinateMbarimi
        {
            get { return koordinateMbarimi; }
            set { koordinateMbarimi = value; }
        }
        public int Status
        {
            get { return status; }
            set { status = value; }
        }
        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            data.beginTransaksion();
            if (!string.IsNullOrWhiteSpace(Description))
            Description = string.Format("{0}:{1}", DbCore.DbAdmin.clsPerdorues.ktheEmer(idKrijuesi), Description);
            clsMesazh u_ruajt = ruaj(data);
          
            if (u_ruajt.Status) data.commitTransaksion(); else data.rollbackTransaksion();
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseCRM data)
        {
            if (data.ekzistonDetyreNeKeteDatePerAgjent(this.idAuto, this.idPerdoruesi, this.idKlienti, this.data.Date, this.IdNdermarje))
                return new clsMesazh(false, "Detyra/Klienti i zgjedhur ekziston per kete agjent ne kete date!");

            int idAuto;
            clsMesazh u_ruajt = data.ruajTakim(out idAuto, this.idPerdoruesi, this.idKlienti, this.data, this.idKrijuesi, this.dtKrijimi, this.idModifikimi, this.dtModifikimi, this.idStatusDok, this.startDate.Date, this.endTime.Date, this.allDay, this.description, this.idNdermarje, this.lloji, this.koordinateFillimi, this.koordinateMbarimi, this.status);
            this.idAuto = idAuto;
            return u_ruajt;
        }
    

        public clsMesazh modifiko()
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            data.beginTransaksion();
            clsMesazh u_modifikua = modifiko(data);
            if (u_modifikua.Status)
                data.commitTransaksion();
            else
                data.rollbackTransaksion();
            return u_modifikua;
        }

        public clsMesazh modifiko(clsDatabaseCRM data)
        {
          
                if (data.ekzistonDetyreNeKeteDatePerAgjent(this.idAuto,this.idPerdoruesi, this.idKlienti, this.data.Date,this.IdNdermarje))
                    return new clsMesazh(false, "Detyra/Klienti i zgjedhur ekziston per kete agjent ne kete date!");
             clsSkeduler oldSkeduler = new clsSkeduler(this.idAuto);
             lloji = oldSkeduler.lloji;
            clsMesazh mesazh = fshi(data, this.idAuto, this.idPerdoruesi);
            FormatoMesazhin(oldSkeduler);
            if (mesazh.Status)
                mesazh = ruaj(data);

            return mesazh;
        }
        private void FormatoMesazhin(clsSkeduler oldSkeduler)
        {
            if (string.IsNullOrWhiteSpace(description) && string.IsNullOrWhiteSpace(oldSkeduler.description))
                return;

            if (this.description != oldSkeduler.description)
            {
                string emri = "";





                if (string.IsNullOrWhiteSpace(oldSkeduler.Description))
                {
                    emri = string.Format("\n{0}:", DbCore.DbAdmin.clsPerdorues.ktheEmer(this.idModifikimi));
                    this.description = string.Format("{0}{1}", emri, description);
                }
                else
                {
                    if ((oldSkeduler.idModifikimi != 0 && oldSkeduler.idModifikimi == idModifikimi) || (oldSkeduler.idModifikimi == 0 && oldSkeduler.idKrijuesi == idModifikimi))
                        emri = "";
                    this.description = string.Format("{0}{1}{2}", oldSkeduler.description, emri, this.description.Replace(oldSkeduler.Description, string.Empty));
                }
            }
        }
        public static clsMesazh fshi(int idAuto, int idPerdorues)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiTakim(idAuto, idPerdorues);
            data.Dispose();
            return u_fshi;
        }
        public clsMesazh  fshi(clsDatabaseCRM data,int idAuto,int idPerdorues)
        {
            clsMesazh u_fshi = data.fshiTakim(idAuto, idPerdorues);
            return u_fshi;
        }

        public bool mbushTakim(int idAuto)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool sukses = mbushTakim(data.merrTakimSipasId(idAuto));
            data.Dispose();
            return sukses;
        }

        public DataRow ktheTakim(int idAuto)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrTakimSipasId(idAuto);
            data.Dispose();
            return dr;
        }


        #endregion
        #region Metoda Internal

        internal bool mbushTakim(DataRow dbDataRowSkeduler)
        {
            if (dbDataRowSkeduler != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkeduler["IDAUTO"].ToString(), out idAuto);
                    int.TryParse(dbDataRowSkeduler["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowSkeduler["IDKLIENTI"].ToString(), out idKlienti);
                    DateTime.TryParse(dbDataRowSkeduler["DATA"].ToString(), out data);
                    int.TryParse(dbDataRowSkeduler["IDKRIJUESI"].ToString(), out idKrijuesi);
                    DateTime.TryParse(dbDataRowSkeduler["DTKRIJIMI"].ToString(), out dtKrijimi);
                    int.TryParse(dbDataRowSkeduler["IDMODIFIKIMI"].ToString(), out idModifikimi);
                    DateTime.TryParse(dbDataRowSkeduler["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowSkeduler["IDSTATUSDOK"].ToString(), out idStatusDok);
      
                    DateTime.TryParse(dbDataRowSkeduler["STARTDATE"].ToString(), out startDate);
                    DateTime.TryParse(dbDataRowSkeduler["ENDTIME"].ToString(), out endTime);
                    bool.TryParse(dbDataRowSkeduler["ALLDAY"].ToString(), out allDay);
                    bool.TryParse(dbDataRowSkeduler["CELURNEWEB"].ToString(), out celurNeWeb);
                    description = dbDataRowSkeduler["DESCRIPTION"].ToString();
                    int.TryParse(dbDataRowSkeduler["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowSkeduler["LLOJI"].ToString(), out lloji);
                   koordinateFillimi= dbDataRowSkeduler["KOORDINATEFILLIMI"].ToString();
                   koordinateMbarimi=dbDataRowSkeduler["KOORDINATEMBARIMI"].ToString();
                   int.TryParse(dbDataRowSkeduler["STATUS"].ToString(), out status);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te skedulerit nga db-ja");
                }
            }
            else
                return false;
        }
        
        #endregion

       
    }
}
