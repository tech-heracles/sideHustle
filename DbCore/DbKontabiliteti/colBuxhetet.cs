using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbKontabiliteti
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsBuxheti
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    public class colBuxhetet : System.Collections.Generic.List<clsBuxheti>
    {
        #region Konstruktore

        /// <summary>
        /// konstruktore pa parametra
        /// </summary>
        public colBuxhetet()
        {
        }

        /// <summary>
        /// konstruktore me 2 parametra
        /// </summary>
        /// <param name="id">id lidhese</param>
        /// <param name="idlloj">id e llojit</param>
        public colBuxhetet(int id, int idlloj)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                mbushBuxhetet(dbBuxhetet.ktheBuxhetetSipasIdLidheseIdLlojBuxheti(id, idlloj));
            }
        }

        /// <summary>
        /// konstruktore qe mbush colBuxhetet ne baze te id-se lidhese dhe kodit te llojit te buxhetit
        /// </summary>
        /// <param name="id">id lidhese</param>
        /// <param name="kodlloj">kodi i llojit te buxhetit</param>
        public colBuxhetet(int id, string kodlloj)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                mbushBuxhetet(dbBuxhetet.ktheBuxhetetSipasIdLidheseKodLlojBuxheti(id, kodlloj));
            }
        }
        public colBuxhetet(int id, int idlloj, int idnderviti, DateTime dtaktivizimi)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                mbushBuxhetet(dbBuxhetet.ktheBuxhetetSipasIdLidheseIdLlojBuxhetiDheIdNderviti(id, idlloj, idnderviti, dtaktivizimi));
            }
        }
        /// <summary>
        /// mbush collection te buxheteve
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idlloj"></param>
        /// <param name="idNderViti"></param>
        /// <param name="merrProjektBuxhetet">true kur lloji eshte Projekt buxhet dhe false kur lloji eshte Buxhet</param>
        public colBuxhetet(int id, int idlloj, int idNderViti, bool merrProjektBuxhetet, DateTime dt)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                mbushBuxhetet(dbBuxhetet.ktheBuxhetetSipasIdLidheseKategoriShpenzimi(id, idlloj, idNderViti, merrProjektBuxhetet, dt));
            }
        }

        /// <summary>
        /// mbush collection te buxheteve
        /// </summary>
        /// <param name="id"></param>
        /// <param name="idNderViti"></param>
        /// <param name="dtAktivizimi">data  e aktivizimit te buxhetit</param>
        public colBuxhetet(int id, int idNderViti, DateTime dtAktivizimi)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                mbushBuxhetet(dbBuxhetet.ktheBuxhetetSipasIdLidheseDheDateAktivizimi(id, idNderViti, dtAktivizimi));
            }
        }

        public colBuxhetet(int id, int idlloj, string idNderViti, DateTime dt)
            : this(id, idlloj, Convert.ToInt32(idNderViti), false, dt)
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsBuxheti"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsBuxheti this[int index]
        {
            get { return ((clsBuxheti)base[index]); }
        }

        /// <summary>
        /// metoda per shtimin e nje objekti clsBuxheti ne nje indeks te caktuar ne nje arraylist
        /// </summary>
        public void shtoBuxhetNeIndeksin(int index, clsBuxheti buxheti)
        {
            base.Insert(index, buxheti);
        }

        public static DataTable ktheBuxhetetPerQendratEKostos(int idLidhese, int idNdermViti, DateTime dtakt, int idkonfigurdherpagese)
        {
            using (clsDatabaseKontabilitet dbBuxhetet = new clsDatabaseKontabilitet())
            {
                if (dtakt == DateTime.MinValue)
                    dtakt = new DateTime(1970, 1, 1);
                return dbBuxhetet.ktheBuxhetetPerQendratEKostos(idLidhese, idNdermViti, dtakt, idkonfigurdherpagese);
            }
        }

        public static List<string> ktheDataBuxhetetPerQendratEKostos(int idqk, int idnderviti, int idllojbuxheti, int idviti, bool eshteprojekt)
        {
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet();
            List<string> list = new List<string>();
            DataTable dt = db.ktheDataBuxhetetPerQendratEKostos(idqk, idnderviti, idllojbuxheti, idviti, eshteprojekt);
            foreach (DataRow rreshti in dt.Rows)
            {
                list.Add(DateTime.Parse(rreshti["DTAKTIVIZIMI"].ToString()).ToShortDateString());
            }
            db.Dispose();
            return list;
        }

        /// <summary>
        /// ruan buxhetet per gjithe kategorite e shpenzimit te nderm kur behet mbyllje viti
        /// </summary>
        /// <returns>kthen objekt clsMesazh</returns>
        public static clsMesazh krijoBuxhetetPerGjitheKategoriteENder(int idllojBuxheti, int idNdermarje, int idNderVit, DateTime dta, DbAdmin.clsDatabaseAdmin dbAdmin)
        {
            String[] muaj = { "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor" };
            clsDatabaseKontabilitet db = new clsDatabaseKontabilitet(dbAdmin);
            DataTable dt = db.ktheGjitheKategoriShpenzimiSipasNdermarjes(idNdermarje);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    int idKategoria = Convert.ToInt32(row["ID"].ToString());
                    for (int i = 0; i < 12; i++)
                    {
                        int idB;
                        clsMesazh mesazh = db.ruajBuxhet(out idB, idllojBuxheti, idKategoria, muaj[i], Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), idNderVit, dta, 0);
                        if (!mesazh.Status)
                            return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se buxhetit!");
                    }
                }
            }
            return new clsMesazh(true, "Ruajtja e buxheteve per kategorite e shpenzimeve perfundoi me sukses!");
        }

        /// <summary>
        /// ruan buxhetet per kategorine e shpenzimit per te gjitha vitet e ndermarrjes ne te cilat buxhetet e kesaj kategorie nuk ekzistojne
        /// </summary>
        /// <returns>kthen objekt clsMesazh</returns>
        public static clsMesazh krijoBuxhetetPerGjitheVitetENdermPerKategorine(int idllojBuxheti, int idNdermarje, int idKategoriShpenzimi, clsDatabaseKontabilitet db)
        {
            DataTable dt = db.ktheGjitheVitetENdermPerBuxhetetSipasKategorise(idNdermarje, idKategoriShpenzimi, idllojBuxheti);
            if (dt.Rows.Count > 0)
            {
                String[] muaj = { "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor" };
                foreach (DataRow row in dt.Rows)
                {
                    int idNdermViti = Convert.ToInt32(row["idnderviti"]);
                    for (int i = 0; i < 12; i++)
                    {
                        int idB;
                        clsMesazh mesazh = db.ruajBuxhet(out idB, idllojBuxheti, idKategoriShpenzimi, muaj[i], Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), idNdermViti, new DateTime(Convert.ToInt32(row["viti"]), 1, 1), 0);
                        if (!mesazh.Status)
                            return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se buxhetit!");
                    }
                }
            }
            return new clsMesazh(true, "Ruajtja e buxheteve per kategorine e shpenzimit perfundoi me sukses!");
        }

        /// <summary>
        /// ruan buxhetet per qendren e kostos per te gjitha vitet e ndermarrjes ne te cilat buxhetet e kesaj qender kostoje nuk ekzistojne
        /// </summary>
        /// <returns>kthen objekt clsMesazh</returns>
        public static clsMesazh ktheGjitheVitetENdermPerBuxhetetSipasQendraKostos(int idllojBuxheti, int idNdermarje, int idQk, int idnderVitLoguar, clsDatabaseKontabilitet db)
        {
            DataTable dt = db.ktheGjitheVitetENdermPerBuxhetetSipasKategorise(idNdermarje, idQk, idllojBuxheti);
            if (dt.Rows.Count > 0)
            {
                String[] muaj = { "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor" };
                foreach (DataRow row in dt.Rows)
                {
                    int idNdermViti = Convert.ToInt32(row["idnderviti"]);
                    if (idnderVitLoguar == idNdermViti)
                        continue;
                    else
                    {
                        for (int i = 0; i < 12; i++)
                        {
                            int idB;
                            clsMesazh mesazh = db.ruajBuxhet(out idB, idllojBuxheti, idQk, muaj[i], Convert.ToDecimal("0.00"), Convert.ToDecimal("0.00"), idNdermViti, new DateTime(Convert.ToInt32(row["viti"]), 1, 1), 0);
                            if (!mesazh.Status)
                                return new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se buxhetit!");
                        }
                    }
                }
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// <returns>Kthen nje collection me objen=kte te tipit clsBuxhet me vlerat e buxhetit fillestar (0)</returns>
        /// </summary>
        public static colBuxhetet KrijoBuxhetetFillestare(DateTime dt, bool firtRow = true)
        {
            colBuxhetet buxhete = new colBuxhetet
            {
                new clsBuxheti(0, 0, "Totali", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Janar", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Shkurt", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Mars", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Prill", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Maj", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Qershor", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Korrik", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Gusht", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Shtator", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Tetor", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Nentor", 0, 0, 0, 0, 0,dt,0),
                new clsBuxheti(0, 0, "Dhjetor", 0, 0, 0, 0, 0,dt,0)
            };

            if (!firtRow)
                buxhete.RemoveAt(0);

            return buxhete;
        }

        public clsBuxheti KtheBuxhetinTotal()
        {
            decimal buxheti1 = (from a in this select a.Buxheti_1).Sum();
            decimal buxheti2 = (from a in this select a.Buxheti_2).Sum();
            decimal diff1 = (from a in this select a.Diferenca_1).Sum();
            decimal diff2 = (from a in this select a.Diferenca_2).Sum();
            DateTime st = new DateTime(1970, 1, 1);
            if (this.Count > 0)
                st = this[0].DtAktivizimi;
            return new clsBuxheti(0, 0, "Total", buxheti1, buxheti2, 0, diff1, diff2, st, 0);
        }

        public static colBuxhetet KrijoBuxhetetSipasLlojitTeBuxhetit(string vleratBuxhet1, string vleratBuxhet2, DateTime dtaktivizimi, int idkapitull, string vleraShenime = "")
        {
            decimal[] buxh1 = new decimal[12];
            decimal[] buxh2 = new decimal[12];
            string[] shenimet = vleraShenime.Split(',');
            KtheVleratEBuxheteve(vleratBuxhet1, ref buxh1);
            KtheVleratEBuxheteve(vleratBuxhet2, ref buxh2);
            colBuxhetet colBuxh = new colBuxhetet();
            String[] muaj =
            {
                "Janar", "Shkurt", "Mars", "Prill", "Maj", "Qershor", "Korrik", "Gusht", "Shtator", "Tetor", "Nentor", "Dhjetor"
            };
            for (int i = 0; i < 12; i++) //krijohet kolectioni me buxhetet e futura nga perdoruesi
            {
                var clsBuxh = new clsBuxheti
                {
                    Buxheti_1 = buxh1[i],
                    Buxheti_2 = buxh2[i],
                    Muaj = muaj[i],
                    Shenime = vleraShenime == "" ? "" : shenimet[i],
                    DtAktivizimi = dtaktivizimi,
                    IdKonfigUrdherPagese = idkapitull
                };
                colBuxh.Add(clsBuxh);
            }
            return colBuxh;
        }

        public static colBuxhetet KrijoBuxhetet(int idLidhese, string kodLlojBuxheti, int idNderViti, DateTime dt, bool eshteProjektBuxheti = false, bool merrInfoLlogarie = false)
        {
            int idLlojBuxheti = clsLlojBuxheti.mbushIDLlojBuxheti(kodLlojBuxheti);
            colBuxhetet colBuxh;
            if (merrInfoLlogarie) //mbush buxhetet duke llogaritur dhe gjendjen e llogarive sipas muajit dhe si pasoje ndryshojne dhe diferencat
                colBuxh = new colBuxhetet(idLidhese, idLlojBuxheti, idNderViti, dt);
            else
                colBuxh = new colBuxhetet(idLidhese, idLlojBuxheti, idNderViti, eshteProjektBuxheti, dt);
            if (colBuxh.Count == 0)
                colBuxh = KrijoBuxhetetFillestare(dt);
            else
            {
                decimal shuma1 = 0;
                decimal shuma2 = 0;
                decimal shumagjendja = 0;
                for (int i = 0; i < 12; i++) //me vone duhet shtuar edhe gjendja
                {
                    shuma1 += colBuxh[i].Buxheti_1;
                    shuma2 += colBuxh[i].Buxheti_2;
                    shumagjendja += colBuxh[i].Gjendja;
                }
                colBuxh.shtoBuxhetNeIndeksin(0, new clsBuxheti(0, 0, "Total", shuma1, shuma2, shumagjendja, shuma1 - shumagjendja, shuma2 - shumagjendja, dt, 0));
            }
            return colBuxh;
        }

        public static colBuxhetet KrijoBuxhetetEModifikuara(string kodLlojBuxheti, int idLidhese, int idNderViti, bool eshteProjektBuxheti, string vleratBuxheti1, string vleratBuxheti2, DateTime dtAktivizimi, int idkapitull, string vleratBuxhetiShenime = "", bool sipasDates = false)
        {
            colBuxhetet colBuxhparaardhese = new colBuxhetet(idLidhese, clsLlojBuxheti.mbushIDLlojBuxheti(kodLlojBuxheti), idNderViti, eshteProjektBuxheti, dtAktivizimi);

            if (colBuxhparaardhese.Count != 0)
            {
                decimal[] buxh1 = new decimal[12];
                decimal[] buxh2 = new decimal[12];
                for (int i = 0; i < 12; i++)
                {
                    buxh1[i] = colBuxhparaardhese[i].Buxheti_1; //shtohen vlerat e buxheteve  eksistuese
                    buxh2[i] = colBuxhparaardhese[i].Buxheti_2;
                }
                KtheVleratEBuxheteve(vleratBuxheti1, ref buxh1);
                KtheVleratEBuxheteve(vleratBuxheti2, ref buxh2);
                string[] shenimet = vleratBuxhetiShenime.Split(',');
                for (int i = 0; i < 12; i++)
                {
                    colBuxhparaardhese[i].Buxheti_1 = buxh1[i]; //kalon vlerat e reja te buxhetit tek kolectioni
                    colBuxhparaardhese[i].Buxheti_2 = buxh2[i];
                    colBuxhparaardhese[i].Shenime = vleratBuxhetiShenime == "" ? "" : shenimet[i];
                    colBuxhparaardhese[i].DtAktivizimi = dtAktivizimi;
                    colBuxhparaardhese[i].IdKonfigUrdherPagese = idkapitull;
                }
            }
            else
                colBuxhparaardhese = KrijoBuxhetetSipasLlojitTeBuxhetit(vleratBuxheti1, vleratBuxheti2, dtAktivizimi, idkapitull, vleratBuxhetiShenime);
            return colBuxhparaardhese;
        }

        public static void KtheVleratEBuxheteve(string vleratBuxhet, ref decimal[] buxh)
        {
            if (vleratBuxhet != "") //merren te dhenat e hiden fieldeve te buxheteve nga javascipti
            {
                string[] pars1 = vleratBuxhet.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    string[] pars2 = pars1[i].Split(':');
                    buxh[Convert.ToInt32(pars2[0]) - 1] = Decimal.Parse(pars2[1]);
                }
            }
        }

        #endregion

        #region Metoda Private

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        ///  <see cref="DbCore.DbKontabiliteti.clsBuxheti"/> 
        /// </summary>
        private void mbushBuxhetet(DataTable dt)
        {
            foreach (DataRow rreshti in dt.Rows)
            {
                Add(new clsBuxheti(rreshti));
            }
        }
        #endregion
    }
}

