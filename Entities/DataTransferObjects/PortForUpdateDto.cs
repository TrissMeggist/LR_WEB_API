using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class PortForUpdateDto : PortForManipulationDto
    {
        [Required(ErrorMessage = "Port title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Country is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Country is 30 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Type is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Type is 30 characters.")]
        public string Type { get; set; }
        public IEnumerable<ShipForCreationDto> Ships { get; set; }
    }
}
