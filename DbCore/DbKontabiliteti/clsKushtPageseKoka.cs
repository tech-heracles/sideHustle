using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne koken e nje kushti pagese
    ///  (Te dhenat  merren nga tabela : T_KUSHTPAGESEKOKA)
    /// </remarks>
    public class clsKushtPageseKoka
    {
        #region Atribute 

        private int idKoka;
        private String kodiKushtPagese;
        private String emertimiKushtPagese;
        private String llojiKushtPagese;
        private int idAutorizim; 
        private int afati;
        private String ndarja;
        private String intervaliMidisNdarjeve;
        private int numri;
        private int numriNdarjeve;
        private int idNdermarje;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        public colKushtPageseTrupi oColTrupi;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKushtPageseKoka(int idkoka,string kodi, string emertimi, string lloji, int autorizim, int af, string nd, string inter, int nr, int nrNdarje, int idnderm, int idperdorues, int idstatusdok)
        {
            idKoka = idkoka;
            kodiKushtPagese = kodi;
            emertimiKushtPagese = emertimi;
            llojiKushtPagese = lloji;
            idAutorizim = autorizim;
            afati = af;
            ndarja = nd;
            intervaliMidisNdarjeve = inter;
            numri = nr;
            numriNdarjeve = nrNdarje;
            idNdermarje = idnderm;
            idPerdoruesi = idperdorues;
            idStatusDok = idstatusdok;
            OColTrupi = new colKushtPageseTrupi();
        }

        /// <summary>
        /// Konstruktor me 2 parametra
        /// </summary>
        /// <param name="kodi">kodi i kokes se kushtit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        public clsKushtPageseKoka(String kodi, int idnderm)
        {
            clsDatabaseKontabilitet dbKushtPagesKoka = new clsDatabaseKontabilitet();
            mbushKushtPagesaKoka(dbKushtPagesKoka.ktheKushtPageseSipasKodit(kodi, idnderm));
            dbKushtPagesKoka.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se kushtit</param>
        public clsKushtPageseKoka(int id)
        {
            clsDatabaseKontabilitet dbKushtPagesKoka = new clsDatabaseKontabilitet();
            mbushKushtPagesaKoka(dbKushtPagesKoka.ktheKushtPageseSipasID(id));
            dbKushtPagesKoka.Dispose();
        }

        /// <summary>
        /// Ko0nstruktor i klases
        /// </summary>
        public clsKushtPageseKoka()
        { 
        }

        public clsKushtPageseKoka(DataRow rreshti)
        {
            
            mbushKushtPagesaKoka(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e kushtit te pageses
        /// </summary>
        public String KodiKushtPagese
        {
            get { return kodiKushtPagese; }
            set { kodiKushtPagese = value; }
        }

        /// <summary>
        /// Kthen/Vendos emertimin e kushtit te pageses
        /// </summary>
        public String EmertimiKushtPagese
        {
            get { return emertimiKushtPagese; }
            set { emertimiKushtPagese = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e kushtit te pageses -E plote, Me pjese
        /// </summary>
        public String LlojiKushtPagese
        {
            get { return llojiKushtPagese; }
            set { llojiKushtPagese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e autorizimit
        /// </summary>
        public int IdAutorizim
        {
            get { return idAutorizim; }
            set { idAutorizim = value; }
        }

        /// <summary>
        /// Kthen/Vendos afatin (nese pagesa eshte "E Plote")
        /// </summary>
        public int Afati
        {
            get { return afati; }
            set { afati = value; }
        }

        /// <summary>
        /// Kthen/Vendos ndarjen - Perqindje, Interval
        /// </summary>
        public String Ndarja
        {
            get { return ndarja; }
            set { ndarja = value; }
        }

        /// <summary>
        /// Kthen/Vendos intervalin midis ndarjeve - Dite, Jave, Muaj, Numer ditesh
        /// </summary>
        public String IntervaliMidisNdarjeve
        {
            get { return intervaliMidisNdarjeve; }
            set { intervaliMidisNdarjeve = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin (nese pagesa eshte "Me pjese" dhe ndarja eshte "Interval")
        /// </summary>
        public int Numri
        {
            get { return numri; }
            set { numri = value; }
        }

        /// <summary>
        /// Kthen/Vendos  numrin e ndarjeve (nese pagesa eshte "Me pjese" dhe ndarja eshte "Interval")
        /// </summary>
        public int NumriNdarjeve
        {
            get { return numriNdarjeve; }
            set { numriNdarjeve = value; }
        }

        /// <summary>
        ///  Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        ///  Kthen/Vendos trupin e kesaj koke
        /// </summary>
        public colKushtPageseTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }
        public int IdStatusDok
            {
            get { return idStatusDok; }
            set { idStatusDok = value; }
            }
        public int IdPerdoruesi
            {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
            }
        public DateTime DtKrijimi
            {
            get { return dtKrijimi; }

            }
        public DateTime DtModifikimi
            {
            get { return dtModifikimi; }

            }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsKushtPageseKoka dhe trupin e tij
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi">kodi i kokes</param>
        /// <param name="emertimi">emertimi i kushtit</param>
        /// <param name="lloji">Lloji i kushtit</param>
        /// <param name="autorizim">autorizim</param>
        /// <param name="af">afati</param>
        /// <param name="nd">ndarjet</param>
        /// <param name="inter">intervalet</param>
        /// <param name="nr">numri</param>
        /// <param name="nrNdarje">numri i ndarjeve</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="oColTrupi">kolection trupi</param>
        /// <returns>Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.</returns>
        public clsMesazh ruajKushtPagese(int idkoka, string kodi, string emertimi, string lloji, int autorizim, int af, string nd, string inter, int nr, int nrNdarje, int idnderm, colKushtPageseTrupi oColTrupi, int idperdorues, int idstatusdok)
        {
          
            bool statusVeprimi;
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            
            
            try
            {dbKont.beginTransaksion();
                clsMesazh mesazh = new clsMesazh();
                if (!dbKont.ekzistonKushtPagese(kodi, idnderm))
                {
                    idkoka = dbKont.ruajKushtPageseKoka(idkoka, kodi, emertimi, lloji, autorizim, af, nd, inter, nr, nrNdarje, idnderm, idperdorues, idstatusdok);
                    if (idkoka == 0)
                        statusVeprimi = false;
                    else statusVeprimi = true;

                    if (statusVeprimi)
                    {
                        foreach (clsKushtPageseTrupi o in oColTrupi)
                        {
                            if (statusVeprimi)
                            {
                                o.IdKoka = idkoka;
                                if (!(o.Intervali == " ") || !(o.KushtPagese == 0))
                                {
                                    int idT;
                                    if (o.Periudha == null)
                                        o.Periudha = "";
                                    mesazh = dbKont.ruajKushtPageseTrupi(out idT, o.IdKoka, o.Intervali, o.Periudha, o.Dite, o.Zbritje, o.KushtPagese);
                                }
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                return mesazh;
                            }
                        }
                        if (mesazh.Status)
                        {
                            dbKont.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                else mesazh = new clsMesazh(false, "Ekziston nje kusht pagese me kete kod!");

                return mesazh;
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan koken dhe trupin e kushtit te pageses ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka.ruajKushtPagese"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsKushtPageseKoka data = new clsKushtPageseKoka();
            clsMesazh u_ruajt = data.ruajKushtPagese(this.IdKoka, this.KodiKushtPagese, this.EmertimiKushtPagese, this.LlojiKushtPagese, this.IdAutorizim, this.Afati, this.Ndarja, this.IntervaliMidisNdarjeve, this.Numri, this.NumriNdarjeve, this.IdNdermarje, this.OColTrupi, this.idPerdoruesi, this.idStatusDok);
            return u_ruajt;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsKushtPageseKoka dhe trupin e tij
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <param name="kodi">kodi i kokes</param>
        /// <param name="emertimi">emertimi i kushtit</param>
        /// <param name="lloji">Lloji i kushtit</param>
        /// <param name="autorizim">autorizim</param>
        /// <param name="af">afati</param>
        /// <param name="nd">ndarjet</param>
        /// <param name="inter">intervalet</param>
        /// <param name="nr">numri</param>
        /// <param name="nrNdarje">numri i ndarjeve</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <param name="oColTrupi">kolection trupi</param>
        /// <returns>Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.</returns>
        public clsMesazh modifikoKushtPagese(int idkoka, string kodi, string emertimi, string lloji, int autorizim, int af, string nd, string inter, int nr, int nrNdarje, int idnderm, colKushtPageseTrupi oColTrupi, int idperdorues, int idstatusdok)
        {
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
             try
            {
            dbKont.beginTransaksion();
            clsMesazh mesazh = new clsMesazh();
           
                mesazh = dbKont.modifikoKushtPageseKoka(idkoka, kodi, emertimi, lloji, autorizim, af, nd, inter, nr, nrNdarje, idnderm, idperdorues, idstatusdok);
                if (mesazh.Status)
                {
                    mesazh = dbKont.fshiKushtPageseTrupi(idkoka);
                    if (mesazh.Status)
                    {
                        foreach (clsKushtPageseTrupi o in oColTrupi)
                        {
                            if (mesazh.Status)
                            {
                                o.IdKoka = idkoka;
                                if (!(o.Intervali == " ") || !(o.KushtPagese == 0))
                                {
                                    if (o.Periudha == null)
                                        o.Periudha = "";
                                    int idT;
                                    mesazh = dbKont.ruajKushtPageseTrupi(out idT, o.IdKoka, o.Intervali, o.Periudha, o.Dite, o.Zbritje, o.KushtPagese);
                                }
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                return mesazh;
                            }
                        }
                        if (mesazh.Status)
                        {
                            dbKont.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();
                    
                    return mesazh;
                }
            }
             catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon koken dhe trupin e kushtit te pageses ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka.modifikoKushtPagese"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {
            clsKushtPageseKoka data = new clsKushtPageseKoka();
            clsMesazh u_modifikua = data.modifikoKushtPagese(this.IdKoka, this.KodiKushtPagese, this.EmertimiKushtPagese, this.LlojiKushtPagese, this.IdAutorizim, this.Afati, this.Ndarja, this.IntervaliMidisNdarjeve, this.Numri, this.NumriNdarjeve, this.IdNdermarje, this.OColTrupi, this.idPerdoruesi, this.idStatusDok);
            return u_modifikua;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsKushtPageseKoka dhe trupin e tij
        /// </summary>
        /// <param name="idkoka">id e kokes</param>
        /// <returns>Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.</returns>
        public clsMesazh fshiKushtPagese(int idkoka)
        {
         
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();            
           
            clsMesazh mesazh = new clsMesazh();
            try
            { dbKont.beginTransaksion();
                mesazh = dbKont.fshiKushtPageseTrupi(idkoka);
                if (mesazh.Status)
                {
                    mesazh = dbKont.fshiKushtPageseKoka(idkoka);
                    if (mesazh.Status)
                    {
                        dbKont.commitTransaksion();
                        
                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                        return mesazh;
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin koken dhe trupin e kushtit te pageses ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKushtPageseKoka.fshiKushtPagese"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {
            clsDatabaseKontabilitet data = new  clsDatabaseKontabilitet ();
            clsMesazh u_fshi = data.fshiKushtPageseKokaStatus(this.IdKoka, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }
        public static bool ekziston(string kod, int idndermarje)
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            bool ekziston = db.ekzistonKushtPagese(kod, idndermarje);
            db.Dispose();
            return ekziston;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e kushteve te pageses nga databaza
        /// </summary>
        /// <param name="dbDataRowKushtPagesaKoka">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKushtPagesaKoka(DataRow dbDataRowKushtPagesaKoka)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbush kusht pagesa koka.");
            if (dbDataRowKushtPagesaKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowKushtPagesaKoka["IDKOKA"].ToString(), out idKoka);
                    kodiKushtPagese = dbDataRowKushtPagesaKoka["KODIKUSHTPAGESE"].ToString();
                    emertimiKushtPagese = dbDataRowKushtPagesaKoka["EMERTIMIKUSHTPAGESE"].ToString();
                    llojiKushtPagese = dbDataRowKushtPagesaKoka["LLOJIKUSHTPAGESE"].ToString();
                    int.TryParse(dbDataRowKushtPagesaKoka["IDAUTORIZIM"].ToString(), out idAutorizim);
                    int.TryParse(dbDataRowKushtPagesaKoka["AFATI"].ToString(), out afati);
                    ndarja = dbDataRowKushtPagesaKoka["NDARJA"].ToString();
                    intervaliMidisNdarjeve = dbDataRowKushtPagesaKoka["INTERVALIMIDISNDARJEVE"].ToString();
                    int.TryParse(dbDataRowKushtPagesaKoka["NUMRI"].ToString(), out numri);
                    int.TryParse(dbDataRowKushtPagesaKoka["NUMRINDARJEVE"].ToString(), out numriNdarjeve);
                    int.TryParse(dbDataRowKushtPagesaKoka["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKushtPagesaKoka["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowKushtPagesaKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowKushtPagesaKoka["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKushtPagesaKoka["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se kokes se kushteve te pageses nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se kushteve te pageses nga db-ja");
                }
            }
            else
                return false;

            ImbLogger.LogTraceShitje("Mbaroi metoda mbush kusht pagesa koka.");
        }

        #endregion
    }
}

