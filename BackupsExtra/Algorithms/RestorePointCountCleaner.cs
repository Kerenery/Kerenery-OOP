using BackupsExtra.Interfaces;
using BackupsExtra.Models;

namespace BackupsExtra.Algorithms
{
    public class RestorePointCountCleaner : ICleaningAlgorithm
    {
        public bool IsMergeable { get; init; }

        public int? PointsLimit { get; init; }

        public RestorePoint Clean()
        {
            throw new System.NotImplementedException();
        }
    }
}