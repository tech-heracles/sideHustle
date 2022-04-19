using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbInventari
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e makros
    ///  (Te dhenat  merren nga tabela : T_TRUPIMAGAZINA)
    /// </summary>
    public class clsTrupiMakro
    { 

        #region Atribute

        private int idTrupiMakro;
        private int idKokaMakro;
        private int idLlojMakro;
        private int idProdukti;
        private string pershkrimi;
        private int idFunksionMakro;
        private decimal vlera;
        private int renditja;
        private string kodProdukti;
        private DataRow rreshti;
      
        #endregion
       
        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdTrupiMakro {
            get
            {
                return idTrupiMakro;
            }
            set
            {
                idTrupiMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e kokes se makros
        /// </summary>
        public int  IdKokaMakro {
            get
            {
                return idKokaMakro;
            }
            set
            {
                idKokaMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e llojit te makros
        /// </summary>
        public int IdLlojMakro
        {
            get
            {
                return idLlojMakro;
            }
            set
            {
                idLlojMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e produktit
        /// </summary>
        public int  IdProdukti {
            get
            {
                return idProdukti  ;
            }
            set
            {
                idProdukti = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin 
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi ;
            }
            set
            {
                pershkrimi = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos ID-ne e funksionit te makros.
        /// </summary>
        public int IdFunksionMakro
        {
            get
            {
                return idFunksionMakro;
            }
            set
            {
                idFunksionMakro = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos vleften
        /// </summary>
        public decimal Vlera
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
        /// <summary>
        /// Kthen/Vendos renditjen
        /// </summary>
        public int  Renditja {
            get
            {
                return renditja;
            }
            set
            {
                this.renditja = value;
            }

        }
        /// <summary>
        /// Kthen/Vendos kodin e produktit
        /// </summary>
        public string KodProdukti
        {
            get
            {
                return kodProdukti ;
            }
            set
            {
                kodProdukti = value;
            }
        }
    
        #endregion

        #region Kontruktoret

        /// <summary>
        /// konstruktori me parametra
        /// </summary>
        /// <param name="idTrupiMakro"> id ritese e trupit te makros</param>
        /// <param name="idKokaMakro"> id e kokes se makros</param>
        /// <param name="idLlojMakro"> id e lloji te makros</param>
        /// <param name="idProdukti"> id e produktit</param>
        /// <param name="pershkrimi"> pershkrimi</param>
        /// <param name="idFunksionMakro"> id e funksionit te makros</param>
        /// <param name="vlera"> vlera</param>
        /// <param name="renditja"> renditja</param>
        public clsTrupiMakro(int idTrupiMakro, int idKokaMakro, int idLlojMakro, int idProdukti, string pershkrimi, int idFunksionMakro, decimal vlera, int renditja) {
            this.idTrupiMakro = idTrupiMakro;
            this.idKokaMakro = idKokaMakro;
            this.idLlojMakro = idLlojMakro;
            this.idProdukti = idProdukti;
            this.pershkrimi = pershkrimi;
            this.idFunksionMakro = idFunksionMakro;
            this.vlera = vlera;
            this.renditja = renditja;
        }
        
        /// <summary>
        /// konstruktore me 1 parameter
        /// </summary>
        /// <param name="idtrupi">id e trupit</param>
        public clsTrupiMakro(int idtrupi)
        {
            clsDatabaseInventari dbTrupiMakro = new clsDatabaseInventari();
            mbushTrupiMakro(dbTrupiMakro.merrTrupiMakro(idtrupi));
            dbTrupiMakro.Dispose();
        }

        /// <summary>
        /// konstruktori pa parametra
        /// </summary>
        public clsTrupiMakro()
        {
        }

        public clsTrupiMakro(DataRow rreshti)
        {
            
            mbushTrupiMakro(rreshti);
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbushja e trupit te makros nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupiMakro">datarow qe duhet mbushur nga databaza</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        internal bool mbushTrupiMakro(DataRow dbDataRowTrupiMakro)
        {
            if (dbDataRowTrupiMakro != null)
            {

                try
                {

                    int.TryParse(dbDataRowTrupiMakro["IDTRUPIMAKRO"].ToString(), out idTrupiMakro);
                    int.TryParse(dbDataRowTrupiMakro["IDLLOJIMAKRO"].ToString(), out idLlojMakro);
                    int.TryParse(dbDataRowTrupiMakro["IDPRODUKTI"].ToString(), out idProdukti);
                    int.TryParse(dbDataRowTrupiMakro["IDFUNKSIONI"].ToString(), out idFunksionMakro);
                    decimal.TryParse(dbDataRowTrupiMakro["VLERA"].ToString(), out vlera);
                    int.TryParse(dbDataRowTrupiMakro["RENDITJA"].ToString(), out renditja);
                    int.TryParse(dbDataRowTrupiMakro["IDKOKAMAKRO"].ToString(), out idKokaMakro);
                    pershkrimi = dbDataRowTrupiMakro["PERSHKRIMI"].ToString();
                    if (idLlojMakro == 1)
                    {
                        kodProdukti = DbCore.DbInventari.clsArtikulli.ktheKodArtikulliSipasId(idProdukti);
                    }
                    else if (idLlojMakro == 2)
                        kodProdukti = new DbCore.DbInventari.clsKokaMakro(idProdukti).KodiKokaMakro;
                    else kodProdukti = "";

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te makros nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
