using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.SqlClient;

namespace requirement4.Models
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
            gross_monthly_income = monthlypay.ToString("0.00");
            monthly_income_tax = monthlytotaltax.ToString("0.00");
            net_monthly_income = netmonthlyincome.ToString("0.00");

            //unit test
           
                if (AnnualPay <= 0)
                    throw new Exception("Annual Pay Below 0 for " + name);
                //Assert.Equals(<0,AnnualPay);
          

            // console print
            Console.WriteLine("Monthly Payslip for: '" + employee_name + "'");
            Console.WriteLine("Gross Monthly Income: $" + gross_monthly_income);
            Console.WriteLine("Monthly Income Tax: $" + monthly_income_tax);
            Console.WriteLine("Net Monthly Income: $" + net_monthly_income);
            Console.WriteLine("");

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

        public  double calculateTotalTax(double annualpay)
        {
            double tax = 0;
            Boolean TAX40 = false; //indicator if the amount exceeds condition
            Boolean TAX30 = false;
            Boolean TAX20 = false;
            Boolean TAX = false;
            List<double> SalaryBracket = new List<double> {0, 20000, 40000, 80000, 180000 };
            List<double> TaxRate = new List<double> {0, 10, 20, 30, 40 };

            //Simplified calculation replacing code below using for loop
            for (int i = SalaryBracket.Count; i > 0; i--)
            {
                if (annualpay > SalaryBracket[i-1])
                {
                    if (TAX == true)
                        tax += (SalaryBracket[i] - SalaryBracket[i - 1]) * TaxRate[i - 1] / 100;
                    else
                    {
                        tax += (annualpay - SalaryBracket[i - 1]) * TaxRate[i - 1] / 100;
                        TAX = true;
                    }
                }
            }

            //Initial calculation
            /* if (annualpay > SalaryBracket[3])
             {
                 tax += (annualpay - SalaryBracket[3]) * TaxRate[3] / 100;
                 TAX40 = true;
             }

             if (annualpay > SalaryBracket[2])
             {
                 if (TAX40 == true)
                     tax += (SalaryBracket[3]- SalaryBracket[2]) * TaxRate[2] / 100;
                 else
                     tax += (annualpay - SalaryBracket[2]) * TaxRate[2] / 100;
                 TAX30 = true;
             }

             if (annualpay > SalaryBracket[1])
             {
                 if (TAX30 == true)
                     tax += SalaryBracket[1] * TaxRate[1] / 100;
                 else
                     tax += (annualpay - SalaryBracket[1]) * TaxRate[1] / 100;
                 TAX20 = true;
             }

             if (annualpay > SalaryBracket[0])
             {
                 if (TAX20 == true)
                     tax += SalaryBracket[0] * TaxRate[0] / 100;
                 else
                     tax += (annualpay - SalaryBracket[0]) * TaxRate[0] / 100;
             }

             if (annualpay <= SalaryBracket[0])
             {
                 tax = 0;
             }*/

            if (annualpay <= 0)
                throw new Exception("Annual Pay Below 0 ");

            return tax;
        }

        public List<PayslipDisplay> getPayslip()
        {
            List<PayslipDisplay> list = new List<PayslipDisplay>();

            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = ("Data Source=DESKTOP-0I48N42;Initial Catalog=FAVE;Integrated Security=True");
                con.Open();
                string query = "SELECT time_stamp,employee_name,annual_salary,monthly_income_tax FROM PAYSLIP order by time_stamp";

                SqlCommand command = new SqlCommand(query, con);



                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            string data1 = reader.GetDateTime(0).ToString();
                            string data2 = reader.GetString(1);
                            string data3 = reader.GetDouble(2).ToString();
                            string data4 = reader.GetDouble(3).ToString();
                            list.Add(new PayslipDisplay(data1, data2, data3, data4));
                    
                        }
                    }
                    else
                    {
                        Console.WriteLine("No records found in database table");
                    }

                    reader.Close();
                }
                con.Close();
                return list;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                //var list = new List<PayslipDisplay>();
                return list;

            }

        }
    }
}
