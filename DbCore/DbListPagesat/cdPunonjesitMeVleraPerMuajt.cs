using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;
using System.Collections.Concurrent;
namespace DbCore.DbListPagesat
{
    /// <summary>
    /// kjo perdor nje concurrent dictionary per te manipuluar te dhenat e muajve per punonjesit
    /// </summary>
    public class cdPunonjesitMeVleratPerMuajt
    {
        private const string VleratEMuajveSuffix = "colMuajiListpagesa";
        private ConcurrentDictionary<int, Dictionary<string, colKomponenteMuaji>> _cdVleratPerMuajt;
        private HttpSessionState _sessionState;
        public cdPunonjesitMeVleratPerMuajt(HttpSessionState session)
        {
            _cdVleratPerMuajt = new ConcurrentDictionary<int, Dictionary<string, colKomponenteMuaji>>(mySessionObjects.MerrNgaSession<Dictionary<int, Dictionary<string, colKomponenteMuaji>>>(session, VleratEMuajveSuffix) ?? new Dictionary<int, Dictionary<string, colKomponenteMuaji>>());
            _sessionState = session;
        }

        /// <summary>
        /// vendos ne sesion te gjithe vlerat qe ka dictionary
        /// </summary>
        public void RuajGjitheVleratColKomponente()
        {
            mySessionObjects.RuajNeSession(_sessionState, _cdVleratPerMuajt.ToDictionary(x=>x.Key,v=>v.Value), VleratEMuajveSuffix);
        }



        /// <summary>
        ///     ruan ne dictionary vlerat e punonjesit per komponente muaji
        /// </summary>
        /// <param name="session"></param>
        /// <param name="vlerat"></param>
        /// <param name="idPunonjesi"></param>
        public void RuajVleratENjePunonjesiNeSession(Dictionary<string, colKomponenteMuaji> vlerat, int idPunonjesi)
        {
            _cdVleratPerMuajt[idPunonjesi] = vlerat;
        }
        /// <summary>
        /// merr nga dictionary vlerat e ketij punonjesi
        /// </summary>
        /// <param name="idPunonjesi"></param>
        /// <returns></returns>
        public Dictionary<string, colKomponenteMuaji> MerrVleratENjePunonjesi(int idPunonjesi)
        {
            Dictionary<string, colKomponenteMuaji> objTmp;
            return _cdVleratPerMuajt.TryGetValue(idPunonjesi, out objTmp) ? objTmp : new Dictionary<string, colKomponenteMuaji>();
        }
        /// <summary>
        /// boshon sesionin nga vlerat e muajve
        /// </summary>
        /// <param name="session"></param>
        public static void HiqVleratNgaSessioni(HttpSessionState session)
        {
            mySessionObjects.hiqObjectNeSesion(session, VleratEMuajveSuffix);
        }


    }
}