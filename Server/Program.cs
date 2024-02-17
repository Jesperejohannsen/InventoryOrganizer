using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "MyAllowSpecificOrigins",
                          builder =>
                          {
                              builder.WithOrigins("http://localhost:3000") // or the port your React app runs on
                                     .AllowAnyHeader()
                                     .AllowAnyMethod();
                          });
    });

app.UseCors("MyAllowSpecificOrigins");

app.Run();
