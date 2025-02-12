

using ChatRoomWithBot.Domain.Events;
using MassTransit;
using System.Globalization;
using System.Net.Http.Headers; 
using CsvHelper;
using CsvHelper.Configuration;

namespace ChatRoomWithBot.Service.WorkerService
{
    internal class ChatMessageCommandEventConsumer : IConsumer<ChatMessageCommandEvent>
    {
       
       


        public async Task Consume(ConsumeContext<ChatMessageCommandEvent> context)
        {
            try
            {
                var stockCode = context.Message.Message.Replace("/stock=", "").ToLowerInvariant();

                var httpclient = new HttpClient();

                var url = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";

                using var msg = new HttpRequestMessage(HttpMethod.Get, new Uri(url));
                msg.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/csv"));
                using var resp = await httpclient.SendAsync(msg);
                resp.EnsureSuccessStatusCode();


                var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
                {
                    HasHeaderRecord = true,
                    Comment = '#',
                    AllowComments = true,
                    Delimiter = ",",
                };

                await using var s = await resp.Content.ReadAsStreamAsync();
                using var sr = new StreamReader(s);
                using var csvReader = new CsvReader(sr, csvConfig);
                var record = csvReader.GetRecords<Stock>().First();
                var result = string.Empty;
                if (record != null)
                {
                      result = $"{stockCode.ToUpper()} quote is ${record.Open} per share";
                    Console.WriteLine(result);
                }


                var chatResponseCommandEvent = new ChatResponseCommandEvent()
                {
                    CodeRoom = context.Message.CodeRoom,
                    Message = result,
                    UserId = Guid.Empty,
                    UserName = "bot"

                }; 

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           


        }
    }
}
