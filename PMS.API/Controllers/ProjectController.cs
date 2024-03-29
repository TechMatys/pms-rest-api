﻿using PMS.Core.Interface.Services;
using PMS.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace PMS.API.Controllers
{
    [Route("project")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _ProjectService;

        public ProjectController(IProjectService ProjectService)
        {
            _ProjectService = ProjectService ?? throw new ArgumentNullException(nameof(ProjectService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectListModel>>> GetAllProject()
        {
            var response = await _ProjectService.GetAllProject();

            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    statusCode = HttpStatusCode.InternalServerError
                });
            }

            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var response = await _ProjectService.GetProjectById(id);
            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "No records found",
                    statusCode = HttpStatusCode.NotFound
                });
            }

            return Ok(new
            {
                data = response,
                statusCode = HttpStatusCode.OK
            });
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] Project ProjectModal)
        {
            var response = await _ProjectService.Create(ProjectModal);
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }

            if (response < 1)
            {
                return Ok(new
                {
                    message = "Project name already exists",
                    statusCode = HttpStatusCode.Conflict,
                });
            }

            return Ok(new
            {
                data = response,
                message = "Created",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<int>> Update(int id, [FromBody] Project ProjectModal)
        {
            var response = await _ProjectService.Update(id, ProjectModal);
            if (response == null)
            {
                return Ok(new
                {
                    data = response,
                    message = "Server error",
                    statusCode = HttpStatusCode.InternalServerError,
                });
            }

            if (response == 0)
            {
                return Ok(new
                {
                    message = "No Records Found",
                    statusCode = HttpStatusCode.NotFound,
                });
            }

            if (response < 1)
            {
                return Ok(new
                {
                    message = "Project name Already Exists",
                    statusCode = HttpStatusCode.Conflict,
                });
            }

            return Ok(new
            {
                data = response,
                message = "Updated",
                statusCode = HttpStatusCode.OK,
            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete(int id)
        {
            var response = await _ProjectService.Delete(id);
            if (response == null)
            {
                return Ok(new
                {
                    message = "Server Error",
                    StatusCode = HttpStatusCode.InternalServerError,
                });
            }

            if (response < 1)
            {
                return Ok(new
                {
                    message = "Record not found",
                    statusCode = HttpStatusCode.NotFound,
                });
            }

            return Ok(new
            {
                message = "Deleted",
                statusCode = HttpStatusCode.OK
            });
        }
    }
}
