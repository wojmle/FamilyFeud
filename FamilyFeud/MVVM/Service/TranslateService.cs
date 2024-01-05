using System;
using System.Collections.Generic;
using OpenAI_API;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFeud.MVVM.Service
{
    public class TranslateService
    {
        public TranslateService()
        {
            
        }

        public async Task<List<string>> TranslateText(string input, string languageFrom)
        {
            var apiKey = "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";
            var apiModel = "text-davinci-003";

            List<string> rq = new List<string>();
            string rs = "";

            OpenAIAPI api = new OpenAIAPI(new APIAuthentication(apiKey));
            var completionRequest = new OpenAI_API.Completions.CompletionRequest()
            {
                Prompt = "Translate this into 1. French and 2. Armenian:\r\n" + input,
                Model = apiModel,
                Temperature = 0.3,
                MaxTokens = 100,
                TopP = 1.0,
                FrequencyPenalty = 0.0,
                PresencePenalty = 0.0,

            };

            var result = await api.Completions.CreateCompletionsAsync(completionRequest);
            foreach (var choice in result.Completions)
            {
                rs = choice.Text;
                rq.Add(choice.Text);
            }

            return rq;
        }


        //Console.OutputEncoding = Encoding.UTF8;
        //Console.WriteLine(rq.FirstOrDefault());
    }
}
