using System;
using System.Data;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;
using BackupsExtra.Tools;

namespace BackupsExtra.Algorithms
{
    public class AlgorithmFactory
    {
        public IAlgorithm CreateSplitAlgorithm(
            Limit limit, Repository repository, DateTime? date = null, int? pointsCount = null)
        {
            return limit switch
            {
                Limit.DateLimit => new SplitStorageAlgorithm(limit, repository, date),
                Limit.RestorePoints => new SplitStorageAlgorithm(limit, repository, pointsCount: pointsCount),
                Limit.Hybrid => new SplitStorageAlgorithm(limit, repository, date, pointsCount),
                Limit.DateLimitMergeable => new SplitStorageAlgorithm(limit, repository, date),
                Limit.RestorePointsMergeable => new SplitStorageAlgorithm(limit, repository, pointsCount: pointsCount),
                Limit.HybridMergeable => new SplitStorageAlgorithm(limit, repository, date, pointsCount),
                _ => throw new BackupsExtraException("there is no such limit")
            };
        }

        public IAlgorithm CreateSingleAlgorithm(
            Limit limit, Repository repository, DateTime? date = null, int? pointsCount = null)
        {
            return limit switch
            {
                Limit.DateLimit => new SingleStorageAlgorithm(limit, repository, date),
                Limit.RestorePoints => new SingleStorageAlgorithm(limit, repository, pointsCount: pointsCount),
                Limit.Hybrid => new SingleStorageAlgorithm(limit, repository, date, pointsCount),
                Limit.DateLimitMergeable => new SingleStorageAlgorithm(limit, repository, date),
                Limit.RestorePointsMergeable => new SingleStorageAlgorithm(limit, repository, pointsCount: pointsCount),
                Limit.HybridMergeable => new SingleStorageAlgorithm(limit, repository, date, pointsCount),
                _ => throw new BackupsExtraException("there is no such limit")
            };
        }
    }
}