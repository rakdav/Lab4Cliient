using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Model
{
    class Response
    {
        public string? access_token { get; set; }
        public string? username { get; set; }
        public  Response(string? access_token, string? username)
        {
            this.access_token = access_token;
            this.username = username;
        }
    }
}
