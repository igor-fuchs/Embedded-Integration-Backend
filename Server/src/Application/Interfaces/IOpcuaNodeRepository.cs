namespace Application.Interfaces;

using Domain.Entities;

/// <summary>
/// Repository interface for OPC UA node persistence.
/// Following the Repository pattern for data access abstraction.
/// </summary>
public interface IOpcuaNodeRepository
{
    /// <summary>
    /// Gets all stored OPC UA nodes.
    /// </summary>
    Task<IEnumerable<OpcuaNode>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets a specific OPC UA node by name.
    /// </summary>
    /// <param name="name">The node name</param>
    Task<OpcuaNode?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks if a node with the given name exists.
    /// </summary>
    Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default);

    /// <summary>
    /// Gets the current count of stored nodes.
    /// </summary>
    Task<int> GetCountAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds a new OPC UA node.
    /// </summary>
    Task AddAsync(OpcuaNode node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing OPC UA node.
    /// </summary>
    Task UpdateAsync(OpcuaNode node, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes an OPC UA node by name.
    /// </summary>
    /// <returns>True if removed, false if not found</returns>
    Task<bool> RemoveAsync(string name, CancellationToken cancellationToken = default);
}
