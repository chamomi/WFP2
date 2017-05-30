using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf2
{
    public class Uppercase : IPlugin
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string Do(string str)
        {
            return str.ToUpper();
        }
    }
}
