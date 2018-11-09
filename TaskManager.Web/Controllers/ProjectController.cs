﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Infrastructure.UnitOfWork;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    public class ProjectController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Project>> GetProjects()
        {
            return await _unitOfWork.ProjectRepository.Get().Include(x=>x.Tags).ToListAsync();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> AddProject([FromBody]Project project)
        {
            if (_unitOfWork.ProjectRepository.Get().Any(x => x.Name == project.Name))
                return BadRequest(project);

            _unitOfWork.ProjectRepository.Insert(project);

            await _unitOfWork.Save();

            return Ok(_unitOfWork.ProjectRepository.Get(x=>x.Id == project.Id).Include(x=>x.Tags).FirstOrDefault());
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProject([FromBody]Project updatedProject)
        {
            var project = _unitOfWork.ProjectRepository
                .Get(x => x.Id == updatedProject.Id)
                .FirstOrDefault();

            project.Name = updatedProject.Name;

            project.Description = updatedProject.Description;

            project.Type = updatedProject.Type;

            project.Progress = updatedProject.Progress;

            project.Deadline = updatedProject.Deadline;

            //project.Tags = updatedProject.Tags;

            _unitOfWork.ProjectRepository.Update(project);

            await _unitOfWork.Save();
            
            return Ok(project);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> DeleteProject([FromBody] Project project)
        {

            _unitOfWork.ProjectRepository.Delete(project);

            await _unitOfWork.Save();

            return Ok(project);
        }
    }
}
