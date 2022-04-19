using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsGrupetPerdoruesit
    {
        #region Atributet

        private int idGrupiPerdorues;
        private String grupiPerdoruesPershkrimi;
        private int grupiAktivPerdorues;
        private DateTime grupiPerdoruesData;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public clsGrupetPerdoruesit(int idgrupiperdorues, String grupiperdoruespershkrimi, int grupiaktivperdorues, DateTime grupiperdoruesdata)
        {
            this.idGrupiPerdorues = idgrupiperdorues;
            this.grupiPerdoruesPershkrimi = grupiperdoruespershkrimi;
            this.grupiAktivPerdorues = grupiaktivperdorues;
            this.grupiPerdoruesData = grupiperdoruesdata;
        }

        public clsGrupetPerdoruesit()
        { 
        }

        public clsGrupetPerdoruesit(DataRow rreshti)
        {
            
            mbushGrupetPerdoruesit(rreshti);
        }

        #endregion

        #region Properties

        public int IdGrupiPerdorues
        {
            get { return idGrupiPerdorues; }
            set { idGrupiPerdorues = value; }
        }

        public String GrupiPerdoruesPershkrimi
        {
            get { return grupiPerdoruesPershkrimi; }
            set { grupiPerdoruesPershkrimi = value; }
        }

        public int GrupiAktivPerdorues
        {
            get { return grupiAktivPerdorues; }
            set { grupiAktivPerdorues = value; }
        }

        public DateTime GrupiPerdoruesData
        {
            get { return grupiPerdoruesData; }
            set { grupiPerdoruesData = value; }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushGrupetPerdoruesit(DataRow dbDataRowGrupetPerdoruesit)
        {
            if (dbDataRowGrupetPerdoruesit != null)
            {
                try
                {
                    idGrupiPerdorues = int.Parse(dbDataRowGrupetPerdoruesit["IDGRUPIPERD"].ToString());
                    grupiPerdoruesPershkrimi = dbDataRowGrupetPerdoruesit["GRUPIPERDPERSHK"].ToString();
                    grupiAktivPerdorues = int.Parse(dbDataRowGrupetPerdoruesit["GRUPIPERDAKTIV"].ToString());
                    grupiPerdoruesData = (DateTime)dbDataRowGrupetPerdoruesit["GRUPIPERDDATA"];
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se grupeve te perdoruesve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
