using System;
using System.Linq;
using DbCore.DbShare;

namespace DbCore.DbAdmin
{
    public class colGridaKoka : System.Collections.Generic.List<clsGridaKoka>
    {
        #region Metoda Publike

        public new clsGridaKoka this[int index] => base[index];

        public static colGridaKoka KrijoColGridaKoka(colGridaTrupi colTrupi, int idNdermarrja, int idKomponente, int idKonfigurimi, int idGjuha, out colGridaTrupi oColTrupiLupat)
        {
            var colKokat = new colGridaKoka();
            oColTrupiLupat = new colGridaTrupi();
            var idKokat = colTrupi.GroupBy(p => p.IdKoka);

            foreach (var koka in idKokat)
            {
                var gridaKoka = new clsGridaKoka
                {
                    EmriGridaKoka = colTrupi.First(item => item.IdKoka == Convert.ToInt32(koka.Key)).GridKokaEmri,
                    IdKomponente = idKomponente
                };
                var trupat = colTrupi.FindAll(item => item.IdKoka == Convert.ToInt32(koka.Key));
                foreach (var tr in trupat)
                {
                    if (tr.KodLupa == "Pa Lupe" || tr.KodLupa == "0" || tr.KodLupa == "")
                        tr.IdKonfigLupaMultiple = string.Empty;
                    else if (!tr.KodLupa.Contains('-'))
                    {
                        var idKonfigAmb = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(tr.KodLupa, idNdermarrja);
                        if (idKonfigAmb <= 0)
                            throw new MyException("Lloji i konfigurimit " + tr.KodLupa + " nuk ekziston!");
                        tr.IdKonfigAmbjenteLupa = idKonfigAmb;
                        tr.IdKonfigLupaMultiple = string.Empty;
                    }
                    else
                        oColTrupiLupat.AddRange(colGridaTrupi.KriColGridaTrupi(tr.KodLupa, tr.KodiTrupi, idNdermarrja));

                    gridaKoka.OColGridaTrupi.Add(tr);
                }

                if (!gridaKoka.OColGridaTrupi.Any())
                {
                    var colGridaTrupi = new colGridaTrupi();
                    colGridaTrupi.mbushGrideTrupinSipasEmerGride(idKonfigurimi, gridaKoka.EmriGridaKoka, idGjuha);
                    gridaKoka.OColGridaTrupi = colGridaTrupi;
                }

                gridaKoka.OColGridaTrupi.RregulloRenditje();
                colKokat.Add(gridaKoka);
            }

            return colKokat;
        }

        #endregion
    }
}
