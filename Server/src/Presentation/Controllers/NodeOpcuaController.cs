namespace Presentation.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("opcua-nodes")]
public class NodeOpcuaController : ControllerBase
{
    [HttpGet]
    public IActionResult GetNodes()
    {
        return Ok("Get nodes");
    }

    [HttpPost]
    public IActionResult PutNodes(string name, object value)
    {

        return Ok($"Received node: {name} with value: {value}");
    }
}