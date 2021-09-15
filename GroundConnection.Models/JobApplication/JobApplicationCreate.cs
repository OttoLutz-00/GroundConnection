using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
    public class JobApplicationCreate
    {
        public int JobId { get; set; }
        public int UserId { get; set; }
        public DateTimeOffset DateAccepted { get; set; }
    }
}
