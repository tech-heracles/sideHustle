using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbArkaBanka
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti  Urdherpagese
    ///  (Te dhenat  merren nga tabela : T_TRUPIUDHERPAGESA)
    /// </summary>
    public class clsTrupiUrdherPagese
    {
        private const string gabimNeTeDhena = "ERROR: Gabim gjate marrjes se trupit te Urdherpageses nga db-ja";
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private int idGrupi;
        private int idTitulli;
        private int idKapitulli;
        private int idLlogArtikulli;
        private int idLlogAnaliza;
        private string kodProjekti;
        private decimal shuma;
        private string objekti;
        private DataRow rreshti;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>

        public clsTrupiUrdherPagese(int idtrupi, int idkoka, int idgrupi, int idtitulli, int kapitulli, int idllogartikulli, int idlloganaliza, string kodprojekti, decimal shuma, string objekti)
        {
            idTrupi = idtrupi;
            idKoka = idkoka;
            idGrupi = idgrupi;
            idTitulli = idtitulli;
            idKapitulli = kapitulli;
            idLlogArtikulli = idllogartikulli;
            idLlogAnaliza = idlloganaliza;
            kodProjekti = kodprojekti;
            this.shuma = shuma;
            this.objekti = objekti;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idKoka">id e kokes</param>
        public clsTrupiUrdherPagese(int idtrupi)
        {
            clsDatabaseArkaBanka db = new clsDatabaseArkaBanka();

            mbushTrupUrdherPagese(db.ktheTrupiUrdherPageseSipasID(idtrupi));
            db.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiUrdherPagese()
        {
        }

        /// <summary>
        /// mbush trupin sipas te dhenave te grides
        /// </summary>
        /// <param name="rreshtDoku"></param>
        /// <param name="komp"></param>
        /// <param name="idndermarje"></param>
        public clsTrupiUrdherPagese(Dictionary<string, object> rreshtDoku, int idndermarje)
        {
            string grupi = rreshtDoku["txtGrupi"].ToString();
            string titulli = rreshtDoku["txtTitulli"].ToString();
            string kapitulli = rreshtDoku["txtKapitulli"].ToString();
            string artikulli = rreshtDoku["txtArtikulli"].ToString();
            string analiza = rreshtDoku["txtAnalize"].ToString();
            string kodprojekti = rreshtDoku["txtKodProjekti"].ToString();
            string shuma = rreshtDoku["txtShuma"].ToString();
            string objekti = rreshtDoku["txtObjekti"].ToString();
            if (grupi != "" && titulli != "" && kapitulli != "" && analiza != "" && artikulli != "")
            {
                //if (artikulli.StartsWith("231") && analiza.StartsWith("231") && kodprojekti == "")
                //{
                //    new Exception("Duhet te vendosni kodin e projektit!");
                //    return;
                //}
                if (grupi != null && grupi != "null" && grupi != "")
                {
                    clsKonfigUrdherPagese gr = new clsKonfigUrdherPagese(grupi, idndermarje, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Grup));
                    if (gr.Id == 0)
                        new Exception("Nje nga grupet nuk ekziston!");
                    IdGrupi = gr.Id;
                }
                if (titulli != null && titulli != "null" && titulli != "")
                {
                    clsKonfigUrdherPagese tit = new clsKonfigUrdherPagese(titulli, idndermarje, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Titull));
                    if (tit.Id == 0)
                        new Exception("Nje nga titujt nuk ekziston!");
                    idTitulli = tit.Id;
                }
                if (kapitulli != null && kapitulli != "null" && kapitulli != "")
                {
                    clsKonfigUrdherPagese kap = new clsKonfigUrdherPagese(kapitulli, idndermarje, Convert.ToInt32(DbCore.DbArkaBanka.LlojeKonfigurimeUrdherPagese.Kapitull));
                    if (kap.Id == 0)
                        new Exception("Nje nga kapitujt nuk ekziston!");
                    idKapitulli = kap.Id;
                }
                if (artikulli != null && artikulli != "null" && artikulli != "")
                {
                    DbCore.DbKontabiliteti.clsLlogari llog = new DbCore.DbKontabiliteti.clsLlogari(artikulli, idndermarje);
                    if (llog.IdLlogari == 0)
                        new Exception("Nje nga llogarite nuk ekziston!");
                    if (!llog.Aktiv)
                        new Exception("Nje nga llogarite nuk eshte aktive!");
                    IdLlogArtikulli = llog.IdLlogari;
                }
                if (analiza != null && analiza != "null" && analiza != "")
                {
                    DbCore.DbKontabiliteti.clsLlogari llog = new DbCore.DbKontabiliteti.clsLlogari(analiza, idndermarje);
                    if (llog.IdLlogari == 0)
                        new Exception("Nje nga llogarite nuk ekziston!");
                    if (!llog.Aktiv)
                        new Exception("Nje nga llogarite nuk eshte aktive!");
                    IdLlogAnaliza = llog.IdLlogari;
                }

                if (kodprojekti != null && kodprojekti != "null")
                    KodProjekti = kodprojekti;
                if (shuma != null && shuma != "null" && shuma != "")
                    Shuma = decimal.Parse(shuma);
                if (objekti != null && objekti != "null")
                    Objekti = objekti;
            }
        }

        public clsTrupiUrdherPagese(DataRow rreshti)
        {
            
            mbushTrupUrdherPagese(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se dokumentit te Urdherpageses.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne  e grupit
        /// </summary>
        public int IdGrupi
        {
            get { return idGrupi; }
            set { idGrupi = value; }
        }
        /// <summary>
        /// kthen vendos id e titullit
        /// </summary>
        public int IdTitulli
        {
            get
            {
                return idTitulli;
            }
            set
            {
                idTitulli = value;
            }
        }
        /// <summary>
        /// kthen /vendos id e kapitullit
        /// </summary>
        public int IdKapitulli
        {
            get
            {
                return idKapitulli;
            }
            set
            {
                idKapitulli = value;
            }
        }
        /// <summary>
        /// kthen vendos idllogari si artikull
        /// </summary>
        public int IdLlogArtikulli
        {
            get
            {
                return idLlogArtikulli;
            }
            set
            {
                idLlogArtikulli = value;
            }
        }
        /// <summary>
        /// kthen vendos id llogari si analize
        /// </summary>
        public int IdLlogAnaliza
        {
            get
            {
                return idLlogAnaliza;
            }
            set
            {
                idLlogAnaliza = value;
            }
        }
        /// <summary>
        /// kthen/ vendos kod projektin
        /// </summary>
        public string KodProjekti
        {
            get
            {
                return kodProjekti;
            }
            set
            {
                kodProjekti = value;
            }
        }
        /// <summary>
        /// kthen vendos objektin
        /// </summary>
        public string Objekti
        {
            get
            {
                return objekti;
            }
            set
            {
                objekti = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos paguar.
        /// </summary>
        public decimal Shuma
        {
            get { return shuma; }
            set { shuma = value; }
        }

        #endregion

        #region Metoda publike

        /// <summary>
        /// ruan objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.ruajTrupiUrdherPagese"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            using (clsDatabaseArkaBanka data = new clsDatabaseArkaBanka())
            {
                int id;
                clsMesazh u_ruajt = data.ruajTrupiUrdherPagese(out id, IdKoka, IdGrupi, idTitulli, idKapitulli, idLlogArtikulli, IdLlogAnaliza, kodProjekti, Shuma, objekti);
                IdTrupi = id;
                data.Dispose();
                return u_ruajt;
            }
        }

        /// <summary>
        /// Modifikon objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.modifikoTrupiUrdherPagese"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();

            clsMesazh u_modifikua = data.modifikoTrupiUrdherPagese(IdTrupi, IdKoka, IdGrupi, idTitulli, idKapitulli, idLlogArtikulli, IdLlogAnaliza, kodProjekti, Shuma, objekti);
            data.Dispose();
            return u_modifikua;

        }

        /// <summary>
        /// Fshin objektin e  trupit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.fshiTrupiUrdherPageseSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseArkaBanka data = new clsDatabaseArkaBanka();

            clsMesazh u_fshi = data.fshiTrupiUrdherPageseSipasID(IdTrupi);
            data.Dispose();
            return u_fshi;

        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit sipas id se kokes nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.ktheGjitheTrupiShitjeNgaKoka"/> 
        /// </summary>
        /// <returns > nje objekt colTrupiShitje me te gjithe trupat e nje dokumenti</returns>
        public colTrupiUrdherPagese merriSipasKoka()
        {
            colTrupiUrdherPagese data = new colTrupiUrdherPagese();
            data.mbushTrupiUrdherPagese(IdKoka, null);
            return data;
        }

        /// <summary>
        /// Merr objektin e  trupit se dokumentit sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseArkaBanka.ktheTrupiSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsTrupiShitje me trupin e dokumentit te kerkuar</returns>
        public clsTrupiUrdherPagese merriSipasID()
        {
            clsTrupiUrdherPagese data = new clsTrupiUrdherPagese(IdTrupi);
            return data;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e shitjes nga databaza
        /// </summary>
        /// <param name="dbDataRowTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupUrdherPagese(DataRow dbDataRowTrup)
        {
            if (dbDataRowTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrup["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowTrup["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowTrup["IDGRUPI"].ToString(), out idGrupi);
                    int.TryParse(dbDataRowTrup["IDTITULLI"].ToString(), out idTitulli);
                    int.TryParse(dbDataRowTrup["IDKAPITULLI"].ToString(), out idKapitulli);
                    int.TryParse(dbDataRowTrup["IDLLOGARTIKULLI"].ToString(), out idLlogArtikulli);
                    int.TryParse(dbDataRowTrup["IDLLOGANALIZA"].ToString(), out idLlogAnaliza);
                    kodProjekti = dbDataRowTrup["KODPROJEKTI"].ToString();
                    decimal.TryParse(dbDataRowTrup["SHUMA"].ToString(), out shuma);
                    objekti = dbDataRowTrup["OBJEKTI"].ToString();
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
