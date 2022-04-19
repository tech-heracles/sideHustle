using System;
﻿using DbCore.IMBUtils.Extensions;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje pasqyre financiare
    ///  (Te dhenat  merren nga tabela : T_PASQYRAFINANCIAREKOKA)
    ///</remarks>
    public class clsPasqyreFinanciare
    {
        #region Atribute

        private int _idPasqyresFin;
        private bool _model;
        private int _idNdermarja;
        private int _viti;
        private int _idPerdoruesi;
        private int _idStatusDok;
        private DateTime _dtKrijimi;
        private DateTime _dtModifikimi;
        private DataRow _rreshti;

        #endregion

        #region Konstruktoret
        
        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se pasqyres financiare</param>
        public clsPasqyreFinanciare(int id)
        {
            using (var dbPasqyraFinanc = new clsDatabaseKontabilitet())
                MbushPasqyraFinanciare(dbPasqyraFinanc.kthePasqyraFinaciareSipasId(id));
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsPasqyreFinanciare()
        {
        }

        public clsPasqyreFinanciare(DataRow rreshti)
        {
            MbushPasqyraFinanciare(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdPasqyresFin
        {
            get { return _idPasqyresFin; }
            set { _idPasqyresFin = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e pasqyres financiare
        /// </summary>
        public string KodiPasqyresFin { get; set; }

        /// <summary>
        /// Kthen/Vendos emertimin e pasqyres financiare
        /// </summary>
        public string EmertimiPasqyresFin { get; set; }

        /// <summary>
        /// tregon nese kjo pasqyre financiare eshte pasqyre model apo jo ne menyre qe te mos fshihet
        /// </summary>
        public bool Model
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos tipin e pasqyres financiare
        /// </summary>
        public string TipiPasqyresFin { get; set; }

        /// <summary>
        /// Kthen/Vendos metoden e pasqyres financiare
        /// </summary>
        public string Metoda { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarja
        {
            get { return _idNdermarja; }
            set { _idNdermarja = value; }
        }

        /// <summary>
        /// Kthen/Vendos vitin
        /// </summary>
        public int Viti
        {
            get { return _viti; }
            set { _viti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit
        /// </summary>
        public int IdPerdoruesi
        {
            get { return _idPerdoruesi; }
            set { _idPerdoruesi = value; }
        }

        public int IdStatusDok
        {
            get { return _idStatusDok; }
            set { _idStatusDok = value; }
        }

        public DateTime DtKrijimi => _dtKrijimi;

        public DateTime DtModifikimi => _dtModifikimi;

        /// <summary>
        /// Collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare"/>
        /// </summary>
        public colTrupPasqyreFinaciare OColTrupi { get; set; }

        #endregion

        #region Metoda Publike
        
        public clsMesazh Fshi()
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.fshiPasqyreKokaStatus(this._idPasqyresFin, this._idPerdoruesi);
        }

        /// <summary>
        ///  Ruan objektin ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajPASH"/>
        /// <param name="idpasqyres">Id e pasqyres financiare</param>
        /// <param name="kodipasqyres">Kodi i pasqyres financiare</param>
        /// <param name="emertimipasqyres">Emertimi i pasqyres financiare</param>
        /// <param name="tipipasqyres">Tipi i pasqyres financiare - Bilanc, PASH, Cash Flow</param>
        /// <param name="metoda">Metoda - Direkt, Indirekt</param>
        /// <param name="idndermarja">Id e ndermarrjes</param>
        /// <param name="vit">Viti</param>
        /// <param name="idndermvit">Id lidhese ndermarrje vit</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="ocoltrupipash">Collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare"/></param>     
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh RuajPasqyre(int idpasqyres, 
            string kodipasqyres, 
            string emertimipasqyres, 
            string tipipasqyres, 
            string metoda, 
            int idndermarja, 
            int vit, 
            int idperdoruesi, 
            colTrupPasqyreFinaciare ocoltrupipash, 
            int idstatusdok)
        {
            var dbKont = new clsDatabaseKontabilitet();
            try
            {
                dbKont.beginTransaksion();
                var mesazh = new clsMesazh();
                mesazh = dbKont.ruajPasqyraFinanciareKoka(out idpasqyres, kodipasqyres, emertimipasqyres, tipipasqyres, metoda, idndermarja, vit, idperdoruesi, idstatusdok, _model);
                if (!mesazh.Status)
                {
                    dbKont.rollbackTransaksion();
                    return mesazh;
                }
                DataTable dtLlogarite = new DataTable();
                DataTable dtBuxhetet = new DataTable();
                foreach (var trupi in ocoltrupipash)
                {
                    trupi.IdKoka = idpasqyres;
                    int idT;
                    mesazh = dbKont.ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal, trupi.KodZeri, trupi.ShfaqBij);
                    if (!mesazh.Status)
                    {
                       dbKont.rollbackTransaksion();
                       return mesazh;
                    }
                    trupi.IdTrupi = idT;
                    if (trupi.OColLlogarite.Count > 0)
                    {
                        foreach (var llog in trupi.OColLlogarite)
                        {
                            llog.IdTrupi = trupi.IdTrupi;
                            llog.IdPerdoruesi = idperdoruesi;
                        }
                        dtLlogarite.Merge(trupi.OColLlogarite.ToDataTable());  
                    }
                    if (tipipasqyres != "Cash Flow" && trupi.OColBuxhetet.Count > 0)
                    {
                        foreach (var buxheti in trupi.OColBuxhetet)
                        {
                            buxheti.IdLidhese = trupi.IdTrupi;
                        }
                        dtBuxhetet.Merge(trupi.OColBuxhetet.ToDataTable());
                    }
                }
                if (dtLlogarite.Rows.Count > 0)
                {
                    mesazh = dbKont.ruajLlogariTrupiPasqyresDt(dtLlogarite);
                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }
                if (dtBuxhetet.Rows.Count > 0)
                {
                    mesazh = dbKont.ruajBuxhetDt(dtBuxhetet);
                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbKont.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;  
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        ///  Modifikon objektin ne tabelen perkatese ne databaze.Therret funksionin
        ///  <see cref="DbCore.DbKontabiliteti.clsPASH.modifikoPASHAndBuxhete"/>
        /// <param name="idpasqyres">Id e pasqyres financiare</param>
        /// <param name="kodipasqyres">Kodi i pasqyres financiare</param>
        /// <param name="emertimipasqyres">Emertimi i pasqyres financiare</param>
        /// <param name="tipipasqyres">Tipi i pasqyres financiare - Bilanc, PASH, Cash Flow</param>
        /// <param name="metoda">Metoda - Direkt, Indirekt</param>
        /// <param name="idperdoruesi">Id e perdoruesit</param>
        /// <param name="ocoltrupipash">Collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare"/></param>   
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh ModifikoPasqyre(int idpasqyres, 
            string kodipasqyres, 
            string emertimipasqyres, 
            string tipipasqyres, 
            string metoda, 
            int idperdoruesi, 
            colTrupPasqyreFinaciare ocoltrupipash, 
            int idstatusdok)
        {
            var colTrupi = new colTrupPasqyreFinaciare(idpasqyres);
            var colBuxh = new colBuxhetet();
            var colLlog = new colLlogariaTrupiPasqyres();

            foreach (var t in colTrupi)
            {
                colBuxh.AddRange(new colBuxhetet(t.IdTrupi, "PasqyreFinanciare"));
                colLlog.AddRange(new colLlogariaTrupiPasqyres(t.IdTrupi));
            }

            var dbKont = new clsDatabaseKontabilitet();

            try
            {
                dbKont.beginTransaksion();
                var mesazh = new clsMesazh(true);

                foreach (var o in colBuxh)
                {
                    if (mesazh.Status)
                        mesazh = dbKont.fshiBuxhet(o.IdLidhese);
                    else
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }

                foreach (var l in colLlog)
                {
                    if (mesazh.Status)
                        mesazh = dbKont.fshiLlogariPasqyreTrupi(l.IdTrupi);
                    else
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }

                foreach (var t in colTrupi)
                {
                    if (mesazh.Status)
                        mesazh = dbKont.fshiPasqyreTrupi(t.IdTrupi);
                    else
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }

                mesazh = dbKont.modifikoPasqyraFinanciareKoka(idpasqyres, kodipasqyres, emertimipasqyres, tipipasqyres, metoda, idperdoruesi, idstatusdok);
                if (!mesazh.Status)
                {
                    dbKont.rollbackTransaksion();
                    return mesazh;
                }

                DataTable dtLlogarite = new DataTable();
                DataTable dtBuxhetet = new DataTable();
                foreach (var trupi in ocoltrupipash)
                {
                    trupi.IdKoka = idpasqyres;
                    int idT;
                    mesazh = dbKont.ruajTrupinPasqyresFinanciare(out idT, trupi.IdKoka, trupi.PershkrimiZerit, trupi.PrindiZerit, trupi.NiveliZerit, trupi.LlojiZerit, trupi.GjeneroTotal, trupi.KodZeri, trupi.ShfaqBij);
                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                    trupi.IdTrupi = idT;
                    if (trupi.OColLlogarite.Count > 0)
                    {
                        foreach (var llog in trupi.OColLlogarite)
                        {
                            
                            llog.IdTrupi = trupi.IdTrupi;
                            llog.IdPerdoruesi = idperdoruesi;
                        }
                        dtLlogarite.Merge(trupi.OColLlogarite.ToDataTable());
                    }
                    if(trupi.OColBuxhetet.Count > 0)
                    {
                        foreach (var buxheti in trupi.OColBuxhetet)
                        {
                            buxheti.IdLidhese = trupi.IdTrupi;   
                        }
                        dtBuxhetet.Merge(trupi.OColBuxhetet.ToDataTable());
                    }
                }
                if (dtLlogarite.Rows.Count > 0)
                {
                    mesazh = dbKont.ruajLlogariTrupiPasqyresDt(dtLlogarite);
                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }
                if (dtBuxhetet.Rows.Count > 0)
                {
                    mesazh = dbKont.ruajBuxhetDt(dtBuxhetet);
                    if (!mesazh.Status)
                    {
                        dbKont.rollbackTransaksion();
                        return mesazh;
                    }
                }
                dbKont.commitTransaksion();
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// kontroloon nese ekziston pasqyre me kete kod ne kete ndermarrje
        /// </summary>
        /// <param name="kodi"></param>
        /// <param name="idNdermarrje"></param>
        public static bool EkzistonPasqyreMeKeteKod(string kodi, int idNdermarrje)
        {
            using (var dbKont = new clsDatabaseKontabilitet())
                return dbKont.ekzistonPasqyreMeKeteKodSipasNdermarrje(kodi, idNdermarrje);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush pasqyra financiare nga databaza
        /// </summary>
        /// <param name="dbDataRowPasqyraFinanc">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool MbushPasqyraFinanciare(DataRow dbDataRowPasqyraFinanc)
        {
            if (dbDataRowPasqyraFinanc != null)
            {
                try
                {
                    int.TryParse(dbDataRowPasqyraFinanc["IDPASQFINKOKA"].ToString(), out _idPasqyresFin);
                    KodiPasqyresFin = dbDataRowPasqyraFinanc["KODIPASQFINKOKA"].ToString();
                    EmertimiPasqyresFin = dbDataRowPasqyraFinanc["EMERTIMIPASQFINKOKA"].ToString();
                    TipiPasqyresFin = dbDataRowPasqyraFinanc["TIPIPASQFINKOKA"].ToString();
                    Metoda = dbDataRowPasqyraFinanc["METODAPASQFINKOKA"].ToString();
                    int.TryParse(dbDataRowPasqyraFinanc["IDNDERMARJE"].ToString(), out _idNdermarja);
                    int.TryParse(dbDataRowPasqyraFinanc["VITI"].ToString(), out _viti);
                    int.TryParse(dbDataRowPasqyraFinanc["IDPERDORUESI"].ToString(), out _idPerdoruesi);
                    int.TryParse(dbDataRowPasqyraFinanc["IDSTATUSDOK"].ToString(), out _idStatusDok);
                    bool.TryParse(dbDataRowPasqyraFinanc["MODEL"].ToString(), out _model);
                    DateTime.TryParse(dbDataRowPasqyraFinanc["DTKRIJIMI"].ToString(), out _dtKrijimi);
                    DateTime.TryParse(dbDataRowPasqyraFinanc["DTMODIFIKIMI"].ToString(), out _dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se pasqyrave financiare nga db-ja");
                }
            }
            return false;
        }

        #endregion
    }
}
