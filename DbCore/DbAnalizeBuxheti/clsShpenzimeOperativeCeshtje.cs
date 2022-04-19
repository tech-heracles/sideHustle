using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.DbAnalizeBuxheti
{
    public class clsShpenzimeOperativeCeshtje
    {

        /*
         [KOKAID]
        ,[]
        ,[]
        ,[]
        ,[]
        ,[]
       
         */
        public int KokaId { get; set; }
        public int NrCeshtjeParaardhes { get; set; }
        public int NrCeshtjeVitiAktual { get; set; }
        public int NrCeshtjeVitiPasardhes { get; set; }
        public int idNdermarrje { get; set; }
        public DateTime? DtDok { get; set; }

        public clsShpenzimeOperativeCeshtje() { }
        public static clsShpenzimeOperativeCeshtje Krijo(IDataRecord record)
        {
            return new clsShpenzimeOperativeCeshtje
            {

                KokaId=!Convert.IsDBNull(record["KOKAID"])?Convert.ToInt32(record["KOKAID"]):0,
                NrCeshtjeVitiAktual=!Convert.IsDBNull(record["NRCESHTJEVITIAKTUAL"])?Convert.ToInt32(record["NRCESHTJEVITIAKTUAL"]):0,
                NrCeshtjeParaardhes = !Convert.IsDBNull(record["NRCESHTJEPARAARDHES"]) ? Convert.ToInt32(record["NRCESHTJEPARAARDHES"]) : 0,
                NrCeshtjeVitiPasardhes = !Convert.IsDBNull(record["NRCESHTJEVITIPASARDHES"]) ? Convert.ToInt32(record["NRCESHTJEVITIPASARDHES"]) : 0,
                DtDok = !Convert.IsDBNull(record["DTDOK"]) ? Convert.ToDateTime(record["DTDOK"]) : (DateTime?)null,
                idNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0,
            };



        }
        public void Mbush(IDataRecord record)
        {
            KokaId = !Convert.IsDBNull(record["KOKAID"]) ? Convert.ToInt32(record["KOKAID"]) : 0;
            NrCeshtjeVitiAktual = !Convert.IsDBNull(record["NRCESHTJEVITIAKTUAL"]) ? Convert.ToInt32(record["NRCESHTJEVITIAKTUAL"]) : 0;
            NrCeshtjeParaardhes = !Convert.IsDBNull(record["NRCESHTJEPARAARDHES"]) ? Convert.ToInt32(record["NRCESHTJEPARAARDHES"]) : 0;
            NrCeshtjeVitiPasardhes = !Convert.IsDBNull(record["NRCESHTJEVITIPASARDHES"]) ? Convert.ToInt32(record["NRCESHTJEVITIPASARDHES"]) : 0;
            DtDok = !Convert.IsDBNull(record["DTDOK"]) ? Convert.ToDateTime(record["DTDOK"]) : (DateTime?)null;
            idNdermarrje = !Convert.IsDBNull(record["IDNDERMARRJE"]) ? Convert.ToInt32(record["IDNDERMARRJE"]) : 0;
        }

        public clsMesazh Ruaj()
        {

            //int kokaID = -1;
            clsDatabaseAnalizeBuxheti dbAB = new clsDatabaseAnalizeBuxheti();
            dbAB.beginTransaksion();
            clsMesazh mesazh = dbAB.RuajShpenzimeOperativeCeshtje(KokaId, DtDok, idNdermarrje, NrCeshtjeParaardhes, NrCeshtjeVitiAktual, NrCeshtjeVitiPasardhes);
            if (!mesazh.Status)
                dbAB.rollbackTransaksion();
            else
                dbAB.commitTransaksion();  //    KokaId = kokaID;

            return mesazh;
        }
        //public static int MerrVlerenDefaultTeKokes(clsDatabaseAnalizeBuxheti dbAB,int idNdermarrje)
        //{
        //    int kokaID = -1;
        //    clsMesazh mesazh = dbAB.KrijoKokenDefaultDheMerrIdKoke(idNdermarrje);
        //}
        public clsShpenzimeOperativeCeshtje(int kokaID)
        {
           using(clsDatabaseAnalizeBuxheti dbAB=new clsDatabaseAnalizeBuxheti())
           {
               dbAB.MerrShpenzimeOperativeCeshtje(this, kokaID);
           }
        }
    }

}
