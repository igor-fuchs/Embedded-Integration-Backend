namespace Application.DTOs.Requests;

using System.Text.Json;

/// <summary>
/// Request DTO for creating a new OPC UA node.
/// </summary>
public sealed record CreateNodeRequest(
    string Name,
    JsonElement Value
);

/// <summary>
/// Request DTO for updating an OPC UA node value.
/// </summary>
public sealed record UpdateNodeRequest(
    JsonElement Value
);
