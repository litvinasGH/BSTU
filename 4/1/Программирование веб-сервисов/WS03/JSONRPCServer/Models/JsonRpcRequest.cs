
using System.Text.Json;

namespace JSONRPCServer.Models;

public class JsonRpcRequest
{
    public string? Jsonrpc { get; set; }

    public string? Method { get; set; }

    public JsonElement Params { get; set; }

    public JsonElement Id { get; set; }
}
