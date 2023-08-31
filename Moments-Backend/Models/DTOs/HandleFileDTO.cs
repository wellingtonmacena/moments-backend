namespace Moments_Backend.Models.DTOs
{
    public class HandleFileDTO
    {
        public string ImageURL { get; set; }
        public string ImagePath{ get; set; }

        public HandleFileDTO(string imageURL, string imagePath)
        {
            ImageURL = imageURL;
            ImagePath = imagePath;
        }
    }
}
