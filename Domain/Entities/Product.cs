using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string text1 { get; set; }
        public string text2 { get; set; }
        public string text3 { get; set; }
        public int birthYear { get; set; }
        public string owners { get; set; }
        public string webbUrl { get; set; }
        public string showcase_Item1_Name { get; set; }
        public string showcase_Item1_Text { get; set; }
        public string imageUrl_Item1 { get; set; }
        public string showcase_Item2_Name { get; set; }
        public string showcase_Item2_Text { get; set; }
        public string imageUrl_Item2 { get; set; }

        public string region { get; set; }

        public string webbImage_Item1 { get; set; } // easier for Discord bot to find
        public string webbImage_Item2 { get; set; } // easier for Discord bot to find
    }
}
