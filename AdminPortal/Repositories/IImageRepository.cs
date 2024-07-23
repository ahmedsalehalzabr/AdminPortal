using AdminPortal.Models.Domain;

namespace AdminPortal.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
