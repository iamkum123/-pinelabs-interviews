using Microsoft.VisualStudio.TestTools.UnitTesting;
using requirement1.Models;

namespace requirement1
{

    [TestClass()]
    public class test
    {
        [TestMethod()]
        public void TestFunction()
        {
            PayslipResponse e1 = new PayslipResponse();

            try
            {
                e1.generate_monthly_payslip("Ren", 60000);
                e1.generate_monthly_payslip("John", 200000);
                e1.generate_monthly_payslip("Tim", 80150);
                e1.generate_monthly_payslip("Sarah", -500);
            }


            catch (Exception e)
            {

                Assert.Fail(e.Message);
            }

        }

        [TestMethod()]
        public void TestResultFunction()
        {
            Models.PayslipResponse e1 = new Models.PayslipResponse();
            Models.PayslipResponse e2 = new Models.PayslipResponse();
            Models.PayslipResponse e3 = new Models.PayslipResponse();

            e1.generate_monthly_payslip("tim", 60000);
            e2.generate_monthly_payslip("tim", 80150);
            e3.generate_monthly_payslip("tim", 200000);

            Assert.AreEqual(6000, Math.Round(Convert.ToDouble(e1.monthly_income_tax) * 12));
            Assert.AreEqual(10045, Math.Round(Convert.ToDouble(e2.monthly_income_tax) * 12));
            Assert.AreEqual(48000, Math.Round(Convert.ToDouble(e3.monthly_income_tax) * 12));



        }


    }
}
