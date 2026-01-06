namespace Domain.DTOs;

using System.Text.Json;

/// <summary>
/// DTO for OPC UA Node.
/// Value is stored as JsonElement to prevent unsafe deserialization.
/// </summary>
public record NodeOpcuaDto
(
    string Name,
    JsonElement Value  // Tipo seguro - não permite deserialização de tipos arbitrários
);