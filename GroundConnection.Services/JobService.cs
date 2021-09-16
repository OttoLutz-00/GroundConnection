using GroundConnection.Data;
using GroundConnection.Models.JobModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroundConnection.Services
{
    public class JobService
    {
        private readonly Guid _userId;

        public JobService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateJob(JobCreate model)
        {
            var entity = new Job()
            {
                UserId = model.UserId,
                OwnerId = _userId,
                CreatedUTC = DateTimeOffset.Now,
                JobDescription = model.JobDescription,
                ExpectedCompletionDate = model.ExpectedCompletionDate,
                IsActive = true,
                Location = model.Location
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Jobs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
