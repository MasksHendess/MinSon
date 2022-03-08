using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MinSon.Attributes;
using MinSon.Handlers.Dialogue;
using MinSon.Handlers.Dialogue.Steps;
using MinSon.Services;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class HerroWorld : BaseCommandModule
    {
        #region speak
        [Command("saykaffekungen")]
        [Description("JAJA KAFFEKUNGEN")]
        [RequireCategories(ChannelCheckMode.Any, "Text Channels")]
        public async Task JAJAKAFFEKUNGEN(CommandContext ctx)
        {
            if(ctx.Guild.Owner != ctx.User )
            {
                await ctx.Channel.SendMessageAsync("You are not Geralf").ConfigureAwait(false);
            }
            else
            {
                var msg = await new DiscordMessageBuilder()
                 .WithContent($"Yaa Yaa Kaeffei keaungen")
                 .HasTTS(true)
                 .SendAsync(ctx.Channel);
            }
            //Inline reply?
            //        var msg = await new DiscordMessageBuilder()
            //.WithContent($"I'm talking to *you*!")
            //.WithReply(ctx.Message.Id, true)
            //.SendAsync(ctx.Channel);

            ///Txt->speach
         

            //  await ctx.Channel.SendMessageAsync("JA JA KAFFEKUNGEN!").ConfigureAwait(false);
        }

        [Command("saybrocoli")]
        [Description("Ask bot what it thinks of Brocoli")]
        public async Task brocoli(CommandContext ctx)
        {
            var msg = await new DiscordMessageBuilder()
                .WithContent($"I hate brocoli")
                .HasTTS(true)
                .SendAsync(ctx.Channel);

        }
        [Command("say")]
        [Description("say string")]
        public async Task say(CommandContext ctx, [RemainingText] string text)
        {
            var msg = await new DiscordMessageBuilder()
                .WithContent(text)
                .HasTTS(true)
                .SendAsync(ctx.Channel);
        }

        [Command("sayfu")]
        [Description("fuck you +person+")]
        public async Task fuckYou(CommandContext ctx, [RemainingText] string name)
        {
            var msg = await new DiscordMessageBuilder()
                .WithContent($"Fuck You " + name)
                .HasTTS(true)
                .SendAsync(ctx.Channel);
        }

        [Command("saybutts")]
        [Description("B00BI3Z & BuTTZ")]
        public async Task butts(CommandContext ctx)
        {
            var msg = await new DiscordMessageBuilder()
                .WithContent($"I like big butts and I can not lie")
                .HasTTS(true)
                .SendAsync(ctx.Channel);

            // await ctx.Channel.SendMessageAsync("JA JA KAFFEKUNGEN!").ConfigureAwait(false);
        }
        #endregion
        [Command("dialogue")]
        public async Task dialogue(CommandContext ctx)
        {

            var inputStep = new TextStep("Do something!", null, minLength: 10);
            var secretStep = new IntStep("Walk into the waterfall", null, maxValue: 100);
            string input = string.Empty;
            int value = 0;

            inputStep.OnValidResult += (result) => {
                input = result;

                if(result == "secret waterfall")
                {
                    inputStep.SetNextStep(secretStep);
                }
            };

            secretStep.OnValidResult += (result) => value = result;

            var userChannel = await ctx.Member.CreateDmChannelAsync().ConfigureAwait(false);
            var inputDialogHandler = new DialogueHandler
            (
                ctx.Client,
                userChannel,
                ctx.User,
                inputStep
                );

            bool succeeded = await inputDialogHandler.ProcessStep().ConfigureAwait(false);

            if (!succeeded)
                return;

            await ctx.Channel.SendMessageAsync(input).ConfigureAwait(false);


            await ctx.Channel.SendMessageAsync(value.ToString()).ConfigureAwait(false);
        }
    }
}
// Mention
//var msg = await new DiscordMessageBuilder()
//  .WithContent($"✔ UserMention(user): Hey, {ctx.User.Mention}! Listen!")
//  .WithAllowedMentions(new IMention[] { new UserMention(ctx.User) })
//  .SendAsync(ctx.Channel);