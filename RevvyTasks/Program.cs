using FluentValidation;
using RevvyTasks.Abstractions;
using RevvyTasks.Services;
using RevvyTasks.Controllers;
using RevvyTasks.Dtos;
using RevvyTasks.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


builder.Services.AddScoped<IValidator<IClerkCollection>, ClerkCollectionValidator>();
builder.Services.AddScoped<ICertificateChainCreator, CertificateChainCreator>();
builder.Services.AddScoped<ISumRepresentationFinder, SumRepresentationFinder>();
builder.Services.AddSingleton<ITaskTester, TaskTester>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.Lifetime.ApplicationStarted.Register( async () =>
{
    using var httpClient = new HttpClient();

    var testService = app.Services.GetRequiredService<ITaskTester>();

    await testService.SetupFirstTask(httpClient);
    await testService.SetupSecondTask(httpClient);
        
});

app.MapControllers();

app.MapSumFinderEndpoints();

app.Run();


