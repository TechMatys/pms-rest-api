using PMS.Core.Interface.Repositories;
using PMS.Core.Interface.Services;
using PMS.Core.Model;

namespace PMS.Core.Services
{
    public class CompanyInvoiceService : ICompanyInvoiceService
    {
        public readonly ICompanyInvoiceRepository _CompanyInvoiceRepository;

        public CompanyInvoiceService(ICompanyInvoiceRepository CompanyInvoiceRepository)
        {
            _CompanyInvoiceRepository = CompanyInvoiceRepository ?? throw new ArgumentNullException(nameof(CompanyInvoiceRepository));
        }

        public async Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoice()
        {
            return await _CompanyInvoiceRepository.GetAllCompanyInvoices();
        }

        public async Task<CompanyInvoice> GetCompanyInvoiceById(int id)
        {
            return await _CompanyInvoiceRepository.GetCompanyInvoiceById(id);
        }

        public async Task<int> Create(CompanyInvoice fields)
        {
            return await _CompanyInvoiceRepository.Create(fields);
        }           

        public async Task<int> Delete(int id)
        {
            return await _CompanyInvoiceRepository.Delete(id);
        }
    }
}
