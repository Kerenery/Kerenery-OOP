using System.Collections.Generic;

namespace Backups.Services
{
    public interface IAlgorithm
    {
        void CreateCopy(List<string> filePath);
    }
}