using DbCore;

namespace AlphaWeb.Core.Interfaces.Data
{
    /// <summary>
    /// Ky interface duhet te implementoeht per te gjithe objeketet qe lidhen me nje tabele ne db.
    /// Ben te detyrueshem implementimin e metodave te nevojshme per lexim shkrim
    /// </summary>
    public interface IDataBase : IDataBaseReader
    {
        clsMesazh Ruaj();
        clsMesazh Modifiko();
        clsMesazh Fshi();

    }
}
