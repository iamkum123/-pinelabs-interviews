using Azure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using requirement1.Models;
using System.Diagnostics;

namespace requirement1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        [Route("~/api/submitpayslip")]
        [HttpGet]
        public void GetPayslip()
        {


            PayslipResponse response = new PayslipResponse();

            response.generate_monthly_payslip("Ren", 60000);
            response.generate_monthly_payslip("John", 200000);
            response.generate_monthly_payslip("Tim", 80150);

            // var jsonResponse = JsonConvert.SerializeObject(response.getPayslip());
            //return jsonResponse;


        }
    }
}