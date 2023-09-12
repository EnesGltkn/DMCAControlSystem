using DMCATXT.Models;
using DMCATXT.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace DMCATXT.Controllers
{
    public class DmcaController : Controller
    {
        private IDataService _dataService;
        public DmcaController(IDataService dataService)
        {
            _dataService = dataService;
        }

        public ActionResult Index()
        {
            UploadInfo uploadInfo = new UploadInfo();
            return PartialView("UploadPage", uploadInfo);
        }

        [HttpPost]
        public ActionResult Save(UploadInfo upload)
        {
            var time = DateTime.Now;
            if (upload.File != null && upload.File.Length > 0)
            {
                string fileName = upload.File.FileName;

                var filePath = Path.Combine("C://Users//burak//source//repos//DMCATXT//DMCATXT//uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    upload.File.CopyTo(stream);
                }
                var results = _dataService.GetDataService(upload.CustomerName, upload.Name, upload.Surname, time, fileName);
                return PartialView("SaveResult", results);
            }

            return PartialView("Error");
        }

        [HttpPost]
        public IActionResult SaveFile(UploadInfo model)
        {
            if (model.File != null && model.File.Length > 0)
            {
                string fileName = model.File.FileName;

                var filePath = Path.Combine("C://Users//burak//source//repos//DMCATXT//DMCATXT//uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }
            }

            return View("Index");
        }

    }

}
