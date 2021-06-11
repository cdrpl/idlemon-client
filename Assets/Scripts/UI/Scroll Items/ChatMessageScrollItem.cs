using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the scroll item used for displaying chat messages.
/// </summary>
public class ChatMessageScrollItem : MonoBehaviour
{
    public ChatMessage ChatMessage => chatMessage;

    public Image background;
    public Text usernameLabel;
    public Text messageLabel;

    /// <summary>
    /// Scroll item will change to this color if the message was sent by the user.
    /// </summary>
    public Color sentColor = Color.green;

    ChatMessage chatMessage;

    public void Draw(ChatMessage chatMessage)
    {
        this.chatMessage = chatMessage;

        usernameLabel.text = chatMessage.senderName;
        messageLabel.text = chatMessage.message;

        if (chatMessage.userId == Global.User.id)
        {
            background.color = sentColor;
            usernameLabel.alignment = TextAnchor.MiddleRight;
            messageLabel.alignment = TextAnchor.MiddleRight;
        }
    }
}
