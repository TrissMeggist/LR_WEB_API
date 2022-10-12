using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Ships
    {
        [Column("ShipsId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Ships title is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Title is 30 characters.")] public string Title { get; set; }
        [Required(ErrorMessage = "Class is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Class is 30 characters.")] public string Class { get; set; }
        
        [ForeignKey(nameof(Ports))]
        public Guid PortsId { get; set; }
        public Ports Port { get; set; }
    }
}
