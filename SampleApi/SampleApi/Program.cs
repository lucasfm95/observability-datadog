using Serilog;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .Enrich.WithProperty("ServiceName", "SampleApi")
#if DEBUG
    .WriteTo.Console()
#else
    .WriteTo.Console(new JsonFormatter(renderMessage: true))
#endif
    .CreateLogger();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Host.UseSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();