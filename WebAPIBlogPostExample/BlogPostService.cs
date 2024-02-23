using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using WebAPIBlogPostExample.Model;

namespace WebAPIBlogPostExample
{
    public class BlogPostService
    {
        private readonly HttpClient _httpClient;
        private readonly string baseUrl = "https://api.hatchways.io/assessment/blog";

        public BlogPostService(HttpClient httpClient)
        {
            //Created httpClient Object using dependency injection
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        /// <summary>
        /// GetBlogPostsAsync Method to get data from api
        /// </summary>
        /// <param name="tag">Required Parameter</param>
        /// <param name="sortBy">Optional Parameter with default value</param>
        /// <param name="direction">Optional Parameter with default value</param>
        /// <returns>List of BlogPost</returns>
        public async Task<List<BlogPost>> GetBlogPostsAsync(string tag,string? sortBy,string? direction)
        {
            string url = baseUrl + "/posts?tag="+tag+"&sortBy="+sortBy+"&direction="+direction;
            var response = await _httpClient.GetFromJsonAsync<Dictionary<string, List<BlogPost>>>(url);
            
            var blogPosts = response?["posts"] ?? new List<BlogPost>();

            if (blogPosts == null)
            {
                // Handle the case where the API response is empty or not in the expected format
                return new List<BlogPost>();
            }

            return blogPosts;
        }
    }
}
