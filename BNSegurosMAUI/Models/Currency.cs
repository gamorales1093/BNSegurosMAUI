using System;
using Newtonsoft.Json;

namespace BNSegurosMAUI.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [JsonProperty("Currency")]
        public string Symbol { get; set; }
    }
}
