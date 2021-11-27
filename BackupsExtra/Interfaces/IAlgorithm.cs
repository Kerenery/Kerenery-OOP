using BackupsExtra.Enums;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface IAlgorithm
    {
        AlgoType Type { get; }
        RestorePoint Copy(JobObject jobObject, Repository repositoryToSave, int term);
    }
}