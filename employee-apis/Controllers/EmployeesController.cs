using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace employee_apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeService<Domain.Models.Employee, Data.Models.Employee> _employeeService;

        public EmployeesController(EmployeeService<Domain.Models.Employee, Data.Models.Employee> employeeService)
        {
            _employeeService = employeeService;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Domain.Models.Employee> GetAll()
        {
            var Employees = _employeeService.GetAll();
            return Employees;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Employee = _employeeService.GetOne(id);
            if (Employee == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(Employee);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Domain.Models.Employee Employee)
        {
            if (Employee == null)
                return BadRequest();

            var emp = _employeeService.Add(Employee);
            //return Created($"api/Employees/{id}", id);  //HTTP201 Resource created
            return Ok(emp);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Domain.Models.Employee Employee)
        {
            if (Employee == null || Employee.ID != id)
                return BadRequest();

            int retVal = _employeeService.Update(Employee);
            if (retVal == 0)
                return StatusCode(304);  //Not Modified
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(Employee);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _employeeService.Remove(id);
            if (retVal == 0)
                return NotFound();  //Not Found 404
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //Precondition Failed  - concurrency
            else
                return NoContent();   	     //No Content 204
        }

    }
}