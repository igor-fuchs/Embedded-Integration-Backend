namespace Domain.DTOs;

using System.Text.Json;

/// <summary>
/// DTO for an OPC UA node value.
/// </summary>
public record NodeValueOpcuaDto(
    JsonElement Value
);
