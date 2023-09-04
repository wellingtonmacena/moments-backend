namespace Moments_Backend.Utils
{
    public static class ImageUtils
    {
        public static string GenerateNewFilename(string filename)
        {
            string guid = Guid.NewGuid().ToString().Substring(0, 20).Replace("-", "");
            string filenameExtension = filename.Substring(filename.Length-4, 4);

            return $"{guid}{filenameExtension}";
        
        }
    }
}
