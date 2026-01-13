namespace Presentation.Hubs;

using Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

/// <summary>
/// SignalR hub for real-time OPC UA node notifications.
/// </summary>
public sealed class OpcuaNodeHub : Hub<IOpcuaNodeHubClient>
{
    private readonly ILogger<OpcuaNodeHub> _logger;
    private readonly IOpcuaNodeNotificationService _notificationService;
    private readonly ISimulationFrontNodeProvider _nodeProvider;

    public OpcuaNodeHub(
        ILogger<OpcuaNodeHub> logger,
        IOpcuaNodeNotificationService notificationService,
        ISimulationFrontNodeProvider nodeProvider)
    {
        _logger = logger;
        _notificationService = notificationService;
        _nodeProvider = nodeProvider;
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

    /// <summary>
    /// Subscribes the current client to the Separator Station group to receive updates.
    /// Sends the initial state of all nodes upon subscription.
    /// </summary>
    public async Task SubscribeToSimulationFront()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, _nodeProvider.GroupName);
        _logger.LogInformation(
            "Client {ConnectionId} subscribed to {GroupName} group",
            Context.ConnectionId,
            _nodeProvider.GroupName);

        // Send initial state to the newly subscribed client
        var initialState = await _notificationService.GetSimulationFrontInitialStateAsync();
        await Clients.Caller.SimulationFrontInitialState(initialState);
        
        _logger.LogDebug(
            "Sent initial state to client {ConnectionId}: {NodeCount} nodes",
            Context.ConnectionId,
            initialState.Count);
    }

    /// <summary>
    /// Unsubscribes the current client from the Separator Station group.
    /// </summary>
    public async Task UnsubscribeFromSimulationFront()
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, _nodeProvider.GroupName);
        _logger.LogInformation(
            "Client {ConnectionId} unsubscribed from {GroupName} group",
            Context.ConnectionId,
            _nodeProvider.GroupName);
    }
}

