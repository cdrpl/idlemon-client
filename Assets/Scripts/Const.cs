/// <summary>
/// Constant values.
/// </summary>
public static class Const
{
    /// <summary>
    /// The IP address of the Idlemon server.
    /// </summary>
    public const string SERVER = "localhost:3000";

    /// <summary>
    /// URL for making HTTP requests to the Idlemon server.
    /// </summary>
    public const string HTTP_URL = "http://" + SERVER;

    /// <summary>
    /// URL for the Idlemon WebSocket server.
    /// </summary>
    public const string WS_URL = "ws://" + SERVER + "/ws";

    // Units
    public const int MAX_STARS = 10;
    public const int MAX_UNIT_LEVEL = 290;
    public const int UNIT_TEAM_SIZE = 6;

    /* Campaign Resources */

    /// <summary>
    /// Number of seconds until campaign resources are full.
    /// </summary>
    public const int CAMPAIGN_MAX_COLLECT = 60 * 60 * 24;

    public const int CAMPAIGN_EXP_PER_SEC = 5;

    public const int CAMPAIGN_GOLD_PER_SEC = 20;

    public const int CAMPAIGN_EXP_STONE_PER_SEC = 2;

    public const int CAMPAIGN_EXP_GROWTH = 2;

    public const int CAMPAIGN_GOLD_GROWTH = 1;

    public const int CAMPAIGN_EXP_STONE_GROWTH = 3;

    /// <summary>
    /// The size of the read buffer for the WebSocket client.
    /// </summary>
    public const int WEB_SOCKET_READ_BUFFER = 1024;

    public enum Resource
    {
        Gold,
        Gems,
        ExpStone,
        EvoStone
    }

    public enum WebSocketMessageType
    {
        ChatMessage
    }
}
