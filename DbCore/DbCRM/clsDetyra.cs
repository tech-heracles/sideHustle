using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using System;
using System.Collections.Generic;
using System.Data;

namespace DbCore.DbCRM
{
    public class clsDetyra
    {
        #region Atribute

        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private int idDetyre;
        private int idKrijuesi;
        private int idModifikues;
        private int idNdermarje;        
        private int idStatusDok;
        private int kategoria;
        private string kodi;
        private string pershkrimi;
        private int rendesia;
        private string autorizimet;
        #endregion Atribute

        #region Konstruktoret

        public clsDetyra()
        {
        }

        public clsDetyra(int idDetyre)
        {
            clsDatabaseCRM dbCrm = new clsDatabaseCRM();
            mbushDetyre(ktheDetyre(idDetyre));
            dbCrm.Dispose();
        }

        public clsDetyra(string kodDetyre,string autorizimet, int rendesia, int kategoria, string pershkrim, int idKrijuesi, DateTime dtKrijimi, int idModifikimi, DateTime dtModifikimi, int idStatusDok, int idNdermarje, bool shtim)
        {
            this.kodi = kodDetyre;
            this.rendesia = rendesia;
            this.kategoria = kategoria;
            this.pershkrimi = pershkrim;
            this.idKrijuesi = idKrijuesi;
            this.dtKrijimi = dtKrijimi;
            this.idModifikues = idModifikimi;
            this.dtModifikimi = dtModifikimi;
            this.idStatusDok = idStatusDok;
            this.idNdermarje = idNdermarje;
            this.autorizimet = autorizimet;
            clsMesazh mesazh = kontrolloDetyre(shtim);
            if (!mesazh.Status)
                throw new DbCore.MyException(mesazh.PershkrimMesazhi);
        }

        #endregion Konstruktoret

        #region Properties

        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }
        }

        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }

        public int IdDetyre
        {
            get { return idDetyre; }
            set { idDetyre = value; }
        }

        public int IdKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        public int IdModifikues
        {
            get { return idModifikues; }
            set { idModifikues = value; }
        }

        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        public int Kategoria { get { return kategoria; } set { kategoria = value; } }

        public string Kodi { get { return kodi; } set { kodi = value; } }

        public string Pershkrim { get { return pershkrimi; } set { pershkrimi = value; } }

        public int Rendesia { get { return rendesia; } set { rendesia = value; } }

        public string Autorizimet { get { return autorizimet; } set { autorizimet = value; } }
        #endregion Properties

        #region Metoda Publike

        public clsMesazh kontrolloDetyre(bool shtim)
        {
            if (String.IsNullOrEmpty(this.kodi))
                return new clsMesazh(false, "Kodi nuk duhet te jete bosh!");

            if (!clsFunksione.LejoVetemAlphaNumerik(this.kodi))
                return new clsMesazh(false, "Kodi nuk eshte i rregullt!");
            if (String.IsNullOrEmpty(this.pershkrimi))
                return new clsMesazh(false, "Pershkrimi nuk duhet te jete bosh!");
            if (shtim && ekzistonDetyreSipasKodit(this.kodi, idNdermarje))
                return new clsMesazh(false, "Ekziston nje detyre me kete kod!");
            return new clsMesazh(true, "Kontrollet kaluan me sukses!");
        }

        public static clsMesazh fshi(int idDetyre,int idPerdoruesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            clsMesazh u_fshi = data.fshiDetyre(idDetyre,idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        public static DataRow ktheDetyre(int idDetyre)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            DataRow dr = data.merrDetyreSipasID(idDetyre);
            data.Dispose();
            return dr;
        }

        public clsMesazh ruaj(IDictionary<string, object> hfNrAuto)
        {
            clsMesazh u_ruajt;
            clsDatabaseCRM data = new clsDatabaseCRM();
            bool kaNdryshimNumri;
            data.beginTransaksion();
            try
            {
                u_ruajt = kontrolloDetyre(out kaNdryshimNumri, data, hfNrAuto, false);
                if (!u_ruajt.Status)
                {
                    data.rollbackTransaksion();
                    return u_ruajt;
                }
                u_ruajt = ruaj(data);
                if (!u_ruajt.Status)
                {
                    data.rollbackTransaksion();
                    return u_ruajt;
                }
                data.commitTransaksion();
                return u_ruajt;
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            }
        }

        private clsMesazh kontrolloDetyre(out bool kaNdryshimNrAuto, clsDatabaseCRM db, IDictionary<string, object> hfNrAutoDet, bool modifikim)
        {
            kaNdryshimNrAuto = false;
            if (!modifikim)
            {
                clsMesazh mes = new clsMesazh();
                if (hfNrAutoDet != null)
                {
                    mes = kontrolloNrAutoDetyre(out kaNdryshimNrAuto, db, hfNrAutoDet);
                    if (!mes.Status)
                        return mes;
                }
                return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
            }
            return new clsMesazh(true, "Kontrollet u kaluan me sukses");
        }

        private clsMesazh kontrolloNrAutoDetyre(out bool kaNdryshimNumri, clsDatabaseCRM db, IDictionary<string, object> hfNrAutoDet)
        {
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
            List<NrAuto> list = DbAdmin.clsNrAutom.kontrollogjithenumrat(dbadm, hfNrAutoDet, DateTime.Today);
            if (NrAuto.ktheVlerenEre(list, "KodDetyra") != "")
                this.kodi = NrAuto.ktheVlerenEre(list, "KodDetyra");
            DbCore.clsMesazh mes = NrAuto.ruajvlera(out kaNdryshimNumri, list, DateTime.Today, this.idKrijuesi, this.idNdermarje, dbadm);
            return new clsMesazh(mes.Status, mes.PershkrimMesazhi);
        }

        public clsMesazh modifiko(int idperdoruesi)
        {
            clsDatabaseCRM data = new clsDatabaseCRM();
            data.beginTransaksion();
            clsMesazh u_modifikua = modifiko(data,idperdoruesi );
            if (u_modifikua.Status)
                data.commitTransaksion();
            else
                data.rollbackTransaksion();
            return u_modifikua;
        }
               
       public static bool ekzistonDetyreSipasKodit(string kodi, int idNdermarrje)
       {
           clsDatabaseCRM dbCrm = new clsDatabaseCRM();
           bool ekziston = dbCrm.ekzistonDetyreMeKeteKod(idNdermarrje, kodi);
           dbCrm.Dispose();
           return ekziston;
       }



        #endregion Metoda Publike

        #region Metoda Private
       
       private bool mbushDetyre(int idDetyre)
       {
           clsDatabaseCRM data = new clsDatabaseCRM();
           bool sukses = mbushDetyre(data.merrDetyreSipasID(idDetyre));
           data.Dispose();
           return sukses;
       }

        private clsMesazh ruaj(clsDatabaseCRM dbCRM)
        {
            if (dbCRM.ekzistonDetyreMeKeteKod(this.idNdermarje,this.kodi))
                return new clsMesazh(false, "Ekziston nje detyre me kete kod!");
            clsMesazh u_ruajt = dbCRM.ruajDetyre(out this.idDetyre, this.Rendesia, this.Kategoria, this.Kodi, this.idKrijuesi,this.idStatusDok, this.pershkrimi, this.idNdermarje);

            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbCRM);
            if (Autorizimet != "")
            {
                colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
                string[] pars1 = Autorizimet.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    clsLidhjeAutorizim lidhje = new clsLidhjeAutorizim();
                    lidhje.IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]);
                    //lidhje.IdAutorizimeKoka = new DbAdmin.clsDatabaseAdmin().ktheAutorizim(pars1[i])[0].IdAutorizimKoka;
                    colLidhjet.Add(lidhje);
                }
                DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbCRM );
                foreach (clsLidhjeAutorizim o in colLidhjet)
                {
                    o.IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Detyra", dbKont);
                    o.IdLidhese = IdDetyre;
                    u_ruajt = dbAdmin.ruajLidhjeAutorizim(o.IdLidhjeAutorizim, o.IdLidhese, o.IdLloji, o.IdAutorizimeKoka, 1);
                    if (!u_ruajt.Status)
                    {
                        return u_ruajt;
                    }
                }
            }
            
            
            return u_ruajt;
        }

        private clsMesazh modifiko(clsDatabaseCRM dbCRM,int idperdoruesi)
        {
            clsMesazh mesazh = dbCRM.modifikoDetyre(idDetyre, this.Rendesia, this.Kategoria, this.Kodi, this.idModifikues, this.idStatusDok, this.pershkrimi, this.idNdermarje);
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbCRM);
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(IdDetyre, "Detyra", dbAdmin);
            colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
            if (autorizimet != "")
            {
                string[] pars1 = autorizimet.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    colLidhjet.Add(new clsLidhjeAutorizim() { IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]) });
                }
            }
            DbKontabiliteti.clsDatabaseKontabilitet dbKont = new DbKontabiliteti.clsDatabaseKontabilitet(dbCRM );
            for (int i = 0; i < colLidhjet.Count; i++)
            {
                int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                if (idAutorizimKoka == -1)
                    continue;
                colLidhjet[i].IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti("Detyra", dbKont);
                colLidhjet[i].IdLidhese = IdDetyre;
                clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                if (lidhjeNjejte != null)
                {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                    colLidhjetAutorizim.Remove(lidhjeNjejte);
                    continue;
                }
                mesazh = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                if (!mesazh.Status)
                    return mesazh;
            }
            //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
            mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, idperdoruesi, dbAdmin);
                                  
            return mesazh;
        }

        #endregion

        #region Metoda Internal

        internal bool mbushDetyre(DataRow dbDataRowDetyra)
        {
            if (dbDataRowDetyra != null)
            {
                try
                {
                    int.TryParse(dbDataRowDetyra["IDDETYRA"].ToString(), out idDetyre);
                    kodi = dbDataRowDetyra["KODI"].ToString();
                    int.TryParse(dbDataRowDetyra["IDKRIJUES"].ToString(), out idKrijuesi);
                    DateTime.TryParse(dbDataRowDetyra["DTKRIJIMI"].ToString(), out dtKrijimi);
                    int.TryParse(dbDataRowDetyra["IDMODIFIKUES"].ToString(), out idModifikues);
                    DateTime.TryParse(dbDataRowDetyra["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    int.TryParse(dbDataRowDetyra["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowDetyra["RENDESIA"].ToString(), out rendesia);
                    int.TryParse(dbDataRowDetyra["KATEGORIA"].ToString(), out kategoria);
                    pershkrimi = dbDataRowDetyra["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowDetyra["IDNDERMARRJE"].ToString(), out idNdermarje);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se fushave te detyres nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion Metoda Internal
    }
}