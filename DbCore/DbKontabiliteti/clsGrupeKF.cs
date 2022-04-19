using System;
using System.Data;
using DbCore.IMBUtils.Validation;
using static System.Convert;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  grupimet e klient/furnitoreve
    ///  (Te dhenat  merren nga tabela : T_GRUPEKF)
    /// </summary>
    public class clsGrupeKF
    {
        #region Atributet
        
        private readonly string _prindi;

        #endregion 

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos kodin e kodifikimit
        /// </summary>
        public string KodGrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne pershkrimin e kodifikimit.
        /// </summary>
        public string PershkrimGrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi { get; set; }

        /// <summary>
        /// Kthen/Vendos nivelin e kodifikimit.
        /// </summary>
        public int NivelGrupi { get; set; }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje { get; set; }

        /// <summary>
        /// Kthen/Vendos statusin e kodifikimit.
        /// </summary>
        public int IdStatusDok { get; set; }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi { get; private set; }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi { get; private set; }

        /// <summary>
        /// Kthen/Vendos grupin e kodifikimit.
        /// </summary>
        public int LlojKodifikimi { get; set; }

        /// <summary>
        /// Kthen/Vendos tipin e kf (klient apo furnitor).
        /// </summary>
        public int LlojKF { get; set; }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsGrupeKF()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="idgrupi"> id ritese e kodifikimit</param>
        /// <param name="kodgrupi">kodi i kodifikimit</param>
        /// <param name="pershkrimgrupi">pershkrimi i kodifikimit</param>
        /// <param name="idPrindi">id e prindit</param>
        /// <param name="nivelgrupi"> niveli i kodifikimit</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit te kodifikimit</param>
        /// <param name="llojKodifikimi">lloji i kodifikimit</param>
        /// <param name="llojKf">lloji i kf (klient apo furnitor)</param>
        public clsGrupeKF(int idgrupi, string kodgrupi, string pershkrimgrupi, int idPrindi, int nivelgrupi, int idPerdoruesi, int idNdermarje, int idStatusDok, int llojKodifikimi, int llojKf)
        {
            IdGrupi = idgrupi;
            KodGrupi = kodgrupi;
            PershkrimGrupi = pershkrimgrupi;
            IdPrindi = idPrindi;
            NivelGrupi = nivelgrupi;
            IdPerdoruesi = idPerdoruesi;
            IdNdermarje = idNdermarje;
            IdStatusDok = idStatusDok;
            LlojKodifikimi = llojKodifikimi;
            LlojKF = llojKf;
        }
        
        public clsGrupeKF(string kodgrupi, string pershkrimgrupi, string prindi, int idPerdoruesi, int idNdermarje, int idstatusdok, int llojKodifikimi, int llojKf, int nivelgrupi, clsDatabaseKontabilitet dbK)
        {
            KodGrupi = kodgrupi;
            PershkrimGrupi = pershkrimgrupi;
            _prindi = prindi;
            IdPerdoruesi = idPerdoruesi;
            IdNdermarje = idNdermarje;
            IdStatusDok = idstatusdok;
            LlojKodifikimi = llojKodifikimi;
            LlojKF = llojKf;
            NivelGrupi = nivelgrupi;

            var mesazh = Kontrollo(dbK);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
        }

        /// <summary>
        /// konstruktor me parameter id e kodifikimit
        /// </summary>
        /// <param name="idgrupi">id e kodifikimit</param>
        public clsGrupeKF(int idgrupi)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.merrKodifikimKF(idgrupi));
        }

        public clsGrupeKF(string kodgrupi, int idndermarje, int llojkodifikimi, int llojkf, clsDatabaseKontabilitet databaseKontabilitet)
        {
            Mbush(databaseKontabilitet.merrKodifikimKFSipasKodLloj(kodgrupi, idndermarje, llojkodifikimi, llojkf));
        }

        public clsGrupeKF(string kodgrupi, int idndermarje, int llojkodifikimi, int llojkf)
        {
            using (var databaseKontabilitet = new clsDatabaseKontabilitet())
                Mbush(databaseKontabilitet.merrKodifikimKFSipasKodLloj(kodgrupi, idndermarje, llojkodifikimi, llojkf));
        }

        public clsGrupeKF(DataRow rreshti)
        {
            Mbush(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsGrupeKF KrijoKGrupeKlientFurnitorPerImport(string kodgrupi, string pershkrimgrupi, string prindi, int idPerdoruesi, int idNdermarje, int idstatusdok, string llojGrupimi, string llojKlientfur, clsDatabaseKontabilitet dbK)
        {
            try
            {
                int llojKodifikimi;
                switch (llojGrupimi)
                {
                    case "Grupimi 1":
                        llojKodifikimi = 1;
                        break;
                    case "Grupimi 2":
                        llojKodifikimi = 2;
                        break;
                    case "Grupimi 3":
                        llojKodifikimi = 3;
                        break;
                    default:
                        llojKodifikimi = 0;
                        break;
                }

                switch (llojKlientfur)
                {
                    case "Klient":
                        LlojKF = 0;
                        break;
                    case "Furnitor":
                        LlojKF = 1;
                        break;
                }

                var prindGrupi = new clsGrupeKF(prindi, idNdermarje, llojKodifikimi, LlojKF, dbK);
                int nivelgrupi = prindGrupi.NivelGrupi + 1;

                return new clsGrupeKF(kodgrupi, pershkrimgrupi, prindi, idPerdoruesi, idNdermarje, idstatusdok, llojKodifikimi, LlojKF, nivelgrupi, dbK);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
        /// <summary>
        /// Ruan objektin kodifikim kf ne tabelen perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Ruaj()
        {
            var data = new clsDatabaseKontabilitet();
            var mesazh = data.ruajGrupKF(out var idGrupi, KodGrupi, PershkrimGrupi, IdPrindi, NivelGrupi, IdPerdoruesi, IdNdermarje, IdStatusDok, LlojKodifikimi, LlojKF);
            IdGrupi = idGrupi;
            data.Dispose();
            return mesazh;
        }

        /// <summary>
        /// Modifikon objektin kodifikim kf ne tabelen perkatese ne databaze. 
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>

        public clsMesazh Modifiko()
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.modifikoGrupKF(IdGrupi, KodGrupi, PershkrimGrupi, IdPrindi, NivelGrupi, IdPerdoruesi, IdNdermarje, IdStatusDok, LlojKodifikimi, LlojKF);
        }

        /// <summary>
        /// Fshin objektin kodifikim kf ne tabelen perkatese ne databaze.
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh Fshi()
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.fshiGrupKF(IdGrupi, IdPerdoruesi);
        }

        /// <summary>
        /// kontrollon nese grupi eshte prind
        /// </summary>
        /// <param name="idgrupi"></param>
        /// <param name="idNdermarje"></param>
        /// <returns></returns>
        public static bool EshtePrind(int idgrupi, int idNdermarje)
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.eshtePrindKF(idgrupi, idNdermarje);
        }

        public static bool KaVeprimeGrupKf(int id)
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.kaVeprimeGrupKF(id);
        }

        public static bool EkzistonGrupKfSipasKodLloje(string kodgrupi, int idndermarje, int llojkodifikimi, int llojkf)
        {
            using (var data = new clsDatabaseKontabilitet())
                return data.ekzistonGrupKFSipasKodLloje(kodgrupi, idndermarje, llojkodifikimi, llojkf);
        }

        #endregion

        #region Metoda Private

        private clsMesazh Kontrollo(clsDatabaseKontabilitet dbK)
        {
            if (string.IsNullOrEmpty(KodGrupi))
                return new clsMesazh(false, "Plotesoni kodin e grupit!");

            var mesazh = clsFunksione.kontrolloKaraktereMeMesazh(KodGrupi, FusheKontrolli.Kodi, false);
            if (!mesazh.Status)
                return new clsMesazh(false, $"Gabim te grupi me kod: {KodGrupi}: " + mesazh.PershkrimMesazhi);

            if (string.IsNullOrEmpty(PershkrimGrupi))
                return new clsMesazh(false, "Plotesoni pershkrimin e grupit!");

            mesazh = clsFunksione.kontrolloKaraktereMeMesazh(PershkrimGrupi, FusheKontrolli.Pershkrimi, true);
            if (!mesazh.Status)
                return new clsMesazh(false, $"Gabim te grupi me kod: {KodGrupi}: " + mesazh.PershkrimMesazhi);

            if (LlojKodifikimi == 0)
                return new clsMesazh(false, $"Plotesoni llojin per grupin {KodGrupi}!");

            if (EkzistonGrupKfSipasKodGrupi(KodGrupi, IdNdermarje, LlojKodifikimi, dbK))
            {
                return new clsMesazh(false, $"Ekziston nje grup me kod {KodGrupi}!");
            }

            if (!string.IsNullOrEmpty(_prindi))
            {
                var prindGrupi = new clsGrupeKF(_prindi, IdNdermarje, LlojKodifikimi, LlojKF, dbK);
                if (prindGrupi.IdGrupi <= 0)
                    return new clsMesazh(false, $"Prindi {_prindi} i grupit me kod {KodGrupi} nuk ekziston!");

                if (KaVeprimeGrupKf(prindGrupi.IdGrupi))
                    return new clsMesazh(false, $"Grupi {prindGrupi.KodGrupi} eshte perdorur ne veprime dhe nuk mund te detajohet. Ju lutem zgjidhni nje grup tjeter si prind te grupit me kod {KodGrupi}");

                IdPrindi = prindGrupi.IdGrupi;
            }

            return new clsMesazh(true, "Kontrollet e grupit u kaluan me sukses");
        }

        private static bool EkzistonGrupKfSipasKodGrupi(string kodGrupi, int idNdermarje, int llojKodifikimi, clsDatabaseKontabilitet data)
        {
            return data.ekzistonGrupKFSipasKodGrupi(kodGrupi, idNdermarje, llojKodifikimi);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kodifikimin e klient furnitoreve nga databaza
        /// </summary>
        /// <param name="dataRow">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void Mbush(DataRow dataRow)
        {
            if (dataRow != null)
            {
                try
                {
                    IdGrupi = !IsDBNull(dataRow["IDGRUPI"])
                        ? ToInt32(dataRow["IDGRUPI"])
                        : 0;
                    KodGrupi = dataRow["KODGRUPI"].ToString();
                    PershkrimGrupi = dataRow["PERSHKRIMGRUPI"].ToString();
                    IdPrindi = !IsDBNull(dataRow["IDPRINDI"])
                        ? ToInt32(dataRow["IDPRINDI"])
                        : 0;
                    NivelGrupi = !IsDBNull(dataRow["NIVELGRUPI"])
                        ? ToInt32(dataRow["NIVELGRUPI"])
                        : 0;
                    IdPerdoruesi = !IsDBNull(dataRow["IDPERDORUESI"])
                        ? ToInt32(dataRow["IDPERDORUESI"])
                        : 0;
                    IdNdermarje = !IsDBNull(dataRow["IDNDERMARJE"])
                        ? ToInt32(dataRow["IDNDERMARJE"])
                        : 0;
                    IdStatusDok = !IsDBNull(dataRow["IDSTATUSDOK"])
                        ? ToInt32(dataRow["IDSTATUSDOK"])
                        : 0;
                    DtKrijimi = !IsDBNull(dataRow["DTKRIJIMI"])
                        ? ToDateTime(dataRow["DTKRIJIMI"])
                        : DateTime.MinValue;
                    DtModifikimi = !IsDBNull(dataRow["DTMODIFIKIMI"])
                        ? ToDateTime(dataRow["DTMODIFIKIMI"])
                        : DateTime.MinValue;
                    LlojKodifikimi = !IsDBNull(dataRow["LLOJKODIFIKIMI"])
                        ? ToInt32(dataRow["LLOJKODIFIKIMI"])
                        : 0;
                    LlojKF = !IsDBNull(dataRow["LLOJKF"])
                        ? ToInt32(dataRow["LLOJKF"])
                        : 0;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kodeve te artikullit nga db-ja");
                }
            }
        }

        #endregion
    }
}
