namespace Domain.Constants;

/// <summary>
/// Contains OPC UA Node IDs for the Simulation Front subscription group.
/// </summary>
public static class SimulationFrontNodeIds
{
    /// <summary>
    /// SignalR group name for simulation front subscriptions.
    /// </summary>
    public const string GroupName = "SimulationFront";

    #region Actuators

    public const string ActuatorAinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_A_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorAinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_A_SHM"".""STATUS"".""RETRACT_POSITION""";
    public const string ActuatorBinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_B_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorBinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_B_SHM"".""STATUS"".""RETRACT_POSITION""";
    public const string ActuatorCinAdvance = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_C_SHM"".""STATUS"".""ADVANCE_POSITION""";
    public const string ActuatorCinRetract = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ACTUATOR_C_SHM"".""STATUS"".""RETRACT_POSITION""";

    #endregion

    #region Conveyors

    public const string ConveyorLeftRunning = @"ns=3;s=""ST005_BUFFER_SHM"".""CONVEYOR_SHM"".""STATUS"".""RUNNING""";
    public const string ConveyorRightRunning = @"ns=3;s=""ST007_BUFFER_SHM"".""CONVEYOR_SHM"".""STATUS"".""RUNNING""";
    public const string BigConveyorRunning = @"ns=3;s=""ST010_SEPARATOR_SHM"".""ENTRY_CONVEYOR_SHM"".""STATUS"".""RUNNING""";

    #endregion

    #region Robot Left

    public const string RobotLeftToHome = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""HOME_POSITION""";
    public const string RobotLeftMovingToDrop = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""DROP_POSITION""";
    public const string RobotLeftToPick = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PICK_POSITION""";
    public const string RobotLeftToAntecipation = @"ns=3;s=""ST005_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PART_DEPOSITED""";

    #endregion

    #region Robot Right

    public const string RobotRightToHome = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""HOME_POSITION""";
    public const string RobotRightMovingToDrop = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""DROP_POSITION""";
    public const string RobotRightToPick = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PICK_POSITION""";
    public const string RobotRightToAntecipation = @"ns=3;s=""ST007_BUFFER_SHM"".""ROBOT_SHM"".""STATUS"".""PART_DEPOSITED""";

    #endregion

    #region Parts

    public const string CreateParts = @"ns=3;s=""ST005_BUFFER_FB_IDB"".""EMITTER_FB"".""R_TRIG_PartCreated"".Q";

    #endregion

    /// <summary>
    /// Gets all node IDs for the simulation front subscription.
    /// </summary>
    public static IReadOnlyList<string> AllNodeIds =>
    [
        // Actuators
        ActuatorAinAdvance,
        ActuatorAinRetract,
        ActuatorBinAdvance,
        ActuatorBinRetract,
        ActuatorCinAdvance,
        ActuatorCinRetract,
        // Conveyors
        ConveyorLeftRunning,
        ConveyorRightRunning,
        BigConveyorRunning,
        // Robot Left
        RobotLeftToHome,
        RobotLeftMovingToDrop,
        RobotLeftToPick,
        RobotLeftToAntecipation,
        // Robot Right
        RobotRightToHome,
        RobotRightMovingToDrop,
        RobotRightToPick,
        RobotRightToAntecipation,
        // Parts
        CreateParts
    ];

    /// <summary>
    /// Checks if a given node ID is part of the separator station subscription group.
    /// </summary>
    /// <param name="nodeId">The OPC UA node ID to check.</param>
    /// <returns>True if the node ID is part of the group; otherwise, false.</returns>
    public static bool IsSimulationFrontNode(string nodeId) =>
        AllNodeIds.Contains(nodeId);
}
