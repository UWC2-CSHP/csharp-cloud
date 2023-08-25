using HelloWorldService;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<IContactRepository, ContactRepository>(); // ADD ME

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    // true is to allow for controller comments
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);

    // ADD ME to resolve issue with unique method/path
    options.ResolveConflictingActions(apiDesc => apiDesc.First());
});

builder.Services.AddMvc(config => {
    //config.Filters.Add<LoggingActionFilter>(); // ADD ME
}).AddXmlSerializerFormatters();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCors();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseExceptionHandler("/error/500");

app.UseAuthorization();

app.MapControllers();

app.Run();
