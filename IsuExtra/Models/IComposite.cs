using System.Collections.Generic;

namespace IsuExtra.Models
{
    public interface IComposite
    {
        public Component Add(Component component);
        public void Remove(Component component);
    }
}