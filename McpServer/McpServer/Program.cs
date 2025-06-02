using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Linq;

using System.ComponentModel;


var builder = Host.CreateApplicationBuilder(args);

// 設定 Console Log
builder.Logging.AddConsole(options =>
{
    options.LogToStandardErrorThreshold = LogLevel.Trace;
});

// 註冊 MCP Server，使用 stdio 傳輸方式
builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();

await builder.Build().RunAsync();

// MCP 工具定義
[McpServerToolType]
public static class EchoTool
{
    [McpServerTool, Description("Echoes the message back to the client.")]
    public static string Echo(string message) => $"Hello from C#: {message}";

    [McpServerTool, Description("Reverses the message.")]
    public static string ReverseEcho(string message) => new string(message.Reverse().ToArray());
}



[McpServerToolType]
public static class DateTools
{
    [McpServerTool, Description("取得今天的日期（格式：yyyy-MM-dd）")]
    public static string GetTodayDate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd");
    }
}




