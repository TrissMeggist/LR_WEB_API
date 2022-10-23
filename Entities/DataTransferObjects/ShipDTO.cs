using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ShipDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Class { get; set; }
    }
}
