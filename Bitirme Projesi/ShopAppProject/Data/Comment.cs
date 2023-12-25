//Data/Comment.cs

using System.ComponentModel.DataAnnotations;

namespace ShopAppProject.Data
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string? UserId { get; set; }
        public int ProductId { get; set; }
        public string? UserName { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }

    }

}
