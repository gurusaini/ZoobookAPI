using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ZoobookAPI.Data;
using ZoobookAPI.Models;

namespace ZoobookAPI.Controllers
{
    /// <summary>
    /// Author: Gurjeet Singh
    /// Created On: 29/11/2023
    /// Scope: Web API controller for employee table
    /// </summary>
    public class EmployeesController : ApiController
    {
        private DataAccess objDataAccess = new DataAccess();
        // GET: api/Employees
        [HttpGet]
        public IQueryable<Employee> GetEmployees()
        {
            return objDataAccess.GetEmployees();
        }

        // GET: api/Employees/1
        [HttpGet]
        [ResponseType(typeof(Employee))]        
        public async Task<IHttpActionResult> GetEmployee(int id)
        {
            Employee employee = await objDataAccess.GetDetails(id);
            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        // PUT: api/Employees/1
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != employee.Id)
            {
                return BadRequest();
            }

            try
            {
                await objDataAccess.UpdateEmployee(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!objDataAccess.EmployeeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Employees
        [HttpPost]
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> PostEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await objDataAccess.AddEmployee(employee);
            return CreatedAtRoute("DefaultApi", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/1
        [HttpDelete]
        [ResponseType(typeof(Employee))]
        public async Task<IHttpActionResult> DeleteEmployee(int id)
        {
            Employee employee = await objDataAccess.GetDetails(id);
            if (employee == null)
            {
                return NotFound();
            }
            await objDataAccess.DeleteEmployee(employee);
            return Ok(employee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                objDataAccess.Dispose();
            }
            base.Dispose(disposing);
        }        
    }
}