using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Service;
using Serilog;

namespace employee_apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly DepartmentService<Domain.Models.Department, Data.Models.Department> _departmentService;

        public DepartmentsController(DepartmentService<Domain.Models.Department, Data.Models.Department> departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public IEnumerable<Domain.Models.Department> GetAll()
        {
            var Departments = _departmentService.GetAll();
            return Departments;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var Department = _departmentService.GetOne(id);
            if (Department == null)
            {
                Log.Error("GetById({ ID}) NOT FOUND", id);
                return NotFound();
            }

            return Ok(Department);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Domain.Models.Department Department)
        {
            if (Department == null)
                return BadRequest();

            var Dep = _departmentService.Add(Department);
            //return Created($"api/Employees/{id}", id);  //HTTP201 Resource created
            return Ok(Dep);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Domain.Models.Department Department)
        {
            if (Department == null || Department.ID != id)
                return BadRequest();

            int retVal = _departmentService.Update(Department);
            if (retVal == 0)
                return StatusCode(304);  //Not Modified
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //412 Precondition Failed  - concurrency
            else
                return Accepted(Department);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            int retVal = _departmentService.Remove(id);
            if (retVal == 0)
                return NotFound();  //Not Found 404
            else if (retVal == -1)
                return StatusCode(412, "DbUpdateConcurrencyException");  //Precondition Failed  - concurrency
            else
                return NoContent();   	     //No Content 204
        }
    }
}