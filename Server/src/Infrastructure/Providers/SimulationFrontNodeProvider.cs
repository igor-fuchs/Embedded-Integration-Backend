namespace Infrastructure.Providers;

using Application.Interfaces;
using Application.Options;
using Microsoft.Extensions.Options;

/// <summary>
/// Provides access to Simulation Front OPC UA node IDs from configuration.
/// </summary>
public class SimulationFrontNodeProvider : ISimulationFrontNodeProvider
{
    private readonly SimulationFrontOptions _options;
    private readonly Lazy<IReadOnlyList<string>> _allNodeIds;
    private readonly Lazy<IReadOnlyDictionary<string, string>> _nodeIdToAlias;
    private readonly Lazy<HashSet<string>> _nodeIdSet;

    public SimulationFrontNodeProvider(IOptions<SimulationFrontOptions> options)
    {
        _options = options.Value;
        _allNodeIds = new Lazy<IReadOnlyList<string>>(() => _options.Nodes.Values.ToList());
        _nodeIdToAlias = new Lazy<IReadOnlyDictionary<string, string>>(() =>
            _options.Nodes.ToDictionary(
                kvp => kvp.Value,
                kvp => kvp.Key,
                StringComparer.Ordinal));
        _nodeIdSet = new Lazy<HashSet<string>>(() =>
            new HashSet<string>(_options.Nodes.Values, StringComparer.Ordinal));
    }

    /// <inheritdoc />
    public string GroupName => _options.GroupName;

    /// <inheritdoc />
    public IReadOnlyList<string> AllNodeIds => _allNodeIds.Value;

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> AliasToNodeId => _options.Nodes;

    /// <inheritdoc />
    public IReadOnlyDictionary<string, string> NodeIdToAlias => _nodeIdToAlias.Value;

    /// <inheritdoc />
    public bool IsSimulationFrontNode(string nodeId) => _nodeIdSet.Value.Contains(nodeId);

    /// <inheritdoc />
    public string? GetAlias(string nodeId) =>
        NodeIdToAlias.TryGetValue(nodeId, out var alias) ? alias : null;

    /// <inheritdoc />
    public string? GetNodeId(string alias) =>
        AliasToNodeId.TryGetValue(alias, out var nodeId) ? nodeId : null;
}
