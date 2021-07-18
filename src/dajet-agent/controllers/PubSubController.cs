using DaJet.Agent.Service.Model;
using DaJet.Agent.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace DaJet.Agent.Service.Controllers
{
    [Route("dajet")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class PubSubServiceController : ControllerBase
    {
        private const string APPLICATION_JSON = "application/json";
        private IPubSubService PubSubService { get; }
        public PubSubServiceController(IPubSubService service)
        {
            PubSubService = service;
        }
        [HttpGet("nodes")] public ActionResult SelectNodes()
        {
            List<Node> list = PubSubService.SelectNodes();
            string json = JsonSerializer.Serialize(list);
            return Content(json, APPLICATION_JSON, Encoding.UTF8);
        }
        [HttpGet("nodes/{id}")] public ActionResult SelectNode([FromRoute] int id)
        {
            Node node = PubSubService.SelectNode(id);
            if (node == null)
            {
                return NotFound();
            }
            else
            {
                string json = JsonSerializer.Serialize(node);
                return Content(json, APPLICATION_JSON, Encoding.UTF8);
            }
        }
        [HttpPost("nodes")] public ActionResult CreateNode([FromBody] Node node)
        {
            PubSubService.CreateNode(node);
            string json = JsonSerializer.Serialize(node);
            HttpContext.Response.StatusCode = 201;
            _ = HttpContext.Response.Headers.TryAdd("Location", "dajet/nodes/" + node.Id.ToString());
            return Content(json, APPLICATION_JSON, Encoding.UTF8);
        }
        [HttpPut("nodes")] public ActionResult UpdateNode([FromBody] Node node)
        {
            PubSubService.UpdateNode(node);
            return Ok();
        }
        [HttpDelete("nodes/{id}")] public ActionResult DeleteNode([FromRoute] int id)
        {
            PubSubService.DeleteNode(new Node() { Id = id });
            return Ok();
        }
    }
}
