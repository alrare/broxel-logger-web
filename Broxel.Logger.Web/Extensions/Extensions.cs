
using Serilog;
using Serilog.Context;
using System.Reflection;
using Serilog.Sinks.GoogleCloudLogging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Serilog.Filters;
using Serilog.Exceptions;



namespace LoggerExtensions
{
    public static class Extensions
    {
        public static IConfiguration AddBroxelLoggerWeb(this IConfiguration configuration, Func<LoggerConfiguration, LoggerConfiguration>? configure = null)
        {
            var config = new GoogleCloudLoggingSinkOptions();

            var keyFile = configuration.GetValue<string>("GoogleCloudLogging:KeyFile");
            configuration.GetSection("GoogleCloudLogging").Bind(config);
            var resource = Environment.CurrentDirectory + "/" + keyFile;

            if (resource != null)
            {
                using var sr = new StreamReader(resource);
                config.GoogleCredentialJson = sr.ReadToEnd();
            }

            var log = new LoggerConfiguration();
            if (configure != null)
            {
                log = configure(log);
            }

            Log.Logger = log
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .WriteTo.GoogleCloudLogging(config)
                //.WriteTo.Logger(lc => lc.Filter.ByIncludingOnly(Matching.WithProperty("Level", "Information")))
                .CreateLogger();

            return configuration;
        }
    }
}
