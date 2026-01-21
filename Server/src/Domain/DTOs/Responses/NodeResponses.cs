namespace Domain.DTOs.Responses;

using System.Text.Json;
using Domain.Entities;

/// <summary>
/// Response DTO for OPC UA node data.
/// </summary>
public sealed record NodeResponse(
    string Name,
    JsonElement Value
)
{
    /// <summary>
    /// Creates a response from domain entity.
    /// </summary>
    public static NodeResponse FromEntity(OpcuaNode node) => new(
        node.Name,
        node.Value
    );
}

/// <summary>
/// Response DTO for a list of OPC UA nodes.
/// </summary>
public sealed record NodeListResponse(
    IReadOnlyList<NodeResponse> Nodes,
    int TotalCount
)
{
    public static NodeListResponse FromEntities(IEnumerable<OpcuaNode> nodes)
    {
        var nodeList = nodes.Select(NodeResponse.FromEntity).ToList();
        return new NodeListResponse(nodeList, nodeList.Count);
    }
}

/// <summary>
/// Response DTO for a list of OPC UA node names from configuration.
/// </summary>
public sealed record NodeNamesListResponse(
    IReadOnlyList<string> NodeNames,
    int TotalCount
);
