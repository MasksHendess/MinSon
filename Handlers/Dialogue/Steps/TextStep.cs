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
   public class TextStep : DialogueStepBase
    {
        private IDialogueStep _nextStep;
        private readonly int? _minLength;
        private readonly int? _maxLength;

        public TextStep(string content, IDialogueStep nextStep, int? minLength = null, int? maxLength = null): base(content)
        {
            _nextStep = nextStep;
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public Action<string> OnValidResult { get; set; } = delegate { };

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

            if(_minLength.HasValue)
            {
                embedBuilder.AddField("Min Length:", $"{_minLength.Value} characters");
            }

            if(_maxLength.HasValue)
            {
                embedBuilder.AddField("Max Length:", $"{_maxLength.Value} characters");
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

                if (_minLength.HasValue && messageResult.Result.Content.Length < _minLength.Value)
                {
                    await TryAgain(channel, $"Your input is {_minLength.Value - messageResult.Result.Content.Length} characters to short");
                    continue;
                }
                if (_maxLength.HasValue && messageResult.Result.Content.Length > _maxLength.Value)
                {
                    await TryAgain(channel, $"Your input is {_maxLength.Value - messageResult.Result.Content.Length} characters to short long");
                    continue;
                }

                OnValidResult(messageResult.Result.Content);

                return false;

            }
        }
    }
}
