using System;
using BackupsExtra.Models;

namespace BackupsExtra.Interfaces
{
    public interface ICleaningAlgorithm
    {
        void Clean(Backup backup);
    }
}