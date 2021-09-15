using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Data
{
    public enum StatusOfJob
    {
        Pending,
        Approved,
        Declined,
        
    }
    public class JobApplication
    {
        public int Id { get; set; }
        public int JobId { get; set; }
        [ForeignKey(nameof(Job))]
        public virtual Job Job { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTimeOffset CreatedUTC { get; set; }
        public DateTimeOffset? DateAccepted { get; set; }
        //public DateTimeOffset? ModifiedUTC { get; set; }
        public StatusOfJob JobStatus { get; set; } 
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
        public byte?[] FileContent { get; set; }

    }
}
