using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Moments_Backend.Models
{
    [Table("comments")]
    public class Comment
    {
        public Comment(int id, string text, DateTime createdAt)
        {
            Id = id;
            Text = text;
            CreatedAt = createdAt;
        }

        public Comment()
        {
        }

        [Key]
        [Column("id")]
        [DefaultValue(0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DefaultValue("Username")]
        [Column("username")]
        [JsonProperty("username")]
        public string Username { get; set; }

        [DefaultValue("Text")]
        [Column("text")]
        [JsonProperty("text")]
        public string Text { get; set; }

        [DefaultValue(0)]
        [Column("moment_id")]
        [JsonProperty("moment_id")]
        public int MomentId { get; set; }

        [Column("created_at")]
        [JsonProperty("created_at")]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public override string? ToString()
        {
            return base.ToString();
        }
    }
}
