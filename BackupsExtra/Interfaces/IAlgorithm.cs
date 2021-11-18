using BackupsExtra.Enums;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface IAlgorithm
    {
        Limit LimitType { get; }
        RestorePoint Copy();
    }
}