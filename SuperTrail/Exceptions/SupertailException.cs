using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperTrail.Exceptions
{
    internal class SupertailException:AssertionException
    {
        public SupertailException(string messge):base(messge)
        {
            
        }
    }
}
