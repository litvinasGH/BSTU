using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WS01_01.Controllers;

[ApiController]
[Route("stack")]
public class StackController : ControllerBase
{
    private const string StackKey = "Lab01.Stack";
    private static int ResultGlobal = 0;

    private string GetInstanceName() => Environment.GetEnvironmentVariable("INSTANCE_NAME") ?? "unknown";

    // GET /stack
    [HttpGet]
    public IActionResult Get()
    {
        var stack = LoadStack();

        return Ok(new ResultResponse
        {
            Result = CalculateResult(stack),
            Instance = GetInstanceName()
        });
    }

    // POST /stack
    // Body: { "RESULT": 10 }
    [HttpPost]
    public IActionResult Post([FromBody] ResultRequest request)
    {
        ResultGlobal = request.Result;

        var stack = LoadStack();

        return Ok(new ResultResponse
        {
            Result = CalculateResult(stack),
            Instance = GetInstanceName()
        });
    }

    // PUT /stack?ADD=5
    [HttpPut]
    public IActionResult Put([FromQuery(Name = "ADD")] int add)
    {
        var stack = LoadStack();

        stack.Push(add);
        SaveStack(stack);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(stack),
            Instance = GetInstanceName()
        });
    }

    // DELETE /stack
    [HttpDelete]
    public IActionResult Delete()
    {
        var stack = LoadStack();

        if (stack.Count == 0)
        {
            return Conflict(new ErrorResponse
            {
                Error = "Стек пуст"
            });
        }

        stack.Pop();
        SaveStack(stack);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(stack),
            Instance = GetInstanceName()
        });
    }

    private Stack<int> LoadStack()
    {
        var json = HttpContext.Session.GetString(StackKey);

        if (string.IsNullOrEmpty(json))
        {
            return new Stack<int>();
        }

        var values = JsonSerializer.Deserialize<int[]>(json)
                     ?? Array.Empty<int>();

        return new Stack<int>(values.Reverse());
    }

    private void SaveStack(Stack<int> stack)
    {
        var json = JsonSerializer.Serialize(stack.ToArray());

        HttpContext.Session.SetString(StackKey, json);
    }

    private static long CalculateResult(Stack<int> stack)
    {
        var baseResult = ResultGlobal;

        var top = stack.TryPeek(out var value) ? value : 0;

        return (long)baseResult + top;
    }
}

public sealed class ResultRequest
{
    [JsonPropertyName("RESULT")]
    public int Result { get; set; }
}

public sealed class ResultResponse
{
    [JsonPropertyName("RESULT")]
    public long Result { get; set; }

    [JsonPropertyName("INSTANCE")]
    public string Instance { get; set; } = string.Empty;
}

public sealed class ErrorResponse
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = string.Empty;
}