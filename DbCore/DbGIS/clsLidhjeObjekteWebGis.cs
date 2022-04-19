using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;

using DbCore.DbGIS;


namespace DbCore.DbGIS
{
    public class clsLidhjeObjekteWebGis
    {
        #region Atribute

        private int varGid;
        private string varThe_geom;
        private int varIDLAYER;
        private int varIDLAYERSTYPE;
        private int varIDMAGAZINA;
        private int varIDKODIFIKIMI;
        private int varIDARTIKULLI;
        private int varIDSERIALI;
        private int varIDKOKADOK;
        private int varIDTRUPIDOKLIDHES;
        private DateTime varDTMODIFIKIMI;
        private int varIDPERDORUESI;
        private int varIDNDERMARJE;
        private string varKODI;
        private string varPERSHKRIMI;
        private string varSERIALKOD;
        private string varKODKODIFIKIMI;
        private int varNrStatusi;

        //Per ruajtjen e historikut te veprimeve dhe te lidhjes se objekteve
        private string varVeprimi;
        private int varGidPrindi;
       

        #endregion

        #region Konstruktoret

        public clsLidhjeObjekteWebGis()
        {
        }
        public clsLidhjeObjekteWebGis(DataRow db)
        {
            mbushLidhjeObjekteWebGis(db);
        }
        public clsLidhjeObjekteWebGis(int gid, string the_geom, int IDLAYER, int IDLAYERSTYPE, int IDMAGAZINA, int IDKODIFIKIMI, int IDARTIKULLI, int IDSERIALI, int IDKOKADOK, int IDTRUPIDOKLIDHES, DateTime DTMODIFIKIMI, int IDPERDORUESI, int IDNDERMARJE, string KODI, string PERSHKRIMI, string SERIALKOD, string KODKODIFIKIMI, int NRSTATUSI)
        {
            this.gid = gid;
            this.the_geom = the_geom;
            this.IDLAYERSTYPE = IDLAYERSTYPE;
            this.IDMAGAZINA = IDMAGAZINA;
            this.IDKODIFIKIMI = IDKODIFIKIMI;
            this.IDARTIKULLI = IDARTIKULLI;
            this.IDSERIALI = IDSERIALI;
            this.IDKOKADOK = IDKOKADOK;
            this.IDTRUPIDOKLIDHES = IDTRUPIDOKLIDHES;
            this.DTMODIFIKIMI = DTMODIFIKIMI;
            this.IDPERDORUESI = IDPERDORUESI;
            this.IDNDERMARJE = IDNDERMARJE;
            this.KODI = KODI;
            this.PERSHKRIMI = PERSHKRIMI;
            this.SERIALKOD = SERIALKOD;
            this.KODKODIFIKIMI = KODKODIFIKIMI;
            this.NRSTATUSI = NRSTATUSI;
        }
        public clsLidhjeObjekteWebGis(int gid, string the_geom, int IDLAYER, int IDLAYERSTYPE, int IDMAGAZINA, int IDKODIFIKIMI, int IDARTIKULLI, int IDSERIALI, int IDKOKADOK, int IDTRUPIDOKLIDHES, string DTMODIFIKIMI, int IDPERDORUESI, int IDNDERMARJE, string KODI, string PERSHKRIMI, string SERIALKOD, string KODKODIFIKIMI, string Veprimi, int GidPrindi, int NRSTATUSI)
        {
            this.gid = gid;
            this.the_geom = the_geom;
            this.IDLAYER = IDLAYER;
            this.IDLAYERSTYPE = IDLAYERSTYPE;
            this.IDMAGAZINA = IDMAGAZINA;
            this.IDKODIFIKIMI = IDKODIFIKIMI;
            this.IDARTIKULLI = IDARTIKULLI;
            this.IDSERIALI = IDSERIALI;
            this.IDKOKADOK = IDKOKADOK;
            this.IDTRUPIDOKLIDHES = IDTRUPIDOKLIDHES;
            this.DTMODIFIKIMI = DateTime.Now;
            this.IDPERDORUESI = IDPERDORUESI;
            this.IDNDERMARJE = IDNDERMARJE;
            this.KODI = KODI;
            this.PERSHKRIMI = PERSHKRIMI;
            this.SERIALKOD = SERIALKOD;
            this.KODKODIFIKIMI = KODKODIFIKIMI;
            this.NRSTATUSI = NRSTATUSI;
            this.Veprimi = Veprimi;
            this.GidPrindi = GidPrindi;
        }
       
        #endregion

        #region Properties
        public int gid
        {
            get { return varGid; }
            set { varGid = value; }
        }
        public string the_geom
        {
            get { return varThe_geom; }
            set { varThe_geom = value; }
        }
        public int IDLAYER
        {
            get { return varIDLAYER; }
            set { varIDLAYER = value; }
        }
        public int IDLAYERSTYPE
        {
            get { return varIDLAYERSTYPE; }
            set { varIDLAYERSTYPE = value; }
        }
        public int IDMAGAZINA
        {
            get { return varIDMAGAZINA; }
            set { varIDMAGAZINA = value; }
        }
        public int IDKODIFIKIMI
        {
            get { return varIDKODIFIKIMI; }
            set { varIDKODIFIKIMI = value; }
        }
        public int IDARTIKULLI
        {
            get { return varIDARTIKULLI; }
            set { varIDARTIKULLI = value; }
        }
        public int IDSERIALI
        {
            get { return varIDSERIALI; }
            set { varIDSERIALI = value; }
        }
        public int IDKOKADOK
        {
            get { return varIDKOKADOK; }
            set { varIDKOKADOK = value; }
        }
        public int IDTRUPIDOKLIDHES
        {
            get { return varIDTRUPIDOKLIDHES; }
            set { varIDTRUPIDOKLIDHES = value; }
        }
        public DateTime DTMODIFIKIMI
        {
            get { return varDTMODIFIKIMI; }
            set { varDTMODIFIKIMI = value; }
        }
        public int IDPERDORUESI
        {
            get { return varIDPERDORUESI; }
            set { varIDPERDORUESI = value; }
        }
        public int IDNDERMARJE
        {
            get { return varIDNDERMARJE; }
            set { varIDNDERMARJE = value; }
        }
        public string KODI
        {
            get { return varKODI; }
            set { varKODI = value; }
        }
        public string PERSHKRIMI
        {
            get { return varPERSHKRIMI; }
            set { varPERSHKRIMI = value; }
        }
        public string SERIALKOD
        {
            get { return varSERIALKOD; }
            set { varSERIALKOD = value; }
        }
        public string KODKODIFIKIMI
        {
            get { return varKODKODIFIKIMI; }
            set { varKODKODIFIKIMI = value; }
        }

        public string Veprimi
        {
            get { return varVeprimi; }
            set { varVeprimi = value; }
        }
        public int GidPrindi
        {
            get { return varGidPrindi; }
            set { varGidPrindi = value; }
        }

        public int NRSTATUSI
        {
            get { return varNrStatusi; }
            set { varNrStatusi = value; }
        }
        #endregion

        #region Metoda Internal
        internal bool mbushLidhjeObjekteWebGis(DataRow db)
            {
                if (db != null)
                {
                    try
                    {
                        int.TryParse(db["gid"].ToString(), out varGid);                                     //1
                        varThe_geom = db["the_geom"].ToString();                                            //2
                        int.TryParse(db["IDLAYER"].ToString(), out varIDLAYER);                             //3
                        int.TryParse(db["IDLAYERSTYPE"].ToString(), out varIDLAYERSTYPE);                   //4
                        int.TryParse(db["IDMAGAZINA"].ToString(), out varIDMAGAZINA);                       //5
                        int.TryParse(db["IDKODIFIKIMI"].ToString(), out varIDKODIFIKIMI);                   //6
                        int.TryParse(db["IDARTIKULLI"].ToString(), out varIDARTIKULLI);                     //7
                        int.TryParse(db["IDSERIALI"].ToString(), out varIDSERIALI);                         //8
                        int.TryParse(db["IDKOKADOK"].ToString(), out varIDKOKADOK);                         //9
                        int.TryParse(db["IDTRUPIDOKLIDHES"].ToString(), out varIDTRUPIDOKLIDHES);           //10
                        DateTime.TryParse(db["DTMODIFIKIMI"].ToString(), out varDTMODIFIKIMI);              //11
                        int.TryParse(db["IDPERDORUESI"].ToString(), out varIDPERDORUESI);                   //12
                        int.TryParse(db["IDNDERMARJE"].ToString(), out varIDNDERMARJE);                     //13
                        varKODI = db["KODI"].ToString();                                                    //14
                        varPERSHKRIMI = db["PERSHKRIMI"].ToString();                                        //15
                        varSERIALKOD = db["SERIALKOD"].ToString();                                          //16
                        varKODKODIFIKIMI = db["KODKODIFIKIMI"].ToString();                                  //17
                        return true;
                    }
                    catch (InvalidCastException)
                    {
                        throw new Exception("ERROR: Gabim gjate marrjes se skedareve nga db-ja");
                    }
                }
                else
                    return false;
            }
        #endregion

        #region Medoda Publike
        public void mbushObjekteNgaJS(Dictionary<string, object> tempObjekti)
        {
            this.gid = int.Parse(tempObjekti["gid"].ToString());
            this.the_geom = tempObjekti["the_geom"].ToString();
            this.IDLAYERSTYPE = int.Parse(tempObjekti["IDLAYERSTYPE"].ToString());
            this.IDMAGAZINA = int.Parse(tempObjekti["IDMAGAZINA"].ToString());
            this.IDKODIFIKIMI = int.Parse(tempObjekti["IDKODIFIKIMI"].ToString());
            this.IDARTIKULLI = int.Parse(tempObjekti["IDARTIKULLI"].ToString());
            this.IDSERIALI = int.Parse(tempObjekti["IDSERIALI"].ToString());
            this.IDKOKADOK = int.Parse(tempObjekti["IDKOKADOK"].ToString());
            this.IDTRUPIDOKLIDHES = int.Parse(tempObjekti["IDTRUPIDOKLIDHES"].ToString());
            this.DTMODIFIKIMI = DateTime.Parse(tempObjekti["DTMODIFIKIMI"].ToString());
            this.IDPERDORUESI = int.Parse(tempObjekti["IDPERDORUESI"].ToString());
            this.IDNDERMARJE = int.Parse(tempObjekti["IDNDERMARJE"].ToString());
            this.KODI = tempObjekti["KODI"].ToString();
            this.PERSHKRIMI = tempObjekti["PERSHKRIMI"].ToString();
            this.SERIALKOD = tempObjekti["SERIALKOD"].ToString();
            this.KODKODIFIKIMI = tempObjekti["KODKODIFIKIMI"].ToString();

            this.Veprimi = tempObjekti["Veprimi"].ToString();
            this.GidPrindi = int.Parse(tempObjekti["GidPrindi"].ToString());
        }
        #endregion
}
}

