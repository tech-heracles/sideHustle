using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne Auditimin e nje klone
    ///  te caktuar per nje tabele te caktuar
    ///  (Te dhenat merren nga tabela :T_AUDITIMI)
    /// </summary> 
    public class clsAuditim
    {
        #region Attributes
        /// <summary>
        /// 
        /// </summary>
        private int idAuditim;
        private int idTabele;
        private int idKolone;
        private int idNderViti;
        private String emerrealkolone;
        private int idPerdoruesi;
        private DataRow dr;
        #endregion

        #region Properties
        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdAuditim
        {
            get { return idAuditim; }
            set { idAuditim = value; }
        }

        /// <summary>
        /// Kthen ID-ne e NdermarrjeVitit per te cilin eshte ruajtur ky konfigurim Auditimi. Pa konfigurimi mund 
        /// te behet ne nivel ndermarrjeje dhe viti.
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        /// <summary>
        /// Kthen ID-ne e tabeles se ciles i perket auditimi qe perfaqson ky objekt.
        /// </summary>
        public int IdTabele
        {
            get { return idTabele; }
            set { idTabele = value; }
        }

        /// <summary>
        /// Kthen ID-ne e kolones se  ciles i perket auditimi qe perfaqson ky objekt.
        /// </summary>
        public int IdKolone
        {
            get { return idKolone; }
            set { idKolone = value; }
        }
        /// <summary>
        /// Kthen ID-ne e perdoruesit qe ka krijuar kete auditim
        /// </summary>
        public int IdPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public String Emerrealkolone
        {
            get
            {
                return emerrealkolone;
            }
            set
            {
                if (emerrealkolone == value)
                    return;
                emerrealkolone = value;
            }
        }
        #endregion

        #region Kontruktoret

        /// <summary>
        /// Konstruktori default i klases
        /// </summary>
        public clsAuditim()
        {
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        //konstruktoret
        public clsAuditim(int idauditim, int idgrupi, int idtabele, int idkolone, int idndermarrjevit, int idperdoruesi)
        {
            idAuditim = idauditim;
            idTabele = idtabele;
            idKolone = idkolone;
            idNderViti = idndermarrjevit;
            idPerdoruesi = idperdoruesi;

        }
        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsAuditim(int idgrupi, int idtabele, int idkolone, int idndermarrjevit, int idperdoruesi)
        {
            idTabele = idtabele;
            idKolone = idkolone;
            idNderViti = idndermarrjevit;
            idPerdoruesi = idperdoruesi;
        }

        public clsAuditim(DataRow dr)
        {
            
            mbushAuditim(dr);
        }
        #endregion

        #region Metoda Publike
        /// <summary>
        /// Ruan objektin e Auditimit ne databaze.
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_ruajt = data.ruajAuditim(this.IdAuditim, this.IdTabele, this.IdKolone, this.IdNderViti, this.IdPerdoruesi);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Fshin objektin e Auditimit ne databaze.
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiAuditim(this.IdTabele, this.IdKolone);
            data.Dispose();
            return u_fshi;
        }
        #endregion

        #region Metoda Internal

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        internal bool mbushAuditim(DataRow rreshti)
        {
            if (rreshti != null)
            {
                try
                {
                    int.TryParse(rreshti["IDAUDIT"].ToString(), out idAuditim);
                    int.TryParse(rreshti["IDTABELE"].ToString(), out idTabele);
                    int.TryParse(rreshti["IDKOLONE"].ToString(), out idKolone);
                    int.TryParse(rreshti["IDNDERVITI"].ToString(), out idNderViti);
                    emerrealkolone = rreshti["EMERREALKOLONE"].ToString();
                    int.TryParse(rreshti["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se auditimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}