using Serilog;
using LoggerExtensions;
using Broxel.Logger.Web.Middleware;
using Broxel.Logger.Web.Configuration;


try
{
    var builder = WebApplication.CreateBuilder(args);

    var isActiveInf = builder.Configuration["DisableLogLevel:isActiveInf"];
    var isActiveWar = builder.Configuration["DisableLogLevel:isActiveWar"];
    var isActiveErr = builder.Configuration["DisableLogLevel:isActiveErr"];

    builder.Configuration.AddBroxelLoggerWeb(options =>
    {
        return options.ReadFrom.Configuration(builder.Configuration);
    });

    builder.Host.UseSerilog();
    //Add services to the container.
    builder.Services.AddControllers();

    builder.Services.AddScoped<ISettings, Settings>();            //Bandera
    builder.Services.AddHttpClient();                             //CorrelationId
    builder.Services.AddHttpContextAccessor();                    //IpAddress


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();


    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.UseMiddleware<Middleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host has terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
