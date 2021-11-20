using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}