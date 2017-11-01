using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using QuoteCortanaSkill.Services;
using System;

namespace QuoteCortanaSkill.Dialogs
{
    [Serializable]
    public class QuoteDialog : IDialog<object>
    {
        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            if (await result is Activity activity)
            {
                await HandleQuoteIntentAsync(context);
            }
        }

        private async Task HandleQuoteIntentAsync(IDialogContext context)
        {
            var randomQuote = await QuoteService.GetQuoteAsync();

            // create hero card
            var heroCard = new HeroCard
            {
                Title = "Random Quote",
                Subtitle = $"by {randomQuote.QuoteAuthor}",
                Text = $"\"{randomQuote.QuoteText}\""
            };

            var message = context.MakeMessage();
            message.Attachments.Add(heroCard.ToAttachment());
            message.Speak = $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">Here is a random quote for you.<break />{randomQuote.QuoteText} by {randomQuote.QuoteAuthor}.</speak>";

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
    }
}