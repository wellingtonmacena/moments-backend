using Moments_Backend.Models.DTOs;
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
        [JsonProperty("id")]
        public int Id { get; set; }

        [DefaultValue("Title")]
        [Column("title")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [DefaultValue("Teste")]
        [Column("description")]
        [JsonProperty("description")]

        public string Description { get; set; }
        [DefaultValue("Teste.com.br")]
        [Column("image_url")]
        [JsonProperty("image_url")]
        public string? ImageURL { get; set; }

        [DefaultValue("teste")]
        [Column("image_path")]
        [JsonProperty("image_path", NullValueHandling = NullValueHandling.Ignore)]
        public string? ImagePath { get; set; }

        [Column("created_at")]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        [Column("updated_at")]
        [DefaultValue("2023-08-27 16:54:20.644 -0300")]
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("comments")]
        public List<Comment>? Comments { get; set; }


        public Moment()
        {
            Comments??= new List<Comment>();
        }

        public Moment(int id, string title, string description, string? imageURL, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            ImageURL = imageURL;
            CreatedAt = createdAt;
        }

        public Moment(int id, string title, string description, string? imageURL, DateTime createdAt, List<Comment>? comments) : this(id, title, description, imageURL, createdAt)
        {
            Comments = comments;
        }

        public override string? ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{{Id: {Id},  ");
            stringBuilder.AppendLine($"Title: {Title},  ");
            stringBuilder.AppendLine($"Description: {Description},  ");
            stringBuilder.AppendLine($"ImageURL: {ImageURL},  ");
            stringBuilder.AppendLine($"ImagePath: {ImagePath},  ");
            stringBuilder.AppendLine($"CreatedAt: {CreatedAt},  ");
            stringBuilder.AppendLine($"UpdatedAt: {UpdatedAt}}}");

            return stringBuilder.ToString();
        }

        public void SetCreationInfo(HandleFileDTO handleFileDTO)
        {
            ImageURL = handleFileDTO.ImageURL;
            ImagePath = handleFileDTO.ImagePath;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = CreatedAt;
        }
    }
}
