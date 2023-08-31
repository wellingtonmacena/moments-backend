using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Moments_Backend.Data;
using Moments_Backend.Interfaces;
using Moments_Backend.Repositories;
using Moments_Backend.Repositories.Interfaces;
using Moments_Backend.Services;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers()
       .AddNewtonsoftJson(options =>
       {
           options.SerializerSettings.ContractResolver = new DefaultContractResolver();
           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
       });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<AppDbContext, LocalPostgresContext>();
builder.Services.AddTransient<ICommentRepository, PostgresCommentRepository>();
builder.Services.AddTransient<IMomentRepository, PostgresMomentRepository>();
builder.Services.AddTransient<IHandleFile, AWSHandleFileService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine( Environment.CurrentDirectory, "Uploads")),
    RequestPath = "/Uploads"
});
app.MapControllers();

app.Run();
