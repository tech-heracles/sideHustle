using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace DbCore.DbAdmin
{
    public class clsRoli
    {
        #region Attribute
        /// <summary>
        /// id-ja e rolit
        /// </summary>
        private int idRoli;
        /// <summary>
        /// kodi i rolit
        /// </summary>
        private String kodRoli;
        /// <summary>
        /// pershkrimi i rolit
        /// </summary>
        private String pershkrimRoli;
        /// <summary>
        /// Aktiv apo jo
        /// </summary>
        private bool aktivRoli;
        /// <summary>
        /// data krijimit te rolit
        /// </summary>
        private DateTime dtKrijimi;
        /// <summary>
        /// data e modifikimit te fundit
        /// </summary>
        private DateTime dtModifikimi;
        /// <summary>
        /// id-ja e krijuesit
        /// </summary>
        private int idPerdoruesi;
        /// <summary>
        /// modeli
        /// </summary>
        private int model;
        private int idLicenca;
        private int idStatusDok;
        private DataRow rreshti;
        #endregion

        #region Properties
        

        /// <summary>
        /// Get Set: kodi i rolit
        /// </summary>
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
        /// <summary>
        /// Get Set: pershkrimi i rolit
        /// </summary>
        public String KodRoli
        {
            get
            {
                return kodRoli;
            }
            set
            {
                if (kodRoli == value)
                    return;
                kodRoli = value;
            }
        }
        /// <summary>
        /// Get Set: pershkrimi i rolit
        /// </summary>
        public String PershkrimRoli
        {
            get
            {
                return pershkrimRoli;
            }
            set
            {
                if (pershkrimRoli == value)
                    return;
                pershkrimRoli = value;
            }
        }
        /// <summary>
        /// Get Set: Aktiv apo jo
        /// </summary>
        public bool AktivRoli
        {
            get
            {
                return aktivRoli;
            }
            set
            {
                if (aktivRoli == value)
                    return;
                aktivRoli = value;
            }
        }
        /// <summary>
        /// Get Set: id-ja e krijuesit
        /// </summary>
        public int IdPerdoruesi
        {
            get
            {
                return idPerdoruesi;
            }
            set
            {
                if (idPerdoruesi == value)
                    return;
                idPerdoruesi = value;
            }
        }
        /// <summary>
        /// Get Set: modelin
        /// 
        /// </summary>
        public int Model
        {
            get
            {
                return model;
            }
            set
            {
                if (model == value)
                    return;
                model = value;
            }
        }
        /// <summary>
        /// Get Set: modelin
        /// 
        /// </summary>
        public int IdLicenca
        {
            get
            {
                return idLicenca;
            }
            set
            {
                if (model == value)
                    return;
                idLicenca = value;
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
            set { dtKrijimi = value; }
        }
        public DateTime DtModifikimi
        {
            get { return dtModifikimi; }
            set { dtModifikimi = value; }
        }
        #endregion

        #region Konstruktoret
        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsRoli()
        {
        }

        /// <summary>
        /// krijon nje objekt te tipit grupiPerdorues duke lexuar te dhenat nga db-ja ne baze te id-se se dhene
        /// </summary>
        /// <param name="idRoli">id-ja e dhene per te lexuar te dhenat nga db-ja</param>
        public clsRoli(int idRoli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (!this.mbushRolin(data.merrRolin(idRoli)))
            {
                data.Dispose();
                return; //roli me id idroli nuk ekziston
            }
            data.Dispose();
            //throw new Exception("ERROR: Gabim gjate leximit te rolit " + idRoli + "nga databaza");
        }

        /// <summary>
        /// Konstruktor i plote
        /// </summary>
        /// <param name="idRoli"></param>
        /// <param name="grupiPerdoruesKodi"></param>
        /// <param name="grupiPerdoruesPershkrimi"></param>
        /// <param name="grupiAktivPerdorues"></param>
        /// <param name="grupiPerdoruesData"></param>
        public clsRoli(int idRoli, String kodRoli, String pershkrimRoli, bool aktivRoli, DateTime dateKrijimi, DateTime dateModifikimi, int idKrijuesi, int model, int idLicenca, int idstatusdok)
        {
            this.idRoli = idRoli;
            this.kodRoli = kodRoli;
            this.pershkrimRoli = pershkrimRoli;
            this.aktivRoli = aktivRoli;
            this.dtKrijimi = dateKrijimi;
            this.dtModifikimi = dateModifikimi;
            this.idPerdoruesi = idKrijuesi;
            this.model = model;
            this.idLicenca = idLicenca;
            this.idStatusDok = idstatusdok;
        }

        public clsRoli(DataRow rreshti)
        {
            
            mbushRolin(rreshti);
        }

        #endregion

        #region Metoda Internal
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataRow mbushRolin()
        {
            try
            {
                DataTable rolet = new DataTable("rolet");

                rolet.Columns.Add("IDROLI", (new System.Decimal()).GetType());
                rolet.Columns.Add("KODIROLI", ("").GetType());
                rolet.Columns.Add("PERSHKROLI", ("").GetType());
                rolet.Columns.Add("AKTIVROLI", (new System.Boolean()).GetType());
                rolet.Columns.Add("KRIJUESI", (new System.Decimal()).GetType());
                rolet.Columns.Add("MODEL", (new System.Decimal()).GetType());
                rolet.Columns.Add("IDLICENCA", (new System.Decimal()).GetType());
                rolet.Columns.Add("IDSTATUSDOK", (new System.Decimal()).GetType());
                rolet.Columns.Add("DTKRIJIMI", (new System.DateTime()).GetType());
                rolet.Columns.Add("DTMODIFIKIMI", (new System.DateTime()).GetType());

                DataRow rreshti = rolet.NewRow();

                rreshti["IDROLI"] = this.idRoli;
                rreshti["KODIROLI"] = this.kodRoli;
                rreshti["PERSHKROLI"] = this.pershkrimRoli;
                rreshti["AKTIVROLI"] = this.aktivRoli;
                rreshti["KRIJUESI"] = this.idPerdoruesi;
                rreshti["MODEL"] = this.model;
                rreshti["IDLICENCA"] = this.idLicenca;
                rreshti["IDSTATUSDOK"] = this.idStatusDok;
                rreshti["DTKRIJIMI"] = this.dtKrijimi;
                rreshti["DTMODIFIKIMI"] = this.dtModifikimi;
                return rreshti;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roli"></param>
        /// <returns></returns>
        internal DataRow mbushRolin(clsRoli roli)
        {
            try
            {
                DataTable rolet = new DataTable("rolet");

                rolet.Columns.Add("IDROLI", (new System.Decimal()).GetType());
                rolet.Columns.Add("KODIROLI", ("").GetType());
                rolet.Columns.Add("PERSHKROLI", ("").GetType());
                rolet.Columns.Add("AKTIVROLI", (new System.Boolean()).GetType());
                rolet.Columns.Add("KRIJUESI", (new System.Decimal()).GetType());
                rolet.Columns.Add("MODEL", (new System.Decimal()).GetType());
                rolet.Columns.Add("IDLICENCA", (new System.Decimal()).GetType());
                rolet.Columns.Add("IDSTATUSDOK", (new System.Decimal()).GetType());
                rolet.Columns.Add("DTKRIJIMI", (new System.DateTime()).GetType());
                rolet.Columns.Add("DTMODIFIKIMI", (new System.DateTime()).GetType());

                DataRow rreshti = rolet.NewRow();

                rreshti["IDROLI"] = roli.idRoli;
                rreshti["KODIROLI"] = roli.kodRoli;
                rreshti["PERSHKROLI"] = roli.pershkrimRoli;
                rreshti["AKTIVROLI"] = roli.aktivRoli;
                rreshti["KRIJUESI"] = roli.idPerdoruesi;
                rreshti["MODEL"] = roli.model;
                rreshti["IDLICENCA"] = roli.idLicenca;
                rreshti["IDSTATUSDOK"] = roli.idStatusDok;
                rreshti["DTKRIJIMI"] = roli.dtKrijimi;
                rreshti["DTMODIFIKIMI"] = roli.dtModifikimi;
                return rreshti;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// mbush objektin nga nje datarow i marr nga db-ja
        /// </summary>
        /// <param name="rreshti">datarow me te dhenat e te drejtes</param>
        /// <returns>True nese te dhenat merren me sukses, False perndryshe</returns>
        internal bool mbushRolin(DataRow rreshti)
        {
            try
            {
                this.idRoli = Convert.ToInt32(rreshti["IDROLI"]);
                this.kodRoli = Convert.ToString(rreshti["KODIROLI"]);
                this.pershkrimRoli = Convert.ToString(rreshti["PERSHKROLI"]);
                this.aktivRoli = Convert.ToBoolean(rreshti["AKTIVROLI"]);
                this.idPerdoruesi = Convert.ToInt32(rreshti["KRIJUESI"]);
                this.model = Convert.ToInt32(rreshti["MODEL"]);
                this.idLicenca = Convert.ToInt32(rreshti["IDLICENCA"]);
                this.idStatusDok = Convert.ToInt32(rreshti["IDSTATUSDOK"]);
                DateTime.TryParse(rreshti["DTKRIJIMI"].ToString(), out  dtKrijimi);
                DateTime.TryParse(rreshti["DTMODIFIKIMI"].ToString(), out dtModifikimi);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// krijon nje kopje ekzakte te objektit (kopjim ne vlere)
        /// </summary>
        /// <returns>kopja ekzakte</returns>
        internal clsRoli clone()
        {
            clsRoli roli = new clsRoli();
            roli.idRoli = this.idRoli;
            roli.kodRoli = this.kodRoli;
            roli.pershkrimRoli = this.pershkrimRoli;
            roli.aktivRoli = this.aktivRoli;
            roli.idPerdoruesi = this.idPerdoruesi;
            roli.model = this.model;
            roli.idLicenca = this.idLicenca;
            roli.idStatusDok = this.idStatusDok;
            roli.dtKrijimi = this.dtKrijimi;
            roli.dtModifikimi = this.dtModifikimi;
            return roli;
        }

        #endregion

        #region Metoda Publike

        public static DataRow merrRolDR(int idRoli)
        {
            clsDatabaseAdmin dbartikuj = new clsDatabaseAdmin();
            DataRow rreshti = dbartikuj.merrRolDR(idRoli);
            dbartikuj.Dispose();
            return rreshti;
        }

        public bool mbushRolSipasId(int idRoli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            return mbushRolin(data.merrRolin(idRoli));            
        }

        /// <summary>
        /// shkruan te drejten ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh update(clsDatabaseAdmin data)
        {
            try
            {
                bool u_modifikua = data.modifikoRol(this.idRoli, this.pershkrimRoli, this.aktivRoli, this.IdPerdoruesi, this.idStatusDok);
                if (!u_modifikua)
                    return new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes!");
                return new clsMesazh(true, "Ruajtja përfundoi me sukses!");
            }
            catch
            {
                return new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes!");
            }
        }    

        /// <summary>
        /// update-on te dhenat e rolit
        /// </summary>
        /// <param name="idGjuha">id e gjuhes</param>
        /// <param name="tedrejta">te drejtat ku ruhen vitet dhe ndermarrjet</param>
        /// <param name="idllojlicence">lloji i licences</param>        
        /// <returns>nje objekt te tipit ClsMesazh, me status true nqs ndryshimet kryhen me sukses dhe false ne te kundert.</returns>
        public clsMesazh updateRolin(int idGjuha, ArrayList tedrejta, int idllojlicence, string ndryshuarTeDrejtat)
        {
            using (var scope = new MyTransactionScope())
            using (var data = new clsDatabaseAdmin())
            {
                
                try
                {
                    clsMesazh mesazh = new clsMesazh();
                    mesazh = this.update(data);
                    if (!mesazh.Status)                    
                        return mesazh;                    

                    if (bool.Parse(ndryshuarTeDrejtat)) //nqs nuk jane ndryshuar ndermarrjet apo vitet e zgjedhura atehere nuk duhet te fshihen apo te shtohen te drejta e tjera
                    {
                        ArrayList oldtedrejtat = new ArrayList();
                        colNdermarrjet ndermarrjet = new colNdermarrjet(idRoli, data);
                        //mbush te gjitha te drejtat nga db per te pare si ka qene
                        foreach (clsNdermarrje nder in ndermarrjet)
                        {
                            colVitet vitet = new colVitet();
                            vitet.mbushVitetTeNdermarjesDheRolit(idRoli, nder.IdNdermarrje, data);
                            foreach (clsViti vit in vitet)
                                oldtedrejtat.Add(nder.IdNdermarrje + "," + vit.KodiViti + "," + true + "," + vit.IdViti);
                        }
                        
                        //cdo rresht qe gjendet tek te drejtat sic ka qene hiqet, sepse do te thote se ekzistojne si rreshta ne databaze
                        //duam ato qe nuk ekzistojne te krijohen si rreshta ne db
                        for (int i = 0; i < tedrejta.Count; i++)
                        {
                            for (int j = 0; j < oldtedrejtat.Count; j++)
                            {
                                if (tedrejta[i].ToString().Split(',')[0] == oldtedrejtat[j].ToString().Split(',')[0] && tedrejta[i].ToString().Split(',')[1] == oldtedrejtat[j].ToString().Split(',')[1]
                                    && tedrejta[i].ToString().Split(',')[3] == oldtedrejtat[j].ToString().Split(',')[3])
                                {
                                    oldtedrejtat.RemoveAt(j);
                                    break;
                                }
                            }
                        }
                        //nese ka vite qe nuk jane ne ambjent, por gjenden tek te drejtat (pra kemi fshire komplet nje ndermarrje nga te drejtat) atehere fshijme te gjithe rreshtat perkates
                        for (int j = 0; j < oldtedrejtat.Count; j++)
                        {
                            int idNdermarrje = int.Parse(oldtedrejtat[j].ToString().Split(',')[0]);
                            int idViti = int.Parse(oldtedrejtat[j].ToString().Split(',')[3]);

                            clsTeDrejtaRoliKoka fshiTeDrejta = new clsTeDrejtaRoliKoka();
                            mesazh = fshiTeDrejta.fshiTeDrejtatNgaTabelat(data, idNdermarrje, idViti, this.idRoli);
                            if (!mesazh.Status)
                                return mesazh;
                        }
                        //per te gjithe te ndermarrjet qe jane ne ambjent kontrollojme nese ato ekzistojne ne databaze si rreshta. Nuk na intereson si jane vlerat,
                        //mjafton te ekzistojne. Ne rast se skemi te drejta tek nje vit i caktuar, por ai ekziston ne databaze, (pra kemi hequr te drejtat nga nje vit por jo gjithe ndermarrja, atehere e fshijme ate rresht 

                        for (int i = 0; i < tedrejta.Count; i++)
                        {
                            var teDrejtaArray = tedrejta[i].ToString().Split(',');
                            int idNdermarrje = int.Parse(teDrejtaArray[0]);
                            int idVit = int.Parse(teDrejtaArray[3]);
                            if (!clsTeDrejtaRoli.kaTeDrejtaRoliPerNdermarrjeDheVit(idRoli, idNdermarrje, idVit, data))
                            {
                                if (bool.Parse(teDrejtaArray[2]))
                                {
                                    clsTeDrejtaRoliKoka ruajTeDrejta = new clsTeDrejtaRoliKoka();

                                    mesazh = ruajTeDrejta.krijoTeGjitheTeDrejtaBaze(data, idNdermarrje, idVit, idRoli, idllojlicence);
                                    if (!mesazh.Status)
                                        return mesazh;
                                }

                            }
                            else if(!bool.Parse(teDrejtaArray[2]))
                            {
                                clsTeDrejtaRoliKoka fshiTeDrejta = new clsTeDrejtaRoliKoka();
                                mesazh = fshiTeDrejta.fshiTeDrejtatNgaTabelat(data, idNdermarrje, idVit, this.idRoli);
                                if (!mesazh.Status)
                                    return mesazh;
                            }
                            
                        }
                    }
                    scope.Complete();
                    return new clsMesazh(true, "Ruajtja përfundoi me sukses!");
                }
                catch
                {
                    return new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes së të dhënave!");
                }
            }
        }
        

        public clsMesazh krijoRolDheTeDrejtaBaze(int idGjuha, String kodRoli, String pershkrimRoli, bool aktivRoli, int idKrijuesi, int model, int idlicenca, ArrayList tedrejta, int idstatusdok, int idLlojLicenca)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = new clsMesazh();
            int idRoli = -1;
            try
            { 
                data.beginTransaksion();
                if (data.ekzistonRol(kodRoli, idlicenca) > 0)
                {
                    data.rollbackTransaksion();
                    return new clsMesazh(false, "Ekziston një rol me këtë kod!");
                }
                mesazh = data.krijoRol(out idRoli, kodRoli, pershkrimRoli, aktivRoli, idKrijuesi, model, idlicenca, idstatusdok);
                this.idRoli = idRoli;
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
                clsTeDrejtaRoliKoka ruajTeDrejta = new clsTeDrejtaRoliKoka();
                for (int i = 0; i < tedrejta.Count; i++)
                {
                    if (tedrejta[i].ToString().Split(',')[2] == "false")
                        continue;
                    int idNdermarrje = int.Parse(tedrejta[i].ToString().Split(',')[0]);
                    int idViti = int.Parse(tedrejta[i].ToString().Split(',')[3]);                    

                    mesazh = ruajTeDrejta.krijoTeGjitheTeDrejtaBaze(data, idNdermarrje, idViti, idRoli, idLlojLicenca);
                    if (!mesazh.Status)
                    {
                        data.rollbackTransaksion();
                        return mesazh;
                    }
                }
                data.commitTransaksion();
                return new clsMesazh(true, "Ruajtja përfundoi me sukses!");
            }
            catch (Exception)
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi një gabim gjatë ruajtjes!");
            }
        }


        /// <summary>
        /// Fshin objektin e te drejtave ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiRolStatus"/> 
        /// </summary>
        public bool fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool u_fshi = data.fshiRolStatus(this.idRoli, this.idPerdoruesi);
            return u_fshi;
        }
        
        
        public clsMesazh ruajRoleDefaultMeTeDrejtaRaportesh(int idndermarje, int idviti, int idperdoruesi, int idllojlicence, int idlicenca)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh mesazh = data.ruajRoleDefaultMeTeDrejtaRaportesh(idndermarje, idviti, idperdoruesi, idllojlicence, idlicenca);
            data.Dispose();
            return mesazh;
        }
        
        /// <summary>
        /// Ndryshon llojin e licences
        /// </summary>
        /// <param name="idlicenca">idlincenca</param>
        /// <param name="idllojlicence">lloji i licences per tu bere</param>
        /// <returns></returns>
        public bool ndryshoLlojLicence(int idlicenca, int idllojlicence)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = data.ndryshoLlojLicence(idlicenca, idllojlicence);
            data.Dispose();
            return sukses;
        }

        public static clsMesazh ruajRolAdminNdermarjeDheRaportesh(int idndermarje, int idviti, int idperdoruesi, int idRoli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();

            clsTeDrejtaRoliKoka shtoTeDrejta = new clsTeDrejtaRoliKoka();
            return shtoTeDrejta.ShtoTeGjitheTeDrejtaBazeNeNjeMeTransaksionNeSp(data, idndermarje, idviti, idperdoruesi, idRoli);

            #region Ishte Me Pare
            /* Denisa komentuar pasi ka kaluar ne sp gjate strukturimit dt:05/06/2015
            bool sukses = false;
            try
            {
                data.beginTransaksion();
                sukses = data.ruajRolAdminNdermarje(idndermarje, idviti, idperdoruesi, idRoli);
                if (!sukses)
                {
                    data.rollbackTransaksion();
                    return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se te drejtave!");
                }
                clsMesazh mesazh = data.ruajTeDrejtaPerRaportetNdermarje(idndermarje, idviti, idperdoruesi, idRoli);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }  mesazh = data.ruajTeDrejtaPerTabeshNdermarje(idndermarje, idviti, idperdoruesi, idRoli);
                if (!mesazh.Status)
                {
                    data.rollbackTransaksion();
                    return mesazh;
                }
                data.commitTransaksion();
                return mesazh;
            }
            catch
            {
                data.rollbackTransaksion();
                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se te drejtave!"); ;
            }*/
            #endregion
        }

        public static bool hiqTeDrejtaBij(int idndermarje)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = data.hiqTeDrejtaBij(idndermarje);
            data.Dispose();
            return sukses;
        }
        public static int ktheIdRoliSipasKodit(string kodi, int idlicenca, int idPerdoruesi)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ekzistonRolSipasKodit(kodi, idlicenca, idPerdoruesi);
            }
        }

        /// <summary>
        /// Nuk perdoret.
        /// </summary>
        public static int ktheIdRoli(string kodi, int idlicenca)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
            {
                return db.ekzistonRol(kodi, idlicenca);
            }
        }

        #endregion

        #region Metoda Private


        #endregion
    }
}