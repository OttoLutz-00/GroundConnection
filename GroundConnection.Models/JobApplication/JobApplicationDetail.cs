using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
   public class JobApplicationDetail
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public string Location { get; set; }
        
    }
}
