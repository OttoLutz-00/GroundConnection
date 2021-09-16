﻿using GroundConnection.Models.JobApplication;
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
    public class JobApplicationController2Controller : ApiController
    {
        private JobApplicationServices2 CreateJobApplicationServices()
        {
            var userid = Guid.Parse(User.Identity.GetUserId());
            var services = new JobApplicationServices2(userid);
            return services;
        }

        [HttpPost]
        public IHttpActionResult Apply(JobApplicationCreate model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var service = CreateJobApplicationServices();

            if (!service.ApplyForJob(model))
                return InternalServerError();

            return Ok("Congratulation, YOur Job Application is succsfull");


        }
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

        [HttpGet]
        public IHttpActionResult GetAllJobApplicationByJobId(int id)
        {
            var service = CreateJobApplicationServices();
            var jobApplication = service.GetJobApplicationsByJobId(id);
            return Ok(jobApplication);

        }


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

    }

}
