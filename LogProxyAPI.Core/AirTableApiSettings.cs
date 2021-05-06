using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.Core
{
    public class AirTableApiSettings
    {
        public const string SectionName = nameof(AirTableApiSettings);
        public string AirTableApiUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
