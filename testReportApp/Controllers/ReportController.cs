using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.NETCore;
using System.IO;
using System;
using testReportApp.IRepo;
using System.Threading.Tasks;

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
                var reportPath = Path.Combine(_webHostEnvironment.WebRootPath, "Reports", "EmployeeData.rdl");
                var empData = await _empRepo.GetDatas(); 
                LocalReport report = new LocalReport();

                var reportDefinition = System.IO.File.ReadAllBytes(reportPath);
                report.LoadReportDefinition(new MemoryStream(reportDefinition));

                report.DataSources.Add(new ReportDataSource("DataSet1", empData));

                // Set any report parameters if needed
                // var parameters = new ReportParameter("Parameter1", "Parameter value");
                // report.SetParameters(new[] { parameters });

                byte[] pdf = report.Render("PDF");
                return File(pdf, "application/pdf");                //, "GeneratedReport.pdf"
            }
            catch (Exception ex)
            {
                return Content($"{ex.Message}");
            }
        }


    }
}
