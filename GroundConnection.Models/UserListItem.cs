using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models
{
    public class UserListItem
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        public string Skills { get; set; }
    }
}
