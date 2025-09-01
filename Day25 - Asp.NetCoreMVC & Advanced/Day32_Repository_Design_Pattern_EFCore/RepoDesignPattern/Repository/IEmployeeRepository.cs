using System;
using System.Collections.Generic;
using RepoDesignPattern.Models;

namespace RepoDesignPattern.Repository
{
    public interface IEmployeeRepository : IDisposable
    {
        IEnumerable<Employee> GetAllEmployee();
        
        Employee GetEmployeeById(int employeeId);
        int AddEmployee(Employee employeeEntity);
        int UpdateEmployee(Employee employeeEntity);
        void DeleteEmployee(int employeeId);
    }
}