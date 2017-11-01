using QuoteCortanaSkill.Models;
using QuoteCortanaSkill.Utils;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace QuoteCortanaSkill.Services
{
    public static class QuoteService
    {
        public static async Task<Quote> GetQuoteAsync()
        {
            try
            {
                var quote = await HttpService.GetRequestAsync<Quote>(Statics.QuoteUrl);
                return quote;
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"QuoteService | GetQuoteAsync | {ex}");
                return null;
            }
        }
    }
}