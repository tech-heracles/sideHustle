using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe bejne lidhjen e roleve me perdoruesit
    ///  (Te dhenat  merren nga tabela : T_ROLPERDORUES)
    /// </summary>
    public class clsRolPerdorues
    {
        #region Attribute

        /// <summary>
        /// id-ja ritese e rolperdoruesit
        /// </summary>
        private int idRolPerdorues;

        /// <summary>
        /// id e rolit
        /// </summary>
        private int idRoli;

        /// <summary>
        /// id e perdoruesit
        /// </summary>
        private int idPerdorues;

        #endregion

        #region Properties

        /// <summary>
        /// Get Set: id e roliPerdoruesit
        /// </summary>
        public int IdRolPerdorues
        {
            get
            {
                return idRolPerdorues;
            }
            set
            {
                if (idRolPerdorues == value)
                    return;
                idRolPerdorues = value;
            }
        }

        /// <summary>
        /// Get Set: id i rolit
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
        /// Get Set: id e perdoruesit
        /// </summary>
        public int IdPerdorues
        {
            get
            {
                return idPerdorues;
            }
            set
            {
                if (idPerdorues == value)
                    return;
                idPerdorues = value;
            }
        }

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor bosh
        /// </summary>
        public clsRolPerdorues()
        {
        }

        public clsRolPerdorues(DataRow rreshti)
        {
            mbushRolPerdoruesin(rreshti);
        }
        /// <summary>
        /// krijon nje objekt te tipit rolPerdorues duke lexuar te dhenat nga db-ja ne baze te id-se se dhene
        /// </summary>
        /// <param name="idRolPerdorues">id-ja e dhene per te lexuar te dhenat nga db-ja</param>
        public clsRolPerdorues(int idRolPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            if (!this.mbushRolPerdoruesin(data.merrRolPerdorues(idRolPerdorues)))
            {
                data.Dispose();
                return; //roliperdoruesi me id idrolperdoruesi nuk ekziston
            }
            data.Dispose();
            //throw new Exception("ERROR: Gabim gjate leximit te roliperdoruesit " + idRolPerdoruesi + "nga databaza");
        }

        /// <summary>
        /// Konstruktor i plote
        /// </summary>
        /// <param name="idRolPerdorues"></param>
        /// <param name="idRoli"></param>
        /// <param name="idPerdorues"></param>
        public clsRolPerdorues(int idRolPerdorues, int idRoli, int idPerdorues)
        {
            this.idRolPerdorues = idRolPerdorues;
            this.idRoli = idRoli;
            this.idPerdorues = idPerdorues;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal DataRow mbushRolPerdoruesin()
        {
            try
            {
                DataTable roleperdorues = new DataTable("roleperdorues");

                roleperdorues.Columns.Add("IDROLPERDORUES", (new System.Decimal()).GetType());
                roleperdorues.Columns.Add("IDROLI", (new System.Decimal()).GetType());
                roleperdorues.Columns.Add("IDPERDORUES", (new System.Decimal()).GetType());
                DataRow rreshti = roleperdorues.NewRow();

                rreshti["IDROLPERDORUES"] = this.idRolPerdorues;
                rreshti["IDROLI"] = this.idRoli;
                rreshti["IDPERDORUES"] = this.idPerdorues;

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
        internal DataRow mbushRolPerdoruesin(clsRolPerdorues rolperdoruesi)
        {

            try
            {
                DataTable roleperdorues = new DataTable("roleperdorues");

                roleperdorues.Columns.Add("IDROLPERDORUES", (new System.Decimal()).GetType());
                roleperdorues.Columns.Add("IDROLI", (new System.Decimal()).GetType());
                roleperdorues.Columns.Add("IDPERDORUES", (new System.Decimal()).GetType());
                DataRow rreshti = roleperdorues.NewRow();


                rreshti["IDROLPERDORUES"] = rolperdoruesi.idRolPerdorues;
                rreshti["IDROLI"] = rolperdoruesi.idRoli;
                rreshti["IDPERDORUES"] = rolperdoruesi.idPerdorues;
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
        internal bool mbushRolPerdoruesin(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    this.idRolPerdorues = Convert.ToInt32(rreshti["IDROLPERDORUES"]);
                    this.idRoli = Convert.ToInt32(rreshti["IDROLI"]);
                    this.idPerdorues = Convert.ToInt32(rreshti["IDPERDORUES"]);

                }
                catch (Exception)
                {
                    return false;

                }
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// krijon nje kopje ekzakte te objektit (kopjim ne vlere)
        /// </summary>
        /// <returns>kopja ekzakte</returns>
        internal clsRolPerdorues clone()
        {
            clsRolPerdorues rolperdoruesi = new clsRolPerdorues();
            rolperdoruesi.idRolPerdorues = this.idRolPerdorues;
            rolperdoruesi.idRoli = this.idRoli;
            rolperdoruesi.idPerdorues = this.idPerdorues;

            return rolperdoruesi;
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// shkruan te drejten ne databaze
        /// </summary>
        /// <returns>kthehet True nese shkruhet me sukses, False perndryshe</returns>
        public clsMesazh update()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            //data.krijoManager();

            clsMesazh u_modifikua = data.modifikoRolPerdorues(this.idRolPerdorues, this.idRoli, this.idPerdorues);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPerdorues">id e perdoruesit</param>
        /// <param name="idRol">id e rolit</param>
        /// <returns>kthen id-ne e rolit te krijuar ose zero nese krijimi deshtoi</returns>
        public int krijoRolPerdorues(int idRol, int idPerdorues)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            //data.krijoManager();
            int idRolPerdorues = data.krijoRolPerdorues(idRol, idPerdorues);
            if (idRolPerdorues <= 0)
            {
                data.Dispose();
                return idRolPerdorues;
            }

            if (!mbushRolPerdoruesin(data.merrRolPerdorues(idRolPerdorues)))
            {
                data.Dispose();
                return -1;
            }
            data.Dispose();
            return idRolPerdorues;
        }

        /// <summary>
        /// Fshin objektin e rolPerdoruesit.Therret funksionin
        /// :  <see cref="DbCore.DbAdmin.clsDatabaseAdmin.fshiRolPerdorues"/> 
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiRolPerdorues(this.idRoli);
            data.Dispose();
            return u_fshi;
        }

        public static bool eshteRoliLidhurMePerdorues(int idRoli)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            bool ekziston = data.eshteRoliLidhurMePerdorues(idRoli);
            data.Dispose();
            return ekziston;
        }

        public static string merrKodeRoleshPerPerdoruesin(int idPerdoruesi)
        {
            using (var db = new clsDatabaseAdmin())
                return db.merrKodeRoleshPerPerdoruesin(idPerdoruesi).Trim(';');
        }

        public static bool KaPerdoruesiKeteRol(int IdPerdoruesi, string KodRoli)
        {
            return merrKodeRoleshPerPerdoruesin(IdPerdoruesi).Split(';').Contains(KodRoli);
        }
        #endregion

        #region Metoda Private


        #endregion
    }
}
