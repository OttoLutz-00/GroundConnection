using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Data
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }

        public string  Skills { get; set; }
        
    }
}
