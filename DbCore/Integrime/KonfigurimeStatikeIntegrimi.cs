using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.Integrime
{
    public static class KonfigurimeStatikeIntegrimi
    {

        public static bool FakeResponse { get; private set; }
        public static bool FakePin { get; private set; }
        static KonfigurimeStatikeIntegrimi()
        {
            var fakeResponse = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FAKE_RESPONSE);
            var fakePin = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.fakePin);
            FakeResponse = fakeResponse == null ? false : Convert.ToBoolean(fakeResponse);
            FakePin = fakePin == null ? false : Convert.ToBoolean(fakePin);
          
        }
    }
}
