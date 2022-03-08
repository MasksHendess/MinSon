using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using MinSon.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class TeamCommands : BaseCommandModule
    {
        [Command("rolesjoin")]
        public async Task join(CommandContext ctx)
        {
            var joinEmbed = new DiscordEmbedBuilder
            {
                Title = "Boobies and Butts",
                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail { Url = ctx.Client.CurrentUser.AvatarUrl },
                //Url = ctx.Client.CurrentUser.AvatarUrl,
                Color = DiscordColor.Green
            };

            var joinMessage = await ctx.Channel.SendMessageAsync(embed: joinEmbed).ConfigureAwait(false);

            var thumbUpEmoji = DiscordEmoji.FromName(ctx.Client, ":+1:");
            var thumbDownEmoji = DiscordEmoji.FromName(ctx.Client, ":-1:");

            await joinMessage.CreateReactionAsync(thumbUpEmoji).ConfigureAwait(false);
            await joinMessage.CreateReactionAsync(thumbDownEmoji).ConfigureAwait(false);

            var interactivity = ctx.Client.GetInteractivity();
            var result = await interactivity.WaitForReactionAsync(
                 x => x.Message == joinMessage &&
                 x.User == ctx.User &&
                (x.Emoji == thumbUpEmoji ||
                 x.Emoji == thumbDownEmoji)).ConfigureAwait(false);

            if (result.Result.Emoji == thumbUpEmoji)
            {
                var role = ctx.Guild.GetRole(942161938933219380); // Such hardcode value much sad 
                await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
            }
            else if (result.Result.Emoji == thumbDownEmoji)
            {
                var role = ctx.Guild.GetRole(942161938933219380); // Such hardcode value much sad 
                await ctx.Member.RevokeRoleAsync(role).ConfigureAwait(false);
            }
            else
            {

            }

            await joinMessage.DeleteAsync().ConfigureAwait(false);
        }

        [Command("rolegive")]
        public async Task sort(CommandContext ctx, string rolename)
        {
            // get all users with no role
            // assign role
            var role = GetRoleByName(ctx, rolename); // get role using a rolenmae instead of id
            if (role != null)
            {
                var members = ctx.Guild.Members;

                foreach (var member in members)
                {
                    if (member.Value.Roles.Count() <= 0) // condition which members gets assigned role.
                    {
                        await member.Value.GrantRoleAsync(role).ConfigureAwait(false);
                    }

                    // await ctx.Member.GrantRoleAsync(role).ConfigureAwait(false);
                }
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Invalid role").ConfigureAwait(false);
            }
        }

        [Command("roletake")]
        public async Task unsort(CommandContext ctx, string rolename)
        {
            // get all users with no role
            // remove role
            var role = GetRoleByName(ctx, rolename); // not hardcode very pro
            if (role != null)
            {
                var members = ctx.Guild.Members;

                foreach (var member in members)
                {
                    if (member.Value.Roles.Count() >= 0) // dont think if statment needed ? 
                    {
                        await member.Value.RevokeRoleAsync(role).ConfigureAwait(false);
                        await ctx.Channel.SendMessageAsync("Revoked role " + role.Name + " from " + member.Value.Username).ConfigureAwait(false);
                    }

                }

            }
        }
        private DiscordRole GetRoleByName(CommandContext ctx, string name)
        {
            var result = ctx.Guild.Roles.Where(x => x.Value.Name == name).FirstOrDefault();

            return result.Value;
        }
    }
}