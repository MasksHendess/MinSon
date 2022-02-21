using DSharpPlus;
using DSharpPlus.Entities;
using MinSon.Handlers.Dialogue.Steps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Handlers.Dialogue
{
    public class DialogueHandler
    {
        private readonly DiscordClient _client;
        private readonly DiscordChannel _channel;
        private IDialogueStep currentstep;
        private readonly DiscordUser _user;


        public DialogueHandler(DiscordClient client, DiscordChannel channel, DiscordUser user, IDialogueStep startingstep)
        {
            _client = client;
            _channel = channel;
            _user = user;
            currentstep = startingstep;
        }


        private readonly List<DiscordMessage> messages = new List<DiscordMessage>();

        public async Task<bool> ProcessStep()
        {
            while (currentstep != null)
            {
                currentstep.OnMessageAdded += (message) => messages.Add(message);

                bool canceled = await currentstep.ProcessStep(_client, _channel, _user).ConfigureAwait(false);

                if(canceled)
                {
                    await DeleteMessages().ConfigureAwait(false);

                    var cancelEmbed = new DiscordEmbedBuilder
                    {
                        Title = "Dialouge has successfully been canceled",
                        Description = _user.Mention,
                        Color = DiscordColor.Green
                    };

                    await _channel.SendMessageAsync(embed: cancelEmbed).ConfigureAwait(false);

                    return false;
                }

                currentstep = currentstep.NextStep;
            }

            await DeleteMessages().ConfigureAwait(false);

            return true;
        }

        private async Task DeleteMessages()
        {
            if (_channel.IsPrivate) { return; }

            foreach (var message in messages)
            {
                await message.DeleteAsync().ConfigureAwait(false);
            }
        }
    }
}
