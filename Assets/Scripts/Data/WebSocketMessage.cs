public class WebSocketMessage
{
    public Const.WebSocketMessageType type;

    /// <summary>
    /// Data can be deserialized into a more specific object based on the message type.
    /// </summary>
    public byte[] data;
}
