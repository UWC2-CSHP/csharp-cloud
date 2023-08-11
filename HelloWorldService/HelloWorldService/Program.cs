using HelloWorldService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(config => {
    config.Filters.Add<LoggingActionFilter>(); // ADD ME
}).AddXmlSerializerFormatters();

var app = builder.Build();

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
