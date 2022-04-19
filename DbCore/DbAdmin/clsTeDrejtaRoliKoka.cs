using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsTeDrejtaRoliKoka
    {
        #region Atributet
        private int idDrejta;
        private int idNdermarrje;
        private int idViti;
        private int idRoli;
        private String textRoli;
        private bool dNdryshoCmimShitje;
        private bool dNdryshoCmimBlerje;
        private bool dNdryshoZbritjeAnalitike;
        private bool dNdryshoZbritjeTotale;
        private bool dKonvertimi;
        private bool dKonvertimSipasUrdherShitje;
        private colTeDrejtaRoli ocolTeDrejtaRoli;
        private DataRow rreshti;
        #endregion

        #region Properties

        public int IdDrejta
        {
            get
            {
                return idDrejta;
            }
            set
            {
                if (idDrejta == value)
                    return;
                idDrejta = value;
            }
        }
        public int IdNdermarrje
        {
            get
            {
                return idNdermarrje;
            }
            set
            {
                if (idNdermarrje == value)
                    return;
                idNdermarrje = value;
            }
        }
        public int IdViti
        {
            get
            {
                return idViti;
            }
            set
            {
                if (idViti == value)
                    return;
                idViti = value;
            }
        }
        public int IdRoli
        {
            get
            {
                return idRoli;
            }
            set
            {
                if (idRoli == value)
                    return;
                idRoli = value;
            }
        }
        public String TextRoli
        {
            get
            {
                return textRoli;
            }
            set
            {
                if (textRoli == value)
                    return;
                textRoli = value;
            }
        }        
        /// <summary>
        /// Get Set: NdryshoCmimShitje
        /// </summary>
        public bool DNdryshoCmimShitje
        {
            get
            {
                return dNdryshoCmimShitje;
            }
            set
            {
                dNdryshoCmimShitje = value;
            }
        }
        /// <summary>
        /// Get Set: NdryshoCmimBlerje
        /// </summary>
        public bool DNdryshoCmimBlerje
        {
            get
            {
                return dNdryshoCmimBlerje;
            }
            set
            {
                dNdryshoCmimBlerje = value;
            }
        }
        /// <summary>
        /// Get Set: NdryshoZbritjeAnalitike
        /// </summary>
        public bool DNdryshoZbritjeAnalitike
        {
            get
            {
                return dNdryshoZbritjeAnalitike;
            }
            set
            {
                dNdryshoZbritjeAnalitike = value;
            }
        }
        /// <summary>
        /// Get Set: NdryshoZbritjeTotale
        /// </summary>
        public bool DNdryshoZbritjeTotale
        {
            get
            {
                return dNdryshoZbritjeTotale;
            }
            set
            {
                dNdryshoZbritjeTotale = value;
            }
        }
        /// <summary>
        /// Get Set: Vetem te drejte konvertimi ne Fature shitje
        /// </summary>
        public bool DKonvertimi
        {
            get
            {
                return dKonvertimi;
            }
            set
            {
                dKonvertimi = value;
            }
        }
        /// <summary>
        /// Get Set: Ne qofte se eshte true, atehere roli nuk mund te beje modifikime gjate konvertimit te fatures
        /// </summary>
        public bool DKonvertimSipasUrdherShitje
        {
            get
            {
                return dKonvertimSipasUrdherShitje;
            }
            set
            {
                dKonvertimSipasUrdherShitje = value;
            }
        }
        /// <summary>
        /// Cdo koke ka trupin e vet si objet te lidhur me te
        /// </summary>
        public colTeDrejtaRoli oColTeDrejtaRoli
        {
            get { return ocolTeDrejtaRoli; }
            set { ocolTeDrejtaRoli = value; }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsTeDrejtaRoliKoka()
        {
        }

        public clsTeDrejtaRoliKoka(int idDrejta)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (!this.mbushTeDrejten(data.merrTeDrejte(idDrejta)))
            {
                data.Dispose();
                throw new Exception("ERROR: Gabim gjate leximit te se drejtes " + idDrejta + "nga databaza");
            }
            data.Dispose();
        }

        /// <summary>
        /// Konstruktor i plote
        /// </summary>
        /// <param name="idDrejta"></param>
        /// <param name="idPrindi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idModul"></param>
        /// <param name="textModuli"></param>
        /// <param name="idKomponente"></param>
        /// <param name="pershkrimKomponente"></param>
        /// <param name="idRoli"></param>
        /// <param name="dMod"></param>
        /// <param name="dFsh"></param>
        /// <param name="dAmb"></param>
        public clsTeDrejtaRoliKoka(int idDrejta, int idNdermarrje, int idViti, int idRoli, bool ndryshoCmimShitje, bool ndryshoCmimBlerje, bool ndryshoZbritjeAnalitike, bool ndryshoZbritjeTotale, bool konvertimi, bool konvSipasUSH)
        {
            this.idDrejta = idDrejta;
            this.idNdermarrje = idNdermarrje;
            this.idViti = idViti;
            this.idRoli = idRoli;
            this.dNdryshoCmimShitje = ndryshoCmimShitje;
            this.dNdryshoCmimBlerje = ndryshoCmimBlerje;
            this.dNdryshoZbritjeAnalitike = ndryshoZbritjeAnalitike;
            this.dNdryshoZbritjeTotale = ndryshoZbritjeTotale;
            this.dKonvertimi = konvertimi;
            this.dKonvertimSipasUrdherShitje = konvSipasUSH;
        }

        public clsTeDrejtaRoliKoka(DataRow rreshti)
        {
            mbushTeDrejten(rreshti);
        }

        #endregion

        #region Metoda Publike
        /// <summary>
        /// Ruan te gjithe te drejtat per koken dhe trupin, nese ndonje nga ruajtjet nuk ecen kthen direkt mesazh gabimi, me pas behet rollback aty ku therritet
        /// Ruan historikun
        /// </summary>
        /// <param name="data">clsDatabaseAdmin</param>
        /// <param name="idNdermarrje"> ndermarrja</param>
        /// <param name="idViti">viti</param>
        /// <param name="idRoli">roli</param>
        /// <param name="idLlojLicenca">licensa</param>
        /// <returns>then mesazh per suksesin ose mos suksesin e inserteve</returns>
        public clsMesazh krijoTeGjitheTeDrejtaBaze(clsDatabaseAdmin data, int idNdermarrje, int idViti, int idRoli, int idLlojLicenca)
        {
            clsMesazh mesazh = new clsMesazh();
            int idDrejta = -1;

            mesazh = data.krijoTeDrejtaKokaNgaDefault(out idDrejta, idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
                return mesazh;
            mesazh = data.krijoTeDrejtaKomponenteshNgaDefault(idDrejta, idLlojLicenca);
            if (!mesazh.Status)
                return mesazh;
            mesazh = data.krijoTeDrejtaRaporteshBazeNgaDefault(idDrejta);
            if (!mesazh.Status)
                return mesazh;
            mesazh = data.krijoTeDrejtaTabeshBazeNgaDefault(idDrejta);
            if (!mesazh.Status)
                return mesazh;
            mesazh = data.krijoTeDrejtaNivelRegjistrimiNgaDefault(idDrejta, idNdermarrje);
            if (!mesazh.Status)
                return mesazh;
            mesazh = data.krijoTeDrejtaKategoriNgaDefault(idDrejta, idNdermarrje);

            return mesazh;
        }

        /// <summary>
        /// Fshin te gjithe te drejtat per koken dhe trupin, nese ndonje nga fshirjet nuk ecen kthen direkt mesazh gabimi, me pas behet rollback aty ku therritet
        /// Ruhen ne historik
        /// </summary>
        /// <param name="data">clsDatabaseAdmin nga ku eshte hapur transaksioni</param>
        /// <param name="idNdermarrje">ndermarrja</param>
        /// <param name="idViti">viti</param>
        /// <param name="idRoli">roli</param>
        /// <returns>then mesazh per suksesin ose mos suksesin e fshirjeve</returns>
        public clsMesazh fshiTeDrejtatNgaTabelat(clsDatabaseAdmin data, int idNdermarrje, int idViti, int idRoli)
        {
            clsMesazh mesazh = new clsMesazh();
            mesazh = data.fshiTeDrejtatKTrupi(idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.fshiTeDrejtatKoka(idNdermarrje, idViti, idRoli);

            return mesazh;
        }
        
        /// <summary>
        /// Ben Klonimin e te drejtave nga nje ndermarrje
        /// Ben dhe ruajtjen ne historik
        /// </summary>
        /// <param name="data">clsDatabaseAdmin nga ku eshte hapur transaksioni</param>
        /// <param name="idNdermarrje">Ndermarrja nga ku po kopjohen</param>
        /// <param name="idViti">Viti i ndermarrjes nga ku po kopjohen</param>
        /// <param name="idRoli">Roli i ndermarrjes nga ku po kopjohen</param>
        /// <param name="idNdermarjedest">Ndermarrja Destinacion ku po shkon</param>
        /// <param name="idVitidest">Viti Destinacion ku po shkon</param>
        /// <returns>then mesazh per suksesin ose mos suksesin e klonimeve</returns>
        public clsMesazh klonoTeGjitheTeDrejtaNgaNdermarrje(clsDatabaseAdmin data, int idNdermarrje, int idViti, int idRoli, int idNdermarjedest, int idVitidest, DataTable teDrejtaNiveleshRegjistrimi)
        {
            clsMesazh mesazh = new clsMesazh();
            int idDrejta = -1;

            mesazh = data.klonoTeDrejtaKokaNgaNdermarrje(out idDrejta, idNdermarrje, idViti, idRoli, idNdermarjedest, idVitidest);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.klonoTeDrejtaKomponenteshNgaNdermarrje(idDrejta, idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.klonoTeDrejtaRaporteshNgaNdermarrje(idDrejta, idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.klonoTeDrejtaTabeshNgaNdermarrje(idDrejta, idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.klonoTeDrejtaKategorishNgaNdermarrje(idDrejta, idNdermarrje, idViti, idRoli);
            if (!mesazh.Status)
            {
                return mesazh;
            }
            mesazh = data.klonoTeDrejtaNivelRegjistrimeshNgaNdermarrje(idDrejta, idNdermarrje, idViti, idRoli, teDrejtaNiveleshRegjistrimi);
            return mesazh;
        }

        /// <summary>
        /// Fillimisht ben fshirjen e te drejtave te vjetra te ndermarrjes destinacion dhe me pas ben shtimin. Keto futen brenda te njejtit transaksion
        /// </summary>
        /// <param name="idRoli">roli</param>
        /// <param name="idndermarrje">ndermarrja nga ku kopjohen</param>
        /// <param name="idviti">viti nga ku kompjohen</param>
        /// <param name="idndermarjedest">ndermarrja destinacion</param>
        /// <param name="idvitidest">vitit destinacion</param>
        /// <returns></returns>
        public clsMesazh klonoTeDrejtatNgaNjeNdermarrjeDest(int idRoli, int idndermarrje, int idviti, int idndermarjedest, int idvitidest)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            try
            {
                data.beginTransaksion();
                DataTable teDrejtaNiveleshRegjistrimi = data.merrTeDrejtatRolTrupPerNiveleRegjistrimiTeNdryshme(idndermarrje, idndermarjedest, idviti, idvitidest, idRoli);

                mesazh = fshiTeDrejtatNgaTabelat(data, idndermarjedest, idvitidest, idRoli);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }

                mesazh = klonoTeGjitheTeDrejtaNgaNdermarrje(data, idndermarrje, idviti, idRoli, idndermarjedest, idvitidest, teDrejtaNiveleshRegjistrimi);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }

                data.commitTransaksion();
                return new clsMesazh(true, "Klonimi perfundoi me sukses");
            }
            catch
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate klonimit te te drejtave!");
            }

        }

        /// <summary>
        /// Ben Klonimin e te drejtave nga mbyllja e vitit
        /// Ruan historikun
        /// </summary>
        /// <param name="data">clsDatabaseAdmin nga ku eshte hapur transaksioni</param>
        /// <param name="idNdermarrje">Ndermarrja nga ku po kopjohen</param>
        /// <param name="idVitidest">Viti Destinacion ku po shkon</param>
        /// <returns>then mesazh per suksesin ose mos suksesin e klonimeve</returns>
        public clsMesazh klonoTeGjitheTeDrejtaPerMbylljeViti(clsDatabaseAdmin data, int idNdermarrje, int idViti, int idVitidest)
        {
            clsMesazh mesazh = new clsMesazh();

            mesazh = data.klonoTeDrejtaKokaPerMbylljeViti(idNdermarrje, idViti, idVitidest);
            if (!mesazh.Status)
                return mesazh;

            mesazh = data.klonoTeDrejtaKomponenteshPerMbylljeViti(idNdermarrje, idViti, idVitidest);
            if (!mesazh.Status)
                return mesazh;

            mesazh = data.klonoTeDrejtaRaporteshPerMbylljeViti(idNdermarrje, idViti, idVitidest);
            if (!mesazh.Status)
                return mesazh;

            mesazh = data.klonoTeDrejtaTabeshPerMbylljeViti(idNdermarrje, idViti, idVitidest);
            if (!mesazh.Status)
                return mesazh;

            mesazh = data.klonoTeDrejtaKategorishPerMbylljeViti(idNdermarrje, idViti, idVitidest);
            if (!mesazh.Status)
                return mesazh;

            mesazh = data.klonoTeDrejtaNiveleRegjistrimiPerMbylljeViti(idNdermarrje, idViti, idVitidest);

            return mesazh;
        }

        /// <summary>
        /// Ruajtja per trupin dhe koken e rol te drejtave behet njehersh ne sp.
        /// </summary>
        /// <param name="idndermarje">Ndermarrja e re</param>
        /// <param name="idviti">Viti i Ri i ndermarrjes</param>
        /// <param name="idperdoruesi">perdoruesi qe po ben shtimin</param>
        /// <param name="idRoli">Roli qe i perket</param>
        /// <returns>Kthen mesazh per shtimin e sukseshem ose jo</returns>
        public clsMesazh ShtoTeGjitheTeDrejtaBazeNeNjeMeTransaksionNeSp(clsDatabaseAdmin data, int idndermarje, int idviti, int idperdoruesi, int idRoli)
        {
            clsMesazh mesazh = new clsMesazh();
            try
            {
                mesazh = data.ShtoTeGjitheTeDrejtaBazeNeNjeMeTransaksionNeSp(idndermarje, idviti, idperdoruesi, idRoli);
            }
            catch
            {
                mesazh = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se te drejtave!"); ;
            }
            return mesazh;
        }
        #endregion

        #region Metoda Internal
        
        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool mbushTeDrejten(DataRow rreshti)
        {
            try
            {
                idDrejta = Convert.ToInt32(rreshti["IDDREJTA"]);
                idNdermarrje = Convert.ToInt32(rreshti["IDNDERMARRJE"]);
                idViti = Convert.ToInt32(rreshti["IDVITI"]);
                idRoli = Convert.ToInt32(rreshti["IDROLI"]);
                dNdryshoCmimShitje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMSHITJE"]);
                dNdryshoCmimBlerje = Convert.ToBoolean(rreshti["D_NDRYSHOCMIMBLERJE"]);
                dNdryshoZbritjeAnalitike = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJEANALITIKE"]);
                dNdryshoZbritjeTotale = Convert.ToBoolean(rreshti["D_NDRYSHOZBRITJETOTALE"]);
                dKonvertimi = Convert.ToBoolean(rreshti["D_KONVERTIMI"]);
                dKonvertimSipasUrdherShitje = Convert.ToBoolean(rreshti["D_KONVURDHER"]);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        
        #endregion
    }
}
