using System;
using System.Collections.Generic;
using System.Data;
using DbCore.IMBUtils.Types;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne buxhetin
    ///  (Te dhenat  merren nga tabela : T_BUXHETI)
    ///</remarks>
    public class clsBuxheti
    {
        #region Atribute

        private int idBuxheti;
        private int idLlojBuxheti;
        private int idLidhese;
        private String muaj;
        private decimal buxheti_1;
        private decimal buxheti_2;
        private decimal gjendja;
        private decimal diferenca_1;
        private decimal diferenca_2;
        private int idNdermViti;
        private string shenime = "";
        private DateTime dtAktivizimi;
        private int idKonfigUrdherPagese;



        #endregion

        #region Konstruktore
        public clsBuxheti(Dictionary<string, object> rreshtDokuKlient, int idllojbuxheti)
        {
            muaj = rreshtDokuKlient["Muaj"].ToString();
            decimal.TryParse(rreshtDokuKlient["Buxheti_1"].ToString(), out buxheti_1);
            decimal.TryParse(rreshtDokuKlient["Buxheti_2"].ToString(), out buxheti_2);
            decimal.TryParse(rreshtDokuKlient["Gjendja"].ToString(), out gjendja);
            decimal.TryParse(rreshtDokuKlient["Diferenca_1"].ToString(), out diferenca_1);
            decimal.TryParse(rreshtDokuKlient["Diferenca_2"].ToString(), out diferenca_2);
            shenime = rreshtDokuKlient.ContainsKey("Shenime") ? Converter.ToStringOrEmpty(rreshtDokuKlient["Shenime"]) : "";
            idLlojBuxheti = idllojbuxheti;
            dtAktivizimi = new DateTime(1970, 1, 1);
           
        }


        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idbuxheti">Id e buxhetit</param>
        /// <param name="idllojbuxheti">Id e llojit te buxhetit <seealso cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/></param>
        /// <param name="idlidhese">Id lidhese</param>
        /// <param name="muaji">Muaji</param>
        /// <param name="buxh_1">Buxheti 1</param>
        /// <param name="buxh_2">Buxheti 2</param>
        /// <param name="gjen">Gjendja</param>
        /// <param name="diff1">Diferenca 1</param>
        /// <param name="diff2">Diferenca 2</param>
        public clsBuxheti(int idbuxheti, int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, decimal gjen, decimal diff1, decimal diff2, string shenime, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {
            idBuxheti = idbuxheti;
            idLlojBuxheti = idllojbuxheti;
            idLidhese = idlidhese;
            muaj = muaji;
            buxheti_1 = buxh_1;
            buxheti_2 = buxh_2;
            gjendja = gjen;
            diferenca_1 = diff1;
            diferenca_2 = diff2;
            this.shenime = shenime;
            this.dtAktivizimi = dtaktivizimi;
            this.idKonfigUrdherPagese = idkonfigurdherpagese;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="idllojbuxheti">Id e llojit te buxhetit <seealso cref="DbCore.DbKontabiliteti.clsLlojBuxheti"/></param>
        /// <param name="idlidhese">Id lidhese</param>
        /// <param name="muaji">Muaji</param>
        /// <param name="buxh_1">Buxheti 1</param>
        /// <param name="buxh_2">Buxheti 2</param>
        /// <param name="gjen">Gjendja</param>
        /// <param name="diff1">Diferenca 1</param>
        /// <param name="diff2">Diferenca 2</param>
        public clsBuxheti(int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, decimal gjen, decimal diff1, decimal diff2, DateTime dtaktivizimi, int idkonfigurdherpagese)
        {
            idLlojBuxheti = idllojbuxheti;
            idLidhese = idlidhese;
            muaj = muaji;
            buxheti_1 = buxh_1;
            buxheti_2 = buxh_2;
            gjendja = gjen;
            diferenca_1 = diff1;
            diferenca_2 = diff2;
            this.dtAktivizimi = dtaktivizimi;
            this.idKonfigUrdherPagese = idkonfigurdherpagese;
        }
        public clsBuxheti(int idllojbuxheti, int idlidhese, String muaji, decimal buxh_1, decimal buxh_2, decimal gjen, decimal diff1, decimal diff2, string shenime, DateTime dtaktivizimi, int idkonfigurdherpagese) : this(idllojbuxheti, idlidhese, muaji, buxh_1, buxh_2, gjen, diff1, diff2,dtaktivizimi,idkonfigurdherpagese)
        {
            this.shenime = shenime;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsBuxheti()
        {
        }

        public clsBuxheti(DataRow rreshti)
        {
            
            mbushBuxhet(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdBuxheti
        {
            get { return idBuxheti; }
            set { idBuxheti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te buxhetit
        /// </summary>
        public int IdLlojBuxheti
        {
            get { return idLlojBuxheti; }
            set { idLlojBuxheti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese
        /// </summary>
        public int IdLidhese
        {
            get { return idLidhese; }
            set { idLidhese = value; }
        }

        /// <summary>
        /// Kthen/Vendos muajin
        /// </summary>
        public String Muaj
        {
            get { return muaj; }
            set { muaj = value; }
        }

        /// <summary>
        /// Kthen/Vendos Buxhetin 1
        /// </summary>
        public Decimal Buxheti_1
        {
            get { return buxheti_1; }
            set { buxheti_1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos Buxhetin 2
        /// </summary>
        public Decimal Buxheti_2
        {
            get { return buxheti_2; }
            set { buxheti_2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos Diferencen 1
        /// </summary>
        public Decimal Diferenca_1
        {
            get { return diferenca_1; }
            set { diferenca_1 = value; }
        }

        /// <summary>
        /// Kthen/Vendos Diferencen 2
        /// </summary>
        public Decimal Diferenca_2
        {
            get { return diferenca_2; }
            set { diferenca_2 = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen
        /// </summary>
        public Decimal Gjendja
        {
            get { return gjendja; }
            set { gjendja = value; }
        }
        /// <summary>
        /// Kthen/Vendos IdVitin e ndermarrjes
        /// </summary>
        public int IdNdermViti
        {
            get { return idNdermViti; }
            set { idNdermViti = value; }
        }
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

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

        public int IdKonfigUrdherPagese
        {
            get
            {
                return idKonfigUrdherPagese;
            }

            set
            {
                idKonfigUrdherPagese = value;
            }
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajBuxhet"/>
        /// </summary>
        /// <param name="eshteProjektBuxhet">Kur lloji i buxhetit eshte Kategori Shpenzimi dhe tregon nese do jene vlera te buxhetit faktike apo parashikuese. 
        /// True kur vlerat e buxheteve te kategorise se shpenzimit jane parashikuese dhe false kur jane faktike</param>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(bool eshteProjektBuxhet)
        {
            using (clsDatabaseKontabilitet data = new clsDatabaseKontabilitet())
            {
                int id;
                return data.ruajBuxhet(out id, this.IdLlojBuxheti, this.IdLidhese, this.Muaj, this.Buxheti_1, this.Buxheti_2, this.IdNdermViti, eshteProjektBuxhet, this.shenime, this.dtAktivizimi, this.idKonfigUrdherPagese);
            }
        }

        /// <summary>
        /// /// Ruan objektin e buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajBuxhet"/>
        /// </summary>
        /// <returns></returns>
        public clsMesazh ruaj()
        {
            return ruaj(false);
        }

        /// <summary>
        /// Modifikon objektin e buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoBuxhet"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public bool modifiko()
        {
            using (clsDatabaseKontabilitet data = new clsDatabaseKontabilitet())
            {
                return data.modifikoBuxhet(this.IdBuxheti, this.IdLlojBuxheti, this.IdLidhese, this.Muaj, this.Buxheti_1, this.Buxheti_2, this.dtAktivizimi, this.idKonfigUrdherPagese).Status;
            }
        }

        /// <summary>
        /// Fshin objektin e buxhetit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiBuxhet"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public bool fshi()
        {
            using (clsDatabaseKontabilitet data = new clsDatabaseKontabilitet())
            {
                return data.fshiBuxhet(this.IdLidhese).Status;
            }
        }
        /// <summary>
        /// metode qe tregon nese per kategorine e shpenzimit, ekzistojne buxhetet ose projekt buxhetet per kete kategori
        /// </summary>
        /// <param name="idLidhese"></param>
        /// <param name="idLlojBuxheti"></param>
        /// <param name="idVitiProjektBuxheti">nese eshte projekt buxheti i kalon id e vitit te tbl T_VITET_PROJEKTBUXHET nese eshte buxhet i kalon id nderm e vitit te loguar</param>
        /// <param name="eshteProjektBuxhet">true nese eshte lloji projekt buxhet te kategoria e shpenzimit, false nese eshte buxhet</param>
        /// <returns>kthen true, nese ekzistojne, false perndryshe</returns>
        public static bool ekzistonProjektBuxhetiPerKategorine(int idLidhese, int idLlojBuxheti, int idVitiProjektBuxheti, bool eshteProjektBuxhet, DateTime dtaktivizimi)
        {
            using (clsDatabaseKontabilitet data = new clsDatabaseKontabilitet())
            {
                return ekzistonProjektBuxhetiPerKategorine(idLidhese, idLlojBuxheti, idVitiProjektBuxheti, eshteProjektBuxhet,dtaktivizimi, data);
            }
        }
        public static bool ekzistonProjektBuxhetiPerKategorine(int idLidhese, int idLlojBuxheti, int idVitiProjektBuxheti, bool eshteProjektBuxhet,DateTime dtaktivizimi, clsDatabaseKontabilitet data)
        {
            return data.ekzistonProjektBuxhetiPerKategorine(idLidhese, idLlojBuxheti, idVitiProjektBuxheti, eshteProjektBuxhet,dtaktivizimi);
        }
        public static bool ekzistonBuxhetPerDaten(int idLidhese, int idLlojBuxheti, int idVitiProjektBuxheti, DateTime dt, clsDatabaseKontabilitet data)
        {
            return data.ekzistonBuxhetPerDaten(idLidhese, idLlojBuxheti, idVitiProjektBuxheti, dt);
        }
        
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush buxhetet nga databaza
        /// </summary>
        /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushBuxhet(DataRow dbDataRowBuxhet)
        {
            if (dbDataRowBuxhet != null)
            {
                try
                {
                    int.TryParse(dbDataRowBuxhet["IDBUXHETI"].ToString(), out idBuxheti);
                    int.TryParse(dbDataRowBuxhet["IDLLOJBUXHETI"].ToString(), out idLlojBuxheti);
                    int.TryParse(dbDataRowBuxhet["IDLIDHESE"].ToString(), out idLidhese);
                    int.TryParse(dbDataRowBuxhet["IDKONFIGURDHERPAGESE"].ToString(), out idKonfigUrdherPagese);
                    muaj = dbDataRowBuxhet["MUAJ"].ToString();
                    Decimal.TryParse(dbDataRowBuxhet["BUXHETI_1"].ToString(), out buxheti_1);
                    Decimal.TryParse(dbDataRowBuxhet["BUXHETI_2"].ToString(), out buxheti_2);
                    Decimal.TryParse(dbDataRowBuxhet["gjendja"].ToString(), out gjendja);
                    Decimal.TryParse(dbDataRowBuxhet["diferenca_1"].ToString(), out diferenca_1);
                    Decimal.TryParse(dbDataRowBuxhet["diferenca_2"].ToString(), out diferenca_2);
                    DateTime.TryParse(dbDataRowBuxhet["DtAktivizimi"].ToString(), out dtAktivizimi);
                    object objShenime = dbDataRowBuxhet["Shenime"];
                    shenime = objShenime != null && objShenime != DBNull.Value ? Convert.ToString(objShenime) : "";
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se buxheteve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
