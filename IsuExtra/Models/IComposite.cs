using System.Collections.Generic;

namespace IsuExtra.Models
{
    public interface IComposite
    {
        Component Add(Component component);
        void Remove(Component component);
    }
}