using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne nje fushe te caktuar e cila do te 
    ///  auditohet.(Te dhenat  merren nga tabela : T_GRUPAUDITIMITRUPI)
    /// </summary>
    public class clsGrupAuditimiTrupi
    {
        #region Atribute

        private int idGrupi;
        private int idTabele;
        private int idKolone;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsGrupAuditimiTrupi(int idgrupi, int idtabele, int idkolone)
        {
            idGrupi = idgrupi;
            idTabele = idtabele;
            idKolone = idkolone;

        }

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsGrupAuditimiTrupi()
        {
        }

        public clsGrupAuditimiTrupi(DataRow rreshti)
        {
            
            mbushGrupAuditimTrup(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne e grupit te cilit i perket kjo fushe qe do auditohet.
        /// </summary>
        public int IdGrupi
        {
            get { return idGrupi; }
            set { idGrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e tabeles se ciles i perket kjo fushe qe do auditohet.
        /// </summary>
        public int IdTabele
        {
            get { return idTabele; }
            set { idTabele = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e kolone qe perfaqson kjo fushe qe do auditohet.
        /// </summary>
        public int IdKolone
        {
            get { return idKolone; }
            set { idKolone = value; }
        }

        //public bool ruaj()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    bool u_ruajt = data.ruajGrupAuditimi(this);
        //    return u_ruajt;
        //}

        //public bool modifiko()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    bool u_modifikua = data.modifikoGrupAuditimi(this);
        //    return u_modifikua;
        //}

        //public bool fshi()
        //{
        //    clsDatabaseKontabilitet data = new clsDatabaseKontabilitet();
        //    bool u_fshi = data.fshiGrupAuditimi(this);
        //    return u_fshi;
        //}

        #endregion

        #region Metoda Internal

        internal bool mbushGrupAuditimTrup(DataRow dbDataRowGrupAuditimTrup)
        {
            if (dbDataRowGrupAuditimTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowGrupAuditimTrup["IDGRUPAUDIT"].ToString(), out idGrupi);
                    int.TryParse(dbDataRowGrupAuditimTrup["IDTABELE"].ToString(), out idTabele);
                    int.TryParse(dbDataRowGrupAuditimTrup["IDKOLONE"].ToString(), out idKolone);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te grupit te auditimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}