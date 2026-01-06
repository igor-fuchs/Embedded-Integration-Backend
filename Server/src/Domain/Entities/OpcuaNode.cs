namespace Domain.Entities;

using System.Text.Json;

/// <summary>
/// Represents an OPC UA Node entity.
/// </summary>
public sealed class OpcuaNode
{
    public string Name { get; private set; }
    public JsonElement Value { get; private set; }

    private OpcuaNode(string name, JsonElement value)
    {
        Name = name;
        Value = value;
    }

    /// <summary>
    /// Creates a new OPC UA node.
    /// </summary>
    public static OpcuaNode Create(string name, JsonElement value)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Node name cannot be empty.", nameof(name));

        return new OpcuaNode(name, value);
    }

    /// <summary>
    /// Updates the node value.
    /// </summary>
    public void UpdateValue(JsonElement newValue)
    {
        Value = newValue;
    }
}
