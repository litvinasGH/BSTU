using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SLP11.Models
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public PostsController(AppDbContext db) => _db = db;

        // GET /api/posts
        [HttpGet]
        public async Task<IEnumerable<Post>> Get() =>
            await _db.Posts.ToListAsync();

        // GET /api/posts/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            return post is not null ? Ok(post) : NotFound();
        }

        // POST /api/posts
        [HttpPost]
        public async Task<ActionResult<Post>> Post(Post newPost)
        {
            _db.Posts.Add(newPost);
            await _db.SaveChangesAsync();

            return CreatedAtAction(
                nameof(Get),
                new { id = newPost.Id },
                newPost
            );
        }

        // PUT /api/posts/{id}
        [HttpPut("{id:int}")]
        public async Task<ActionResult<Post>> Put(int id, Post updated)
        {
            if (id != updated.Id) return BadRequest();

            var existing = await _db.Posts.FindAsync(id);
            if (existing is null) return NotFound();

            existing.Title = updated.Title;
            existing.Body = updated.Body;
            existing.UserId = updated.UserId;

            await _db.SaveChangesAsync();
            return Ok(existing);
        }

        // DELETE /api/posts/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _db.Posts.FindAsync(id);
            if (post is null) return NotFound();

            _db.Posts.Remove(post);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
