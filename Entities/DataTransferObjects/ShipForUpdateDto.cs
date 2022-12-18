using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ShipForUpdateDto : ShipForManipulationDto
    {
        [Required(ErrorMessage = "Ship title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the title is 30 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Class is a required field.")]
        [MaxLength(20, ErrorMessage = "Maximum length for the Class is 20 characters.")]
        public string Class { get; set; }
    }
}
