using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public interface IAlgorithmHandler
    {
        IAlgorithmHandler SetNext(IAlgorithmHandler algorithmHandler);

        RestorePoint Handle(IAlgorithm algorithm, JobObject jobObject, Repository repository);
    }
}