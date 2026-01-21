namespace Presentation.Hubs;

/// <summary>
/// Extension methods for OPC UA node hubs.
/// </summary>
public static class OpcuaNodeHubExtensions
{
    public static WebApplication MapHubs(this WebApplication app)
    {
        app.MapHub<OpcuaNodeHub>("/hub/opcua-nodes");
        return app;
    }
}