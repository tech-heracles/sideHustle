using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbAdmin
{


        /// <remarks>
        /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje ambjent e hapjes shitje blerje arka
        /// </remarks>
        public class clsAmbjent
        {
            #region Atribute

            private int id;
            private string pershkrimi;
            private int idKomponente;
            private DataRow rreshti;

            #endregion

            #region Konstruktoret

            /// <summary>
            /// Konstruktor i klases
            /// </summary>
            public clsAmbjent(int id, String pershkrim, int idkomp)
            {
                this.id = id;
                pershkrimi = pershkrim;
                idKomponente=idkomp;
            }

            /// <summary>
            /// Konstruktor i klases
            /// </summary>
            /// <param name="kodllojbuxheti"></param>
            public clsAmbjent(String pershkrim)
            {
                pershkrimi = pershkrim;
            }

            /// <summary>
            /// Konstruktor i klases
            /// </summary>
            public clsAmbjent()
            {
            }

            public clsAmbjent(DataRow rreshti)
            {
                mbushAmbjent(rreshti);
            }

            #endregion

            #region Properties

            public int IdKomponente
            {
                get
                {
                    return idKomponente;
                }
                set
                {
                    idKomponente = value;
                }
            }
            /// <summary>
            /// Kthen/Vendos ID-ne qe gjenerohet automatikisht
            /// </summary>
            public int Id
            {
                get { return id; }
                set { id = value; }
            }

            /// <summary>
            /// Kthen/Vendos pershkrimin e panelit
            /// </summary>
            public String Pershkrimi
            {
                get { return pershkrimi; }
                set { pershkrimi = value; }
            }

            #endregion

            #region Metoda Publike


             ///<summary>
            /// Kthen/Vendos nje objekt lloj buxheti. Therret funksionin <see cref="DbCore.DbKontabiliteti.clsDatabaseKontabilitet.merrLlojBuxheti"/>
            /// </summary>
            public void merr(int id)
            {
                clsDatabaseAdmin data = new clsDatabaseAdmin();
                data.merrAmbjente(id);
                data.Dispose();
            }

            #endregion

            #region Metoda Internal

            /// <summary>
            /// mbush llojet e buxhetit nga databaza
            /// </summary>
            /// <param name="dbDataRowLlogari">datarow qe duhet mbushur nga db</param>
            /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
            internal bool mbushAmbjent(DataRow dbDataRow)
            {
                if (dbDataRow != null)
                {
                    try
                    {

                        int.TryParse(dbDataRow["ID"].ToString(), out id);
                        pershkrimi = dbDataRow["PERSHKRIMI"].ToString();
                           int.TryParse(dbDataRow["IDKOMPONENTE"].ToString(), out idKomponente);
                        return true;
                    }
                    catch (InvalidCastException)
                    {
                        throw new Exception("ERROR: Gabim gjate marrjes se ambjentit nga db-ja");
                    }
                }
                else
                    return false;
            }

            #endregion

        }
    }


