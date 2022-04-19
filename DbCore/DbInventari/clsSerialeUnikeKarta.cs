using System;
using System.Data;

namespace DbCore.DbInventari
{
    public class clsSerialeUnikeKarta : clsSerialeUnikeMagazina
    {
        #region Atribute

        private string userCode;
        private string _serialiDytesor;
        private string _cardSerialNo;
        private string _phoneSerialNo;
        private string _userCode;

        #endregion

        public string Airtime { get; set; }

        #region Konstruktore

        public clsSerialeUnikeKarta(int idNdermarrje, int idKategori, int idFormati, string serialiKryesore, clsArtikulli artikull, clsArtikulli artSet, string serialiDytesor, string cardSerialNo, string phoneSerialNo, string userCode, string airtime, bool shfaqSerialKryesor, int idMag) :
           base(idNdermarrje, idKategori, idFormati, serialiKryesore, artikull, artSet, shfaqSerialKryesor, idMag)
        {
            _serialiDytesor = serialiDytesor;
            _cardSerialNo = cardSerialNo;
            _phoneSerialNo = phoneSerialNo;
            _userCode = userCode;
            Airtime = airtime;
            Sasia = 1;
        }

        public clsSerialeUnikeKarta(IDataRecord record)
        {
            Mbush(record);
        }

        #endregion

        #region Metoda Publike

        public override clsMesazh Ruaj()
        {
            using (var serialMagazineDb = new clsDatabaseInventari())
            {
                var mesazh = serialMagazineDb.ruajSerialeUnikeMagazine(out var id, IdTrupiMagazine, IdKokaMagazine, IdLlojDokumentMagazine, IdKategoriSeriali, IdFormatSeriali, IdArtikulli, IdTVSH, Cmimi,
                    Sasia, IdSeti, SerialiKryesore, _serialiDytesor, _cardSerialNo, _phoneSerialNo, _userCode, Airtime);
                Id = id;
                return mesazh;
            }
        }

        internal override void shtoParametratKokaTrup(int idTrupi, int idKoka, int idLlojDokumentiMagazine, float cmimi)
        {
            IdTrupiMagazine = idTrupi;
            IdKokaMagazine = idKoka;
            IdLlojDokumentMagazine = idLlojDokumentiMagazine;
            Cmimi = cmimi;
        }

        public sealed override void Mbush(IDataRecord record)
        {
            base.Mbush(record);
            _cardSerialNo = record["CARDSERIALNO"].ToString();
            _phoneSerialNo = record["PHONESERIALNO"].ToString();
            _userCode = record["USERCODE"].ToString();
            Airtime = record["AIRTIME"].ToString();
            _serialiDytesor = record["SERIALI_DYTESOR"].ToString();

        }
        internal override DataRow FillDataRow(DataRow dtRow)
        {
            base.FillDataRow(dtRow);
            dtRow["CARDSERIALNO"] = _cardSerialNo;
            dtRow["PHONESERIALNO"] = _phoneSerialNo;
            dtRow["USERCODE"] = _userCode;
            dtRow["AIRTIME"] = Airtime == "" ? 0 : Convert.ToInt32(Airtime);
            dtRow["SERIALI_DYTESOR"] = _serialiDytesor;
            return dtRow;
        }

        public override bool EshteSerialINjejteDytesor(string seriali)
        {
            return !string.IsNullOrEmpty(_serialiDytesor) && !string.IsNullOrEmpty(seriali) && _serialiDytesor == seriali;
        }

        public override string MerrSerialDytesor()
        {
            return _serialiDytesor;
        }

        #endregion
    }
}
