namespace Application.Interfaces;

/// <summary>
/// Provides access to Simulation Front OPC UA node IDs from configuration.
/// </summary>
public interface ISimulationFrontNodeProvider
{
    /// <summary>
    /// Gets the name of the subscription group.
    /// </summary>
    string GroupName { get; }

    /// <summary>
    /// Gets all node IDs for the simulation front subscription.
    /// </summary>
    IReadOnlyList<string> AllNodeIds { get; }

    /// <summary>
    /// Gets the dictionary mapping alias names to their OPC UA node IDs.
    /// </summary>
    IReadOnlyDictionary<string, string> AliasToNodeId { get; }

    /// <summary>
    /// Gets the dictionary mapping OPC UA node IDs to their alias names.
    /// </summary>
    IReadOnlyDictionary<string, string> NodeIdToAlias { get; }

    /// <summary>
    /// Checks if a given node ID is part of the simulation front subscription group.
    /// </summary>
    /// <param name="nodeId">The node ID to check.</param>
    /// <returns>True if the node ID belongs to this group; otherwise, false.</returns>
    bool IsSimulationFrontNode(string nodeId);

    /// <summary>
    /// Gets the alias for a given node ID.
    /// </summary>
    /// <param name="nodeId">The OPC UA node ID.</param>
    /// <returns>The alias if found; otherwise, null.</returns>
    string? GetAlias(string nodeId);

    /// <summary>
    /// Gets the node ID for a given alias.
    /// </summary>
    /// <param name="alias">The alias name.</param>
    /// <returns>The node ID if found; otherwise, null.</returns>
    string? GetNodeId(string alias);
}
