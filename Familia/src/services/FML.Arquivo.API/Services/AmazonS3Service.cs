using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using FML.File.API.Data.Cont;
using FML.File.API.Services.Interfaces;

namespace FML.File.API.Services
{
    public class AmazonS3Service : IAmazonS3Service
    {
        private readonly IAmazonS3 _awsS3Client;

        public AmazonS3Service(IConfiguration configuration)
        {
            //var secretsManagerClient = new AmazonSecretsManagerClient();
            //var awsKeyID = GetSecretValue(secretsManagerClient, Constants.AwsKeyID);
            //var awsSecretKey = GetSecretValue(secretsManagerClient, Constants.AwsSecretKey);

            var awsKeyID = configuration[Constants.AwsKeyID];
            var awsSecretKey = configuration[Constants.AwsSecretKey];

            if (string.IsNullOrEmpty(awsKeyID) || string.IsNullOrEmpty(awsSecretKey))
            {
                throw new InvalidOperationException("AWS credentials are not set in the environment variables.");
            }

            var basicAwsCredentials = new BasicAWSCredentials(awsKeyID, awsSecretKey);
            var cfg = new AmazonS3Config
            {
                RegionEndpoint = Amazon.RegionEndpoint.USEast1
            };

            _awsS3Client = new AmazonS3Client(basicAwsCredentials, cfg);
        }

        public async Task<bool> SaveFileAsync(string bucket, string key, IFormFile file)
        {
            using var newMemoryStream = new MemoryStream();
            await file.CopyToAsync(newMemoryStream);

            var fileTransferUtility = new TransferUtility(_awsS3Client);

            await fileTransferUtility.UploadAsync(new TransferUtilityUploadRequest
            {
                InputStream = newMemoryStream,
                BucketName = bucket,
                Key = key,
                ContentType = file.ContentType
            });

            return true;
        }

        public async Task<string> GetFileUrlAsync(string bucket, string key)
        {
            var request = new GetPreSignedUrlRequest
            {
                BucketName = bucket,
                Key = key,
                Expires = DateTime.Now.AddHours(1)
            };
            return _awsS3Client.GetPreSignedURL(request);
        }

        private string GetSecretValue(IAmazonSecretsManager secretsManagerClient, string secretName)
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };

            var response = secretsManagerClient.GetSecretValueAsync(request).Result;
            return response.SecretString;
        }
    }
}
