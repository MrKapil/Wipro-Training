//EmployeeRepository.cs

using RepositoryWithMVC.Repository;
using Microsoft.EntityFrameworkCore;
using RepoDesignPattern.Models;
using RepoDesignPattern.Data;


namespace RepoDesignPattern.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeContext _context;

        public EmployeeRepository(EmployeeContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employees.ToList();
        }

        public Employee GetEmployeeById(int studentId)
        {
            return _context.Employees.Find(studentId);
        }
        public int AddEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Employees.Add(employeeEntity);
                _context.SaveChanges();
                result = employeeEntity.EmployeeId;
            }

            return result;
        }

        public int UpdateEmployee(Employee employeeEntity)
        {
            int result = -1;

            if (employeeEntity != null)
            {
                _context.Entry(employeeEntity).State = EntityState.Modified;
                _context.SaveChanges();
                result = employeeEntity.EmployeeId;
            }

            return result;
        }

        public void DeleteEmployee(int employeeId)
        {
            Employee employeeEntity = _context.Employees.Find(employeeId);
            _context.Employees.Remove(employeeEntity);
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}