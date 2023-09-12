using DMCATXT.Models;
using DMCATXT.Services;
using Microsoft.AspNetCore.Mvc;

namespace DMCATXT.Controllers
{
    public class PageController : Controller
    {
        private IDataService _dataService;
        public PageController(IDataService dataService) 
        {
            _dataService = dataService;
        }

        public ActionResult Index()
        {
            UploadInfo uploadInfo = new UploadInfo();
            return PartialView("PageResult", uploadInfo);
        }

        [HttpGet]
        public async Task<IActionResult> Get(DateTime dateTo, DateTime dateFrom, string customer, string name, string surName)
        {
            var results = await _dataService.GetQueryService(dateTo, dateFrom, customer, name, surName);
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

    }
}
