namespace Domain.Interfaces;

using Domain.DTOs;
using System.Text.Json;

public interface INodeOpcuaService
{
    IEnumerable<NodeOpcuaDto> GetAllNodes();
    NodeOpcuaDto? GetNode(string name);
    NodeOpcuaDto SetNode(string name, JsonElement value);
    bool RemoveNode(string name);
}
