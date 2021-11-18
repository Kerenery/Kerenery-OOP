using System;
using BackupsExtra.Enums;
using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class SingleStorageAlgorithm : IAlgorithm
    {
        public SingleStorageAlgorithm(Limit limit, Repository repository, DateTime? date = null, int? pointsCount = null)
        {
            LimitType = limit;
        }

        public Limit LimitType { get; }

        public RestorePoint Copy()
        {
            throw new System.NotImplementedException();
        }
    }
}