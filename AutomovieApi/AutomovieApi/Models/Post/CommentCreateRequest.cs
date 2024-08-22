using System.ComponentModel.DataAnnotations;

namespace AutomovieApi.Models.Post
{
    public class CommentCreateRequest
    {
        public int AnId { get; set; }
        public string CommentText { get; set; }
    }
}
