namespace Moments_Backend.Utils
{
    public static class ImageUtils
    {
        public static string GenerateNewFilename(string filename)
        {
            if(filename.Length>=10)
                return $"{Guid.NewGuid().ToString().Substring(0, 10)}-{filename.Substring(filename.Length - 10, 10)}";
            else
                return $"{Guid.NewGuid().ToString().Substring(0, 10)}-{filename}";
        }
    }
}
