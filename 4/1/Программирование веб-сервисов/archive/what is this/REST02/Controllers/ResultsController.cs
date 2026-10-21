using Microsoft.AspNetCore.Mvc;
using ResultsCollection;

namespace REST01.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultsController : ControllerBase
{
    private readonly ResultsCollection.Results _results;

    public ResultsController(ResultsCollection.Results results)
    {
        _results = results;
    }

    // GET: api/Results
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