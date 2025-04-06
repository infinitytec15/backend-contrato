using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ContractManager.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace ContractManager.Infrastructure.Services;

public class WasabiStorageService : IStorageService
{
    private readonly string _accessKey;
    private readonly string _secretKey;
    private readonly string _bucketName;
    private readonly string _serviceUrl;

    public WasabiStorageService(IConfiguration config)
    {
        _accessKey = config["Wasabi:AccessKey"]!;
        _secretKey = config["Wasabi:SecretKey"]!;
        _bucketName = config["Wasabi:BucketName"]!;
        _serviceUrl = config["Wasabi:ServiceUrl"]!;
    }

    public async Task<string> UploadAsync(Stream fileStream, string fileName)
    {
        var config = new AmazonS3Config
        {
            ServiceURL = _serviceUrl,
            ForcePathStyle = true
        };

        using var client = new AmazonS3Client(_accessKey, _secretKey, config);
        var uploadRequest = new TransferUtilityUploadRequest
        {
            InputStream = fileStream,
            Key = fileName,
            BucketName = _bucketName,
            CannedACL = S3CannedACL.PublicRead
        };

        var transferUtility = new TransferUtility(client);
        await transferUtility.UploadAsync(uploadRequest);

        return $"{_serviceUrl}/{_bucketName}/{fileName}";
    }
}