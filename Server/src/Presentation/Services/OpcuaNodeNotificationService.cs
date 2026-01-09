namespace Presentation.Services;

using Application.DTOs.Responses;
using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.SignalR;
using Presentation.Hubs;

/// <summary>
/// Service for sending real-time notifications to connected SignalR clients.
/// </summary>
public sealed class OpcuaNodeNotificationService : IOpcuaNodeNotificationService
{
    private readonly IHubContext<OpcuaNodeHub, IOpcuaNodeHubClient> _hubContext;
    private readonly ILogger<OpcuaNodeNotificationService> _logger;

    public OpcuaNodeNotificationService(
        IHubContext<OpcuaNodeHub, IOpcuaNodeHubClient> hubContext,
        ILogger<OpcuaNodeNotificationService> logger)
    {
        _hubContext = hubContext;
        _logger = logger;
    }

    /// <inheritdoc/>
    public async Task NotifySimulationFrontNodeAsync(NodeResponse node, CancellationToken cancellationToken = default)
    {
        if (!SimulationFrontNodeIds.IsSimulationFrontNode(node.Name)) return;
        
        // Map to alias name
        NodeResponse response = new NodeResponse(SimulationFrontNodeIds.NodeIdToAlias[node.Name], node.Value);

        _logger.LogDebug(
            "Notifying SimulationFront group of node update: ({AliasName})",
            response.Name);

        await _hubContext.Clients
            .Group(SimulationFrontNodeIds.GroupName)
            .SimulationFrontNode(response);
    }
}
