using Microsoft.EntityFrameworkCore;
using FinanceTracker.Data; 

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register DbContext and configure the connection string
builder.Services.AddDbContext<FinanceTrackerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("FinanceTracker.API")));  // Specify the API project as the migrations assembly


// Enable CORS to allow requests from React app (port 3000)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseDeveloperExceptionPage();  // Shows detailed errors during development
}

app.UseCors("AllowReactApp"); // Apply CORS policy

app.UseRouting();

app.UseEndpoints(endpoints =>
{
        endpoints.MapControllers();
});

app.UseHttpsRedirection();


