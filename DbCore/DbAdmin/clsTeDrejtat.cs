using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne te drejtat e nje perdoruesi
    ///  te caktuar.(Te dhenat  merren nga tabela : T_DREJTAT)
    /// </summary>
    public class clsTeDrejtat
    {
        #region Atributet

        private int idDrejta;
        private int idNderViti;
        private int idKomponente;
        private int idModul;
        private int idPerdorues;
        private int idDrejtaVeprim;
        private int idAmbjentiModuli;
        private String ambjentiModuli;
        private String komponente;
        private int perdoruesApoGrup;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtat(int iddrejta, int idndermarrjevit, int idkomponente, int idmodul, int idperdorues, int iddrejtaveprim, int idambjentimoduli, int perdoruesapogrup)
        {
            idDrejta = iddrejta;
            idNderViti = idndermarrjevit;
            idKomponente = idkomponente;
            idModul = idmodul;
            idPerdorues = idperdorues;
            idDrejtaVeprim = iddrejtaveprim;
            idAmbjentiModuli = idambjentimoduli;
            perdoruesApoGrup = perdoruesapogrup;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtat(int idndermarrjevit, int idkomponente, int idmodul, int idperdorues, int iddrejtaveprim, int idambjentimoduli, int perdoruesapogrup)
        {
            idNderViti = idndermarrjevit;
            idKomponente = idkomponente;
            idModul = idmodul;
            idPerdorues = idperdorues;
            idDrejtaVeprim = iddrejtaveprim;
            idAmbjentiModuli = idambjentimoduli;
            perdoruesApoGrup = perdoruesapogrup;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsTeDrejtat()
        {
        }

        public clsTeDrejtat(DataRow rreshti)
        {
            
            mbushTeDrejten(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdDrejta
        {
            get { return idDrejta; }
            set { idDrejta = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe lidh vitin me ndermarrjen
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes mbi te cilen do caktohen te drejtat
        /// </summary>
        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e modulit mbi te cilin do caktohen te drejtat
        /// </summary>
        public int IdModul
        {
            get { return idModul; }
            set { idModul = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e perdoruesit qe ka po vendos te drejtat
        /// </summary>
        public int IdPerdorues
        {
            get { return idPerdorues; }
            set { idPerdorues = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e veprimit (lexim, shkrim, fshirje etj) sipas te cilit do aplikohet e drejta
        /// </summary>
        public int IdDrejtaVeprim
        {
            get { return idDrejtaVeprim; }
            set { idDrejtaVeprim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ambjentit mbi te cilin do caktohen te drejtat
        /// </summary>
        public int IdAmbjentiModuli
        {
            get { return idAmbjentiModuli; }
            set { idAmbjentiModuli = value; }
        }

        /// <summary>
        /// Kthen emrin e ambjentit mbi te cilin do caktohen te drejtat
        /// </summary>
        public String AmbjentiModuli
        {
            get { return ambjentiModuli; }
            set { ambjentiModuli = value; }
        }

        /// <summary>
        /// Kthen emrin e komponentes mbi te cilin do caktohen te drejtat
        /// </summary>
        public String Komponente
        {
            get { return komponente; }
            set { komponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos numrin qe percakton nese te drejtat po caktohen per perdorues(0) apo grup(1)
        /// </summary>
        public int PerdoruesApoGrup
        {
            get { return perdoruesApoGrup; }
            set { perdoruesApoGrup = value; }
        }

        #endregion

        #region Metoda Publike
                

        #endregion

        #region Metoda Internal

        internal bool mbushTeDrejten(DataRow dbDataRowTeDrejten)
        {
            if (dbDataRowTeDrejten != null)
            {
                try
                {
                    int.TryParse(dbDataRowTeDrejten["IDDREJTA"].ToString(), out idDrejta);
                    int.TryParse(dbDataRowTeDrejten["IDNDERVITI"].ToString(), out idNderViti);
                    int.TryParse(dbDataRowTeDrejten["IDKOMPON"].ToString(), out idKomponente);
                    int.TryParse(dbDataRowTeDrejten["IDMODUL"].ToString(), out idModul);
                    int.TryParse(dbDataRowTeDrejten["IDPERDORUES"].ToString(), out idPerdorues);
                    int.TryParse(dbDataRowTeDrejten["IDDREJTAVEPRIM"].ToString(), out idDrejtaVeprim);
                    int.TryParse(dbDataRowTeDrejten["IDAMBJMODULI"].ToString(), out idAmbjentiModuli);
                    int.TryParse(dbDataRowTeDrejten["PERDORUESAPOGRUP"].ToString(), out perdoruesApoGrup);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se te drejtave nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
