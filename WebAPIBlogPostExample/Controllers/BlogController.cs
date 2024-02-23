using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebAPIBlogPostExample.Model;

namespace WebAPIBlogPostExample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly BlogPostService _blogPostService;

        public BlogController(BlogPostService blogPostService)
        {
            _blogPostService = blogPostService ?? throw new ArgumentNullException(nameof(blogPostService));
        }
        
        [HttpGet(Name = "GetBlogPosts")]
        public async Task<ActionResult<List<BlogPost>>> GetBlogPosts(string tag,string? sortBy="id",string? direction="asc")
        {
            // Validate 'tag' parameter
            if (string.IsNullOrWhiteSpace(tag))
            {
                return BadRequest("The 'tag' parameter is required and cannot be empty or contain only whitespaces.");
            }

            // Validate 'sortBy' and 'direction' parameters
            if (!IsValidSortBy(sortBy) || !IsValidDirection(direction))
            {
                return BadRequest("Invalid values for 'sortBy' or 'direction' parameters.");
            }

            var blogPosts = await _blogPostService.GetBlogPostsAsync(tag,sortBy,direction);
            return Ok(blogPosts);
        }
        //Validate SortBy Value

        private bool IsValidSortBy(string? sortBy)
        {
            return sortBy == "id" || sortBy == "popularity" || sortBy == "likes" || sortBy == "reads";
        }
        //Validate Direction Value
        private bool IsValidDirection(string? direction)
        {
            return direction == "asc" || direction == "desc";
        }
    }
}
