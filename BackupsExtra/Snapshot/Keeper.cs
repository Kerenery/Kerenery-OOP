using System.Collections.Generic;
using System.IO;
using System.Linq;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using Newtonsoft.Json;
using Serilog;

namespace BackupsExtra.Snapshot
{
    public class Keeper
    {
        private List<BackupsSnapshot> _shots = new ();
        private BackupExtraService _backupService;

        public Keeper(BackupExtraService backupService)
        {
            _backupService = backupService;
        }

        public void Backup()
        {
            _shots.Add(_backupService.Save());
            File.WriteAllText(
                Path.Combine(Directory.GetCurrentDirectory(), "1.json"),
                JsonConvert.SerializeObject(
                    _shots.Last(),
                    new JsonSerializerSettings()
                    {
                        TypeNameHandling = TypeNameHandling.Auto,
                    }));
            Log.Information("backup service is saved");
        }

        public void Restore()
        {
            using StreamReader reader = new (Path.Combine(Directory.GetCurrentDirectory(), $"1.json"));
            string jsonReader = reader.ReadToEnd();
            BackupsSnapshot snapshot = JsonConvert.DeserializeObject<BackupsSnapshot>(
            jsonReader, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
            }) ?? throw new BackupsExtraException("deserialization error occured");
            _backupService.Restore(snapshot);
            Log.Information("backup service is restored");
        }
    }
}