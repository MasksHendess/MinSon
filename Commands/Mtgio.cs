using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MtgApiManager.Lib.Core;
using MtgApiManager.Lib.Model;
using MtgApiManager.Lib.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Commands
{
    public class Mtgio : BaseCommandModule
    {
        #region commandmtg
        [Command("mtg")]
        [Description("Get a normal magic card from mtgio (Special cases not handled by this command: split card, aftermath cards)" +
            "ex: ?mtg Badlands")]
        public async Task getCardbyCardName(CommandContext ctx, [RemainingText] string name)
        {
            //
            if (name.Contains("/")) // Cards that contain / are split cards or aftermath cards
            {
               await ctx.Channel.SendMessageAsync("Invalid Card Name, use another fetch command if you are trying to fetch a aftermath / Split  card").ConfigureAwait(false);
            }
            else
            {
                // mtgio setup
                IMtgServiceProvider serviceProvider = new MtgServiceProvider();
                ICardService service = serviceProvider.GetCardService();

                //get data from api
                var result = await service.Where(x => x.Name, name).AllAsync();

                // Handle data from api
                if (result.IsSuccess && result.Value.Count > 0)
                {
                    //Handle Transform cards / dualfaced cards
                    if (result.Value.FirstOrDefault().Layout == "transform")
                    {
                        var cardFrontside = result.Value.Where(x => x.ImageUrl != null).FirstOrDefault(); 
                        
                        //Get the cards backside by checking that a item in result has a cmc not matching the cmc of the frontside (backsides typically dont have a cmc)
                        var cardBackside = result.Value.Where(x => x.ImageUrl != null && x.ManaCost != cardFrontside.ManaCost).FirstOrDefault();


                        await ctx.Channel.SendMessageAsync(cardFrontside.ImageUrl.ToString() + "\t" + cardBackside.ImageUrl.ToString()).ConfigureAwait(false);
                    }
                    else
                    {
                        // Handle normal one sided cards
                        var value = result.Value.FirstOrDefault();
                        await ctx.Channel.SendMessageAsync(value.ImageUrl.ToString()).ConfigureAwait(false);
                    }
                }
                else
                {
                    // User typed command wrong
                    var exception = result.Exception;
                    await ctx.Channel.SendMessageAsync("Failed to find").ConfigureAwait(false);
                }
            }
        }
        #endregion
        #region SplitCards
        [Command("mtgas")]
        [Description("Fetch special case card from mtgio such as (a)ftermath and (s)plit card " +
            "Ex: ?mtgas a Claim/Fame")]
        public async Task getAftermathCard(CommandContext ctx, 
            [Description("determines the cards layout, input a for aftermath and s for splitcard.  ex: ??fetch a Claim/Fame")]string type,
            [RemainingText] string name)
        {

            string layout = "";
            switch (type)
            {
                case "a":
                    layout = "aftermath";
                    break;
                case "s":
                    layout = "split";
                    break;
                default:
                    layout = " ";
                    break;
            }
            // mtgio setup
            // Does this mean i cant get TO? Mabye just use one kappa.
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();

            IOperationResult<List<ICard>> result;

            if (name.Contains("/") && layout!=null) // Cards that contain / are split cards or aftermath cards
            {
                var splitcard = name.Split(' ', '/'); // api need claim || faim ! both


                // Call api with layout parameter 
                result = await service.Where(x => x.Name, splitcard[0]).Where(x => x.Layout, layout).AllAsync(); // Find First Side of the card
                var card = result.Value.Where(x => x.ImageUrl != null).FirstOrDefault(); // just looking for that image 

                if (result.IsSuccess && result.Value.Count > 0)
                    await ctx.Channel.SendMessageAsync(card.ImageUrl.ToString()).ConfigureAwait(false);

            }
            else
            {
                // No special cases? Find card normally
                await getCardbyCardName(ctx , name);
            }
        }
        #endregion
        #region Booster
        [Command("mtgbooster")]
        [Description("")]
        public async Task getbooster(CommandContext ctx, [RemainingText] string set)    
        {
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ISetService service = serviceProvider.GetSetService();
            var result = await service.GenerateBoosterAsync(set);

            if (result.IsSuccess && result.Value.Count > 0)
            {
                int basicLand = 0;
                string cards = "";
                var embedBooster = new DiscordEmbedBuilder
                {
                    Title =  result.Value.FirstOrDefault().SetName + " Booster",
                    
                };

                foreach (var card in result.Value)
                {

                    var seqie = result.Value.FirstOrDefault().Set;
                    if (card.Type.Contains("Basic Land"))
                        basicLand++;

                    if (basicLand > 1 && card.Type.Contains("Basic Land"))
                    {
                        ICardService cardservice = serviceProvider.GetCardService();
                        var lmao = await cardservice.Where(x => x.Set, set).Where(x => x.Rarity, "Common").AllAsync();
                        card.ImageUrl = lmao.Value.FirstOrDefault().ImageUrl;
                    }

                    var embed = new DiscordEmbedBuilder
                    {
                        Title = card.Name,
                        Description = card.ImageUrl.ToString(),
                        ImageUrl = card.ImageUrl.AbsoluteUri,

                    };
                    embedBooster.Description += "\n" + embed.Description;
                    cards +=  card.ImageUrl + " ";
                    // await ctx.Channel.SendMessageAsync(card.ImageUrl.ToString()).ConfigureAwait(false); // JAJA spamaa
                }
                await ctx.Channel.SendMessageAsync(embed: embedBooster).ConfigureAwait(false);
                await ctx.Channel.SendMessageAsync(basicLand.ToString()).ConfigureAwait(false);
            }


        }
        #endregion
    }

}