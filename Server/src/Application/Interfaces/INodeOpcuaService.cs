namespace Application.Interfaces;

using Domain.DTOs;

public interface INodeOpcuaService
{
    IEnumerable<NodeOpcuaDto> GetAllNodes();
    NodeOpcuaDto? GetNode(string name);
    NodeOpcuaDto SetNode(string name, object value);
    bool RemoveNode(string name);
}
