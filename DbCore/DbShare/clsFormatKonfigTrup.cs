using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DbCore.DbShare
{
    /// <summary>
    ///  Kjo eshte klasa qe sherben per objektet qe perfaqsojne trupin e konfigurimit te formatit te numrit
    ///  (Te dhenat  merren nga tabela : T_FORMATKONFIGNRTRUP)
    /// </summary>
    public class clsFormatKonfigTrup
    {
        #region Atribute

        public static int defaultFormatSasia = 2;
        public static int defaultFormatCmimi = 2;
        public static int defaultFormatVlefta = 2;
        public static int defaultFormatZbritja = 2;
        public static string defaultFormatStringSasia = "0.00";
        public static string defaultFormatStringCmimi = "0.00";
        public static string defaultFormatStringVlefta = "0.00";
        public static string defaultFormatStringZbritja = "0.00";

        private int idFormatKonfigTrup;
        private int idFormatKonfig;
        private int idMonedha;
        private int shifraPasPresjesSasia;
        private int shifraPasPresjesCmimi;
        private int shifraPasPresjesVlefta;
        private int shifraPasPresjesZbritja;
        private string kodMonedhe;
        private string formatSasia;
        private string formatCmimi;
        private string formatVlefta;
        private string formatZbritja;
        private DataRow rreshti;

        #endregion

        #region Konstruktoret

        public clsFormatKonfigTrup()
        {
        }

        public clsFormatKonfigTrup(int idFormatKonfigTrup, int idFormatKonfig, int idMon, int idSasi, int idCmim, int idVlefta, int idZbritja, string monedha, string sasi, string cmim, string vlefta, string zbritje)
        {
            this.idFormatKonfig = idFormatKonfigTrup;
            this.idFormatKonfig = idFormatKonfig;
            this.idMonedha = idMon;
            this.shifraPasPresjesSasia = idSasi;
            this.shifraPasPresjesCmimi = idCmim;
            this.shifraPasPresjesVlefta = idVlefta;
            this.shifraPasPresjesZbritja = idZbritja;
            this.kodMonedhe = monedha;
            this.formatSasia = sasi;
            this.formatCmimi = cmim;
            this.formatVlefta = vlefta;
            this.formatZbritja = zbritje;
        }

        public clsFormatKonfigTrup(int idSasi, int idCmim, int idVlefta, int idZbritja, string sasi, string cmim, string vlefta, string zbritje)
        {
            this.shifraPasPresjesSasia = idSasi;
            this.shifraPasPresjesCmimi = idCmim;
            this.shifraPasPresjesVlefta = idVlefta;
            this.shifraPasPresjesZbritja = idZbritja;
            this.formatSasia = sasi;
            this.formatCmimi = cmim;
            this.formatVlefta = vlefta;
            this.formatZbritja = zbritje;
        }

        public clsFormatKonfigTrup(int idSasi, int idCmim, int idVlefta, int idZbritja, int idMonedha)
        {
            this.shifraPasPresjesSasia = idSasi;
            this.shifraPasPresjesCmimi = idCmim;
            this.shifraPasPresjesVlefta = idVlefta;
            this.shifraPasPresjesZbritja = idZbritja;
            this.idMonedha = idMonedha;
        }

        public clsFormatKonfigTrup(DataRow rreshti)
        {
            
            mbushFormatKonfigTrupi(rreshti);
        }

        #endregion

        #region Metoda Publike

        public clsFormatKonfigTrup krijoFormatKonfigTrup(Dictionary<string, object> rresht, int idNdermarrje)
        {
            if (rresht["KodMonedhe"] == null || rresht["KodMonedhe"].ToString() == "")
                throw new MyException("Monedha nuk eshte e sakte!");

            if (rresht["ShifraPasPresjesSasia"] == null || rresht["ShifraPasPresjesSasia"].ToString() == "")
                throw new MyException("Formati i sasise nuk eshte i sakte!");

            if (rresht["ShifraPasPresjesCmimi"] == null && rresht["ShifraPasPresjesCmimi"].ToString() == "")
                throw new MyException("Formati i cmimit nuk eshte i sakte!");

            if (rresht["ShifraPasPresjesVlefta"] == null && rresht["ShifraPasPresjesVlefta"].ToString() == "")
                throw new MyException("Formati i vleftes nuk eshte i sakte!");

            if (rresht["ShifraPasPresjesZbritja"] == null && rresht["ShifraPasPresjesZbritja"].ToString() == "")
                throw new MyException("Formati i vleftes nuk eshte i sakte!");

            this.idFormatKonfigTrup = int.Parse(rresht["IdFormatKonfigTrup"].ToString());
            this.idFormatKonfig = int.Parse(rresht["IdFormatKonfig"].ToString());
            int idMonedha = 0;
            DbAdmin.clsMonedha mon = new DbAdmin.clsMonedha();
            mon.mbushMonedhen(rresht["KodMonedhe"].ToString(), idNdermarrje);
            idMonedha = mon.IdMonedha;
            this.kodMonedhe = rresht["KodMonedhe"].ToString();
            this.idMonedha = idMonedha;

            this.shifraPasPresjesSasia = int.Parse(rresht["ShifraPasPresjesSasia"].ToString());
            this.shifraPasPresjesCmimi = int.Parse(rresht["ShifraPasPresjesCmimi"].ToString());
            this.shifraPasPresjesVlefta = int.Parse(rresht["ShifraPasPresjesVlefta"].ToString());
            this.shifraPasPresjesZbritja = int.Parse(rresht["ShifraPasPresjesZbritja"].ToString());
            return this;
        }

        public clsMesazh ruaj()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_ruajt = data.ruajFormatTrupi(this.idFormatKonfigTrup, this.idFormatKonfig, this.IdMonedha, this.ShifraPasPresjesSasia, this.ShifraPasPresjesCmimi, this.ShifraPasPresjesVlefta, this.ShifraPasPresjesZbritja);
            data.Dispose();
            return u_ruajt;
        }

        public clsMesazh ruaj(clsDatabaseShare data)
        {
            if (data == null)
                data = new clsDatabaseShare();
            clsMesazh u_ruajt = data.ruajFormatTrupi(this.idFormatKonfigTrup, this.idFormatKonfig, this.idMonedha, this.shifraPasPresjesSasia, this.shifraPasPresjesCmimi, this.shifraPasPresjesVlefta, this.shifraPasPresjesZbritja);
            return u_ruajt;
        }

        public clsMesazh modifiko()
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_modifikua = data.modifikoFormatTrupi(this.idFormatKonfigTrup, this.idFormatKonfig, this.IdMonedha, this.ShifraPasPresjesSasia, this.ShifraPasPresjesCmimi, this.ShifraPasPresjesVlefta, this.ShifraPasPresjesZbritja);
            data.Dispose();
            return u_modifikua;
        }

        public clsMesazh modifiko(clsDatabaseShare data)
        {
            if (data == null)
                data = new clsDatabaseShare();
            clsMesazh u_modifikua = data.modifikoFormatTrupi(this.idFormatKonfigTrup, this.idFormatKonfig, this.IdMonedha, this.ShifraPasPresjesSasia, this.ShifraPasPresjesCmimi, this.ShifraPasPresjesVlefta, this.ShifraPasPresjesZbritja);
            return u_modifikua;
        }

        public static clsMesazh fshiSipasKoka(int idKoka)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiFormatTrupi(idKoka);
            data.Dispose();
            return u_fshi;
        }

        public static clsMesazh fshiSipasKoka(clsDatabaseShare data, int idKoka)
        {
            if (data == null)
                data = new clsDatabaseShare();
            clsMesazh u_fshi = data.fshiFormatTrupi(idKoka);
            return u_fshi;
        }

        public bool mbushFormatNrKonfigTrupSipasIdTrupi(int idTrupi)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatKonfigTrupi(data.ktheFormatNrKonfigTrupSipasIdTrupi(idTrupi));
            data.Dispose();
            return mbush;
        }

        public bool mbushFormatNrKonfigTrupSipasIdKokaDheMonedha(int idKoka, int idMonedha)
        {
            clsDatabaseShare data = new clsDatabaseShare();
            bool mbush = mbushFormatKonfigTrupi(data.ktheFormatNrKonfigTrupSipasIdKokaDheMonedha(idKoka, idMonedha));
            data.Dispose();
            return mbush;
        }
        public static clsFormatKonfigTrup ktheFormatNumriMonedhe(clsFormatiKonfig formatNrPerKonfig, int idMonedha)
        {
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                return formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
            else
                return new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
        }
        /// <summary>
        /// Kthen formatin e monedhes sipas parametrave.
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idKonfig"></param>
        /// <param name="idMonedha"></param>
        /// <returns>nese idKonfig 0 ath kthehet konfigurimi default,nese idMonedha 0 ath merret monedha e ndermarrjes
        /// </returns>
        public static clsFormatKonfigTrup MerrFormatMonedheSipasNdermarrjesDheKonfig(int idNdermarrje, int idKonfig, int idMonedha)
        {
            if (idNdermarrje == 0) throw new MyException("IdNdermarrje nuk duhet te jete 0");
            clsFormatKonfigTrup formatMonedhe;
            if (idKonfig != 0)
            {
                if (idMonedha == 0)
                    idMonedha = clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje);
                var formatNrPerKonfig = new clsFormatiKonfig();
                formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfig);
                formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedha);
            }
            else
            {
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            }
            return formatMonedhe;
        }

        public static string KtheFormatStringSipasKonfigurimFormatNumri(int idNdermarrje, int idKonfig, LlojFusheFormatNumri llojformatfushe)
        {
            string formatNumri;
            var formatNr = MerrFormatMonedheSipasNdermarrjesDheKonfig(idNdermarrje, idKonfig, 0);
            switch (llojformatfushe)
            {
                case LlojFusheFormatNumri.Sasi:
                    formatNumri = formatNr.ShifraPasPresjesSasia.ToString();
                    break;
                case LlojFusheFormatNumri.Vlere:
                    formatNumri = formatNr.ShifraPasPresjesVlefta.ToString();
                    break;
                case LlojFusheFormatNumri.Cmim:
                    formatNumri = formatNr.ShifraPasPresjesCmimi.ToString();
                    break;
                case LlojFusheFormatNumri.Zbritje:
                    formatNumri = formatNr.ShifraPasPresjesZbritja.ToString();
                    break;
                default:
                    return string.Empty;
            }

            return string.IsNullOrEmpty(formatNumri) ? "n2" : $"n{formatNumri}";
        }

        #endregion

        #region Properties

        public int IdFormatKonfigTrup
        {
            get { return idFormatKonfigTrup; }
            set { idFormatKonfigTrup = value; }
        }

        public int IdFormatKonfig
        {
            get { return idFormatKonfig; }
            set { idFormatKonfig = value; }
        }

        public int IdMonedha
        {
            get { return idMonedha; }
            set { idMonedha = value; }
        }

        public int ShifraPasPresjesSasia
        {
            get { return shifraPasPresjesSasia; }
            set { shifraPasPresjesSasia = value; }
        }

        public int ShifraPasPresjesCmimi
        {
            get { return shifraPasPresjesCmimi; }
            set { shifraPasPresjesCmimi = value; }
        }

        public int ShifraPasPresjesVlefta
        {
            get { return shifraPasPresjesVlefta; }
            set { shifraPasPresjesVlefta = value; }
        }

        public int ShifraPasPresjesZbritja
        {
            get { return shifraPasPresjesZbritja; }
            set { shifraPasPresjesZbritja = value; }
        }

        public String FormatSasi
        {
            get { return formatSasia; }
            set { formatSasia = value; }
        }

        public String FormatCmimi
        {
            get { return formatCmimi; }
            set { formatCmimi = value; }
        }

        public String FormatVlefta
        {
            get { return formatVlefta; }
            set { formatVlefta = value; }
        }

        public String FormatZbritja
        {
            get { return formatZbritja; }
            set { formatZbritja = value; }
        }

        public String KodMonedhe
        {
            get { return kodMonedhe; }
            set { kodMonedhe = value; }
        }

        #endregion

        #region Metoda Internal

        internal bool mbushFormatKonfigTrupi(DataRow dbDataRowFormatKonfigTrup)
        {
            if (dbDataRowFormatKonfigTrup != null)
            {
                try
                {
                    int.TryParse(dbDataRowFormatKonfigTrup["IDFORMKONFIGTRUPI"].ToString(), out idFormatKonfigTrup);
                    int.TryParse(dbDataRowFormatKonfigTrup["IDFORMKONFIGKOKA"].ToString(), out idFormatKonfig);
                    int.TryParse(dbDataRowFormatKonfigTrup["IDMONEDHA"].ToString(), out idMonedha);
                    int.TryParse(dbDataRowFormatKonfigTrup["ShifraPasPresjesSasia"].ToString(), out shifraPasPresjesSasia);
                    int.TryParse(dbDataRowFormatKonfigTrup["ShifraPasPresjesCmimi"].ToString(), out shifraPasPresjesCmimi);
                    int.TryParse(dbDataRowFormatKonfigTrup["ShifraPasPresjesVlefta"].ToString(), out shifraPasPresjesVlefta);
                    int.TryParse(dbDataRowFormatKonfigTrup["ShifraPasPresjesZbritja"].ToString(), out shifraPasPresjesZbritja);
                    kodMonedhe = dbDataRowFormatKonfigTrup["KODIMONEDHA"].ToString();
                    //formatSasia = dbDataRowFormatKonfigTrup["FORMATSASI"].ToString();
                    //formatCmimi = dbDataRowFormatKonfigTrup["FORMATCMIMI"].ToString();
                    //formatVlefta = dbDataRowFormatKonfigTrup["FORMATVLEFTA"].ToString();
                    //formatZbritja = dbDataRowFormatKonfigTrup["FORMATZBRITJA"].ToString();
                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate cast-it!");
                }
                catch (Exception)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se formatit te konfigurimit nga db-ja!");
                }
            }
            else
                return false;
        }

        #endregion
    }
}