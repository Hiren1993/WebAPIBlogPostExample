namespace WebAPIBlogPostExample.Model
{
    public class BlogPost
    {
        public int id { get; set; }
        public string? author { get; set; }
        public int authorId { get; set; }
        public int likes { get; set; }
        public double popularity { get; set; }
        public int reads { get; set; }
        public string[]? tags { get; set; }
    }
}
