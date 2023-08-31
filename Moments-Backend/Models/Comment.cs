using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Moments_Backend.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DefaultValue("Username")]
        [Column("username")]
        public string Username { get; set; }
        [DefaultValue("Text")]
        [Column("text")]
        public string Text { get; set; }
        [DefaultValue("MomentId")]
        [Column("moment_id")]
        public int MomentId { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonConstructor]
        public Comment(int id, string username, string text, int momentId, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Username = username;
            Text = text;
            MomentId = momentId;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
