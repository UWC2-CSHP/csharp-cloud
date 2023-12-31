using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProcessS3Input;

public class Function
{
    IAmazonS3 S3Client { get; set; }

    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {
        S3Client = new AmazonS3Client();
    }

    /// <summary>
    /// Constructs an instance with a preconfigured S3 client. This can be used for testing outside of the Lambda environment.
    /// </summary>
    /// <param name="s3Client"></param>
    public Function(IAmazonS3 s3Client)
    {
        this.S3Client = s3Client;
    }

    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
    /// to respond to S3 notifications.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(S3Event evnt, ILambdaContext context)
    {
        context.Logger.LogInformation("ProcessS3Input BEGIN 1.0"); // ADD ME

        // string destinationBucketName;

        // context.ClientContext.Environment.TryGetValue("DestinationBucket", out destinationBucketName);

        // context.Logger.LogInformation(string.Format("DestinationBucketName =[{0}]", destinationBucketName)); // ADD ME

        var eventRecords = evnt.Records ?? new List<S3Event.S3EventNotificationRecord>();

        foreach (var record in eventRecords)
        {
            var s3Event = record.S3;
            if (s3Event == null)
            {
                context.Logger.LogInformation("NULL s3Event"); // ADD ME
                continue;
            }

            try
            {
                context.Logger.LogInformation(s3Event.Object.Key); // ADD ME

                var response = await this.S3Client.GetObjectMetadataAsync(s3Event.Bucket.Name, s3Event.Object.Key);

                context.Logger.LogInformation(response.Headers.ContentType);
            }
            catch (Exception e)
            {
                context.Logger.LogError($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogError(e.Message);
                context.Logger.LogError(e.StackTrace);
                throw;
            }

            CopyToOutput(s3Event.Bucket.Name, s3Event.Object.Key);
            DeleteInput(s3Event.Bucket.Name, s3Event.Object.Key);
        }

        context.Logger.LogInformation("ProcessS3Input END"); // ADD ME
    }

    private void DeleteInput(string sourceBucketName, string sourceObjectKey)
    {
        var deleteObjectRequest = new DeleteObjectRequest
        {
            BucketName = sourceBucketName,
            Key = sourceObjectKey
        };

        var response = S3Client.DeleteObjectAsync(deleteObjectRequest).Result;

    }

    private void CopyToOutput(string sourceBucketName, string sourceObjectKey)
    {
        var destinationBucketName = "uw-output";
        var destinationObjectKey = sourceObjectKey;

        var response = new CopyObjectResponse();
        try
        {
            var request = new CopyObjectRequest
            {
                SourceBucket = sourceBucketName,
                SourceKey = sourceObjectKey,
                DestinationBucket = destinationBucketName,
                DestinationKey = destinationObjectKey,
            };
            response = S3Client.CopyObjectAsync(request).Result;
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error copying object: '{ex.Message}'");
        }

    }
}