namespace Infrastructure.Services;

using Domain.Interfaces;
using Domain.DTOs;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

public class NodeOpcuaService : INodeOpcuaService
{
    private readonly IMemoryCache _cache;
    private const string CacheKeyPrefix = "opcua_node_";
    private const string AllNodesKey = "opcua_all_nodes";
    private const int MaxNodes = 100;

    public NodeOpcuaService(IMemoryCache cache)
    {
        _cache = cache;
    }

    /// <summary>
    /// Gets all OPC UA nodes stored in the cache.
    /// </summary>
    public IEnumerable<NodeOpcuaDto> GetAllNodes()
    {
        var nodeNames = _cache.GetOrCreate(AllNodesKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(1);
            return new HashSet<string>();
        }) ?? new HashSet<string>();

        var nodes = new List<NodeOpcuaDto>();
        foreach (var name in nodeNames)
        {
            var node = GetNode(name);
            if (node is not null)
            {
                nodes.Add(node);
            }
        }

        return nodes;
    }

    /// <summary>
    /// Gets a specific OPC UA node by name.
    /// </summary>
    /// <param name="name">OPC UA node name</param>
    /// <returns>The OPC UA node DTO or null if not found</returns>
    public NodeOpcuaDto? GetNode(string name)
    {
        var cacheKey = GetCacheKey(name);
        return _cache.Get<NodeOpcuaDto>(cacheKey);
    }

    /// <summary>
    /// Sets or updates an OPC UA node in the cache.
    /// </summary>
    /// <param name="name">OPC UA node name</param>
    /// <param name="value">OPC UA node value</param>
    /// <exception cref="InvalidOperationException">Thrown when max node limit is reached</exception>
    public NodeOpcuaDto SetNode(string name, JsonElement value)
    {
        var nodeNames = _cache.GetOrCreate(AllNodesKey, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(1);
            return new HashSet<string>();
        }) ?? new HashSet<string>();

        // Check max nodes limit
        if (!nodeNames.Contains(name) && nodeNames.Count >= MaxNodes)
        {
            throw new InvalidOperationException($"Maximum number of nodes ({MaxNodes}) reached. Cannot create new nodes.");
        }

        var node = new NodeOpcuaDto(name, value);
        var cacheKey = GetCacheKey(name);

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(30))
            .SetSize(1);  // Define size for cache control

        _cache.Set(cacheKey, node, cacheOptions);

        nodeNames.Add(name);
        _cache.Set(AllNodesKey, nodeNames);

        return node;
    }

    /// <summary>
    /// Removes an OPC UA node from the cache.
    /// </summary>
    /// <param name="name">OPC UA node name</param>
    /// <returns>True if the node was removed, false if not found</returns>
    public bool RemoveNode(string name)
    {
        var cacheKey = GetCacheKey(name);
        var node = GetNode(name);

        if (node is null)
        {
            return false;
        }

        _cache.Remove(cacheKey);

        // Remove from tracked names
        var nodeNames = _cache.Get<HashSet<string>>(AllNodesKey);
        if (nodeNames is not null)
        {
            nodeNames.Remove(name);
            _cache.Set(AllNodesKey, nodeNames);
        }

        return true;
    }

    private static string GetCacheKey(string name) => $"{CacheKeyPrefix}{name}";
}
