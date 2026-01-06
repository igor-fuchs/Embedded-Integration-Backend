namespace Application.Services;

using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;

/// <summary>
/// Application service for OPC UA node operations.
/// </summary>
public sealed class OpcuaNodeService : IOpcuaNodeService
{
    private readonly IOpcuaNodeRepository _repository;
    private const int MaxNodes = 100;

    public OpcuaNodeService(IOpcuaNodeRepository repository)
    {
        _repository = repository;
    }

    public async Task<NodeListResponse> GetAllNodesAsync(CancellationToken cancellationToken = default)
    {
        var nodes = await _repository.GetAllAsync(cancellationToken);
        return NodeListResponse.FromEntities(nodes);
    }

    public async Task<NodeResponse> GetNodeByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var node = await _repository.GetByNameAsync(name, cancellationToken);
        
        if (node is null)
        {
            throw new NotFoundException("Node", name);
        }

        return NodeResponse.FromEntity(node);
    }

    public async Task<NodeResponse> CreateNodeAsync(CreateNodeRequest request, CancellationToken cancellationToken = default)
    {
        // Check if node already exists
        if (await _repository.ExistsAsync(request.Name, cancellationToken))
        {
            throw new ConflictException("Node", request.Name);
        }

        // Check node limit
        var currentCount = await _repository.GetCountAsync(cancellationToken);
        if (currentCount >= MaxNodes)
        {
            throw new ResourceLimitExceededException("nodes", MaxNodes);
        }

        // Create domain entity
        var node = OpcuaNode.Create(request.Name, request.Value);
        
        await _repository.AddAsync(node, cancellationToken);

        return NodeResponse.FromEntity(node);
    }

    public async Task<NodeResponse> UpdateNodeAsync(string name, UpdateNodeRequest request, CancellationToken cancellationToken = default)
    {
        var node = await _repository.GetByNameAsync(name, cancellationToken);
        
        if (node is null)
        {
            throw new NotFoundException("Node", name);
        }

        node.UpdateValue(request.Value);
        
        await _repository.UpdateAsync(node, cancellationToken);

        return NodeResponse.FromEntity(node);
    }

    public async Task DeleteNodeAsync(string name, CancellationToken cancellationToken = default)
    {
        var removed = await _repository.RemoveAsync(name, cancellationToken);
        
        if (!removed)
        {
            throw new NotFoundException("Node", name);
        }
    }
}
