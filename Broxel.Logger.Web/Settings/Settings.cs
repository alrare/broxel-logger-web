using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broxel.Logger.Web.Configuration
{
    public class Settings : ISettings
    {
        private readonly IConfiguration _configuration;
        public Settings(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string IsActiveInf => _configuration["DisableLogLevel:isActiveInf"];

        public string IsActiveWar => _configuration["DisableLogLevel:isActiveWar"];

        public string IsActiveErr => _configuration["DisableLogLevel:isActiveErr"];


    }
}
