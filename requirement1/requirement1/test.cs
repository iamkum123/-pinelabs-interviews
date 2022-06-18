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


    }
}
