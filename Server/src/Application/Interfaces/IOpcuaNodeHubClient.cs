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
    /// Called when a separator station node is updated.
    /// This is sent only to clients subscribed to the SimulationFront group.
    /// </summary>
    Task SimulationFrontNode(NodeResponse node);
}
