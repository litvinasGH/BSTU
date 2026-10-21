using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace WS01_02.Controllers;

[ApiController]
[Route("stack")]
public class StackController : ControllerBase
{
    private const string CookieName = "WS01_02.State";

    // GET /stack
    [HttpGet]
    public IActionResult Get()
    {
        var state = ReadState();

        if (!Request.Cookies.ContainsKey(CookieName))
            WriteState(state);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(state)
        });
    }

    // POST /stack
    // Body: {"RESULT": 10}
    [HttpPost]
    public IActionResult Post([FromBody] ResultRequest request)
    {
        var state = ReadState();
        state.BaseResult = request.Result;

        WriteState(state);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(state)
        });
    }

    // PUT /stack?ADD=5
    [HttpPut]
    public IActionResult Put([FromQuery(Name = "ADD")] int add)
    {
        var state = ReadState();

        state.StackTopFirst.Insert(0, add);

        WriteState(state);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(state)
        });
    }

    // DELETE /stack
    [HttpDelete]
    public IActionResult Delete()
    {
        var state = ReadState();

        if (state.StackTopFirst.Count == 0)
        {
            return Conflict(new
            {
                error = "Стек пуст"
            });
        }

        state.StackTopFirst.RemoveAt(0);

        WriteState(state);

        return Ok(new ResultResponse
        {
            Result = CalculateResult(state)
        });
    }

    private ClientState ReadState()
    {
        if (!Request.Cookies.TryGetValue(CookieName, out var cookie))
            return new ClientState();

        try
        {
            var json = Uri.UnescapeDataString(cookie);

            return JsonSerializer.Deserialize<ClientState>(json)
                   ?? new ClientState();
        }
        catch (JsonException)
        {
            return new ClientState();
        }
    }

    private void WriteState(ClientState state)
    {
        var json = JsonSerializer.Serialize(state);
        var value = Uri.EscapeDataString(json);

        Response.Cookies.Append(CookieName, value, new CookieOptions
        {
            HttpOnly = true,
            IsEssential = true,
            SameSite = SameSiteMode.Lax,
            Secure = Request.IsHttps,
            Path = "/",
            Expires = DateTimeOffset.UtcNow.AddMinutes(30)
        });
    }

    private static long CalculateResult(ClientState state)
    {
        int top = state.StackTopFirst.Count > 0
            ? state.StackTopFirst[0]
            : 0;

        return (long)state.BaseResult + top;
    }
}

public sealed class ClientState
{
    public int BaseResult { get; set; }

    public List<int> StackTopFirst { get; set; } = new();
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
}