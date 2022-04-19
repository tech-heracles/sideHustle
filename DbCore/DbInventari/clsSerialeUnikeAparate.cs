using System.Data;

namespace DbCore.DbInventari
{
    public class clsSerialeUnikeAparate : clsSerialeUnikeMagazina
    {

        #region Konstruktore

        public clsSerialeUnikeAparate() : base()
        {

        }

        public clsSerialeUnikeAparate(int idNdermarrje, int idKategori, int idFormati, string artikulli, string artikullSet, string serialiKryesore) : base(idNdermarrje, idKategori, idFormati, artikulli, artikullSet, serialiKryesore)
        {
            Sasia = 1;
        }

        public clsSerialeUnikeAparate(int idNdermarrje, int idKategori, int idFormati, string serialiKryesore, clsArtikulli artikull, clsArtikulli artSet, bool shfaqSerialKryesor, int idMag) : base(idNdermarrje, idKategori, idFormati, serialiKryesore, artikull, artSet, shfaqSerialKryesor, idMag)
        {
            Sasia = 1;
        }

        public clsSerialeUnikeAparate(IDataRecord record) 
        {
            Mbush(record);
        }


        #endregion

        #region Metoda Publike

        public override clsMesazh Ruaj()
        {
            clsMesazh mesazh = new MesazhGabimi();
            int id = 0;
            using (clsDatabaseInventari serialMagazineDb = new clsDatabaseInventari())
            {
                mesazh = serialMagazineDb.ruajSerialeUnikeMagazine(out id, IdTrupiMagazine, IdKokaMagazine, IdLlojDokumentMagazine, IdKategoriSeriali, IdFormatSeriali, IdArtikulli, IdTVSH, Cmimi, Sasia, IdSeti, SerialiKryesore, ShfaqSerialKryesorNeGride);
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

        public override void Mbush(IDataRecord record)
        {
            base.Mbush(record);
        }


        internal override DataRow FillDataRow(DataRow dtRow)
        {
            base.FillDataRow(dtRow);
            return dtRow;
        }

        #endregion
    }
}
