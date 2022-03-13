using DSharpPlus.Entities;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class DiscordRepository : IDiscordRepository
    {
        private readonly MinSonDBContext dbContext;
        public DiscordRepository(MinSonDBContext DBContext)
        {
            dbContext = DBContext;
        }

        public async void saveMembers(List<DiscordMember> members)
        {
            var users = dbContext.discordUsers.ToList();
            List<discordUser> newMembers = new List<discordUser>();
            foreach (var wingus in members)
            {
                discordUser newUser = new discordUser();

                newUser.AvatarHash = wingus.AvatarHash;
                newUser.AvatarUrl = wingus.AvatarUrl;
                newUser.DefaultAvatarUrl = wingus.DefaultAvatarUrl;
                newUser.email = wingus.Email;
                newUser.mention = wingus.Mention;
                newUser.userName = wingus.Username;
                newUser.Discriminator = wingus.Discriminator;
                newUser.loacal = wingus.Locale;
                newUser.Verified = wingus.Verified;
                
                if (users.Where(x => x.userName == wingus.Username).Count() == 0 && wingus.IsBot != true)
                {
                    //If new eamil is unique, person does not exsist in database
                    // ITS ANTI DUPLICATES

                    dbContext.discordUsers.Add(newUser);
                    dbContext.SaveChanges();
                }
            }

        }

    }
}
