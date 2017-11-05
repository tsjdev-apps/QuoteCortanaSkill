using QuoteCortanaSkill.Extensions;
using System.Collections.Generic;

namespace QuoteCortanaSkill.Services
{
    public static class MessagesService
    {
        private static readonly List<string> LaunchMessages = new List<string>
        {
            "Welcome to the Random Quote! Please ask for quote with: Tell me a quote.",
            "Greetings. For the a randomly picked quote ask for it.",
            "Howdy. I know many quotes. Do you want to get one? Simply ask for it."
        };

        private static readonly List<string> HelpMessages = new List<string>
        {
            "I will tell you a quote by a famous person if you want. Please ask for example: Tell me a quote.",
            "Please ask for example: Tell me a quote.",
            "Do you want a quote? Ask for it.",
        };

        private static readonly List<string> ErrorMessages = new List<string>
        {
            "Oh no, something went wrong.",
            "Something went wrong.",
            "Mayday, I need help, because something went wrong.",
            "That did not work, please try again later"
        };

        private static readonly List<string> QuoteBaseMessages = new List<string>
        {
            "Here is a random quote for you.",
            "Maybe you will like this one.",
            "Here we go.",
            "I hope you like it."
        };

        public static string GetLaunchMessage()
        {
            return LaunchMessages.PickRandom();
        }

        public static string GetHelpMessage()
        {
            return HelpMessages.PickRandom();
        }

        public static string GetErrorMessage()
        {
            return ErrorMessages.PickRandom();
        }

        public static string GetQuoteBaseMessage()
        {
            return QuoteBaseMessages.PickRandom();
        }
    }
}