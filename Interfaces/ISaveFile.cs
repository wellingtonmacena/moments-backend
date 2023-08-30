namespace Moments_Backend.Interfaces
{
    public interface ISaveFile
    {
        Task<string> Execute(IFormFile imageFile);
    }
}