namespace requirement4.Models
{
    public class PayslipDisplay
    {

        public string time_stamp { get; set; }
        public string employee_name { get; set; }
        public string annual_salary { get; set; }
        public string monthly_income_tax { get; set; }

        public PayslipDisplay(string time_stamp, string employee_name, string annual_salary, string monthly_income_tax)
        {
            this.time_stamp = time_stamp;
            this.employee_name = employee_name;
            this.annual_salary = annual_salary;
            this.monthly_income_tax = monthly_income_tax;
        }

        public PayslipDisplay()
        {
            this.time_stamp = DateTime.Now.ToString();
            this.employee_name = "";
            this.annual_salary = "";
            this.monthly_income_tax = "";
        }
    }
}
