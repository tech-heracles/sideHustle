using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne koken e nje skeme kontabel
    ///  (Te dhenat  merren nga tabela : T_KOKASKEMAKONTAB)
    ///</remarks>
    public class clsSkemaKontabelKoka
    {
        #region Atribute

        private int idSkemaKontabelKoka;
        private String kodiSkemaKontabelKoka;
        private String pershkrimiSkemaKontabelKoka;
        private bool aktivSkemaKontabelKoka;
        private string idAutorizimSkemaKontabelKoka;
        private int idKursiSkemaKontabelKoka;
        private int nrAutoSkemaKontabelKoka;
        private int idNderViti;
        private int idPerdoruesi;
        private int idNdermarje;

        private colSkemaKontabelTrupi oColTrupi;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelKoka(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka,
                                     bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, 
                                        int idndermvit, int idperdoruesi, int idnderm)
        {
            idSkemaKontabelKoka = idskemakontabelkoka;
            kodiSkemaKontabelKoka = kodiskemakontabelkoka;
            pershkrimiSkemaKontabelKoka = pershkrimiskemakontabelkoka;
            aktivSkemaKontabelKoka = aktivskemakontabelkoka;         
            idKursiSkemaKontabelKoka = idkursiskemakontabelkoka;
            nrAutoSkemaKontabelKoka = nrautoskemakontabelkoka;
            idNderViti = idndermvit;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idnderm;
            oColTrupi = new colSkemaKontabelTrupi();
        }

        public clsSkemaKontabelKoka(String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka,
                                    bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, 
                                    int idndermvit,int idperdoruesi, int idnderm)
        {
            kodiSkemaKontabelKoka = kodiskemakontabelkoka;
            pershkrimiSkemaKontabelKoka = pershkrimiskemakontabelkoka;
            aktivSkemaKontabelKoka = aktivskemakontabelkoka;
            idKursiSkemaKontabelKoka = idkursiskemakontabelkoka;
            nrAutoSkemaKontabelKoka = nrautoskemakontabelkoka;
            idNderViti = idndermvit;
            idPerdoruesi = idperdoruesi;
            idNdermarje = idnderm;
        }

        /// <summary>
        /// konstruktori me 1 parameter string
        /// </summary>
        /// <param name="kodiSkemaKontabelKoka">kodi i kokes se skemes kontabel</param>
        public clsSkemaKontabelKoka(String kodiSkemaKontabelKoka)
        {
            clsDatabaseKontabilitet dbKokaSkemaKont = new clsDatabaseKontabilitet();
            mbushSkemaKontabelKoka(dbKokaSkemaKont.ktheSkemeKontabelSipasKodit(kodiSkemaKontabelKoka));
            dbKokaSkemaKont.Dispose();
        }

        /// <summary>
        /// konstruktori me 1 parameter int
        /// </summary>
        /// <param name="id">id e kokes se skemes kontabel</param>
        public clsSkemaKontabelKoka(int id)
        {
            clsDatabaseKontabilitet dbKokaSkemaKont = new clsDatabaseKontabilitet();
            mbushSkemaKontabelKoka(dbKokaSkemaKont.ktheSkemeKontabelSipasId(id));
            dbKokaSkemaKont.Dispose();
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaKontabelKoka()
        { 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdSkemaKontabelKoka
        {
            get { return idSkemaKontabelKoka; }
            set { idSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin e skemes kontabel
        /// </summary>
        public String KodiSkemaKontabelKoka
        {
            get { return kodiSkemaKontabelKoka; }
            set { kodiSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e skemes kontabel
        /// </summary>
        public String PershkrimiSkemaKontabelKoka
        {
            get { return pershkrimiSkemaKontabelKoka; }
            set { pershkrimiSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos faktin nese skema kontabel eshte aktive apo jo
        /// </summary>
        public bool AktivSkemaKontabelKoka
        {
            get { return aktivSkemaKontabelKoka; }
            set { aktivSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e autorizimit
        /// </summary>
        public string IdAutorizimSkemaKontabelKoka
        {
            get { return idAutorizimSkemaKontabelKoka; }
            set { idAutorizimSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kursit
        /// </summary>
        public int IdKursiSkemaKontabelKoka
        {
            get { return idKursiSkemaKontabelKoka; }
            set { idKursiSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin atomatik
        /// </summary>
        public int NrAutoSkemaKontabelKoka
        {
            get { return nrAutoSkemaKontabelKoka; }
            set { nrAutoSkemaKontabelKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsSkemaKontabelTrupi"/>
        /// </summary>
        public colSkemaKontabelTrupi OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese ndermarrje - vit
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }
        
        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan koken dhe trupin e skemes kontabel.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKokenTrupin"/>
        /// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        /// <param name="kodiskemakontabelkoka">kodi i kokes se skemes kotabel</param>
        /// <param name="pershkrimiskemakontabelkoka">pershrkimi i kokes se skemes kontabel</param>
        /// <param name="aktivskemakontabelkoka">nese koka e skemese kontabel eshte aktive ose jo</param>
        /// <param name="idkursiskemakontabelkoka">id e kursit te skemes kontabel</param>
        /// <param name="nrautoskemakontabelkoka">auto nr i skemes kontabel</param>
        /// <param name="idndermvit">id qe lidh ndermarrjen me vitin</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruajKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka,
            int idndermvit, int idperdoruesi, int idnderm, colSkemaKontabelTrupi oColTrupi)
        {
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            //dbManager.ConnectionString = dbManager.GetConnectionString();
            //dbManager.Open();
            
            bool statusVeprimi;
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            try
            {
            dbKont.beginTransaksion();
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            
                idskemakontabelkoka = dbKont.ruajKoken(out idskemakontabelkoka, kodiskemakontabelkoka, pershkrimiskemakontabelkoka, aktivskemakontabelkoka, idkursiskemakontabelkoka,
                    nrautoskemakontabelkoka, idndermvit, idperdoruesi, idnderm);

                if (idskemakontabelkoka == 0)
                    statusVeprimi = false;
                else statusVeprimi = true;

                clsSkemaKontabelKoka koka = new clsSkemaKontabelKoka(idskemakontabelkoka);
                if (statusVeprimi)
                {
                    foreach (clsSkemaKontabelTrupi o in oColTrupi)
                    {
                        if (statusVeprimi)
                        {
                            o.IdKoka = idskemakontabelkoka;
                            int idS;
                            mesazh = dbKont.ruajTrupin(out idS, o.DebiKrediSkemaKontabelTrupi, o.IdKoka, o.IdSkemaModel, o.IdLlogariSkemaKontabelTrupi);
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        if (koka.IdAutorizimSkemaKontabelKoka != "")
                        {
                            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbKont.merrManager());
                            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                            string[] pars1 = koka.IdAutorizimSkemaKontabelKoka.Split(',');
                            for (int i = 0; i < pars1.Length; i++)
                            {
                                DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                                lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                                //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                                colLidhjet.Add(lidhje);
                            }
                            foreach (DbAdmin.clsLidhjeAutorizim o in colLidhjet)
                            {
                                if (mesazhAdmin.Status)
                                {
                                    o.IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
                                    o.IdLidhese = idskemakontabelkoka;
                                    mesazhAdmin = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka);
                                }
                                else
                                {
                                    dbKont.rollbackTransaksion();
                                    
                                    mesazh.Status = false;
                                    mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                    return mesazh;
                                }
                            }
                        }
                        if (mesazhAdmin.Status)
                        {
                            dbKont.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, "Ruajtja perfundoi me sukses!");
                            return mesazh;
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }
        //[Obsolete("Perdor nga klasa perkatese: ruajKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        //    "int idndermvit, int idperdoruesi, int idnderm, colSkemaKontabelTrupi oColTrupi)", true)]
        //public clsMesazh ruaj()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    clsMesazh u_ruajt = data.ruajKokenTrupin(this);
        //    return u_ruajt;
        //}

        /// <summary>
        /// Modifikon koken dhe trupin e skemes kontabel.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoKokenTrupin"/>
        /// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        /// <param name="kodiskemakontabelkoka">kodi i kokes se skemes kotabel</param>
        /// <param name="pershkrimiskemakontabelkoka">pershrkimi i kokes se skemes kontabel</param>
        /// <param name="aktivskemakontabelkoka">nese koka e skemese kontabel eshte aktive ose jo</param>
        /// <param name="idkursiskemakontabelkoka">id e kursit te skemes kontabel</param>
        /// <param name="nrautoskemakontabelkoka">auto nr i skemes kontabel</param>
        /// <param name="idperdoruesi">id e perdoruesit</param>
        /// <param name="idnderm">id e ndermarrjes</param>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifikoKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka,
            int idperdoruesi, int idnderm, colSkemaKontabelTrupi oColTrupi)
        {
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");
            //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idskemakontabelkoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
            DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.colLidhjetAutorizim(idskemakontabelkoka, "SkematKontabel");
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            //dbManager.ConnectionString = dbManager.GetConnectionString();
            //dbManager.Open();
            

            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            try
            {
            dbKont.beginTransaksion();

            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            
                mesazh = dbKont.modifikoKoken(idskemakontabelkoka, kodiskemakontabelkoka, pershkrimiskemakontabelkoka, aktivskemakontabelkoka, idkursiskemakontabelkoka,
                    nrautoskemakontabelkoka, idperdoruesi, idnderm);
                if (mesazh.Status)
                {
                    mesazh = dbKont.fshiTrupinEKokes(idskemakontabelkoka);
                    if (mesazh.Status)
                    {
                        clsSkemaKontabelKoka koka = new clsSkemaKontabelKoka(idskemakontabelkoka);
                        foreach (clsSkemaKontabelTrupi o in oColTrupi)
                        {
                            if (mesazh.Status)
                            {
                                int idS;
                                o.IdKoka = idskemakontabelkoka;
                                mesazh = dbKont.ruajTrupin(out idS, o.DebiKrediSkemaKontabelTrupi, o.IdKoka, o.IdSkemaModel, o.IdLlogariSkemaKontabelTrupi);
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                return mesazh;
                            }
                        }
                        if (mesazh.Status)
                        {
                            DbAdmin.colLidhjetAutorizim colLidhjet = new DbAdmin.colLidhjetAutorizim();
                            if (koka.IdAutorizimSkemaKontabelKoka != "")
                            {

                                string[] pars1 = koka.IdAutorizimSkemaKontabelKoka.Split(',');
                                for (int i = 0; i < pars1.Length; i++)
                                {
                                    DbAdmin.clsLidhjeAutorizim lidhje = new DbAdmin.clsLidhjeAutorizim();
                                    lidhje.IdAutorizimeKoka = DbAdmin.clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                                    colLidhjet.Add(lidhje);
                                }

                            }
                            DbAdmin.clsDatabaseAdmin dbAdmin = new DbAdmin.clsDatabaseAdmin(dbKont.merrManager());
                            if (colLidhjetAutorizim.Count < colLidhjet.Count)//rasti kur jane shtuar rreshta trupi
                            {
                                for (int i = 0; i < colLidhjet.Count; i++)
                                {
                                    if (mesazhAdmin.Status)
                                    {
                                        colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
                                        colLidhjet[i].IdLidhese = idskemakontabelkoka;
                                        if (i < colLidhjetAutorizim.Count)
                                        {

                                            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
                                            mesazhAdmin = dbAdmin.modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
                                        }
                                        else
                                            mesazhAdmin = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
                                    }
                                    else
                                    {
                                        dbKont.rollbackTransaksion();
                                        
                                        mesazh.Status = false;
                                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                        return mesazh;
                                    }
                                }
                            }
                            else//rasti kur jane fshire rreshta
                            {
                                int count = 0;
                                for (int i = 0; i < colLidhjetAutorizim.Count; i++)
                                {
                                    if (mesazhAdmin.Status)
                                    {
                                        if (count < colLidhjet.Count)
                                        {
                                            colLidhjet[i].IdLloji = DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel");
                                            colLidhjet[i].IdLidhese = idskemakontabelkoka;

                                            colLidhjet[i].IdLidhjeAutorizim = colLidhjetAutorizim[i].IdLidhjeAutorizim;
                                            mesazhAdmin = dbAdmin.modifikoLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka);
                                        }
                                        else
                                        {
                                            mesazhAdmin = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[i].IdLidhjeAutorizim);
                                        }
                                        count++;
                                    }
                                    else
                                    {
                                        dbKont.rollbackTransaksion();
                                        
                                        mesazh.Status = false;
                                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                        return mesazh;
                                    }
                                }
                            }
                            if (mesazhAdmin.Status)
                            {
                                dbKont.commitTransaksion();
                                
                                mesazh = new clsMesazh(true, "Modifikimi perfundoi me sukses!");
                                return mesazh;
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                mesazh.Status = false;
                                mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                                return mesazh;
                            }
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }
        //[Obsolete("Perdor nga klasa perkatese: modifikoKokenTrupin(int idskemakontabelkoka, String kodiskemakontabelkoka, String pershkrimiskemakontabelkoka, bool aktivskemakontabelkoka, int idkursiskemakontabelkoka, int nrautoskemakontabelkoka, " +
        //    "int idperdoruesi, int idnderm, colSkemaKontabelTrupi oColTrupi)", true)]
        //public clsMesazh modifiko()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    clsMesazh u_modifikua = data.modifikoKokenTrupin(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// Kthen nje objekt koka skeme kontabiliteti duke filtruar sipas kodit.
        /// Therret funksionin<see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ktheSkemeKontabelSipasKodit"/>
        /// </summary>
        public clsSkemaKontabelKoka merrSkemeKontabelSipasKodit()
        {
            //clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
            //return data.merrSkemeKontabelSipasKodit(this);
            clsSkemaKontabelKoka data = new clsSkemaKontabelKoka(this.KodiSkemaKontabelKoka);
            return data;
        }

        /// <summary>
        /// Fshin koken dhe trupin e skemes kontabel.
        /// Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.fshiSkemeKontabel"/>
        /// <param name="idskemakontabelkoka">id e kokes se skemes kontabel</param>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshiSkemeKontabel(int idskemakontabelkoka)
        {
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematKontabel");
            DbAdmin.colLidhjetAutorizim autorizime = new DbAdmin.colLidhjetAutorizim(idskemakontabelkoka, "SkematKontabel");
            //DbAdmin.colLidhjetAutorizim autorizime = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idskemakontabelkoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
            //dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            //dbManager.ConnectionString = dbManager.GetConnectionString();
            //dbManager.Open();
            

            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            try
            {
            dbKont.beginTransaksion();

            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            

                mesazh = dbKont.fshiTrupinEKokes(idskemakontabelkoka);
                if (mesazh.Status)
                {
                    foreach (DbAdmin.clsLidhjeAutorizim o in autorizime)
                    {
                        if (mesazhAdmin.Status)
                        {
                            mesazhAdmin = o.fshi();
                        }

                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                            return mesazh;
                        }
                    }
                    if (mesazhAdmin.Status)
                    {
                        mesazh = dbKont.fshiKoken(idskemakontabelkoka);
                        if (mesazh.Status)
                        {
                            dbKont.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, "Ruajtja perfundoi me sukses!");
                            return mesazh;
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazhAdmin.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                else
                {
                    dbKont.rollbackTransaksion();
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }
        //[Obsolete("Perdor nga klasa perkatese: fshiSkemeKontabel(int idskemakontabelkoka)", true)]
        //public clsMesazh fshi()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    clsMesazh u_fshi = data.fshiSkemeKontabel(this);
        //    return u_fshi;
        //}

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kokat e skemave kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaKontabelKoka">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushSkemaKontabelKoka(DataRow dbDataRowSkemaKontabelKoka)
        {
            if (dbDataRowSkemaKontabelKoka != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABID"].ToString(), out idSkemaKontabelKoka);
                    kodiSkemaKontabelKoka = dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABKODI"].ToString();
                    pershkrimiSkemaKontabelKoka = dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABPERSHK"].ToString();
                    bool.TryParse(dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABAKTIV"].ToString(), out aktivSkemaKontabelKoka);
                    //colLlojeBuxhetesh colLloj = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet().merrLlojBuxhetiSipasKodit("SkematKontabel");
                    DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.colLidhjetAutorizim(idSkemaKontabelKoka, "SkematKontabel");
                    //DbAdmin.colLidhjetAutorizim lidhje = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idSkemaKontabelKoka, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematKontabel"));
                    if (lidhje.Count != 0)
                    {
                        idAutorizimSkemaKontabelKoka = DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[0].IdAutorizimeKoka);
                        //idAutorizimSkemaKontabelKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[0].IdAutorizimeKoka)[0].KodiAutorizim;
                        for (int i = 1; i < lidhje.Count; i++)
                            idAutorizimSkemaKontabelKoka += "," + DbAdmin.clsAutorizimKoka.ktheKodAutorizim(lidhje[i].IdAutorizimeKoka);
                            //idAutorizimSkemaKontabelKoka += "," + new DbAdmin.clsDatabaseAdmin().ktheAutorizim(lidhje[i].IdAutorizimeKoka)[0].KodiAutorizim;
                    }
                    else
                        idAutorizimSkemaKontabelKoka = "";
                    // koka.IdAutorizimSkemaKontabelKoka = int.Parse(rreshti[4].ToString());
                    int.TryParse(dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABIDKURSI"].ToString(), out idKursiSkemaKontabelKoka);
                    int.TryParse(dbDataRowSkemaKontabelKoka["KOKASKEMAKONTABNRAUTO"].ToString(), out nrAutoSkemaKontabelKoka);
                    int.TryParse(dbDataRowSkemaKontabelKoka["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowSkemaKontabelKoka["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokave te skemave kontabel nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
