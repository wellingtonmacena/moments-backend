using Moments_Backend.Interfaces;
using Moments_Backend.Utils;

namespace Moments_Backend.Services
{
    public class LocalSaveFileService : ISaveFile
    {
        public LocalSaveFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public async Task<string> Execute(IFormFile imageFile)
        {
            var url = _configuration.GetValue<string>("ASPNETCORE_URLS");
            Directory.CreateDirectory("Uploads");
            string filename = ImageUtils.GenerateNewFilename(imageFile.FileName);
            string filePath = Path.Combine("Uploads", filename);
            using (var memoryStream = new FileStream(filePath, FileMode.Create, System.IO.FileAccess.Write))
            {
                imageFile.CopyTo(memoryStream);
  
                return $"{url}/Uploads/{filename}";
            }
        }

    }
}

