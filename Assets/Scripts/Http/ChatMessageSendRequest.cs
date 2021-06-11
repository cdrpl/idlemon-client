using Newtonsoft.Json;
using System.Threading.Tasks;

public class ChatMessageSendRequest
{
    const string URL = "/chat/message/send";

    public string message;

    public ChatMessageSendRequest(string message)
    {
        this.message = message;
    }

    public static async Task<ChatMessageSendResponse> Send(string message)
    {
        ChatMessageSendRequest request = new ChatMessageSendRequest(message);
        string data = JsonConvert.SerializeObject(request);
        return await Http.PostRequest<ChatMessageSendResponse>(URL, data);
    }
}
