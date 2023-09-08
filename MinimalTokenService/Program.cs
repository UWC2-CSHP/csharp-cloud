using MinimalTokenService;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/{userName}/{password}", (string userName, string password) => { 
    var token = TokenHelper.GetToken(userName, password);

    return Results.Ok(new TokenResponse { Token =  token});
});

app.MapPost("/", (TokenRequest tokenRequest) => {
    var token = TokenHelper.GetToken(
        tokenRequest.UserName, tokenRequest.Password);

    return Results.Ok(new TokenResponse{ Token = token });
});

app.Run();
