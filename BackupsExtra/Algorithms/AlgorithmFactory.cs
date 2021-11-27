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
                Limit.Merge => new MergeCleaner(),
                _ => throw new BackupsExtraException("unknown limit"),
            };
        }

        public IAlgorithm CreateBackupAlgorithm(AlgoType algoType)
        {
            return algoType switch
            {
                AlgoType.SingleStorage => new SingleStorageAlgorithm(),
                AlgoType.SplitStorage => new SplitStorageAlgorithm(),
                _ => throw new BackupsExtraException("unknown algorithm type")
            };
        }
    }
}