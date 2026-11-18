
namespace JSONRPCServer.Models;

public class JsonRpcResponse
{
    public string Jsonrpc { get; set; } = "2.0";

    public object? Result { get; set; }

    public JsonRpcError? Error { get; set; }

    public object? Id { get; set; }
}

public class JsonRpcError
{
    public int Code { get; set; }

    public string Message { get; set; } = "";
}
