﻿using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeById(int id);
        Task<bool> Create(Employee fields);
        Task<bool> Update(int id, Employee fields);
        Task<bool> Delete(int id);
    }
}
