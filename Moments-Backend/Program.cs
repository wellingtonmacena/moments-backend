using Microsoft.Extensions.FileProviders;
using Moments_Backend.Data;
using Moments_Backend.Interfaces;
using Moments_Backend.Repositories;
using Moments_Backend.Repositories.Interfaces;
using Moments_Backend.Services;
using Newtonsoft.Json.Serialization;
using Serilog;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
ConfigureServices(builder.Services);
Configure(builder);

var app = builder.Build();
app.UseCors("AllowAngularOrigins");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseWatchDogExceptionLogger();
//app.UseWatchDog(opt =>
//{
//    opt.WatchPageUsername = builder.Configuration["LogsCredentials:User"];
//    opt.WatchPagePassword = builder.Configuration["LogsCredentials:Password"];
//});



app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Environment.CurrentDirectory, "Uploads")),
    RequestPath = "/Uploads"
});

Debug.WriteLine(Path.Combine(Environment.CurrentDirectory, "Uploads"));
Console.WriteLine(Path.Combine(Environment.CurrentDirectory, "Uploads"));
app.MapControllers();

app.Run();


static void Configure(WebApplicationBuilder builder)
{
    builder.Host.UseSerilog((hostBuilder, loggerConfiguration) =>
    {
        var elasticsearchSettings = hostBuilder.Configuration["ElasticConfiguration:Uri"];

        var envName = builder.Environment.EnvironmentName.ToLower().Replace(".", "-");
        var yourAppName = hostBuilder.Configuration["ElasticConfiguration:AppName"];
        var yourTemplateName = hostBuilder.Configuration["ElasticConfiguration:TemplateName"];
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
         .WriteTo.Debug()
         .WriteTo.Console()
        .Enrich.WithProperty("Environment", environment)
        .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticsearchSettings))
        {
            IndexFormat = $"{yourAppName}-{envName}-{DateTimeOffset.Now:yyyy-MM}",
            AutoRegisterTemplate = true,
            OverwriteTemplate = true,
            TemplateName = yourTemplateName,
            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
            TypeName = null,
            BatchAction = ElasticOpType.Create
        });
    });
}

void ConfigureServices(IServiceCollection services)
{
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAngularOrigins",
        builder =>
        {
            builder.WithOrigins(
                                "http://localhost:4200"
                                )
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
    });

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
    //builder.Services.AddWatchDogServices(opt =>
    //{
    //    opt.IsAutoClear = false;
    //    // opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Weekly;
    //    opt.SetExternalDbConnString = builder.Configuration["MomentsDatabase:MomentsLogs"];
    //    opt.DbDriverOption = WatchDogDbDriverEnum.PostgreSql;
    //});

    builder.Services.AddDbContext<AppDbContext, LocalPostgresContext>(ServiceLifetime.Transient);
    builder.Services.AddSingleton<IMomentRepository, PostgresMomentRepository>();
    builder.Services.AddSingleton<IHandleFile, LocalHandleFileService>();
}
