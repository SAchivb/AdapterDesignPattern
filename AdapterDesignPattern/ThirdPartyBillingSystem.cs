using System;
using System.Collections.Generic;
namespace AdapterDesignPattern
{
    
    public class ThirdPartyBillingSystem
    {
       //3rdparty billingsystem emp accept info of each emp salay
        public void ProcessSalary(List<Employee> listEmployee)
        {
            foreach (Employee employee in listEmployee)
            {
                Console.WriteLine("Rs." + employee.Salary + " Salary Credited to " + employee.Name + " Account");
            }
        }
    }
}
