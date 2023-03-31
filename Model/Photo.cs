using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public record Photo
    {
        public string FileName { get; set; } = "";
        public string URI { get; set; }
        public string? ALT { get; set; }

    }
}
