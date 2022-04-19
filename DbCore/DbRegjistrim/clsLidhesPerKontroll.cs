using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne lidhjen e nivelit me kontrollet per tabelen
    ///  (Te dhenat  merren nga tabela : T_LIDHESPERKONTROLL)
    /// </remarks>
    public class clsLidhesPerKontroll
    {
        #region Atributet

        private int id;
        private int idNivel;
        private int idTabPerKontroll;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="idNivel">id niveli</param>
        /// <param name="idTabPerKontroll">id e tabeles per kontroll</param>
        public clsLidhesPerKontroll(int id, int idNivel, int idTabPerKontroll)
        {
            this.id = id;
            this.idNivel = idNivel;
            this.idTabPerKontroll = idTabPerKontroll;
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsLidhesPerKontroll()
        {
        }

        public clsLidhesPerKontroll(DataRow rreshti)
        {
            
            mbushLidhes(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos Id-ne qe gjenerohet automatikisht
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos kodin
        /// </summary>
        public int IdNivel
        {
            get
            {
                return idNivel;
            }
            set { idNivel = value; }
        }

        /// <summary>
        /// Kthen /vendos id e tabeles per kontroll
        /// </summary>
        public int IdTabPerKontroll
        {
            get { return idTabPerKontroll; }
            set
            {
                idTabPerKontroll = value;
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan lidhjen e nivelit me tabelen per kontroll
        /// <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajLidhesPerKontroll"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {//metoda qe therret klasen clsDatabaseRegjistrim per ruajtjen e nje fletekontabel
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int id;
            clsMesazh u_ruajt = data.ruajLidhesPerKontroll(out id, this.IdNivel, this.IdTabPerKontroll);
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon lidhjen e nivelit me tabelen per kontroll
        /// <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoLidhesPerKontroll"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public clsMesazh modifiko()
        {//metoda qe therret klasen clsDatabaseKontabilitet per modifikimin e nje fletekontabel
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoLidhesPerKontroll(this.Id, this.IdNivel, this.IdTabPerKontroll);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin lidhjen e nivelit me tabelen per kontroll
        /// <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiLidhesPerKontroll"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public clsMesazh fshi()
        {//metoda qe therret klasen clsDatabaseKontabilitet per fshirjen e nje fletekontabel
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiLidhesPerKontroll(this.Id);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush lidhes per kontroll nga databaza
        /// </summary>
        /// <param name="dbDataRowLidhes">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushLidhes(DataRow dbDataRowLidhes)
        {
            if (dbDataRowLidhes != null)
            {
                try
                {
                    int.TryParse(dbDataRowLidhes["ID"].ToString(), out id);
                    int.TryParse(dbDataRowLidhes["IDNIVEL"].ToString(), out idNivel);
                    int.TryParse(dbDataRowLidhes["IDTABPERKONTROLL"].ToString(), out idTabPerKontroll);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se lidhes per kontroll nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
