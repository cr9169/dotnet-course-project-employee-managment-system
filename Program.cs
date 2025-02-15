using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Registers AppDbContext in the dependency injection (DI) container , making it available for use throughout the application.
builder.Services.AddDbContext<AppDbContext>(
    // Configures the database context options to use an in-memory database named "EmployeeDb".
    options => options.UseInMemoryDatabase("EmployeeDb")
);

// Read allowed origins from configuration (appsettings.json)
string[]? allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();

builder.Services.AddCors(
    options => options.AddPolicy("MyCors", builder => {
        if (allowedOrigins != null)
        {
            builder.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader();
        }
    })
);

// AddScoped adds a service, the EmployeeRepository to the DI system for the current life time (Scoped).
// This means that for every HTTP request, it will create a new instance of the EmployeeRepository.
// This means every time IEmployeeRepository is requested, DI creates an EmployeeRepository instance.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();

builder.Services.AddControllers();

// Register Swagger
// find all the methods in the controllers to create a configuration file for the API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Checks if the application is running in the development environment.
if(app.Environment.IsDevelopment()) {
    // Enables Swagger middleware to generate the API documentation.
    app.UseSwagger();
    // Configures the Swagger UI for interactive API documentation.
    app.UseSwaggerUI(c => {
        // Defines the Swagger JSON file location and sets the API title.
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        // Sets Swagger UI as the default page by removing the /swagger prefix.
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("MyCors");

// Maps all controller routes to handle HTTP requests in the application.
// This ensures that API endpoints defined in controllers (marked with [ApiController]) are correctly registered and accessible.
app.MapControllers();

app.Run();


