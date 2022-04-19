using System.Globalization;

namespace AlphaWeb.Core.Interfaces.Localization
{
    //TODO need to think about this
    public interface IMessagesResource
    {
        string Get(string name);
        string Get(string name, CultureInfo ci);
        string Get(string name, int IdGjuha);

        string this[string name] { get; }
    }
}
