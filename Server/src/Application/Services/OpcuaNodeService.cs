namespace Application.Services;

using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Interfaces;
using Application.Options;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.Extensions.Options;

/// <summary>
/// Application service for OPC UA node operations.
/// </summary>
public sealed class OpcuaNodeService : IOpcuaNodeService
{
    private readonly IOpcuaNodeRepository _repository;
    private readonly IValidationService _validator;
    private readonly IOpcuaNodeNotificationService _notificationService;
    private readonly SimulationFrontOptions _simulationFrontOptions;
    private const int MaxNodes = 100;

    public OpcuaNodeService(
        IOpcuaNodeRepository repository,
        IValidationService validator,
        IOpcuaNodeNotificationService notificationService,
        IOptions<SimulationFrontOptions> simulationFrontOptions)
    {
        _repository = repository;
        _validator = validator;
        _notificationService = notificationService;
        _simulationFrontOptions = simulationFrontOptions.Value;
    }

    public async Task<NodeListResponse> GetRegisteredNodesAsync(CancellationToken cancellationToken = default)
    {
        var nodes = await _repository.GetAllAsync(cancellationToken);
        return NodeListResponse.FromEntities(nodes);
    }

    public Task<NodeNamesListResponse> GetNodeNamesAsync(CancellationToken cancellationToken = default)
    {
        var nodeNames = _simulationFrontOptions.Nodes.Values.ToList();
        var response = new NodeNamesListResponse(nodeNames, nodeNames.Count);
        return Task.FromResult(response);
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
        // Validate request
        await _validator.ValidateAsync(request, cancellationToken);

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

        var response = NodeResponse.FromEntity(node);

        // Notify connected group clients
        await _notificationService.NotifySimulationFrontNodeAsync(response, cancellationToken);

        return response;
    }

    public async Task<NodeResponse> UpdateNodeAsync(string name, UpdateNodeRequest request, CancellationToken cancellationToken = default)
    {
        // Validate request
        await _validator.ValidateAsync(request, cancellationToken);

        var node = await _repository.GetByNameAsync(name, cancellationToken);

        if (node is null)
        {
            throw new NotFoundException("Node", name);
        }

        node.UpdateValue(request.Value);

        await _repository.UpdateAsync(node, cancellationToken);

        var response = NodeResponse.FromEntity(node);

        // Notify connected group clients
        await _notificationService.NotifySimulationFrontNodeAsync(response, cancellationToken);

        return response;
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
