using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.List
{
    public class List: List<string>
    {
        public List(IEnumerable<string> list): base(list)
        {
        }

        public bool IsOrdered { get; set; }

        public char Bullet { get; set; }

        public override string ToString()
        {
            return this.DefaultIfEmpty().Aggregate((a, b) => string.Format("{0}, {1}", a, b));
        }
    }
}
