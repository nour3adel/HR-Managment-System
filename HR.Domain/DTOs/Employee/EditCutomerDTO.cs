using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.Employee
{
    public class EditCutomerDTO
    {

        public string id { get; set; }
        public string fullname { get; set; }

        public string username { get; set; }

        public string email { get; set; }

        public string address { get; set; }

        public string phonenumber { get; set; }
    }
}
