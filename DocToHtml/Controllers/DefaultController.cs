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
        public IActionResult Converter([FromBody] Converter m)
        {
            var ret = (dynamic)null;
            try
            {
                ret = ConverterModule.Converter(m);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ret;
        }
    }
}