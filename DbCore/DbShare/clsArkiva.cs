using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.DataBase;
using System.IO;
namespace DbCore.DbShare
{
    public class clsArkiva
    {
        #region Attribute

        private int idArkiva;
        private int idDok;
        private int idLlojDok;
        private DateTime dtKrijimi;
        private string filetype;
        private string filePath;
        private string filename;
        private string shenime;
        private DateTime dtModifikimi;
        private int idStatusDok;
        private int idKrijuesi;
        private int idPerdoruesi;
        private int idKategoriArkive;
        private bool urlSakte;
        private String emerConn;
        private bool thumb;
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IDArkiva
        {
            get { return idArkiva; }
            set { idArkiva = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  e dok te regjistrimit qe po i bashkangjitet nje/disa file.
        /// </summary>
        public int IDDok
        {
            get { return idDok; }
            set { idDok = value; }
        }

        public String EmerConn
        {
            get{return emerConn;}
            set { emerConn = value; }
        }

        ///<summary>
        ///Kthen/Vendos Id-ne e llojit te dok
        ///</summary>
        public int IDLlojDok
        {
            get { return idLlojDok; }
            set { idLlojDok = value; }
        }

        ///<summary>
        ///Kthen/Vendos Daten e krijimit qe gjenerohet automatikisht
        ///</summary>
        public DateTime DtKrijimi
        {
            get
            {
                return dtKrijimi;
            }
            set
            {
                dtKrijimi = value;
            }
        }

        ///<summary>
        ///Kthen/Vendos daten e modifikimit te arkives
        ///</summary>
        public DateTime DtModifikimi
        {
            get
            {
                return dtModifikimi;
            }
            set
            {
                dtModifikimi = value;
            }
        }

        ///<summary>
        ///Kthen/Vendos tipin e file te uploaduar
        ///</summary>
        public string FileType
        {
            get { return filetype; }
            set { filetype = value; }
        }

        ///<summary>
        ///Kthen/Vendos Emrin e file-it te uploaduar
        ///</summary
        public string FileName
        {
            get { return filename; }
            set { filename = value; }
        }

        ///<summary>
        ///Kthen/Vendos Shenimet e dok te arkivuar
        ///<summary>
        public string Shenime
        {
            get { return shenime; }
            set { shenime = value; }
        }

        ///<summary>
        ///Kthen/Vendos Pathin kur eshte ruajtur file
        ///</summary>
        public string Path
        {
            get
            {
                return filePath;
            }
            set
            {
                filePath = value;
            }
        }

        ///<summary> 
        ///Kthen/Vendos Statusin e dok 
        ///</summary>
        public int IDStatusDok
        {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        ///<summary>
        ///Kthen/Vendos ID-ne e perdoruesit qe e arkivoi file
        ///</summary>
        public int IDKrijuesi
        {
            get { return idKrijuesi; }
            set { idKrijuesi = value; }
        }

        ///<summary>
        ///Kthen/Vendos ID-ne e perdoruesit qe modifikoi file e arkivuar
        ///</summary>
        public int IDPerdoruesi
        {
            get { return idPerdoruesi; }
            set { idPerdoruesi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKategoriArkive
        {
            get { return idKategoriArkive; }
            set { idKategoriArkive = value; }
        }
        public bool UrlSakte
        {
            get { return urlSakte; }
            set { urlSakte = value; }
        }
        public bool Thumb
        {
            get { return thumb; }
            set { thumb = value; }
        }
        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori pa parametra
        /// </summary>
        public clsArkiva() { }
        public clsArkiva(string path)
        {
            ///mbush arkiven ne baze te pathit
        }

        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="idarkiva">id incrementuese e arkives</param>
        /// <param name="iddok">id e dok. te lidhur me arkive</param>
        /// <param name="idllojdok">lloji i dok. te lidhur me arkive</param>
        /// <param name="dtmodifikimi"></param>
        /// <param name="ftype">tipi i file te uploaduar</param>
        /// <param name="fpath">vendi ku do te ruhet file</param>
        /// <param name="fname">emri i file</param>
        /// <param name="fshenime">shenime rreth filet te arkivuar</param>
        /// <param name="idstatusdok">statusi i file te arkivuar</param>
        /// <param name="idkrijuesi">id e perdoruesit qe e krijoji </param>
        /// <param name="idperdoruesi">id e perdoruesit qe e modifikoi</param>
        public clsArkiva(int idarkiva, int iddok, int idllojdok, string ftype, string fpath, string fname, string fshenime, int idstatusdok, int idkrijuesi, int idperdoruesi, int idKategoriArkive,String emerConn,
            bool thumb=false)
        {
            idArkiva = idarkiva;
            idDok = iddok;
            idLlojDok = idllojdok;
            filetype = ftype;
            filename = fname;
            filePath = fpath;
            shenime = fshenime;
            idStatusDok = idstatusdok;
            idKrijuesi = idkrijuesi;
            idPerdoruesi = idperdoruesi;
            this.idKategoriArkive = idKategoriArkive;
            this.emerConn = emerConn;
            this.thumb = thumb;
            
        }
        
        /// <summary>
        /// Konstruktori me parametra
        /// </summary>
        /// <param name="iddok">id e dok. te lidhur me arkive</param>
        /// <param name="idllojdok">lloji i dok. te lidhur me arkive</param>
        /// <param name="ftype">tipi i file te uploaduar</param>
        /// <param name="fpath">vendi ku do te ruhet file</param>
        /// <param name="fname">emri i file</param>
        /// <param name="fshenime">shenime rreth filet te arkivuar</param>
        /// <param name="idstatusdok">statusi i file te arkivuar</param>
        /// <param name="idkrijuesi">id e perdoruesit qe e krijoji </param>
        /// <param name="idperdoruesi">id e perdoruesit qe e modifikoi</param>
        public clsArkiva(int iddok, int idllojdok, string ftype, string fpath, string fname, string fshenime, int idstatusdok, int idkrijuesi, int idperdoruesi,String emerConn)
        {
            idDok = iddok;
            idLlojDok = idllojdok;
            filetype = ftype;
            filename = fname;
            filePath = fpath;
            shenime = fshenime;
            idStatusDok = idstatusdok;
            idKrijuesi = idkrijuesi;
            idPerdoruesi = idperdoruesi;
            this.emerConn = emerConn;
 
        }

        public clsArkiva(int iddok, int idllojdok, DateTime dtmodifikimi, string fpath, int idstatusdok, int userid)
        {
            idDok = iddok;
            idLlojDok = idllojdok;
            DtModifikimi = dtmodifikimi;
            filePath = fpath;
            idStatusDok = idstatusdok;
            idPerdoruesi = userid;
        }

        public clsArkiva(DataRow rreshti)
        {
            
            mbushArkive(rreshti);
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin klient/furnitor ne tabelen perkatese ne databaze.Therret funksionin
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public clsMesazh ruaj()
        {
            clsMesazh msg = new clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes!");
            clsDatabaseShare db = new clsDatabaseShare();
            try
            {
                db.beginTransaksion();

                msg = ruajArkiva(db);

                if (!msg.Status)
                {
                    db.rollbackTransaksion();
                    return msg;
                }
                db.commitTransaksion();
                return msg;
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                db.rollbackTransaksion();
                return msg;
            }
        }

        
        public clsMesazh update(clsDatabaseShare db)
        {
            clsMesazh msg = new clsMesazh(true);
            
            msg = updateArkiva(db);
            return msg;
        }

        public clsMesazh fshiUpdateStatus()
        {
            clsMesazh msg = new clsMesazh(true);
            clsDatabaseShare db = new clsDatabaseShare();
            db.beginTransaksion();

            msg = fshiArkivaUpdateStatus(db);

            if (!msg.Status)
            {
                db.rollbackTransaksion();
                return msg;
            }
            db.commitTransaksion();
            return msg;
        }

        /// <summary>
        /// kontrollon nese ekziston dokumenti ne baze te llojit dhe ID-se
        /// </summary>
        /// <param name="llojDok"></param>
        /// <param name="idDok"></param>
        /// <returns></returns>
        public static bool ekzistonDokumenti(int llojDok, int idDok)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            bool kaDokument = db.ekzistonDokumentiSipasLlojDokAndID(llojDok,idDok);
            db.Dispose();

            return kaDokument;
        }
        public static string merrThumbnailDefault( int idDok)
        {
            clsDatabaseShare db = new clsDatabaseShare();
            string path = db.merrThumnailDefault(idDok);
            db.Dispose();
            return path;
        }
        public static string ktheImazhPerdoruesi(int idPerdorues)
        {
            using (clsDatabaseShare dbShare = new clsDatabaseShare())
                return dbShare.ktheImazhPerdoruesi(idPerdorues);
        }

     
        public clsMesazh fshiArkivaUpdateStatus(clsDatabaseShare db)
        {
            string conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());

            clsMesazh mesazh = db.FshiArkiva(idDok, idLlojDok, filePath, idStatusDok, idPerdoruesi,conn);
            return mesazh;
        }
        public clsMesazh fshiArkiven()
        {
            return fshiArkiven(IDPerdoruesi,Path);
        }
        public static clsMesazh fshiArkiven(int idPerdoruesi, string path)
        {
            return new clsDatabaseShare().FshiArkiva(idPerdoruesi, path);
        }
        #endregion
        public static string pathiFizik(string pathArkiva, string path) { 
            return pathArkiva + @path.Replace("~\\", "\\");
        }
        public static clsMesazh lexoFotoBase64(out string fotoBase64 , string path)
        {
            fotoBase64 = null;
            if (!File.Exists(path))
            {
                return new clsMesazh(false, "Nuk ekzistion fotoja");
            }
            fotoBase64 = Convert.ToBase64String(File.ReadAllBytes(path));
            return new DbCore.clsMesazh(true, "Me sukses");
        }
        public static Object[] LexoFototPerNdermarrjenShtuarPasDatesFunditLexuar(int numerFotosh, int numerChunk, string kodNdermarrje, string perdorues, string DataFunditLexuar, string pathArkiva)
        {
            DataTable tablelaFotove = new clsDatabaseShare().merrFototPerNdermarrjenShtuarPasDatesFunditLexuar(numerFotosh, numerChunk, kodNdermarrje, perdorues, DataFunditLexuar);
            return LexoFototPerNdermarrjenShtuarPasDatesFunditLexuar(tablelaFotove, pathArkiva);
          
        }
        public static Object[] LexoFototPerNdermarrjenShtuarPasDatesFunditLexuar(DataTable fototDT, string pathArkiva)
        {

            int nrFotoshLexuar = fototDT.Rows.Count;
          //  Object[] fotot = new Object[nrFotoshLexuar];
            List<Object> list = new List<Object>();
            for (int i = 0; i < nrFotoshLexuar; i++)
            {

                DataRow row = fototDT.Rows[i];
                string fotoja = "";
                if (Convert.ToInt32(row["IDSTATUSDOK"]) != 2) 
                    lexoFotoBase64(out fotoja, pathiFizik(pathArkiva, row["PATH"].ToString()));

                if (fotoja != null )
                    list.Add( new
                    {
                        foto = fotoja,
                        kodArtikull = row["KODARTIKULLI"].ToString(),
                        iddok = row["IDDOK"].ToString(),
                        idLlojDok = row["IDLLOJDOK"].ToString(),
                        DTKRIJIMI =  row["DTKRIJIMI"].ToString(),
                        DTMODIFIKIM = row["DTMODIFIKIM"].ToString(),
                        FILETYPE = row["FILETYPE"],
                        FILENAME = row["FILENAME"],
                        IDSTATUSDOK = row["IDSTATUSDOK"],
                        THUMBNAIL = Convert.ToBoolean(row["THUMBNAIL"])
                    });
            }
            return list.ToArray();
            //return fotot;


        }
        


        #region Metoda Internal

        internal clsMesazh ruajArkiva(clsDatabaseShare db)
        {
            int id = -1;
            clsMesazh mesazh;
            if (thumb)
                mesazh=db.setThumbnail(idDok, idLlojDok, filePath, idPerdoruesi);
            mesazh = db.RuajArkiva(out id, idDok, idLlojDok, filetype, filePath, emerConn, filename, shenime, idStatusDok, idKrijuesi, idPerdoruesi, idKategoriArkive, thumb);
            this.idArkiva = id;        
            return mesazh;
        }

        internal clsMesazh updateArkiva(clsDatabaseShare db)
        {
            clsMesazh mesazh = db.UpdateArkiva(idArkiva, idDok, idLlojDok, filetype, filePath, filename, shenime, idStatusDok, idKrijuesi, idPerdoruesi, idKategoriArkive,emerConn, thumb);
            return mesazh;
        }

        /// <summary>
        /// mbush arkivat nga databaza
        /// </summary>
        /// <param name="dbDataRowSkemaAzhornimi">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushArkive(DataRow dbDataRowArkiva)
        {
            if (dbDataRowArkiva != null)
            {
                try
                {
                    int.TryParse(dbDataRowArkiva["IDARKIVA"].ToString(), out idArkiva);
                    int.TryParse(dbDataRowArkiva["IDDOK"].ToString(), out idDok);
                    int.TryParse(dbDataRowArkiva["IDLLOJDOK"].ToString(), out idLlojDok);
                    filetype = dbDataRowArkiva["FILETYPE"].ToString();
                    filePath = dbDataRowArkiva["PATH"].ToString();
                    emerConn = dbDataRowArkiva["CONNEMER"].ToString();
                    filename = dbDataRowArkiva["FILENAME"].ToString();
                    shenime = dbDataRowArkiva["SHENIME"].ToString();
                    bool.TryParse(dbDataRowArkiva["URLSAKTE"].ToString(), out urlSakte);
                    int.TryParse(dbDataRowArkiva["IDSTATUSDOK"].ToString(), out idStatusDok);
                    int.TryParse(dbDataRowArkiva["IDKRIJUESI"].ToString(), out idKrijuesi);
                    int.TryParse(dbDataRowArkiva["IDPERDORUESI"].ToString(), out idPerdoruesi);
                    int.TryParse(dbDataRowArkiva["IDKATEGORIARKIVE"].ToString(), out idKategoriArkive);
                    DateTime.TryParse(dbDataRowArkiva["DTKRIJIMI"].ToString(), out dtKrijimi);
                    DateTime.TryParse(dbDataRowArkiva["DTMODIFIKIMI"].ToString(), out dtModifikimi);
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se arkivave nga db-ja");
                }
            }
            else
                return false;
        }

        public static clsMesazh NdryshoPath(string oldPath, string newDirectory,string newFileName, int idPerdoruesi,String conn)
        {
           

            using (clsDatabaseShare dbShare = new clsDatabaseShare())
                return dbShare.ModifikoSipasPathit(oldPath,newDirectory+@"\"+newFileName,newFileName,idPerdoruesi,conn);
        }

        public static  String ktheConnString (string conn)
        {
            String connectionString;
            if (conn == MyConnectionsManager.ConnStringNameDefault)
                connectionString = "";
            else
                connectionString = conn;

            return connectionString;
        }


        #endregion

    }
}