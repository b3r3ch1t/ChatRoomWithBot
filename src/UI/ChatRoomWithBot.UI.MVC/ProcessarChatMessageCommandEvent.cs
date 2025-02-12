using System.Globalization;
using ChatRoomWithBot.Domain;
using ChatRoomWithBot.Domain.Bus;
using ChatRoomWithBot.Domain.Events;
using ChatRoomWithBot.Domain.Interfaces;
using CsvHelper;
using CsvHelper.Configuration;

namespace ChatRoomWithBot.UI.MVC;

public class ProcessarChatMessageCommandEvent : IProcessarChatMessageCommandEvent
{
    private readonly IHttpRequests _httpRequests;
    private readonly IChatManagerDomain _chatManagerDomain;
    public ProcessarChatMessageCommandEvent(IHttpRequests httpRequests, IChatManagerDomain chatManagerDomain)
    {
        _httpRequests = httpRequests;
        _chatManagerDomain = chatManagerDomain;
    }

    public async Task<CommandResponse> ProcessarChatMessageCommandEventAsync(ChatMessageCommandEvent? chatMessage)
    {
        try
        {
            if (chatMessage == null) return CommandResponse.Fail("Mensagem Inválida ");

            var stockCode = chatMessage.Message.Replace("/stock=", "").ToLowerInvariant();

            var url = $"https://stooq.com/q/l/?s={stockCode}&f=sd2t2ohlcv&h&e=csv";

            
            var r = await _httpRequests.GetAsync<string>(baseUrl: url, "");

            var m = ConverterParaMensagem(r.Data, stockCode, chatMessage.CodeRoom);

            var result = await _chatManagerDomain.SendMessageAsync(m);
            return result;
        }
        catch (Exception e)
        {
            return CommandResponse.Fail(e);
        }
    }

    private ChatResponseCommandEvent ConverterParaMensagem(string texto, string stockCode, Guid codeRoom)
    {
        try
        {

            var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = true,
                Comment = '#',
                AllowComments = true,
                Delimiter = ",",
            };



            using TextReader reader = new StringReader(texto);

            using var csvReader = new CsvReader(reader, csvConfig);


            var record = csvReader.GetRecords<Stock>().First();
            var result = string.Empty;
            if (record != null)
            {
                result = $"{stockCode.ToUpper()} quote is ${record.Open} per share";
                Console.WriteLine(result);
            }


            var chatResponseCommandEvent = new ChatResponseCommandEvent()
            {
                CodeRoom = codeRoom,
                Message = result,
                UserId = Guid.Empty,
                UserName = "bot"

            };

            return chatResponseCommandEvent;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}