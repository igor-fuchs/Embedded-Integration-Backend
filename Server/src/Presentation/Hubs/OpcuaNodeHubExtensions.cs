namespace Presentation.Hubs;

/// <summary>
/// Extension methods for OPC UA node hubs.
/// </summary>
public static class OpcuaNodeHubExtensions
{
    public static IApplicationBuilder MapHubs(this IApplicationBuilder app)
    {
        return app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<OpcuaNodeHub>("/hubs/opcua-nodes");
        });
    }
}