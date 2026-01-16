namespace Application.Interfaces;

using Domain.DTOs.Requests;
using Domain.DTOs.Responses;

/// <summary>
/// Interface for OPC UA node service operations.
/// </summary>
public interface IOpcuaNodeService
{
    Task<NodeListResponse> GetRegisteredNodesAsync(CancellationToken cancellationToken = default);
    Task<NodeNamesListResponse> GetNodeNamesAsync(CancellationToken cancellationToken = default);
    Task<NodeResponse> GetNodeByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<NodeResponse> CreateNodeAsync(CreateNodeRequest request, CancellationToken cancellationToken = default);
    Task<NodeResponse> UpdateNodeAsync(string name, UpdateNodeRequest request, CancellationToken cancellationToken = default);
    Task DeleteNodeAsync(string name, CancellationToken cancellationToken = default);
}