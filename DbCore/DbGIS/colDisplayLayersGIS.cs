using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace DbCore.DbGIS
{
    public class colDisplayLayersGIS : List<clsDisplayLayersGIS>
    {
        #region Konstruktori
        public colDisplayLayersGIS()
        {
        }
        public colDisplayLayersGIS(IEnumerable<clsDisplayLayersGIS> collection)
            : base(collection)
        {
        }

        #endregion

        #region Metoda Publike
        public new clsDisplayLayersGIS this[int index]
        {
            get
            {
                return ((clsDisplayLayersGIS)base[index]);
            }
        }
        public bool shtoLayer(clsDisplayLayersGIS layer)
        {
            this.Add(layer);
            if (base.Contains(layer))
                return true;
            else return false;
        }
        public bool ekzistonLayer(clsDisplayLayersGIS layer)
        {
            if (base.Contains(layer))
                return true;
            else return false;
        }
        public bool merrDisplayLayersGIS(int idPerdorues, int gjuha, int idNdermarrje, int idViti)
        {
            using (clsDatabaseGIS db = new clsDatabaseGIS())
            {
                DbCore.DbGIS.colLayersTrupiGIS tempTrupi = new DbCore.DbGIS.colLayersTrupiGIS();
                tempTrupi.merrLayersTrupiGIS(idNdermarrje, gjuha);

                DbCore.DbGIS.colLayersColsGIS tempKolonat = new DbCore.DbGIS.colLayersColsGIS();
                tempKolonat.merrTeGjitheKolonat(idNdermarrje, gjuha);

                return mbushDisplayLayersMeTrashigim(db.mbushDisplayLayersGIS(idPerdorues, gjuha, idNdermarrje, idViti), tempTrupi, tempKolonat);
            }
        }


        /// <summary>
        /// Merr Trupin e Grides ne dritaren e kerkimit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idLayeri"></param>
        /// <param name="kodLayeri"></param>
        /// <param name="kolonaGride"></param>
        /// <param name="filter"></param>
        /// <param name="gjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="grupo"></param>
        /// <returns></returns>
        public static DataTable MerrTeDhenaPerGridenEKerkimit(int idNdermarrje, int idnderviti, int idLayeri, string kodLayeri, string kolonaGride, string filter, int gjuha, int idPerdoruesi, bool grupo)
        {

            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                return dbGIS.merrTrupinEGrides(idNdermarrje, idnderviti, idLayeri, kodLayeri, kolonaGride, filter, gjuha, idPerdoruesi, grupo);
            }
        }

        /// <summary>
        /// Merr elementet ne dritaren e informacionit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idLayeri"></param>
        /// <param name="kolonaGride"></param>
        /// <param name="gid"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="gjuha"></param>
        /// <returns></returns>
        public static DataTable MerrTeDhenaPerInfon(int idNdermarrje, int idnderviti, int idLayeri, string kolonaGride, int gid, int idPerdoruesi, int gjuha)
        {
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                return dbGIS.merrTeDhenaPerInfo(idNdermarrje, idnderviti, idLayeri, kolonaGride, gid, idPerdoruesi, gjuha);
            }
        }

        /// <summary>
        /// Merr fushat qe do te shfaqen ne dritaren e informacionit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idLayeri"></param>
        /// <param name="Visible"></param>
        /// <param name="gjuha"></param>
        /// <returns></returns>
        public static string MerrFushaLayeri(int idNdermarrje, int idnderviti, int idLayeri, bool Visible, int gjuha)
        {
            string kolonatVisible = "";
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                DataTable dt = dbGIS.merrFushaLayeri(idNdermarrje, idnderviti, idLayeri, gjuha);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["VISIBLE"]))
                    {
                        if (string.IsNullOrEmpty(kolonatVisible))
                        {
                            kolonatVisible = string.Format("[{0}]", dt.Rows[i]["FUSHA"].ToString());
                            continue;
                        }

                        kolonatVisible = string.Format("{0},[{1}]", kolonatVisible, dt.Rows[i]["FUSHA"]);
                    }
                }
                return kolonatVisible;
            }
        }

        /// <summary>
        /// Merr fushat per griden e kerkimit
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idLayeriOsIdLayerType"></param>
        /// <param name="kodiLayeri"></param>
        /// <param name="idGjuha"></param>
        /// <param name="grupo"></param>
        /// <returns></returns>
        public static string[] MerrFushaLayeri(int idNdermarrje, int idnderviti, int idLayeriOsIdLayerType, string kodiLayeri, int idGjuha, bool grupo)
        {
            string kolonatVisible = "";
            string kolonaJoVisible = "";
            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                DataTable dt = dbGIS.merrFushaLayeriSipasKoditDheIdTipiOseIdLayeri(idNdermarrje, idnderviti, idLayeriOsIdLayerType, kodiLayeri, idGjuha, grupo);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[i]["VISIBLE"]))
                    {
                        if (string.IsNullOrEmpty(kolonatVisible))
                        {
                            kolonatVisible = string.Format("[{0}]", dt.Rows[i]["FUSHA"].ToString());
                            continue;
                        }

                        kolonatVisible = string.Format("{0},[{1}]", kolonatVisible, dt.Rows[i]["FUSHA"]);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(kolonaJoVisible))
                        {
                            kolonaJoVisible = string.Format("[{0}]", dt.Rows[i]["FUSHA"].ToString());
                            continue;
                        }
                        kolonaJoVisible = string.Format("{0},[{1}]", kolonaJoVisible, dt.Rows[i]["FUSHA"]);
                    }
                }
                return new string[] { kolonatVisible, kolonaJoVisible };
            }
        }
        #endregion

        #region Metoda Interial

        private bool mbushDisplayLayersMeTrashigim(DataTable dt, colLayersTrupiGIS tempTrupi, colLayersColsGIS tempKolonat)
        {
            int idLayer;
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    int.TryParse(dt.Rows[i]["IDLAYER"].ToString(), out idLayer);
                    List<clsLayersTrupiGIS> findListTrupi = tempTrupi.FindAll(x => x.IDLAYER == idLayer);
                    colLayersTrupiGIS tempcolLayersTrupiGIS = new colLayersTrupiGIS(findListTrupi);

                    List<clsLayersColsGIS> findListKolonat = tempKolonat.FindAll(x => x.IDLAYER == idLayer);
                    colLayersColsGIS tempcolLayersKolonaGIS = new colLayersColsGIS(findListKolonat);

                    Add(new clsDisplayLayersGIS(dt.Rows[i], tempcolLayersTrupiGIS, tempcolLayersKolonaGIS));
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool mbushDisplayLayersPaTrup(DataTable dt)
        {
            try
            {
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    Add(new clsDisplayLayersGIS(dt.Rows[i]));
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
