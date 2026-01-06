namespace Presentation.Controllers;

using Domain.Interfaces;
using Domain.DTOs;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("opcua")]
public class NodeOpcuaController : ControllerBase
{
    private readonly INodeOpcuaService _nodeService;

    public NodeOpcuaController(INodeOpcuaService nodeService)
    {
        _nodeService = nodeService;
    }

    #region Get Nodes
    /// <summary>
    /// Gets all OPC UA nodes.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<NodeOpcuaDto>), StatusCodes.Status200OK)]
    public IActionResult GetNodes()
    {
        var nodes = _nodeService.GetAllNodes();
        return Ok(nodes);
    }
    #endregion

    #region Get Node
    /// <summary>
    /// Gets a specific OPC UA node by name.
    /// </summary>
    /// <param name="name">The node name</param>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetNode([FromRoute] string name)
    {
        var node = _nodeService.GetNode(name);
        
        if (node is null)
        {
            return NotFound(new { message = $"Node '{name}' not found" });
        }

        return Ok(node);
    }
    #endregion

    #region Create Node
    /// <summary>
    /// Creates a new OPC UA node.
    /// </summary>
    /// <param name="request">The node creation request</param>
    [HttpPost]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
    public IActionResult CreateNode([FromBody] NodeOpcuaDto request)
    {
        var existingNode = _nodeService.GetNode(request.Name);
        if (existingNode is not null)
        {
            return Conflict(new { message = $"Node '{request.Name}' already exists" });
        }

        try
        {
            var node = _nodeService.SetNode(request.Name, request.Value);
            return CreatedAtAction(nameof(GetNode), new { name = node.Name }, node);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable, 
                new { message = ex.Message });
        }
    }
    #endregion

    #region Update Node
    /// <summary>
    /// Updates an existing OPC UA node.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="request">The update request</param>
    [HttpPut("{name}")]
    [ProducesResponseType(typeof(NodeOpcuaDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateNode([FromRoute] string name, [FromBody] NodeValueOpcuaDto request)
    {
        var existingNode = _nodeService.GetNode(name);
        if (existingNode is null)
        {
            return NotFound(new { message = $"Node '{name}' not found" });
        }

        var node = _nodeService.SetNode(name, request.Value);
        return Ok(node);
    }
    #endregion

    #region Delete Node
    /// <summary>
    /// Deletes an OPC UA node.
    /// </summary>
    /// <param name="name">The node name</param>
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteNode([FromRoute] string name)
    {
        var removed = _nodeService.RemoveNode(name);
        
        if (!removed)
        {
            return NotFound(new { message = $"Node '{name}' not found" });
        }

        return NoContent();
    }
    #endregion
}