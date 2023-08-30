using Amazon;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace Moments_Backend.Services
{
    public class SaveFileService
    {
        public SaveFileService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration { get; }

        public async Task<string> Execute(IFormFile imageFile)
        {

            string accessKeyId = _configuration.GetValue<string>("AWS-S3:AccessKeyId");
            string secretKeyId = _configuration.GetValue<string>("AWS-S3:SecretKeyId");
            string bucketName = _configuration.GetValue<string>("AWS-S3:BucketName");


            using (var amazonS3client = new AmazonS3Client(accessKeyId, secretKeyId, RegionEndpoint.USEast2))
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
                        Key = imageFile.FileName,
                        // S3 bucket name
                        BucketName = bucketName,
                        // File content type
                        ContentType = imageFile.ContentType,
                        
                        
                    };

                    var transferUtility = new TransferUtility(amazonS3client);
                    await transferUtility.UploadAsync(request);
                    
                }
            }

            return "";

        }
    }
}
