using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbAdmin
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne te dhenat e  grupikf per skema workflow
    ///  (Te dhenat  merren nga tabela : T_GRUPKFPERWORKFLOW)
    /// </remarks>
    public class clsGrupeKFPerWorkFlow
    {
         #region Atributet

        private int id;
        private int idTrupi;
        private int idGrupKf;
        private DataRow rreshti;

        
        #endregion 

        #region Konstruktoret
        
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        /// <param name="id">Id qe gjenerohet automatikisht</param>
        /// <param name="idtrupi">Id e trupi</param>
        /// <param name="idgrupi">id e grupit </param>
       
        public clsGrupeKFPerWorkFlow(int id, int idtrupi,int idgrupi)
        {
            this.id = id;
            this.idTrupi = idtrupi;
            this.idGrupKf = idgrupi;
       
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        
        public clsGrupeKFPerWorkFlow(  int idtrupi,int idgrupi)
        {
            this.idTrupi = idtrupi;
            this.idGrupKf = idgrupi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsGrupeKFPerWorkFlow()
        { 
        }

        public clsGrupeKFPerWorkFlow(DataRow rreshti)
        {
            
            mbushGrupekfPerWorkFlow(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit
        /// </summary>
        public int IdTrupi
        {
            get { return idTrupi; }
            set { idTrupi = value; }
        }

        /// <summary>
        /// Kthen/Vendos id e grupit kf
        /// </summary>
        public int IdGrupKf
        {
            get
            {
                return idGrupKf;
            }
            set
            {
                idGrupKf = value;
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan ojektin e kontaktit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.ruajKontakt"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public bool ruaj()
        {
            clsDatabaseAdmin data = new  clsDatabaseAdmin ();
            int id;
            clsMesazh u_ruajt = data.ruajGrupeKfperWorkflow(out id,idTrupi, this.idGrupKf);
            data.Dispose();
            return u_ruajt.Status;
        }

        /// <summary>
        /// Modifikon ojektin e kontaktit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoKontakt"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public bool modifiko()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_modifikua = data.modifikoGrupeKfperWorkflow(id, idTrupi, this.idGrupKf);
            data.Dispose();
            return u_modifikua.Status;
        }

        /// <summary>
        /// Fshin ojektin e kontaktit ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.modifikoKontakt"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public bool fshi()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            clsMesazh u_fshi = data.fshiGrupeKfperWorkflow(this.id);
            data.Dispose();
            return u_fshi.Status;
        }

        /// <summary>
        /// Merr nje objekt kontakti. Therret funksionin
        /// <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrKontakt"/>
        /// </summary>
        public void merr()
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            data.merrGrupeKfperWorkFlow(this.Id);
            data.Dispose();
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kontaktet nga databaza
        /// </summary>
        /// <param name="dbDataRowKontKlieFurn">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushGrupekfPerWorkFlow(DataRow dbDataRowKontKlieFurn)
        {
            if (dbDataRowKontKlieFurn != null)
            {
                try
                {
                    int.TryParse(dbDataRowKontKlieFurn["IDKONTAKTIKLIENTFURNITOR"].ToString(), out id);
                    int.TryParse(dbDataRowKontKlieFurn["IDTRUPISKEMAWORKFLOW"].ToString(), out idTrupi);
                    int.TryParse(dbDataRowKontKlieFurn["IDGRUPKF"].ToString(), out idGrupKf);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kontakteve nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
