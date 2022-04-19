using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbListPagesat
{
    /// <summary>
    ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsPunesim
    ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <seealso cref="clsPunesim"/>
    ///    

    public class colPunesim : List<clsPunesim>
    {
        private const string gabimpunonjesi = "Punonjesi nuk u ruajt!";
        private const string gabimpunesimi = "Nje nga punesimet nuk u ruajt!";
        #region Konstruktoret

        /// <summary>
        /// konstruktor pa parameter
        /// </summary>
        public colPunesim()
        {
        }

        /// <summary>
        /// konstruktori qe implementon klasen baze
        /// </summary>
        /// <param name="collection">koleksion me clsPunesim</param>
        public colPunesim(IEnumerable<clsPunesim> collection)
            : base(collection)
        {

        }
        /// <summary>
        /// kontrukutori me 1 parametra
        /// merr punesimet e nje punonjesi
        /// </summary>
        /// <param name="idpunonjes"> idpunonjes</param>
        public colPunesim(int idpunonjes) : this(new clsDatabazeListPagesa().kthePunesimSipasIdPuneonjesi(idpunonjes))
        {
        }
        #endregion

        #region Metoda Publike

        /// <summary>
        /// kthen objektin <see cref="clsPunesim"/>  qe ndodhet ne nje index te caktuar te arraylist-es
        /// </summary>
        public new clsPunesim this[int index]
        {
            get { return ((clsPunesim)base[index]); }
        }
        public static colPunesim KthePunesimSipasIdPuneonjesiDt(int idpunonjes, int idgjuha)
        {
            using (clsDatabazeListPagesa db = new clsDatabazeListPagesa())
                return new colPunesim(db.kthePunesimSipasIdPuneonjesiDt(idpunonjes, idgjuha).OrderByDescending(x=>x.DtAktivizimi));
        }

        internal clsMesazh modifikoPunesime(clsDatabazeListPagesa db, clsPunonjes punonjesvjeter, int idPunonjes, int idPerdoruesi, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            clsMesazh mesazh = new clsMesazh();
            //TODO Getson (merge sql)
            clsPunesim punesimfundit = new clsPunesim();
            if (punonjesvjeter.OColPunesimet.Count > 0)
                punesimfundit = punonjesvjeter.OColPunesimet[0];
            foreach (clsPunesim punesim in this)
            {
                string fushatmod = "";
                string mesazhmod = "";
                punesim.IdPunonjes = idPunonjes;
                if (punesim.IdPunesim > 0)//heqim punesimet qe jane dhe para modifikimit dhe pas
                {
                    punesimfundit = punonjesvjeter.OColPunesimet.Find(x => x.IdPunesim == punesim.IdPunesim);
                    punonjesvjeter.OColPunesimet.RemoveAt(punonjesvjeter.OColPunesimet.FindIndex(x => x.IdPunesim == punesim.IdPunesim));
                }

                if (punesim.IdPunesim <= 0)//nqs eshte punesim i ri e shtojme perndryshe e modifikojme. ne te dy rastet bejme krahasimin me punesimin e fundit
                {
                    mesazhmod = "U shtua punesim i ri. " + clsPunesim.kontrollopunesim(punesim, punesimfundit, out fushatmod, db);
                    mesazh = punesim.ruaj(db);
                }
                else
                {
                    mesazhmod = clsPunesim.kontrollopunesim(punesim, punesimfundit, out fushatmod, db);
                    mesazh = punesim.modifiko(db);
                }
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgNjePunesimFail", ci));
                

                if (mesazhmod != "U modifikuan fushat per punesimin:")
                {
                    mesazh = clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, mesazhmod, fushatmod, db);
                    if (!mesazh.Status)
                    {
                        return new clsMesazh(false, rm.GetString("msgPunonjesRuajFail",ci));
                    }
                }
            }

            foreach (clsPunesim punesim in punonjesvjeter.OColPunesimet)//fshijme punesimet qe nuk jane me pas modifikimit
            {
                mesazh = punesim.fshi(db);
                if (!mesazh.Status)
                    return new clsMesazh(false, gabimpunesimi);

                mesazh = clsPunonjes.ruajLog(idPunonjes, idPerdoruesi, 1, "U fshi punesimi", "U fshi punesimi", db);
                if (!mesazh.Status)
                    return new clsMesazh(false, gabimpunonjesi);
            }
            if (ekzistonListpagesePaPunesimAktivPerPunonjesin(idPunonjes))
                return new clsMesazh(false, rm.GetString("msgListepagesaPaPunesimAktiv", ci));
            return new clsMesazh(true, "Punesimet u ruajten me sukses!");
        }


        private static bool ekzistonListpagesePaPunesimAktivPerPunonjesin(int idPunonjes)
        {
            using (var db = new clsDatabazeListPagesa())
                return db.ekzistonListpagesePaPunesimAktivPerPunonjesin(idPunonjes);
        }
        public static DataTable kthePunesimNdermarrjesAndAutorizimeDTExport(int idnderm)
        {
            using (clsDatabazeListPagesa dbartikuj = new clsDatabazeListPagesa())
            {
                DataTable tabela = dbartikuj.kthePunesimNdermarrjesAndAutorizimeDTExport(idnderm);
                return tabela;
            }
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
        /// </summary>
        /// <param name="dt"> data table me te dhenat e tipit punesim</param>
        /// <returns>true ose false nqs objekti u mbush ne rregull me te dhena</returns>
        //public bool mbushPunesim(DataTable dt)
        //{
        //    //try
        //    //{

        //        foreach (DataRow rreshti in dt.Rows)
        //        {
        //            //clsPunesim skema = new clsPunesim();
        //            //skema.mbushPunesim(rreshti);
        //            Add(new clsPunesim(rreshti));
        //        }

        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return false;
        //    //    //throw;
        //    //}
        //    return true;
        //}

        #endregion
        public static colPunesim MerrPunesimTeFunditPerPunonjesit(List<int> punonjesIDs, DateTime data)
        {
            clsDatabazeListPagesa db = new clsDatabazeListPagesa();
            var col = new colPunesim(db.kthePunesimTefunditPerPunonjesit(data, punonjesIDs));
            db.Dispose();

            return col;
        }
    }
}
