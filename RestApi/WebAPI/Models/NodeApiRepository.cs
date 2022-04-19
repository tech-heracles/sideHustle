using DbCore.DbShare;
using DbCore.DbIntegrime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbCore;

namespace RestApi.WebAPI.Models
{
    public class NodeApiRepository
    {
        public static object GetLastModifiedDateMessage()
        {
            return new { dateModifikimi = clsNjoftime.MerrDateFunditNJoftime() };
        }

        public static clsMesazh RuajMesazhet(colNjoftime njoftime)
        {
            
            if (njoftime == null || njoftime.Count == 0) return new MesazhInformimi("Nuk ka asgje per te ruajtur!");
            return njoftime.Ruaj();
        }

        public static clsMesazh RuajMesazhetPerdoruesHistorik(clsNjoftimePerdorues njoftimeperdorues)
        {

            if (njoftimeperdorues == null) return new MesazhInformimi("Nuk ka asgje per te ruajtur!");
            return njoftimeperdorues.Ruaj();
        }

        public static colJobAutomatike MerrJobetAutomatike()
        {
            return  new colJobAutomatike();
        }

        public static colJobAutomatikeParametra MerrJobAutomatikeParametra(int idskeduleri)
        {
            return new colJobAutomatikeParametra(idskeduleri);
        }

        public static clsMesazh ModifikoJobAutomatike(clsJobAutomatike paramJobAutomatike)
        {
            return  paramJobAutomatike.Modifiko();
        }

    }
}
