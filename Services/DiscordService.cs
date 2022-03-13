using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
    public class DiscordService : IDiscordService
    {
        private readonly IDiscordRepository discordRepository;

        public DiscordService(IDiscordRepository discordRepository_)
        {
            discordRepository = discordRepository_;
        }
        public async void saveMembers(List<DiscordMember> members)
        {
            discordRepository.saveMembers(members);
        }
    }
}
