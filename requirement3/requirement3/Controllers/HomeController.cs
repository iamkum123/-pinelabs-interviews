using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using requirement3.Models;
using System.Diagnostics;

namespace requirement3.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        [HttpPost]
        public string SubmitPayslip(PayslipRequest req)
        {
            if (ModelState.IsValid)
            {
                //var monthlyData = generate_monthly_payslip(name, annualpay);

                PayslipResponse response = new PayslipResponse();
                response.generate_monthly_payslip(req.Name, req.AnnualPay);


                var jsonResponse = JsonConvert.SerializeObject(response);
                return jsonResponse;

            }
            else
            {
                return "Error";
            }
        }

    }
}