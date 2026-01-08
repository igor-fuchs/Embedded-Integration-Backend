namespace Presentation.Services;

using Application.DTOs.Responses;
using Application.Interfaces;
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
    public async Task NotifyNodeCreatedAsync(NodeResponse node, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Notifying clients of node creation: {NodeName}", node.Name);
        await _hubContext.Clients.All.NodeCreated(node);
    }

    /// <inheritdoc/>
    public async Task NotifyNodeUpdatedAsync(NodeResponse node, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Notifying clients of node update: {NodeName}", node.Name);
        await _hubContext.Clients.All.NodeUpdated(node);
    }

    /// <inheritdoc/>
    public async Task NotifyNodeDeletedAsync(string nodeName, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Notifying clients of node deletion: {NodeName}", nodeName);
        await _hubContext.Clients.All.NodeDeleted(nodeName);
    }
}
