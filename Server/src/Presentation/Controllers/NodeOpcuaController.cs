namespace Presentation.Controllers;

using Application.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class NodeOpcuaController : ControllerBase
{
    private readonly INodeOpcuaService _nodeService;

    public NodeOpcuaController(INodeOpcuaService nodeService)
    {
        _nodeService = nodeService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NodeOpcuaDto>), StatusCodes.Status200OK)]
    public IActionResult GetNodes()
    {
        var nodes = _nodeService.GetAllNodes();
        return Ok(nodes);
    }

    [HttpGet("{name}")]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetNode(string name)
    {
        var node = _nodeService.GetNode(name);
        
        if (node is null)
        {
            return NotFound($"Node '{name}' not found");
        }

        return Ok(node);
    }

    [HttpPost]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateNode([FromBody] NodeOpcuaDto nodeDto)
    {
        if (string.IsNullOrWhiteSpace(nodeDto.name))
        {
            return BadRequest("Node name is required");
        }

        var node = _nodeService.SetNode(nodeDto.name, nodeDto.value);
        return CreatedAtAction(nameof(GetNode), new { name = node.name }, node);
    }

    [HttpPut("{name}")]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult UpdateNode(string name, [FromBody] object value)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Node name is required");
        }

        var node = _nodeService.SetNode(name, value);
        return Ok(node);
    }

    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteNode(string name)
    {
        var removed = _nodeService.RemoveNode(name);
        
        if (!removed)
        {
            return NotFound($"Node '{name}' not found");
        }

        return NoContent();
    }
}