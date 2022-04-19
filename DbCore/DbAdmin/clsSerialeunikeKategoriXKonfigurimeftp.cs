using System;
using System.Collections.Generic;
using System.Data;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbAdmin
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne lidhjen mes kategorive te serialeve dhe konfigurimeve Ftp 
    ///  (Te dhenat  merren nga tabela : T_SERIALEUNIKE_KATEGORI_X_KONFIGURIMEFTP)
    /// </summary>
    public class clsSerialeunikeKategoriXKonfigurimeftp:IDataBase
    {
        #region Atributet

        private int idLidhje;
        private int idKategori;
        private int idKonfigurimFtp;
        private int idStatusDok;
        private IDataRecord record;

        #endregion

        #region Konstruktoret

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsSerialeunikeKategoriXKonfigurimeftp(int idLidhje, int idKategori, int idKonfigurimFtp, int idStatusDok)
        {
            this.idLidhje = idLidhje;
            this.idKategori = idKategori;
            this.idKonfigurimFtp = idKonfigurimFtp;
            this.idStatusDok = idStatusDok;
        }

        /// <summary>
        /// Konstruktori i klases
        /// </summary>
        public clsSerialeunikeKategoriXKonfigurimeftp()
        {
        }

        public clsSerialeunikeKategoriXKonfigurimeftp(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int IdLidhje
        {
            get { return idLidhje; }
            set { idLidhje = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne  qe perfaqson kategorine e serialit  
        /// I merr vlerat nga tabela T_SERIALEUNIKE_KATEGORI).
        /// </summary>
        public int IdKategori {
            get { return idKategori; }
            set { idKategori = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e Konfigurimit ftp
        /// I merr vlerat nga tabela T_KONFIGURIMEFTP
        /// </summary>
        public int IdKonfigurimFtp {
            get { return idKonfigurimFtp; }
            set { idKonfigurimFtp = value; }
        }
 
        public int IdStatusDok {
            get { return idStatusDok; }
            set { idStatusDok = value; }
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Ruan objektin e lidhjes se autorizimit ne tabelen perkatese ne databaze.
        /// </summary>
        public clsMesazh Ruaj()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.ruajLidhjeKategoriserialKonfigurimftp(out this.idLidhje, this.idKategori, this.idKonfigurimFtp);
        }

        /// <summary>
        /// Modifikon rreshtin perkates ne databaze duke perdorur te dhenat qe jane tek objekti i lidhjes.
        /// </summary>
        public clsMesazh Modifiko()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.modifikoLidhjeKategoriserialKonfigurimftp(this.idLidhje, this.idKategori, this.idKonfigurimFtp);
        }

        /// <summary>
        /// Fshin rreshtin perkates nga tabela perkatese ne databaze sipas ID-se qe i eshte caktuar objektit te lidhjes.
        /// </summary>
        public clsMesazh Fshi()
        {
            using (clsDatabaseAdmin data = new clsDatabaseAdmin())
                return data.fshiLidhjeKategoriserialKonfigurimftp(this.IdLidhje);
        }
        public static clsMesazh ruajLidhjetKategoriKonfigurim(string konfigurimeFtp, int idKategori, int idNdermarrje)
        {
            var mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            if (konfigurimeFtp != "")
            {
                colSerialeunikeKategoriXKonfigurimeftp colLidhjet = new colSerialeunikeKategoriXKonfigurimeftp();
                string[] pars1 = konfigurimeFtp.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    clsSerialeunikeKategoriXKonfigurimeftp lidhje = new clsSerialeunikeKategoriXKonfigurimeftp();
                    lidhje.IdKonfigurimFtp = clsKonfigurimFtp.ktheIDKonfigurimFtp(pars1[i], idNdermarrje);
                    colLidhjet.Add(lidhje);
                }
                foreach (clsSerialeunikeKategoriXKonfigurimeftp o in colLidhjet)
                {
                    if (o.IdKonfigurimFtp == -1)
                        continue;
                    o.IdKategori = idKategori;
                    mesazh = o.Ruaj();
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            return mesazh;
        }
        public static clsMesazh modifikoLidhjetKategoriKonfigurim(string konfigurimeFtp, int idKategori, int idNdermarrje)
        {
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            colSerialeunikeKategoriXKonfigurimeftp colLidhjetEKategorise = new colSerialeunikeKategoriXKonfigurimeftp(idKategori);
            colSerialeunikeKategoriXKonfigurimeftp colLidhjet = new colSerialeunikeKategoriXKonfigurimeftp();
            if (konfigurimeFtp != "")
            {
                string[] pars1 = konfigurimeFtp.Split(',');
                for (int i = 0; i < pars1.Length; i++)
                {
                    colLidhjet.Add(new clsSerialeunikeKategoriXKonfigurimeftp()
                    {
                        IdKonfigurimFtp = clsKonfigurimFtp.ktheIDKonfigurimFtp(pars1[i], idNdermarrje)
                    });
                }
            }
            for (int i = 0; i < colLidhjet.Count; i++)
            {
                int idKonfigurimFtp = colLidhjet[i].IdKonfigurimFtp;
                if (idKonfigurimFtp == -1)
                    continue;
                colLidhjet[i].IdKategori = idKategori;
                clsSerialeunikeKategoriXKonfigurimeftp lidhjeNjejte = colLidhjetEKategorise.Find(x => x.IdKonfigurimFtp == idKonfigurimFtp);
                if (lidhjeNjejte != null)
                {   //i heqim nga collectioni konfigurimet ftp qe s'jane ndryshuar sepse ne te do ngelen vetem konfigurimet ftp qe do te fshihen (vendosen status 2) 
                    colLidhjetEKategorise.Remove(lidhjeNjejte);
                    continue;
                }
                mesazh = colLidhjet[i].Ruaj();
                if (!mesazh.Status)
                    return mesazh;
            }
            //fshihen konfigurimet ftp te vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane
            for (int j = 0; j < colLidhjetEKategorise.Count; j++)
            {
                mesazh = colLidhjetEKategorise[j].Fshi();
                if (!mesazh.Status)
                    return mesazh;
            }
            return mesazh;
        }

        public void Mbush(IDataRecord record)
        {
            try
            {
                int.TryParse(record["IDLIDHJE"].ToString(), out idLidhje);
                int.TryParse(record["IDKATEGORI"].ToString(), out idKategori);
                int.TryParse(record["IDKONFIGURIMFTP"].ToString(), out idKonfigurimFtp);
                int.TryParse(record["IDSTATUSDOK"].ToString(), out idStatusDok);
            } catch (InvalidCastException ex)
            {
                ImbLogger.Error(ex);
                throw new Exception("ERROR: Gabim casti gjate marrjes se lidhjes kategori seriali me konfigurim ftp nga db-ja!");
            } catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException("ERROR: Gabim gjate marrjes se lidhjes kategori seriali me konfigurim ftp nga databaza!");
            }
        }

        #endregion
    }
}
