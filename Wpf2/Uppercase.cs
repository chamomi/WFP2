using System;

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
