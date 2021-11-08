using System.Collections.Generic;
using System.IO;
using System.Linq;
using Banks.Accounts;
using Banks.Tools;
using Newtonsoft.Json;

namespace Banks.SnapShot
{
    public class Caretaker
    {
        private readonly List<IMemento> _mementos = new List<IMemento>();
        public IAccount Account { get; init; }

        public void Backup()
        {
            _mementos.Add(Account.Save());
            using StreamWriter file = File.CreateText(@$"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\{_mementos.Count}.json");
            JsonSerializer serializer = new ();
            serializer.Serialize(file, _mementos.Last());
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
                throw new BanksException("there are no mementos");

            using StreamReader reader =
                new (@$"C:\Users\djhit\RiderProjects\is\Kerenery\Banks\Snapshots\{_mementos.Count}.json");
            string json = reader.ReadToEnd();

            IMemento memento = Account switch
            {
                DebitAccount => JsonConvert.DeserializeObject<DebitAccountMemento>(json),
                CreditAccount => JsonConvert.DeserializeObject<CreditAccountMemento>(json),
                DepositAccount => JsonConvert.DeserializeObject<DepositAccountMemento>(json),
                _ => throw new BanksException("unknown account type")
            };

            _mementos.Remove(memento);
            Account.Restore(memento);
        }
    }
}