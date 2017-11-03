using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using QuoteCortanaSkill.Services;
using System;
using System.Collections.Generic;
using QuoteCortanaSkill.Extensions;
using Microsoft.Cognitive.LUIS;
using System.Web.Configuration;

namespace QuoteCortanaSkill.Dialogs
{
    [Serializable]
    public class QuoteDialog : IDialog<object>
    {
        private const string HelloIntent = "HelloIntent";
        private const string HelpIntent = "HelpIntent";
        private const string QuoteIntent = "QuoteIntent";
        private const string None = "None";

        private readonly List<string> _launchMessages = new List<string>
        {
            "Welcome to the Random Quote ! Please ask for example: Tell me a quote.",
            "Greeting. For the a randomly picked quote ask for it."
        };

        private readonly List<string> _helpMessages = new List<string>
        {
            "I will tell you a quote by a famous person if you want. Please ask for example: Tell me a quote.",
            "Please ask for example: Tell me a quote.",
            "Do you want a quote? Ask for it.",
        };

        private readonly List<string> _errorMessages = new List<string>
        {
            "Oh no, something went wrong.",
            "Something went wrong.",
            "Mayday, I need help, because something went wrong.",
            "That did not work, please try again later"
        };



        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            if (await result is Activity activity)
            {
                var client = new LuisClient(WebConfigurationManager.AppSettings["LuisAppId"], WebConfigurationManager.AppSettings["LuisSubscriptionKey"]);

                var prediction = await client.Predict(activity.Text);
                var predictedIntent = prediction.TopScoringIntent.Name;

                switch (predictedIntent)
                {
                    case None:
                    case HelloIntent:
                        await GetRandomAnswerAsync(context, _launchMessages);
                        break;
                    case HelpIntent:
                        await GetRandomAnswerAsync(context, _helpMessages);
                        break;
                    case QuoteIntent:
                        await HandleQuoteIntentAsync(context);
                        break;
                    default:
                        await GetRandomAnswerAsync(context, _errorMessages);
                        break;
                }
            }
        }

        private async Task GetRandomAnswerAsync(IDialogContext context, List<string> messages)
        {
            var message = messages.PickRandom();
            await context.SayAsync(message, message);

            context.Wait(MessageReceivedAsync);
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