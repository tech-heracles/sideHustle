using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne skemat e azhornimit
    ///  (Te dhenat merren nga tabela :T_SKEMAAZHORNIMI)
    /// </summary> 
    public class clsSkemaAzhornimi
    {
        #region Atribute

        private int idSkemaAzhornimi;
        private int idGrupiLlogaria;
        private int idNengrupiLlogaria;
        private int menyraAzhornimit;
        private int idLlogariFitim;
        private int idLlogariHumbje;
        private int idNdermarje;
        private int idNderViti;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsSkemaAzhornimi(int id, int idgr, int idnengr, int men, int fitim, int humbje, int idnder, int idnderviti)
        {
            idSkemaAzhornimi = id;
            idGrupiLlogaria = idgr;
            idNengrupiLlogaria = idnengr;
            menyraAzhornimit = men;
            idLlogariFitim = fitim;
            idLlogariHumbje = humbje;
            idNdermarje = idnder;
            idNderViti = idnderviti;
        }

        /// <summary>
        /// konstruktor me 1 parameter
        /// </summary>
        /// <param name="id">id e kokes se skemes se azhornimit</param>
        public clsSkemaAzhornimi(int id)
        {
            clsDatabaseRegjistrim dbSkemaAzhornimi = new clsDatabaseRegjistrim();
            mbushSkemaAzhornimi(dbSkemaAzhornimi.merrSkemeAzhornimi(id));
            dbSkemaAzhornimi.Dispose();
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsSkemaAzhornimi()
        { 
        }

        public clsSkemaAzhornimi(DataRow rreshti)
        {
            
            mbushSkemaAzhornimi(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdSkemaAzhornimi
        {
            get { return idSkemaAzhornimi; }
            set { idSkemaAzhornimi = value; }
        }
        
        /// <summary>
        /// Kthen/Vendos ID-ne grupit te llogarise qe i eshte caktuar skemes se azhornimit
        /// </summary>
        public int IdGrupiLlogaria
        {
            get { return idGrupiLlogaria; }
            set { idGrupiLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne nengrupit te llogarise qe i eshte caktuar skemes se azhornimit
        /// </summary>
        public int IdNengrupiLlogaria
        {
            get { return idNengrupiLlogaria; }
            set { idNengrupiLlogaria = value; }
        }

        /// <summary>
        /// Kthen/Vendos menyren e azhornimit qe i eshte caktuar skemes se azhornimit
        /// </summary>
        public int MenyraAzhornimit
        {
            get { return menyraAzhornimit; }
            set { menyraAzhornimit = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise fitim qe i eshte caktuar skemes se azhornimit
        /// </summary>
        public int IdLlogariFitim
        {
            get { return idLlogariFitim; }
            set { idLlogariFitim = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e llogarise humbje qe i eshte caktuar skemes se azhornimit
        /// </summary>
        public int IdLlogariHumbje
        {
            get { return idLlogariHumbje; }
            set { idLlogariHumbje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e ndermarrjes
        /// </summary>
        public int IdNdermarje
        {
            get { return idNdermarje; }
            set { idNdermarje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne lidhese ndermarrje - vit
        /// </summary>
        public int IdNderViti
        {
            get { return idNderViti; }
            set { idNderViti = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e skemes se azhornimit ne databaze.
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajSkemeAzhornimi"/>
        /// </summary>
        public clsMesazh ruaj()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            int idSkem;
            clsMesazh u_ruajt = data.ruajSkemeAzhornimi(out idSkem, this.IdGrupiLlogaria, this.IdNengrupiLlogaria, this.MenyraAzhornimit, this.IdLlogariFitim, this.IdLlogariHumbje, this.IdNdermarje, this.IdNderViti);
            this.IdSkemaAzhornimi = idSkem;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e skemes se azhornimit ne databaze.
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoSkemeAzhornimi"/>
        /// </summary>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoSkemeAzhornimi(this.IdSkemaAzhornimi, this.IdGrupiLlogaria, this.IdNengrupiLlogaria, this.MenyraAzhornimit, this.IdLlogariFitim, this.IdLlogariHumbje, this.IdNdermarje, this.IdNderViti);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e skemes se azhornimit ne databaze.
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiSkemeAzhornimi"/>
        /// </summary>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiSkemeAzhornimi(this.IdSkemaAzhornimi);
            data.Dispose();
            return u_fshi;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush skemat e azhornimit nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaAzhornimi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushSkemaAzhornimi(DataRow dbDataRowSkemaAzhornimi)
        {
            if (dbDataRowSkemaAzhornimi != null)
            {
                try
                {
                    int.TryParse(dbDataRowSkemaAzhornimi["IDSKEMAAZHORNIMI"].ToString(), out idSkemaAzhornimi);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDGRUPILLOGARIA"].ToString(), out idGrupiLlogaria);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDNENGRUPILLOGARIA"].ToString(), out idNengrupiLlogaria);
                    int.TryParse(dbDataRowSkemaAzhornimi["MENYRAAZHORNIMIT"].ToString(), out menyraAzhornimit);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDLLOGARIFITIM"].ToString(), out idLlogariFitim);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDLLOGARIHUMBJE"].ToString(), out idLlogariHumbje);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDNDERMARJE"].ToString(), out idNdermarje);
                    int.TryParse(dbDataRowSkemaAzhornimi["IDNDERVITI"].ToString(), out idNderViti);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se skemave te azhornimit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion

    }
}
