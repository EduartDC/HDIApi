using Microsoft.AspNetCore.Mvc;
using HDIApi.Models;
using System.Collections.Generic;
using HDIApi.Providers;
using HDIApi.DTOs;
using HDIApi.Bussines.Interface;

namespace HDIApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DriverController : ControllerBase
    {
     
        private IDriverProvider driverProvider;


        public DriverController( [FromBody] IDriverProvider _driverProvider)
        {
            driverProvider = _driverProvider;
            

        }

        
        [HttpPost("SetNewDriver")]
        public async Task<IActionResult> SetNewDriver([FromBody] DriverclientDTO newDriverClient)
        {
           int code =  await driverProvider.SetNewDriver(newDriverClient);

            return StatusCode(code);
        }

        

    }

}
