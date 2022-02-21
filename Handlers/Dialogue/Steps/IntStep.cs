using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinSon.Handlers.Dialogue.Steps
{
   public class IntStep : DialogueStepBase
    {
        private IDialogueStep _nextStep;
        private readonly int? _minValue;
        private readonly int? _maxValue;

        public IntStep(string content, IDialogueStep nextStep, int? minValue = null, int? maxValue = null): base(content)
        {
            _nextStep = nextStep;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public Action<int> OnValidResult { get; set; } = delegate { };

        public override IDialogueStep NextStep => _nextStep;

        public void SetNextStep(IDialogueStep nextStep)
        {
            _nextStep = nextStep;
        }

        public override async Task<bool> ProcessStep(DiscordClient client, DiscordChannel channel, DiscordUser user)
        {
            var embedBuilder = new DiscordEmbedBuilder
            {
                Title = "Plz respond below",
                Description = $"{user.Mention}, {_content}",
            };

            embedBuilder.AddField("To stop the Dialogue", " use the ?cancel command");

            if(_minValue.HasValue)
            {
                embedBuilder.AddField("Max Value:", $"{_minValue.Value} ");
            }

            if(_maxValue.HasValue)
            {
                embedBuilder.AddField("Max Value:", $"{_maxValue.Value} ");
            }

            var interactivity = client.GetInteractivity();

            while(true)
            {
                var embed = await channel.SendMessageAsync(embed: embedBuilder).ConfigureAwait(false);

                OnMessageAdded(embed);

                var messageResult = await interactivity.WaitForMessageAsync(
                    x => x.ChannelId == channel.Id && x.Author.Id == user.Id).ConfigureAwait(false);

                OnMessageAdded(messageResult.Result);

                if (messageResult.Result.Content.Equals("?cancel", StringComparison.OrdinalIgnoreCase))
                    return true;

                if(!int.TryParse(messageResult.Result.Content, out int inputValue))
                {
                    await TryAgain(channel, $"Your input is {_maxValue.Value - messageResult.Result.Content.Length} not an integer");
                    continue;
                }

                if (_minValue.HasValue && messageResult.Result.Content.Length < _minValue.Value)
                {
                    await TryAgain(channel, $"Your input is {_minValue.Value - messageResult.Result.Content.Length} characters to short");
                    continue;
                }
                if (_maxValue.HasValue && messageResult.Result.Content.Length > _maxValue.Value)
                {
                    await TryAgain(channel, $"Your input is {_maxValue.Value - messageResult.Result.Content.Length} not an integer");
                    continue;
                }

                OnValidResult(inputValue);

                return false;

            }
        }
    }
}
