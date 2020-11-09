using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocToHtml.Model;
using DocToHtml.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocToHtml.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpPost(Name = "Converter")]
        //public IActionResult Converter([FromBody] Converter m)
        //{
        //    try
        //    {
        //        var ret = ConverterModule.Converter(m);
        //        return Ok(new { result = ret});
                
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
          
        //}


        public IActionResult Converter()
        {
            try
            {
                var ret = ConverterModule.Converter();
                return Ok(new { result = ret });

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}