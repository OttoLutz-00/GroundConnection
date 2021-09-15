using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Models.JobApplication
{
   public  class JobApplicationListItem
    {
        public string UserName { get; set; }
        public string Skills { get; set; }
        public DateTimeOffset? AcceptedDate { get; set; }
        public int Id { get; set; }


    }
}
