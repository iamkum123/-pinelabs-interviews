using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using requirement4.Models;
using System.Diagnostics;

namespace requirement4.Controllers
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

        [Route("~/api/getpayslip")]
        [HttpGet]
        public string GetPayslip()
        {


            PayslipResponse response = new PayslipResponse();

            //To console print all exisiting payslip from DB
            foreach (var payslipList in response.getPayslip())
            {
                Console.WriteLine(JsonConvert.SerializeObject(payslipList));
            }


            //To return all existing payslip with root name
            var Payslipwrapper = new
            {
                salary_computations = response.getPayslip()

            };

            var jsonResponse = JsonConvert.SerializeObject(Payslipwrapper);
            return jsonResponse;


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