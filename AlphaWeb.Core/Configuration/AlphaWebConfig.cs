using System.Configuration;
using System.Xml;

namespace AlphaWeb.Core.Configuration
{
    /// <summary>
    /// Represents startup AlphaWeb configuration parameters
    /// </summary>
    public class AlphaWebConfig : IConfigurationSectionHandler
    {
        public object Create(object parent, object configContext, XmlNode section)
        {
            return new AlphaWebConfig();
        }
    }
}
