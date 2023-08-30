using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Moments_Backend.Models
{
    [Table("moments")]
    public class Moment
    {
        [Key]
        [Column("id")]
        [DefaultValue(0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DefaultValue("Title")]
        [Column("title")]
        public string Title { get; set; }
        [DefaultValue("Teste")]
        [Column("description")]
        public string Description { get; set; }
        [DefaultValue("Teste.com.br")]
        [Column("image_url")]
        public string ImageURL { get; set; }

        [Column("created_at")]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        [Column("updated_at")]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        public DateTime UpdatedAt { get; set; }
        public Moment(int id, string title, string description, string imageURL, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageURL = imageURL;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public Moment()
        {
        }

        public override string? ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{{Id: {Id},  ");
            stringBuilder.AppendLine($"Title: {Title},  ");
            stringBuilder.AppendLine($"Description: {Description},  ");
            stringBuilder.AppendLine($"ImageURL: {ImageURL},  ");
            stringBuilder.AppendLine($"CreatedAt: {CreatedAt},  ");
            stringBuilder.AppendLine($"UpdatedAt: {UpdatedAt}}}");

            return stringBuilder.ToString();
        }
    }
}
