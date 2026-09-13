using Amazon.Runtime.SharedInterfaces;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using ShopApplication.Repositories;

namespace ShopInfrastructure.services.FileStorage;

public class ArvanCloudFileStorage(
    IAmazonS3 s3Client,
    IConfiguration configuration) : IFileStorage
{
    private readonly string? _bucketName = configuration["ArvanCloud:BucketName"];

    public async Task<string> UploadAsync(
        Stream file,
        string fileName,
        string contentType,
        CancellationToken cancellationToken = default)
    {
        var key = fileName;

        var request = new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = file,
            ContentType = contentType
        };

        await s3Client.PutObjectAsync(
            request,
            cancellationToken);

        return key;
    }

    public async Task DeleteAsync(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = _bucketName,
            Key = fileName
        };

        await s3Client.DeleteObjectAsync(
            request,
            cancellationToken);
    }

    public async Task<Stream?> GetAsync(
        string fileName,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _bucketName,
                Key = fileName
            };

            var response = await s3Client.GetObjectAsync(
                request,
                cancellationToken);

            var memoryStream = new MemoryStream();

            await response.ResponseStream.CopyToAsync(
                memoryStream,
                cancellationToken);

            memoryStream.Position = 0;

            return memoryStream;
        }
        catch (AmazonS3Exception ex)
            when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }
}