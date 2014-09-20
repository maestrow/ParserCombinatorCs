using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Company
{
    public class Company
    {
        public string CompanyName { get; set; }
        public IEnumerable<Employee> Employees { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}",
                CompanyName,
                Employees
                    .Select(a => a.ToString())
                    .DefaultIfEmpty()
                    .Aggregate((a, b) => string.Format("{0}, {1}", a, b)));
        }
    }
}
