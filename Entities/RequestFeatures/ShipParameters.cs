using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.RequestFeatures
{
    public class ShipParameters : RequestParameters
    {
        public ShipParameters()
        {
            OrderBy = "title";
        }
        public string SearchTerm { get; set; }
    }
}
