
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class clsQendraKostoPunonjes
    {
        private const string gabimpunonjesi = "Punonjesi nuk u ruajt!";
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se qendrave nga db-ja";
        #region Atribute

        private int id;
        private int idPunonjes;
        private int idQendraKosto1;
        private int idQendraKosto2;
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
        public clsQendraKostoPunonjes(string nrpersonal, string qk1, string qk2, DateTime dtaktivizimi, int idperdoruesi, int idNdermarrje)
        {
            int idpunonjes = 0;
            if (!string.IsNullOrEmpty(nrpersonal))
            {
                clsPunonjes pun = new clsPunonjes(nrpersonal, idNdermarrje);
                idpunonjes = pun.IdPunonjes;
            }
            int idqk1;
            if (!string.IsNullOrEmpty(qk1))
            {
                DbCore.DbQendraKosto.clsQendraKosto dep = new DbCore.DbQendraKosto.clsQendraKosto(qk1, idNdermarrje);

                idqk1 = dep.Id;
            }
            else idqk1 = 0;
            int idqk2;
            if (!string.IsNullOrEmpty(qk2))
            {
                DbCore.DbQendraKosto.clsQendraKosto dep = new DbCore.DbQendraKosto.clsQendraKosto(qk2, idNdermarrje);

                idqk2 = dep.Id;
            }
            else idqk2 = 0;
           


            this.idPunonjes = idpunonjes;
            this.idQendraKosto1 = idqk1;
            this.idQendraKosto2 = idqk2;
           
            this.dtAktivizimi = dtaktivizimi;
            this.idPerdoruesi = idperdoruesi;

            clsMesazh mesazh = this.kontrollo(nrpersonal, qk1, qk2, idNdermarrje);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }
        private clsMesazh kontrollo(string nrpersonal, string qk1, string qk2,  int idNdermarrje)
        {

            if (dtAktivizimi.ToShortDateString() == "01/01/0001")
                return new clsMesazh(false, "Ju lutem vendosni nje date aktivizimi!");
            if (nrpersonal != "")
            {
                if (!clsPunonjes.ekzistonPunonjes(nrpersonal, idNdermarrje))
                    return new clsMesazh(false, "Punonjesi me Nr Personal " + nrpersonal + " nuk ekziston!");
            }
            if (qk1 != "")
            {
                if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(qk1, idNdermarrje))
                    return new clsMesazh(false, "Qendra e kosto 1 nuk ekziston");
            }
            if (qk2 != "")
            {
                if (!DbCore.DbQendraKosto.clsQendraKosto.ekzistonQK(qk2, idNdermarrje))
                    return new clsMesazh(false, "Qendra e kosto 2 nuk ekziston");

                DbCore.DbQendraKosto.clsQendraKosto qendra = new DbCore.DbQendraKosto.clsQendraKosto(qk2, idNdermarrje);
                DbCore.DbQendraKosto.clsQendraKosto qendraprind = new DbCore.DbQendraKosto.clsQendraKosto(qk1, idNdermarrje);
                if (qendra.IdPrindi != qendraprind.Id)
                    return new clsMesazh(false, "Qendra e kosto 2 nuk lidhet me qendren e kostos 1");
            }
           
            return new clsMesazh(true, "Kontrollet u kaluan me sukses!");
        }
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsQendraKostoPunonjes()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="id">id punesim</param>
        public clsQendraKostoPunonjes(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushQendraKosto(db.ktheQendraKostoPunonjes(id));
            db.Dispose();
        }

        public clsQendraKostoPunonjes(DataRow rreshti)
        {
            
            mbushQendraKosto(rreshti);
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
        /// Kthen/Vendos ID-ne e departamentit
        /// </summary>
        public int IdQendraKosto1
        {
            get
            {
                return idQendraKosto1;
            }
            set
            {
                idQendraKosto1 = value;
            }
        }

        /// <summary>
        /// kthen/vendos id e nendepartamentit
        /// </summary>
        public int IdQendraKosto2
        {
            get
            {
                return idQendraKosto2;
            }
            set
            {
                idQendraKosto2 = value;
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
            clsMesazh u_ruajt = data.ruajQendraKostoPunonjes(out id, idPunonjes, idQendraKosto1, idQendraKosto2, dtAktivizimi, idPerdoruesi);
            this.Id = id;
            return u_ruajt;
        }
        public clsMesazh modifiko(int idqendra)
        {
            this.id = idqendra;
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
          
            clsMesazh u_modifikua = data.modifikoQendraKostoPunonjes(id, idPunonjes, idQendraKosto1, idQendraKosto2,  dtAktivizimi, idPerdoruesi);
            return u_modifikua;
        }
        internal clsMesazh modifikoQendra(clsDatabazeListPagesa db, colQendraKostoPunonjes qendravjetra, int idPunonjes, int idPerdoruesi,ResourceManager rm,CultureInfo ci)
        {
            clsMesazh mesazh = new clsMesazh(true);
            clsQendraKostoPunonjes qenv = qendravjetra.Find(x => x.DtAktivizimi.ToShortDateString() == this.DtAktivizimi.ToShortDateString());
            string fushatmod = "";
            string mesazhmod = "";
            if (qenv != null)//nqs ekziston qendra ne kete date e modifikojme prn e shtojme
            {
                mesazhmod = kontrolloqendra(qenv, this, out fushatmod, db);
                this.Id = qenv.Id;
                this.IdPunonjes = qenv.IdPunonjes;
                mesazh = this.modifiko(db);
            }
            else
            {
                if (qendravjetra.Count > 0)
                    mesazhmod = kontrolloqendra(qendravjetra[0], this, out fushatmod, db);
                else
                {
                    mesazhmod = "U modifikuan fushat per qendrat e kostos:";
                }
                if (this.IdQendraKosto1 != 0 || this.IdQendraKosto2 != 0)
                {

                    this.IdPunonjes = idPunonjes;
                    mesazh = this.ruaj(db);
                }

            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSeQendraKosto", ci));
            if (mesazhmod != "U modifikuan fushat per qendrat e kostos:")
            {
                mesazh =clsPunonjes. ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                if (!mesazh.Status)
                    return new clsMesazh(false, gabimpunonjesi);
            }
            return new clsMesazh(true, "Qendra e kostos u ruajten me sukses!");
        }
        private string kontrolloqendra(clsQendraKostoPunonjes qenv, clsQendraKostoPunonjes qendraKostoPunesim, out string fushatmod, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per qendrat e kostos:";
            fushatmod = "U modifikuan fushat per qendrat e kostos:";
            DbQendraKosto.clsDatabaseQendraKosto dbqendra = new DbQendraKosto.clsDatabaseQendraKosto(db );
           
            if (qenv.IdQendraKosto1 != qendraKostoPunesim.IdQendraKosto1)
            {
                mesazh += " Qendra e kostos 1,";
                DbQendraKosto.clsQendraKosto qvj = new DbQendraKosto.clsQendraKosto(qenv.IdQendraKosto1, dbqendra);
                DbQendraKosto.clsQendraKosto qri = new DbQendraKosto.clsQendraKosto(qendraKostoPunesim.IdQendraKosto1, dbqendra);
                fushatmod += String.Format(" Qendra e kostos 1 nga {0} ne {1},", qvj.Kodi, qri.Kodi);
            }
            if (qenv.IdQendraKosto2 != qendraKostoPunesim.IdQendraKosto2)
            {
                mesazh += " Qendra e kostos 2,";
                DbQendraKosto.clsQendraKosto qvj = new DbQendraKosto.clsQendraKosto(qenv.IdQendraKosto2, dbqendra);
                DbQendraKosto.clsQendraKosto qri = new DbQendraKosto.clsQendraKosto(qendraKostoPunesim.IdQendraKosto2, dbqendra);
                fushatmod += String.Format(" Qendra e kostos 2 nga {0} ne {1},", qvj.Kodi, qri.Kodi);
            }
            if (qenv.DtAktivizimi != qendraKostoPunesim.DtAktivizimi)
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", qenv.DtAktivizimi.ToShortDateString(), qendraKostoPunesim.DtAktivizimi.ToShortDateString());
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
            mbushQendraKosto(data.ktheQendraKostoPunonjes(id));
            data.Dispose();
        }
        public static bool ekzistonQKPerKetePunonjeMeKeteDateAktivizimi(string nrpersonal, DateTime dtaktivizimi, out int idpunesimi)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
            {
             return   db.ekzistonQKPerKetePunonjeMeKeteDateAktivizimi(nrpersonal, dtaktivizimi, out idpunesimi);
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush punesimin nga databaza
        /// </summary>
        /// <param name="dbDataRowPunesim">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushQendraKosto(DataRow dbDataRowPunesim)
        {
            if (dbDataRowPunesim != null)
            {
                try
                {
                    int.TryParse(dbDataRowPunesim["ID"].ToString(), out id);
                    int.TryParse(dbDataRowPunesim["IDPUNONJES"].ToString(), out idPunonjes);
                    int.TryParse(dbDataRowPunesim["IDQENDERKOSTO1"].ToString(), out idQendraKosto1);
                    int.TryParse(dbDataRowPunesim["IDQENDERKOSTO2"].ToString(), out idQendraKosto2);
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
