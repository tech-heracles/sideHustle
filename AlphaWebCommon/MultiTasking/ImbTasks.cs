using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Fasterflect;

namespace DbCore.IMBUtils.MultiTasking
{
    public class ImbTasks
    {
        /// <summary>
        /// Kjo metode sherben per te ndare ne disa thirrje ne paralel nje metode qe therret nje stored procedure dhe qe ka si parameter nje list me id,ose list me objekte
        /// thirrja ne paralel mundesohet duke copezuar listen ne disa pjese ne varesi te numrit te taskeve qe eshte vendosur te krijohen
        /// </summary>
        /// <typeparam name="TKey">Tipi i Key te dictionary-it</typeparam>
        /// <typeparam name="TListOfParam">Tipi i objektit qe do kete lista e parametrave qe do copetohet </typeparam>
        /// <typeparam name="TCollection">Tipi i vleres se nje cifti ne dictionary-in qe do kthehet</typeparam>
        /// <param name="nrTaskesh">Numri i taskeve qe do te krijohen per te ekzekutuar  ne paralel metoden e marr si parameter</param>
        /// <param name="methodName">emri i metodes qe gjendet ne collection e cila do te ekzekutohet ne paralel me parametra te ndryshem</param>
        /// <param name="listOfData">Lista me objekte primitiv apo kompleks qe eshte parametri i pare i metodes qe do te ekzekutohet ne paralel.Kjo list do te copetohet ne disa pjese ne varesi te numrit te taskeve duke ekzekutuar disa here metoden e marr si parameter,me parametra te ndryshem </param>
        /// <param name="parametra">parametrat e tjere te metodes qe do thirret ne paralel,duke perjashtuar te parin </param>
        /// <returns>Kthen nje dictionay te krijuar me rezultatin e plote</returns>
        public static Dictionary<TKey, TCollection> MerrDictionaryAsParallel<TKey,TListOfParam, TCollection>(int nrTaskesh, string methodName, List<TListOfParam> listOfData, params object[] parametra)
        {
            var currentContext = HttpContext.Current;
            var myIdsCount = listOfData.Count;
            var thela = myIdsCount / nrTaskesh;
            if (thela == 0)
            {
                thela = myIdsCount;
                nrTaskesh = 1;
            }
            var tasks = new Task<Dictionary<TKey, TCollection>>[nrTaskesh];
            var method = typeof(TCollection).GetMethod(methodName);
            var listOfIds = new List<TListOfParam>[nrTaskesh];

            for (var i = 0; i < nrTaskesh; i++)
            {
                var baseValue = i;
                if (i == nrTaskesh - 1)
                    listOfIds[i] = listOfData.Skip(thela * i).ToList();
                else
                    listOfIds[i] = listOfData.Skip(thela * i).Take(thela).ToList();

                tasks[i] = Task.Factory.StartNew(b =>
                {
                    HttpContext.Current = currentContext;
                    var parametraNew = new object[parametra.Length + 1];
                    parametra.CopyTo(parametraNew, 1);
                    parametraNew[0] = listOfIds[(int)b];
                    return (Dictionary<TKey, TCollection>)method.Call(parametraNew);
                }, baseValue);
            }
            return Task.WhenAll(tasks).Result.SelectMany(x => x).ToDictionary(cifti => cifti.Key, cifti => cifti.Value);
        }



        /// <summary>
        /// Kjo metode sherben per te ndare ne disa thirrje ne paralel nje metode qe therret nje stored procedure dhe qe ka si parameter nje list me id,ose list me objekte
        /// thirrja ne paralel mundesohet duke copezuar listen ne disa pjese ne varesi te numrit te taskeve qe eshte vendosur te krijohen
        /// </summary>
        /// <typeparam name="TListOfParam">Tipi i objektit qe do kete lista e parametrave qe do copetohet </typeparam>
        /// <typeparam name="TResult">Tipi i objektit qe do kthehet</typeparam>
        /// <param name="nrTaskesh">numri i taskeve qe do krijohen per tu ekzektutuar ne paralel</param>
        /// <param name="methodName">emri i metodes qe duhet te gjendet tek tipi i objektit qe do kthehet</param>
        /// <param name="listOfData">lista e qe do i kalohet si parameter metodes</param>
        /// <param name="parametra">parametrat e tjere te metodes qe do thirret ne paralel,duke perjashtuar te parin </param>
        /// <returns>Kthen nje List<TResult> te krijuar me rezultatin e plote</returns>
        public static List<TResult> MerrListAsParallel<TListOfParam, TResult>(int nrTaskesh, string methodName, List<TListOfParam> listOfData, params object[] parametra)
        {
            var currentContext = HttpContext.Current;
            var myIdsCount = listOfData.Count;
            var thela = myIdsCount / nrTaskesh;
            if (thela == 0)
            {
                thela = myIdsCount;
                nrTaskesh = 1;
            }
           
            var tasks = new Task<List<TResult>>[nrTaskesh];
            var method = typeof(TResult).GetMethod(methodName);
            var listOfIds = new List<TListOfParam>[nrTaskesh];
            for (var i = 0; i < nrTaskesh; i++)
            {
                var baseValue = i;
                if (i == nrTaskesh - 1)
                    listOfIds[i] = listOfData.Skip(thela * i).ToList();
                else
                    listOfIds[i] = listOfData.Skip(thela * i).Take(thela).ToList();
                tasks[i] = Task.Factory.StartNew(b =>
                {
                    HttpContext.Current = currentContext;
                    var parametraNew = new object[parametra.Length + 1];
                    parametra.CopyTo(parametraNew, 1);
                    parametraNew[0] = listOfIds[(int)b];
                    return new List<TResult>((IEnumerable<TResult>)method.Call(parametraNew));
                }, baseValue);
            }
            return Task.WhenAll(tasks).Result.SelectMany(x => x).ToList();
        }


    }
}
