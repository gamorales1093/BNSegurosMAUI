using System;
using System.Collections.Generic;

namespace BNSegurosMAUI.Models
{
    public class Sinister
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public List<Step> Steps { get; set; }

        //Not in service
        public string Image { get; set; }
    }
}
