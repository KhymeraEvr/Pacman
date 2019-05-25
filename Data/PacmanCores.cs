using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    class PacmanCores: ObjectCores
    {
        [JsonProperty("Direction")]
        public string Direction { get; set; }
    }
}
