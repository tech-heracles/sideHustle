using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbInventari;
using System.Data;
using System.Web;
using System.Globalization;


namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  trupin e nje dokumenti ndryshim sasi/cmim
    ///  (Te dhenat  merren nga tabela : T_TRUPINDRYSHIMCMIMSASI)
    /// </summary>
    public class clsTrupiNdryshimCmimSasi
    {

        #region Attributet
        private int idTrupi;
        private int idKoka;
        private int idArtikull;
        private string kodArtikull;
        private string pershkrimArtikull;
        private int idNjesia;
        private double sasiaGjendje;
        private double cmimiGjendje;
        private double vleftaGjendje;
        private double cmimiRi;
       
        private double sasiaRe;
        private double vleftaRe;
        private int idMag;
        private DataRow rreshti;
       
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="cm">cmimi i artikullit</param>
        /// <param name="dt"> data e dokumentit</param>
        /// <param name="idArt"> id e artikullit</param>
          /// <param name="idmag">id e magazines</param>
        /// <param name="idKoka">id e kokes se dokumentit te magazines</param>
        /// <param name="idTrup">id ritese e trupit te dokumentit te magazines</param>
        /// <param name="idNjes">id e njesise se artikullit</param>
        /// <param name="KodArt">kodi i artikullit</param>
        /// <param name="cmimri">koeficienti midis dy njesive te artikullit</param>
        /// <param name="pershkArt">pershkrimi i artikullit</param>
        /// <param name="sas"> sasia e artikullit</param>
        /// <param name="sasire"> sasia progresive e artikullit</param>
         /// <param name="vl"> vlefta e artikullit</param>
        /// <param name="vlre"> vlefta progresive e artikullit</param>
       
        public clsTrupiNdryshimCmimSasi(int idTrup, int idKoka, int idArt, string KodArt, string pershkArt, int idNjes, double sas, double cm,
          double vl, double cmimri,  double sasire, double vlre, int idmag)
        {
            idTrupi = idTrup;
           this. idKoka = idKoka;
     
            idArtikull = idArt;
            kodArtikull = KodArt;
            pershkrimArtikull = pershkArt;
            idNjesia = idNjes;
            sasiaGjendje = sas;
            cmimiGjendje = cm;
            vleftaGjendje = vl;
            cmimiRi = cmimri;
    
            sasiaRe = sasire;
            vleftaRe = vlre;
            idMag = idmag;
           
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        public clsTrupiNdryshimCmimSasi(int idTrupi)
        {
            clsDatabaseRegjistrim dbtrupMagazine = new clsDatabaseRegjistrim();
            mbushTrupNdryshimCmimSasi(dbtrupMagazine.ktheTrupiNdryshimCmimSasiSipasID(idTrupi));
            dbtrupMagazine.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="idTrupi"></param>
        /// <param name="dbtrupMagazine"></param>
        public clsTrupiNdryshimCmimSasi(int idTrupi, clsDatabaseRegjistrim dbtrupMagazine)
        {

            mbushTrupNdryshimCmimSasi(dbtrupMagazine.ktheTrupiNdryshimCmimSasiSipasID(idTrupi));
        }

        public bool krijoTrupMagazinaNgaGrida(int idNdermarrje,  int idkodi, string emertimi, string kodi, int njesia, double sasia, double cmimi, double vlefta, int magazina, clsDatabaseInventari db)
        {
            if (idkodi == 0 || idkodi == -1)
                return false;
       
            clsArtikulli art = new clsArtikulli(idkodi, db);
            if (art.IdArtikulli == 0)
            {
                throw new Exception("Nje nga artikujt nuk ekziston!");
            }
            if (art.Klasa == 2 || art.Klasa == 3)
                return false;
            if (art.Klasa == 4)
            {
                throw new Exception("Artikull i perbere!");
            }
            else
            {
                idArtikull = idkodi;
                kodArtikull = kodi;
                pershkrimArtikull = emertimi;
                idNjesia = njesia; ;
                if (art.Njesi1Artikulli == idNjesia)//nese njesia e zgjedhuer eshte njesia 1 e artikullit ath koeficienti vendoset 1
                    this.cmimiRi = 1;
                else
                    this.cmimiRi = double.Parse(art.KoeficientArtikulli.ToString());//nese njesia e zgjedhur nuk eshte njesia e pare e artikullit ath koeficienti vendoset sa koeficienti i percaktuar tek artikulli                        
                this.sasiaGjendje = sasia;
                this.cmimiGjendje = cmimi ;
                this.vleftaGjendje = vlefta ;
                idMag = magazina;
           
            }
            return true;
        }

    
        /// <summary>
        /// Sherben per te krijuar trupMagazinen nga grida client
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="eshteTransferim"></param>
        /// <param name="rreshtDokuKlient"></param>
        public clsTrupiNdryshimCmimSasi(int idNdermarrje, int idPerdorues,  Dictionary<string, object> rreshtDokuKlient)
        {
          
            string kodi = rreshtDokuKlient["txtKodi"].ToString();
            if (kodi == "")
                return;
            int idKodi = Convert.ToInt32(rreshtDokuKlient["txtIdKodi"]);
            string emertimi = rreshtDokuKlient["txtEmertimi"].ToString();
            
            string njesia = rreshtDokuKlient["txtNjesia"].ToString();
            string sasia = rreshtDokuKlient["txtSasia"].ToString();
            string cmimi = rreshtDokuKlient["txtCmimi"].ToString();
            string vlefta = rreshtDokuKlient["txtVlefta"].ToString(); 
            string sasiare = rreshtDokuKlient["txtSasiaRe"].ToString();
            string cmimiri = rreshtDokuKlient["txtCmimiRi"].ToString();
            string vleftare = rreshtDokuKlient["txtVleftaRe"].ToString();
            string magazina = rreshtDokuKlient["txtMagazina"].ToString();
           
            if (kodi != null && kodi != "null" && kodi != "")
            {
               
                clsArtikulli art = new clsArtikulli();
                if (!clsArtikulli.ekziston(kodi, idNdermarrje))
                {
                    throw new Exception("Nje nga artikujt nuk ekziston!");
                }
                art.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
                if (art.IdArtikulli == 0)
                {
                    throw new Exception("Nuk keni autorizime per artikullin " + kodi + "!");
                }
                if (!art.Aktiv)
                {
                    throw new Exception("Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
                }
                if (idKodi != art.IdArtikulli)
                    throw new Exception("Artikulli me kod: " + art.KodArtikulli + " eshte marre gabimisht!");
                idArtikull = art.IdArtikulli;
     
                kodArtikull = kodi;
              
                if (emertimi != null && emertimi != "null")
                    pershkrimArtikull = emertimi;
              
                if (njesia != null && njesia != "null" && njesia != "")
                {
                    clsNjesiArtikulli njesi = new clsNjesiArtikulli();
                    njesi.mbushNjesiArtikulliMeKod(njesia, idNdermarrje); //kevi ndryshim nga me pershk me kod
              
                    idNjesia = njesi.IdNjesia;
                  
                }
                if (sasia != null && sasia != "null" && sasia != "")
                    this.sasiaGjendje = double.Parse(sasia);
                if (cmimi != null && cmimi != "null" && cmimi != "")
                    this.cmimiGjendje = double.Parse(cmimi);
                if (vlefta != null && vlefta != "null" && vlefta != "")
                    this.vleftaGjendje = double.Parse(vlefta);
                if (sasiare != null && sasiare != "null" && sasiare != "")
                    this.sasiaRe = double.Parse(sasiare);
                if (cmimiri != null && cmimiri != "null" && cmimiri != "")
                    this.cmimiRi = double.Parse(cmimiri);
                if (vleftare != null && vleftare != "null" && vleftare != "")
                    this.vleftaRe = double.Parse(vleftare);

                int idMagHyrje = -1;
         
                if (magazina != null && magazina != "null" && magazina != "")
                {
                    DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje);
                    if (njesiadm.IdNjesiAdministrative <= 0)
                        throw new Exception("Nje nga magazinat nuk ekziston!");
                    njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                    if (njesiadm.IdNjesiAdministrative < 1)
                        throw new Exception("Nuk keni autorizime ne kete magazine!");
                    if (magazina != "" && !njesiadm.Aktiv)
                        throw new Exception("Magazina nuk eshte aktive!");
                    idMagHyrje = njesiadm.IdNjesiAdministrative;
                }

              

              
                    idMag = idMagHyrje;
               
            }
            else
                idArtikull = -1;
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsTrupiNdryshimCmimSasi()
        {
        }

        public clsTrupiNdryshimCmimSasi(DataRow rreshti)
        {
            
            mbushTrupNdryshimCmimSasi(rreshti);
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
        /// Kthen/Vendos ID-ne e kokes se dokumentit te ndryshim cmim sasi.
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }
     

        /// <summary>
        /// Kthen/Vendos ID-ne e artikullit .
        /// </summary>
        public int IdArtikulli
        {
            get { return idArtikull; }
            set { idArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos Kodi i artikullit.
        /// </summary>
        public String KodiArtikull
        {
            get { return kodArtikull; }
            set { kodArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i artikullit.
        /// </summary>
        public String PershkrimArtikull
        {
            get { return pershkrimArtikull; }
            set { pershkrimArtikull = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e njesise se artikullit.
        /// </summary>
        public int IdNjesia
        {
            get { return idNjesia; }
            set { idNjesia = value; }
        }

        /// <summary>
        /// Kthen/Vendos sasia e artikullit.
        /// </summary>
        public double SasiaGjendje
        {
            get { return sasiaGjendje; }
            set { sasiaGjendje = value; }
        }

        /// <summary>
        /// Kthen/Vendos cmimi i artikullit.
        /// </summary>
        public double CmimiGjendje
        {
            get { return cmimiGjendje; }
            set { cmimiGjendje = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta e artikullit.
        /// </summary>
        public double VleftaGjendje
        {
            get { return vleftaGjendje; }
            set { vleftaGjendje = value; }
        }

        /// <summary>
        /// Kthen/Vendos koeficienti midis dy njesive te artikullit.
        /// </summary>
        public double CmimiRi
        {
            get { return cmimiRi; }
            set { cmimiRi = value; }
        }


        /// <summary>
        /// Kthen/Vendos sasia progresive e artikullit.
        /// </summary>
        public double SasiaRe
        {
            get { return sasiaRe; }
            set { sasiaRe = value; }
        }

        /// <summary>
        /// Kthen/Vendos vlefta progresive e artikullit.
        /// </summary>
        public double VleftaRe
        {
            get { return vleftaRe; }
            set { vleftaRe = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e magazines.
        /// </summary>
        public int IdMag
        {
            get { return idMag; }
            set { idMag = value; }
        }


      
        #endregion

        #region Metoda Publike

        public clsMesazh krijoTrupNdryshimCmimSasiNgaImporti(string kodi, string njesia, double sasia, double cmimi, double vlefta, string magazina,  int idNdermarrje, int idPerdorues, double sasiare,double cmimiri, double vleftare)
        {
           
            if (kodi == "")
                return new clsMesazh(false, "Plotesoni kodin e artikullit!");
            if (!clsArtikulli.ekziston(kodi, idNdermarrje))
            {
                return new clsMesazh(false, "Ky artikull nuk ekziston!");
            }
            clsArtikulli art = new clsArtikulli();
            art.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
            if (art.IdArtikulli == 0)
            {
                return new clsMesazh(false, "Nuk keni autorizime per artikullin " + kodi + "!");
            }
            if (!art.Aktiv)
            {
                return new clsMesazh(false, "Artikulli me kod:" + art.KodArtikulli + " nuk eshte aktiv!");
            }
            this.idArtikull = art.IdArtikulli;
            this.kodArtikull = art.KodArtikulli;
            this.pershkrimArtikull = art.PershkrimArtikulli;
          

            if (njesia == "")
                return new clsMesazh(false, "Njesia e artikullit nuk duhet te jete bosh!");
            if (!clsNjesiArtikulli.ekzistonNjesiArtikulliMeKod(njesia, idNdermarrje))
                return new clsMesazh(false, "Njesia e artikullit nuk ekziston!");

            clsNjesiArtikulli njesi = new clsNjesiArtikulli();
            njesi.mbushNjesiArtikulliMeKod(njesia, idNdermarrje);
            this.idNjesia = njesi.IdNjesia;
           
            if (sasia == 0)
                return new clsMesazh(false, "Nuk lejohet sasia 0!");
            if (cmimi != vlefta / sasia)
                return new clsMesazh(false, "Vlerat nuk jane te sakta!");
            this.sasiaGjendje = sasia;
            this.cmimiGjendje = cmimi;
            this.vleftaGjendje = vlefta;
            this.sasiaRe= sasiare;
            this.cmimiRi = cmimiri;
            this.vleftaRe = vleftare;
          
            int idMagHyrje = -1;
           if (magazina != null && magazina != "null" && magazina != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative njesiadm = new DbCore.DbRegjistrim.clsNjesiAdministrative(magazina, idNdermarrje);
                if (njesiadm.IdNjesiAdministrative <= 0)
                    return new clsMesazh(false, "Nje nga magazinat nuk ekziston!");
                njesiadm = new clsNjesiAdministrative(magazina, idNdermarrje, idPerdorues);

                if (njesiadm.IdNjesiAdministrative < 1)
                    return new clsMesazh(false, "Nuk keni autorizime ne kete magazine!");
                if (magazina != "" && !njesiadm.Aktiv)
                    return new clsMesazh(false, "Magazina nuk eshte aktive!");
                idMagHyrje = njesiadm.IdNjesiAdministrative;
            }

                this.idMag = idMagHyrje;

            return new clsMesazh(true, "Trupi i magazines u krijua me sukses!");
        }

        /// <summary>
        /// Ruan objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajTrupiNdryshimCmimSasi(out id, this.idKoka,  this.IdArtikulli, this.IdNjesia, this.SasiaGjendje, this.VleftaGjendje, this.CmimiGjendje,  this.SasiaRe, this.VleftaRe, this.IdMag, this.cmimiRi);
            this.idTrupi = id;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// modifikon objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoTrupiMagazina"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoTrupiNdryshimCmimSasi(this.idTrupi, this.idKoka, this.IdArtikulli, this.IdNjesia, this.SasiaGjendje, this.VleftaGjendje, this.CmimiGjendje, this.SasiaRe, this.VleftaRe, this.IdMag, this.cmimiRi);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// fshin objektin e  trupit te dokumentit te magazines ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiTrupiMagazinaSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiNdryshimCmimSasiSipasID(this.idTrupi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// fshin objektet e  trupit te dokumentit te magazines sipas id se kokes ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiTrupiMagazinaSipasKoka"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshiSipasKoka()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiTrupiNdryshimCmimSasiSipasKoka(this.idKoka);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektet e  trupit te dokumentit te magazines sipas kokes nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheTrupiMagazinaNgaKoka"/> 
        /// </summary>
        /// <returns > nje objekt  colTrupiMagazina me te gjithe trupat e nje dokumenti</returns>
        public colTrupiNdryshimCmimSasi merriSipasKoka()
        {
            colTrupiNdryshimCmimSasi data = new colTrupiNdryshimCmimSasi();
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            data.mbushGjitheTrupiNdryshimCmimSasiNgaKoka(this.idKoka);
            return data;
        }


        public static bool kaVeprimeMagPerArtikull(int idArt, int idNderm)
        {
            clsDatabaseRegjistrim dat = new clsDatabaseRegjistrim();
            bool kaVeprime = dat.kaVeprimeMagArtikulliNdryshim(idArt, idNderm);
            dat.Dispose();
            return kaVeprime;
        }

        /// <summary>
        /// Merr objektin e  trupit te dokumentit te magazines sipas id nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheTrupiMagazinaSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsTrupiNdryshimCmimSasi me trupin e dokumentit te magazines te kerkuar</returns>
        public clsTrupiNdryshimCmimSasi merriSipasID()
        {
            clsTrupiNdryshimCmimSasi data = new clsTrupiNdryshimCmimSasi(this.idTrupi);
            return data;
          
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e magazines nga databaza
        /// </summary>
        /// <param name="dbDataRowTrup">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupNdryshimCmimSasi(DataRow dbDataRowTrup)
        {
            if (dbDataRowTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrup["idTrupi"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowTrup["idKoka"].ToString(), out idKoka);
             
                    int.TryParse(dbDataRowTrup["IDARTIKULL"].ToString(), out idArtikull);
                    int.TryParse(dbDataRowTrup["IDNJESIA"].ToString(), out idNjesia);
                    double.TryParse(dbDataRowTrup["SASIAGjendje"].ToString(), out sasiaGjendje);
                    double.TryParse(dbDataRowTrup["CMIMIGjendje"].ToString(), out cmimiGjendje);
                    double.TryParse(dbDataRowTrup["VLEFTAGjendje"].ToString(), out vleftaGjendje);
                    double.TryParse(dbDataRowTrup["Cmimiri"].ToString(), out cmimiRi);
             
                    double.TryParse(dbDataRowTrup["Sasiare"].ToString(), out sasiaRe);
                    double.TryParse(dbDataRowTrup["Vleftare"].ToString(), out vleftaRe);
                    int.TryParse(dbDataRowTrup["IDMAG"].ToString(), out idMag);
                   
                   
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te ndryshim cmim sasi nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
