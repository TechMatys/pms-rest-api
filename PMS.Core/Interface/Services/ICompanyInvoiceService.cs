using PMS.Core.Model;

namespace PMS.Core.Interface.Services
{
    public interface ICompanyInvoiceService
    {
        Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoice();
        Task<int> Create(CompanyInvoice fields);
        Task<int> Delete(int id);
    }
}
