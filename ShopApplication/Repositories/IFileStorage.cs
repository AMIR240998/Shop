namespace ShopApplication.Repositories;

public interface IFileStorage
{
    Task<string> UploadAsync(
        Stream file,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default);

    Task DeleteAsync(
        string fileName,
        CancellationToken cancellationToken = default);

    Task<Stream?> GetAsync(
        string fileName,
        CancellationToken cancellationToken = default);
}