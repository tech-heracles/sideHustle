using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne grupet e perdoruesve. Cdo perdoruesi i caktohet
    ///  nje grup. Perdoruesi nga grupi merr te drejtat fillestare.(Te dhenat  merren nga tabela : T_GRUPIPERDORUES)
    /// </summary>
    public class clsGrupiPerdorues
    {
        #region Atributet

        private int idGrupiPerdorues;
        private String grupiPerdoruesKodi;
        private String grupiPerdoruesPershkrimi;
        private bool grupiAktivPerdorues;
        private DateTime grupiPerdoruesData;
        private int idPerdoruesi;

        private colTeDrejtat oColTeDrejta;
        private DataRow rreshti;
        private int idndermarrjeviti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupiPerdorues(int idgrupiperdorues, String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)
        {
            idGrupiPerdorues = idgrupiperdorues;
            grupiPerdoruesKodi = grupiperdorueskodi;
            grupiPerdoruesPershkrimi = grupiperdoruespershkrimi;
            grupiAktivPerdorues = grupiaktivperdorues;
            grupiPerdoruesData = grupiperdoruesdata;
            idPerdoruesi = idperdoruesi;
            oColTeDrejta = new colTeDrejtat();
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupiPerdorues(String grupiperdorueskodi, String grupiperdoruespershkrimi, bool grupiaktivperdorues, DateTime grupiperdoruesdata, int idperdoruesi)
        {
            grupiPerdoruesKodi = grupiperdorueskodi;
            grupiPerdoruesPershkrimi = grupiperdoruespershkrimi;
            grupiAktivPerdorues = grupiaktivperdorues;
            grupiPerdoruesData = grupiperdoruesdata;
            idPerdoruesi = idperdoruesi;
        }

        /// <summary>
        /// konstrukor me 2 parametra
        /// </summary>
        /// <param name="id"> id e grupit te perdoruesve</param>
        /// <param name="idndermarrjevit">id qe lidh ndermarrjen me vitin</param>
        public clsGrupiPerdorues(int id, int idndermarrjevit)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            mbushGrupPerdorues(data.merrGrupPerdorues(id), idndermarrjevit);
            data.Dispose();
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGrupiPerdorues()
        { 
        }

        public clsGrupiPerdorues(DataRow rreshti)
        {
            
            mbushGrupPerdoruesGjitheNdermarrje(rreshti);
        }

        public clsGrupiPerdorues(DataRow rreshti, int idndermarrjeviti)
        {
            
            mbushGrupPerdorues(rreshti, idndermarrjeviti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdGrupiPerdorues
        {
            get { return idGrupiPerdorues; }
            set { idGrupiPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos Kodin per grupin e perdoruesve. Sherben si identifikues llogjik.
        /// </summary>
        public String GrupiPerdoruesKodi
        {
            get { return grupiPerdoruesKodi; }
            set { grupiPerdoruesKodi = value; }
        }

        /// <summary>
        /// Kthen/Vendos Pershkrimi per grupin e perdoruesve.
        /// </summary>
        public String GrupiPerdoruesPershkrimi
        {
            get { return grupiPerdoruesPershkrimi; }
            set { grupiPerdoruesPershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos atributin qe tregon nese grupi eshte aktiv apo jo.
        /// </summary>
        public bool GrupiAktivPerdorues
        {
            get { return grupiAktivPerdorues; }
            set { grupiAktivPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos daten kur eshte krijuar ky grup.
        /// </summary>
        public DateTime GrupiPerdoruesData
        {
            get { return grupiPerdoruesData; }
            set { grupiPerdoruesData = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka krijuar kete grup.
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value;  }
        }

        /// <summary>
        /// Kthen/Vendos nje collection me te objekte te tipit <c> clsTeDrejtat</c>. Cdo objekt i tipit 
        /// <c>clsGrupiPerdorues</c> permban nje collection te tille.
        /// </summary>
        public colTeDrejtat OColTeDrejtat
        {
            get { return oColTeDrejta; }
            set { oColTeDrejta = value; }
        }

        #endregion

        #region Metoda Publike

        ///// <summary>
        ///// Ruan ne databaze nje objekt te tipti <c>clsGrupiPerdorues</c>, sebashku me collectionin me 
        ///// objekte te tipit <c> clsTeDrejtat</c>.
        ///// </summary>
        //public clsMesazh ruaj()
        //{
        //    //clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    clsMesazh u_ruajt = ruajGrupPerdoruesishTeDrejta(this);
        //    return u_ruajt;
        //}

        ///// <summary>
        ///// Modifikon ne databaze nje objekt te tipti <c>clsGrupiPerdorues</c> sipas , sebashku me collectionin me 
        ///// objekte te tipit <c> clsTeDrejtat</c>.
        ///// </summary>
        //public clsMesazh modifiko()
        //{
        //    //clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    clsMesazh u_modifikua = modifikoGrupTeDrejta(this);
        //    return u_modifikua;
        //}

        /// <summary>
        /// Fshin nga databaza nje objekt te tipti <c>clsGrupiPerdorues</c>
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiGrupPerdoruesish(this.IdGrupiPerdorues);
            data.Dispose();
            return u_fshi;
        }

        ///// <summary>
        ///// Nuk perdoret.
        ///// </summary>
        //public void merr()
        //{
        //    clsDatabaseAdmin data = new clsDatabaseAdmin();
        //    data.merrGrupPerdoruesish(this.IdGrupiPerdorues);
        //    data.Dispose();
        //}

        /// <summary>
        ///Kthen nje collection me objekte te tipit <c>clsGrupiPerdorues</c> te cilat i merr nga tabela 
        ///perkatese e databazes.
        /// </summary>
        public colGrupetPerdoruesve merriTeGjithe()
        {
            colGrupetPerdoruesve data = new colGrupetPerdoruesve();
            data.mbushGjitheGrupetPerdoruesve();            
            return data;
        }

        //public clsMesazh ruajGrupPerdoruesishTeDrejta(clsGrupiPerdorues grup)
        //{
        //    clsMesazh mesazh;
        //    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();            
        //    dbAdmin.beginTransaksion();
        //    try
        //    {
        //        if (dbAdmin.ekzistonGrupPerdoruesiMeKeteKod(grup.GrupiPerdoruesKodi))
        //        {
        //            return new clsMesazh(false, "Ekziston nje grup me te njejtin kod!");
        //        }
        //        int idG;                
        //        mesazh = dbAdmin.ruajGrupPerdoruesish(out idG, grup.GrupiPerdoruesKodi, grup.GrupiPerdoruesPershkrimi, grup.GrupiAktivPerdorues, grup.GrupiPerdoruesData, grup.IdPerdoruesi);
        //        if (!mesazh.Status)
        //        {
        //            mesazh = new clsMesazh(false, mesazh.PershkrimMesazhi);
        //            return mesazh;
        //        }
        //        grup.IdGrupiPerdorues = idG;
        //        foreach (clsTeDrejtat o in grup.OColTeDrejtat)
        //        {
        //            o.IdPerdorues = grup.IdGrupiPerdorues;
        //            if (!dbAdmin.ekzistonEDrejta(o.IdNderViti, o.IdKomponente, o.IdAmbjentiModuli, o.IdModul, o.IdDrejtaVeprim, o.IdPerdorues))
        //            {
        //                mesazh = dbAdmin.ruajTeDrejte(o.IdDrejta, o.IdNderViti, o.IdKomponente, o.IdModul, o.IdPerdorues, o.IdDrejtaVeprim, o.PerdoruesApoGrup);
        //                if (!mesazh.Status)
        //                {
        //                    dbAdmin.rollbackTransaksion();
        //                    return mesazh;
        //                }
        //            }
        //        }

        //        dbAdmin.commitTransaksion();
        //        mesazh = new clsMesazh(true, "Ruajtja perfundoi me sukses!");
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        dbAdmin.rollbackTransaksion();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        //public clsMesazh modifikoGrupTeDrejta(clsGrupiPerdorues grup)
        //{
        //    clsMesazh mesazh;
        //    clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
        //    dbAdmin.beginTransaksion();
        //    try
        //    {
        //        mesazh = dbAdmin.modifikoGrupPerdoruesish(grup.IdGrupiPerdorues, grup.GrupiPerdoruesKodi, grup.GrupiPerdoruesPershkrimi, grup.GrupiAktivPerdorues, grup.GrupiPerdoruesData, grup.IdPerdoruesi);
        //        if (!mesazh.Status)
        //        {
        //            dbAdmin.rollbackTransaksion();
        //            return mesazh;
        //        }
        //        mesazh = dbAdmin.fshiTeDrejtaGrupPerdorues(grup.IdGrupiPerdorues);
        //        if (!mesazh.Status)
        //        {
        //            dbAdmin.rollbackTransaksion();
        //            return mesazh;
        //        }
        //        foreach (clsTeDrejtat o in grup.OColTeDrejtat)
        //        {
        //            o.IdPerdorues = grup.IdGrupiPerdorues;
        //            if (!dbAdmin.ekzistonEDrejta(o.IdNderViti, o.IdKomponente, o.IdAmbjentiModuli, o.IdModul, o.IdDrejtaVeprim, o.IdPerdorues))
        //            {
        //                mesazh = dbAdmin.ruajTeDrejte(o.IdDrejta, o.IdNderViti, o.IdKomponente, o.IdModul, o.IdPerdorues, o.IdDrejtaVeprim, o.PerdoruesApoGrup);
        //                if (!mesazh.Status)
        //                {
        //                    dbAdmin.rollbackTransaksion();
        //                    return mesazh;
        //                }
        //            }
        //        }
        //        dbAdmin.commitTransaksion();
        //        mesazh = new clsMesazh(true, "Ruajtja perfundoi me sukses!");
        //        return mesazh;
        //    }
        //    catch (Exception)
        //    {
        //        dbAdmin.rollbackTransaksion();
        //        return new clsMesazh(false, ce.Message);
        //    }
        //}

        #endregion

        #region Metoda Internal

        internal bool mbushGrupPerdoruesGjitheNdermarrje(DataRow dbDataRowGrupPerdorues)
        {
            if (dbDataRowGrupPerdorues != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupPerdorues["IDGRUPIPERD"].ToString(), out idGrupiPerdorues);
                    grupiPerdoruesKodi = dbDataRowGrupPerdorues["GRUPIKODI"].ToString();
                    grupiPerdoruesPershkrimi = dbDataRowGrupPerdorues["GRUPIPERDPERSHK"].ToString();
                    bool.TryParse(dbDataRowGrupPerdorues["GRUPIPERDAKTIV"].ToString(), out grupiAktivPerdorues);
                    DateTime.TryParse(dbDataRowGrupPerdorues["GRUPIPERDDATA"].ToString(), out grupiPerdoruesData);
                    int.TryParse(dbDataRowGrupPerdorues["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    colGrupetPerdoruesve colGrup = new colGrupetPerdoruesve();
                    oColTeDrejta = colGrup.merriGjitheTeDrejtatPerGjitheNdermarrjet(this);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te perdoruesit nga db-ja");
                }
            }
            else
                return false;
        }

        internal bool mbushGrupPerdorues(DataRow dbDataRowGrupPerdorues, int idndermarrjeviti)
        {
            if (dbDataRowGrupPerdorues != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupPerdorues["IDGRUPIPERD"].ToString(), out idGrupiPerdorues);
                    grupiPerdoruesKodi = dbDataRowGrupPerdorues["GRUPIKODI"].ToString();
                    grupiPerdoruesPershkrimi = dbDataRowGrupPerdorues["GRUPIPERDPERSHK"].ToString();
                    bool.TryParse(dbDataRowGrupPerdorues["GRUPIPERDAKTIV"].ToString(), out grupiAktivPerdorues);
                    DateTime.TryParse(dbDataRowGrupPerdorues["GRUPIPERDDATA"].ToString(), out grupiPerdoruesData);
                    int.TryParse(dbDataRowGrupPerdorues["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    colGrupetPerdoruesve colGrup = new colGrupetPerdoruesve();
                    oColTeDrejta = colGrup.merriGjitheTeDrejtat(this, idndermarrjeviti);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupit te perdoruesit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
