using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SLP11.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Body { get; set; } = "";
    }
}
