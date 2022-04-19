using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class clsBandaPunonjes
    {
        private const string gabimpunonjesi = "Punonjesi nuk u ruajt!";
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se bandave nga db-ja";
        #region Atribute

        private int id;
        private int idPunonjes;
        private int idGrupimGlobal;
        private int idGrupimLokal;
        private DateTime dtAktivizimi;
        private int idPerdoruesi;
        private DataRow rreshti;


        #endregion

        #region Konstruktor

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id">id e punesimit</param>
        /// <param name="idPunonjes">id e punonjesit</param>
        /// <param name="idqk1"> id e departamentit</param>
        /// <param name="idqk2">id e nendepartamentit</param>
        /// <param name="detyra">detyra</param>
        /// <param name="nrKontrate">nrkontrate</param>
        /// <param name="idgrupimglobal">tipkontrate</param>
        /// <param name="dtFillimi">data e fillimit</param>
        /// <param name="dtPerfundimi">data e perfundimit</param>
        /// <param name="llogBankare">llogaria bankare</param>
        /// <param name="idBanka">banka</param>
        /// <param name="larguar">larguar</param>
        /// <param name="dtLargimi">data e largimit</param>
        /// <param name="arsyeja"> arsyeja e largimit</param>
        /// <param name="periudhaNjoftimi"> periudha e njoftimit</param>
        /// <param name="neProve">ne prove</param>
        /// <param name="periudhaProve">periudha ne prove</param>
        public clsBandaPunonjes(string nrpersonal,  string grupimglobal, string grupimlocal, DateTime dtaktivizimi, int idperdoruesi, int idNdermarrje)
        {
            int idpunonjes = 0;
            if (!string.IsNullOrEmpty(nrpersonal))
            {
                clsPunonjes pun = new clsPunonjes(nrpersonal, idNdermarrje);
                idpunonjes = pun.IdPunonjes;
            }
            
            int idglobal;
            if (!string.IsNullOrEmpty(grupimglobal))
            {
                clsGrupimeLocaleGlobale tip = new clsGrupimeLocaleGlobale(grupimglobal, idNdermarrje);
                idglobal = tip.Id;
            }
            else idglobal = 0;
            int idlocal;
            if (!string.IsNullOrEmpty(grupimlocal))
            {
                clsGrupimeLocaleGlobale grupi = new clsGrupimeLocaleGlobale(grupimlocal, idNdermarrje);
                idlocal = grupi.Id;
            }
            else idlocal = 0;


            this.idPunonjes = idpunonjes;
           
            this.idGrupimGlobal = idglobal;
            this.idGrupimLokal = idlocal;
            this.dtAktivizimi = dtaktivizimi;
            this.idPerdoruesi = idperdoruesi;

            clsMesazh mesazh = this.kontrollo(nrpersonal,  grupimglobal, grupimlocal, idNdermarrje);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }
        private clsMesazh kontrollo(string nrpersonal,  string global, string local, int idNdermarrje)
        {

            if (dtAktivizimi.ToShortDateString() == "01/01/0001")
                return new clsMesazh(false, "Ju lutem vendosni nje date aktivizimi!");
            if (nrpersonal != "")
            {
                if (!clsPunonjes.ekzistonPunonjes(nrpersonal, idNdermarrje))
                    return new clsMesazh(false, "Punonjesi me Nr Personal " + nrpersonal + " nuk ekziston!");
            }
            
            if (global != "")
            {
                if (!clsGrupimeLocaleGlobale.ekzistonGrupimeLocaleGlobale(global, idNdermarrje))
                    return new clsMesazh(false, "Grupimi global nuk ekziston");
            }
            if (local != "")
            {
                if (!clsGrupimeLocaleGlobale.ekzistonGrupimeLocaleGlobale(local, idNdermarrje))
                    return new clsMesazh(false, "Grupimi local nuk ekziston");
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsBandaPunonjes()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="id">id punesim</param>
        public clsBandaPunonjes(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushBanda(db.ktheBandaPunonjes(id));
            db.Dispose();
        }

        public clsBandaPunonjes(DataRow rreshti)
        {
            
            mbushBanda(rreshti);
        }
        #endregion

        #region Properties
        /// <summary>
        /// data e ndryshimit te fundit
        /// </summary>
        public DateTime DtAktivizimi
        {
            get
            {
                return dtAktivizimi;
            }
            set
            {
                dtAktivizimi = value;
            }
        }
       
        /// <summary>
        /// id e perdoruesit qe beri ndryshimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e punonjesit
        /// </summary>
        public int IdPunonjes
        {
            get { return idPunonjes; }
            set { idPunonjes = value; }
        }


       

        /// <summary>
        /// kthen/vendos tipin e kontrates
        /// </summary>
        public int IdGrupimGlobal
        {
            get
            {
                return idGrupimGlobal;
            }
            set
            {
                idGrupimGlobal = value;
            }
        }

        
        /// <summary>
        /// kthen pagen me shtesen
        /// </summary>
        public int IdGrupimLokal
        {
            get
            {
                return idGrupimLokal;
            }
            set
            {
                idGrupimLokal = value;
            }
        }
        #endregion


        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            data.beginTransaksion();

            clsMesazh u_ruajt = ruaj(data);
            if (!u_ruajt.Status)
                data.rollbackTransaksion();
            else data.commitTransaksion();
            return u_ruajt;
        }
        public clsMesazh ruaj(clsDatabazeListPagesa data)
        {
              int id;
            clsMesazh u_ruajt = data.ruajBandaPunonjes(out id, idPunonjes,   idGrupimGlobal, idGrupimLokal,  dtAktivizimi, idPerdoruesi);
            this.Id = id;
            return u_ruajt;
        }
        public clsMesazh modifiko(int idbanda)
        {
            this.id = idbanda;
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            data.beginTransaksion();

            clsMesazh u_ruajt = modifiko(data);
            if (!u_ruajt.Status)
                data.rollbackTransaksion();
            else data.commitTransaksion();
            return u_ruajt;
        }
        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(   clsDatabazeListPagesa data)
        {
          
            clsMesazh u_modifikua = data.modifikoBandaPunonjes(id, idPunonjes,   idGrupimGlobal,  idGrupimLokal,  dtAktivizimi, idPerdoruesi);
            return u_modifikua;
        }
        internal clsMesazh modifikoBanda(clsDatabazeListPagesa db, colBandaPunonjes bandavjeter, int idPunonjes, int idPerdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true);
            clsBandaPunonjes band = bandavjeter.Find(x => x.DtAktivizimi.ToShortDateString() == this.DtAktivizimi.ToShortDateString());
            string fushatmod = "";
            string mesazhmod = "";
            if (band != null)//nqs ekziston qendra ne kete date e modifikojme prn e shtojme
            {
                mesazhmod = kontrollobanda(band, this, out fushatmod, db);
                this.Id = band.Id;
                this.IdPunonjes = band.IdPunonjes;
                mesazh = this.modifiko(db);
            }
            else
            {
                if (bandavjeter.Count > 0)
                    mesazhmod = kontrollobanda(bandavjeter[0], this, out fushatmod, db);
                else
                {
                    mesazhmod = "U modifikuan fushat per bandat:";
                }
                if ( this.IdGrupimGlobal != 0 || this.IdGrupimLokal != 0)
                {

                    this.IdPunonjes = idPunonjes;
                    mesazh = this.ruaj(db);
                }

            }
            if (!mesazh.Status)
                return new clsMesazh(false, "Gabim gjate ruatjes se bandave");
            if (mesazhmod != "U modifikuan fushat per bandat:")
            {
                mesazh =clsPunonjes. ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                if (!mesazh.Status)
                    return new clsMesazh(false, gabimpunonjesi);
            }
            return new clsMesazh(true, "Bandat u ruajten me sukses!");
        }
        private string kontrollobanda(clsBandaPunonjes band, clsBandaPunonjes bandavjeter, out string fushatmod, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per bandat:";
            fushatmod = "U modifikuan fushat per bandat:";
            DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db );
            if (band.IdGrupimGlobal != bandavjeter.IdGrupimGlobal)
            {
                mesazh += " Grupimi global,";
                clsGrupimeLocaleGlobale gvj = new clsGrupimeLocaleGlobale(band.IdGrupimGlobal, db);
                clsGrupimeLocaleGlobale gri = new clsGrupimeLocaleGlobale(bandavjeter.IdGrupimGlobal, db);
                fushatmod += String.Format(" Grupimi global nga {0} ne {1},", gvj.Pershkrimi, gri.Pershkrimi);
            }
            if (band.IdGrupimLokal != bandavjeter.IdGrupimLokal)
            {
                mesazh += " Grupimi lokal,";
                clsGrupimeLocaleGlobale gvj = new clsGrupimeLocaleGlobale(band.IdGrupimLokal, db);
                clsGrupimeLocaleGlobale gri = new clsGrupimeLocaleGlobale(bandavjeter.IdGrupimLokal, db);
                fushatmod += String.Format(" Grupimi lokal nga {0} ne {1},", gvj.Pershkrimi, gri.Pershkrimi);
            }
           
            if (band.DtAktivizimi != bandavjeter.DtAktivizimi)
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", band.DtAktivizimi.ToShortDateString(), bandavjeter.DtAktivizimi.ToShortDateString());
            }
            if (mesazh.Substring(mesazh.Length - 1, 1) == ",")
            {
                mesazh = mesazh.Substring(0, mesazh.Length - 1) + ".";
                fushatmod = fushatmod.Substring(0, fushatmod.Length - 1) + ".";
            }
            return mesazh;
        }
        /// <summary>
        /// merr punesim sipas id
        /// </summary>
        public void merr()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            mbushBanda(data.ktheBandaPunonjes(id));
            data.Dispose();
        }
        public static bool ekzistonBandaPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
             return   db.ekzistonBandaPerKetePunonjeMeKeteDateAktivizimi(nrpersonal, dtaktivizimi, out idpunesimi);
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush punesimin nga databaza
        /// </summary>
        /// <param name="dbDataRowPunesim">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushBanda(DataRow dbDataRowPunesim)
        {
            if (dbDataRowPunesim != null)
            {
                try
                {
                    int.TryParse(dbDataRowPunesim["ID"].ToString(), out id);
                    int.TryParse(dbDataRowPunesim["IDPUNONJES"].ToString(), out idPunonjes);
                    int.TryParse(dbDataRowPunesim["IDGRUPIMGLOBAL"].ToString(), out idGrupimGlobal);
                    int.TryParse(dbDataRowPunesim["IDGRUPIMLOKAL"].ToString(), out idGrupimLokal);
                    
                    
                    DateTime.TryParse(dbDataRowPunesim["DTAKTIVIZIMI"].ToString(), out dtAktivizimi);
                    int.TryParse(dbDataRowPunesim["IDPERDORUESI"].ToString(), out idPerdoruesi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception(gabimNeTeDhena);
                }
            }
            else
                return false;
        }

        #endregion
    }
}
