var builder = WebApplication.CreateBuilder(args);

//Add controller routes
builder.Services.AddControllers();

//Add Swagger settings
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure development environment settings
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DualWrite.RFE.Api v1");
    });
    Console.WriteLine("API available at:");
    Console.WriteLine("http://localhost:5059");
    Console.WriteLine("Swagger: http://localhost:5059/swagger");

    app.Lifetime.ApplicationStarted.Register(() =>
    {
        var url = app.Urls.FirstOrDefault();

        if (url is not null)
        {
            System.Diagnostics.Process.Start(
                "xdg-open",
                $"{url}/swagger"
            );
        }
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
