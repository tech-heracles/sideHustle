using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Types;

namespace DbCore.DbListPagesat
{
    public class clsKomponenteMuaji
    {
        #region Atribute

        private int id;
        private int idKompListPagese;
        private int muaji;
        private decimal vleraParam;
        private decimal vlera;
        private string kodKomponente;
        private string komponente;
        private int njesia;
        private string emerparam;
        private int tipi;
        private int viti;
        #endregion

        #region Konstruktor

        public clsKomponenteMuaji(int id, int idkomp, int muaji, int viti, decimal vleraparam, decimal vlera, string kodkomp, string komp, string param, int njesia, int tipi)
        {
            this.id = id;
            idKompListPagese = idkomp;
            this.muaji = muaji;
            this.viti = viti;
            vleraParam = vleraparam;
            this.vlera = vlera;
            this.kodKomponente = kodkomp;
            this.komponente = komp;
            this.emerparam = param;
            this.njesia = njesia;
            this.tipi = tipi;
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsKomponenteMuaji()
        {
        }



        /// <summary>
        /// mbush objektin sipas te dhenat e grides
        /// </summary>
        /// <param name="rreshtDokuKlient"></param>
        public clsKomponenteMuaji(Dictionary<string, object> rreshtDokuKlient)
        {
            idKompListPagese = int.Parse(rreshtDokuKlient["IdKompListPagese"].ToString());
            viti = int.Parse(rreshtDokuKlient["Viti"].ToString());
            muaji = int.Parse(rreshtDokuKlient["Muaji"].ToString());
            VleraParam = decimal.Parse(rreshtDokuKlient["VleraParam"].ToString());
            Vlera = decimal.Parse(rreshtDokuKlient["Vlera"].ToString());


        }
        #endregion

        #region Properties

        /// <summary>
        /// Kthen/Vendos ID-ne qe gjenerohet automatikisht.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e trupit
        /// </summary>
        public int IdKompListPagese
        {
            get { return idKompListPagese; }
            set { idKompListPagese = value; }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e komponentes se pages
        /// </summary>
        public int Muaji
        {
            get
            {
                return muaji;
            }
            set
            {
                muaji = value;
            }
        }

        public int Viti
        {
            get
            {
                return viti;
            }
            set
            {
                viti = value;
            }
        }
        /// <summary>
        /// kthen/vendos vleren e parametrit
        /// </summary>
        public decimal VleraParam
        {
            get
            {
                return vleraParam;
            }
            set
            {
                vleraParam = value;
            }
        }

        /// <summary>
        /// kthen/vendos vleren
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
        /// kthen kodin e komponentes
        /// </summary>
        public string KodKomponente
        {
            get
            {
                return kodKomponente;
            }
        }

        /// <summary>
        /// emri i komponentes
        /// </summary>
        public string Komponente
        {
            get
            {
                return komponente;
            }
        }

        /// <summary>
        /// njesia
        /// </summary>
        public int Njesia
        {
            get
            {
                return njesia;
            }
        }

        /// <summary>
        /// emer parametri
        /// </summary>
        public string Emerparam
        {
            get
            {
                return emerparam;
            }
        }

        /// <summary>
        /// tipi i komponentes
        /// </summary>
        public int Tipi
        {
            get
            {
                return tipi;
            }
        }
        #endregion

        #region Metoda Publike

        public static IEnumerable<clsKomponenteMuaji> MerrListKomponenteshMuaji(List<int> idKomponentesh)
        {
            return new clsDatabazeListPagesa().MerrKomponenteMuajiSipasKompLpIds(idKomponentesh);
        }
        /// <summary>
        /// Ruan objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.ruajPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese ruajtja perfundoi me sukses</returns>
        public bool ruaj()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            int id;
            clsMesazh u_ruajt = data.ruajKompMuaji(out id, idKompListPagese, muaji, viti, vleraParam, vlera);
            data.Dispose();
            return u_ruajt.Status;

        }

        /// <summary>
        /// Modifikon objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.modifikoPagaShtesa"/>
        /// </summary>
        /// <returns>Kthen true nese modifikimi perfundoi me sukses</returns>
        public bool modifiko()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_modifikua = data.modifikoKompMuaji(id, idKompListPagese, muaji, viti, vleraParam, vlera);
            data.Dispose();
            return u_modifikua.Status;
        }

        /// <summary>
        /// Fshin objektin e paga shtesa ne tabelen perkatese ne databaze.Therret funksionin
        /// <see cref="clsDatabazeListPagesa.fshiKomponenteListPagese"/>
        /// </summary>
        /// <returns>Kthen true nese fshirja perfundoi me sukses</returns>
        public bool fshi()
        {
            clsDatabazeListPagesa data = new clsDatabazeListPagesa();
            clsMesazh u_fshi = data.fshiKompMuaji(idKompListPagese);
            data.Dispose();
            return u_fshi.Status;
        }

        public static clsKomponenteMuaji Krijo(IDataRecord record)
        {
            clsKomponenteMuaji kompMuaji = new clsKomponenteMuaji();
            kompMuaji.mbushKomponenteMuaji(record);
            return kompMuaji;
        }

        public clsKomponenteMuaji Clone()
        {
            return new clsKomponenteMuaji(id, idKompListPagese, muaji, viti, vleraParam, vlera, kodKomponente, komponente, emerparam, njesia, tipi);
        }


        #endregion

        #region Metoda Internal



        /// <summary>
        /// mbush skema sigurimi nga databaza
        /// </summary>
        /// <param name="record">datarecord qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal void mbushKomponenteMuaji(IDataRecord record)
        {

            try
            {
                Converter.ParseExact(record["ID"].ToString(), out id, "id");
                Converter.ParseExact(record["IDKOMPLISTPAGESE"].ToString(), out idKompListPagese, "idKompListPagese");
                Converter.Parse(record["MUAJI"].ToString(), out muaji, "muaji");
                Converter.Parse(record["VITI"].ToString(), out viti, "viti");
                Converter.ParseExact(record["VLERAPARAM"].ToString(), out vleraParam, "vleraParam");
                Converter.ParseExact(record["VLERA"].ToString(), out vlera, "vlera");
                komponente = record["Komponente"].ToString();
                kodKomponente = record["KodKomponente"].ToString();
                Converter.Parse(record["Njesia"].ToString(), out njesia, "njesia");
                emerparam = record["EmerParam"].ToString();
                Converter.Parse(record["Tipi"].ToString(), out tipi, "tipi");

            }
            catch (MyWarnException warn)
            {
                ImbLogger.Warn($"gabim ne mbushjen e komponenteve te listpageses per:{idKompListPagese} {warn.Message}");
            }
            catch (MyException myex)
            {
                throw new MyException( "gabim ne mbushjen e komponenteve te listpageses per :{0} {1}", idKompListPagese, myex.Message);
            }

        }

        #endregion
    }
}

