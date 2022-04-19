using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

using System.Collections;
using System.Web.Script.Serialization;

using DbCore.DbGIS;

namespace DbCore.DbGIS
{
    public class Perdorues : System.Web.UI.Page
    {
        private DbAccessLayer.IDBManager dbManager;

        private int idPerdorues;
        private int idRol;
              
        public Perdorues(int idPerdoruesi,int idRoli)
        {
            idPerdorues = idPerdoruesi;
            idRol = idRoli;
        }

        //zevendesuar me merrLayersSipasVeprimit
        public Layers merrLayeratAfishuar(int gjuha, int idNdermarrje)
        {

            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from  F_GIS_merrLayersSipasRolitDheGjuhes(@id_rol,@gjuha,@IDNDERMARRJE)", con);
            Layers layeratAfishuar;
            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN); 
            layeratAfishuar = new Layers();
            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        LayerElement l = new LayerElement(Convert.ToInt32(rdr["id_layer"]), rdr["layer_name"].ToString(), rdr["layer_url"].ToString(), rdr["format"].ToString(), float.Parse(rdr["tile_size"].ToString()), Convert.ToInt32(rdr["id_workspace"]), rdr["title"].ToString(),
                           Convert.ToInt32(rdr["id_folder"]), rdr.GetBoolean(rdr.GetOrdinal("transparent")), rdr.GetBoolean(rdr.GetOrdinal("tiled")), rdr.GetBoolean(rdr.GetOrdinal("is_base_layer")), rdr.GetBoolean(rdr.GetOrdinal("visibility")), rdr.GetBoolean(rdr.GetOrdinal("singletile")),
                         rdr["name"].ToString(), rdr["workspace_name"].ToString(), Convert.ToInt32(rdr["renditja_ne_app"]), float.Parse(rdr["opacity"].ToString()), rdr["url_metadata"].ToString(), rdr.GetBoolean(rdr.GetOrdinal("eshteNeLegjende")), rdr["prinderitName"].ToString());

                        layeratAfishuar.shtoElement(l);
                    }
                }

                return layeratAfishuar;
            }
            catch (SqlException)
            {
                return layeratAfishuar;
            }
            finally
            {
                con.Dispose();
                cmd.Dispose();
                con.Close();
            }
        }

        public Folders merrFolderatAfishuar(int gjuha, int idNdermarrje)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);

            SqlCommand cmd = new SqlCommand("select * from  F_GIS_merrFoldersSipasRolitDheGjuhes(@id_rol,@gjuha,@IDNDERMARRJE)", con);

            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN); 
            Folders folderatAfishuar= new Folders();

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {


                     /*  FolderElement f = new FolderElement(Convert.ToInt32(rdr["id_layer"]), rdr["layer_name"].ToString(), rdr["layer_url"].ToString(), rdr["format"].ToString(), float.Parse(rdr["tile_size"].ToString()), Convert.ToInt32(rdr["id_workspace"]), rdr["title"].ToString(),
                           Convert.ToInt32(rdr["id_folder"]), rdr.GetBoolean(rdr.GetOrdinal("transparent")), rdr.GetBoolean(rdr.GetOrdinal("tiled")), rdr.GetBoolean(rdr.GetOrdinal("is_base_layer")), rdr.GetBoolean(rdr.GetOrdinal("visibility")), rdr.GetBoolean(rdr.GetOrdinal("singletile")),
                         rdr["name"].ToString(), rdr["workspace_name"].ToString(), Convert.ToInt32(rdr["renditja_ne_app"]), float.Parse(rdr["opacity"].ToString()), rdr["url_metadata"].ToString(), rdr.GetBoolean(rdr.GetOrdinal("eshteNeLegjende")));

                        layeratAfishuar.shtoElement(l);*/

                        FolderElement f = new FolderElement(Convert.ToInt32(rdr["id_folder"]), rdr["folder_name"].ToString(), (float)Convert.ToDouble(rdr["renditja"]), Convert.ToInt32(rdr["expanded"]));
                        folderatAfishuar.shtoElement(f);
                    }
                }

                return folderatAfishuar;
            }
            catch (SqlException)
            {
                return folderatAfishuar;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }


        
        
        }

        public Layers MerrLayeratKerkim(int gjuha, int idNdermarrje)
        {
            //Ketu do te merren vetem Layerat per Kerkim, kur te shtohen te drejtat dhe konceptimi per cdo layer
            //colLayerGIS colLay = new colLayerGIS();
            //colLay.merrLayersSipasRolitDheGjuhes(idRol, gjuha, new clsDatabaseGIS());
            //colLayerGIS colLayNew = new colLayerGIS();
            //colLayNew.ktheLayeratGatiPerKerkim(colLay);
            //return colLayNew;

            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);
            SqlCommand cmd = new SqlCommand("select * from  F_GIS_merrLayersSipasRolitDheGjuhesPerKerkim(@id_rol,@gjuha,@IDNDERMARRJE)", con);
            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN); 
            Layers layeratKerkim = new Layers();

            string layerValueBosh = "{\"layer\":\"bosh\",\"url_servicewfs\":\"\",\"workspace_name\":\"\",\"layer_title\":\"\",\"url_servicewms\":\"\"}";
            LayerElement lbosh = new LayerElement(-1, "", layerValueBosh);
            layeratKerkim.shtoElement(lbosh);

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string url_servicewms = rdr["layer_url"].ToString() + "?&";
                        string url_servicewfs = url_servicewms.Replace("wms?", "wfs?");
                        string layerValues = "{\"layer\":\"" + rdr["name"].ToString() + "\",\"url_servicewfs\":\"" + url_servicewfs + "\",\"workspace_name\":\"" + rdr["workspace_name"].ToString() + "\",\"layer_name_app\":\"" + rdr["layer_name_app"].ToString() + "\",\"title\":\"" + rdr["title"].ToString() + "\",\"url_servicewms\":\"" + url_servicewms + "\"}";
                        LayerElement l = new LayerElement(Convert.ToInt32(rdr["id_layer"]), rdr["title"].ToString(), layerValues);
                        layeratKerkim.shtoElement(l);
                    }
                }
                return layeratKerkim;
            }
            catch (SqlException)
            {
                return layeratKerkim;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();
            }
        }

        public Layers MerrLayeratEditim(int gjuha, int idNdermarrje)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()

            SqlCommand cmd = new SqlCommand("select * from F_GIS_merrLayersSipasRolitDheGjuhesPerEditim(@id_rol,@gjuha,@IDNDERMARRJE)", con);

            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN);   
            Layers layeratEditim = new Layers();

            string layerValueBosh = "{\"layer\":\"bosh\",\"url_servicewfs\":\"\",\"workspace_name\":\"\",\"layer_title\":\"\",\"url_servicewms\":\"\"}";
            LayerElement lbosh = new LayerElement(-1, "", layerValueBosh);
            layeratEditim.shtoElement(lbosh);

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        LayerElement l = new LayerElement(rdr["name"].ToString(), rdr["title"].ToString(), Convert.ToInt32(rdr["id_layer"]));
                        layeratEditim.shtoElement(l);
                    }
                }

                return layeratEditim;
            }
            catch (SqlException)
            {
                return layeratEditim;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }

        
        }
        public Layers MerrLayeratPerExport(int gjuha, int idNdermarrje)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()

            SqlCommand cmd = new SqlCommand("select * from F_GIS_merrLayersSipasRolitDheGjuhesPerExport(@id_rol,@gjuha,@IDNDERMARRJE)", con);

            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN);
            Layers layeratExport = new Layers();
            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        LayerElement l = new LayerElement( rdr["title"].ToString(), Convert.ToInt32(rdr["id_layer"]),rdr["layer_name"].ToString());
                        layeratExport.shtoElement(l);
                    }
                }

                return layeratExport;
            }
            catch (SqlException)
            {
                return layeratExport;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }
        
        }
        public Layers merrLayeratInformacion(int gjuha, int idNdermarrje)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()
            Layers layeratInfo;
            SqlCommand cmd = new SqlCommand("select * from F_GIS_merrLayersSipasRolitDheGjuhesPerInfo(@id_rol,@gjuha,@IDNDERMARRJE)", con);

            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN);            

            layeratInfo = new Layers();

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {
                        LayerElement l = new LayerElement(rdr["name"].ToString(), rdr["title"].ToString(), Convert.ToInt32(rdr["id_layer"]));

                        List<clsPerkthimElement> kolonat = l.merrKolonatPerkthimPerLayer(gjuha, Convert.ToInt32(rdr["id_layer"]));

                        l.vendosKolonatLayer(kolonat);
                        layeratInfo.shtoElement(l);
                    }
                }

                return layeratInfo;
            }
            catch (SqlException)
            {
                return layeratInfo;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }




        
        }
        public Layers merrLayerPerBuffer(int gjuha, int idNdermarrje)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()

            SqlCommand cmd = new SqlCommand("select * from F_GIS_merrLayersSipasRolitDheGjuhesPerBuffer(@id_rol,@gjuha,@IDNDERMARRJE)", con);

            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN);  
             Layers layeratBuffer = new Layers();

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    while (rdr.Read())
                    {

                        LayerElement l = new LayerElement(rdr["name"].ToString(), rdr["title"].ToString(), Convert.ToInt32(rdr["id_layer"]));
                            
                        layeratBuffer.shtoElement(l);
                    }
                }

                return layeratBuffer;
            }
            catch (SqlException)
            {
                return layeratBuffer;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }


        
        }

        public bool kaTeDrejteLayerBuffer(int gjuha, int idNdermarrje, string layerName)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()
            SqlCommand cmd = new SqlCommand("prc_GIS_eshteLayersSipasRolitDheGjuhesPerBuffer", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN);  
            SqlParameter layName = new SqlParameter("@layerName", SqlDbType.NVarChar);
             layName.Value = layerName;
             cmd.Parameters.Add(layName); 

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                
            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }
        
        
        
        }

        public bool kaTeDrejteLayerEditim(int gjuha, int idNdermarrje, string layerName)
        {
            dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
            dbManager.ConnectionString = dbManager.GetConnectionString();

            SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()
            SqlCommand cmd = new SqlCommand("prc_GIS_eshteLayersSipasRolitDheGjuhesPerEditim", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
            rol.Value = idRol;
            cmd.Parameters.Add(rol);
            SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
            gj.Value = gjuha;
            cmd.Parameters.Add(gj);
            SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
            idN.Value = idNdermarrje;
            cmd.Parameters.Add(idN); 
            SqlParameter layName = new SqlParameter("@layerName", SqlDbType.NVarChar);
            layName.Value = layerName;
            cmd.Parameters.Add(layName);

            try
            {
                con.Open();
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    if (rdr.HasRows)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }


            }
            catch (SqlException)
            {
                return false;
            }
            finally
            {
                //rdr.Dispose();
                con.Dispose();
                cmd.Dispose();
                con.Close();

            }



        }

        public Layers merrLayerPerTopologyPike(int gjuha, int idNdermarrje)
      {
          dbManager = new DbAccessLayer.DBManager(DataProvider.SqlServer);
          dbManager.ConnectionString = dbManager.GetConnectionString();

          SqlConnection con = new SqlConnection(dbManager.ConnectionString);    //DbConn.merrConnStringUser()
          Layers layeratPerTopoPike;
          SqlCommand cmd = new SqlCommand("prc_T_GIS_App_GEOMETRYCOLS_merrSipasRolitDheGjuhesPerTopologji", con);
          cmd.CommandType = CommandType.StoredProcedure;
          SqlParameter rol = new SqlParameter("@id_rol", SqlDbType.Int);
          rol.Value = idRol;
          cmd.Parameters.Add(rol);
          SqlParameter gj = new SqlParameter("@gjuha", SqlDbType.Int);
          gj.Value = gjuha;
          cmd.Parameters.Add(gj);
          SqlParameter idN = new SqlParameter("@IDNDERMARRJE", SqlDbType.Int);
          idN.Value = idNdermarrje;
          cmd.Parameters.Add(idN); 
          layeratPerTopoPike = new Layers();
          try
          {
              con.Open();
              using (SqlDataReader rdr = cmd.ExecuteReader())
              {

                  while (rdr.Read())
                  {
                      LayerElement l = new LayerElement(rdr["emer_tb"].ToString(), rdr["title"].ToString(), Convert.ToInt32(rdr["id_layer"]));
                      layeratPerTopoPike.shtoElement(l);
                  }
              }

              return layeratPerTopoPike;
          }
          catch (SqlException)
          {
              return layeratPerTopoPike;
          }
          finally
          {
              //rdr.Dispose();
              con.Dispose();
              cmd.Dispose();
              con.Close();

          }


      
      }
       
        public List<Dictionary<string, object>> ChangeKeyFromListDictionaryForGridViewAll(int tableId, List<Dictionary<string, object>> lista)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            colKonfigurimeGIS allPerkthimet = new colKonfigurimeGIS();
            allPerkthimet.mbushKonfigurime(tableId, db);

            foreach (var listaRow in lista)
            {
                row = new Dictionary<string, object>();
                foreach (var listaColumn in listaRow)
                {
                    clsKonfigurimeGIS tempKonfig = new clsKonfigurimeGIS();
                    string newKey = tempKonfig.findPershkrimByFusha(allPerkthimet, listaColumn.Key).Pershkrimi; //findPershkrimByFusha(allPerkthimet, listaColumn.Key).Pershkrimi;
                    row.Add(newKey, listaColumn.Value);
                }
                rows.Add(row);
            }
            return rows;
        }

        public List<Dictionary<string, object>> ChangeKeyFromListDictionaryForGridView(int tableId, List<Dictionary<string, object>> lista)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (var listaRow in lista)
            {
                row = new Dictionary<string, object>();
                foreach (var listaColumn in listaRow)
                {
                    DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
                    string newKey = db.perktheElement(tableId, listaColumn.Key);
                    row.Add(newKey, listaColumn.Value);
                }
                rows.Add(row);
            }
            return rows;
        }

        public List<Dictionary<string, object>> ConvertDataTabletoList(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                rows.Add(row);
            }
            return rows;
        }
    }
}