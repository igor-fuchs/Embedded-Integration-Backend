namespace Presentation.Hubs;

using Application.Interfaces;
using Domain.Constants;
using Microsoft.AspNetCore.SignalR;

/// <summary>
/// SignalR hub for real-time OPC UA node notifications.
/// </summary>
public sealed class OpcuaNodeHub : Hub<IOpcuaNodeHubClient>
{
    private readonly ILogger<OpcuaNodeHub> _logger;
    private readonly IOpcuaNodeNotificationService _notificationService;

    public OpcuaNodeHub(
        ILogger<OpcuaNodeHub> logger,
        IOpcuaNodeNotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
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
        await Groups.AddToGroupAsync(Context.ConnectionId, SimulationFrontNodeIds.GroupName);
        _logger.LogInformation(
            "Client {ConnectionId} subscribed to {GroupName} group",
            Context.ConnectionId,
            SimulationFrontNodeIds.GroupName);

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
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, SimulationFrontNodeIds.GroupName);
        _logger.LogInformation(
            "Client {ConnectionId} unsubscribed from {GroupName} group",
            Context.ConnectionId,
            SimulationFrontNodeIds.GroupName);
    }
}

