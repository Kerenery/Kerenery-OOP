using Backups.Interfaces;
using Backups.Services;
using Backups.Tools;

namespace Backups.Models
{
    public class Context
    {
        private IAlgorithm _algorithm;
        public Context(IAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public void SetAlgorithm(IAlgorithm algorithm)
        {
            _algorithm = algorithm ?? throw new BackupException("algorithm cant be null");
        }

        public IRepository CreateCopy(RestorePoint restorePoint, IRepository repository) => _algorithm.CreateCopy(restorePoint, repository);

        public bool IsAlgorithmExists() => _algorithm is not null;
    }
}