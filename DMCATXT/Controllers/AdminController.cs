using DMCATXT.Models;
using DMCATXT.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace DMCATXT.Controllers
{
    public class AdminController : Controller
    {
        private IDataService _dataService;

        public AdminController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            UploadInfo uploadInfo = new UploadInfo();
            return PartialView("AdminPageResult", uploadInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime dateTo, DateTime dateFrom, string customer, string name, string surName)
        {
            var results = await _dataService.GetAdminQueryService(dateTo, dateFrom, customer, name, surName);
            return PartialView("QueryResult", results);
        }

        public IActionResult DownloadFile(string fileName)
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "C://Users//burak//source//repos//DMCATXT//DMCATXT//uploads", fileName);
            if (System.IO.File.Exists(filePath))
            {
                var fileBytes = System.IO.File.ReadAllBytes(filePath);
                return File(fileBytes, "application/octet-stream", fileName);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateApproval(List<int> ids)
        {
            foreach(var id in ids)
            {
                await _dataService.UpdateApproval(id);
            }
            return Ok();
        }



    }
}
