namespace Application.Interfaces;

using Application.DTOs.Requests;
using Application.DTOs.Responses;

/// <summary>
/// Interface for OPC UA node service operations.
/// </summary>
public interface IOpcuaNodeService
{
    Task<NodeListResponse> GetRegisteredNodesAsync(CancellationToken cancellationToken = default);
    Task<NodesNameListResponse> GetNodesNameAsync(CancellationToken cancellationToken = default);
    Task<NodeResponse> GetNodeByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<NodeResponse> CreateNodeAsync(CreateNodeRequest request, CancellationToken cancellationToken = default);
    Task<NodeResponse> UpdateNodeAsync(string name, UpdateNodeRequest request, CancellationToken cancellationToken = default);
    Task DeleteNodeAsync(string name, CancellationToken cancellationToken = default);
}