using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    public class clsVleraKonfigurimiKasa
    {
        #region Atributet
        private int idVlera;
        private int idKonfigurimi;
        private int idOpsioni;
        private string pershkrimOpsioni;
        private string vlera;
        private int idTaksa;
        private DataRow rreshti;
        #endregion
        #region Properties
        public int IdVlera
        {
            get
            {
                return idVlera;
            }
            set
            {
                idVlera = value;
            }
        }
        public int IdKonfigurimi
        {
            get
            {
                return idKonfigurimi;
            }
            set
            {
                idKonfigurimi = value;
            }
        }
        public int IdOpsioni
        {
            get
            {
                return idOpsioni;
            }
            set
            {
                idOpsioni = value;
            }
        }
        public string Vlera
        {
            get
            {
                return vlera;
            }
            set
            {
                vlera = value;
            }
        }
        public string PershkrimOpsioni
        {
            get
            {
                return pershkrimOpsioni;
            }
            set
            {
                pershkrimOpsioni = value;
            }
        }
        public int IdTaksa
        {
            get
            {
                return idTaksa;
            }
            set
            {
                idTaksa = value;
            }
        }
        #endregion
        #region konstruktoret
        public clsVleraKonfigurimiKasa()
        {
        }
        public clsVleraKonfigurimiKasa(int idvlera, int idkonfigurimi, int idopsioni, string pershkrimi, string vlera, int idtaksa)
        {
            this.idVlera = idvlera;
            this.idKonfigurimi = idkonfigurimi;
            this.idOpsioni = idopsioni;
            this.pershkrimOpsioni = pershkrimi;
            this.vlera = vlera;
            this.idTaksa = idtaksa;
        }
        public clsVleraKonfigurimiKasa(int idtaksa)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (!this.mbushVleraKonfigurimKase(data.merrVleraSipasIdTaksa(idtaksa)))
            {
                data.Dispose();
                return;
            }
            data.Dispose();
        }

        public clsVleraKonfigurimiKasa(DataRow rreshti)
        {
            
            mbushVleraKonfigurimKase(rreshti);
        }

        #endregion
        #region Metoda Internal
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataRow mbushVleraKonfigurimKase()
        {
            try
            {
                DataTable kasa = new DataTable("kasa");

                kasa.Columns.Add("IDVLERAKONFIGURIMIKASA", (new System.Decimal()).GetType());
                kasa.Columns.Add("IDKONFIGURIMI", (new System.Decimal()).GetType());
                kasa.Columns.Add("IDOPSIONI", (new System.Decimal()).GetType());
                kasa.Columns.Add("PERSHKRIM", ("").GetType());
                kasa.Columns.Add("VLERA", ("").GetType());
                kasa.Columns.Add("IDTAKSA", (new System.Decimal()).GetType());
                DataRow rreshti = kasa.NewRow();

                rreshti["IDVLERAKONFIGURIMIKASA"] = this.idVlera;
                rreshti["IDKONFIGURIMI"] = this.idKonfigurimi;
                rreshti["IDOPSIONI"] = this.idOpsioni;
                rreshti["PERSHKRIM"] = this.pershkrimOpsioni;
                rreshti["VLERA"] = this.vlera;
                rreshti["IDTAKSA"] = this.idTaksa;
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
        internal DataRow mbushVleraKonfigurimKase(clsVleraKonfigurimiKasa kasat)
        {

            try
            {
                DataTable kasa = new DataTable("kasa");

                kasa.Columns.Add("IDVLERAKONFIGURIMIKASA", (new System.Decimal()).GetType());
                kasa.Columns.Add("IDKONFIGURIMI", (new System.Decimal()).GetType());
                kasa.Columns.Add("IDOPSIONI", (new System.Decimal()).GetType());
                kasa.Columns.Add("PERSHKRIM", ("").GetType());
                kasa.Columns.Add("VLERA", ("").GetType());
                kasa.Columns.Add("IDTAKSA", (new System.Decimal()).GetType());
                DataRow rreshti = kasa.NewRow();


                rreshti["IDVLERAKONFIGURIMIKASA"] = kasat.idVlera;
                rreshti["IDKONFIGURIMI"] = kasat.idKonfigurimi;
                rreshti["IDOPSIONI"] = kasat.idOpsioni;
                rreshti["PERSHKRIM"] = kasat.pershkrimOpsioni;
                rreshti["VLERA"] = kasat.vlera;
                rreshti["IDTAKSA"] = kasat.idTaksa;
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
        internal bool mbushVleraKonfigurimKase(DataRow rreshti)
        {
            if (rreshti == null)
                return false;
            try
            {
                int.TryParse(rreshti["IDVLERAKONFIGUIMIKASA"].ToString(), out idVlera);
                int.TryParse(rreshti["IDKONFIGURIMI"].ToString(), out idKonfigurimi);
                int.TryParse(rreshti["IDOPSIONI"].ToString(), out idOpsioni);
                this.pershkrimOpsioni = rreshti["PERSHKRIMI"].ToString();
                this.vlera = rreshti["VLERA"].ToString();
                int.TryParse(rreshti["IDTAKSA"].ToString(), out idTaksa);

            }
            catch (Exception)
            {
                return false;

            }
            return true;

        }
        #endregion

        #region     Metoda Publike
        public clsMesazh fshiVleraKonfigurimKase(int idkonfigurimi, clsDatabaseAdmin data)
        {
            return data.fshiVleraKonfigurimKase(idkonfigurimi);
        }

        public bool merrVleraSipasIdTaksaJoFshire(int idTaksa, int idKonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool sukses = mbushVleraKonfigurimKase(data.merrVleraSipasIdTaksaJoFshire(idTaksa, idKonfigurimi));
            data.Dispose();
            return sukses;
        }
        public clsMesazh ruajVleraKonfigurimKase(clsDatabaseAdmin data)
        {
            return data.ruajVleraKonfigurimKase(out idVlera, idKonfigurimi, idOpsioni, vlera, this.idTaksa);
        }
        public int merrIdOpsioni(string pershkrimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            int idOpsioni = data.merrIdOpsioni(pershkrimi);
            data.Dispose();
            return idOpsioni;
        }
        public string merrVlereOpsioni(string pershkrimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            string vlereOpsioni = data.merrVlereOpsioni(pershkrimi);
            data.Dispose();
            return vlereOpsioni;
        }

        public void updatePLU(int idndermarrje, int plu, int idKonfigurimiKases)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.updatePLU(idndermarrje, plu, idKonfigurimiKases);
            data.Dispose();
        }

        public string merrPLUActualNumberPerKase(int idndermarrje, int idKonfigurimi)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsVleraKonfigurimiKasa konfig = new clsVleraKonfigurimiKasa();
            DataRow rreshti = data.merrPLUActualNumberPerKase(idndermarrje, idKonfigurimi);
            if (rreshti != null)
            {
                konfig.mbushVleraKonfigurimKase(rreshti);
                data.Dispose();
                return konfig.Vlera;
            }
            data.Dispose();
            return "-1";
        }
        #endregion
    }
}
