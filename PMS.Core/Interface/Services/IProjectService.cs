﻿using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProject();
        Task<Project> GetProjectById(int id);
        Task<bool> Create(Project fields);
        Task<bool> Update(int id, Project fields);
        Task<bool> Delete(int id);
    }
}
