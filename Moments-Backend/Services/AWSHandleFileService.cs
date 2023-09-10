using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Moments_Backend.Interfaces;
using Moments_Backend.Models.DTOs;
using Moments_Backend.Utils;
using SharpCompress.Common;
using System.Diagnostics;

namespace Moments_Backend.Services
{
    public class AWSHandleFileService : IHandleFile
    {
        private string _accessKeyId;
        private string _secretKeyId;
        private string _bucketName;

        public AWSHandleFileService(IConfiguration configuration)
        {
            _configuration = configuration;
            _accessKeyId = _configuration.GetValue<string>("AWS-S3:PRD:AccessKeyId");
            _secretKeyId = _configuration.GetValue<string>("AWS-S3:PRD:SecretKeyId");
            _bucketName = _configuration.GetValue<string>("AWS-S3:PRD:BucketName");
        }

        private IConfiguration _configuration { get; }

        public async Task<HandleFileDTO> Save(IFormFile imageFile)
        {
            using (var amazonS3client = new AmazonS3Client(_accessKeyId, _secretKeyId, RegionEndpoint.USEast2))
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Copy file content to memory stream
                    imageFile.CopyTo(memoryStream);
                    // Create request with file name, bucket name and file content/memory stream
                    var request = new TransferUtilityUploadRequest
                    {
                        InputStream = memoryStream,
                        // File name
                        Key = ImageUtils.GenerateNewFilename(imageFile.FileName),
                        // S3 bucket name
                        BucketName = _bucketName,
                        // File content type
                        ContentType = imageFile.ContentType,
                        StorageClass = S3StorageClass.StandardInfrequentAccess,
                        PartSize = imageFile.Length,
                        CannedACL = S3CannedACL.PublicRead,
                    };

                    var transferUtility = new TransferUtility(amazonS3client);
                    await transferUtility.UploadAsync(request);

                    return new HandleFileDTO($"https://{_bucketName}.s3.us-east-2.amazonaws.com/{request.Key}", request.Key);
                }
            }
        }

        public async Task<bool> Delete(string filepath)
        {
            using (var amazonS3client = new AmazonS3Client(_accessKeyId, _secretKeyId, RegionEndpoint.USEast2))
            {
                var transferUtility = new TransferUtility(amazonS3client);
                await transferUtility.S3Client.DeleteObjectAsync(_bucketName, filepath);
                return true;
            }
        }

        public async Task<bool> DeleteAll()
        {
            try
            {
                using (var amazonS3client = new AmazonS3Client(_accessKeyId, _secretKeyId, RegionEndpoint.USEast2))
                {
                    var request = new ListObjectsRequest
                    {
                        BucketName = _bucketName
                    };

                    var response = await amazonS3client.ListObjectsAsync(request);
                    var keys = new List<KeyVersion>();
                    foreach (var item in response.S3Objects)
                    {
                        // Here you can provide VersionId as well.
                        keys.Add(new KeyVersion { Key = item.Key });
                        Debug.WriteLine(item.Key);
                    }

                    var multiObjectDeleteRequest = new DeleteObjectsRequest()
                    {
                        BucketName = _bucketName,
                        Objects = keys
                    };

                    var deleteObjectsResponse = await amazonS3client.DeleteObjectsAsync(multiObjectDeleteRequest);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
            
        }
    }
}
