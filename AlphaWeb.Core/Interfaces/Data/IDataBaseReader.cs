using System.Data;

namespace AlphaWeb.Core.Interfaces.Data
{
    /// <summary>
    /// eshte i nevojshem per te implementuar mbushjen e nje objekti apo collectioni me objekte
    /// </summary>
    public interface IDataBaseReader
    {
        void Mbush(IDataRecord record);
    }
}
