using DaJet.Agent.Service.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace DaJet.Agent.Service
{
    [ApiController][Route("cfg")]
    public sealed class WebApiController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly AuthenticationProvider _authenticator;
        private readonly JsonSerializerOptions _serializerOptions;
        public WebApiController(IServiceProvider serviceProvider, AuthenticationProvider authenticator)
        {
            _authenticator = authenticator;
            _serviceProvider = serviceProvider;
            
            _serializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
        }

        [HttpGet("version")]
        [Produces("text/plain")]
        public ContentResult Home()
        {
            string version = typeof(Program).Assembly.GetName().Version.ToString();

            return Content(version, "text/plain", Encoding.UTF8);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] AppUser model)
        {
            var user = _authenticator.Authenticate(model.Name, model.Pswd);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(user);
        }


        [HttpGet("")][Authorize()]
        [Produces("application/json")]
        public ActionResult ConfigInfo()
        {
            var options = _serviceProvider?.GetService<IOptions<AppSettings>>();

            if (options == null)
            {
                return NotFound();
            }

            string json = JsonSerializer.Serialize(options, _serializerOptions);

            return Content(json, "application/json; charset=utf-8", Encoding.UTF8);
        }
    }
}