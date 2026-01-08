namespace Presentation.Hubs;

using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

/// <summary>
/// SignalR hub for real-time OPC UA node notifications.
/// </summary>
public sealed class OpcuaNodeHub : Hub<IOpcuaNodeHubClient>
{
    private readonly ILogger<OpcuaNodeHub> _logger;

    public OpcuaNodeHub(ILogger<OpcuaNodeHub> logger)
    {
        _logger = logger;
    }

    public override async Task OnConnectedAsync()
    {
        _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _logger.LogInformation("Client disconnected: {ConnectionId}", Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

}

