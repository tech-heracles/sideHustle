using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje konfigurim
    ///  te nje modeli per fushat shtese.
    ///  (Te dhenat  merren nga tabela : T_MODELIFUSHASHTESE)
    /// </summary>
    public class clsModeliFushaShtese
    {
        #region Atributet

        private int idModeliFushaShtese;
        private string kodiModeliFushaShtese;
        private string pershkrimModeliFushaShtese;
        private int idLlojModeliFushaShtese;
        private int idPerdoruesi;
        private int idNdermarje;
        private string idNivelAutorizimi;
        private colFushatShtese colFushat;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        private colLidhjetAutorizim lidhjeAutorizim;

        #endregion Atributet

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>

        public clsModeliFushaShtese(int idmodeliFushaShtese, string kodimodeliFushaShtese, String pershkrimmodeliFushaShtese, int idllojmodeliFushaShtese, int idperdoruesi, int idndermarje, int idstatusdok, string autorizimi, colFushatShtese colFushat)
        {
            idModeliFushaShtese = idmodeliFushaShtese;
            kodiModeliFushaShtese = kodimodeliFushaShtese;
            pershkrimModeliFushaShtese = pershkrimmodeliFushaShtese;
            idLlojModeliFushaShtese = idllojmodeliFushaShtese;
            idPerdoruesi = idperdoruesi;
            idStatusDok = idstatusdok;
            idNdermarje = idndermarje;
            idNivelAutorizimi = autorizimi;
            this.colFushat = colFushat;

            lidhjeAutorizim = string.IsNullOrEmpty(autorizimi) ? new colLidhjetAutorizim() : new colLidhjetAutorizim(IdNivelAutorizimi.Split(','), 13, IdModeliFushaShtese);

        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>

        public clsModeliFushaShtese(String kodi, int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModeliFusheShtese(data.ktheModelinFushaShteseSipasKodit(kodi, idndermarje));
            data.Dispose();
        }

        public clsModeliFushaShtese(int id)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushModeliFusheShtese(data.merrModeletFushaShtese(id));
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsModeliFushaShtese()
        {
        }

        public clsModeliFushaShtese(DataRow rreshti)
        {
            
            mbushModeliFusheShtese(rreshti);
        }

        #endregion Konstruktoret

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdModeliFushaShtese
        {
            get
            {
                return idModeliFushaShtese;
            }
            set
            {
                idModeliFushaShtese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin qe i caktohet ketij modeli te fushave shtese.
        /// </summary>
        public string KodiModeliFushaShtese
        {
            get
            {
                return kodiModeliFushaShtese;
            }
            set
            {
                kodiModeliFushaShtese = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin qe i caktohet ketij modeli te fushave shtese.
        /// </summary>
        public String PershkrimModeliFushaShtese
        {
            get { return pershkrimModeliFushaShtese; }
            set { pershkrimModeliFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llojit pte ketij modeli. Kjo ID i referohet tabekes T_LLOJMODELIFUSHASHTESE
        /// </summary>
        public int IdLlojModeliFushaShtese
        {
            get { return idLlojModeliFushaShtese; }
            set { idLlojModeliFushaShtese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe e ka krijuar kete model.
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
        /// Kthen/Vendos ID-ne e ndermarrjes qe i perket ky model i fushave shtese.
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos nivelet e autorizimeve
        /// </summary>
        public string IdNivelAutorizimi
        {
            get
            {
                return idNivelAutorizimi;
            }
            set
            {
                idNivelAutorizimi = value;
            }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbAdmin.clsFushaShtese"/> . Cdo objekt
        /// i modelit te fushave shtese, mban nje bashkesi me fusha shtese.
        /// </summary>
        public colFushatShtese ColFushat
        {
            get
            {
                return colFushat;
            }
            set
            {
                colFushat = value;
            }
        }

        public colLidhjetAutorizim LidhjeAutorizim
        {
            get { return lidhjeAutorizim; }
            set { lidhjeAutorizim = value; }
        }

        #endregion Properties

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e modelit te fushes shtese ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            try
            {
                using (var scope = new MyTransactionScope())
                {
                    clsMesazh u_ruajt = ruajModelinAndFushatShtese();
                    if (u_ruajt)
                        scope.Complete();
                    return u_ruajt;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e modelit te fushes shtese ne tabelen perkatese ne databaze. Theret funksionin
        /// </summary>
        public clsMesazh modifiko()
        {
            try
            {
                using (var scope = new MyTransactionScope())
                {

                    clsMesazh u_ruajt = modifikoModelinAndFushatShtesev2();
                    if (u_ruajt)
                        scope.Complete();
                    return u_ruajt;
                }
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e modelit te fushes shtese nga tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiModeliFushaShteseStatus(idModeliFushaShtese, idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Kthen nje collection me objekte te tipit  <see cref="DbCore.DbAdmin.clsModeliFushaShtese"/>
        /// </summary>
        public colModeletFushaShtese merriTeGjithe()
        {
            colModeletFushaShtese data = new colModeletFushaShtese(IdNdermarje, idPerdoruesi);
            return data;
        }

        public clsMesazh ruajModelinAndFushatShtese()
        {
            try
            {

                clsMesazh mesazh;
                mesazh = ekzistonModelMeKeteKod();

                if (!mesazh) return mesazh;
                mesazh = ruajModelinPaFushat();
                if (!mesazh) return mesazh;

                //ruan lidhjen me autorizimet
                if (LidhjeAutorizim.Count > 0)
                {
                    LidhjeAutorizim.ForEach(x => x.IdLidhese = IdModeliFushaShtese);
                    mesazh = lidhjeAutorizim.Ruaj();
                    if (!mesazh) return mesazh;
                }
                //vendos id e modelit per te gjithe fushat shtese
                ColFushat.ForEach(x => x.IdModeliFushaShtese = IdModeliFushaShtese);

                mesazh = ColFushat.Ruaj();
                if (!mesazh) return mesazh;

                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                ImbLogger.Error(ce);
                return new clsMesazh(false, ce.Message);
            }
        }

        public clsMesazh modifikoModelinAndFushatShtesev2()
        {//transaksioni per te modifikuar edhe modelin dhe fushat shtese
            try
            {
                clsMesazh mesazh = ModifikoModelinPaFushat();
                if (!mesazh) return mesazh;

                var lidhjeAutorizimOld = new colLidhjetAutorizim(IdModeliFushaShtese, 13);
                LidhjeAutorizim.ForEach(x => x.IdLidhese = IdModeliFushaShtese);

                mesazh = colLidhjetAutorizim.Modifiko(lidhjeAutorizimOld, LidhjeAutorizim);
                if (!mesazh) return mesazh;

                //vendos id e modelit per te gjithe fushat shtese
                ColFushat.ForEach(x => x.IdModeliFushaShtese = IdModeliFushaShtese);
                var fushatOld = new colFushatShtese(IdModeliFushaShtese);

                mesazh = colFushatShtese.Modifiko(ColFushat, fushatOld);
                if (!mesazh) return mesazh;

                mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                return mesazh;
            }
            catch (Exception ce)
            {
                ImbLogger.Error(ce);
                return new clsMesazh(false, ce.Message);
            }
        }

        public static bool kaveprime(int id)
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.kaVeprimeModelFushaShtese(id);
            }
        }

        public static string MerrFushaVisibleModeli(int idModeli, int gjuha)
        {
            return MerrFushaModeli(idModeli, gjuha)[0];
        }
        public static string[] MerrFushaModeli(int idModeli, int gjuha)
        {
            string kolonatVisible = "";
            string kolonaJoVisible = "";
            using (var db = new clsDatabaseAdmin())
            {
                var dt = db.MerrFushaModeli(idModeli, gjuha);
                if (dt.Rows.Count == 0) throw new MyException($"Nuk ka asnje fushe per modelin {idModeli}");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["VISIBLE"]))
                    {
                        if (string.IsNullOrEmpty(kolonatVisible))
                        {
                            kolonatVisible = string.Format("[{0}]", dt.Rows[i]["FUSHA"].ToString());
                            continue;
                        }

                        kolonatVisible = string.Format("{0},[{1}]", kolonatVisible, dt.Rows[i]["FUSHA"]);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(kolonaJoVisible))
                        {
                            kolonaJoVisible = string.Format("[{0}]", dt.Rows[i]["FUSHA"].ToString());
                            continue;
                        }
                        kolonaJoVisible = string.Format("{0},[{1}]", kolonaJoVisible, dt.Rows[i]["FUSHA"]);
                    }
                }
                return new string[] { kolonatVisible, kolonaJoVisible };
            }
        }
        #endregion Metoda Publike

        #region private

        private clsMesazh ruajModelinPaFushat()
        {
            int idMF = -1;
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                dbAdmin.ruajModeliFushaShtese(out idMF, KodiModeliFushaShtese, PershkrimModeliFushaShtese, IdLlojModeliFushaShtese, IdPerdoruesi, IdNdermarje, idStatusDok);
                IdModeliFushaShtese = idMF;
                return new clsMesazh(true, "Modeli u ruajt me sukses");
            }
        }

        private clsMesazh ModifikoModelinPaFushat()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.modifikoModeliFushaShtese(IdModeliFushaShtese, KodiModeliFushaShtese, PershkrimModeliFushaShtese, IdLlojModeliFushaShtese, IdPerdoruesi, IdNdermarje, idStatusDok);
            }
        }

        private clsMesazh ekzistonModelMeKeteKod()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                if (dbAdmin.ekzistonModelFushaShtese(KodiModeliFushaShtese, IdNdermarje))
                    return new clsMesazh(false, "Ekziston nje model me kete kod!");
                return new clsMesazh(true);
            }
        }

        #endregion private

        #region Metoda Internal

        internal bool mbushModeliFusheShtese(DataRow dbDataRowModeliFusheShtese)
        {
            if (dbDataRowModeliFusheShtese != null)
            {
                try
                {
                    int.TryParse(dbDataRowModeliFusheShtese["IDMODELIFUSHASHTESE"].ToString(), out idModeliFushaShtese);
                    kodiModeliFushaShtese = dbDataRowModeliFusheShtese["KODIMODELIFUSHASHTESE"].ToString();
                    pershkrimModeliFushaShtese = dbDataRowModeliFusheShtese["PERSHKRIMMODELIFUSHASHTESE"].ToString();
                    int.TryParse(dbDataRowModeliFusheShtese["IDLLOJMODELIFUSHASHTESE"].ToString(), out idLlojModeliFushaShtese);
                    int.TryParse(dbDataRowModeliFusheShtese["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowModeliFusheShtese["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowModeliFusheShtese["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowModeliFusheShtese["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowModeliFusheShtese["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    idNivelAutorizimi = "";
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se modelit te fushes shtese nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion Metoda Internal
    }
}