using DMCATXT.Models;

namespace DMCATXT.Services
{
    public interface IDataService
    {
        Task<List<UploadInfo>> GetDataService(string customerName, string name, string surname, DateTime createdDate, string fileName);
        Task<List<UploadInfo>> GetQueryService(DateTime dateTo, DateTime dateFrom, string customer,string name, string surName);
        Task UpdateApproval(int id);
        Task<List<UploadInfo>> GetAdminQueryService(DateTime dateTo, DateTime dateFrom, string customer, string name, string surName);
    }
}
