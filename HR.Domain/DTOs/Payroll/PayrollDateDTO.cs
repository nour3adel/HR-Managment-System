using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Domain.DTOs.Payroll
{
    public class PayrollDateDTO
    {
        public DateOnly startdate {  get; set; }
        public DateOnly enddate {  get; set; }
    }
}
