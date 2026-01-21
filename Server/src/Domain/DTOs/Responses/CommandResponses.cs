namespace Domain.DTOs.Responses;

/// <summary>
/// Response DTO for a single command from the front-end.
/// </summary>
public sealed record CommandResponse(
    string Name,
    string Value
);

/// <summary>
/// Response DTO for a list of commands from the front-end.
/// </summary>
public sealed record CommandListResponse(
    IReadOnlyList<CommandResponse> Commands,
    int TotalCount
)
{
    public static CommandListResponse FromCommands(IEnumerable<CommandResponse> commands)
    {
        var commandList = commands.ToList();
        return new CommandListResponse(commandList, commandList.Count);
    }
}
