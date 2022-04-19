using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;

namespace DbCore.IMBUtils
{
    public static class FileReader
    {
        public static DataTable CsvReaderMeHeader(Stream fileContent)
        {
            var table = new DataTable();
            using (var csvReader = new StreamReader(fileContent))
            using (var parser = new CsvTextFieldParser(csvReader))
            {
                if (!parser.EndOfData)
                {
                    var emrat = parser.ReadFields();
                    foreach (var emer in emrat)
                        table.Columns.Add(emer);

                    while (!parser.EndOfData)
                    {
                        table.Rows.Add(parser.ReadFields());
                    }
                }
            }

            return table;
        }



        /// <summary>
        /// lexon sdedarin e ngarkuar per import
        /// </summary>
        /// <param name="f">file i ngarkuar</param>
        /// <param name="simboliNdares"> simboli ndares per cdo kolone</param>
        /// <param name="nrFushave">nr kolonave qe permban skedari</param>
        /// <returns></returns>
        public static DataTable FileReaderSerialeUnike(HttpPostedFile f, string simboliNdares, List<string> Fushat, bool meEmertimKolonash)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            using (var sr = new StreamReader(f.InputStream))
            {
                string[] vlerat;
                char[] karakterindares = "".ToCharArray();
                karakterindares = simboliNdares.ToCharArray();
                if (sr.EndOfStream) return table;
                if (!meEmertimKolonash)
                {
                    foreach (var fushe in Fushat)
                        table.Columns.Add(fushe);
                }
                else
                {

                    vlerat = lexoRresht(sr, karakterindares, Fushat.Count, false);
                    foreach (var vlera in vlerat)
                    {
                        if (table.Columns.Contains(vlera.Trim()))
                            throw new Exception("File i importit permban 2 kolona me te njejtin emer");
                        if (Fushat.Exists(fushe => fushe == vlera.Trim()))
                            table.Columns.Add(vlera.Trim());
                        else
                            throw new MyException("File i importit nuk perputhet me konfigurimin");
                    }
                }
                while (!sr.EndOfStream)
                {
                    vlerat = lexoRresht(sr, karakterindares, Fushat.Count, true);
                    if (!(vlerat.Length == 1 && string.IsNullOrWhiteSpace(vlerat[0])))
                        table.Rows.Add(vlerat);
                }


            }

            return table;
        }

        static string[] lexoRresht(StreamReader sr, char[] karakterindares, int gjatesiaENevojshme, bool mundTeJeteRreshtBosh)
        {
            string[] vlerat = sr.ReadLine().Split(karakterindares);
            if (mundTeJeteRreshtBosh && vlerat.Length == 1 && string.IsNullOrWhiteSpace(vlerat[0]))
                return vlerat;
            if (vlerat.Length != gjatesiaENevojshme)
            {
                throw new MyException("File i importit nuk perputhet me konfigurimin");
            }

            return vlerat;
        }
    }
}
