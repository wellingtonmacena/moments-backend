using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moments_Backend.Models
{
    [Table("comments")]
    public class Comment
    {
        

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

        public Comment()
        {
        }

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
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{{Id: {Id},  ");
            stringBuilder.AppendLine($"Username: {Username},  ");
            stringBuilder.AppendLine($"Text: {Text},  ");
            stringBuilder.AppendLine($"MomentId: {MomentId},  ");
            stringBuilder.AppendLine($"CreatedAt: {CreatedAt},  ");
            stringBuilder.AppendLine($"UpdatedAt: {UpdatedAt}}}");

            return stringBuilder.ToString();

        }
    }
}
