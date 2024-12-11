using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Model
{
    class User
    {
        public long IdUser { get; set; }
        public required string Login { get; set; }
        public required string Password { get; set; }
    }
}
