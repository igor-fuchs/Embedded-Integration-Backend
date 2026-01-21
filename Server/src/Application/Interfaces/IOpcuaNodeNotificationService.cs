namespace Application.Interfaces;

using Domain.DTOs.Responses;

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

    /// <summary>
    /// Gets the initial state of all SimulationFront nodes.
    /// Returns all aliases with their current values (or null if not yet registered).
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of all SimulationFront nodes with their current values.</returns>
    Task<IReadOnlyList<NodeResponse>> GetSimulationFrontInitialStateAsync(CancellationToken cancellationToken = default);
}
