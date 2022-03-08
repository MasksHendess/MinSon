using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    class Polls : BaseCommandModule
    {
        [Command("yeboi")]
        public async Task ButtonsNshit(CommandContext ctx)
        {
            var interactivty = ctx.Client.GetInteractivity();
            // Create button
            var btn = new DiscordButtonComponent(ButtonStyle.Success, "theButton", "Yeboi Kaffekungen!", false);

            //Create a message to attach button to
            var yeboi = new DiscordMessageBuilder();
            // YEEEEEEEEEEEEBOOOOOOOOOOOOOOOOOOOOOOOI
            var embed = new DiscordEmbedBuilder
            {
                Title = "Bookings and reservations",
                Description = "If you wish to book or enquire about a boost with our member teams and guilds, click on a button below to be put in contac with a team matching your request.",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail()
            };
            embed.Thumbnail.Url = "https://static.wikia.nocookie.net/zelda_gamepedia_en/images/6/6b/TLoZ_Series_Majora%27s_Mask_Render.png/revision/latest/scale-to-width-down/320?cb=20210315025921";

            yeboi.AddEmbed(embed);
            yeboi.WithContent(" ").AddComponents(btn);
            var pollMessage = await ctx.Channel.SendMessageAsync(yeboi).ConfigureAwait(false);
        }

        [Command("pollexample")]
        public async Task Poll (CommandContext ctx, TimeSpan duration, params DiscordEmoji[] emojiOptions)
        {
            var interactivty = ctx.Client.GetInteractivity();
            var options = emojiOptions.Select(x => x.ToString());

            var embed = new DiscordEmbedBuilder
            {
                Title = "Poll",
                Description = string.Join(" ", options),
        };

          var pollMessage =  await ctx.Channel.SendMessageAsync(embed).ConfigureAwait(false);

            foreach (var option in emojiOptions)
            {
                await pollMessage.CreateReactionAsync(option).ConfigureAwait(false);
            }

            var result = await interactivty.CollectReactionsAsync(pollMessage, duration).ConfigureAwait(false);
            var distinctResult = result.Distinct(); // Duplicate protection
            var results = distinctResult.Select(x => $"{x.Emoji}: {x.Total}");

            await ctx.Channel.SendMessageAsync(string.Join("\n", results)).ConfigureAwait(false);
        }
    }
}
