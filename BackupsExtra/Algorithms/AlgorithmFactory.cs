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
        public ICleaningAlgorithm CreateCleaningAlgorithm(Limit limit, DateTime? date = null, int? pointsLimit = null, Limit? preference = null)
        {
            return limit switch
            {
                Limit.DateLimit => new RestorePointDateCleaner()
                {
                    CleaningDate = date ?? throw new BackupsExtraException("date can't be null"),
                },
                Limit.RestorePoints => new RestorePointCountCleaner()
                {
                    PointsLimit = pointsLimit ?? throw new BackupsExtraException("points count can't be null"),
                },
                Limit.Hybrid => new HybridCleaner()
                {
                    CleaningDate = date ?? throw new BackupsExtraException("date can't be null"),
                    PointsLimit = pointsLimit ?? throw new BackupsExtraException("points count can't be null"),
                    Preference = preference,
                },
                Limit.DateLimitMergeable => new RestorePointDateCleaner()
                {
                    CleaningDate = date ?? throw new BackupsExtraException("date can't be null"),
                    IsMergeable = true,
                },
                Limit.RestorePointsMergeable => new RestorePointCountCleaner()
                {
                    IsMergeable = true,
                    PointsLimit = pointsLimit ?? throw new BackupsExtraException("points count can't be null"),
                },
                Limit.HybridMergeable => new HybridCleaner()
                {
                    IsMergeable = true,
                    CleaningDate = date ?? throw new BackupsExtraException("date can't be null"),
                    PointsLimit = pointsLimit ?? throw new BackupsExtraException("points count can't be null"),
                    Preference = preference,
                },
                _ => throw new BackupsExtraException("unknown limit"),
            };
        }

        public IAlgorithm CreateBackupAlgorithm(AlgoType algoType)
        {
            return algoType switch
            {
                AlgoType.Single => new SingleStorageAlgorithm(),
                AlgoType.Split => new SplitStorageAlgorithm(),
                _ => throw new BackupsExtraException("unknown algorithm type")
            };
        }
    }
}