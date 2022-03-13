using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Domain.Entities
{
    public class discordUser
    {
        public int Id { get; set; }
        public string AvatarHash { get; set; }
        public string AvatarUrl { get; set; }
        public string DefaultAvatarUrl { get; set; }
        public string Discriminator { get; set; }
        public string userName { get; set; }
        public string mention { get; set; }
        public string email { get; set; }

        public string loacal { get; set; }

        public bool? MfAEnabled { get; set; }

        public bool? Verified { get; set; }
        //private void nothing()
        //{
        //    DiscordUser chump = new DiscordUser();

        //    chump.
        //}
    }
}


