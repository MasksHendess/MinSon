using System;
using System.Threading.Tasks;

namespace MinSon
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }

        //public async Task AnnounceJoinedUser(SocketGuildUser user)
        //{
        //    var channel = _client.GetChannel(743578569476931626) as SocketTextChannel; // Gets the channel to send the message in
        //    await channel.SendMessageAsync($"Welcome {user.Mention} to {channel.Guild.Name}"); //Welcomes the new user
        //    return;
        //}
    }
}