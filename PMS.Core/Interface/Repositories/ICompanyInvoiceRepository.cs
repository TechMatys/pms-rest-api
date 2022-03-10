using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface ICompanyInvoiceRepository
    {
        Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoices();
        Task<int> Create(CompanyInvoice fields);
        Task<int> Delete(int id);
    }
}
