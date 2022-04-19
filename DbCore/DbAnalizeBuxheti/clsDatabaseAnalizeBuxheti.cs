using System;
using System.Collections.Generic;
using System.Data;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsDatabaseAnalizeBuxheti : DbData
    {
        #region konstruktoret

        public clsDatabaseAnalizeBuxheti()
            : base()
        { }

        public clsDatabaseAnalizeBuxheti(DbData db)
            : base(db)
        {
        }
        public clsDatabaseAnalizeBuxheti(string connectionName) : base(connectionName)
        {

        }
        #endregion konstruktoret

        #region PERMBLEDHESE E P/BUXHETIT PER VITIN PASARDHES

        /// <summary>
        /// merr te gjitha rreshtat e projekt buxhetit permbledhes per vitin pasardhes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsPBuxhetPermbledhes</returns>
        internal IEnumerable<clsPBuxhetPermbledhes> MerrPBuxhetPermbledhes(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PBUXHET_PERMBLEDHES_merrSipasNdermarrjes", clsPBuxhetPermbledhes.Krijo);
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pbBuxhetID"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokPBuxhetPermbledhes(int pbBuxhetID, int idModifikuesi, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@PBUXHETID", pbBuxhetID, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHET_PERMBLEDHES_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pbBuxhetId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="pagat"></param>
        /// <param name="fondiVecante"></param>
        /// <param name="sigurimeShoqerore"></param>
        /// <param name="mallRadheSherbime"></param>
        /// <param name="subvencione"></param>
        /// <param name="transferimKorrBrendshme"></param>
        /// <param name="transferimKorrHuaja"></param>
        /// <param name="shpenzimeKapitalePaTrup"></param>
        /// <param name="shpenzimeKapitaleTrup"></param>
        /// <param name="transfertaKapitale"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajPBuxhetPermbledhes(out int pbBuxhetId, int rreshtiId, decimal pagat, decimal fondiVecante, decimal sigurimeShoqerore, decimal mallRadheSherbime, decimal subvencione, decimal transferimKorrBrendshme, decimal transferimKorrHuaja, decimal shpenzimeKapitalePaTrup, decimal shpenzimeKapitaleTrup, decimal transfertaKapitale, int idNdermarrje, int IdModifikuesi, int IdNdermVit)
        {
            pbBuxhetId = -1;
            dbManager.Open();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@PBUXHETID", pbBuxhetId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PAGAT", pagat, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FONDIVECANTE", fondiVecante, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SIGURIMESHOQERORE", sigurimeShoqerore, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MALLRADHESHERBIME", mallRadheSherbime, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SUBVENCIONIME", subvencione, ParameterDirection.Input);
            dbManager.AddParameters(7, "@TRANSFERIMKORRBRENDSHME", transferimKorrBrendshme, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TRANSFERIMKORRHUAJA", transferimKorrHuaja, ParameterDirection.Input);
            dbManager.AddParameters(9, "@SHPENZIMEKAPITALEPATRUP", shpenzimeKapitalePaTrup, ParameterDirection.Input);
            dbManager.AddParameters(10, "@SHPENZIMEKAPITALETRUP", shpenzimeKapitaleTrup, ParameterDirection.Input);
            dbManager.AddParameters(11, "@TRANSFERTAKAPITALE", transfertaKapitale, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHET_PERMBLEDHES_insert");
            pbBuxhetId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

    


        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e projektbuxhetit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultPBuxhetPermbledhes(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHET_PERMBLEDHES_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i projektbuxhetit permbledhes u krye me sukses!");
        }



        internal clsMesazh fshiRreshtNgaPbPermbledhes(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHET_PERMBLEDHES_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        #endregion PERMBLEDHESE E P/BUXHETIT PER VITIN PASARDHES

        #region PARASHIKIMI I TE ARDHURAVE TE VETA TE MINISTRIVE DHE INSTITUCIONEVE BUXHETORE

        /// <summary>
        /// merr te gjitha rreshtat e parashikimit te te ardhurave te veta te ministrive
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsParashikimiTeArdhura</returns>
        internal IEnumerable<clsParashikimiTeArdhura> MerrParashikimiTeArdhura(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PARASHIKIMI_TE_ARDHURA_merrSipasNdermarrjes", clsParashikimiTeArdhura.Krijo);
        }



        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e parashikimit te te ardhurave
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultParashikimiTeArdhura(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIMI_TE_ARDHURA_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i parashikimit te te ardhurave u krye me sukses!");
        }


        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pTaId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokParashikimiTeArdhura(int pTaID, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PTAID", pTaID, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIMI_TE_ARDHURA_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pTaId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="teArdhuraTotaleParardhes"></param>
        /// <param name="iTakojneInstitucionitAktuale"></param>
        /// <param name="derdhenNeBuxhetAktuale"></param>
        /// <param name="iTakojneInstitucionitPasardhes"></param>
        /// <param name="derdhenNeBuxhetPasardhes"></param>
        /// <param name="parashikimiPlus2"></param>
        /// <param name="parashikimiPlus3"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <param name="IdNdermVit"></param>
        /// <returns></returns>
        public clsMesazh RuajParashikimiTeArdhura(out int pTaId, int rreshtiId, decimal teArdhuraTotaleParardhes, decimal iTakojneInstitucionitAktuale, decimal derdhenNeBuxhetAktuale, decimal iTakojneInstitucionitPasardhes, decimal derdhenNeBuxhetPasardhes, decimal parashikimiPlus2, decimal parashikimiPlus3, int idNdermarrje, int IdModifikuesi,int IdNdermVit)
        {
            pTaId = -1;
            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@PTAID", pTaId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TE_ARDHURATOTALE_PARAARDHES", teArdhuraTotaleParardhes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ITAKOJNE_INSTITUCIONIT_AKTUALE", iTakojneInstitucionitAktuale, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DERDHEN_NE_BUXHET_AKTUALE", derdhenNeBuxhetAktuale, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ITAKOJNE_INSTITUCIONIT_PASARDHES", iTakojneInstitucionitPasardhes, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DERDHEN_NE_BUXHET_PASARDHES", derdhenNeBuxhetPasardhes, ParameterDirection.Input);
            dbManager.AddParameters(7, "@PARASHIKIMI_PLUS2", parashikimiPlus2, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PARASHIKIMI_PLUS3", parashikimiPlus3, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIMI_TE_ARDHURA_insert");
            pTaId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaParashikimTeArdhura(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIMI_TE_ARDHURA_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        #endregion PARASHIKIMI I TE ARDHURAVE TE VETA TE MINISTRIVE DHE INSTITUCIONEVE BUXHETORE

        #region PARASHIKIMI I SHPENZIMEVE PER PERSONELIN PER VITIN PASARDHES CONFIG

        /// <summary>
        /// merr te gjitha rreshtat e Parashikimit te Shpenzimeve te personelit config
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsParashikimShpenzPersoneliConfig</returns>
        internal IEnumerable<clsParashikimShpenzPersoneliConfig> MerrParashikimShpenzPersoneliConfig(int idNdermarrje, bool prind)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            string sp = (!prind) ? "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_merrSipasNdermarrjes" 
                              : "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_merrPrindSipasNdermarrjes";
            return dbManager.GetIEnumerbale(sp, clsParashikimShpenzPersoneliConfig.Krijo);
        }
      

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e konfigurimit te parashikimit te shpenzimeve te personelit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultParashikimShpenzPersoneliConfig(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i konfigurimit te parashikimit te shpenzimeve te personelit u krye me sukses!");
        }


        internal clsMesazh updateParashikimShpenzPersoneliConfig(int PShPConfigId, int RreshtiId, string KlasaKategoria, decimal PagaBaze, decimal PagaPozicion, int IdNdermarrje, int IdModifikuesi, int IdNdermVit, bool prind, int idPrindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@PSHPKONFIGID", PShPConfigId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@RRESHTIID", RreshtiId, ParameterDirection.Input);
            if (KlasaKategoria != null)
                dbManager.AddParameters(2, "@KLASA_KATEGORIA", KlasaKategoria, ParameterDirection.Input);
            else
                dbManager.AddParameters(2, "@KLASA_KATEGORIA", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGA_BAZE", PagaBaze, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGA_POZICION", PagaPozicion, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDMODIFIKUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PRIND", prind, ParameterDirection.Input);
            if (idPrindi <= 0)
                dbManager.AddParameters(9, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(9, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_update");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pShPConfigId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokParashikimShpenzPersoneliConfig(int pShPConfigId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PSHPCONFIGID", pShPConfigId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pShPConfigId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="klasaKategoria"></param>
        /// <param name="pagaBaze"></param>
        /// <param name="pagaPozicion"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajParashikimShpenzPersoneliConfig(out int pShPConfigId, int rreshtiId, string klasaKategoria, decimal pagaBaze, decimal pagaPozicion, int idNdermarrje, int IdModifikuesi, int IdndermVit, bool prind, int idPrindi)
        {
            pShPConfigId = -1;
            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@PSHPKONFIGID", pShPConfigId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            if (klasaKategoria != null)
                dbManager.AddParameters(2, "@KLASA_KATEGORIA", klasaKategoria, ParameterDirection.Input);
            else
                dbManager.AddParameters(2, "@KLASA_KATEGORIA", DBNull.Value, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PAGA_BAZE", pagaBaze, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PAGA_POZICION", pagaPozicion, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdndermVit, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PRIND", prind, ParameterDirection.Input);
            if (idPrindi <= 0)
                dbManager.AddParameters(9, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(9, "@IDPRINDI", idPrindi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_insert");
            pShPConfigId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaParashikimShpenzKonfig(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal bool KaVeprimeMeKeteParashikimShpenzimi(int rreshtiId)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRRESHTI", rreshtiId, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_kaVeprime"));

            return pergjigja == 1;
        }
        internal bool EshtePerdorurKyZePrind(int rreshtiId)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRRESHTI", rreshtiId, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_KONFIG_eshtePerdorurPrindi"));

            return pergjigja > 0;
        }
        #endregion PARASHIKIMI I SHPENZIMEVE PER PERSONELIN PER VITIN PASARDHES CONFIG

        #region PARASHIKIMET PER SHPENZIME KAPITALE PER 3 VITE

        /// <summary>
        /// merr te gjitha rreshtat e parashikimit te shpenzimeve kapitale
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsShpenzimeKapitale</returns>
        internal IEnumerable<clsShpenzimeKapitale> MerrShpenzimeKapitale(int idNdermarrje,int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_SHPENZIME_KAPITALE_merrSipasNdermarrjes", clsShpenzimeKapitale.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e parashikimit te shpenzimeve kapitale
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultShpenzimeKapitale(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_KAPITALE_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i parashikimit te shpenzimeve kapitale u krye me sukses!");
        }

        internal clsMesazh fshiRreshtNgaShpenzimeKapitale(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_KAPITALE_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="shKId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokShpenzimeKapitale(int shKId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@SHKID", shKId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_KAPITALE_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="shKId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="totalParardhes"></param>
        /// <param name="pritshmiAktual"></param>
        /// <param name="shpenzKapitalePatrupezuarArdhme"></param>
        /// <param name="shpenzKapitaleTrupezuarArdhme"></param>
        /// <param name="transferimeKapitalArdhme"></param>
        /// <param name="derdhenNeBuxhetPasardhes"></param>
        /// <param name="parashikimTotaliPlus2"></param>
        /// <param name="parashikimTotaliPlus3"></param>
        /// <param name="totaliArdhme"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajShpenzimeKapitale(out int shKId, int rreshtiId, decimal totalParardhes,
            decimal pritshmiAktual, decimal shpenzKapitalePatrupezuarArdhme, decimal shpenzKapitaleTrupezuarArdhme,
            decimal transferimeKapitalArdhme, decimal totaliArdhme, decimal parashikimTotaliPlus2, decimal parashikimTotaliPlus3, int idNdermarrje, int IdModifikuesi, int IdNdermVit)
        {
            shKId = -1;
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@SHKID", shKId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@TOTAL_PARARDHES", totalParardhes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PRITSHMI_AKTUAL", pritshmiAktual, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHPENZ_KAPITALE_PATRUPEZUAR_ARDHME", shpenzKapitalePatrupezuarArdhme, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHPENZ_KAPITALE_TRUPEZUAR_ARDHME", shpenzKapitaleTrupezuarArdhme, ParameterDirection.Input);
            dbManager.AddParameters(6, "@TRANSFERIM_KAPITAL_ARDHME", transferimeKapitalArdhme, ParameterDirection.Input);
            dbManager.AddParameters(7, "@TOTALI_ARDHME", totaliArdhme, ParameterDirection.Input);
            dbManager.AddParameters(8, "@PARASHIKIM_TOTALI_PLUS2", parashikimTotaliPlus2, ParameterDirection.Input);
            dbManager.AddParameters(9, "@PARASHIKIM_TOTALI_PLUS3", parashikimTotaliPlus3, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_KAPITALE_insert");
            shKId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        #endregion PARASHIKIMET PER SHPENZIME KAPITALE PER 3 VITE

        #region PROJEKT BUXHETI PER 3 VITE

        /// <summary>
        /// merr te gjitha rreshtat e projekt buxhetit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsPBuxhet3Vjecar</returns>
        internal IEnumerable<clsPBuxheti3Vjecar> MerrPBuxheti3Vjecar(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PBUXHETI_3VJECAR_merrSipasNdermarrjes", clsPBuxheti3Vjecar.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e projekt buxhetit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultPBuxheti3Vjecar(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHETI_3VJECAR_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i projekt buxhetit per 3 vitet ne vazhdim u krye me sukses!");
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pB3VID"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokPBuxheti3Vjecar(int pB3VID, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PB3VID", pB3VID, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHETI_3VJECAR_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pB3VId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="buxhetiParaardhes"></param>
        /// <param name="teArdhuratParardhes"></param>
        /// <param name="buxhetiAktual"></param>
        /// <param name="teArdhuratAktual"></param>
        /// <param name="buxhetiPlus1"></param>
        /// <param name="teArdhuratPlus1"></param>
        /// <param name="buxhetiPlus2"></param>
        /// <param name="teArdhuratPlus2"></param>
        /// <param name="buxhetiPlus3"></param>
        /// <param name="teArdhuratPlus3"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajPBuxheti3Vjecar(out int pB3VId, int rreshtiId, decimal buxhetiParaardhes,
            decimal teArdhuratParardhes, decimal buxhetiAktual, decimal teArdhuratAktual,
            decimal buxhetiPlus1, decimal teArdhuratPlus1, decimal buxhetiPlus2, decimal teArdhuratPlus2,
            decimal buxhetiPlus3, decimal teArdhuratPlus3, int idNdermarrje, int IdModifikuesi, int IdNdermVit)
        {
            pB3VId = -1;
            dbManager.Open();
            dbManager.CreateParameters(15);
            dbManager.AddParameters(0, "@PB3VID", pB3VId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@BUXHETI_PARAARDHES", buxhetiParaardhes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@TEARDHURAT_PARAARDHES", teArdhuratParardhes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@BUXHETI_AKTUAL", buxhetiAktual, ParameterDirection.Input);
            dbManager.AddParameters(5, "@TEARDHURAT_AKTUAL", teArdhuratAktual, ParameterDirection.Input);
            dbManager.AddParameters(6, "@BUXHETI_PLUS1", buxhetiPlus1, ParameterDirection.Input);
            dbManager.AddParameters(7, "@TEARDHURAT_PLUS1", teArdhuratPlus1, ParameterDirection.Input);
            dbManager.AddParameters(8, "@BUXHETI_PLUS2", buxhetiPlus2, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TEARDHURAT_PLUS2", teArdhuratPlus2, ParameterDirection.Input);
            dbManager.AddParameters(10, "@BUXHETI_PLUS3", buxhetiPlus3, ParameterDirection.Input);
            dbManager.AddParameters(11, "@TEARDHURAT_PLUS3", teArdhuratPlus3, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(13, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(14, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHETI_3VJECAR_insert");
            pB3VId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaPbuxhet3Vjecar(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PBUXHETI_3VJECAR_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        #endregion PROJEKT BUXHETI PER 3 VITE

        #region Rreshta Ambjenti

        /// <summary>
        /// modifikon direkt rreshtin per ambjentin e zgjedhur
        /// </summary>
        /// <param name="rreshtiId"></param>
        /// <param name="KodiRreshtit"></param>
        /// <param name="PershkrimiRreshtit"></param>
        /// <param name="IdAmbjenti"></param>
        /// <param name="IdNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        internal clsMesazh ModifikoRreshtaAmbjenti(int rreshtiId, string KodiRreshtit, string PershkrimiRreshtit, decimal IdAmbjenti, int IdNdermarrje, int IdModifikuesi, string NjesiaMatese, int IdNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI_RRESHTIT", KodiRreshtit, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI_RRESHTIT", PershkrimiRreshtit, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDAMBJENTI", IdAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDMODIFIKUESI", IdModifikuesi, ParameterDirection.Input);
            if (NjesiaMatese == null)
                dbManager.AddParameters(6, "@NJESIAMATESE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@NJESIAMATESE", NjesiaMatese, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_Rreshta_Ambjenti_update");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// merr te gjitha rreshtat e ambjentit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsRreshtaAmbjenti</returns>
        internal IEnumerable<clsRreshtaAmbjenti> MerrRreshtaAmbjenti(int idNdermarrje, int idAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMBJENTI", idAmbjenti, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_RRESHTA_AMBJENTI_merrSipasNdermarrjesDheAmbjentit", clsRreshtaAmbjenti.Krijo);
        }

        /// <summary>
        /// ruan rreshtin e ri
        /// </summary>
        /// <param name="RreshtiId"></param>
        /// <param name="KodiRreshtit"></param>
        /// <param name="PershkrimiRreshtit"></param>
        /// <param name="IdAmbjenti"></param>
        /// <param name="IdNdermarrje"></param>
        /// <param name="idKrijuesi"></param>
        /// <returns></returns>
        internal clsMesazh RuajRreshtaAmbjenti(out int RreshtiId, string KodiRreshtit, string PershkrimiRreshtit, int IdAmbjenti, int IdNdermarrje, int idKrijuesi, string njesiaMatese, int IdNdermVit)
        {
            RreshtiId = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@RRESHTIID", RreshtiId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI_RRESHTIT", KodiRreshtit, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI_RRESHTIT", PershkrimiRreshtit, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDAMBJENTI", IdAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            if(njesiaMatese == null)
                dbManager.AddParameters(6, "@NJESIAMATESE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@NJESIAMATESE", njesiaMatese, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_Rreshta_Ambjenti_insert");
            RreshtiId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// fshin rreshtin
        /// </summary>
        /// <param name="rreshtiId"></param>
        /// <returns></returns>
        internal clsMesazh FshiRreshtaAmbjenti(int rreshtiId)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_Rreshta_Ambjenti_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        /// <summary>
        /// kotnrollon nese rreshti i dhene eshte perdorur ne ambjentin e dhene
        /// </summary>
        /// <param name="idAmbjenti"></param>
        /// <param name="idRreshti"></param>
        /// <returns></returns>
        internal bool KaVeprimeMeKeteRresht(int idAmbjenti, int idRreshti)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDRRESHTI", idRreshti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMBJENTI", idAmbjenti, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_Rreshta_Ambjenti_KaVeprime"));

            return pergjigja == 1;
        }

        public bool EkzistonKyKodRreshtaAmbjenti(string kodi, int idAmbjenti, int idNdermarrje)
        {
            int count;

            dbManager.Open();

            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODI_RRESHTIT", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMBJENTI", idAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            count = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_Rreshta_Ambjenti_EkzistonKyKod"));

            return count > 0;
        }

        internal void MerrRreshtAmbjenti(int idRreshti, clsRreshtaAmbjenti rresht)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRRESHTI", idRreshti, ParameterDirection.Input);
            dbManager.FillObject<clsRreshtaAmbjenti>("prc_T_AB_RRESHTA_AMBJENTI_MerrSipasID", rresht.Mbush);
        }
        #endregion Rreshta Ambjenti

        #region AMBJENTI

        /// <summary>
        /// merr te gjithe ambjentet
        /// </summary>
        /// <returns>kthen nje liste me clsAmbjenti</returns>
        internal IEnumerable<clsAmbjenti> MerrAmbjentet()
        {
            dbManager.Open();
            return dbManager.GetIEnumerbale("prc_T_AB_AMBJENTI_select", clsAmbjenti.Krijo);
        }

        #endregion AMBJENTI

        #region PLANIFIKIMI I PRODUKTEVE TE PROGRAMIT NE SASI DHE VLERE

        /// <summary>
        /// merr te gjitha rreshtat e planifikimit te produkteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsPlanifikimiProdukteve</returns>
        internal IEnumerable<clsPlanifikimiProdukteve> MerrPlanifikimiProdukteve(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PLANIFIKIMI_PRODUKTEVE_merrSipasNdermarrjes", clsPlanifikimiProdukteve.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e planifikimit te produkteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultPlanifikimiProdukteve(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PLANIFIKIMI_PRODUKTEVE_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i planifikimit te produkteve u krye me sukses!");
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pPId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokPlanifikimiProdukteve(int pPId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PPID", pPId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PLANIFIKIMI_PRODUKTEVE_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pPId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="njesiaId"></param>
        /// <param name="sasiorPlus1"></param>
        /// <param name="vlerorPlus1"></param>
        /// <param name="sasiorPlus2"></param>
        /// <param name="vlerorPlus2"></param>
        /// <param name="sasiorPlus3"></param>
        /// <param name="vlerorPlus3"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajPlanifikimiProdukteve(out int pPId, int rreshtiId,
            decimal sasiorPlus1, decimal vlerorPlus1, decimal sasiorPlus2, decimal vlerorPlus2,
            decimal sasiorPlus3, decimal vlerorPlus3, int idNdermarrje, int IdModifikuesi, int IdNdermVit)
        {
            pPId = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@PPID", pPId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@SASIOR_PLUS1", sasiorPlus1, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLEROR_PLUS1", vlerorPlus1, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLEROR_PLUS2", vlerorPlus2, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SASIOR_PLUS2", sasiorPlus2, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VLEROR_PLUS3", vlerorPlus3, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SASIOR_PLUS3", sasiorPlus3, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PLANIFIKIMI_PRODUKTEVE_insert");
            pPId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaPlanifikimiProdukteve(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PLANIFIKIMI_PRODUKTEVE_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        #endregion PLANIFIKIMI I PRODUKTEVE TE PROGRAMIT NE SASI DHE VLERE

        #region SHPENZIME OPERATIVE KONFIG

        /// <summary>
        /// ruan nje kategori te re shpenzimi
        /// </summary>
        /// <param name="shokId"></param>
        /// <param name="Kodi"></param>
        /// <param name="Pershkrimi"></param>
        /// <param name="IdPrindi"></param>
        /// <param name="IdKrijuesi"></param>
        /// <param name="IdNdermarrje"></param>
        /// <param name="niveli"></param>
        /// <returns></returns>
        internal clsMesazh RuajShpenzimeOperativeKonfig(out int shokId, string Kodi, string Pershkrimi, int IdPrindi, int IdKrijuesi, int IdNdermarrje, int niveli, int IdNdermVit)
        {
            shokId = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@SHOKID", shokId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", Pershkrimi, ParameterDirection.Input);
            if (IdPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input); else dbManager.AddParameters(3, "@IDPRINDI", IdPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_insert");
            shokId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// modifikon nje kategori ekzistuse
        /// </summary>
        /// <param name="ShokId"></param>
        /// <param name="Kodi"></param>
        /// <param name="Pershkrimi"></param>
        /// <param name="IdPrindi"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        internal clsMesazh ModifikoShpenzimeOperativeKonfig(int ShokId, string Kodi, string Pershkrimi, int IdPrindi, int idNdermarrje, int IdModifikuesi, int niveli, int idNdermVIt)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@SHOKID", ShokId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", Pershkrimi, ParameterDirection.Input);
            if(IdPrindi==0)
                dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
            dbManager.AddParameters(3, "@IDPRINDI", IdPrindi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@IDMODIFIKUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", idNdermVIt, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_modifiko");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// fshin nje kategori ekzistuese
        /// </summary>
        /// <param name="ShokId"></param>
        /// <returns></returns>
        internal clsMesazh fshiKonfgurimShpenzimeOperativeUpdDel(int ShokId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@SHOKID", ShokId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void MbushOperativeKonfigSipasID(clsShpenzimeOperativeKonfig shpenzKonfig, int shokId)
        {/*
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOKID", shokId, ParameterDirection.Input);
            shpenzKonfig = dbManager.FillObject<clsShpenzimeOperativeKonfig>("prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_merrSipasID", clsShpenzimeOperativeKonfig.Krijo);
       */ }

        /// <summary>
        /// merr te gjitha konfigurimet e shpenzimeve operative
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsShpenzimeOperativeKonfig</returns>
        internal IEnumerable<clsShpenzimeOperativeKonfig> MerrShpenzimeOperativeKonfig(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_selectAll", clsShpenzimeOperativeKonfig.Krijo);
        }

        internal bool kaFemijShpenzimiOperativ(int shokID)
        {
            int pergjigja = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOKID", shokID, ParameterDirection.Input);
            pergjigja = Convert.ToInt16(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_KaFemijShpenzimi"));
            return pergjigja == 1;
        }
        internal bool KaRegjistrimeMeKeteShpenzimOperativ(int shokID)
        {

            int pergjigja = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOKID", shokID, ParameterDirection.Input);
            pergjigja = Convert.ToInt16(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_eshtePerdorurNeRregjistrim"));
            return pergjigja == 1;
        }
        internal bool KaPrindShpenzimiOperativ(int shokID)
        {
            int pergjigja = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOKID", shokID, ParameterDirection.Input);
            pergjigja = Convert.ToInt16(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_KaPrindShpenzimi"));
            return pergjigja == 1;
        }
        internal clsMesazh PerditesoNiveletEShpenzimeveOperative(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NEW", dt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_perditesoNivelet");
            return new clsMesazh(true, "te dhenat u modifikuan me sukses!");
        }

        internal bool KaVeprimeMeKeteShpenzim(int shokId)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOKID", shokId, ParameterDirection.Input);
            
            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_KaVeprime"));

            return pergjigja == 1;
        }

        public bool EkzistonKyKodShpenzimeOperative(string kodi, int idNdermarrje)
        {
            int count;

            dbManager.Open();

            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            count = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_EkzistonKyKod"));

            return count > 0;
        }

        #endregion SHPENZIME OPERATIVE KONFIG

        #region SHPENZIME OPERATIVE

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="shoId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokShpenzimeOperative(int shoId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@SHOID", shoId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="shoId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokShpenzimeOperativeSipasShokId(int shokId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@SHOKID", shokId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_updateDelSipasShokId");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
        

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="shoId"></param>
        /// <param name="shokId"></param>
        /// <param name="ngaBuxhetiParaardhes"></param>
        /// <param name="ngaTeArdhuratParaardhes"></param>
        /// <param name="totalParaardhes"></param>
        /// <param name="ngaBuxhetiAktuale"></param>
        /// <param name="ngaTeArdhuratAktuale"></param>
        /// <param name="totalAktuale"></param>
        /// <param name="njesiaArdhme"></param>
        /// <param name="kostoPerNjesiArdhme"></param>
        /// <param name="shpenzimePlanifikuarArdhme"></param>
        /// <param name="limitiArdhme"></param>
        /// <param name="kerkesaGjykatesArdhme"></param>
        /// <param name="diferencaKerkeseLimitArdhme"></param>
        /// <param name="shpenzimeTePlanifikuarTeArdhuraArdhme"></param>
        /// <param name="totalKerkesaTeArdhuraArdhme"></param>
        /// <param name="vlersimiZyresArdhme"></param>
        /// <param name="limitiArdhmePlus1"></param>
        /// <param name="kerkesaGjykatesArdhmePlus1"></param>
        /// <param name="diferencaKerkeseLimitArdhmePlus1"></param>
        /// <param name="vlersimiZyresArdhmePlus1"></param>
        /// <param name="limitiArdhmePlus2"></param>
        /// <param name="kerkesaGjykatesArdhmePlus2"></param>
        /// <param name="diferencaKerkeseLimitArdhmePlus2"></param>
        /// <param name="vlersimiZyresArdhmePlus2"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idModifikuesi"></param>
        /// <returns></returns>
        internal clsMesazh RuajShpenzimeOperative(out int shoId, int shokId, decimal ngaBuxhetiParaardhes, decimal ngaTeArdhuratParaardhes, decimal totalParaardhes,
            decimal njesiaAktuale, decimal ngaBuxhetiAktuale, decimal ngaTeArdhuratAktuale, decimal totalAktuale,
            decimal njesiaArdhme, decimal kostoPerNjesiArdhme, decimal shpenzimePlanifikuarArdhme, decimal limitiArdhme,
            decimal kerkesaGjykatesArdhme, decimal diferencaKerkeseLimitArdhme, decimal shpenzimeTePlanifikuarTeArdhuraArdhme,
            decimal totalKerkesaTeArdhuraArdhme, decimal vlersimiZyresArdhme, decimal limitiArdhmePlus1,
            decimal kerkesaGjykatesArdhmePlus1, decimal diferencaKerkeseLimitArdhmePlus1, decimal vlersimiZyresArdhmePlus1, decimal limitiArdhmePlus2,
            decimal kerkesaGjykatesArdhmePlus2, decimal diferencaKerkeseLimitArdhmePlus2, decimal vlersimiZyresArdhmePlus2, int idNdermarrje, int idModifikuesi,int kokaID, int idNdermVit)
        {
            shoId = -1;
            dbManager.Open();
            dbManager.CreateParameters(30);
            dbManager.AddParameters(0, "@SHOID", shoId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@SHOKID", shokId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NGABUXHETI_PARAARDHES", ngaBuxhetiParaardhes, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NGATEARDHURAT_PARARDHES", ngaTeArdhuratParaardhes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TOTAL_PARAARDHES", totalParaardhes, ParameterDirection.Input);
            if (njesiaAktuale == 0) dbManager.AddParameters(5, "@NJESIA_AKTUALE", DBNull.Value, ParameterDirection.Input);
            else  dbManager.AddParameters(5, "@NJESIA_AKTUALE", njesiaAktuale, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NGABUXHETI_AKTUALE", ngaBuxhetiAktuale, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NGATEARDHURAT_AKTUALE", ngaTeArdhuratAktuale, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TOTAL_AKTUALE", totalAktuale, ParameterDirection.Input);
            if (njesiaArdhme == 0) dbManager.AddParameters(9, "@NJESIA_ARDHME", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@NJESIA_ARDHME", njesiaArdhme, ParameterDirection.Input);
            dbManager.AddParameters(10, "@KOSTO_PERNJESI_ARDHME", kostoPerNjesiArdhme, ParameterDirection.Input);
            dbManager.AddParameters(11, "@SHPENZIME_PLANIFIKUAR_ARDHME", shpenzimePlanifikuarArdhme, ParameterDirection.Input);
            dbManager.AddParameters(12, "@LIMITI_ARDHME", limitiArdhme, ParameterDirection.Input);
            dbManager.AddParameters(13, "@KERKESA_GJYKATES_ARDHME", kerkesaGjykatesArdhme, ParameterDirection.Input);
            dbManager.AddParameters(14, "@DIFERENCA_KERKESE_LIMIT_ARDHME", diferencaKerkeseLimitArdhme, ParameterDirection.Input);
            dbManager.AddParameters(15, "@SHPENZIME_TEPLANIFIKUAR_TEARDHURA_ARDHME", shpenzimeTePlanifikuarTeArdhuraArdhme, ParameterDirection.Input);
            dbManager.AddParameters(16, "@TOTAL_KERKESA_TEARDHURA_ARDHME", totalKerkesaTeArdhuraArdhme, ParameterDirection.Input);
            dbManager.AddParameters(17, "@VLERSIMIZYRES_ARDHME", vlersimiZyresArdhme, ParameterDirection.Input);
            dbManager.AddParameters(18, "@LIMITI_ARDHME_PLUS1", limitiArdhmePlus1, ParameterDirection.Input);
            dbManager.AddParameters(19, "@KERKESA_GJYKATES_ARDHME_PLUS1", kerkesaGjykatesArdhmePlus1, ParameterDirection.Input);
            dbManager.AddParameters(20, "@DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS1", diferencaKerkeseLimitArdhmePlus1, ParameterDirection.Input);
            dbManager.AddParameters(21, "@VLERSIMIZYRES_ARDHME_PLUS1", vlersimiZyresArdhmePlus1, ParameterDirection.Input);
            dbManager.AddParameters(22, "@LIMITI_ARDHME_PLUS2", limitiArdhmePlus2, ParameterDirection.Input);
            dbManager.AddParameters(23, "@KERKESA_GJYKATES_ARDHME_PLUS2", kerkesaGjykatesArdhmePlus2, ParameterDirection.Input);
            dbManager.AddParameters(24, "@DIFERENCA_KERKESE_LIMIT_ARDHME_PLUS2", diferencaKerkeseLimitArdhmePlus2, ParameterDirection.Input);
            dbManager.AddParameters(25, "@VLERSIMIZYRES_ARDHME_PLUS2", vlersimiZyresArdhmePlus2, ParameterDirection.Input);
            dbManager.AddParameters(26, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(27, "@IDKRIJUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(28, "@KOKAID", kokaID, ParameterDirection.Input);
            dbManager.AddParameters(29, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_insert");
            shoId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaShpenzimeOperative(int shoId)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SHOID", shoId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal IEnumerable<clsShpenzimeOperative> MerrShpenzimeOperative(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_SHPENZIME_OPERATIVE_merrSipasNderrmarjes", clsShpenzimeOperative.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e shpenzimeveOperative
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultShpenzimeOperative(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i shpenzimeve operative u krye me sukses!");
        }
        #endregion SHPENZIME OPERATIVE

        #region Parashikimi i Shpenzimeve te personelit
        /// <summary>
        /// merr te gjitha rreshtat e funksionve te parashikimit te shpenzimeve te personelit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsParashikimiTeArdhura</returns>
        internal IEnumerable<clsParashikimShpenzPersoneli> MerrParashikimShpenzPersoneli(int idNdermarrje,int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_merrSipasNdermarrjes", clsParashikimShpenzPersoneli.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e parashikimit te shpenzimeve te personelit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultParashikimShpenzPersoneli(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i parashikimit te shpenzimeve te personelit u krye me sukses!");
        }

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="pShPId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokParashikimShpenzPersoneli(int pShPId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PSHPID", pShPId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="pShPId"></param>
        /// <param name="funksioniId"></param>
        /// <param name="nrPunonjesish"></param>
        /// <param name="vjetersiaMesatare"></param>
        /// <param name="shtesaPuneJashteOrarit"></param>
        /// <param name="shtesaPageTeRregulluara"></param>
        /// <param name="fondPagePerSigurimeShoqerore"></param>
        /// <param name="niveliShteses"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajParashikimShpenzPersoneli(out int pShPId, int funksioniId, decimal nrPunonjesish, decimal vjetersiaMesatare, decimal shtesaPuneJashteOrarit, decimal shtesaPageTeRregulluara, decimal fondPagePerSigurimeShoqerore, decimal niveliShteses, int idNdermarrje, int IdModifikuesi, int IdNdermVit)
        {
            pShPId = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@PSHPID", pShPId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@FUNKSIONIID", funksioniId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NRPUNONJESISH", nrPunonjesish, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VJETERSIA_MESATARE", vjetersiaMesatare, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHTESA_PUNE_JASHTORARIT", shtesaPuneJashteOrarit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHTESA_PAGE_TE_RREGULLUARA", shtesaPageTeRregulluara, ParameterDirection.Input);
            dbManager.AddParameters(6, "@FOND_PAGE_PER_SIGURIME_SHOQERORE", fondPagePerSigurimeShoqerore, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NIVELI_SHTESES", niveliShteses, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_PARASHIKIM_SHPENZ_PERSONELI_insert");
            pShPId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }


        #endregion Parashikimi i Shpenzimeve te personelit


        #region RAPORTET
        /// <summary>
        /// kthen nje datatable per te mbushur griden e raporteve
        /// </summary>
        /// <param name="sp"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal DataTable MerrDataTablePerRaportin(string sp, int idNdermarrje, int idNdermVIT)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVIT, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, sp).Tables[0];
        }

        internal DataTable MerrDataTablePerRaportin(string sp, Dictionary<string, object> parametra)
        {
            dbManager.Open();
            dbManager.CreateParameters(parametra.Count);
            int counter = 0;
            foreach (KeyValuePair<string, object> entry in parametra)
            {
                dbManager.AddParameters(counter++, $"@{entry.Key.ToUpper()}", entry.Value, ParameterDirection.Input);
            }
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, sp).Tables[0];
        }

      

        #endregion RAPORTET

        #region INVENTARI SIPAS PERDORUESVE
        /// <summary>
        /// merr te gjitha rreshtat e inventarit sipas perdoruesve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsInventariPerdorues</returns>
        internal IEnumerable<clsInventariPerdorues> MerrInventariPerdorues(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_INVENTARI_PERDORUES_merrSipasNdermarrjes", clsInventariPerdorues.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e inventarit sipas perdoruesve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultInventariPerdorues(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_PERDORUES_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i inventarit sipas perdoruesve u krye me sukses!");
        }


        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="inventariId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokInventariPerdorues(int inventariId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@INVENTARIID", inventariId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_PERDORUES_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="inventariId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        public clsMesazh RuajInventariPerdorues(out int inventariId, int rreshtiId, int gjyqtare, int ndihmes, int sekretare, int administrata, int sallaCivile, int sallaPenale, int sherbimi, int tjeter, int idNdermarrje, int IdModifikuesi)
        {
            inventariId = -1;
            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@INVENTARIID", inventariId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", rreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJYQTARE", gjyqtare, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDIHMES", ndihmes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SEKRETARE", sekretare, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ADMINISTRATA", administrata, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SALLA_CIVILE", sallaCivile, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SALLA_PENALE", sallaPenale, ParameterDirection.Input);
            dbManager.AddParameters(8, "@SHERBIMI", sherbimi, ParameterDirection.Input);
            dbManager.AddParameters(9, "@TJETER", tjeter, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_PERDORUES_insert");
            inventariId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal clsMesazh fshiRreshtNgaInventariPerdorues(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_PERDORUES_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        #endregion INVENTARI SIPAS PERDORUESVE

        #region PASQYRA ORGANIKE
        internal IEnumerable<clsTrupiPasqyraOrganike> MerrTrupinPasqyraOrganike(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKADOK", idKoka, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_TRUPI_PASQYRAORGANIKE_merrTrupin", clsTrupiPasqyraOrganike.Krijo);
        }

        /// <summary>
        /// kthen trupin e updatetuar
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal colTrupiPasqyraOrganike RuajTrupinPasqyraOrganike(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@TRUPI", dt, ParameterDirection.Input);
            return new colTrupiPasqyraOrganike(dbManager.GetIEnumerbale("prc_T_AB_TRUPI_PASQYRAORGANIKE_UpdateTrupin", clsTrupiPasqyraOrganike.Krijo));

        }



        internal clsMesazh RuajKokenPasqyraOrganike(out int IdKokaDok, string NrDok, DateTime? DtDok, int Muaji, int TotaliFemra, int TotaliMeshkuj, int IdKrijuesi, int IdModifikuesi, int IdNdermarrje, DateTime? DtKrijimi, DateTime? DtModifikimi,int idStatusDok, int idNdermvit)
        {
            IdKokaDok = -1;
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDKOKADOK", IdKokaDok, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRDOK", NrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTDOK", DtDok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MUAJI", Muaji, ParameterDirection.Input);
            dbManager.AddParameters(4, "@TOTALIFEMRA", TotaliFemra, ParameterDirection.Input);
            dbManager.AddParameters(5, "@TOTALIMESHKUJ", TotaliMeshkuj, ParameterDirection.Input);

            if (IdKrijuesi == 0) dbManager.AddParameters(6, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDKRIJUESI", IdKrijuesi, ParameterDirection.Input);

            if (IdModifikuesi == 0) dbManager.AddParameters(7, "@IDMODIFIKUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDMODIFIKUESI", IdModifikuesi, ParameterDirection.Input);

            dbManager.AddParameters(8, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);

            if (DtKrijimi == null) dbManager.AddParameters(9, "@DTKRIJIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@DTKRIJIMI", DtKrijimi, ParameterDirection.Input);

            if (DtModifikimi == null) dbManager.AddParameters(10, "@DTMODIFIKIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@DTMODIFIKIMI", DtModifikimi, ParameterDirection.Input);
             dbManager.AddParameters(11, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMVIT", idNdermvit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_KOKA_PASQYRAORGANIKE_ins");
            IdKokaDok = Convert.ToInt32(dbManager.Parameters[0].Value);

            return new clsMesazh(true, "Ruajtja u krye me sukses!");

        }

        internal clsMesazh FshiUpdateStatusDokPasqyraOrganike(int IdKokaDok, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKADOK", IdKokaDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_KOKA_PASQYRAORGANIKE_fshiUpdDel");
            return new clsMesazh(true, "Fshirja u krye me sukses!");
        }

        internal IEnumerable<clsKokaPasqyraOrganike> MerrListPasqyraOrganike(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_KOKA_PASQYRAORGANIKE_MerrPasqyratSipasNdermarrjes", clsKokaPasqyraOrganike.Krijo);
        }

        internal void MerrPasqyraOrganikeSipasId(int idKoka, clsKokaPasqyraOrganike koka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKADOK", idKoka, ParameterDirection.Input);
            dbManager.FillObject<clsKokaPasqyraOrganike>("prc_T_AB_KOKA_PASQYRAORGANIKE_MerrSipasID", koka.Mbush);
        }
        internal clsMesazh RuajTrupPasqyraOrganike(out int idTrupi, int IdKokaDok, int IdProfesioni, decimal VleraFakt, decimal VleraPlan, int IdStatusDok, int IdKrijuesi, int TotaliFemra, int TotaliMeshkuj)
        {
            idTrupi = -1;
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDTRUPIDOK", idTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKADOK", IdKokaDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPROFESIONI", IdProfesioni, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERAFAKT", VleraFakt, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERAPLAN", VleraPlan, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", IdStatusDok, ParameterDirection.Input);

            if (IdKrijuesi == 0)
                dbManager.AddParameters(6, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDKRIJUESI", IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@TOTALIFEMRA", TotaliFemra, ParameterDirection.Input);
            dbManager.AddParameters(8, "@TOTALIMESHKUJ", TotaliMeshkuj, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_TRUPI_PASQYRAORGANIKE_ins");
            idTrupi = Convert.ToInt32(dbManager.Parameters[0].Value);
            return new clsMesazh(true, "Ruajtja u krye me sukses!");
        }


        internal bool EkzistonKyNumerDokumentiPasqyraOrganike(string nrDok, int idNdermarrje, int idNdermVit)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_PASQYRAORGANIKE_ekzistonKyNrDokumenti"));

            return pergjigja > 0;
        }
        #endregion


        #region EVIDENCA STATISTIKORE
        internal IEnumerable<clsTrupiEvidencaStatistikore> MerrTrupinEvidencaStatistikore(int idKoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKADOK", idKoka, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_TRUPI_EVIDENCASTATISTIKORE_merrTrupin", clsTrupiEvidencaStatistikore.Krijo);
        }

        /// <summary>
        /// kthen trupin e updatetuar
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        internal colTrupiEvidencaStatistikore RuajTrupinEvidencaStatistikore(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@TRUPI", dt, ParameterDirection.Input);
            return new colTrupiEvidencaStatistikore(dbManager.GetIEnumerbale("prc_T_AB_TRUPI_EVIDENCASTATISTIKORE_UpdateTrupin", clsTrupiEvidencaStatistikore.Krijo));

        }



        internal clsMesazh RuajKokenEvidencaStatistikore(out int IdKokaDok, int TreMujori, int GjyqtarPlan, int IdModifikuesi, int IdNdermarrje, int IdNdermVit)
        {
            IdKokaDok = -1;
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKADOK", IdKokaDok, ParameterDirection.Output);
            dbManager.AddParameters(1, "@TREMUJORI", TreMujori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJYQTARPLAN", GjyqtarPlan, ParameterDirection.Input);

            if (IdModifikuesi == 0) dbManager.AddParameters(3, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_KOKA_EVIDENCASTATISTIKORE_ins");
            IdKokaDok = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja u krye me sukses!");

        }

        internal clsMesazh FshiUpdateStatusDokEvidencaStatistikore(int IdKokaDok, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKADOK", IdKokaDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_KOKA_EVIDENCASTATISTIKORE_fshiUpdDel");
            return new clsMesazh(true, "Fshirja u krye me sukses!");
        }

        internal IEnumerable<clsKokaEvidencaStatistikore> MerrListEvidencaStatistikore(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_KOKA_EVIDENCASTATISTIKORE_MerrEvidencatSipasNdermarrjes", clsKokaEvidencaStatistikore.Krijo);
        }

        internal void MerrEvidencaStatistikoreSipasId(int idKoka, clsKokaEvidencaStatistikore evidenca)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKADOK", idKoka, ParameterDirection.Input);
            dbManager.FillObject<clsKokaEvidencaStatistikore>("prc_T_AB_KOKA_EVIDENCASTATISTIKORE_MerrSipasID", evidenca.Mbush);
        }

        internal clsMesazh RuajTrupEvidencaStatistikore(out int idTrupi, int IdKokaDok, int RreshtiId, int NumriGjithsej, int NumriPerfunduar, int IdStatusDok, int IdKrijuesi)
        {
            idTrupi = -1;
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDTRUPIDOK", idTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKADOK", IdKokaDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@RRESHTIID", RreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NUMRI_GJITHSEJ", NumriGjithsej, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NUMRI_PERFUNDUAR", NumriPerfunduar, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", IdStatusDok, ParameterDirection.Input);

            if (IdKrijuesi == 0)
                dbManager.AddParameters(6, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(6, "@IDKRIJUESI", IdKrijuesi, ParameterDirection.Input);


            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_TRUPI_EVIDENCASTATISTIKORE_ins");
            idTrupi = Convert.ToInt32(dbManager.Parameters[0].Value);
            return new clsMesazh(true, "Ruajtja u krye me sukses!");
        }

        internal bool EkzistonKyDokumentEvidencaStatistikore(int gjyqtarPlan, int treMujori, int idNdermarrje, int idNdermVit)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@GJYQTARPLAN", gjyqtarPlan, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TREMUJORI", treMujori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_EVIDENCASTATISTIKORE_ekzistonKyDokument"));

            return pergjigja > 0;
        }

        internal clsMesazh fshiRreshtNgaEvidencaStatistikore(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_EVIDENCA_STATISTIKORE_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        #endregion EVIDENCA STATISTIKORE

        #region INVENTARI SIPAS VITEVE

        /// <summary>
        /// shenon si te fshire rekordin e caktuar
        /// </summary>
        /// <param name="inventariId"></param>
        /// <returns></returns>
        internal clsMesazh fshiUpdateStatusDokInventariVite(int inventariId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@INVENTARIID", inventariId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_VITE_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// shton nje rekord te ri me statusdok 1,perdoret kur behet modifikimi i nje rreshti,pasi fshihet rreshti i vjeter shtohet i riu
        /// </summary>
        /// <param name="inventariId"></param>
        /// <param name="rreshtiId"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="IdModifikuesi"></param>
        /// <returns></returns>
        internal clsMesazh RuajInventariVite(out int inventariId, int RreshtiId, int Viti2003, int Viti2004, int Viti2005, int Viti2006, int Viti2007, int Viti2008, int Viti2009, int Viti2010, int Viti2011, int Viti2012, int Viti2013, int Viti2014, int Viti2015, int Viti2016, int Viti2017, int Viti2018, int Viti2019, int Viti2020, int IdNdermarrje, int IdModifikuesi)
        {
            inventariId = -1;
            dbManager.Open();
            dbManager.CreateParameters(22);
            dbManager.AddParameters(0, "@INVENTARIID", inventariId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@RRESHTIID", RreshtiId, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VITI2003", Viti2003, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VITI2004", Viti2004, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VITI2005", Viti2005, ParameterDirection.Input);
            dbManager.AddParameters(5, "@VITI2006", Viti2006, ParameterDirection.Input);
            dbManager.AddParameters(6, "@VITI2007", Viti2007, ParameterDirection.Input);
            dbManager.AddParameters(7, "@VITI2008", Viti2008, ParameterDirection.Input);
            dbManager.AddParameters(8, "@VITI2009", Viti2009, ParameterDirection.Input);
            dbManager.AddParameters(9, "@VITI2010", Viti2010, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VITI2011", Viti2011, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VITI2012", Viti2012, ParameterDirection.Input);
            dbManager.AddParameters(12, "@VITI2013", Viti2013, ParameterDirection.Input);
            dbManager.AddParameters(13, "@VITI2014", Viti2014, ParameterDirection.Input);
            dbManager.AddParameters(14, "@VITI2015", Viti2015, ParameterDirection.Input);
            dbManager.AddParameters(15, "@VITI2016", Viti2016, ParameterDirection.Input);
            dbManager.AddParameters(16, "@VITI2017", Viti2017, ParameterDirection.Input);
            dbManager.AddParameters(17, "@VITI2018", Viti2018, ParameterDirection.Input);
            dbManager.AddParameters(18, "@VITI2019", Viti2019, ParameterDirection.Input);
            dbManager.AddParameters(19, "@VITI2020", Viti2020, ParameterDirection.Input);
            dbManager.AddParameters(20, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(21, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_VITE_insert");
            inventariId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        /// <summary>
        /// merr te gjitha rreshtat e inventarit sipas viteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns>kthen nje liste me clsInventariVite</returns>
        internal IEnumerable<clsInventariVite> MerrInventariVite(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_INVENTARI_VITE_merrSipasNdermarrjes", clsInventariVite.Krijo);
        }

        /// <summary>
        /// thirret ne krijim te ndermarrjes per krijimin e inventarit sipas viteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh KrijoDokumentDefaultInventariVite(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_VITE_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i inventarit sipas viteve u krye me sukses!");
        }

        internal clsMesazh fshiRreshtNgaInventariVite(int rreshtiID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RRESHTIID", rreshtiID, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_INVENTARI_VITE_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        #endregion INVENTARI SIPAS VITEVE

        internal IEnumerable<clsNjesiMatese> MerrNjesiteMatese()
        {
            dbManager.Open();
            return dbManager.GetIEnumerbale("prc_T_AB_NJESIMATESE_merrNjesite", clsNjesiMatese.Krijo);
        }

        internal IEnumerable<clsPivotGrid> MerrKolonaPerPivotGrid(int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE",idKomponente, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PIVOTGRID_merrKolonat", clsPivotGrid.Krijo);
        }

        internal clsMesazh RuajShpenzimeOperativeCeshtje(int kokaID, DateTime? DtDok, int idNdermarrje, int NrCeshtjeParaardhes, int NrCeshtjeVitiAktual, int NrCeshtjeVitiPasardhes)
        {
           // kokaID = -1;
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@KOKAID", kokaID, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DTDOK", DtDok.Value, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NRCESHTJEPARAARDHES", NrCeshtjeParaardhes, ParameterDirection.Input);
            dbManager.AddParameters(4, "@NRCESHTJEVITIAKTUAL", NrCeshtjeVitiAktual, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRCESHTJEVITIPASARDHES", NrCeshtjeVitiPasardhes, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_CESHTJE_upd");
           // kokaID = Convert.ToInt32(dbManager.Parameters[0].Value);
            return new clsMesazh(true, "Ruajtja u krye me sukses!");
        }

        internal void MerrShpenzimeOperativeCeshtje(clsShpenzimeOperativeCeshtje obj, int kokaID)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@KOKAID", kokaID, ParameterDirection.Input);
            dbManager.FillObject<clsShpenzimeOperativeCeshtje>("prc_T_AB_SHPENZIME_OPERATIVE_CESHTJE_select", obj.Mbush);
        
        }

        internal IEnumerable<clsTrupiEvidencaStatistikore> MerrTrupinEvidencaStatistikore(int idNdermarrje, int idAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDAMBJENTI", idAmbjenti, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_TRUPI_EVIDENCASTATISTIKORE_merrTrupinDefault", clsTrupiEvidencaStatistikore.Krijo);
        }

        internal bool EkzistonNjeDokumentPerKetePeriudhe(int IdNdermarrje, int TreMujori, int idNdermVit)
        {

            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@TREMUJORI", TreMujori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);


            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_KOKA_EVIDENCASTATISTIKORE_ekzistonPeriudha"));

            return pergjigja > 0;
        }

        internal int MerrNivelShpenzimiOperativ(int idPrindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idPrindi, ParameterDirection.Input);



            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_SHPENZIME_OPERATIVE_KONFIG_merrNivelSipasID"));

        }



        #region konfigurimi i zerave te prokurimeve publike
        internal IEnumerable<clsRealizimProkurimesh> MerrRealizimProkurimesh(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_selectAll", clsRealizimProkurimesh.Krijo);
        }


        internal bool KaRegjistrimeMeKeteRealizimProkurimesh(int rpkId)
        {

            int pergjigja = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RPKID", rpkId, ParameterDirection.Input);
            pergjigja = Convert.ToInt16(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_eshtePerdorurNeRregjistrim"));
            return pergjigja == 1;
        }



        internal clsMesazh RuajRealizimProkurimeshKonfig(out int rpkId, string Kodi, string Pershkrimi, int IdPrindi, int IdKrijuesi, int IdNdermarrje, int niveli, int IdNdermVit)
        {
            rpkId = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@RPKID", rpkId, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", Pershkrimi, ParameterDirection.Input);
            if (IdPrindi == 0) dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input); else dbManager.AddParameters(3, "@IDPRINDI", IdPrindi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUESI", IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_insert");
            rpkId = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh fshiKonfigurimRealizimProkurimeshUpdDel(int RpkId, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@RPKID", RpkId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal bool kaFemijRealizimi(int rpkId)
        {
            int pergjigja = 0;
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RPKID", rpkId, ParameterDirection.Input);
            pergjigja = Convert.ToInt16(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_KaFemij"));
            return pergjigja == 1;
        }

        internal clsMesazh ModifikoRealizimProkurimeshKonfig(int RpkId, string Kodi, string Pershkrimi, int IdPrindi, int idNdermarrje, int IdModifikuesi, int niveli, int idNdermVIt)
        {
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@RPKID", RpkId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMI", Pershkrimi, ParameterDirection.Input);
            if (IdPrindi == 0)
                dbManager.AddParameters(3, "@IDPRINDI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(3, "@IDPRINDI", IdPrindi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@IDMODIFIKUESI", IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(6, "@NIVELI", niveli, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDNDERMVIT", idNdermVIt, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_modifiko");
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }

        internal int MerrNivelRealizimProkurimesh(int idPrindi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idPrindi, ParameterDirection.Input);



            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_merrNivelSipasID"));

        }

        internal clsMesazh KrijoDokumentDefaultRealizimProkurimesh(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_insertAllDefault");
            return new clsMesazh(true, "Regjistrimi default i zerave te prokurimeve u krye me sukses!");
        }

        internal clsMesazh PerditesoNiveletEZeraveTeProkurimit(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NEW", dt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KONFIG_perditesoNivelet");
            return new clsMesazh(true, "te dhenat u modifikuan me sukses!");
        }
        #endregion konfigurimi i zerave te prokurimeve publike

        #region regjistrimi  i realizimeve te prokurimeve publike
        internal IEnumerable<clsTrupiRealizimProkurimesh> MerrTrupinRealizimProkurimi(int idKokaRp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKARP", idKokaRp, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_REALIZIM_PROKURIMESH_TRUPI_merrTrupin", clsTrupiRealizimProkurimesh.Krijo);
        }

        internal IEnumerable<clsTrupiRealizimProkurimesh> MerrTrupDefaultRealizimProkurimesh(int idNdermarrje, int idAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idAmbjenti, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_REALIZIM_PROKURIMESH_TRUPI_merrTrupinDefault", clsTrupiRealizimProkurimesh.Krijo);
        }
        internal clsMesazh RuajTrupRealizimProkurimesh(out int idTrupi, int idKokaRp, int rpkId, double fondiLimit, double vleraKontrates, string llojProcedure, string koheTenderi, string operatoriEkonomik, string burimiFinancimit,int idStatusDok, int idKrijuesi)
        {
            idTrupi = -1;
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters(0, "@IDTRUPIRP", idTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKARP", idKokaRp, ParameterDirection.Input);
            dbManager.AddParameters(2, "@RPKID", rpkId, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FONDI_LIMIT", fondiLimit, ParameterDirection.Input);
            dbManager.AddParameters(4, "@VLERA_KONTRATES", vleraKontrates, ParameterDirection.Input);
            if(String.IsNullOrEmpty(llojProcedure))
                dbManager.AddParameters(5, "@LLOJ_PROCEDURE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(5, "@LLOJ_PROCEDURE", llojProcedure, ParameterDirection.Input);
            if (String.IsNullOrEmpty(koheTenderi))
                dbManager.AddParameters(6, "@KOHE_TENDERI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(6, "@KOHE_TENDERI", koheTenderi, ParameterDirection.Input);
            if (String.IsNullOrEmpty(operatoriEkonomik))
                dbManager.AddParameters(7, "@OPERATORI_EKONOMIK", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(7, "@OPERATORI_EKONOMIK", operatoriEkonomik, ParameterDirection.Input);
            if (String.IsNullOrEmpty(burimiFinancimit))
                dbManager.AddParameters(8, "@BURIMI_FINANCIMIT", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(8, "@BURIMI_FINANCIMIT", burimiFinancimit, ParameterDirection.Input);

            dbManager.AddParameters(9, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);

            if (idKrijuesi == 0)
                dbManager.AddParameters(10, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(10, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);


            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_TRUPI_ins");
            idTrupi = Convert.ToInt32(dbManager.Parameters[0].Value);
            return new clsMesazh(true, "Ruajtja u krye me sukses!");
        }

        internal void MerrRealizimProkurimiSipasId(int idKoka, clsKokaRealizimProkurimesh prokure)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKARP", idKoka, ParameterDirection.Input);
            dbManager.FillObject<clsKokaRealizimProkurimesh>("prc_T_AB_REALIZIM_PROKURIMESH_KOKA_MerrSipasID", prokure.Mbush);
        }
        internal int MerrIdProkurimiParashikim(int idKoka, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_MerrParashikimId"));
        }
        internal clsMesazh RuajKokenRealizimProkurimi(out int IdKokaRp,string NrDok, int KaterMujori, bool Parashikim, int IdModifikuesi, int IdNdermarrje, int IdNdermVit)
        {
            IdKokaRp = -1;
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDKOKARP", IdKokaRp, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NRDOK", NrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATERMUJORI", KaterMujori, ParameterDirection.Input);

            if (IdModifikuesi == 0) dbManager.AddParameters(3, "@IDKRIJUESI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(3, "@IDKRIJUESI", IdModifikuesi, ParameterDirection.Input);

            dbManager.AddParameters(4, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMVIT", IdNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(6, "@PARASHIKIM", Parashikim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_ins");
            IdKokaRp = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, "Ruajtja u krye me sukses!");

        }

        internal bool EkzistonNjeDokumentProkurimiPerKetePeriudhe(int IdNdermarrje, int KaterMujori, int idNdermVit)
        {

            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KATERMUJORI", KaterMujori, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);


            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_ekzistonPeriudha"));

            return pergjigja > 0;
        }

        internal clsMesazh FshiUpdateStatusDokProkurimi(int IdKokaRp, int idModifikuesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKARP", IdKokaRp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMODIFIKUESI", idModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_fshiUpdDel");
            return new clsMesazh(true, "Fshirja u krye me sukses!");
        }

        internal clsMesazh fshiZeProkurimi(int rpkId)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@RPKID", rpkId, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_TRUPI_delete");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh updateDelZeProkurimi(int rpkId, int idkokarp)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@RPKID", rpkId, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOKARP", idkokarp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_TRUPI_updateDel");
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal bool EkzistonKyDokumentProkurimi(int idKokaRp, string nrDok, int idNdermarrje, int idNdermVit)
        {
            int pergjigja = -1;

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "IdKokaRp", idKokaRp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);

            pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_AB_REALIZIM_PROKURIMESH_KOKA_ekzistonKyDokument"));

            return pergjigja > 0;
        }

        internal IEnumerable<clsKokaRealizimProkurimesh> MerrListProkurimePublike(int idNdermarrje, int idNdermVit)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_REALIZIM_PROKURIMESH_KOKA_merrSipasNdermarrjes", clsKokaRealizimProkurimesh.Krijo);
        }

        internal DataTable MerrTeDhenatERaportitParashikimProkurimeshPublike(int idNdermarrje, int idNdermVIT)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVIT, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_RAP_AB_REALIZIM_PROKURIMESH_PARASHIKIM").Tables[0];
        }
        #endregion regjistrimi i realizimeve te prokrimeve publike

        #region planifikim dhe realizim
        internal IEnumerable<clsPlanifikimRealizim> MerrListePlanifikimRealizim(int idNdermarrje, int idNdermVit, int idKrijuesi, string suffix)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idNdermVit, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKRIJUESI", idKrijuesi, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_AB_PLANIFIKIM_REALIZIM" + suffix, clsPlanifikimRealizim.Krijo);
        }

        internal void ruajPlanifikimRealizimDt(DataTable ndryshimet)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@PLANIFIKIM_REALIZIM", ndryshimet, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "PRC_T_AB_PLANIFIKIM_REALIZIM_MERGEDT");

        }
        #endregion planifikim dhe realizim
    }
}