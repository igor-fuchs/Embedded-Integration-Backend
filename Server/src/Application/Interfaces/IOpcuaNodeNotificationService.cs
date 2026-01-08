namespace Application.Interfaces;

using Application.DTOs.Responses;

/// <summary>
/// Interface for notifying clients about OPC UA node changes via real-time communication.
/// </summary>
public interface IOpcuaNodeNotificationService
{
    /// <summary>
    /// Notifies all connected clients that a new node was created.
    /// </summary>
    /// <param name="node">The created node data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task NotifyNodeCreatedAsync(NodeResponse node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Notifies all connected clients that a node was updated.
    /// </summary>
    /// <param name="node">The updated node data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task NotifyNodeUpdatedAsync(NodeResponse node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Notifies all connected clients that a node was deleted.
    /// </summary>
    /// <param name="nodeName">The name of the deleted node.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task NotifyNodeDeletedAsync(string nodeName, CancellationToken cancellationToken = default);
}
