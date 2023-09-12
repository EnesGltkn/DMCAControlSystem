using DMCATXT.Models;
using Microsoft.EntityFrameworkCore;

namespace DMCATXT.Services
{
    public class DataService : IDataService
    {
        private IWebHostEnvironment environment;
        private IServiceProvider serviceProvider;
        public DataService(IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            this.environment = env;
            this.serviceProvider = serviceProvider;
        }
        public async Task UpdateApproval(int id)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var sqlContext = scope.ServiceProvider.GetService<SqlDBContext>();
                try
                {
                    var recordToUpdate = await sqlContext.UploadInfos.FirstOrDefaultAsync(u => u.ID == id);

                    if (recordToUpdate != null)
                    {
                        recordToUpdate.Approval = "1";
                        await sqlContext.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    // Hata işleme
                }
            }
        }


        public async Task <List<UploadInfo>> GetDataService(string customerName, string name, string surname,DateTime createdDate, string fileName)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var sqlContext = scope.ServiceProvider.GetService<SqlDBContext>();
                try
                {
                    var maxId = 1;

                    if (sqlContext.UploadInfos.Any())
                    {
                        maxId = sqlContext.UploadInfos.Select(x => x.ID).Max() + 1;
                    }
                    var customerInfo = new UploadInfo()
                    {
                        //ID = maxId,
                        CustomerName = customerName,
                        Name = name,
                        Surname = surname,
                        CreatedDate = createdDate,
                        FilePath = fileName,
                        Approval = "1"
                        
                    };
                    await sqlContext.UploadInfos.AddAsync(customerInfo);
                    await sqlContext.SaveChangesAsync();
                    return new List<UploadInfo> { customerInfo };

                }
                catch(Exception ex)
                {
                    return new List<UploadInfo>();
                }
            }
        }

        public async Task<List<UploadInfo>> GetQueryService(DateTime dateTo, DateTime dateFrom,string customer,string name,string surName)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var sqlContext = scope.ServiceProvider.GetService<SqlDBContext>();
                try
                {
                    var customerInfo = new UploadInfo();
                    var results = (from res in sqlContext.UploadInfos
                                   where res.CreatedDate >= dateFrom && res.CreatedDate <= dateTo
                                   && res.CustomerName == customer
                                   && (string.IsNullOrEmpty(name) || res.Name == name)
                                   && (string.IsNullOrEmpty(surName) || res.Surname == surName)
                                   && !string.IsNullOrEmpty(res.Approval)
                                   select new UploadInfo()
                                   {
                                       CreatedDate = res.CreatedDate,
                                       CustomerName = res.CustomerName,
                                       Name = res.Name,
                                       Surname = res.Surname,
                                       FilePath = res.FilePath,
                                       Approval = res.Approval
                                   }).ToList();
                    results = results.Where(x => x.Approval == "1").ToList();

                    return results;

                }
                catch (Exception ex)
                {
                    return new List<UploadInfo>();
                }

            }
        }

        public async Task<List<UploadInfo>> GetAdminQueryService(DateTime dateTo, DateTime dateFrom, string customer, string name, string surName)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var sqlContext = scope.ServiceProvider.GetService<SqlDBContext>();
                try
                {
                    var customerInfo = new UploadInfo();
                    var results = (from res in sqlContext.UploadInfos
                                   where res.CreatedDate >= dateFrom && res.CreatedDate <= dateTo
                                   && res.CustomerName == customer
                                   && (string.IsNullOrEmpty(name) || res.Name == name)
                                   && (string.IsNullOrEmpty(surName) || res.Surname == surName)
                                   && !string.IsNullOrEmpty(res.Approval)
                                   select new UploadInfo()
                                   {
                                       ID = res.ID,
                                       CreatedDate = res.CreatedDate,
                                       CustomerName = res.CustomerName,
                                       Name = res.Name,
                                       Surname = res.Surname,
                                       FilePath = res.FilePath,
                                       Approval = res.Approval
                                   }).ToList();
                    

                    return results;
                    results = results.Where(x => x.Approval == "1").ToList();
                }
                catch (Exception ex)
                {
                    return new List<UploadInfo>();
                }

            }
        }
    }
}
