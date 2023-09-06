using Moments_Backend.Interfaces;
using Moments_Backend.Models.DTOs;
using Moments_Backend.Utils;

namespace Moments_Backend.Services
{
    public class LocalHandleFileService : IHandleFile
    {
        private IConfiguration _configuration { get; }
        public LocalHandleFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }  

        public async Task<HandleFileDTO> Save(IFormFile imageFile)
        {
            var url = _configuration.GetValue<string>("ASPNETCORE_URLS");
            Directory.CreateDirectory("Uploads");
            string filename = ImageUtils.GenerateNewFilename(imageFile.FileName);
            string filePath = Path.Combine("Uploads", filename);
            using (var memoryStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                imageFile.CopyTo(memoryStream);

                return new HandleFileDTO($"{url}/Uploads/{filename}", filePath);
            }
        }

        public Task<bool> Delete(string filepath)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, filepath);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }    
    }
}

