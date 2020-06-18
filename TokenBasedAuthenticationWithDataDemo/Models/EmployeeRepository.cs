using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TokenBasedAuthenticationWithDataDemo.Models
{
    public class EmployeeRepository : IEmployeeRepository , IDisposable
    {
        UsersDBEntities user = new UsersDBEntities();
        public void Dispose()
        {
            user.Dispose();
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return user.Employees.ToList();
        }
    }
}