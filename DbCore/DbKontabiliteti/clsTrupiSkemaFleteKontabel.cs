using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DbCore.DbKontabiliteti
{
    /// <remarks>
    /// Kjo eshte klasa qe sherben per objektet qe perfaqesojne nje element te trupit te skemes se fletes kontabel
    ///  (Te dhenat  merren nga tabela : T_TRUPISKEMAFLETEKONTABEL)
    ///</remarks>
    public  class clsTrupiSkemaFleteKontabel
    {
        #region Atribute

        private int idTrupiSkemaFK;
        private int idKokaSkemaFK;
        private int idLlogari;
        private string nrLlogari;
        private string emerLlogari;
        private string pershkrimi;
        private int idMonedha;
        private string kodMonedha;
        private double kursi;
        private double vleftaDebi;
        private double vleftaKredi;
        private double vleftaDebiMon;
        private double vleftaKrediMon;
        private DataRow rreshti;
       
        #endregion

        #region Konstruktoret
        
        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTrupiSkemaFleteKontabel( int idTrupiSkemaFK, int idKokaSkemaFK, int idLlogari, string pershkrimi, int idmonedha, double kursi, double vleftadebi, double vleftakredi, double vleftadebimon, double vleftakredimon)
        {
            this.idTrupiSkemaFK = idTrupiSkemaFK;
            this.idKokaSkemaFK = idKokaSkemaFK;
            this.idLlogari  = idLlogari;
            this.pershkrimi = pershkrimi;
            this.idMonedha = idmonedha;
            this.kursi = kursi;
            this.vleftaDebi = vleftadebi;
            this.vleftaKredi = vleftakredi;
            this.vleftaDebiMon = vleftadebimon;
            this.vleftaKrediMon = vleftakredimon;
           
          
        }

        /// <summary>
        /// Konstruktor i klases
        /// </summary>
        public clsTrupiSkemaFleteKontabel()
        {
        }

        public clsTrupiSkemaFleteKontabel(DataRow rreshti)
        {
            
            mbushTrupFletSkemKont(rreshti);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Kthen/vendos ID-ne qe gjenerohet automatikisht
        /// </summary>
        public int IdTrupiSkemaFK
        {
            get
            {
                return idTrupiSkemaFK ;
            }
            set
            {
                idTrupiSkemaFK  = value;
            }
        }

        /// <summary>
        /// Kthen/vendos ID-ne e kokes te ciles i perket trupi
        /// </summary>
        public int IdKokaSkemaFK
        {
            get
            {
                return idKokaSkemaFK;
            }
            set
            {
                idKokaSkemaFK = value;
            }
        }

       

        /// <summary>
        /// Kthen/vendos ID-ne e llogarise
        /// </summary>
        public int IdLlogari
        {
            get
            {
                return idLlogari;
            }
            set
            {
                idLlogari = value;
            }
        }
        /// <summary>
        /// Kthen/Vendos emrin e llogarise
        /// </summary>
        public string EmerLlogari
        {
            get
            {
                return emerLlogari;
            }
            set
            {
                emerLlogari = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos numrin e llogarise
        /// </summary>
        public string NrLlogari
        {
            get
            {
                return nrLlogari;
            }
            set
            {
                nrLlogari = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos pershkrimin e trupit
        /// </summary>
        public string Pershkrimi
        {
            get
            {
                return pershkrimi;
            }
            set
            {
                pershkrimi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos ID-ne e monedhes
        /// </summary>
        public int IdMonedha
        {
            get
            {
                return idMonedha;
            }
            set
            {
                idMonedha = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kusin e monedhes
        /// </summary>
        public double Kursi
        {
            get
            {
                return kursi;
            }
            set
            {
                kursi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleften me te cilen preket llogaria ne debi
        /// </summary>
        public double VleftaDebi
        {
            get
            {
                return vleftaDebi;
            }
            set
            {
                vleftaDebi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleften me te cilen preket llogaria ne kredi
        /// </summary>
        public double VleftaKredi
        {
            get
            {
                return vleftaKredi;
            }
            set
            {
                vleftaKredi = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleren ne monedhe baze me te cilen preket llogaria ne debi
        /// </summary>
        public double VleftaDebiMon
        {
            get
            {
                return vleftaDebiMon;
            }
            set
            {
                vleftaDebiMon = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos vleren ne monedhe baze me te cilen preket llogaria ne kredi
        /// </summary>
        public double VleftaKrediMon
        {
            get
            {
                return vleftaKrediMon;
            }
            set
            {
                vleftaKrediMon = value;
            }
        }

        /// <summary>
        /// Kthen/Vendos kodin e monedhes
        /// </summary>
        public string KodMonedha
        {
            get
            {
                return kodMonedha;
            }
            set
            {
                kodMonedha = value;
            }
        }
     
        #endregion

        #region Metoda Internal

        /// <summary>
        /// mbush trupin e skemes se fleteve kontabel nga databaza
        /// </summary>
        /// <param name="dbDataRowTrupFletSkemKont">datarow qe duhet mbushur nga db</param>
        /// <returns>kthen true nese mbushja kryhet me sukses, ne te kundert false</returns>
        internal bool mbushTrupFletSkemKont(DataRow dbDataRowTrupFletSkemKont)
        {
            if (dbDataRowTrupFletSkemKont != null)
            {
                try
                {
                    int.TryParse(dbDataRowTrupFletSkemKont["IDTRUPISKEMAFK"].ToString(), out idTrupiSkemaFK);
                    int.TryParse(dbDataRowTrupFletSkemKont["IDKOKASKEMAFK"].ToString(), out idKokaSkemaFK);
                    int.TryParse(dbDataRowTrupFletSkemKont["IDLLOGARI"].ToString(), out idLlogari);
                    pershkrimi = dbDataRowTrupFletSkemKont["PERSHKRIMI"].ToString();
                    idMonedha = int.Parse(dbDataRowTrupFletSkemKont["IDMONEDHA"].ToString());
                    kursi = double.Parse(dbDataRowTrupFletSkemKont["KURSI"].ToString());
                    vleftaDebi = double.Parse(dbDataRowTrupFletSkemKont["VLEFTADEBI"].ToString());
                    vleftaKredi = double.Parse(dbDataRowTrupFletSkemKont["VLEFTAKREDI"].ToString());
                    nrLlogari = dbDataRowTrupFletSkemKont["nrllogari"].ToString();
                    emerLlogari = dbDataRowTrupFletSkemKont["EmerLlogari"].ToString();
                    kodMonedha = dbDataRowTrupFletSkemKont["monedhakod"].ToString();
                    vleftaDebiMon = double.Parse(dbDataRowTrupFletSkemKont["VLEFTADEBIMON"].ToString());
                    vleftaKrediMon = double.Parse(dbDataRowTrupFletSkemKont["VLEFTAKREDIMON"].ToString());

                    return true;
                }
                catch (InvalidCastException)
                {
                    throw new Exception("ERROR: Gabim gjate marrjes se trupit te skemes flete kontabel nga db-ja");
                }
            }
            else
                return false;
        }

        #endregion
    }
}

