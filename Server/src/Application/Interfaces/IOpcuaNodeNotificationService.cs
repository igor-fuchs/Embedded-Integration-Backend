namespace Application.Interfaces;

using Application.DTOs.Responses;

/// <summary>
/// Interface for notifying clients about OPC UA node changes via real-time communication.
/// </summary>
public interface IOpcuaNodeNotificationService
{
    /// <summary>
    /// Notifies clients subscribed to the SimulationFront group that a node was created or updated.
    /// </summary>
    /// <param name="node">The created or updated simulation front node data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task NotifySimulationFrontNodeAsync(NodeResponse node, CancellationToken cancellationToken = default);
}
