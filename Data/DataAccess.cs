using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ZoobookAPI.Models;

namespace ZoobookAPI.Data
{
    /// <summary>
    /// Author: Gurjeet Singh
    /// Created On: 29/11/2023
    /// Scope: Database action library for employee table
    /// </summary>
    public class DataAccess
    {
        private ZoobookAPIContext objZoobookAPI = new ZoobookAPIContext();
        public IQueryable<Employee> GetEmployees()
        {
            return objZoobookAPI.Employees;
        }
        public async Task<Employee> GetDetails(int Id)
        {
            return await objZoobookAPI.Employees.FindAsync(Id);
        }
        public async Task<int> AddEmployee(Employee employee)
        {
            objZoobookAPI.Employees.Add(employee);
            var result = await objZoobookAPI.SaveChangesAsync();
            return result;
        }
        public async Task<int> UpdateEmployee(Employee employee)
        {
            objZoobookAPI.Entry(employee).State = EntityState.Modified;
            var result = await objZoobookAPI.SaveChangesAsync();
            return result;
        }
        public async Task<int> DeleteEmployee(Employee employee)
        {
            objZoobookAPI.Employees.Remove(employee);
            var result = await objZoobookAPI.SaveChangesAsync();
            return result;
        }
        public bool EmployeeExists(int Id)
        {
            return objZoobookAPI.Employees.Count(e => e.Id == Id) > 0;
        }
        public void Dispose()
        {
            objZoobookAPI.Dispose();
        }
    }
}