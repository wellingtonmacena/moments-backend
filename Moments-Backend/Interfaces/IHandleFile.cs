using Moments_Backend.Models.DTOs;

namespace Moments_Backend.Interfaces
{
    public interface IHandleFile
    {
        Task<HandleFileDTO> Save(IFormFile imageFile);
        Task<bool> Delete(string filepath);
    }
}