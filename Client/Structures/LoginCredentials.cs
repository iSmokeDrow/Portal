using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Structures
{
    /// <summary>
    /// Container class to hold user-input login variables
    /// </summary>
    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Pin { get; set; }
        public bool Remember { get; set;  }
    }
}
