using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using requirement4.Models;
using System.Diagnostics;

namespace requirement4.Controllers
{
    //[ApiController]
    //[Route("[controller]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            PayslipResponse response = new PayslipResponse();

            /*foreach (var payslipList in response.getPayslip())
            {
                List
            }*/

            return View(response.getPayslip());
        }
        public IActionResult View()
        {
            PayslipResponse response = new PayslipResponse();

            /*foreach (var payslipList in response.getPayslip())
            {
                List
            }*/

            return View(response.getPayslip());
        }

        public IActionResult Submit()
        {
            PayslipSubmit response = new PayslipSubmit();
           
           

            /*foreach (var payslipList in response.getPayslip())
            {
                List
            }*/

            return View(response);
        }

  
        [HttpPost]
        public IActionResult Submit(PayslipSubmit model)
        {
            model.PayslipResponse = new PayslipResponse();
            var response = new PayslipResponse();

            if (model.PayslipRequest.Name != null && model.PayslipRequest.AnnualPay != 0 && model.PayslipRequest.AnnualPay > 0)
            {
                response.generate_monthly_payslip(model.PayslipRequest.Name, model.PayslipRequest.AnnualPay);
                model.PayslipResponse.employee_name = response.employee_name;
                model.PayslipResponse.gross_monthly_income = response.gross_monthly_income;
                model.PayslipResponse.monthly_income_tax = response.monthly_income_tax;
                model.PayslipResponse.net_monthly_income = response.net_monthly_income;
                ViewBag.dataExists = true;
            }
            else
            {
                ViewBag.errorMsg = "Name cannot be empty or Annual Pay cannot be empty/invalid";
            }
            return View(model);

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

       

    }
}