using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.SecretsManager.Model;
using Amazon.SecretsManager;
using System.Text.Json;
using Amazon;
using System.Text.Json.Serialization;

// The function handler that will be called for each Lambda event
var handler = (Data input, ILambdaContext context) =>
{
    context.Logger.LogInformation("This is informational"); // ADD ME
    context.Logger.LogError("This is an error"); // ADD ME

    string secretName = "MyDatabaseCreds";
    string region = "us-west-2";

    IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

    GetSecretValueRequest request = new GetSecretValueRequest
    {
        SecretId = secretName,
        VersionStage = "AWSCURRENT", // VersionStage defaults to AWSCURRENT if unspecified.
    };

    GetSecretValueResponse response;

    try
    {
        response = client.GetSecretValueAsync(request).Result;
    }
    catch (Exception e)
    {
        // For a list of the exceptions thrown, see
        // https://docs.aws.amazon.com/secretsmanager/latest/apireference/API_GetSecretValue.html
        throw e;
    }

    var json = JsonSerializer.Serialize(response);

    context.Logger.LogError(json);
    // {"ARN":"arn:aws:secretsmanager:us-west-2:217532073384:secret:MyDatabaseCreds-5R03ZY","CreatedDate":"2023-09-15T02:55:30.821Z","Name":"MyDatabaseCreds","SecretBinary":null,"SecretString":"{\u0022Password\u0022:\u0022MyPasswordValue\u0022,\u0022UserName\u0022:\u0022MyUserName\u0022}","VersionId":"91ccda93-9c1e-4940-8be5-e542c12c3cb5","VersionStages":["AWSCURRENT"],"ResponseMetadata":{"RequestId":"96874d37-d54a-461a-8f83-9e0d2e2676c7","Metadata":{}},"ContentLength":303,"HttpStatusCode":200}

    string secret = response.SecretString;
    context.Logger.LogError(secret);
    // {"Password":"MyPasswordValue","UserName":"MyUserName"}

    var creds = JsonSerializer.Deserialize<Creds>(secret);
    context.Logger.LogError(string.Format("UserName: [{0}]", creds.UserName));
    context.Logger.LogError(string.Format("Password: [{0}]", creds.Password));

    var result = input.Number1 + input.Number2;

    return new Response { Result = result };
};

// Build the Lambda runtime client passing in the handler to call for each
// event and the JSON serializer to use for translating Lambda JSON documents
// to .NET types.
await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
        .Build()
        .RunAsync();

class Creds
{
    public string Password { get; set; }
    public string UserName { get; set; }
}

class Data // ADD ME
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
}

class Response
{
    public int Result { get; set; }
}