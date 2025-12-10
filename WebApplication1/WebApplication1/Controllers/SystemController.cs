using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace OdevWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        [HttpGet("attribute-map")]
        public ActionResult GetAttributeMap()
        {
            var asm = Assembly.GetExecutingAssembly();
            var controllers = asm.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(Microsoft.AspNetCore.Mvc.ControllerBase)))
                .Select(t => new
                {
                    Controller = t.FullName,
                    Actions = t.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .Where(m => m.IsPublic)
                        .Select(m => new
                        {
                            Name = m.Name,
                            HttpAttributes = m.GetCustomAttributes()
                                .Where(a => a.GetType().Name.StartsWith("Http"))
                                .Select(a => a.GetType().Name).ToArray(),
                            AllAttributes = m.GetCustomAttributes().Select(a => a.GetType().Name).ToArray()
                        }).ToArray()
                }).ToArray();

            return Ok(new { Controllers = controllers });
        }
    }
}
