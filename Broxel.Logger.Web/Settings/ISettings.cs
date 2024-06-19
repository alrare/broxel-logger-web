using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Broxel.Logger.Web.Configuration
{
    public interface ISettings
    {
        string IsActiveInf { get; }
        string IsActiveWar { get; }
        string IsActiveErr { get; }
    }
}
