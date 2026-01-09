namespace Application.DTOs.Responses;

/// <summary>
/// Response DTO for the complete separator station state.
/// </summary>
public sealed record SimulationFrontStateResponse(
    ActuatorsState Actuators,
    ConveyorsState Conveyors,
    RobotLeftState RobotLeft,
    RobotRightState RobotRight,
    PartsState Parts
);

/// <summary>
/// State of all actuators.
/// </summary>
public sealed record ActuatorsState(
    bool ActuatorAinAdvance,
    bool ActuatorAinRetract,
    bool ActuatorBinAdvance,
    bool ActuatorBinRetract,
    bool ActuatorCinAdvance,
    bool ActuatorCinRetract
);

/// <summary>
/// State of all conveyors.
/// </summary>
public sealed record ConveyorsState(
    bool ConveyorLeftRunning,
    bool ConveyorRightRunning,
    bool BigConveyorRunning
);

/// <summary>
/// State of the left robot.
/// </summary>
public sealed record RobotLeftState(
    bool ToHome,
    bool MovingToDrop,
    bool ToPick,
    bool ToAntecipation
);

/// <summary>
/// State of the right robot.
/// </summary>
public sealed record RobotRightState(
    bool ToHome,
    bool MovingToDrop,
    bool ToPick,
    bool ToAntecipation
);

/// <summary>
/// State of parts creation.
/// </summary>
public sealed record PartsState(
    bool CreateParts
);