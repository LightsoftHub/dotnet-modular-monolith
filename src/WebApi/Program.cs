using FluentValidation.AspNetCore;
using Light.Serilog;
using ModularMonolith.Infrastructure;
using ModularMonolith.WebApi;
using Serilog;
using Spectre.Console;

SerilogExtensions.EnsureInitialized();

AnsiConsole.Write(new FigletText("MM API").Color(Color.Blue));

try
{
    var builder = WebApplication.CreateBuilder(args);

    //builder.LoadJsonConfigurations();
    builder.Host.ConfigureSerilog();

    // Add services to the container.

    builder.Services.AddInfrastructureServices();
    builder.Services.ConfigureServices(builder.Configuration);

    //builder.Services.AddHealthChecksService();

    builder.Services
        .AddFluentValidationAutoValidation()
        .AddFluentValidationClientsideAdapters();

    builder.Services
        .AddLowercaseControllers()
        .AddDefaultJsonOptions()
        .AddInvalidModelStateHandler();

    var app = builder.Build();

    // Configure the HTTP request pipeline.

    app.UseHttpsRedirection();

    //app.MapHealthChecksEndpoint();

    app.ConfigurePipelines(builder.Configuration);

    app.MapEndpoints();

    app.Run();
}
catch (Exception ex) when (!ex.GetType().Name.Equals("StopTheHostException", StringComparison.Ordinal))
{
    SerilogExtensions.EnsureInitialized();
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete.");
    Log.CloseAndFlush();
}
