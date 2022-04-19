using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne drejtimin, rrites apo
    ///  zbrites, dhe perdoren tek konfigurimi i numrave automatike
    ///  (Te dhenat nuk merren nga ndonje tabele)
    /// </summary>
    /// <example>
    /// <code>
    ///   List<DbCore.DbAdmin.clsDrejtim> liste = new List<DbCore.DbAdmin.clsDrejtim>();
    ///DbCore.DbAdmin.clsDrejtim drejtimi = new DbCore.DbAdmin.clsDrejtim();
    ///drejtimi.IdDrejtimi = 0;
    ///drejtimi.DrejtimiPershkrimi = "Rrites";
    ///liste.Add(drejtimi);
    ///drejtimi = new DbCore.DbAdmin.clsDrejtim();
    ///drejtimi.IdDrejtimi = 1;
    ///drejtimi.DrejtimiPershkrimi = "Zbrites";
    ///liste.Add(drejtimi);
    /// </code>
    /// </example>
    public class clsDrejtim
    {
        // properties
        private int idDrejtimi;
        private String drejtimiPershkrimi;
        //konstruktoret
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsDrejtim(int iddrejtimi,  String drejtimipershkrimi)
        {
            idDrejtimi = iddrejtimi;
            drejtimiPershkrimi = drejtimipershkrimi;
        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsDrejtim()
        { 
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e objektit qe perfaqson drejtimin.
        /// </summary>
        public int IdDrejtimi
        {
            get { return idDrejtimi; }
            set { idDrejtimi = value; }
        }
        /// <summary>
        /// Kthen/Vendos Pershkrimin-ne e objektit qe perfaqson drejtimin.
        /// </summary>
        public string DrejtimiPershkrimi
        {
            get { return drejtimiPershkrimi; }
            set { drejtimiPershkrimi = value; }
        }

    }
}
