namespace Infrastructure.Repositories;

using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

/// <summary>
/// In-memory cache implementation of the OPC UA node repository.
/// </summary>
public sealed class OpcuaNodeCacheRepository : IOpcuaNodeRepository
{
    private readonly IMemoryCache _cache;
    private const string CacheKeyPrefix = "opcua_node_";
    private const string AllNodesKey = "opcua_all_nodes";
    private static readonly TimeSpan NodeExpiration = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan IndexExpiration = TimeSpan.FromHours(1);

    public OpcuaNodeCacheRepository(IMemoryCache cache)
    {
        _cache = cache;
    }

    public Task<IEnumerable<OpcuaNode>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var nodeNames = GetNodeNamesIndex();
        var nodes = new List<OpcuaNode>();

        foreach (var name in nodeNames)
        {
            var node = _cache.Get<OpcuaNode>(GetCacheKey(name));
            if (node is not null)
            {
                nodes.Add(node);
            }
        }

        return Task.FromResult<IEnumerable<OpcuaNode>>(nodes);
    }

    public Task<OpcuaNode?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var node = _cache.Get<OpcuaNode>(GetCacheKey(name));
        return Task.FromResult(node);
    }

    public Task<bool> ExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        var exists = _cache.TryGetValue(GetCacheKey(name), out _);
        return Task.FromResult(exists);
    }

    public Task<int> GetCountAsync(CancellationToken cancellationToken = default)
    {
        var nodeNames = GetNodeNamesIndex();
        return Task.FromResult(nodeNames.Count);
    }

    public async Task AddAsync(OpcuaNode node, CancellationToken cancellationToken = default)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(NodeExpiration);

        _cache.Set(GetCacheKey(node.Name), node, cacheOptions);

        // Update index
        var nodeNames = GetNodeNamesIndex();
        nodeNames.Add(node.Name);
        SaveNodeNamesIndex(nodeNames);

    }

    public async Task UpdateAsync(OpcuaNode node, CancellationToken cancellationToken = default)
    {

        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(NodeExpiration);

        _cache.Set(GetCacheKey(node.Name), node, cacheOptions);
    }

    public async Task<bool> RemoveAsync(string name, CancellationToken cancellationToken = default)
    {
        var cacheKey = GetCacheKey(name);

        if (!_cache.TryGetValue(cacheKey, out _))
        {
            return false;
        }

        _cache.Remove(cacheKey);

        // Update index
        var nodeNames = GetNodeNamesIndex();
        nodeNames.Remove(name);
        SaveNodeNamesIndex(nodeNames);

        return true;
    }

    private HashSet<string> GetNodeNamesIndex()
    {
        return _cache.GetOrCreate(AllNodesKey, entry =>
        {
            entry.SlidingExpiration = IndexExpiration;
            return new HashSet<string>();
        }) ?? new HashSet<string>();
    }

    private void SaveNodeNamesIndex(HashSet<string> nodeNames)
    {
        var options = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(IndexExpiration);

        _cache.Set(AllNodesKey, nodeNames, options);
    }

    private static string GetCacheKey(string name) => $"{CacheKeyPrefix}{name}";
}
