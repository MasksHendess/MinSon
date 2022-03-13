using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emzi0767.Utilities;
using MinSon.Commands;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.VoiceNext;
using DSharpPlus.Net;
using DSharpPlus.Lavalink;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using DSharpPlus.Entities;

namespace MinSon
{
   public class Bot
    {
      
        public DiscordClient Client { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
    
        public Bot(IServiceProvider services)
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = sr.ReadToEnd();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                //UseInternalLogHandler = true,
            };
            Client = new DiscordClient(config);


            Client.Ready += OnClientReady;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(5)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = false,
                DmHelp = true,
                Services = services
            };
            Commands = Client.UseCommandsNext(commandsConfig);
            //Client.ComponentInteractionCreated += async (s, e) =>
            //{

            //    await e.Interaction.CreateResponseAsync(InteractionResponseType.UpdateMessage, new DiscordInteractionResponseBuilder().WithContent("?"));
               
            //};
            #region Commands 
            //Register Command classes here
            Commands.RegisterCommands<Dice>();
            Commands.RegisterCommands<Mtgio>();
            Commands.RegisterCommands<TeamCommands>();
            Commands.RegisterCommands<Polls>();
            Commands.RegisterCommands<HerroWorld>();
            Commands.RegisterCommands<databaseCommands>();
            #endregion
            Client.ConnectAsync();
            // await lavalink.ConnectAsync(lavalinkConfig); // Make sure this is after Discord.ConnectAsync(). 
           //  await Task.Delay(-1);
        }

        // run bot not web edition
        //public async Task RunAsync()
        //{
        //    var json = string.Empty;

        //    using (var fs = File.OpenRead("config.json"))
        //    using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
        //        json = await sr.ReadToEndAsync().ConfigureAwait(false);

        //    var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

        //    var config = new DiscordConfiguration
        //    {
        //        Token = configJson.Token,
        //        TokenType = TokenType.Bot,
        //        AutoReconnect = true,
        //        MinimumLogLevel = LogLevel.Debug,
        //        //UseInternalLogHandler = true,
        //    };
        //    Client = new DiscordClient(config);


        //    Client.Ready += OnClientReady;

        //    Client.UseInteractivity(new InteractivityConfiguration
        //    {
        //        Timeout = TimeSpan.FromMinutes(5)
        //    });

        //    var commandsConfig = new CommandsNextConfiguration
        //    {
        //        StringPrefixes = new string[] { configJson.Prefix },
        //        EnableMentionPrefix = true,
        //        EnableDms = false,
        //        DmHelp = true,
        //    };
        //    Commands = Client.UseCommandsNext(commandsConfig);



        //    #region Commands 
        //    //Register Command classes here
        //    Commands.RegisterCommands<Dice>();
        //    Commands.RegisterCommands<Mtgio>();
        //    Commands.RegisterCommands<TeamCommands>();
        //    Commands.RegisterCommands<Polls>();
        //    Commands.RegisterCommands<HerroWorld>();
        //    #endregion
        //    Client.ConnectAsync();
        //    // await lavalink.ConnectAsync(lavalinkConfig); // Make sure this is after Discord.ConnectAsync(). 
        //    await Task.Delay(-1);
        //}
        private Task OnClientReady(object sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }

    }
}
