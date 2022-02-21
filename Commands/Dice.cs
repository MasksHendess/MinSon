using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class Dice : BaseCommandModule
    {
        [Command("rolldice")]
        [Description("Returns a random number between 1 and input")]
        public async Task rolldice(CommandContext ctx, [Description("Highest Random number")] int max)
        {
                Random r = new Random();
                int rInt = r.Next(1, max);
                await ctx.Channel.SendMessageAsync(rInt.ToString()).ConfigureAwait(false);
        }

        [Command("rolldiceadv")]
        [Description("Returns 2 random numbers between 1 and input")]
        public async Task rolldiceadvantage(CommandContext ctx, [Description("Highest Random number")] int max)
        {
            for (int i = 0; i < 2; i++)
            {
                Random r = new Random();
                int rInt = r.Next(1, max);
                await ctx.Channel.SendMessageAsync(rInt.ToString()).ConfigureAwait(false);
            }

        }

        [Command("math")]
        [Description("math two integers together")]
        public async Task math(CommandContext ctx, [Description("First Number")] int one,
            [Description("Operator (+ - * /)")] string opperator,
            [Description("Second Number")] int two)
        {
            if (opperator == "+")
                await ctx.Channel.SendMessageAsync((one + two).ToString()).ConfigureAwait(false);
            else if (opperator == "-")
                await ctx.Channel.SendMessageAsync((one - two).ToString()).ConfigureAwait(false);
            else if (opperator == "*")
                await ctx.Channel.SendMessageAsync((one * two).ToString()).ConfigureAwait(false);
            else if (opperator == "/")
                await ctx.Channel.SendMessageAsync((one / two).ToString()).ConfigureAwait(false);
            else
                await ctx.Channel.SendMessageAsync("Invalid opperator").ConfigureAwait(false);
        }

    }
}