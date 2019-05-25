using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pacman.Data
{
    public class ObjectCores
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("X")]
        public float X { get; set; }
        [JsonProperty("Y")]
        public float Y { get; set; }
    }
}
