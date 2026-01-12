using Application.DTOs.Responses;

namespace Application.Interfaces;

/// <summary>
/// Interface for the OPC UA node SignalR hub client methods.
/// </summary>
public interface IOpcuaNodeHubClient
{
    /// <summary>
    /// Called when a new node is created.
    /// </summary>
    Task NodeCreated(NodeResponse node);

    /// <summary>
    /// Called when a node is updated.
    /// </summary>
    Task NodeUpdated(NodeResponse node);

    /// <summary>
    /// Called when a node is deleted.
    /// </summary>
    Task NodeDeleted(string nodeName);

    /// <summary>
    /// Called when a simulation front node is updated.
    /// This is sent only to clients subscribed to the SimulationFront group.
    /// </summary>
    Task SimulationFrontNode(NodeResponse node);

    /// <summary>
    /// Called when a client subscribes to the SimulationFront group.
    /// Sends the initial state of all nodes in the group.
    /// </summary>
    Task SimulationFrontInitialState(IReadOnlyList<NodeResponse> nodes);
}
