using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmsService.Models.Domains
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; }
        public string phone { get; set; }
    }
}
