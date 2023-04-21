using PMS.Core.Model;

namespace PMS.Core.Interface.Repositories
{
    public interface ICompanyInvoiceRepository
    {
        Task<IEnumerable<CompanyInvoiceListModel>> GetAllCompanyInvoicesAsync();
        Task<int?> CreateAsync(CompanyInvoice fields);
        Task<int?> DeleteAsync(int id);
    }
}
