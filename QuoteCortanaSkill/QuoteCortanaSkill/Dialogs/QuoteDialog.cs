using Microsoft.Bot.Builder.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using QuoteCortanaSkill.Services;
using System;
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

        public Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
            return Task.CompletedTask;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                if (await result is Activity activity)
                {
                    var client = new LuisClient(WebConfigurationManager.AppSettings["LuisAppId"], WebConfigurationManager.AppSettings["LuisSubscriptionKey"]);

                    if (string.IsNullOrEmpty(activity.Text))
                    {
                        await SayAnswerAsync(context, MessagesService.GetLaunchMessage());
                        return;
                    }

                    var prediction = await client.Predict(activity.Text);
                    var predictedIntent = prediction.TopScoringIntent.Name;

                    switch (predictedIntent)
                    {
                        case HelloIntent:
                            await SayAnswerAsync(context, MessagesService.GetLaunchMessage());
                            break;
                        case HelpIntent:
                            await SayAnswerAsync(context, MessagesService.GetHelpMessage());
                            break;
                        case QuoteIntent:
                            await HandleQuoteIntentAsync(context);
                            break;
                        default:
                            await SayAnswerAsync(context, MessagesService.GetHelpMessage());
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                await context.PostAsync($"{MessagesService.GetErrorMessage()} {Environment.NewLine} {ex.Message}");
                context.Wait(MessageReceivedAsync);
            }
        }

        private async Task SayAnswerAsync(IDialogContext context, string speak, string text = null)
        {
            await context.SayAsync(text ?? speak, speak);
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
                Text = $"\"{randomQuote.QuoteText.Trim()}\""
            };

            var message = context.MakeMessage();
            message.Attachments.Add(heroCard.ToAttachment());
            message.Speak = $"<speak version=\"1.0\" xmlns=\"http://www.w3.org/2001/10/synthesis\" xml:lang=\"en-US\">{MessagesService.GetQuoteBaseMessage()} <break /> {randomQuote.QuoteText} by {randomQuote.QuoteAuthor}.</speak>";

            await context.PostAsync(message);

            context.Wait(MessageReceivedAsync);
        }
    }
}