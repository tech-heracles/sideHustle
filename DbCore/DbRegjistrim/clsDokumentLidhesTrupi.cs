using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje element te trupit te lidhjes se dokumentave
    ///  (Te dhenat  merren nga tabela : T_DOKUMENTLIDHESTRUPI)
    /// </remarks>
    public class clsDokumentLidhesTrupi
    {
        #region Atribute

        private int idTrupi;
        private int idKoka;
        private int idDokumenti;
        private String llojDokumenti;
        private String statusi;
        private double vleraLidhjes;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsDokumentLidhesTrupi(int idtrupi, int idkoka, int iddokumenti,
                                    String lloddokumenti, String stat, double vleralidhjes)
        {
                idTrupi = idtrupi;
                idKoka = idkoka;
                idDokumenti = iddokumenti;
                llojDokumenti = lloddokumenti;
                statusi = stat;
                vleraLidhjes = vleralidhjes;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsDokumentLidhesTrupi()
        { 
        }

        public clsDokumentLidhesTrupi(DataRow rreshti)
        {
            
            mbushDokumentLidhesTrupi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e kokes te ciles i perket trupi
        /// </summary>
        public int IdKoka
        {
            get { return idKoka; }
            set { idKoka = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi= value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e dokumentit
        /// </summary>
        public int IdDokumenti
        {
            get { return idDokumenti; }
            set { idDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos llojin e dokumentit
        /// </summary>
        public String LlojDokumenti
        {
            get { return llojDokumenti; }
            set { llojDokumenti = value; }
        }

        /// <summary>
        /// Kthen/Vendos statusin 0 = Dokument kryesor, 1= Dokument lidhes
        /// </summary>
        public String Statusi
        {
            get { return statusi; }
            set { statusi = value; }
        }

        /// <summary>
        /// Kthen/Vendos vleren me te cilen behet lidhja
        /// </summary>
        public double VleraLidhjes
        {
            get { return vleraLidhjes; }
            set { vleraLidhjes = value; }
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e dokumentit lidhes nga databaza
        /// </summary>
        /// <param name="dbDataRowDokumentLidhesTrupi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushDokumentLidhesTrupi(DataRow dbDataRowDokumentLidhesTrupi)
        {
            if (dbDataRowDokumentLidhesTrupi != null)
            {
                try
                {
                    int.TryParse(dbDataRowDokumentLidhesTrupi["IDTRUPI"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowDokumentLidhesTrupi["IDKOKA"].ToString(), out idKoka);
                    int.TryParse(dbDataRowDokumentLidhesTrupi["IDDOKUMENTI"].ToString(), out idDokumenti);
                    llojDokumenti = dbDataRowDokumentLidhesTrupi["LLOJDOKUMENTI"].ToString();
                    statusi = dbDataRowDokumentLidhesTrupi["STATUSI"].ToString();
                 VleraLidhjes= Convert.ToDouble(dbDataRowDokumentLidhesTrupi["VLERALIDHUR"]);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te dokumentit lidhes nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}

