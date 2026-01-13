namespace Application.Options;

/// <summary>
/// Configuration options for the Simulation Front subscription group.
/// Maps to the "SimulationFront" section in appsettings.json.
/// </summary>
public class SimulationFrontOptions
{
    /// <summary>
    /// The configuration section name in appsettings.json.
    /// </summary>
    public const string SectionName = "SimulationFront";

    /// <summary>
    /// The name of the subscription group.
    /// </summary>
    public string GroupName { get; set; } = string.Empty;

    /// <summary>
    /// Dictionary mapping alias names to their OPC UA node IDs.
    /// </summary>
    public Dictionary<string, string> Nodes { get; set; } = new();
}
