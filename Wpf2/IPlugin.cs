using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf2
{
    public interface IPlugin
    {
        string Name { get; }
        string Do(string str);
    }

}
