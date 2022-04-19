using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne  nje nivel regjistrimi
    ///  (Te dhenat  merren nga tabela : T_NIVELREGJISTRIMI)
    /// </summary>

    public class clsNivelRegjistrimi
    {

        #region Atribute

        private int idNivel;
        private int idKategori;
        private string kodi;
        private string pershkrimi;
        private int radha;
        private bool aktiv;
        private int idNdermarrje;
        private colNivelRegjistrimi ocolNivelRegjistrimi;
        private string konvertohet;
        private int idPerdoruesi;
        private int idStatusDok;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private DataRow rreshti;
        private bool nrSerialUnik;
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="akt"> gjendjen aktive ose jo te nivelit</param>
        /// <param name="idKat"> id e kategorise ne te cilen ben pjese niveli</param>
        /// <param name="idNiv"> id ritese e nivelit</param>
        /// <param name="kod"> kodi i nivelit</param>
        /// <param name="nderm"> id e ndermarjes qe i perket ky nivel</param>
        /// <param name="pershk"> pershkrimi</param>
        /// <param name="radh"> radha </param>
        public clsNivelRegjistrimi(int idNiv, int idKat, string kod, string pershk, int radh, bool akt, int nderm, int idperdoruesi, int idstatusdok, bool nrSerialUnik)
        {
            idNivel = idNiv;
            idKategori = idKat;
            kodi = kod;
            pershkrimi = pershk;
            radha = radh;
            aktiv = akt;
            idNdermarrje = nderm;
            this.nrSerialUnik = nrSerialUnik;
            ocolNivelRegjistrimi = new colNivelRegjistrimi();
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsNivelRegjistrimi()
        {
        }
        public clsNivelRegjistrimi(string kod, int idNdermarrje)
        {
            mbushNivelRegjistrimiSipasKodiMeKonvertime(kod, idNdermarrje);
        }
        public clsNivelRegjistrimi(string kod, int idNdermarrje, clsDatabaseRegjistrim dbR)
        {
            mbushNivelRegjistrimi(dbR.TransCache.getNivelRegjistrimi(kod, idNdermarrje, dbR));
        }
        public clsNivelRegjistrimi(DataRow rreshti)
        {

            mbushNivelRegjMeKonvertime(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdNivel
        {
            get { return idNivel; }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e kategorise ne te cilen ben pjese.
        /// </summary>
        public int IdKategori
        {
            get { return idKategori; }
            set { idKategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodi i nivelit.
        /// </summary>
        public string Kodi
        {
            get { return kodi; }
            set { kodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimi i nivelit.
        /// </summary>
        public string Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Konvertimin e ketij nivel dokumenti ne nivele te tjera.
        /// </summary>
        public string Konvertohet
        {
            get { return konvertohet; }
            set { konvertohet = value; }
        }

        /// <summary>
        /// Kthen/Vendos radha e nivelit.
        /// </summary>
        public int Radha
        {
            get { return radha; }
            set { radha = value; }
        }

        /// <summary>
        /// Kthen/Vendos gjendjen aktive ose jo te nivelit.
        /// </summary>
        public Boolean Aktiv
        {
            get { return aktiv; }
            set { aktiv = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarjes qe i perket niveli.
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarrje; }
            set { idNdermarrje = value; }
        }

        /// <summary>
        /// Kthen/Vendos koleksionin me nivelet e regjistrimit ne te cilat mund te konvertohet ky nivel.
        /// </summary>
        public colNivelRegjistrimi OColNivelRegjistrimi
        {
            get { return ocolNivelRegjistrimi; }
            set { ocolNivelRegjistrimi = value; }
        }
        public int IdStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        public DateTime DtKrijimi
        {
            get { return dtKrijimi; }

        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }

        }

        /// <summary>
        /// Kthen/Vendos nese numri serial eshte unik per nivelin. 0=false, 1=true, 2=e pa aplikueshme
        /// </summary>
        public bool NrSerialUnik
        {
            get { return nrSerialUnik; }
            set { nrSerialUnik = value; }
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruajNivelRegjistrimiKonvertim(int idNiv, int idKat, string kod, string pershk, int radh, bool akt, int nderm, colNivelRegjistrimi ocolNivelRegjistrimi, int idperdoruesi, int idstatusdok, bool nrSerUnik)
        {
            clsMesazh mesazh = new clsMesazh();
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            dbRegj.beginTransaksion();            
            try
            {
                mesazh = dbRegj.ekzistonKodNivel(idKat, kod, nderm);
                if (!mesazh.Status)
                {
                    mesazh = dbRegj.ruajNivelRegjistrimi(out idNiv, idKat, kod, pershk, radh, akt, nderm, idperdoruesi, idstatusdok, nrSerUnik);
                    if (mesazh.Status && (idKat == 1 || idKat == 2))
                        mesazh = dbRegj.shtoTeDrejtaPerNivelRegjistrimi(idNiv, nderm, idKat);
                    if (mesazh.Status)
                    {
                        foreach (clsNivelRegjistrimi o in ocolNivelRegjistrimi)
                        {
                            if (mesazh.Status)
                                mesazh = dbRegj.ruajKonvertimNiveli(idNiv, o.IdNivel);
                            else
                            {
                                dbRegj.rollbackTransaksion();
                                return new clsMesazh(false, mesazh.PershkrimMesazhi);
                            }
                        }
                        if (mesazh.Status)
                        {
                            dbRegj.commitTransaksion();
                            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbRegj.rollbackTransaksion();
                            return new clsMesazh(false, mesazh.PershkrimMesazhi);
                        }
                    }
                    else
                    {
                        dbRegj.rollbackTransaksion();
                        return new clsMesazh(false, mesazh.PershkrimMesazhi);
                    }
                }
                else
                {
                    dbRegj.rollbackTransaksion();
                    return new clsMesazh(false, mesazh.PershkrimMesazhi);
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// ruan objektin e nivelit te regjistrimit ne tabelen perkatese ne databaze me gjithe konvertimet 
        /// e tij.Therret funksionin
        /// <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi.ruajNivelRegjistrimiKonvertim"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruajNivelPlusKonvertim()
        {
            clsNivelRegjistrimi data = new clsNivelRegjistrimi();
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = data.ruajNivelRegjistrimiKonvertim(this.IdNivel, this.IdKategori, this.Kodi, this.Pershkrimi, this.Radha, this.Aktiv, this.IdNdermarje, this.OColNivelRegjistrimi, this.idPerdoruesi, this.idStatusDok, this.nrSerialUnik);
            return u_ruajt;
        }

        public clsMesazh modifikoNivelRegjistrimi(int idNiv, int idKat, string kod, string pershk, int radh, bool akt, int nderm, colNivelRegjistrimi ocolNivelRegjistrimi, int perdoruesi, int idstatusdok, bool nrSerUnik)
        {//fshin rreshtat e ruajtur me pare ne tab T_NIVELREGJISTRIMIKONVERTO per kete nivel dhe ruan rreshtat e rinj nga col me konvertime
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            try
            {
                clsMesazh mesazh = new clsMesazh();
                mesazh = dbRegj.fshiKonvertimNiveli(idNiv);
                if (mesazh.Status)
                {
                    foreach (clsNivelRegjistrimi o in ocolNivelRegjistrimi)
                    {
                        if (mesazh.Status)
                            mesazh = dbRegj.ruajKonvertimNiveli(idNiv, o.IdNivel);
                        else
                        {
                            dbRegj.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        mesazh = dbRegj.modifikoNivelPaKonvertime(idNiv, idKat, kod, pershk, radh, akt, nderm, perdoruesi, idstatusdok, nrSerUnik);
                        if (mesazh.Status)
                        {
                            dbRegj.commitTransaksion();
                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
                            return mesazh;
                        }
                        else
                        {
                            dbRegj.rollbackTransaksion();
                            return mesazh;
                        }
                    }
                    else
                    {
                        dbRegj.rollbackTransaksion();
                        return mesazh;
                    }
                }
                else
                {
                    dbRegj.rollbackTransaksion();
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon objektin e nivelit te regjistrimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi.modifikoNivelRegjistrimi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsNivelRegjistrimi data = new clsNivelRegjistrimi();
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoNivelRegjistrimi(this.IdNivel, this.IdKategori, this.Kodi, this.Pershkrimi, this.Radha, this.Aktiv, this.IdNdermarje, this.OColNivelRegjistrimi, this.idPerdoruesi, this.idStatusDok, this.nrSerialUnik);
            return u_modifikua;
        }

        public clsMesazh fshiNivelRegjistrimi(int idNiv, colNivelRegjistrimi ocolNivelRegjistrimi)
        {
            clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim();
            //dbRegj.krijoManager();
            dbRegj.beginTransaksion();
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                foreach (clsNivelRegjistrimi o in ocolNivelRegjistrimi)
                {
                    if (mesazh.Status)
                    {
                        mesazh = dbRegj.fshiKonvertimNiveli(o.IdNivel);
                    }
                    else
                    {
                        dbRegj.rollbackTransaksion();

                        return mesazh;
                    }
                }
                mesazh = dbRegj.fshiTeDrejtaPerNivelRegjistrimi(idNiv);
                if(!mesazh.Status)
                {
                    dbRegj.rollbackTransaksion();
                    return mesazh;
                }
                mesazh = dbRegj.fshiNiv(idNiv);
                if (mesazh.Status)
                {
                    dbRegj.commitTransaksion();

                    mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
                    return mesazh;
                }
                else
                {
                    dbRegj.rollbackTransaksion();

                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbRegj.rollbackTransaksion();

                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin objektin e nivelit te regjistrimit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsNivelRegjistrimi.fshiNivelRegjistrimi"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            //  clsNivelRegjistrimi data = new clsNivelRegjistrimi();
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiNivStatus(this.IdNivel, this.idPerdoruesi);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// merr te gjithe objektet e nivelit te regjistrimit sipas id se kategorise nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheNivelRegjistrimi"/> 
        /// </summary>
        /// <returns > nje objekt colNivelRegjistrimi me te gjithe nivelet e regjistrimit te kategorise</returns>
        public colNivelRegjistrimi merriTeGjithe()
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNivelRegjistrimi(this); //i kalohet idKategori
            colNivelRegjistrimi nivele = new colNivelRegjistrimi();
            nivele.mbushNivelRegjistrimi(this.IdKategori, this.idNdermarrje);
            return nivele;

        }

        /// <summary>
        /// merr te gjithe nivelet sipas idndermarjes dhe id perdoruesi nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNivelRegjistrimi"/> 
        /// </summary>
        /// <param name="idNd"> id e ndermarjes</param>
        /// <param name="user"> id e perdoruesit</param>
        /// <returns > nje object colNivelRegjistrimi me te gjithe nivelet e regjistrimit te ndermarjes</returns>
        public colNivelRegjistrimi merrGjitheNivelRegjistrimi(int idNd, int user)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNivelRegjistrimi(idNd, user);
            colNivelRegjistrimi data = new colNivelRegjistrimi();
            data.mbushGjitheNivelRegjistrimi(idNd, user);
            return data;
        }

        /// <summary>
        /// merr te gjithe nivelet sipas idndermarjes dhe id perdoruesi dhe id kategorise nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheNivelRegjistrimiSipasKategori"/> 
        /// </summary>
        /// <param name="idNd"> id e ndermarjes</param>
        /// <param name="user"> id e perdoruesit</param>
        /// <returns > nje object colNivelRegjistrimi me te gjithe nivelet e regjistrimit te ndermarjes dhe te kesaj kategorie</returns>
        public static colNivelRegjistrimi merrGjitheNivelRegjistrimiSipasKategori(int idNd, int user, int idkat)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNivelRegjistrimiSipasKategori(this, idNd, user);
            colNivelRegjistrimi data = new colNivelRegjistrimi();
            data.mbushGjitheNivelRegjistrimiSipasKategoriPlus(idkat, idNd, user);
            return data;
        }

        public colNivelRegjistrimi merrGjitheNivelRegjistrimiSipasKategoriPaAll(int idNd, int user)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheNivelRegjistrimiSipasKategoriPaAll(this, idNd, user);
            colNivelRegjistrimi data = new colNivelRegjistrimi();
            data.mbushGjitheNivelRegjistrimiSipasKategoriPaAll(this.IdKategori, idNd, user);
            return data;
        }

        /// <summary>
        /// merr te gjithe nivelet e regjistrimit ne te cilat konvertohet ky nivel.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKonvertimeNiveli"/> 
        /// </summary>
        /// <returns > nje objekt colNivelRegjistrimi me te gjithe nivelet ne te cilat konvertohet ky nivel</returns>
        public colNivelRegjistrimi merrKonvertimeNiveli()
        {
            colNivelRegjistrimi data = new colNivelRegjistrimi();
            data.mbushKonvertimeNiveli(this.idNivel);
            return data;
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrKonvertimeNiveli(this);
        }

        /// <summary>
        /// merr te gjithe nivelet e regjistrimit ne te cilat konvertohet ky nivel.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKonvertimeNiveliNew"/> 
        /// </summary>
        /// <returns > nje objekt colNivelRegjistrimi me te gjithe nivelet ne te cilat konvertohet ky nivel</returns>
        public colNivelRegjistrimi merrKonvertimeNiveliNew()
        {
            colNivelRegjistrimi data = new colNivelRegjistrimi();
            data.mbushKonvertimeNiveliNew(this.IdNivel);
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrKonvertimeNiveliNew(this);
            return data;
        }

        /// <summary>
        /// merr objektin e nivelit te regjistrimit sipas id ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheNivelRegjistrimiSipasID"/> 
        /// </summary>
        /// <returns > nje objekt clsNivelRegjistrimi qe mban nivelit e regjistrimit te kerkuar</returns>
        public clsNivelRegjistrimi merrNivelRegjSipasId()
        {
            mbushNivelRegjistrimiSipasIdMeKonvertime(this.IdNivel);
            return this;
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNivelRegjistrimiSipasID(this.IdNivel);
        }

        public clsNivelRegjistrimi merrNivelRegjSipasKodi()
        {
            mbushNivelRegjistrimiSipasKodiMeKonvertime(this.Kodi, this.IdNdermarje);
            return this;
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNivelRegjistrimiSipasKodi(this.Kodi, this.IdNdermarje);
        }

        public bool mbushNivelRegjistrimiSipasKodeve(string parameter, string idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodeve(parameter, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }

        public bool mbushNivelRegjistrimiSipasPershkrimitMeKonvertime(string pershkrim, string idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasPershkrimit(pershkrim, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }
        public bool mbushNivelRegjistrimiSipasPershkrimitPaKonvertime(string pershkrim, string idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjPaKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasPershkrimit(pershkrim, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }
        public bool mbushNivelRegjistrimiSipasKodit(string kodi, string idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjPaKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodit(kodi, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }

        public bool mbushNivelRegjistrimiSipasIdMeKonvertime(int idNivel)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasID(idNivel));
            dbNivelRegj.Dispose();
            return mbush;
        }
        /// <summary>
        /// mbush objektin e nivelit te regjistrimit pa koleksionin e konvertimeve
        /// </summary>
        /// <param name="idNivel"></param>
        /// <returns></returns>
        public bool mbushNivelRegjistrimiSipasIdPaKonvertime(int idNivel)
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushNivelRegjistrimiSipasIdPaKonvertime");
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjPaKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasID(idNivel));
            dbNivelRegj.Dispose();
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushNivelRegjistrimiSipasIdPaKonvertime");
            return mbush;
        }
        public bool mbushNivelRegjistrimiSipasIdPaKonvertime(int idNivel, clsDatabaseRegjistrim dbNivelRegj)
        {

            bool mbush = mbushNivelRegjPaKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasID(idNivel));

            return mbush;
        }
        public bool mbushNivelRegjistrimiSipasID(int idNivel, clsDatabaseRegjistrim dbNivelRegj)
        {

            return mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasID(idNivel));
        }
        public bool mbushNivelRegjistrimiSipasKoditPaKonvertime(string kodi, int idNderm)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return mbushNivelRegjPaKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodi(kodi, idNderm));
            }
        }
        public bool mbushNivelRegjistrimiSipasKodiMeKonvertime(string kodi, int idNderm)
        {
            clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim();
            bool mbush = mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodi(kodi, idNderm));
            dbNivelRegj.Dispose();
            return mbush;
        }
        public bool mbushNivelRegjistrimiSipasKodiMeKonvertime(string kodi, int idNderm, clsDatabaseRegjistrim dbNivelRegj)
        {
            return mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodi(kodi, idNderm));
        }
        public bool mbushNivelRegjistrimiSipasKodi(string kodi, int idNderm, clsDatabaseRegjistrim dbNivelRegj)
        {
            return mbushNivelRegjMeKonvertime(dbNivelRegj.ktheNivelRegjistrimiSipasKodi(kodi, idNderm));
        }
        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e nivelit
        /// </summary>
        /// <param name="parameter">parametra</param>
        /// <param name="idNderm">id e ndermarrjes</param>
        /// <returns>id e nivelit</returns>
        public static int mbushIDNivelRegjistrimiSipasKodeve(string parameter, string idNderm)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            int idNivel = dbKatNivelDok.ktheIDNivelRegjistrimiSipasKodeve(parameter, idNderm);
            dbKatNivelDok.Dispose();
            return idNivel;
        }
        /// <summary>
        /// Kthen kodin e nivelit te regjistrimit
        /// </summary>
        /// <param name="idNivelRegjistrimi"></param>
        /// <returns></returns>
        public static string ktheKodNivelRegjistrimi(int idNivelRegjistrimi, clsDatabaseRegjistrim dbNivelRegj)
        {
            return dbNivelRegj.ktheKodNivelRegjistrimi(idNivelRegjistrimi);
        }
        /// <summary>
        /// Kthen kodin e nivelit te regjistrimit
        /// </summary>
        /// <param name="idNivelRegjistrimi"></param>
        /// <returns></returns>
        public static string ktheKodNivelRegjistrimi(int idNivelRegjistrimi)
        {

            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return dbNivelRegj.ktheKodNivelRegjistrimi(idNivelRegjistrimi);
            }
        }
        /// <summary>
        /// Kthen idKategorine e nivelit
        /// </summary>
        /// <param name="idNivelRegjistrimi"></param>
        /// <returns></returns>
        public static int ktheIdKategoriaNivelRegjistrimi(int idNivelRegjistrimi)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return dbNivelRegj.ktheIdKategoriaNivelRegjistrimi(idNivelRegjistrimi);
            }
        }
        /// <summary>
        /// Kthen idKategorine e nivelit
        /// </summary>
        /// <param name="idNivelRegjistrimi"></param>
        /// <param name="dbNivelRegj"></param>
        /// <returns></returns>
        public static int ktheIdKategoriaNivelRegjistrimi(int idNivelRegjistrimi, clsDatabaseRegjistrim dbNivelRegj)
        {
            return dbNivelRegj.ktheIdKategoriaNivelRegjistrimi(idNivelRegjistrimi);
        }
        /// <summary>
        /// Kthen idNivelin sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi"></param>
        /// <param name="idNderm"></param>
        /// <returns></returns>
        public static int ktheIdNivelRegjistrimiSipasKodi(string kodi, int idNderm)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return dbNivelRegj.ktheIdNivelRegjistrimiSipasKodit(kodi, idNderm);
            }
        }
        /// <summary>
        /// Kthen idNivelin sipas kodit dhe idndermarrjes
        /// </summary>
        /// <param name="kodi"></param>
        /// <param name="idNderm"></param>
        /// <param name="dbNivelRegj"></param>
        /// <returns></returns>
        public static int ktheIdNivelRegjistrimiSipasKodi(string kodi, int idNderm, clsDatabaseRegjistrim dbNivelRegj)
        {
            return dbNivelRegj.ktheIdNivelRegjistrimiSipasKodit(kodi, idNderm);
        }


        public static bool kontrolloGjeneruesPerNivelRegjistrimi(int idNivel, int nderm, int idGjenerues, int idkonfig)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return dbNivelRegj.kontrolloGjeneruesPerNivelRegjistrimi(idNivel, nderm, idGjenerues, idkonfig);
            }
        }

        public static clsMesazh ekzistonIdNivelGjeneruesi(int idNivelGjenerues, int idNdermarrje)
        {
            using (clsDatabaseRegjistrim dbNivelRegj = new clsDatabaseRegjistrim())
            {
                return dbNivelRegj.ekzistonIdNivelGjeneruesi(idNivelGjenerues, idNdermarrje);
            }
        }
        #endregion

        #region Metoda Internal
        private bool mbushNivelRegj(DataRow dbDataRowNivelRegj, bool meKonvertime) //todo Nestila duhet ndare si medote vecante marrja e konvertimeve, dhe duhet bere qe te vi direkt nga db me metode
        {
            if (dbDataRowNivelRegj != null)
            {
                try
                {
                    int.TryParse(dbDataRowNivelRegj["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowNivelRegj["IDKATDOK"].ToString(), out idKategori);
                    kodi = dbDataRowNivelRegj["KODI"].ToString();
                    pershkrimi = dbDataRowNivelRegj["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowNivelRegj["RADHA"].ToString(), out radha);
                    bool.TryParse(dbDataRowNivelRegj["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNivelRegj["IDNDERMARRJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNivelRegj["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowNivelRegj["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowNivelRegj["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNivelRegj["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    bool.TryParse(dbDataRowNivelRegj["NRSERIALUNIK"].ToString(), out nrSerialUnik);
                    ocolNivelRegjistrimi = new colNivelRegjistrimi();
                    if (meKonvertime)
                    {
                        ocolNivelRegjistrimi = merrKonvertimeNiveliNew();
                        konvertohet = "";
                        foreach (clsNivelRegjistrimi n in ocolNivelRegjistrimi)
                            konvertohet += n.Kodi + ";";
                        if (konvertohet.Length > 0)
                            konvertohet = konvertohet.Substring(0, konvertohet.Length - 1);
                    }
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se nivelit te regjistrimit nga db-ja");
                }
            }
            else
                return false;
        }

        public clsMesazh mbushNivelRegjistrimi(clsNivelRegjistrimi nivel)
        {
            idNivel = nivel.IdNivel;
            idKategori = nivel.idKategori;
            kodi = nivel.Kodi;
            pershkrimi = nivel.Pershkrimi;
            radha = nivel.Radha;
            aktiv = nivel.Aktiv;
            idNdermarrje = nivel.IdNdermarje;
            idStatusDok = nivel.IdStatusDok;
            idPerdoruesi = nivel.IdPerdoruesi;
            dtKrijimi = nivel.DtKrijimi;
            dtModifikimi = nivel.DtModifikimi;
            nrSerialUnik = nivel.nrSerialUnik;
            return new clsMesazh(true, $"Mbushja e nivelit {nivel.Kodi} u be me sukses!");
        }

        /// <summary>
        /// mbush nivelin e regjistrimit nga databaza bashke me koleksionin e konvertimeve
        /// </summary>
        /// <param name="dbDataRowNivelRegj">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNivelRegjMeKonvertime(DataRow dbDataRowNivelRegj)
        {
            return mbushNivelRegj(dbDataRowNivelRegj, true);
        }

        internal bool mbushNivelRegjPaKonvertime(DataRow dbDataRowNivelRegj)
        {
            return mbushNivelRegj(dbDataRowNivelRegj, false);
        }

        /// <summary>
        /// mbush nivelin e regjistrimit nga databaza
        /// </summary>
        /// <param name="dbDataRowNivelRegj">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushNivelRegjNew(DataRow dbDataRowNivelRegj)
        {
            if (dbDataRowNivelRegj != null)
            {
                try
                {
                    int.TryParse(dbDataRowNivelRegj["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowNivelRegj["IDKATDOK"].ToString(), out idKategori);
                    kodi = dbDataRowNivelRegj["KODI"].ToString();
                    pershkrimi = dbDataRowNivelRegj["PERSHKRIMI"].ToString();
                    int.TryParse(dbDataRowNivelRegj["RADHA"].ToString(), out radha);
                    bool.TryParse(dbDataRowNivelRegj["AKTIV"].ToString(), out aktiv);
                    int.TryParse(dbDataRowNivelRegj["IDNDERMARRJE"].ToString(), out idNdermarrje);
                    int.TryParse(dbDataRowNivelRegj["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowNivelRegj["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    DateTime.TryParse(dbDataRowNivelRegj["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowNivelRegj["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    bool.TryParse(dbDataRowNivelRegj["NRSERIALUNIK"].ToString(), out nrSerialUnik);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se nivelit te regjistrimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}