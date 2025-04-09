using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SdaemonAPiTest.Models;

var builder = WebApplication.CreateBuilder(args); // Create a builder for configuring app services and middleware

// ----------------------------------------------------------
// Register Services to the Dependency Injection Container
// ----------------------------------------------------------

// Register DbContext with connection string from appsettings.json
builder.Services.AddDbContext<SdaemonTestContext>(Options =>
    Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register controller support and configure JSON options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Preserve object references in JSON (avoids circular reference issues)
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

builder.Services.AddControllers(); // Add controller support again (can be merged above)

// Register Swagger/OpenAPI services for API documentation
builder.Services.AddEndpointsApiExplorer(); // Helps expose minimal APIs and controllers in Swagger
builder.Services.AddSwaggerGen(); // Generates Swagger/OpenAPI docs

var app = builder.Build(); // Build the application pipeline

// ----------------------------------------------------------
// Configure the HTTP request pipeline (Middleware setup)
// ----------------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable Swagger in development mode
    app.UseSwaggerUI(); // Show Swagger UI for interactive API testing
}

app.UseHttpsRedirection(); // Force HTTPS redirection for security

app.UseAuthorization(); // Enable Authorization middleware

app.MapControllers(); // Map controller endpoints (attribute routing)

app.Run(); // Run the application
