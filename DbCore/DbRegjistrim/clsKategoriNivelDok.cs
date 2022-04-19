using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbCore.DbAdmin;
using System.Data;

namespace DbCore.DbRegjistrim
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne kategorite e nivelit te dokumentave
    ///  (Te dhenat  merren nga tabela : T_KATEGORINIVELDOK)
    /// </summary>
    ///   <remarks > kjo klase eshte klase ndihmese qe sherben per te shfaqur kategorite e niveleve te dokumentave sipas komponenteve</remarks>
    public class clsKategoriNivelDok
    {
        #region Atribute

        private int idKategori;
        private string pershkrimi;
        private int idKomponente;
        private colNivelRegjistrimi ocolNivelRegjistrimi;
        private int idSuperKategori;
        private bool perImport;
        private int idKatNrAuto;
        private bool formatNumri;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases me parametra
        /// </summary>
        /// <param name="idKat"> id ritese e kategorise</param>
        /// <param name="idKomp"> id e komponentes me te cilen lidhet</param>
        /// <param name="pershk"> pershkrimi i kategorise</param>
        public clsKategoriNivelDok(int idKat, string pershk, int idKomp, int idSuperKat, bool perImport, int idKatNrAuto, bool formatNumri)
        {
            idKategori = idKat;
            pershkrimi = pershk;
            idKomponente = idKomp;
            idSuperKategori = idSuperKat;
            this.perImport = perImport;
            this.IdKatNrAuto = idKatNrAuto;
            ocolNivelRegjistrimi = new colNivelRegjistrimi();
            this.formatNumri = formatNumri;
        }

        /// <summary>
        /// konstruktor me 1 parameter string
        /// </summary>
        /// <param name="pershk">pershkrimi i kategorise</param>
        public clsKategoriNivelDok(string pershk)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            mbushKatNivelDok(dbKatNivelDok.ktheKategoriNivelDok(pershk));
            dbKatNivelDok.Dispose();
        }

        public clsKategoriNivelDok(int idKatDok)
        {
            using (clsDatabaseRegjistrim data = new clsDatabaseRegjistrim())
            {
                mbushKatNivelDok(data.ktheKategoriNivelDokSipasID(idKatDok));
            }
        }

        /// <summary>
        /// Konstruktori i klases pa parametra
        /// </summary>
        public clsKategoriNivelDok()
        {
        }

        public clsKategoriNivelDok(DataRow rreshti)
        {
            
            mbushKatNivelDok(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdKategori
        {
            get { return idKategori; }
            set { idKategori = value; }
        }

        public int IdKatNrAuto
        {
            get
            {
                return idKatNrAuto;
            }
            set
            {
                idKatNrAuto = value;
            }
        }
        public bool PerImport
        {
            get
            {
                return perImport;
            }
            set
            {
                perImport = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos pershkrimin e kategorise.
        /// </summary>
        public String Pershkrimi
        {
            get { return pershkrimi; }
            set { pershkrimi = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes me te cilen lidhet.
        /// </summary>
        public int IdKomponente
        {
            get { return idKomponente; }
            set { idKomponente = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e superkategorise me te cilen lidhet.
        /// </summary>
        public int IdSuperKategori
        {
            get { return idSuperKategori; }
            set { idSuperKategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos koleksion me te gjithe nivel e regjistrimeve te kesaj kategorie.
        /// </summary>
        public colNivelRegjistrimi OColNivelRegjistrimi
        {
            get { return ocolNivelRegjistrimi; }
            set { ocolNivelRegjistrimi = value; }
        }

        public bool FormatNumri
        {
            get
            {
                return formatNumri;
            }
            set
            {
                formatNumri = value;
            }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e  kategorise se nivelit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ruajKategoriNivelDok"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese ruajtja eshte kryer ne rregull apo jo</returns>
        public clsMesazh ruaj()
        {   
            int idkat;
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_ruajt = data.ruajKategoriNivelDok( out idkat, this.Pershkrimi, this.idSuperKategori);
            this.IdKategori = idkat;
            data.Dispose();
            return u_ruajt;
        }

        /// <summary>
        /// Modifikon objektin e  kategorise se nivelit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.modifikoKategoriNivelDok"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese modifikimi eshte kryer ne rregull apo jo</returns>
        public clsMesazh modifiko()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_modifikua = data.modifikoKategoriNivelDok(this.IdKategori, this.Pershkrimi);
            data.Dispose();
            return u_modifikua;
        }

        /// <summary>
        /// Fshin objektin e  kategorise se nivelit te dokumentit ne tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.fshiKategoriNivelDok"/> 
        /// </summary>
        /// <returns > nje objekt clsMesazh qe tregon nese fshirja eshte kryer ne rregull apo jo</returns>
        public clsMesazh fshi()
        {
            clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            clsMesazh u_fshi = data.fshiKategoriNivelDok(this.IdKategori);
            data.Dispose();
            return u_fshi;
        }

        /// <summary>
        /// Merr objektin e  kategorise se nivelit te dokumentit sipas pershkrimit nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKategoriNivelDok"/> 
        /// </summary>
        /// <returns > nje objekt  clsKategoriNivelDok qe permban kategorine e nivelit te dokumentit te kerkuar</returns>
        public clsKategoriNivelDok merr()
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrKategoriNivelDok(this)[0]; 
            clsKategoriNivelDok data = new clsKategoriNivelDok(this.Pershkrimi);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kategorise se nivelit te dokumentit sipas id nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKategoriNivelDokSipasID"/> 
        /// </summary>
        /// <returns > nje objekt  clsKategoriNivelDok qe permban kategorine e nivelit te dokumentit te kerkuar</returns>
        public clsKategoriNivelDok merrSipasId()
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrKategoriNivelDokSipasID(this)[0];
            clsKategoriNivelDok data = new clsKategoriNivelDok();
            data.mbushKategoriNivelDokSipasID(this.IdKategori);
            return data;
        }

        /// <summary>
        /// Merr objektin e  kategorise se nivelit te dokumentit sipas id nga tabelen perkatese ne databaze.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheKategoriNivelDokSipasID"/> 
        /// </summary>
        /// <returns > nje objekt  clsKategoriNivelDok qe permban kategorine e nivelit te dokumentit te kerkuar</returns>
        public clsKategoriNivelDok merrSipasIdPaNivelet()
        {
            clsKategoriNivelDok data = new clsKategoriNivelDok();
            data.mbushKategoriNivelDokSipasIDPaNivele(this.IdKategori);
            return data;
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrKategoriNivelDokSipasIDPaNivele(this)[0];
        }

        /// <summary>
        /// Merr te gjitha objektet e kategorise se nivelit te dokumentit sipas ndermarjes.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheKategoriNivelDok"/> 
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit </param>
        /// <returns > nje objekt  colKategoriNivelDok qe permban kategorite e nivelit te dokumentit  te nje ndermarje</returns>
        public colKategoriNiveleDok merriTeGjithe(int idNdermVit)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheKategoriNivelDok(idNdermVit);
            colKategoriNiveleDok data = new colKategoriNiveleDok();
            data.mbushGjitheKategoriNivelDok();
            return data;
        }

        /// <summary>
        /// Merr te gjitha objektet e kategorise se nivelit te dokumentit sipas ndermarjes qe te mos ngarkohet ne colection kategoria me id = -3 mqs eshte per filtrin ne gride.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheGjitheKategoriNivelDokPa"/> 
        /// </summary>
        /// <param name="idNdermVit">id e ndermarje vitit </param>
        /// <returns > nje objekt  colKategoriNivelDok qe permban kategorite e nivelit te dokumentit  te nje ndermarje</returns>
        public colKategoriNiveleDok merriTeGjithePa(int idNdermVit)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheKategoriNivelDokPa(idNdermVit);
            colKategoriNiveleDok data = new colKategoriNiveleDok();
            data.mbushGjitheKategoriNivelDokPa(idNdermVit);
            return data;
        }

        /// <summary>
        /// Merr gjithe nivelet e regjistrimit te nje kategorie.Therret funksionin
        /// :  <see cref="DbCore.DbRegjistrim.clsDatabaseRegjistrim.ktheNivelRegjistrimi"/> 
        /// </summary>
        /// <returns > nje objekt colNivelRegjistrimi me te gjitha nivelet e regjistrimit te kesaj kategorie.</returns>
        public colNivelRegjistrimi merrNiveleRegjistrimi(int idndermarje)
        {
            colNivelRegjistrimi nivele = new colNivelRegjistrimi();
            nivele.mbushNivelRegjistrimi(this.IdKategori, idndermarje);
            return nivele;
            //clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
            //nivel.IdKategori = this.IdKategori;
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrNivelRegjistrimi(nivel);            
        }

        public colKategoriNiveleDok merriTeGjithePa2(int idNdermVit)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheKategoriNivelDokPa2(idNdermVit);
            colKategoriNiveleDok data = new colKategoriNiveleDok();
            data.mbushGjitheKategoriNivelDokPa2(idNdermVit);
            return data;
        }
                public colKategoriNiveleDok merriTeGjithePaSipasSuperKat(int idsuperkat)
        {
            //clsDatabaseRegjistrim data = new clsDatabaseRegjistrim();
            //return data.merrGjitheKategoriNivelDokPa2(idNdermVit);
            colKategoriNiveleDok data = new colKategoriNiveleDok();
            data.mbushGjitheKategoriNivelDokPaSipasSuperKat(idsuperkat);
            return data;
        }   
    
        /// <summary>
        /// Metode e klases, jo e objektit. Kthen id e kategorise sipas pershkrimit
        /// </summary>
        /// <param name="pershk">pershkrimi i kategorise</param>
        /// <returns>id e kategorise</returns>
        public static int mbushIDKategoriNivDok(string pershk)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            int id = (dbKatNivelDok.ktheKategoriNivelDokSipasKodi(pershk));
            dbKatNivelDok.Dispose();
            return id;
        }

        /// <summary>
        /// Metode qe kthen id e superkategorise se nje kategorie ne baze te id se kesaj te fundit
        /// </summary>
        /// <param name="pershk">id e kategorise</param>
        /// <returns>id e kategorise</returns>
        public static int mbushIDSuperKategoriNivDok(int idKategori)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            int id = (dbKatNivelDok.ktheSuperKategoriNivelDokSipasID(idKategori));
            dbKatNivelDok.Dispose();
            return id;
        }


        /// <summary>
        /// mbush kategori nivele te dokumentit sipas ID
        /// </summary>
        /// <param name="idKategori">id e kategorise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKategoriNivelDokSipasID(int idKategori)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool sukses = mbushKatNivelDok(dbKatNivelDok.ktheKategoriNivelDokSipasID(idKategori));
            dbKatNivelDok.Dispose();
            return sukses;
        }

        /// <summary>
        /// mbush kategori nivele te dokumentit sipas ID
        /// </summary>
        /// <param name="idKategori">id e kategorise</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert kthen false</returns>
        public bool mbushKategoriNivelDokSipasIDPaNivele(int idKategori)
        {
            clsDatabaseRegjistrim dbKatNivelDok = new clsDatabaseRegjistrim();
            bool sukses = mbushKatNivelDokPaNiv(dbKatNivelDok.ktheKategoriNivelDokSipasIDPaNivele(idKategori));
            dbKatNivelDok.Dispose();
            return sukses;
        }

        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush kategorite e nivelit te dokumentit nga databaza
        /// </summary>
        /// <param name="dbDataRowKatNivelDok">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKatNivelDok(DataRow dbDataRowKatNivelDok)
        {
            if (dbDataRowKatNivelDok != null)
            {
                try
                {
                    int.TryParse(dbDataRowKatNivelDok["IDKATDOK"].ToString(), out idKategori);
                    pershkrimi = dbDataRowKatNivelDok["PERSHKRIMI_sq"].ToString();
                    int.TryParse(dbDataRowKatNivelDok["IDKOMPONENTE"].ToString(), out idKomponente);
                    int.TryParse(dbDataRowKatNivelDok["IDSUPERKATEGORI"].ToString(), out idSuperKategori);  
                    bool.TryParse(dbDataRowKatNivelDok["PERIMPORT"].ToString(), out perImport);
                    int.TryParse(dbDataRowKatNivelDok["IDKATNRAUTO"].ToString(), out idKatNrAuto);
                    bool.TryParse(dbDataRowKatNivelDok["FORMATNUMRI"].ToString(), out formatNumri);
                    ocolNivelRegjistrimi = new colNivelRegjistrimi();                    
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nivelit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        /// <summary>
        /// mbush kategorite e nivelit te dokumentit nga databaza
        /// </summary>
        /// <param name="dbDataRowKatNivelDokPaNiv">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushKatNivelDokPaNiv(DataRow dbDataRowKatNivelDokPaNiv)
        {
            if (dbDataRowKatNivelDokPaNiv != null)
            {
                try
                {
                    int.TryParse(dbDataRowKatNivelDokPaNiv["IDKATDOK"].ToString(), out idKategori);
                    pershkrimi = dbDataRowKatNivelDokPaNiv["PERSHKRIMI_sq"].ToString();
                    int.TryParse(dbDataRowKatNivelDokPaNiv["IDKOMPONENTE"].ToString(), out idKomponente);
                    int.TryParse(dbDataRowKatNivelDokPaNiv["IDSUPERKATEGORI"].ToString(), out idSuperKategori);
                    bool.TryParse(dbDataRowKatNivelDokPaNiv["PERIMPORT"].ToString(), out perImport);
                    int.TryParse(dbDataRowKatNivelDokPaNiv["IDKATNRAUTO"].ToString(), out idKatNrAuto);
                    bool.TryParse(dbDataRowKatNivelDokPaNiv["FORMATNUMRI"].ToString(), out formatNumri);
                    ocolNivelRegjistrimi = new colNivelRegjistrimi();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nivelit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        internal bool mbushKatNivelDokPaLupa(DataRow dbDataRowKatNivelDokPaLupa)
        {
            if (dbDataRowKatNivelDokPaLupa != null)
            {
                try
                {
                    int.TryParse(dbDataRowKatNivelDokPaLupa["IDKATDOK"].ToString(), out idKategori);
                    pershkrimi = dbDataRowKatNivelDokPaLupa["PERSHKRIMI_sq"].ToString();
                    int.TryParse(dbDataRowKatNivelDokPaLupa["IDKOMPONENTE"].ToString(), out idKomponente);
                    int.TryParse(dbDataRowKatNivelDokPaLupa["IDSUPERKATEGORI"].ToString(), out idSuperKategori);
                    bool.TryParse(dbDataRowKatNivelDokPaLupa["PERIMPORT"].ToString(), out perImport);
                    int.TryParse(dbDataRowKatNivelDokPaLupa["IDKATNRAUTO"].ToString(), out idKatNrAuto);
                    bool.TryParse(dbDataRowKatNivelDokPaLupa["FORMATNUMRI"].ToString(), out formatNumri);
                    ocolNivelRegjistrimi = new colNivelRegjistrimi();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se kategorive te nivelit te dokumentit nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}
