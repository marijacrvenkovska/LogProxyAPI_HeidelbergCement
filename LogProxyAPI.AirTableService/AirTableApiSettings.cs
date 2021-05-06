using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogProxyAPI.AirTableService
{
    public class AirTableApiSettings
    {
        public const string SectionName = "AirTableApiSettings";
        public string AirTableApiUrl { get; set; }
        public string ApiKey { get; set; }
    }
}
