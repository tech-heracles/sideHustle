using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbInventari
{ /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nivelet e zbritjes
    ///  (Te dhenat  merren nga tabela : T_NIVELZBRITJE)
    /// </summary>
    public class clsNivelZbritje
    {
        #region Atribute

        private int idNivelZbritje;
        private string kodNivelZbritje;
        private string pershkrimNivelZbritje;
        private int idPrindi;     
        private int prioritetiNivelZbritje;
        private int idPerdoruesi;
        //private int idNderViti;
        private int idNdermarje;
        private int idKonfig;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti; 
        #endregion
        
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNivelZbritje
        {
            get { return idNivelZbritje; }
            set { idNivelZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos kodin e nivelit te zbritjes.
        /// </summary>
        public String KodNivelZbritje
        {
            get { return kodNivelZbritje; }
            set { kodNivelZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e nivelit te zbritjes.
        /// </summary>
        public String PershkrimNivelZbritje
        {
            get { return pershkrimNivelZbritje; }
            set { pershkrimNivelZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e prindit.
        /// </summary>
        public int IdPrindi
        {
            get { return idPrindi; }
            set { idPrindi = value; }
        }
        /// <summary>
        /// Kthen/Vendos prioritetin e nivelit te zbritjes.
        /// </summary>
        public int PrioritetiNivelZbritje
        {
            get { return prioritetiNivelZbritje; }
            set { prioritetiNivelZbritje = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarje vitit
        /// </summary>
        //public int IdNderViti
        //{
        //    get { return idNderViti; }
        //    set { idNderViti = value; }
        //}
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }
           /// <summary>
        /// Kthen/Vendos ID-ne e konfigurimit.
        /// </summary>
        public int IdKonfig
        {
            get { return idKonfig; }
            set { idKonfig = value; }
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
     
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idNivelZbritje"> id ritese e nivelit te zbritjes</param>
        /// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        /// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idPrindi"> id e prindit</param>
        /// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        public clsNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok)
        {
            this.idNivelZbritje = idNivelZbritje;
            this.kodNivelZbritje = kodNivelZbritje;
            this.pershkrimNivelZbritje = pershkrimNivelZbritje;
            this.idPrindi = idPrindi;         
            this.prioritetiNivelZbritje = prioritetiNivelZbritje;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idnderm;
            this.idKonfig = idkonfig;
            this.idStatusDok = idstatusdok;
        }
        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        /// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idPrindi"> id e prindit</param>
        /// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        public clsNivelZbritje(string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi,   int prioritetiNivelZbritje, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok)
        {

            this.kodNivelZbritje = kodNivelZbritje;
            this.pershkrimNivelZbritje = pershkrimNivelZbritje;
            this.idPrindi = idPrindi;          
            this.prioritetiNivelZbritje = prioritetiNivelZbritje;
            this.idPerdoruesi = idPerdoruesi;
            //this.idNderViti = idNderViti;
            this.idNdermarje = idnderm; 
            this.idKonfig = idkonfig;
            this.idStatusDok = idstatusdok;
        }
        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsNivelZbritje()
        {
        }

        public clsNivelZbritje(DataRow rreshti)
        {
            
            mbushNivelZbritje(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruajNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idnderm,  int idkonfig, int idstatusdok)
        { //metoda per ruajtjen e nivelit te zbritjeve
            bool ruaj = false;
            clsNivelZbritje nivelZbritje = new clsNivelZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idnderm,idkonfig, idstatusdok);
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseInventari db = new clsDatabaseInventari();
            ruaj = db.ruajNivZbritje(out idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idnderm,idkonfig, idstatusdok);
            db.Dispose();
            this.IdNivelZbritje = idNivelZbritje;
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);            
            int idregj = DbAdmin.clsListeAmbjenteCeljeRegjistrim.ktheIdCR("CNZ", new DbAdmin.clsDatabaseAdmin ());
            //int idregj = dbAdmin.ktheListeAmbjentiCeljeRegjistrim("CNZ")[0].IdCR;
            //int idlloji = dbAdmin.ktheLlojKodi("Kod")[0].IdLlojKodi;
            int idlloji = DbAdmin.clsLlojKodi.ktheIDLlojKodi("Kod",new DbAdmin.clsDatabaseAdmin ());
            
            //if (new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idnderm).IdLidhjeNrAuto!=0)
            //{
            //    DbAdmin.clsACRNumraAutomatike ACR = new DbAdmin.clsACRNumraAutomatike(idregj, idlloji, idnderm);
            //    DbAdmin.clsNrAutom NrAutom = new DbAdmin.clsNrAutom(ACR.IdNumraAutoLidhje);
            //    //DbAdmin.clsNrAutom NrAutom = dbAdmin.ktheNrAutom(ACR.IdNumraAutoLidhje)[0];
            //    int karakteremajtas = NrAutom.MajtasNrAutom.Length;
            //    int karakteredjathtas = ACR.VleraFunditLidhje.Length - NrAutom.DjathtasNrAutom.Length - karakteremajtas;
            //    string vle = ACR.VleraFunditLidhje.Substring(karakteremajtas, karakteredjathtas);
            //    string vlera = NrAutom.gjeneroNumrinAutomatikPasardhes(vle);
            //    ACR.VleraFunditLidhje = vlera;
            //    ACR.modifiko();
            //}

            if (ruaj == true)
            {
                if (nivelZbritje.IdPrindi != 0)
                {
                    //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
                    clsNivelZbritje clsNivele = new clsNivelZbritje();
                    clsNivelZbritje nivelPrindi = new clsNivelZbritje();
                    nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
                    nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
                    clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje);
                    //colNivele.Add(merrNivelZbritje (nivelPrindi)[0] );
                    if (clsNivele != null)
                        //if (colNivele.Count > 0)
                        if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
                        //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
                        {
                            //foreach (clsNivelZbritje n in colNivele)
                            if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                            {
                                clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
                                modifikoNivelZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNdermarje, clsNivele.IdKonfig, clsNivele.idStatusDok);
                                //modifikoNivelZbritje(clsNivele);
                            }
                        }
                }
            }
            return mesazh;
        }
        /// <summary>
        /// Ruan objektin nivel zbritje ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ruajNivelZbritje"/> 
        /// </summary>
        /// <param name="idndermarje"> id e ndermarjes </param>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor: clsMesazh ruajNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm, int idndermarje)", true)]
        //public clsMesazh ruaj(int idndermarje)
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_ruajt = data.ruajNivelZbritje(this.IdNivelZbritje, this.KodNivelZbritje, this.PershkrimNivelZbritje, this.IdPrindi, this.PrioritetiNivelZbritje, this.IdPerdoruesi, this.IdNderViti, this.IdNdermarje, idndermarje);
        //    //clsMesazh u_ruajt = data.ruajNivelZbritje(this, idndermarje );
        //    return u_ruajt;
        //}

        /// <summary>
        /// modifikon nje nivel zbritje duke ndryshuar prioritetet e te gjithe nivele te tjera te te njejtit prind ne varesi te ndryshimit te nivelit qe u modifikua
        /// </summary>
        /// <param name="idNivelZbritje"> id ritese e nivelit te zbritjes</param>
        /// <param name="kodNivelZbritje"> kodi i nivelit te zbritjes</param>
        /// <param name="pershkrimNivelZbritje"> pershkrimi i nivelit te zbritjes</param>
        /// <param name="idPrindi"> id e prindit</param>
        /// <param name="prioritetiNivelZbritje"> prioriteti i nivelit te zbritjes</param>
        /// <param name="idPerdoruesi"> id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNderViti"> id e ndermarje vitit</param>
        /// <param name="idnderm"> id e ndermarjes</param>
        /// <returns> kthen nje obj clsMesazh per te identifikuar statusin e modifikimit se te dhenave ne DB</returns>
        public clsMesazh modifikoNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idnderm, int idkonfig, int idstatusdok)
        {//metoda per modifikimin e nivelit te zbritje
            bool ruaj = false;
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseInventari db = new clsDatabaseInventari();
            clsNivelZbritje nivelZbritje = new clsNivelZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idnderm, idkonfig, idstatusdok);
            clsNivelZbritje nivelipara = new clsNivelZbritje();
            nivelipara.mbushNivelZbritje(nivelZbritje.IdNivelZbritje);
            //clsNivelZbritje nivelipara = merrNivelZbritje(nivelZbritje)[0];
            ruaj = db.modifikoNivZbritje(idNivelZbritje, kodNivelZbritje, pershkrimNivelZbritje, idPrindi, prioritetiNivelZbritje, idPerdoruesi, idnderm, idkonfig, idstatusdok);
            if (ruaj == true)
            {
                mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

                if (nivelZbritje.IdPrindi != 0)
                {
                    if (nivelipara.IdPrindi == nivelZbritje.IdPrindi)
                    {
                        if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
                        {
                            //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
                            clsNivelZbritje clsNivele = new clsNivelZbritje();
                            clsNivelZbritje nivelPrindi = new clsNivelZbritje();
                            nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
                            nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
                            clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje);
                            //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
                            if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
                            {
                                //foreach (clsNivelZbritje n in colNivele)
                                if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                                {
                                    clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
                                    db.modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNdermarje,clsNivele.IdKonfig, clsNivele.idStatusDok);
                                    //modifikoNivZbritje(clsNivele);
                                }

                            }
                            else
                            {
                                //foreach (clsNivelZbritje n in colNivele)
                                if (clsNivele.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && clsNivele.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                                {
                                    clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje - 1;
                                    db.modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNdermarje, clsNivele.IdKonfig, clsNivele.idStatusDok);
                                    //modifikoNivZbritje(clsNivele);
                                }
                            }

                        }
                    }
                    else
                    {
                        //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdPrindi);
                        clsNivelZbritje clsNivele = new clsNivelZbritje();
                        clsNivelZbritje nivelPrindi = new clsNivelZbritje();
                        nivelPrindi.IdNivelZbritje = nivelZbritje.IdPrindi;
                        nivelPrindi.IdNdermarje = nivelZbritje.IdNdermarje;
                        clsNivele.mbushNivelZbritje(nivelPrindi.IdNivelZbritje);
                        //colNivele.Add(merrNivelZbritje(nivelPrindi)[0]);
                        if (clsNivele != null)
                            //if (colNivele.Count > 0)
                            if (nivelZbritje.PrioritetiNivelZbritje <= clsNivele.PrioritetiNivelZbritje)
                            //if (nivelZbritje.PrioritetiNivelZbritje <= colNivele[0].PrioritetiNivelZbritje)
                            {
                                //foreach (clsNivelZbritje n in colNivele)
                                if (clsNivele.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && clsNivele.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                                {
                                    clsNivele.PrioritetiNivelZbritje = clsNivele.PrioritetiNivelZbritje + 1;
                                    db.modifikoNivZbritje(clsNivele.IdNivelZbritje, clsNivele.KodNivelZbritje, clsNivele.PershkrimNivelZbritje, clsNivele.IdPrindi, clsNivele.PrioritetiNivelZbritje, clsNivele.IdPerdoruesi, clsNivele.IdNdermarje, clsNivele.IdKonfig, clsNivele.idStatusDok);
                                    //modifikoNivZbritje(clsNivele);
                                }

                            }
                    }
                }
                else
                {
                    if (nivelipara.PrioritetiNivelZbritje != nivelZbritje.PrioritetiNivelZbritje)
                    {
                        colNiveleZbritjesh colNivele = new colNiveleZbritjesh();
                        colNivele.mbushNiveleZbritjeshSipasPrindit(nivelZbritje.IdNivelZbritje);
                        //colNiveleZbritjesh colNivele = merrNivelZbritjeSipasPrindit(nivelZbritje.IdNivelZbritje);

                        if (nivelZbritje.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje)
                        {
                            foreach (clsNivelZbritje n in colNivele)
                                if (n.PrioritetiNivelZbritje >= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje < nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                                {
                                    n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje + 1;
                                    db.modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNdermarje,n.IdKonfig, n.idStatusDok);
                                    //modifikoNivZbritje(n);
                                }

                        }
                        else
                        {
                            foreach (clsNivelZbritje n in colNivele)
                                if (n.PrioritetiNivelZbritje <= nivelZbritje.PrioritetiNivelZbritje && n.PrioritetiNivelZbritje > nivelipara.PrioritetiNivelZbritje && n.KodNivelZbritje != nivelZbritje.KodNivelZbritje)
                                {
                                    n.PrioritetiNivelZbritje = n.PrioritetiNivelZbritje - 1;
                                    db.modifikoNivZbritje(n.IdNivelZbritje, n.KodNivelZbritje, n.PershkrimNivelZbritje, n.IdPrindi, n.PrioritetiNivelZbritje, n.IdPerdoruesi, n.IdNdermarje,n.IdKonfig,n.idStatusDok);
                                    //modifikoNivZbritje(n);
                                }
                        }

                    }
                }
            }
            db.Dispose();
            return mesazh;
        }
        /// <summary>
        /// Modifikon objektin nivel zbritje ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.modifikoNivelZbritje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        //[Obsolete("Perdor nga klasa perkatese: modifikoNivelZbritje(int idNivelZbritje, string kodNivelZbritje, string pershkrimNivelZbritje, int idPrindi, int prioritetiNivelZbritje, int idPerdoruesi, int idNderViti, int idnderm)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseInventari data = new clsDatabaseInventari();
        //    clsMesazh u_modifikua = data.modifikoNivelZbritje(this.IdNivelZbritje, this.KodNivelZbritje, this.PershkrimNivelZbritje, this.IdPrindi, this.PrioritetiNivelZbritje, this.IdPerdoruesi, this.IdNderViti, this.IdNdermarje);
        //    //clsMesazh u_modifikua = data.modifikoNivelZbritje(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// Fshin objektin nivel zbritje ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.fshiNivelZbritje"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            clsMesazh u_fshi = data.fshiNivelZbritjeStatus(this.IdNivelZbritje, this.idPerdoruesi);
            data.Dispose();
            //clsMesazh u_fshi = data.fshiNivelZbritje(this);
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin nivel zbritje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheNivelZbritje"/> 
        /// </summary>
        public void merr()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            data.ktheNivelZbritje(this.IdNivelZbritje);
            data.Dispose();
            //data.merrNivelZbritje(this);
        }

        /// <summary>
        /// Merr datatable nivele zbritje  te nje ndermarje nga tabela perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbInventari.clsDatabaseInventari.ktheGjitheNiveleZbritjeshSipasNdermarjes"/> 
        /// </summary>
        /// <returns > nje datatable me te gjithe nivelet e cmimeve te kesaj ndermarje</returns>
        public DataTable merriTeGjithe()
        {
            clsDatabaseInventari data = new clsDatabaseInventari();
            DataTable teGjithe = data.ktheGjitheNiveleCmimeshSipasNdermarjes(this.IdNdermarje);
            data.Dispose();
            return teGjithe;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e nivelit te zbritjes sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi">kodi i artikullit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e nivelit te zbritjes</returns>
        public static int ktheIdNivelZbritje(string kodi, int idnderm)
        {
            clsDatabaseInventari dbNivelZbritje = new clsDatabaseInventari();
            int idNivel = (dbNivelZbritje.ktheNivelZbritjeSipasKodit(kodi, idnderm));
            dbNivelZbritje.Dispose();
            return idNivel;
        }

        /// <summary>
        /// Metode e klases, jo e objektit. Kthen nje id e nivelit te zbritjes sipas pershkrimit dhe idndermarrjes
        /// </summary>
        /// <param name="pershkrim">pershkrimi i artikullit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>id e nivelit te zbritjes</returns>
        public static int ktheIdNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)
        {
            clsDatabaseInventari dbNiveleZbritje = new clsDatabaseInventari();
            int idNivel = (dbNiveleZbritje.ktheIDNivelZbritjeSipasPershkrimit(pershkrim, idnderm));
            dbNiveleZbritje.Dispose();
            return idNivel;
        }

        /// <summary>
        /// mbush nivelin e zbritjes sipas pershkrimit
        /// </summary>
        /// <param name="pershkrim">pershkrimi i artikullit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        public bool mbushNivelZbritjeSipasPershkrimit(string pershkrim, int idnderm)
        {
            clsDatabaseInventari dbNiveleZbritje = new clsDatabaseInventari();
            bool sukses = mbushNivelZbritje(dbNiveleZbritje.ktheNivelZbritjeSipasPershkrimit(pershkrim, idnderm));
            dbNiveleZbritje.Dispose();
            return sukses;
        }

    /// <summary>
    /// mbush nivelin e zbritjes sipas id se nivelit te zbritjes dhe id se ndermarrjes
    /// </summary>
    /// <param name="idNivelZbritje">id e nivelit te zbritjes</param>
    /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
    public bool mbushNivelZbritje(int idNivelZbritje)
        {
            using (clsDatabaseInventari dbNiveleZbritje = new clsDatabaseInventari())
            {
                return mbushNivelZbritje(dbNiveleZbritje.ktheNivelZbritje(idNivelZbritje));
            }
        }
        

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e nivelit te zbritjes nga databaza
        /// </summary>
        /// <param name="dbDataRowNivelZbritje">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushNivelZbritje(DataRow dbDataRowNivelZbritje)
        {
            if (dbDataRowNivelZbritje != null)
            {

                try
                {
                    int.TryParse(dbDataRowNivelZbritje["IDNIVELZBRITJE"].ToString(), out idNivelZbritje);
                    kodNivelZbritje = dbDataRowNivelZbritje["KODNIVELZBRITJE"].ToString();
                    pershkrimNivelZbritje = dbDataRowNivelZbritje["PERSHKRIMNIVELZBRITJE"].ToString();
                    int.TryParse(dbDataRowNivelZbritje["IDPRINDI"].ToString(), out idPrindi);
                    int.TryParse(dbDataRowNivelZbritje["PRIORITETINIVELZBRITJE"].ToString(), out prioritetiNivelZbritje);
                    int.TryParse(dbDataRowNivelZbritje["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    //int.TryParse(dbDataRowNivelZbritje["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowNivelZbritje["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowNivelZbritje["IDKONFIG"].ToString(), out idKonfig);
                    int.TryParse(dbDataRowNivelZbritje["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowNivelZbritje["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNivelZbritje["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se nivelit te zbritjes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}