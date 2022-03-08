using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Domain.Entities
{
    public class ShowcaseProduct
    {
        public int Id { get; set; }
        public string webbUrl { get; set; }
        public string brandName { get; set; }
        public string showcase_Item1_Name { get; set; }
        public string showcase_Item1_Text { get; set; }
        public int birthYear { get; set; }
        public string owners { get; set; }
        public string region { get; set; }

        public string webbImage_Item1 { get; set; } // easier for Discord bot to find
    }
}
