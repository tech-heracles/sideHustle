using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    public class colTeDrejtaRoliKoka : List<clsTeDrejtaRoliKoka>
    {
        public static string keyFieldName = "idDrejta";
        public static string parentFieldName = "idPrindi";
        public static int rootPrindi = 0;

        #region Konstruktoret

        /// <summary>
        /// konstruktori bosh
        /// </summary>
        public colTeDrejtaRoliKoka()
        {
        }

        public colTeDrejtaRoliKoka(int roli, int idNdermarrje, int idviti)
        {
            clsDatabaseAdmin db = new clsDatabaseAdmin();
            DataTable dt = db.merrTeDrejta(roli, idNdermarrje, idviti);
            if (!mbushTeDrejtat(dt))
            {
                db.Dispose();
                throw new Exception("ERROR: Gabim gjate mbushjes se collectionit nga db-ja");
            }
            db.Dispose();
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// Lexon te gjithe griden e te drejtave dhe kthen dy koleksione ate te kokes dhe te trupit
        /// </summary>
        /// <param name="teDrejtat">Hidden field 'HfColTeDrejtat' i ambjentit ShtoModifiko_Grup_Perdoruesish</param>
        /// <returns>colTeDrejtaRoliKoka dhe colTeDrejtaRoli</returns>
        public object[] krijoTeGjitheTeDrejtatNeNje(object[] teDrejtat)
        {
            object[] result = new object[2];
            colTeDrejtaRoli colTeDrejtaTrupi = new colTeDrejtaRoli();

            for (int i = 0; i < teDrejtat.Length; i++)
            {
                Dictionary<string, object> rreshtDrejta = (Dictionary<string, object>)teDrejtat[i];

                clsTeDrejtaRoli eDrejtaTrupi = new clsTeDrejtaRoli();
                eDrejtaTrupi = this.shtoObjektTeRiNeKoleksioninTeDrejtaTrup(rreshtDrejta);
                colTeDrejtaTrupi.Add(eDrejtaTrupi);

                shtoObjektTeRiNeKoleksioninTeDrejtaKoke(int.Parse(rreshtDrejta["IdNdermarrje"].ToString()), int.Parse(rreshtDrejta["IdViti"].ToString()), int.Parse(rreshtDrejta["IdRoli"].ToString()), rreshtDrejta);

            }
            result[0] = this;
            result[1] = colTeDrejtaTrupi;

            return result;
        }

        /// <summary>
        /// Lexon rreshtin e hidden field dhe kontrollon nese koka e drejtes nuk ndodhet ne koleksion e shton
        /// </summary>
        /// <param name="idNdermarrje">Ndermarrje id</param>
        /// <param name="idViti">Viti id</param>
        /// <param name="idRoli">Roli id</param>
        /// <param name="rreshtDrejta">Rreshti nga hidden field</param>
        /// <returns>kthen koleksionin e kokes</returns>
        public bool shtoObjektTeRiNeKoleksioninTeDrejtaKoke(int idNdermarrje, int idViti, int idRoli, Dictionary<string, object> rreshtDrejta)
        {
            if (!this.Exists(x => (x.IdNdermarrje == idNdermarrje) && (x.IdViti == idViti) && (x.IdRoli == idRoli)))
            {
                clsTeDrejtaRoliKoka eDrejtaKoka = new clsTeDrejtaRoliKoka();

                eDrejtaKoka.IdDrejta = 0;

                eDrejtaKoka.IdNdermarrje = idNdermarrje;
                eDrejtaKoka.IdViti = idViti;
                eDrejtaKoka.IdRoli = idRoli;

                eDrejtaKoka.DNdryshoCmimShitje = bool.Parse(rreshtDrejta["DNdryshoCmimShitje"].ToString());
                eDrejtaKoka.DNdryshoCmimBlerje = bool.Parse(rreshtDrejta["DNdryshoCmimBlerje"].ToString());
                eDrejtaKoka.DNdryshoZbritjeAnalitike = bool.Parse(rreshtDrejta["DNdryshoZbritjeAnalitike"].ToString());
                eDrejtaKoka.DNdryshoZbritjeTotale = bool.Parse(rreshtDrejta["DNdryshoZbritjeTotale"].ToString());
                eDrejtaKoka.DKonvertimi = bool.Parse(rreshtDrejta["DKonvertimi"].ToString());
                eDrejtaKoka.DKonvertimSipasUrdherShitje = bool.Parse(rreshtDrejta["DKonvertimSipasUrdherShitje"].ToString());

                this.Add(eDrejtaKoka);
            }
            return true;
        }

        /// <summary>
        /// Lexon rreshtin e hidden field dhe per ta shtuar ne koleksionin e trupit te te drejtave
        /// </summary>
        /// <param name="rreshtDrejta">Rreshti nga hidden fied te grides</param>
        /// <returns>kthen nje rresht per komponente</returns>
        public clsTeDrejtaRoli shtoObjektTeRiNeKoleksioninTeDrejtaTrup(Dictionary<string, object> rreshtDrejta)
        {
            clsTeDrejtaRoli eDrejtaTrupi = new clsTeDrejtaRoli();

            eDrejtaTrupi.IdDrejta = int.Parse(rreshtDrejta["IdDrejta"].ToString());
            eDrejtaTrupi.IdKomponente = int.Parse(rreshtDrejta["IdKomponente"].ToString());

            eDrejtaTrupi.IdDrejtaPrindi = int.Parse(rreshtDrejta["IdPrindi"].ToString());
            eDrejtaTrupi.IdDrejtaKoka = int.Parse(rreshtDrejta["IdDrejtaKoka"].ToString());

            eDrejtaTrupi.IdRaporti = int.Parse(rreshtDrejta["IdRaport"].ToString());

            eDrejtaTrupi.IdNivelRegjistrimi = int.Parse(rreshtDrejta["IdNivelRegjistrimi"].ToString());
            eDrejtaTrupi.IdKategoria = int.Parse(rreshtDrejta["IdKategoria"].ToString());

            eDrejtaTrupi.Tabi = null;
            eDrejtaTrupi.DShtimDraft = false;
            eDrejtaTrupi.DModifikimDraft = false;
            if (eDrejtaTrupi.IdKomponente == 708 && rreshtDrejta["PershkrimKomponente"].ToString() != "Punonjës")
            {
                eDrejtaTrupi.Tabi = rreshtDrejta["PershkrimKomponente"].ToString();
            }
            else
            {
                if (eDrejtaTrupi.IdRaporti == -1)
                {
                    if (int.Parse(rreshtDrejta["Tipi"].ToString()) == 3)
                    {
                        eDrejtaTrupi.DShtimDraft = bool.Parse(rreshtDrejta["DShtimDraft"].ToString());
                        eDrejtaTrupi.DModifikimDraft = bool.Parse(rreshtDrejta["DModifikimDraft"].ToString());
                    }
                    else
                    {
                        eDrejtaTrupi.DShtimDraft = bool.Parse(rreshtDrejta["DShtim"].ToString());
                        eDrejtaTrupi.DModifikimDraft = bool.Parse(rreshtDrejta["DMod"].ToString());
                    }
                }
            }
            eDrejtaTrupi.DMod = bool.Parse(rreshtDrejta["DMod"].ToString());
            eDrejtaTrupi.DFsh = bool.Parse(rreshtDrejta["DFsh"].ToString());
            eDrejtaTrupi.DAmb = bool.Parse(rreshtDrejta["DAmb"].ToString());
            eDrejtaTrupi.DShtim = bool.Parse(rreshtDrejta["DShtim"].ToString());
            eDrejtaTrupi.DGjitheDok = bool.Parse(rreshtDrejta["DGjitheDok"].ToString());
            eDrejtaTrupi.DKerko = bool.Parse(rreshtDrejta["DKerko"].ToString());
            eDrejtaTrupi.DEksporto = bool.Parse(rreshtDrejta["DEksporto"].ToString());
            eDrejtaTrupi.DPrinto = bool.Parse(rreshtDrejta["DPrinto"].ToString());
            eDrejtaTrupi.DArkiva = bool.Parse(rreshtDrejta["DArkiva"].ToString());
            eDrejtaTrupi.DKonverto = bool.Parse(rreshtDrejta["DKonverto"].ToString());
            eDrejtaTrupi.DPezullo = bool.Parse(rreshtDrejta["DPezullo"].ToString());
            eDrejtaTrupi.DAutoKonverto = bool.Parse(rreshtDrejta["DAutoKonverto"].ToString());
            eDrejtaTrupi.IdLayer = int.Parse(rreshtDrejta["IdLayer"].ToString());

            return eDrejtaTrupi;
        }

        /// <summary>
        /// Behet ruajtja e te drejtave per nje rol specifik ne nje vit ne nje ndermarrje
        /// Ruhet dhe historiku
        /// </summary>
        /// <param name="teDrejtat">Eshte objekt qe permbaj dy klasa. Klasa e pare eshte per koleksionet e kokes dhe klasa e dyte per koleksionet e trupit</param>
        /// <returns>Kthen pergjigjen nese cdo gje gjate ruajtjes eshte kryer me sukses!</returns>
        public clsMesazh updateTeGjitheTeDrejtaNeNje(object[] teDrejtat)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            clsMesazh mesazh = new clsMesazh();
            colTeDrejtaRoliKoka teDrejtaRolKoka = (colTeDrejtaRoliKoka)teDrejtat[0];
            colTeDrejtaRoli teDrejtaRolTrupi = (colTeDrejtaRoli)teDrejtat[1];
            try
            {
                dbAdmin.beginTransaksion();
                mesazh = dbAdmin.modifikoTeGjitheTeDrejtatKokeNeNje(teDrejtaRolKoka.mbushTeDrejtatRolKoke());
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbAdmin.modifikoTeGjitheTeDrejtatTrupNeNje(teDrejtaRolTrupi.mbushTeDrejtatRolTrup());
                if (!mesazh.Status)
                {
                    dbAdmin.rollbackTransaksion();
                    return mesazh;
                }
                dbAdmin.commitTransaksion();
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            }
            catch (Exception ex)
            {
                dbAdmin.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        /// <summary>
        /// konverton te gjithe klasen ne DataTable per insert/update qe te behet ne grup
        /// </summary>
        /// <param name="edrejtaRolKoka">klasa qe do te konvertohet</param>
        /// <returns>kthen nje datatable pas konvertimit</returns>
        public DataTable mbushTeDrejtatRolKoke()
        {
            if (this.Count == 0) return null;

            DataTable teDrejtatRolKoka = krijoDataTableHeadersPerTeDrejtaRolKoka();
            foreach (clsTeDrejtaRoliKoka edrejtaRolKoka in this)
            {
                teDrejtatRolKoka.LoadDataRow(mbushTeDrejtenRolKoke(teDrejtatRolKoka, edrejtaRolKoka).ItemArray, false);
            }
            return teDrejtatRolKoka;
        }


        #endregion

        #region Metoda Private          

        /// <summary>
        /// Kjo metode perdoret nga vet klasa per te mbushur vet collection-in nga nje dataTable e dhene. 
        /// Kujdes collection-i mund te kete te dhena paraprake.
        /// </summary>
        /// <param name="dt">dataTable i te njejtes forme me colection-in</param>
        /// <returns>Kthen true nese eshte ekzekutuar me sukses, false nese ka ndodhur ndonje exception</returns>
        private bool mbushTeDrejtat(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                this.Add(new clsTeDrejtaRoliKoka(rreshti));
            }
            return true;
        }


        /// <summary>
        /// krijon koken e DataTable ekuivalente te klases qe do te sherbeje per insert/update
        /// </summary>
        /// <returns>kthen koken e DataTable te krijuar</returns>
        private DataTable krijoDataTableHeadersPerTeDrejtaRolKoka()
        {
            DataTable teDrejtatRolKoka = new DataTable("teDrejtatRolKoka");
            teDrejtatRolKoka.Columns.Add("IDDREJTAKOKA", (new System.Decimal()).GetType());
            teDrejtatRolKoka.Columns.Add("IDNDERMARRJE", (new System.Decimal()).GetType());
            teDrejtatRolKoka.Columns.Add("IDVITI", (new System.Decimal()).GetType());
            teDrejtatRolKoka.Columns.Add("IDROLI", (new System.Decimal()).GetType());
            teDrejtatRolKoka.Columns.Add("D_NDRYSHOCMIMSHITJE", (new System.Boolean()).GetType());
            teDrejtatRolKoka.Columns.Add("D_NDRYSHOCMIMBLERJE", (new System.Boolean()).GetType());
            teDrejtatRolKoka.Columns.Add("D_NDRYSHOZBRITJEANALITIKE", (new System.Boolean()).GetType());
            teDrejtatRolKoka.Columns.Add("D_NDRYSHOZBRITJETOTALE", (new System.Boolean()).GetType());
            teDrejtatRolKoka.Columns.Add("D_KONVERTIMI", (new System.Boolean()).GetType());
            teDrejtatRolKoka.Columns.Add("D_KONVURDHER", (new System.Boolean()).GetType());
            return teDrejtatRolKoka;
        }

        /// <summary>
        /// konverton nje objekt te klases ne formatin e nje DataRow qe do te sherbeje per veprimin e Insert/Update ne grup
        /// </summary>
        /// <param name="dataTableHeader">sherben per krijimin e rreshtit nga datable i krijuar per kete rast</param>
        /// <param name="edrejtaRolKoka">eshte objeti i klases qe do te konvertohet</param>
        /// <returns>kthen rreshtin qe perban klasen te konvertuar ne DataRow</returns>
        /// 
        private DataRow mbushTeDrejtenRolKoke(DataTable dataTableHeader, clsTeDrejtaRoliKoka edrejtaRolKoka)
        {
            DataRow rreshti = dataTableHeader.NewRow();
            rreshti["IDDREJTAKOKA"] = edrejtaRolKoka.IdDrejta;
            rreshti["IDNDERMARRJE"] = edrejtaRolKoka.IdNdermarrje;
            rreshti["IDVITI"] = edrejtaRolKoka.IdViti;
            rreshti["IDROLI"] = edrejtaRolKoka.IdRoli;
            rreshti["D_NDRYSHOCMIMSHITJE"] = edrejtaRolKoka.DNdryshoCmimShitje;
            rreshti["D_NDRYSHOCMIMBLERJE"] = edrejtaRolKoka.DNdryshoCmimBlerje;
            rreshti["D_NDRYSHOZBRITJEANALITIKE"] = edrejtaRolKoka.DNdryshoZbritjeAnalitike;
            rreshti["D_NDRYSHOZBRITJETOTALE"] = edrejtaRolKoka.DNdryshoZbritjeTotale;
            rreshti["D_KONVERTIMI"] = edrejtaRolKoka.DKonvertimi;
            rreshti["D_KONVURDHER"] = edrejtaRolKoka.DKonvertimSipasUrdherShitje;
            return rreshti;
        }
        #endregion
    }
}
