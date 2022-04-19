using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Resources;
using System.Text;

namespace DbCore.DbListPagesat
{
    public class clsBankaPunonjes
    {
        private const string gabimpunonjesi = "Punonjesi nuk u ruajt!";
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se bankave nga db-ja";
        #region Atribute

        private int id;
        private int idPunonjes;
        private string llogBankare;
        private int idBanka;
        private decimal limitTel;
        private decimal limitInternet;
        private DateTime dtAktivizimi;
        private int idPerdoruesi;
        private string banka;
        private DataRow rreshti;


        #endregion

        #region Konstruktor

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id">id e punesimit</param>
        /// <param name="idPunesim">id e punonjesit</param>
        /// <param name="nrllog"> id e departamentit</param>
        /// <param name="idbanka">id e nendepartamentit</param>
        /// <param name="detyra">detyra</param>
        /// <param name="nrKontrate">nrkontrate</param>
        /// <param name="limitTel">tipkontrate</param>
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
        public clsBankaPunonjes(int id, int idPunesim, string nrllog, int idbanka, decimal limitTel, decimal limitInternet, DateTime dtaktivizimi, int idperdoruesi)
        {
            this.id = id;
            this.idPunonjes = idPunesim;
            this.llogBankare = nrllog;
            this.idBanka = idbanka;
            this.limitTel = limitTel;
            this.limitInternet = limitInternet;

            this.dtAktivizimi = dtaktivizimi;
            this.idPerdoruesi = idperdoruesi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsBankaPunonjes()
        {
        }
        /// <summary>
        /// konstruktori me 1 parametra
        /// </summary>
        /// <param name="id">id punesim</param>
        public clsBankaPunonjes(int id)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushBanka(db.ktheBankaPunonjes(id));
            db.Dispose();
        }
        public clsBankaPunonjes(int idpunonjes, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            mbushBanka(db.ktheBankaPunonjesSipasIdPuneonjesiDheData(idpunonjes, data));
            db.Dispose();
        }

        public clsBankaPunonjes(DataRow rreshti)
        {
            
            mbushBanka(rreshti);
        }
        #endregion

        #region Properties
        public string Banka
        {
            get
            {
                return banka;
            }
        }
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
        public string LlogBankare
        {
            get
            {
                return llogBankare;
            }
            set
            {
                llogBankare = value;
            }
        }

        /// <summary>
        /// kthen/vendos id e nendepartamentit
        /// </summary>
        public int IdBanka
        {
            get
            {
                return idBanka;
            }
            set
            {
                idBanka = value;
            }
        }



        /// <summary>
        /// kthen/vendos tipin e kontrates
        /// </summary>
        public decimal LimitTel
        {
            get
            {
                return limitTel;
            }
            set
            {
                limitTel = value;
            }
        }


        /// <summary>
        /// kthen pagen me shtesen
        /// </summary>
        public decimal LimitInternet
        {
            get
            {
                return limitInternet;
            }
            set
            {
                limitInternet = value;
            }
        }
        #endregion


        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(clsDatabazeListPagesa data)
        {
           
            int id;
            clsMesazh u_ruajt = data.ruajBankaPunonjes(out id, idPunonjes, llogBankare, idBanka, limitTel, limitInternet, dtAktivizimi, idPerdoruesi);
            this.id = id;
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPunesim"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko(clsDatabazeListPagesa data)
        {
            clsMesazh u_modifikua = data.modifikoBankaPunonjes(id, idPunonjes, llogBankare, idBanka, limitTel, limitInternet, dtAktivizimi, idPerdoruesi);

            return u_modifikua;
        }
        internal clsMesazh modifikoBanke(clsDatabazeListPagesa db, colBankaPunonjes bankavjeter, int IdPunonjes, int idPerdoruesi, ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            clsMesazh mesazh = new clsMesazh();
            clsBankaPunonjes bkv = bankavjeter.Find(x => x.DtAktivizimi == this.DtAktivizimi);
            string fushatmod = "";
            string mesazhmod = "";
            if (bkv != null)//nqs ekziston banka ne kete date modifikoje prn shtoje
            {
                mesazhmod = kontrollobanka(bkv, this, out fushatmod, db);
                this.Id = bkv.Id;
                this.IdPunonjes = IdPunonjes;
                mesazh = this.modifiko(db);
            }
            else
            {
                if (bankavjeter.Count > 0)
                    mesazhmod = kontrollobanka(bankavjeter[0], this, out fushatmod, db);
                this.IdPunonjes = IdPunonjes;
                mesazh = this.ruaj(db);
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgNdodhiNjeGabimGjateRuajtjesSeQendraKosto", ci));

            if (mesazhmod != "U modifikuan fushat per bankat:")
            {
                mesazh =clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                if (!mesazh.Status)
                    return new clsMesazh(false, gabimpunonjesi);
            }
            return new clsMesazh(true, "Banka u ruajt me sukses!");
        }
        private string kontrollobanka(clsBankaPunonjes bank, clsBankaPunonjes bankaPunonjes, out string fushatmod, clsDatabazeListPagesa db)
        {
            string mesazh = "U modifikuan fushat per bankat:";
            fushatmod = "U modifikuan fushat per bankat:";
            DbArkaBanka.clsDatabaseArkaBanka dbarka = new DbArkaBanka.clsDatabaseArkaBanka(db );
            if (bank.IdBanka != bankaPunonjes.IdBanka)
            {
                mesazh += " Banka,";
                DbArkaBanka.clsBanka bvj = new DbArkaBanka.clsBanka();
                bvj.mbushBankeID(bank.IdBanka, dbarka);
                DbArkaBanka.clsBanka bri = new DbArkaBanka.clsBanka();
                bri.mbushBankeID(bankaPunonjes.IdBanka, dbarka);
                fushatmod += String.Format(" Banka nga {0} ne {1},", bvj.KodiBanka, bri.KodiBanka);
            }
            if (bank.LlogBankare != bankaPunonjes.LlogBankare)
            {
                mesazh += " Nr. Llogari Bankare,";
                fushatmod += String.Format(" Nr. Llogari Bankare nga {0} ne {1},", bank.LlogBankare, bankaPunonjes.LlogBankare);
            }
            if (bank.LimitInternet != bankaPunonjes.LimitInternet)
            {
                mesazh += " Limit interneti,";
                fushatmod += String.Format(" Limit interneti nga {0} ne {1},", bank.LimitInternet, bankaPunonjes.LimitInternet);
            }
            if (bank.LimitTel != bankaPunonjes.LimitTel)
            {
                mesazh += " Limit telefoni,";
                fushatmod += String.Format(" Limit telefoni nga {0} ne {1},", bank.LimitTel, bankaPunonjes.LimitTel);
            }
            if (bank.DtAktivizimi != bankaPunonjes.DtAktivizimi)
            {
                mesazh += " Data e aktivizimit,";
                fushatmod += String.Format(" Data e aktivizimit nga {0} ne {1},", bank.DtAktivizimi.ToShortDateString(), bankaPunonjes.DtAktivizimi.ToShortDateString());
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
            mbushBanka(data.ktheBankaPunonjes(id));
            data.Dispose();
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush punesimin nga databaza
        /// </summary>
        /// <param name="dbDataRowPunesim">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushBanka(DataRow dbDataRowPunesim)
        {
            if (dbDataRowPunesim != null)
            {
                try
                {
                    int.TryParse(dbDataRowPunesim["ID"].ToString(), out id);
                    int.TryParse(dbDataRowPunesim["IDPUNONJES"].ToString(), out idPunonjes);
                    llogBankare = dbDataRowPunesim["LLOGBANKARE"].ToString();
                    int.TryParse(dbDataRowPunesim["IDBANKA"].ToString(), out idBanka);
                    decimal.TryParse(dbDataRowPunesim["LIMITTEL"].ToString(), out limitTel);
                    decimal.TryParse(dbDataRowPunesim["LIMITINTERNET"].ToString(), out limitInternet);
                    banka = dbDataRowPunesim["Banka"].ToString();

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

