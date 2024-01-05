using Microsoft.AspNetCore.Mvc;
using HDIApi.Models;
using System.Collections.Generic;
using HDIApi.Providers;
using HDIApi.DTOs;
using HDIApi.Bussines.Interface;
using Microsoft.AspNetCore.Authorization;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EmployeeController : ControllerBase
    {

        private IEmployeeProvider employeeProvider;


        public EmployeeController([FromBody] IEmployeeProvider _employeeProvider)
        {
            employeeProvider = _employeeProvider;


        }


        [HttpPost("SetNewEmployee")]
        public async Task<IActionResult> SetNewEmployee([FromBody] EmployeeDTO newEmployee)
        {
            int code = await employeeProvider.SetNewEmployee(newEmployee);
            return StatusCode(code);
        }

        [HttpGet("GetEmployeeById/{idEmployee}")]
        public async Task<IActionResult> GetEmployeeById(string idEmployee)
        {
            (int code, EmployeeDTO employeeDTO) = await employeeProvider.GetEmployeeById(idEmployee);
            if (code == 200)
                return Ok(employeeDTO);
            else
                return StatusCode(code);
        }

          [HttpPost("SetUpdateEmployee")]
        public async Task<IActionResult> SetUpdateEmployee([FromBody] EmployeeDTO employee)
        {
            int code = await employeeProvider.SetUpdateEmployee(employee);
            return StatusCode(code);
        }

          [HttpGet("GetEmployeeList")]
        public async Task<IActionResult> GetEmployeeList()
        {
            (int code, List<EmployeeDTO> employeeList) = await employeeProvider.GetEmployeeList();
            if (code == 200)
                return Ok(employeeList);
            else
                return StatusCode(code);
        }

       
    }

}
