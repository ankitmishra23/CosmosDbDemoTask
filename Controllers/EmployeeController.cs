using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CosmosDemo.Models;
using CosmosDemo.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmosDemo.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmp repository;
        public EmployeeController(IEmp emp)
        {
            this.repository = emp;
        }

        /// <summary>
        /// This request adds the document to cosmos database.
        /// </summary>
        /// <param name="emp"></param>
        /// <returns></returns>
        [HttpPost("Create")]
        //[ActionName("Create")]
        // [ValidateAntiForgeryToken]
        public async Task<ActionResult> createAsync([Bind("Id,FirstName,LastName,Gender,Salary,Address")] Employee emp)
        {
            await this.repository.AddEmployeeAsync(emp);
            return Ok();
        }

        /// <summary>
        /// This request gets the details of the employee based on it's Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("EmployeeDetailById")]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            return Ok(await this.repository.GetEmployeeAsync(id));
        }

        /// <summary>
        /// This request updates the employee detail by using employee id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("UpdateEmployeeDetails")]
        public async Task<ActionResult> UpdateAsync(string id, Employee employee)
        {
            await this.repository.UpdateEmployeeAsync(id, employee);
            return Ok();
        }

        /// <summary>
        /// This request deletes the document from the DB based of id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("DeleteEmployeeDetailById")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            await this.repository.DeleteEmployeeAsync(id);
            return Ok();
        }

        /// <summary>
        /// This request is helpful in performing all kinds of Cosmos sql queries.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("GetDetailsByExecutingSelectSQLQuery")]
        public async Task<ActionResult> GetDetailsByExecutingSQLQuery(string query)
        {
            return Ok(await this.repository.GetEmployeesAsync(query));
        }
    }
}
