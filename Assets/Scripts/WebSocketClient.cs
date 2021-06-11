using Newtonsoft.Json;
using System;
using System.Text;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class WebSocketClient : MonoBehaviour
{
    /// <summary>
    /// Time in MS before client connect times out.
    /// </summary>
    const int CONNECT_TIMEOUT = 10000;

    /// <summary>
    /// Time in MS before client send times out.
    /// </summary>
    const int SEND_TIMEOUT = 10000;

    /// <summary>
    /// Time in msg between every reconnect attempt.
    /// </summary>
    const int RECON_DELAY = 5000;

    ClientWebSocket client;
    CancellationTokenSource cts;

    /// <summary>
    /// Will broadcast the received message type as well as the data string which should be a json deserializable object.
    /// Listeners should deserialize the string into a more specific object based on the message type.
    /// </summary>
    public UnityEvent<Const.WebSocketMessageType, string> OnMessageReceived;

    void Awake()
    {
        OnMessageReceived = new UnityEvent<Const.WebSocketMessageType, string>();
    }

    void Start()
    {
        cts = new CancellationTokenSource();

        if (Global.IsInit)
        {
            Init();
        }
        else
        {
            Global.OnUserInit.AddListener(Init);
        }
    }

    async void Init()
    {
        await Connect();
    }

    async Task Connect()
    {
        try
        {
            Uri uri = new Uri(Const.WS_URL);

            client = new ClientWebSocket();
            client.Options.SetRequestHeader("Authorization", Global.User.id + ":" + Global.Token);
            await client.ConnectAsync(uri, cts.Token);

            Debug.Log("WebSocket client connected");

            Read();
        }
        catch (Exception e)
        {
            if (e is OperationCanceledException)
            {

            }
            else
            {
                Debug.LogError(e, this);
                Reonnect();
            }
        }
    }

    public async Task SendWebSocketMessage(string message)
    {
        // if client isn't open, wait until the client is reconnected
        while (client.State != WebSocketState.Open)
        {
            await Task.Delay(1000);
        }

        try
        {
            Debug.Log("Send WebSocket Message: " + message);
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            var buffer = new ArraySegment<byte>(bytes);
            await client.SendAsync(buffer, WebSocketMessageType.Text, true, cts.Token);
        }
        catch (Exception e)
        {
            if (e is OperationCanceledException)
            {

            }
            else
            {
                Debug.LogError(e, this);
                Reonnect();
            }
        }
    }


    /// <summary>
    /// Will continually read received messages while the client is open.
    /// </summary>
    async void Read()
    {
        while (client.State == WebSocketState.Open)
        {
            try
            {
                var buffer = new byte[Const.WEB_SOCKET_READ_BUFFER];
                var segment = new ArraySegment<byte>(buffer);

                WebSocketReceiveResult result = await client.ReceiveAsync(segment, cts.Token);

                try
                {
                    HandleWebSocketMessage(result, buffer);
                }
                catch (Exception e)
                {
                    Debug.LogErrorFormat("fail to handle WebSocket message: {0}", e);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e, this);
                Reonnect();
            }
        }
    }

    async void OnDestroy()
    {
        cts.Cancel();

        if (client != null && client.State == WebSocketState.Open)
        {
            await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "App Quit", CancellationToken.None);
        }
    }

    async void Reonnect()
    {
        try
        {
            client.Abort();
            await Task.Delay(RECON_DELAY, cts.Token);
            await Connect();
        }
        catch (OperationCanceledException) // ignore operation canceled
        {
        }
    }

    void HandleWebSocketMessage(WebSocketReceiveResult result, byte[] buffer)
    {
        switch (result.MessageType)
        {
            case WebSocketMessageType.Text:
                // convert message to string
                string msg = Encoding.ASCII.GetString(buffer, 0, result.Count);

                // parse message string into message object
                WebSocketMessage wsMsg = JsonConvert.DeserializeObject<WebSocketMessage>(msg);

                // parse the message data as a string
                string data = Encoding.ASCII.GetString(wsMsg.data, 0, wsMsg.data.Length);

                OnMessageReceived.Invoke(wsMsg.type, data);

                Debug.LogFormat("Received WebSocket message type: {0}", wsMsg.type, this);
                break;

            case WebSocketMessageType.Binary:
                Debug.LogWarning("Received a WebSocket message of type Binary but no handler for binary types exists.", this);
                break;

            case WebSocketMessageType.Close:
                Debug.Log("WebSocket close received", this);
                Reonnect();
                return;

            default:
                Debug.LogWarningFormat("Received WebSocket message of unhandled type: {0}", result.MessageType, this);
                Reonnect();
                break;
        }
    }
}
