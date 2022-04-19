using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using AlphaWeb.Core.Interfaces.Localization;

namespace DbCoreTests.Fake
{
    public class FakeMessages : IMessagesResource
    {
        public string this[string name] { get { return Get(name); } }
        public string Get(string name)
        {
            return "Dokumenti u ruajt me sukses!";
        }

        public string Get(string name, int IdGjuha)
        {
            return "Dokumenti u ruajt me sukses!";
        }

        public string Get(string name, CultureInfo ci)
        {
            return "Dokumenti u ruajt me sukses!";
        }
    }
}
