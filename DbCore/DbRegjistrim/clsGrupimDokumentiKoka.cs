using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DbCore.DbAdmin;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Logging;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  grupimet e dokumentat
    ///  (Te dhenat  merren nga tabela : t_GRUPIMDOKUMENTASHKOKA)
    /// </summary>
    public class clsGrupimDokumentiKoka
    {
        #region Atributet

        private int idGrupimKoka;
        private string kodi;
        private string pershkrimi;
        private int grupi; //1 grupimi 1; 2 grupimi 2; 3 grupimi 3
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private string kategoria;
        private string lloji;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colGrupimDokumentiTrupi colTrupi;
        private DataRow rreshti;
        private string autorizimet;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupimKoka
        {
            get { return idGrupimKoka; }
            set { idGrupimKoka = value; }
        }

        public string Kategoria
        {
            get
            {
                return kategoria;
            }
            set
            {
                kategoria = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos kodin 
        /// </summary>
        public String Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        public string Lloji
        {
            get
            {
                return lloji;
            }
            set
            {
                lloji = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne pershkrimin .
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }


        /// <summary>
        /// Kthen/Vendos grupin 1,2,3.
        /// </summary>
        public int Grupi
        {
            get { return grupi; }
            set { grupi = value; }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin .
        /// </summary>
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten e krijimit.
        /// </summary>
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }
            set { dtKrijimi = value; }

        }

        /// <summary>
        /// Kthen/Vendos daten e modifikimit.
        /// </summary>
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }

        }
        public colGrupimDokumentiTrupi ColTrupi
        {
            get
            {
                return colTrupi;
            }
            set
            {
                colTrupi = value;
            }
        }
        public string Autorizimet
        {
            get { return autorizimet; }
            set { autorizimet = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsGrupimDokumentiKoka()
        {
        }

        /// <summary>
        /// konstruktor me parametra
        /// </summary>
        /// <param name="idgrupi"> id ritese e grupit</param>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="grupi"> grupi</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsGrupimDokumentiKoka(int idgrupi, string kodi, String pershkrimi, int grupi, int idPerdoruesi, int idNdermarje, int idStatusDok, string autorizimet)
        {
            this.idGrupimKoka = idgrupi;
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.grupi = grupi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.autorizimet = autorizimet;
        }

        /// <summary>
        /// konstruktor me parametra pa id 
        /// </summary>
        /// <param name="kodi">kodi </param>
        /// <param name="pershkrimi">pershkrimi </param>
        /// <param name="grupi"> grupi</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe ka kryer veprimin</param>
        /// <param name="idNdermarje">id e ndermarjes</param>
        /// <param name="idStatusDok">id e statusit </param>
        public clsGrupimDokumentiKoka(string kodi, String pershkrimi, int grupi, int idPerdoruesi, int idNdermarje, int idStatusDok, string autorizimet)
        {
            this.kodi = kodi;
            this.pershkrimi = pershkrimi;
            this.grupi = grupi;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idStatusDok;
            this.autorizimet = autorizimet;
            
        }

        /// <summary>
        /// konstruktor me parameter id 
        /// </summary>
        /// <param name="idgrupi">id e grupit</param>
        public clsGrupimDokumentiKoka(int idgrupi)
        {
            using (clsDatabaseRegjistrim dbKont = new clsDatabaseRegjistrim())
            {
                mbushGrup(dbKont.merrGrupim(idgrupi));
            }
        }
        public clsGrupimDokumentiKoka(string kodi, int idNdermarrje, int grupi, int idPerdoruesi, clsDatabaseRegjistrim db)
        {
            mbushGrup(db.TransCache.getGrupimDokumentiKoka(kodi, idNdermarrje, grupi, idPerdoruesi, db));
        }
        /// <summary>
        /// konstruktor me 3 parametra (kodin  dhe id e ndermarrjes dhe grupin)
        /// </summary>
        /// <param name="kodi">kod</param>
        /// <param name="idndermarje">id e ndermarrjes</param>
        /// <param name="grupi">grupi</param>
        public clsGrupimDokumentiKoka(string kodi, int idndermarje, int grupi, int idPerdoruesi)
        {
            using (clsDatabaseRegjistrim dbKont = new clsDatabaseRegjistrim())
            {
                mbushGrup(dbKont.merrGrupimSipasKod(kodi, idndermarje, grupi, idPerdoruesi));
            }
        }

        public clsGrupimDokumentiKoka(string kodi, int idndermarje, int grupi, int idPerdoruesi, int Lloji)
        {
            using (clsDatabaseRegjistrim dbKont = new clsDatabaseRegjistrim())
            {
                mbushGrup(dbKont.merrGrupimSipasKodDheLloj(kodi, idndermarje, grupi, idPerdoruesi, Lloji));
            }
        }

        public clsGrupimDokumentiKoka(DataRow rreshti)
        {
            
            mbushGrup(rreshti);
        }
        public static clsGrupimDokumentiKoka Krijo(IDataRecord dbDataRowGrupe)
        {
            clsGrupimDokumentiKoka grupKoka = new clsGrupimDokumentiKoka();
            int.TryParse(dbDataRowGrupe["IDGRUPIMKOKA"].ToString(), out grupKoka.idGrupimKoka);
            grupKoka.kodi = dbDataRowGrupe["KODI"].ToString();
            grupKoka.pershkrimi = dbDataRowGrupe["PERSHKRIMI"].ToString();
            grupKoka.kategoria = dbDataRowGrupe["Kategoria"].ToString();
            grupKoka.lloji = dbDataRowGrupe["Lloji"].ToString();
            int.TryParse(dbDataRowGrupe["GRUPI"].ToString(), out grupKoka.grupi);
            int.TryParse(dbDataRowGrupe["IDPERDORUESI"].ToString(), out grupKoka.idPerdoruesi);
            int.TryParse(dbDataRowGrupe["IDNDERMARJE"].ToString(), out grupKoka.idNdermarje);
            int.TryParse(dbDataRowGrupe["IDSTATUSDOK"].ToString(), out grupKoka.idStatusDok);
            DateTime.TryParse(dbDataRowGrupe["DTKRIJIMI"].ToString(), out grupKoka.dtKrijimi);
            DateTime.TryParse(dbDataRowGrupe["DTMODIFIKIMI"].ToString(), out grupKoka.dtModifikimi);
            return grupKoka;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin grupin dokumenti ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="ruajGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            data.beginTransaksion();
            clsMesazh u_ruajt = data.ruajGrup(out id, this.Kodi, this.Pershkrimi, this.Grupi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
            if (!u_ruajt.Status)
            {
                data.rollbackTransaksion();
                return u_ruajt;
            }
            foreach (clsGrupimDokumentiTrupi t in colTrupi)
            {
                u_ruajt = data.ruajGrupimTrupi(0, id, t.IdKonfig);
                if (!u_ruajt.Status)
                {
                    data.rollbackTransaksion();
                    return u_ruajt;
                }
            }
            #region Ruajtja e autorizimeve te grupeve
            colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
            if (autorizimet != "")
            {
                string[] pars1 = autorizimet.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    colLidhjet.Add(new clsLidhjeAutorizim() { IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]) });
                }
            }
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(data );
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(id, "GrupeDok", dbAdmin);
            for (int i = 0; i < colLidhjet.Count; i++)
            {
                int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                if (idAutorizimKoka == -1)
                    continue;
                colLidhjet[i].IdLloji = DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("GrupeDok", new DbKontabiliteti.clsDatabaseKontabilitet(data ));
                colLidhjet[i].IdLidhese = id;
                clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                if (lidhjeNjejte != null)
                {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                    colLidhjetAutorizim.Remove(lidhjeNjejte);
                    continue;
                }
                u_ruajt = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                if (!u_ruajt.Status)
                    return u_ruajt;
            }
            //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
            for (int j = 0; j < colLidhjetAutorizim.Count; j++)
            {
                u_ruajt = dbAdmin.fshiLidhjeAutorizim(colLidhjetAutorizim[j].IdLidhjeAutorizim);
                if (!u_ruajt.Status)
                    return u_ruajt;
            }

            #endregion
            data.commitTransaksion();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin grup ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="modifikoGrup"/> 
        /// </summary>
        /// <returns> nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            data.beginTransaksion();

            clsMesazh u_modifikua = data.modifikoGrup(this.IdGrupimKoka, this.Kodi, this.Pershkrimi, this.Grupi, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }
            u_modifikua = data.fshiGrupimTrupiSipasIdKoka(this.idGrupimKoka);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }
            foreach (clsGrupimDokumentiTrupi t in colTrupi)
            {
                u_modifikua = data.ruajGrupimTrupi(0, idGrupimKoka, t.IdKonfig);
                if (!u_modifikua.Status)
                {
                    data.rollbackTransaksion();

                    return u_modifikua;
                }
            }
            #region Ruajtja e autorizimeve te grupeve
            colLidhjetAutorizim colLidhjet = new colLidhjetAutorizim();
            if (autorizimet != "")
            {
                string[] pars1 = autorizimet.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    colLidhjet.Add(new clsLidhjeAutorizim() { IdAutorizimeKoka = clsAutorizimKoka.ktheIDAutorizim(pars1[i]) });
                }
            }
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(data );
            colLidhjetAutorizim colLidhjetAutorizim = new colLidhjetAutorizim(this.IdGrupimKoka, "GrupeDok", dbAdmin);
            for (int i = 0; i < colLidhjet.Count; i++)
            {
                int idAutorizimKoka = colLidhjet[i].IdAutorizimeKoka;
                if (idAutorizimKoka == -1)
                    continue;
                colLidhjet[i].IdLloji = DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("GrupeDok", new DbKontabiliteti.clsDatabaseKontabilitet(data ));
                colLidhjet[i].IdLidhese = this.IdGrupimKoka;
                clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizim.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                if (lidhjeNjejte != null)
                {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2) 
                    colLidhjetAutorizim.Remove(lidhjeNjejte);
                    continue;
                }
                u_modifikua = dbAdmin.ruajLidhjeAutorizim(colLidhjet[i].IdLidhjeAutorizim, colLidhjet[i].IdLidhese, colLidhjet[i].IdLloji, colLidhjet[i].IdAutorizimeKoka, 1);
                if (!u_modifikua.Status)
                    return u_modifikua;
            }
            //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
            
            u_modifikua = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizim, IdPerdoruesi, dbAdmin);
            if (!u_modifikua.Status)
            {
                data.rollbackTransaksion();
                return u_modifikua;
            }
            #endregion

            data.commitTransaksion();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin grup ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="fshiGrup"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            data.beginTransaksion();
            clsMesazh u_fshi = data.fshiGrup(this.IdGrupimKoka, this.idPerdoruesi);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }
            u_fshi = data.fshiGrupimTrupiSipasIdKoka(this.idGrupimKoka);
            if (!u_fshi.Status)
            {
                data.rollbackTransaksion();
                return u_fshi;
            }
            data.commitTransaksion();
            return u_fshi;
        }

        public static bool kaVeprimeGrup(int id)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
               return data.kaVeprimeGrup(id);
            }
        }

        public static bool ekzistonGrup(string kodi, int idndermarje, int grupi)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.ekzistonGrupDokumentash(kodi, idndermarje, grupi);
            }
        }

        public static int ktheIdGrupDokumentash(string kodi, int idndermarje, int grupi)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.ktheIdGrupDokumentash(kodi, idndermarje, grupi);
            }
        }
        public static bool ekzistonGrupiPerKeteLlojDok(int idGrupi, int idKonfig)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                return data.ekzistonGrupiPerKonfig(idGrupi, idKonfig);
            }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushGrupDtSmall(DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(Convert.ToString(dbDataRowGrupe["IDGRUPIMKOKA"]), out idGrupimKoka);
                    kodi = Convert.ToString(dbDataRowGrupe["KODI"]);
                    pershkrimi = Convert.ToString(dbDataRowGrupe["PERSHKRIMI"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupim dokumenti nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush grupimet nga databaza
        /// </summary>
        /// <param name="dbDataRowGrupe">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGrup(DataRow dbDataRowGrupe)
        {
            if (dbDataRowGrupe != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupe["IDGRUPIMKOKA"].ToString(), out idGrupimKoka);
                    kodi = dbDataRowGrupe["KODI"].ToString();
                    pershkrimi = dbDataRowGrupe["PERSHKRIMI"].ToString();
                    kategoria = dbDataRowGrupe["Kategoria"].ToString();
                    lloji = dbDataRowGrupe["Lloji"].ToString();
                    int.TryParse(dbDataRowGrupe["GRUPI"].ToString(), out grupi);
                    int.TryParse(dbDataRowGrupe["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowGrupe["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowGrupe["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowGrupe["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowGrupe["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    autorizimet = Convert.ToString(dbDataRowGrupe["Autorizimet"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    ImbLogger.LogErrorShitje("ERROR: Gabim gjate marrjes se grupim dokumenti nga db-ja");
                    throw new Exception("ERROR: Gabim gjate marrjes se grupim dokumenti nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushGrup(clsGrupimDokumentiKoka grupDok)
        {
            IdGrupimKoka = grupDok.IdGrupimKoka;
            Kodi = grupDok.Kodi;
            Pershkrimi = grupDok.Pershkrimi;
            Kategoria = grupDok.Kategoria;
            Lloji = grupDok.Lloji;
            Grupi = grupDok.Grupi;
            IdPerdoruesi = grupDok.IdPerdoruesi;
            IdNdermarje = grupDok.IdNdermarje;
            IdStatusDok = grupDok.IdStatusDok;
            DtKrijimi = grupDok.DtKrijimi;
            DtModifikimi = grupDok.DtModifikimi;
            Autorizimet = grupDok.Autorizimet;
            return new clsMesazh(true, $"Mbushja e grupimit te dokumentave me kod {grupDok.Kodi} u krye me sukses!");

        }

        #endregion

        public clsGrupimDokumentiKoka Clone()
        {
            return (clsGrupimDokumentiKoka) this.MemberwiseClone();
        }
    }
}
