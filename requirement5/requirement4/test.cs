using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace requirement4
{

    [TestClass()]
    public class test
    {
        [TestMethod()]
        public void TestZeroFunction()
        {
            Models.PayslipResponse e1 = new Models.PayslipResponse();

            try
            {
                var Attempt1 = e1.calculateTotalTax(60000);
                var Attempt2 = e1.calculateTotalTax(-50);
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

            var Attempt1 = e1.calculateTotalTax(60000);
            var Attempt2 = e2.calculateTotalTax(80150);
            var Attempt3 = e3.calculateTotalTax(200000);

            Assert.AreEqual(6000, Math.Round(Convert.ToDouble(Attempt1)));
            Assert.AreEqual(10045, Math.Round(Convert.ToDouble(Attempt2)));
            Assert.AreEqual(48000, Math.Round(Convert.ToDouble(Attempt3)));
            


        }


    }
}
