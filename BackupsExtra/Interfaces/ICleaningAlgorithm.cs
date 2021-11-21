using System;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface ICleaningAlgorithm
    {
        bool IsMergeable { get; }
        RestorePoint MergeClean(Backup backup);

        void Clean(Backup backup);
    }
}