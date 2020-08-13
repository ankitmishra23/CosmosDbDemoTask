using CosmosDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDemo.Services
{
    public interface IEmp
    {
        Task<IEnumerable<Employee>> GetEmployeesAsync(string query);
        Task<Employee> GetEmployeeAsync(string id);
        Task AddEmployeeAsync(Employee employee);
        Task UpdateEmployeeAsync(string id, Employee employee);
        Task DeleteEmployeeAsync(string id);
    }
}
