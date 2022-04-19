using DbCore.MbylljePeriudhe.Interfaces;

namespace DbCore.MbylljePeriudhe
{
    public interface IProcesetContainer
    {
        IProcess GetProcesi(string serverName, int idNdermarrje);
        void SetProcess(IProcess procesi);
        void DeleteProcess(IProcess procesi);
    }
}
