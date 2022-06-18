using System.Data.SqlClient;

namespace requirement3.Models
{
    public class PayslipResponse
    {
        public string? employee_name { get; set; }

        public string? gross_monthly_income { get; set; }

        //public int TemperatureF => 32 + (int)(200 / 0.5556);

        public string? monthly_income_tax { get; set; }

        public string? net_monthly_income { get; set; }

        // public double MonthlyIncomeTax { get; set; }

        //public double NetMonthlyIncome { get; set; }

        public void generate_monthly_payslip(string name, double AnnualPay)
        {
            DateTime dateTime = DateTime.Now;
            //calculation for attributes
            double monthlypay = Math.Round(AnnualPay / 12, 2);
            double monthlytotaltax = Math.Round(calculateTotalTax(AnnualPay) / 12, 2);
            double netmonthlyincome = Math.Round(monthlypay - monthlytotaltax, 2);


            //To assign value to attributes
            employee_name = name;
            gross_monthly_income = monthlypay.ToString("#.00");
            monthly_income_tax = monthlytotaltax.ToString("#.00");
            net_monthly_income = netmonthlyincome.ToString("#.00");

            //unit test
            if (AnnualPay < 0)
                throw new Exception("Annual Pay Below 0 for " + name);

            // console print
            Console.WriteLine("Monthly Payslip for: '" + employee_name + "'");
            Console.WriteLine("Gross Monthly Income: $" + gross_monthly_income);
            Console.WriteLine("Monthly Income Tax: $" + monthly_income_tax);
            Console.WriteLine("Net Monthly Income: $" + net_monthly_income);

            //Insert into DB table
              try
              {
                  SqlConnection con = new SqlConnection();
                  con.ConnectionString = ("Data Source=DESKTOP-0I48N42;Initial Catalog=FAVE;Integrated Security=True");
                  con.Open();
                  String st = "INSERT INTO PAYSLIP VALUES(@time_stamp,@employee_name,@annual_salary,@monthly_income_tax)";
                  SqlCommand cmd = new SqlCommand(st, con);
                  cmd.Parameters.AddWithValue("@time_stamp", dateTime);
                  cmd.Parameters.AddWithValue("@employee_name", employee_name);
                  cmd.Parameters.AddWithValue("@annual_salary", AnnualPay);
                  cmd.Parameters.AddWithValue("@monthly_income_tax", monthlytotaltax);
                  cmd.ExecuteNonQuery();
                  con.Close();
              }
              catch (Exception e)
              {
                  Console.WriteLine("Cannot insert");

              }

        }

        public static double calculateTotalTax(double annualpay)
        {
            double tax = 0;
            Boolean TAX40 = false; //indicator if the amount exceeds condition
            Boolean TAX30 = false;
            Boolean TAX20 = false;
            //  Boolean TAX10 = false;

            if (annualpay > 180000)
            {
                tax += (annualpay - 180000) * 4 / 10;
                TAX40 = true;
            }

            if (annualpay > 80000)
            {
                if (TAX40 == true)
                    tax += 100000 * 3 / 10;
                else
                    tax += (annualpay - 80000) * 3 / 10;
                TAX30 = true;
            }

            if (annualpay > 40000)
            {
                if (TAX30 == true)
                    tax += 40000 * 2 / 10;
                else
                    tax += (annualpay - 40000) * 2 / 10;
                TAX20 = true;
            }

            if (annualpay > 20000)
            {
                if (TAX20 == true)
                    tax += 20000 * 1 / 10;
                else
                    tax += (annualpay - 20000) * 1 / 10;
            }

            if (annualpay <= 20000)
            {
                tax = 0;
            }

            return tax;
        }
    }
}
