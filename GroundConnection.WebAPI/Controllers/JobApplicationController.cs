using GroundConnection.Models.JobApplication;
using GroundConnection.Models.JobModels;
using GroundConnection.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GroundConnection.WebAPI.Controllers
{
    [Authorize]
    public class JobApplicationController : ApiController
    {
        private JobApplicationServices CreateJobApplicationServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var services = new JobApplicationServices(userid);
            return services;
        }
        [Route("api/JobApplication/")]
        [HttpPost]
        public IHttpActionResult Apply(JobApplicationCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateJobApplicationServices();

            if (!service.ApplyForJob(model))
                return BadRequest("You cannot Apply for this Job");

            return Ok("Congratulation, Your Job Application is succsfull");


        }
        [Route("api/Approval")]
        [HttpPut]
        public IHttpActionResult Approve(JobApproval model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateJobApplicationServices();

            if (!service.ApproveJob(model))
                return InternalServerError();

            return Ok("Job Approved");


        }
        [Route("api/JobApplication/JobId/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllJobApplicationByJobId(int id)
        {
            var service = CreateJobApplicationServices();
            var jobApplication = service.GetJobApplicationsByJobId(id);
            return Ok(jobApplication);

        }

        [Route("api/JobApplication/ApplicationId/{id}")]
        [HttpGet]
        public IHttpActionResult GetAllJobApplicationById(int id)
        {
            var service = CreateJobApplicationServices();
            var jobApplication = service.GetJobApplicationByJobApplicationId(id);
            return Ok(jobApplication);

        }

        [HttpDelete]
        public IHttpActionResult DeleteJobApplication(int id)
        {
            var service = CreateJobApplicationServices();
            if (service.DeleteJobApplication(id))
            {
                return Ok("Job Application Succesfully Deleted");
            }
            return InternalServerError();
            

        }
        [Route("api/UploadImage/{id}")]
        [HttpPost]
        public IHttpActionResult UploadProofOfCompletion(ProofOfCompletion model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateJobApplicationServices();
            if (!service.UploadImageForCompletion(model, id))
            {
                return BadRequest("Cannot Attach proof of Completion");
            }
            return Ok("Image uploaded  Succesfully");
        }

        [Route("api/EditImage/{id}")]
        [HttpPut]
        public IHttpActionResult EditProofOfCompletion(ProofOfCompletion model, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var service = CreateJobApplicationServices();
            if (!service.EditImage(model, id))
            {
                return BadRequest("Cannot Edit proof of Completion");
            }
            return Ok("Proof Of Completion Succesfully Edited");
        }

        [Route("api/DeleteProofOfCompletion")]
        [HttpDelete]
        public IHttpActionResult DeleteProofOfCompletion(int id)
        {
         
            var service = CreateJobApplicationServices();
            if (!service.DeleteImage(id))
            {
                return BadRequest("Unable to delete Image");
            }
            return Ok("Proof Of Completion Succesfully Deleted");
        }



    }
}
