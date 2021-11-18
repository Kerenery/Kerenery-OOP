using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public abstract class AlgorithmHandler : IAlgorithmHandler
    {
        protected AlgorithmHandler(IAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        protected IAlgorithm Algorithm { get;  }

        protected IAlgorithmHandler NextHandler { get; set; }

        public IAlgorithmHandler SetNext(IAlgorithmHandler algorithmHandler)
        {
            NextHandler = algorithmHandler
                          ?? throw new BackupsExtraException("handler should be implemented");

            return NextHandler;
        }

        public virtual RestorePoint Handle(IAlgorithm algorithm, JobObject jobObject, Repository repository)
            => NextHandler.Handle(algorithm, jobObject, repository);
    }
}