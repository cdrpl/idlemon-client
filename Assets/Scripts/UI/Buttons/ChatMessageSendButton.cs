using UnityEngine;
using UnityEngine.UI;

public class ChatMessageSendButton : MonoBehaviour
{
    public ChatManager chatManager;

    void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    async void OnClick()
    {
        await chatManager.SendChatMessage();
    }
}
