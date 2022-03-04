﻿using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;


namespace PMS.Core.Services
{
    public class EmployeePaymentService : IEmployeePaymentService
    {
        public readonly IEmployeePaymentRepository _EmployeePaymentRepository;

        public EmployeePaymentService(IEmployeePaymentRepository EmployeePaymentRepository)
        {
            _EmployeePaymentRepository = EmployeePaymentRepository ?? throw new ArgumentNullException(nameof(EmployeePaymentRepository));
        }

        public async Task<IEnumerable<EmployeePaymentListModel>> GetAllEmployeePayment()
        {
            return await _EmployeePaymentRepository.GetAllEmployeePayment();
        }

        public async Task<EmployeePayment> GetEmployeePaymentById(int id)
        {
            return await _EmployeePaymentRepository.GetEmployeePaymentById(id);
        }

        public async Task<bool> Create(EmployeePayment fields)
        {
            return await _EmployeePaymentRepository.Create(fields);
        }

        public async Task<bool> Update(int id, EmployeePayment fields)
        {
            return await _EmployeePaymentRepository.Update(id, fields);
        }

        public async Task<bool> Delete(int id)
        {
            return await _EmployeePaymentRepository.Delete(id);
        }

       
    }

    
}
