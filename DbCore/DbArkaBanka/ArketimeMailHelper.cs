using System;
using System.Data;
using System.Web.SessionState;

namespace DbCore.DbArkaBanka
{
    public class ArketimeMailHelper
    {
        public static (clsMesazh mesazh, clsMesazh informim) DergoFatureMeEmail(string ids, int idGjuha, int idPerdoruesi, int idNdermarrjeVit, int idNdermarrje)
        {
            DataTable dt;
            clsMesazh mesazh = new clsMesazh(true, "");
            clsMesazh informim = new clsMesazh(TipMesazhi.Gabim, "");
            if (ids == "")
                return (new clsMesazh(false, "Nuk mund te merret id e dokumentit."), new clsMesazh(TipMesazhi.Informim, ""));

            using (clsDatabaseArkaBanka db = new clsDatabaseArkaBanka())
                dt = db.merrKlientFurnitorEmalPerDok(ids);

            if (dt.Rows.Count == 0)
                return (new clsMesazh(false, "Emaili nuk u dergua pasi nuk ka klient/furnitor ne dokument! <br>"), informim);

            if (dt.Rows[0]["KLIENTPAEMAIL"].ToString() != "")
                informim = new clsMesazh(TipMesazhi.Gabim, $"Emaili nuk mund te dergohet per klientet:{dt.Rows[0]["KLIENTPAEMAIL"].ToString()} pasi mungon adresa e tyre e emailit!");

            foreach (DataRow r in dt.Rows)
            {
                if (r["EMAILS"].ToString() == "")
                    continue;
                clsMesazh m = EmailComposer.dergoEmailFaturenTeKlietFurnitoret(idGjuha, idPerdoruesi, idNdermarrjeVit, idNdermarrje, Convert.ToInt32(r["IDDOK"].ToString()), r["NRDOK"].ToString(), r["DTDOK"].ToString(), Convert.ToInt32(r["IDRAP"].ToString()), Convert.ToInt32(r["IDDESIGN"].ToString()), r["EMAILS"].ToString().Split(','), r["LLOJDOK"].ToString(), r["LLOJDOKPERSHKRIMI"].ToString());
                if (!mesazh.Status && !informim.PershkrimMesazhi.Contains(m.PershkrimMesazhi))
                    informim.PershkrimMesazhi += m.PershkrimMesazhi + "<br>";
                else if (mesazh.Status && !mesazh.PershkrimMesazhi.Contains(m.PershkrimMesazhi))
                    mesazh.PershkrimMesazhi = m.PershkrimMesazhi;
            }
            return(mesazh, informim);
        }

    }
}
