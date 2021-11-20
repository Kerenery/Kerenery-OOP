using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface ICleaningAlgorithm
    {
        RestorePoint Clean();

        bool IsMergeable { get; }
    }
}