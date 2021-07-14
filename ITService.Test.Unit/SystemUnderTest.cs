using ITService.Test.Unit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITService.Test.Unit
{
    public class SystemUnderTest : IDisposable
    {
        public void Dispose() { }
        public UserProxy CreateUser()
        {
            var user = new UserProxy();
            return user;
        }
    }
}
