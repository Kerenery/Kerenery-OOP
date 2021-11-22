using System;
using System.Collections.Generic;
using System.Linq;
using BackupsExtra.Enums;
using BackupsExtra.Tools;
using Microsoft.VisualBasic.CompilerServices;
using Serilog;

namespace BackupsExtra.Models
{
    public class Backup
    {
        private readonly List<RestorePoint> _restorePoints = new ();
        public Repository Repository { get; init; }

        public Guid Id { get; init; }

        public string Name { get; init; }

        public AlgoType CreatedBy { get; init; }

        public int Term => _restorePoints.Count;

        public RestorePoint AddRestorePoint(RestorePoint restorePoint)
        {
            if (_restorePoints.Any(rp => rp.Id == restorePoint.Id))
            {
                Log.Error($"restore point with {restorePoint.Id} is already added");
                throw new BackupsExtraException("such point is already added");
            }

            _restorePoints.Add(restorePoint);
            Log.Information($"new restore point is added to backup with {Id}");
            return restorePoint;
        }

        public void RemoveRestorePoints(int count)
        {
            if (count >= Term)
                throw new BackupsExtraException($"cant delete so many points, {count} is bigger then points count");

            _restorePoints.RemoveRange(0, count);
            Log.Information($"removed {count} points from backup {Id}");
        }

        public RestorePoint GetFirstPoint() => _restorePoints.FirstOrDefault();
    }
}