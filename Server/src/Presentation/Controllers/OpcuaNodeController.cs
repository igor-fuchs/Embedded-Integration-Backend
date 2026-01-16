namespace Presentation.Controllers;

using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Controller for OPC UA node operations.
/// </summary>
[ApiController]
[Route("api/opcua-nodes")]
[Produces("application/json")]
public sealed class OpcuaNodeController : ControllerBase
{
    private readonly IOpcuaNodeService _nodeService;

    public OpcuaNodeController(IOpcuaNodeService nodeService)
    {
        _nodeService = nodeService;
    }

    /// <summary>
    /// Gets all OPC UA nodes.
    /// </summary>
    /// <response code="200">Returns the list of nodes</response>
    [HttpGet]
    [ProducesResponseType(typeof(NodeListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetRegisteredNodes(CancellationToken cancellationToken)
    {
        var result = await _nodeService.GetRegisteredNodesAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets the names of all registered OPC UA nodes from configuration.
    /// </summary>
    /// <response code="200">Returns the list of node names</response>
    [HttpGet("node-names")]
    [ProducesResponseType(typeof(NodeNamesListResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetNodeNames(CancellationToken cancellationToken)
    {
        var result = await _nodeService.GetNodeNamesAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Gets a specific OPC UA node by name.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Returns the node</response>
    /// <response code="404">Node not found</response>
    [HttpGet("{name}")]
    [ProducesResponseType(typeof(NodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetNode([FromRoute] string name, CancellationToken cancellationToken)
    {
        var result = await _nodeService.GetNodeByNameAsync(name, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Creates a new OPC UA node.
    /// </summary>
    /// <param name="request">The node creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="201">Node created successfully</response>
    /// <response code="400">Invalid request</response>
    /// <response code="409">Node already exists</response>
    /// <response code="503">Node limit exceeded</response>
    [HttpPost]
    [ProducesResponseType(typeof(NodeResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(void), StatusCodes.Status503ServiceUnavailable)]
    public async Task<IActionResult> CreateNode([FromBody] CreateNodeRequest request, CancellationToken cancellationToken)
    {
        var result = await _nodeService.CreateNodeAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetNode), new { name = result.Name }, result);
    }

    /// <summary>
    /// Updates an existing OPC UA node.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="request">The update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="200">Node updated successfully</response>
    /// <response code="400">Invalid request</response>
    /// <response code="404">Node not found</response>
    [HttpPut("{name}")]
    [ProducesResponseType(typeof(NodeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateNode(
        [FromRoute] string name,
        [FromBody] UpdateNodeRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _nodeService.UpdateNodeAsync(name, request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Deletes an OPC UA node.
    /// </summary>
    /// <param name="name">The node name</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <response code="204">Node deleted successfully</response>
    /// <response code="404">Node not found</response>
    [HttpDelete("{name}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteNode([FromRoute] string name, CancellationToken cancellationToken)
    {
        await _nodeService.DeleteNodeAsync(name, cancellationToken);
        return NoContent();
    }
}
