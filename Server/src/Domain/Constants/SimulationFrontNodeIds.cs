namespace Domain.Constants;

/// <summary>
/// Contains OPC UA Node IDs for the Simulation Front subscription group.
/// </summary>
public static class SimulationFrontNodeIds
{
    public const string GroupName = "SimulationFront";

    #region Node Definitions

    // Actuators
    public const string ActuatorAinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_A_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorAinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_A_SHM"".""STATUS"".""RETRACT_POSITION""";
    public const string ActuatorBinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_B_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorBinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_B_SHM"".""STATUS"".""RETRACT_POSITION""";
    public const string ActuatorCinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_C_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorCinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_C_SHM"".""STATUS"".""RETRACT_POSITION""";

    // Conveyors
    public const string ConveyorLeftRunning = @"ns=3;s=""ST005_BUFFER_SHM"".""CONVEYOR_SHM"".""STATUS"".""RUNNING""";
    public const string ConveyorRightRunning = @"ns=3;s=""ST007_BUFFER_SHM"".""CONVEYOR_SHM"".""STATUS"".""RUNNING""";
    public const string BigConveyorRunning = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ENTRY_CONVEYOR_SHM"".""STATUS"".""RUNNING""";

    // Robot Left
    public const string RobotLeftToHome = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""HOME_POSITION""";
    public const string RobotLeftMovingToDrop = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""DROP_POSITION""";
    public const string RobotLeftToPick = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PICK_POSITION""";
    public const string RobotLeftToAntecipation = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PART_DEPOSITED""";
    public const string RobotLeftIsGrabbed = @"ns=3;s=""ST005_BUFFER_FB_IDB"".""ROBOT_FB"".""TurnOnGrab""";

    // Robot Right
    public const string RobotRightToHome = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""HOME_POSITION""";
    public const string RobotRightMovingToDrop = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""DROP_POSITION""";
    public const string RobotRightToPick = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PICK_POSITION""";
    public const string RobotRightToAntecipation = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PART_DEPOSITED""";
    public const string RobotRightIsGrabbed = @"ns=3;s=""ST007_BUFFER_FB_IDB"".""ROBOT_FB"".""TurnOnGrab""";

    // Parts
    public const string CreateParts = @"ns=3;s=""ST005_BUFFER_SHM"".""EMITTER_SHM"".""SENSOR_STATUS";

    #endregion

    #region Computed Collections

    private static IReadOnlyList<string>? _allNodeIds;
    private static IReadOnlyDictionary<string, string>? _aliasToNodeId;
    private static IReadOnlyDictionary<string, string>? _nodeIdToAlias;
    private static HashSet<string>? _nodeIdSet;

    /// <summary>
    /// Gets all node IDs for the simulation front subscription.
    /// </summary>
    public static IReadOnlyList<string> AllNodeIds => _allNodeIds ??= BuildAllNodeIds();

    /// <summary>
    /// Maps friendly alias names to their OPC UA node IDs.
    /// </summary>
    public static IReadOnlyDictionary<string, string> AliasToNodeId => _aliasToNodeId ??= BuildAliasToNodeId();

    /// <summary>
    /// Maps OPC UA node IDs to their friendly alias names.
    /// </summary>
    public static IReadOnlyDictionary<string, string> NodeIdToAlias => _nodeIdToAlias ??= BuildNodeIdToAlias();

    #endregion

    #region Helper Methods

    /// <summary>
    /// Checks if a given node ID is part of the separator station subscription group.
    /// O(1) lookup using HashSet.
    /// </summary>
    public static bool IsSimulationFrontNode(string nodeId)
    {
        _nodeIdSet ??= new HashSet<string>(AllNodeIds, StringComparer.Ordinal);
        return _nodeIdSet.Contains(nodeId);
    }

    /// <summary>
    /// Gets the alias for a given node ID.
    /// </summary>
    public static string? GetAlias(string nodeId) =>
        NodeIdToAlias.TryGetValue(nodeId, out var alias) ? alias : null;

    /// <summary>
    /// Gets the node ID for a given alias.
    /// </summary>
    public static string? GetNodeId(string alias) =>
        AliasToNodeId.TryGetValue(alias, out var nodeId) ? nodeId : null;

    #endregion

    #region Builders

    private static IReadOnlyList<string> BuildAllNodeIds()
    {
        return typeof(SimulationFrontNodeIds)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.IsLiteral && f.FieldType == typeof(string) && f.Name != nameof(GroupName))
            .Select(f => (string)f.GetValue(null)!)
            .ToList();
    }

    private static IReadOnlyDictionary<string, string> BuildAliasToNodeId()
    {
        return typeof(SimulationFrontNodeIds)
            .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
            .Where(f => f.IsLiteral && f.FieldType == typeof(string) && f.Name != nameof(GroupName))
            .ToDictionary(
                f => f.Name,
                f => (string)f.GetValue(null)!,
                StringComparer.Ordinal
            );
    }

    private static IReadOnlyDictionary<string, string> BuildNodeIdToAlias()
    {
        return AliasToNodeId.ToDictionary(
            kvp => kvp.Value,
            kvp => kvp.Key,
            StringComparer.Ordinal
        );
    }

    #endregion
}