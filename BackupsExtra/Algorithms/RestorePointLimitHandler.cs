using System;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class RestorePointLimitHandler : AlgorithmHandler
    {
        public RestorePointLimitHandler(IAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override RestorePoint Handle(IAlgorithm algorithm, JobObject jobObject, Repository repository)
        {
            switch (algorithm.LimitType)
            {
                case Limit.DateLimit:
                    return base.Handle(algorithm, jobObject, repository);
                case Limit.RestorePoints:
                    break;
                case Limit.Hybrid:
                    return base.Handle(algorithm, jobObject, repository);
                case Limit.DateLimitMergeable:
                    return base.Handle(algorithm, jobObject, repository);
                case Limit.RestorePointsMergeable:
                    break;
                case Limit.HybridMergeable:
                    return base.Handle(algorithm, jobObject, repository);
                default:
                    throw new BackupsExtraException("Limit type is invalid");
            }

            return algorithm.Copy();
        }
    }
}