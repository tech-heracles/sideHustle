using System;
using System.Globalization;
using System.Resources;
using AlphaWeb.Core.Interfaces.Localization;

namespace DbCore.IMBUtils.Messages
{

    public class MessagesResource : IMessagesResource
    {
        public static readonly MessagesResource Messages;

        //pointeri per te thirrur funksionin e marrjes se gjuhes per te eleminuar varesine ndaj sessionit ne kete klase
        //kesaj klase nuk i duhet gje se nga merret gjuha (SRP principle)
        private Func<int> _getLanguage;

        static MessagesResource() => Messages = new MessagesResource();

        /// <summary>
        /// kete duhet ta thirresh ne App Start, duke i kaluar si parameter Resource Managerin dhe funskionin per te marr gjuhen
        /// </summary>
        /// <param name="getLanguage"></param>
        public void Initialize(Func<int> getLanguage, ResourceManager resourceManager)
        {
            _getLanguage = getLanguage;
            CurrentResourceManager = resourceManager;
        }

        public CultureInfo CurrentCultureInfo => KtheCultureInfo(_getLanguage.Invoke());
        public static ResourceManager CurrentResourceManager { get; private set; }

        public int IdGjuha => _getLanguage.Invoke();

        /// <summary>
        /// Kthen stringun e duhur sipas gjuhes se loguar
        /// </summary>
        /// <param name="name">kodi i stringut ne resource files</param>
        /// <returns></returns>
        public string this[string name] => Get(name);

        public string Get(string name) => Get(name, CurrentCultureInfo);

        public string Get(string name, CultureInfo ci) => CurrentResourceManager.GetString(name, ci);

        public string Get(string name, int idGjuha) => Get(name, KtheCultureInfo(idGjuha));

        /// <summary>
        /// Kthen CultureInfo ne baze te gjuhes
        /// </summary>
        /// <param name="idGjuha">idGjuha</param>
        /// <returns></returns>
        public static CultureInfo KtheCultureInfo(int idGjuha)
        {
            switch (idGjuha)
            {
                case 0: return new CultureInfo("sq-AL");
                case 1: return new CultureInfo("en-US");
                case 2: return new CultureInfo("fr-FR");
                default: throw new mySessionNewException("CultureInfo ska vlere");
            }
        }
        public static string KtheKodGjuhe(int idGjuha)
        {
            switch (idGjuha)
            {
                case 0: return "sq";
                case 1: return "en";
                case 2: return "fr";
                default: throw new MyException("Id gjuhe e pa njohur");
            }
        }
    }
}