using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace MinSon
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
            //var bot = new Bot();
            //bot.RunAsync().GetAwaiter().GetResult();
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        //public async Task AnnounceJoinedUser(SocketGuildUser user) //welcomes New Players
        //{
        //    var channel = Client.GetChannelAsync(899731960690249760); //gets channel to send message in
        //    await channel.Result.SendMessageAsync("Welcome " + user.Mention + " to the server!"); //Welcomes the new user
        //}

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        //public async Task AnnounceJoinedUser(SocketGuildUser user)
        //{
        //    var channel = _client.GetChannel(743578569476931626) as SocketTextChannel; // Gets the channel to send the message in
        //    await channel.SendMessageAsync($"Welcome {user.Mention} to {channel.Guild.Name}"); //Welcomes the new user
        //    return;
        //}
    }
}

