using AspNetCore.Reporting;
using Microsoft.Reporting.NETCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using testReportApp.IRepo;
using testReportApp.Model;

namespace testReportApp.Controllers
{
    public class ReportController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmpRepo _empRepo;

        public ReportController(IWebHostEnvironment webHostEnvironment, IEmpRepo empRepo)
        {
            _webHostEnvironment = webHostEnvironment;
            _empRepo = empRepo;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Print()
        {
            try
            {
                string mimetype = "";
                int extention = 1;
                var path = $"{this._webHostEnvironment.WebRootPath}\\Reports\\EmployeeData.rdl";
                Dictionary<string, string> para = new Dictionary<string, string>();

                var Empdatas = await _empRepo.GetDatas();

                AspNetCore.Reporting.LocalReport localReport = new AspNetCore.Reporting.LocalReport(path);
                localReport.AddDataSource("DataSet1", Empdatas);

                var result = localReport.Execute(RenderType.Pdf, extention, para, mimetype);
                return File(result.MainStream, "application/pdf","xyz.pdf");


            }
            catch (Exception ex)
            {
                return Content($"{ex.Message}");
            }
        }


    }
}
