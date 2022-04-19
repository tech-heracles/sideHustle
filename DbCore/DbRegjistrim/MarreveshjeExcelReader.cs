using DbCore.DbKontabiliteti;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DbCore.DbRegjistrim
{
    public class MarreveshjeExcelReader
    {
        private string _agreementID;
        private colKlienteFurnitore _klientet;
        private double _vleraBuxhetit;
        private clsKokaShitje _kokashitje;
        private int _idNder;
        private DateTime _dtdok;

        public MarreveshjeExcelReader(int idNder, DateTime dtdok)
        {
            _idNder = idNder;
            _dtdok = dtdok;
            _kokashitje = new clsKokaShitje();
            _klientet = new colKlienteFurnitore();
        }

        public string AgreementID { get => _agreementID; set => _agreementID = value; }
        public colKlienteFurnitore Klientet { get => _klientet; set => _klientet = value; }
        public double VleraBuxhetit { get => _vleraBuxhetit; set => _vleraBuxhetit = value; }
        public clsKokaShitje Kokashitje { get => _kokashitje; set => _kokashitje = value; }

        public void LexoFileMarreveshje()
        {
            var dictionarySkedare = CacheLayer.GlobalCacheManager.MySessionCache.Get<Dictionary<string, HttpPostedFile>>("SkedareMarreveshje");
            if (dictionarySkedare == null)
                throw new MyException(MessagesResource.Messages["msgNukKaFileNgarkuar"]);
            
            HttpPostedFile dic = dictionarySkedare.Last().Value;
            CacheLayer.GlobalCacheManager.MySessionCache.Remove("SkedareMarreveshje");
            string kodeklientesh = "";
            using (ExcelPackage ep = new ExcelPackage(dic.InputStream))
            {
                ExcelWorkbook eW = ep.Workbook;
                ExcelWorksheet sheetAgreementInfo = eW.Worksheets["S15 Agreement Info"];
                ExcelWorksheet sheethandset = eW.Worksheets["S15 Handset"];
                ExcelWorksheet sheetAccount = eW.Worksheets["S15 Account"];
                if (sheetAgreementInfo == null || sheetAccount == null   || sheetAgreementInfo.Cells[4, 11].Value == null || sheethandset.Cells[4, 6].Value == null || string.IsNullOrEmpty(sheetAgreementInfo.Cells[4, 11].Value.ToString().Trim()) || string.IsNullOrEmpty(sheethandset.Cells[4, 6].Value.ToString().Trim()))
                    throw new MyException(MessagesResource.Messages["msgFormatFileGabim"]);

                _agreementID = sheetAgreementInfo.Cells[4, 11].Value.ToString();
                _vleraBuxhetit = Convert.ToDouble(sheethandset.Cells[4, 6].Value);
                int i = 2;
                while (sheetAccount.Cells[i, 1].Value != null && sheetAccount.Cells[i, 1].Value.ToString().Trim() != "")
                {
                    kodeklientesh += sheetAccount.Cells[i, 1].Value.ToString() + ",";
                    i++;
                }
                if (String.IsNullOrEmpty(kodeklientesh))
                    throw new MyException(MessagesResource.Messages["msgFormatFileGabim"]);

                kodeklientesh = kodeklientesh.Remove(kodeklientesh.Length - 1, 1);//hiq presjen ne fund
            }
            ValidoMarreveshje(kodeklientesh);
        }

        private void ValidoMarreveshje(string kodeklientesh)
        {
            _kokashitje.KtheDokMarreveshjeNgaIdMarreveshje(_agreementID);
            _klientet = colKlienteFurnitore.KontrolloEkzistenceKlientFurnitoreVartes(kodeklientesh, _idNder);
            string klientMeMarreveshje = clsKlientFurnitor.KontrolloKlientMeMarreveshjeAktive(kodeklientesh, _dtdok, _agreementID);
            if (!String.IsNullOrEmpty(klientMeMarreveshje))
                throw new MyException(MessagesResource.Messages["msgKlientiKaMarreveshjeAktive"].Replace("#XX", klientMeMarreveshje));
            if (_kokashitje.IdShitjeKoka > 0) // kontrollohet nese klientet ne file jane te njejte me klientet vartes qe ndodhen ne marrveshjen qe po modifikohet
            {
                List<int> idshitje = _kokashitje.ColKlienteFurnitoreVartes.OrderBy(x => x.IdKlientFurnitor).Select(x => x.IdKlientFurnitor).ToList();
                List<int> idfile = _klientet.OrderBy(x => x.IdKlientFurnitor).Select(x => x.IdKlientFurnitor).ToList();
                if (!idshitje.SequenceEqual(idfile))
                    throw new MyException(MessagesResource.Messages["msgKlienteTeNdryshem"].Replace("#xx", _agreementID));

            }
        }
    }
}
