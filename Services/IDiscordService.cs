using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinSon.Services
{
   public interface IDiscordService
    {
        void saveMembers(List<DiscordMember> members);
    }
}
