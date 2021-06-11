using System.Threading.Tasks;

/// <summary>
/// Request for fetching chat logs.
/// </summary>
public class ChatMessageHistoryRequest : HttpRequest<ChatMessageHistoryResponse>
{
    const string PATH = "/chat/message/history";

    /// <summary>
    /// This should be the ID of the chat message which the history should start from.
    /// Set to 0 to receive the most recent logs.
    /// </summary>
    public int start;

    public ChatMessageHistoryRequest(int start = 0)
    {
        this.start = start;
    }

    /// <summary>
    /// Send the HTTP request.
    /// </summary>
    override public async Task<ChatMessageHistoryResponse> Send()
    {
        string url = Const.HTTP_URL + PATH + "?start=" + start.ToString();
        return await Http.GetRequest<ChatMessageHistoryResponse>(url);
    }
}
