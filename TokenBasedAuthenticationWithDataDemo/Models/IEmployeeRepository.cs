using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationWithDataDemo.Models
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployee();
    }
}