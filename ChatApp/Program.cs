using ChatApp.Hub;
using ChatApp.Startup;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(options =>
options.UseLazyLoadingProxies()
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.RegisterServices();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandeling>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("chatHub");

app.UseCors(policy =>
    policy
        .WithOrigins("http://localhost:4200") 
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());

app.Run();


