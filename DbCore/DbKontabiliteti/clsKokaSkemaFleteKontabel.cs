using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Resources;
using System.Globalization;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne koken e nje skeme kontabel
    ///  (Te dhenat  merren nga tabela : T_SKEMEKONTABILITETI)
    /// </remarks>
    public class clsKokaSkemaFleteKontabel
    {
        #region Atribute

        private int idKokaSkemaFK;
        private string kodiKokaSkemaFK;
        private string pershkrimiKokaSkemaFK;
        private int idPerdoruesi;
        private int idNdermarje;
        private int idStatusDok;
        private string idNivelAutorizimi;
        private int idNderViti;
        private DateTime dtKrijimi;
        private DateTime dtModifikimi;
        private colTrupatSkematFletetKontabel oColTrupi;
        private DataRow rreshti;

        #endregion

        #region Kontruktoret

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <param name="kodiKokaSkemaFK">Kodi</param>
        /// <param name="pershkrimiKokaSkemaFK">Pershkrimi</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNderViti">Id lidhese ndermarrje - vit</param>
        /// <param name="idNivelAutorizimi">Id e nivelit te autorizimit</param>
        public clsKokaSkemaFleteKontabel( int idKokaSkemaFK, string kodiKokaSkemaFK, string pershkrimiKokaSkemaFK, int idPerdoruesi, int idNdermarje,int idstatusdok, DateTime dtkrijimi, DateTime dtmodifikimi,string idNivelAutorizimi, int idNderViti)
        {
            this.idKokaSkemaFK = idKokaSkemaFK;
            this.kodiKokaSkemaFK = kodiKokaSkemaFK;
            this.pershkrimiKokaSkemaFK = pershkrimiKokaSkemaFK;
            this.idPerdoruesi = idPerdoruesi;
            this.idNdermarje = idNdermarje;
            this.idStatusDok = idstatusdok;
            this.dtKrijimi = dtkrijimi;
            this.dtModifikimi = dtmodifikimi;
            this.idNivelAutorizimi = idNivelAutorizimi;
            this.idNderViti = idNderViti;
            oColTrupi = new colTrupatSkematFletetKontabel();
          
        }

        /// <summary>
        /// konstruktor me 1 parameter string
        /// </summary>
        /// <param name="kodi">kodi i skemes flete kontabel</param>
        public clsKokaSkemaFleteKontabel(String kodi, int idndermarje)
        {
            clsDatabaseKontabilitet dbKokaSkemeFleteKont= new clsDatabaseKontabilitet();
            mbushKokaSkemaFleteKont(dbKokaSkemeFleteKont.ktheKokaSkemaFleteKontabelSipasKodit(kodi, idndermarje));
            dbKokaSkemeFleteKont.Dispose();
        }

        /// <summary>
        /// konstruktor me 1 parameter int
        /// </summary>
        /// <param name="id">id e kokes se skemes</param>
        public clsKokaSkemaFleteKontabel(int id)
        {
            clsDatabaseKontabilitet dbKokaSkemeFleteKont = new clsDatabaseKontabilitet();
            mbushKokaSkemaFleteKont(dbKokaSkemeFleteKont.ktheKokaSkemaFleteKontabelSipasID(id));
            dbKokaSkemeFleteKont.Dispose();
        }

        /// <summary>
        /// Kontruktor i klases
        /// </summary>
        public clsKokaSkemaFleteKontabel()
        {
        }

        public clsKokaSkemaFleteKontabel(DataRow rreshti)
        {
            
            mbushKokaSkemaFleteKont(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdKokaSkemaFK
        {
            get
            {
                return idKokaSkemaFK ;
            }
            set
            {
                idKokaSkemaFK  = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e skemes kontabel
        /// </summary>
        public string KodiKokaSkemaFK
        {
            get
            {
                return kodiKokaSkemaFK ;
            }
            set
            {
                kodiKokaSkemaFK  = value;
            }
        }
        /// <summary>
          /// Kthen/Vendos id nivel autorizimi
          /// </summary>
        public string IdNivelAutorizimi
        {
            get
            {
                return idNivelAutorizimi;
            }
            set
            {
                idNivelAutorizimi = value;
            }
        }


        /// <summary>
        /// Kthen/Vendos pershkrimin e skemes kontabel
        /// </summary>
        public string PershkrimiKokaSkemaFK
        {
            get
            {
                return pershkrimiKokaSkemaFK;
            }
            set
            {
                pershkrimiKokaSkemaFK = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese ndermarrje - vit
        /// </summary>
        public int IdNdermarje
        {
            get
            {
                return idNdermarje;
            }
            set
            {
                idNdermarje= value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe po kryen veprimin
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                idPerdoruesi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e vitit te ndermarrjes 
        /// </summary>
        public int IdNderViti
        {
            get
            {
                return idNderViti;
            }
            set
            {
                idNderViti = value;
            }
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
        /// <summary>
        /// Kthen/Vendos nje collection me objekte te tipit <see cref="DbCore.DbKontabiliteti.clsTrupiSkemaFleteKontabel"/>
        /// </summary>
        public colTrupatSkematFletetKontabel OColTrupi
        {
            get { return oColTrupi; }
            set { oColTrupi = value; }
        }
       
        
        #endregion

        #region Metoda publike

        /// <summary>
        /// Ekzekuton nje transaksion per te ruajtur nje objekt clsKokaSkemaFleteKontabel dhe trupin e tij
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <param name="kodiKokaSkemaFK">Kodi</param>
        /// <param name="pershkrimiKokaSkemaFK">Pershkrimi</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNderViti">Id lidhese ndermarrje - vit</param>
        /// <param name="idNivelAutorizimi">Id e nivelit te autorizimit</param>
        /// <param name="oColTrupi">colection mbi trupin e kesaj koke</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh ruajSkemaFleteKontabel(int idKokaSkemaFK, string kodiKokaSkemaFK, string pershkrimiKokaSkemaFK, int idPerdoruesi, int idNdermarje, int idstatusdok, colTrupatSkematFletetKontabel oColTrupi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {//transaksioni per te ruajtur skemen fleten kontabel
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematFleteKontabel");
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            dbKont.beginTransaksion();
            bool statusVeprimi;
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            try
            {
                idKokaSkemaFK = dbKont.ruajKokaSkemaFleteKontabel( idKokaSkemaFK, kodiKokaSkemaFK, pershkrimiKokaSkemaFK, idPerdoruesi, idNdermarje,idstatusdok);

                if (idKokaSkemaFK == 0)
                    statusVeprimi = false;
                else statusVeprimi = true;

                if (statusVeprimi)
                {
                    foreach (clsTrupiSkemaFleteKontabel o in oColTrupi)
                    {//behet ruajtja e trupit te fleteve kontabel 
                        if (statusVeprimi)
                        {
                            o.IdKokaSkemaFK = idKokaSkemaFK;
                            int idT;
                            mesazh = dbKont.ruajTrupiSkemaFleteKontabel(out idT, o.IdKokaSkemaFK, o.IdLlogari,o.Pershkrimi, o.IdMonedha,o.Kursi,o.VleftaDebi,o.VleftaKredi, o.VleftaDebiMon,o.VleftaKrediMon);
                        }
                        else
                        {
                            dbKont.rollbackTransaksion();
                            
                            return mesazh;
                        }
                    }
                    if (mesazh.Status)
                    {
                        
                       
                            dbKont.commitTransaksion();

                            mesazh = new clsMesazh(true, rm.GetString("labelRaportMesazhRuajtjaPerfundoiSukses", ci));
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
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Ruan koken dhe trupin e skemes kontabel ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKokaSkemaFleteKontabel.ruajSkemaFleteKontabel"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj(System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {//metoda qe therret klasen clsDatabaseKontabilitet per ruajtjen e nje fletekontabel
            clsKokaSkemaFleteKontabel data = new clsKokaSkemaFleteKontabel();
            clsMesazh u_ruajt = data.ruajSkemaFleteKontabel(this.IdKokaSkemaFK, this.kodiKokaSkemaFK, this.PershkrimiKokaSkemaFK, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.OColTrupi, rm, ci);
            return u_ruajt;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te modifikuar nje objekt clsKokaSkemaFleteKontabel dhe trupin e tij
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <param name="kodiKokaSkemaFK">Kodi</param>
        /// <param name="pershkrimiKokaSkemaFK">Pershkrimi</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="idNderViti">Id lidhese ndermarrje - vit</param>
        /// <param name="idNivelAutorizimi">Id e nivelit te autorizimit</param>
        /// <param name="oColTrupi">colection mbi trupin e kesaj koke</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh modifikoSkemaFleteKontabel(int idKokaSkemaFK, string kodiKokaSkemaFK, string pershkrimiKokaSkemaFK, int idPerdoruesi, int idNdermarje, int idstatusdok, colTrupatSkematFletetKontabel oColTrupi)
        {//transaksioni per te modifikuar fleten kontabel
            //colLlojeBuxhetesh colLloj = merrLlojBuxhetiSipasKodit("SkematFleteKontabel");
          
            //DbAdmin.colLidhjetAutorizim colLidhjetAutorizim = new DbAdmin.clsDatabaseAdmin().merrLidhjeAutorizimSipasIdLidheseIdLloji(idKokaSkemaFK, DbCore.DbKontabiliteti.clsLlojBuxheti.mbushIDLlojBuxheti("SkematFleteKontabel"));
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            dbKont.beginTransaksion();
            //colTrupatSkematFletetKontabel trupat = dbKont.merrTrupatSkematFletetKontabelSipasKokes(idKokaSkemaFK);
            colTrupatSkematFletetKontabel trupat = new colTrupatSkematFletetKontabel(idKokaSkemaFK);
           
            clsMesazh mesazh = new clsMesazh();
            DbCore.clsMesazh mesazhAdmin = new DbCore.clsMesazh(true);
            try
            {
                mesazh = dbKont.modifikoKokaSkemaFleteKontabel(idKokaSkemaFK, kodiKokaSkemaFK, pershkrimiKokaSkemaFK, idPerdoruesi, idNdermarje,idstatusdok);
                if (mesazh.Status)
                {
                    if (trupat.Count < oColTrupi.Count)//rasti kur jane shtuar rreshta trupi
                    {
                        for (int i = 0; i < oColTrupi.Count; i++)
                        {
                            if (mesazh.Status)
                            {
                                oColTrupi[i].IdKokaSkemaFK = idKokaSkemaFK;
                                if (i < trupat.Count)
                                {
                                    oColTrupi[i].IdTrupiSkemaFK = trupat[i].IdTrupiSkemaFK;
                                    mesazh = dbKont.modifikoTrupiSkemaFleteKontabel(oColTrupi[i].IdTrupiSkemaFK, oColTrupi[i].IdKokaSkemaFK, oColTrupi[i].IdLlogari, oColTrupi[i].Pershkrimi, oColTrupi[i].IdMonedha,oColTrupi[i].Kursi,oColTrupi[i].VleftaDebi,oColTrupi[i].VleftaKredi, oColTrupi[i].VleftaDebiMon, oColTrupi[i].VleftaKrediMon);
                                }
                                else
                                {
                                    int idT;
                                    mesazh = dbKont.ruajTrupiSkemaFleteKontabel(out idT, oColTrupi[i].IdKokaSkemaFK, oColTrupi[i].IdLlogari, oColTrupi[i].Pershkrimi, oColTrupi[i].IdMonedha, oColTrupi[i].Kursi, oColTrupi[i].VleftaDebi, oColTrupi[i].VleftaKredi, oColTrupi[i].VleftaDebiMon, oColTrupi[i].VleftaKrediMon);
                                }
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                return mesazh;
                            }
                        }
                    }
                    else//rasti kur jane fshire rreshta
                    {
                        int count = 0;
                        for (int i = 0; i < trupat.Count; i++)
                        {
                            if (mesazh.Status)
                            {
                                if (count < oColTrupi.Count)
                                {
                                    oColTrupi[i].IdKokaSkemaFK = idKokaSkemaFK;

                                    oColTrupi[i].IdTrupiSkemaFK = trupat[i].IdTrupiSkemaFK;
                                    mesazh = dbKont.modifikoTrupiSkemaFleteKontabel(oColTrupi[i].IdTrupiSkemaFK, oColTrupi[i].IdKokaSkemaFK, oColTrupi[i].IdLlogari, oColTrupi[i].Pershkrimi, oColTrupi[i].IdMonedha, oColTrupi[i].Kursi, oColTrupi[i].VleftaDebi, oColTrupi[i].VleftaKredi, oColTrupi[i].VleftaDebiMon, oColTrupi[i].VleftaKrediMon);
                                }
                                else
                                {
                                    mesazh = dbKont.fshiTrupiSkemaFleteKontabel(trupat[i].IdTrupiSkemaFK);
                                }
                                count++;
                            }
                            else
                            {
                                dbKont.rollbackTransaksion();
                                
                                return mesazh;
                            }
                        }
                    }
                    if (mesazh.Status)
                    {
                       
                       
                            dbKont.commitTransaksion();
                            
                            mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
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
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Modifikon koken dhe trupin e skemes kontabel ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKokaSkemaFleteKontabel.modifikoSkemaFleteKontabel"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseKontabilitet per modifikimin e nje fletekontabel
            clsKokaSkemaFleteKontabel data = new clsKokaSkemaFleteKontabel();
            clsMesazh u_modifikua = data.modifikoSkemaFleteKontabel(this.IdKokaSkemaFK, this.kodiKokaSkemaFK, this.PershkrimiKokaSkemaFK, this.IdPerdoruesi, this.IdNdermarje, this.idStatusDok, this.OColTrupi);
            return u_modifikua;
        }

        /// <summary>
        /// Ekzekuton nje transaksion per te fshire nje objekt clsKokaSkemaFleteKontabel dhe trupin e tij
        /// <param name="idKokaSkemaFK">Id qe gjenerohet automatikisht</param>
        /// <returns> Kthen statusin e perfundimit te ekzekutimit te transaksionit (nje objekt clsMesazh qe tregon nese veprimi eshte kryer me sukses apo jo.<seealso cref="DbCore.clsMesazh"/>)</returns>
        /// </summary>
        public clsMesazh fshiSkemaFleteKontabel(int idKokaSkemaFK)
        {//transaksioni per te fshire nje skeme flete kontabel
            clsDatabaseKontabilitet dbKont = new clsDatabaseKontabilitet();
            colTrupatSkematFletetKontabel trupat = new colTrupatSkematFletetKontabel(idKokaSkemaFK);
            //colTrupatSkematFletetKontabel trupat = dbKont.merrTrupatSkematFletetKontabelSipasKokes(idKokaSkemaFK);
            dbKont.beginTransaksion();
           
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                foreach (clsTrupiSkemaFleteKontabel o in trupat)
                {
                    if (mesazh.Status)
                        mesazh = dbKont.fshiTrupiSkemaFleteKontabel(o.IdTrupiSkemaFK);
                    else
                    {
                        dbKont.rollbackTransaksion();
                        
                        return mesazh;
                    }
                }
                if (mesazh.Status)
                {
                    mesazh = dbKont.fshiKokaSkemaFleteKontabel(idKokaSkemaFK);
                    if (mesazh.Status)
                    {
                        dbKont.commitTransaksion();
                        
                        mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
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
                    
                    return mesazh;
                }
            }
            catch (Exception ce)
            {
                dbKont.rollbackTransaksion();
                
                return new clsMesazh(false, ce.Message);
            }
        }

        /// <summary>
        /// Fshin koken dhe trupin e skemes kontabel ne tabelat perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsKokaSkematFletetKontabel.fshiSkemaFleteKontabel"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi(ResourceManager rm, CultureInfo ci)
        {//metoda qe therret klasen clsDatabaseKontabilitet per fshirjen e nje fletekontabel
            clsDatabaseKontabilitet data = new  clsDatabaseKontabilitet ();
            clsMesazh u_fshi = data.fshiStatusKokaSkemaFleteKontabel(this.IdKokaSkemaFK, this.idPerdoruesi, rm, ci);
            data.Dispose();
            return u_fshi;
        }
        
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush koken e skemes flete kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowKokaSkemFletKont">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKokaSkemaFleteKont(DataRow dbDataRowKokaSkemFletKont)
        {
            if (dbDataRowKokaSkemFletKont != null)
            {
                try
                {
                    int.TryParse(dbDataRowKokaSkemFletKont["IDKOKASKEMAFK"].ToString(), out idKokaSkemaFK);
                    kodiKokaSkemaFK = dbDataRowKokaSkemFletKont["KODIKOKASKEMAFK"].ToString();
                    pershkrimiKokaSkemaFK = dbDataRowKokaSkemFletKont["PERSHKRIMIKOKASKEMAFK"].ToString();

                    int.TryParse(dbDataRowKokaSkemFletKont["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowKokaSkemFletKont["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowKokaSkemFletKont["IDSTATUSDOK"].ToString(), out idStatusDok);
                    DateTime.TryParse(dbDataRowKokaSkemFletKont["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowKokaSkemFletKont["DTMODIFIKIMI"].ToString(), out dtModifikimi);

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kokes se skemes se fleteve kontabel nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

