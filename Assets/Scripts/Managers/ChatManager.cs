using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ChatManager : MonoBehaviour
{
    public ChatPanel chatPanel;
    public WebSocketClient webSocketClient;

    void Awake()
    {
        webSocketClient.OnMessageReceived.AddListener(OnMessageRecv);
    }

    public async Task SendChatMessage()
    {
        string message = chatPanel.messageInput.text;

        if (message != string.Empty)
        {
            chatPanel.messageInput.text = string.Empty;

            ChatMessageSendResponse response = await ChatMessageSendRequest.Send(message);

            if (response.HasError)
            {
                FlashMessage.Instance.Flash(response.Error);
            }
        }
    }

    public async void GetChatHistory()
    {
        ChatMessageHistoryResponse response = await new ChatMessageHistoryRequest().Send();

        if (response.HasError)
        {
            FlashMessage.Instance.Flash(response.Error);
        }
        else
        {
            foreach (var message in response.messages)
            {
                chatPanel.AttachMessage(message);
            }
        }
    }

    /// <summary>
    /// Handler for when a message is received from the WebSocket client.
    /// </summary>
    void OnMessageRecv(Const.WebSocketMessageType messageType, string data)
    {
        if (messageType == Const.WebSocketMessageType.ChatMessage)
        {
            ChatMessage msg = JsonConvert.DeserializeObject<ChatMessage>(data);
            chatPanel.AttachMessage(msg);
        }
    }
}
