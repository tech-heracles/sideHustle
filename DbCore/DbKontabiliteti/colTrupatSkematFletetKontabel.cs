using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data.SqlClient;
using System.Data;


namespace DbCore.DbKontabiliteti
{
        /// <summary>
        ///  Kjo eshte klasa qe mban nje liste me objekte te tipit clsTrupiSkemaFleteKontabel
        ///  dhe lejon te manipulohet kjo liste nepermjet indekseve
        ///  (Te dhenat nuk merren nga ndonje tabele)
        /// </summary>
        public class colTrupatSkematFletetKontabel : System.Collections.Generic.List<clsTrupiSkemaFleteKontabel>
        {
            #region Konstruktoret

            /// <summary>
            /// konstruktore pa parametra
            /// </summary>
            public colTrupatSkematFletetKontabel()
            {
            }

            /// <summary>
            /// konstruktor me 1 parameter
            /// </summary>
            /// <param name="idKoka">id e kokes se skemes</param>
            public colTrupatSkematFletetKontabel(int idKoka)
            {
                clsDatabaseKontabilitet dbTrupSkemFletKont = new clsDatabaseKontabilitet();
                mbushTrupSkemaFK(dbTrupSkemFletKont.ktheTrupatSkematFletetKontabelSipasKokes(idKoka));
                dbTrupSkemFletKont.Dispose();
            }

            #endregion

            #region Metoda publike

            /// <summary>
            /// kthen objektin <see cref="DbCore.DbKontabiliteti.clsTrupiSkemaFleteKontabel"/>  qe ndodhet ne nje index te caktuar te arraylist-es
            /// </summary>
            public new clsTrupiSkemaFleteKontabel this[int index]
            {
                get { return ((clsTrupiSkemaFleteKontabel)base[index]); }
            }

            #endregion

            #region Metoda private

            /// <summary>
            /// metoda perdoret gjeresisht ne rastet kur marrim te dhenat e kthyera ne ndonje store procedure ne formatin e nje datatable
            ///  <see cref="DbCore.DbKontabiliteti.clsTrupiSkemaFleteKontabel"/> 
            /// </summary>
            private bool mbushTrupSkemaFK(DataTable dt)
            {
                //try
                //{

                    foreach (DataRow rreshti in dt.Rows)
                    {
                        //clsTrupiSkemaFleteKontabel trupiSkemaFK = new clsTrupiSkemaFleteKontabel();
                        //trupiSkemaFK.mbushTrupFletSkemKont(rreshti);
                        Add(new clsTrupiSkemaFleteKontabel(rreshti));
                    }

                //}
                //catch (Exception)
                //{
                //    return false;
                //    //throw;
                //}
                return true;
            }

            #endregion
            [Obsolete("Perdor: bool mbushTrupSkemaFK(DataTable dt)", true)]
            public colTrupatSkematFletetKontabel mbushArrayListTrupiSkemaFKsh(DataSet ds)
            {
                colTrupatSkematFletetKontabel trupatSkemaFK = new colTrupatSkematFletetKontabel();
                foreach (DataRow rreshti in ds.Tables[0].Rows)
                {
                    clsTrupiSkemaFleteKontabel trupiSkemaFK = new clsTrupiSkemaFleteKontabel();

                    trupiSkemaFK.IdTrupiSkemaFK = int.Parse(rreshti[0].ToString());
                    trupiSkemaFK.IdKokaSkemaFK = int.Parse (rreshti[1].ToString());
                   
                
                    trupiSkemaFK.IdLlogari = int.Parse(rreshti[3].ToString());
                   
                    trupatSkemaFK.Add(trupiSkemaFK);
                }
                return trupatSkemaFK;
            }
        }
}