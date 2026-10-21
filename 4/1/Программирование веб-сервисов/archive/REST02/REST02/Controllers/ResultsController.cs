using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultsAuthenticate;
using ResultsCollection;

namespace REST01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly ResultsCollection.Results _results;
    private readonly Authenticate _authenticate;

    public ResultsController(
        ResultsCollection.Results results,
        Authenticate authenticate)
    {
        _results = results;
        _authenticate = authenticate;
    }

    // POST: api/Results/SignIn
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public IActionResult SignIn([FromBody] SignInRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Login) ||
            string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest();
        }

        string? token = _authenticate.SignIn(
            request.Login,
            request.Password);

        if (token == null)
        {
            return NotFound();
        }

        return Ok(new { token });
    }

    // GET: api/Results
    [Authorize(Roles = "READER,WRITER")]
    [HttpGet]
    public IActionResult GetAll()
    {
        var results = _results.GetAll();

        if (results.Count == 0)
        {
            return NoContent();
        }

        return Ok(results);
    }

    // GET: api/Results/1
    [Authorize(Roles = "READER,WRITER")]
    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var result = _results.Get(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // POST: api/Results
    [Authorize(Roles = "WRITER")]
    [HttpPost]
    public IActionResult Post([FromBody] ResultRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Value))
        {
            return BadRequest();
        }

        var result = _results.Add(request.Value);

        return CreatedAtAction(
            nameof(Get),
            new { id = result.Id },
            result);
    }

    // PUT: api/Results/1
    [Authorize(Roles = "WRITER")]
    [HttpPut("{id:int}")]
    public IActionResult Put(int id, [FromBody] ResultRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Value))
        {
            return BadRequest();
        }

        var result = _results.Update(id, request.Value);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    // DELETE: api/Results/1
    [Authorize(Roles = "WRITER")]
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var result = _results.Delete(id);

        if (result == null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}

public class ResultRequest
{
    public string Value { get; set; } = string.Empty;
}

public class SignInRequest
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
