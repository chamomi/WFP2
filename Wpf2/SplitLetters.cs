using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf2
{
    public class SplitLetters : IPlugin
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
            Trace.WriteLine("split");
            var nstr = "";
            char[] chars = str.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
                nstr = nstr + chars[i] + " ";
            return nstr;
        }
    }
}
