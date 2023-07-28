using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;

// The function handler that will be called for each Lambda event
var handler = (Data input, ILambdaContext context) =>
{
    context.Logger.LogInformation("This is informational"); // ADD ME
    context.Logger.LogError("This is an error"); // ADD ME

    var result = input.Number1 + input.Number2;

    return new Response { Result = result };
};

// Build the Lambda runtime client passing in the handler to call for each
// event and the JSON serializer to use for translating Lambda JSON documents
// to .NET types.
await LambdaBootstrapBuilder.Create(handler, new DefaultLambdaJsonSerializer())
        .Build()
        .RunAsync();

class Data // ADD ME
{
    public int Number1 { get; set; }
    public int Number2 { get; set; }
}

class Response
{
    public int Result { get; set; }
}