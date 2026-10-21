using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ResultsAuthenticate;
using ResultsCollection;

namespace REST01.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class ResultsController : ControllerBase
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

    public sealed record ValueRequest(string? Value);

    // GET: /api/Results
    [HttpGet]
    [Authorize(Roles = "READER,WRITER")]
    public ActionResult<IReadOnlyList<ResultItem>> GetAll()
    {
        var items = _results.All();

        if (items.Count == 0)
        {
            return NoContent();
        }

        return Ok(items);
    }

    // GET: /api/Results/{k}
    [HttpGet("{k:int}")]
    [Authorize(Roles = "READER,WRITER")]
    public ActionResult<ResultItem> Get(int k)
    {
        var item = _results.Get(k);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // POST: /api/Results
    [HttpPost]
    [Authorize(Roles = "WRITER")]
    public ActionResult<ResultItem> Post([FromBody] ValueRequest? body)
    {
        if (body?.Value is null)
        {
            return BadRequest(new
            {
                error = "value must be a string"
            });
        }

        var item = _results.Add(body.Value);

        return CreatedAtAction(
            nameof(Get),
            new { k = item.Id },
            item);
    }

    // PUT: /api/Results/{k}
    [HttpPut("{k:int}")]
    [Authorize(Roles = "WRITER")]
    public ActionResult<ResultItem> Put(
        int k,
        [FromBody] ValueRequest? body)
    {
        if (body?.Value is null)
        {
            return BadRequest(new
            {
                error = "value must be a string"
            });
        }

        var item = _results.Update(k, body.Value);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // DELETE: /api/Results/{k}
    [HttpDelete("{k:int}")]
    [Authorize(Roles = "WRITER")]
    public ActionResult<ResultItem> Delete(int k)
    {
        var item = _results.Delete(k);

        if (item is null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    // POST: /api/Results/SignIn
    [HttpPost("SignIn")]
    [AllowAnonymous]
    public async Task<IActionResult> SignIn(
        [FromBody] SignInRequest? request)
    {
        if (string.IsNullOrWhiteSpace(request?.Login)
            || string.IsNullOrWhiteSpace(request.Password))
        {
            return BadRequest(new
            {
                error = "login and password are required"
            });
        }

        var token = await _authenticate.SignInAsync(
            request.Login,
            request.Password);

        if (token is null)
        {
            return NotFound(new
            {
                error = "invalid credentials"
            });
        }

        return Ok(new
        {
            token
        });
    }
}
