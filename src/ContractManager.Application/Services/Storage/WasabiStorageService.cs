using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;
using ContractManager.Application.Interfaces.Storage;

namespace ContractManager.Application.Services.Storage;

public class WasabiStorageService : IDocumentStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly string _bucketName;

    public WasabiStorageService()
    {
        var config = new AmazonS3Config
        {
            RegionEndpoint = RegionEndpoint.USEast1,
            ServiceURL = "https://s3.sa-east-1.wasabisys.com", // Região da Wasabi
            ForcePathStyle = true
        };

        _s3Client = new AmazonS3Client("YOUR_ACCESS_KEY", "YOUR_SECRET_KEY", config);
        _bucketName = "YOUR_BUCKET_NAME";
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName)
    {
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = fileName,
            BucketName = _bucketName,
            CannedACL = S3CannedACL.PublicRead
        };

        var fileTransferUtility = new TransferUtility(_s3Client);
        await fileTransferUtility.UploadAsync(uploadRequest);

        return $"https://s3.sa-east-1.wasabisys.com/{_bucketName}/{fileName}";
    }

    public async Task<string> UploadAsync(string fileName, byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        return await UploadAsync(stream, fileName);
    }
}