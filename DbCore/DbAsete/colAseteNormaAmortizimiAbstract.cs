using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DbCore.DbAsete
{
    /// <summary>
    /// MODULI ASETE:
    /// Mban nje list objektesh clsAseteNormaAmortizimi (objekte per normat e amortizimit sipas artikujve dhe llojeve te amortizimit) ne modulin e Aseteve.
    /// Te dhenat merret nga tabela T_ASETE_LIDHJE_ARTIKULL_LLOJAMORT
    /// </summary>
    public abstract class colAseteNormaAmortizimiAbstract : List<clsAseteNormaAmortizimiAbstract>
    {
        public virtual enumObjekteAmortizimi objektiKod => enumObjekteAmortizimi.ABSTRACT;

        #region Konstruktore

        /// <summary>
        /// MODULI ASETE:
        /// Krijon nje list objektesh bosh te klases clsAseteNormaAmortizimi per normat e amortizimit sipas artikujve dhe llojeve te amortizimit.
        /// </summary>
        public colAseteNormaAmortizimiAbstract()
        {

        }

        public static colAseteNormaAmortizimiAbstract krijoInstance(enumObjekteAmortizimi objekti)
        {
            if (objekti == enumObjekteAmortizimi.REZERVA)
                return new colNormaAmortizimiRezerva();
            else
                return new colAseteNormaAmortizimi();
        }

        #endregion

        #region Metoda Publike

        public clsMesazh ruaj()
        {
            foreach (DbAsete.clsAseteNormaAmortizimiAbstract grup in this)
            {
                clsMesazh mesazh = grup.ruaj();
                if (!mesazh) return mesazh;
            }
            return new clsMesazh(true, IMBUtils.Messages.MessagesResource.Messages["labelRaportMesazhRuajtjaPerfundoiSukses"]); 
        }

        /// <summary>
        /// MODULI ASETE:
        /// Merr te gjithe normat e artikullit qe kerkojme pavaresisht nga standarti.
        /// </summary>
        /// <param name="idArtikulli">(int) Id e artikullit.</param>
        /// <param name="idndermarje">(int) Id e ndermarrjes.</param>
        /// <returns>Kthen True nese nuk ndodh asnje gabim gjate marrjes se normave te artikullit ose False ne te kundert.</returns>
        public bool merrArtikullNormaAmortizimiTeGjitha(int idArtikulli, int idndermarje, DateTime dtaktivizimi)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            bool pergjigja = mbushNormaAmortizimiList(moduliAsete.ktheArtikullNormaAmortizimiTeGjitha(idArtikulli, idndermarje,dtaktivizimi));
            moduliAsete.Dispose();
            return pergjigja;
        }
        public bool merrArtikullNormaAmortizimiFillestare(int idndermarje, DateTime data)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            bool pergjigja = mbushNormaAmortizimiList(moduliAsete.ktheArtikullNormaAmortizimiFillestare(idndermarje, data));
            moduliAsete.Dispose();
            return pergjigja;
        }
        public static DataTable ktheArtikujNormaAmortizimiDTExport(int idnderm, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract dbartikuj = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            DataTable tabela = dbartikuj.ktheArtikujNormaAmortizimiDTExport(idnderm);
            dbartikuj.Dispose();
            return tabela;
        }
        public bool ktheArtikullNormaAmortizimiSipasIdKodifikimit(int idkodifikimi, int idndermarje, DateTime data)
        {
            clsDatabazeAseteAbstract dbartikuj = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            bool pergjigja = mbushNormaAmortizimiList(dbartikuj.ktheArtikullNormaAmortizimiSipasIdKodifikimit(idkodifikimi, idndermarje, data));
            dbartikuj.Dispose();
            return pergjigja;
        }
        public bool ktheArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            bool pergjigja = mbushNormaAmortizimiList(moduliAsete.ktheArtikujNormaAmortizimiBrendaDatave(idArtikulli, idStandarti, dateFillimi, datePerfundimi));
            moduliAsete.Dispose();
            return pergjigja;
        }
        public bool ktheBoolArtikujNormaAmortizimiBrendaDatave(int idArtikulli, int idStandarti, DateTime dateFillimi, DateTime datePerfundimi)
        {
            clsDatabazeAseteAbstract moduliAsete = clsDatabazeAseteAbstract.krijoInstance(this.objektiKod);
            return moduliAsete.ktheBoolArtikujNormaAmortizimiBrendaDatave(idArtikulli, idStandarti, dateFillimi, datePerfundimi);
        }

        public static List<string> merrDataArtikujNorma(int idkoka, enumObjekteAmortizimi objektiKod)
        {
            clsDatabazeAseteAbstract db = clsDatabazeAseteAbstract.krijoInstance(objektiKod);
            List<string> list = new List<string>();
            DataTable dt = db.merrDataNdryshimiNormaAmortizimi(idkoka);
            db.Dispose();
            using (dt)
            {
                foreach (DataRow rreshti in dt.Rows)
                {
                    list.Add(DateTime.Parse(rreshti["DTAKTIVIZIMI"].ToString()).ToShortDateString());
                }
            }
            return list;

        }

        #endregion

        #region Metoda Abstrakte

        public abstract bool mbushNormaAmortizimiList(DataTable dt);

        #endregion
    }
}
