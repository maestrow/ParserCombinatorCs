using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.List
{
    public class ListItem
    {
        public string Caption { get; set; }
        public List<ListItem> Sublist { get; set; }
    }
}
