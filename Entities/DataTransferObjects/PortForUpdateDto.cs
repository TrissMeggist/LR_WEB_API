using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class PortForUpdateDto
    {
        public string Title { get; set; }
        public string Country { get; set; }
        public string Type { get; set; }
        public IEnumerable<ShipForCreationDto> Ships { get; set; }
    }
}
