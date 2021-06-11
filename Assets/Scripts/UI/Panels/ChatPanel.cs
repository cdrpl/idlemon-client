using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChatPanel : Panel
{
    /// <summary>
    /// The input field for the chat message text.
    /// </summary>
    public InputField messageInput;
    public Transform scrollContent;

    /// <summary>
    /// Prefab scroll item for displaying chat messages.
    /// </summary>
    public GameObject chatMessageScrollItem;
    public ChatMessageSendButton chatMessageSendBtn;
    public WebSocketClient webSocketClient;

    /// <summary>
    /// Keep track of instantiated scroll items.
    /// </summary>
    LinkedList<GameObject> scrollItems = new LinkedList<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject current = EventSystem.current.currentSelectedGameObject;

            if (current == messageInput.gameObject)
            {
                chatMessageSendBtn.GetComponent<Button>().onClick.Invoke();

#if UNITY_EDITOR || !UNITY_ANDROID
                messageInput.ActivateInputField();
#endif
            }
        }
    }

    void OnEnable()
    {
#if UNITY_EDITOR || !UNITY_ANDROID
        StartCoroutine(R_ActivateInputField());
#endif
        scrollContent.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Clear all chat messages when disabling the chat panel.
    /// </summary>
    void OnDisable()
    {
        var cur = scrollItems.First;

        while (cur != null)
        {
            Destroy(cur.Value);
            cur = cur.Next;
        }

        scrollItems.Clear();
    }

    /// <summary>
    /// Attach the message to the chat panel view.
    /// </summary>
    public void AttachMessage(ChatMessage chatMessage)
    {
        GameObject scrollItemInstance = InstantiateScrollItem(chatMessageScrollItem);
        scrollItemInstance.GetComponent<ChatMessageScrollItem>().Draw(chatMessage); // update the scroll item view
        AddScrollItemToLinkedList(scrollItemInstance, chatMessage);
    }

    /// <summary>
    /// Instantiate a new scroll item instance and attach it to the scroll view.
    /// </summary>
    GameObject InstantiateScrollItem(GameObject scrollItem)
    {
        // instantiate scroll item
        GameObject scrollItemInstance = Instantiate<GameObject>(scrollItem);
        scrollItemInstance.transform.SetParent(scrollContent);
        scrollItemInstance.transform.localScale = Vector3.one;

        return scrollItemInstance;
    }

    /// <summary>
    /// Add scroll item to linked list and update the scroll item sibling index based on the order of the chat message IDs.
    /// </summary>
    void AddScrollItemToLinkedList(GameObject scrollItemInstance, ChatMessage chatMessage)
    {
        var curNode = scrollItems.First;

        // check if scroll item can be added to first or last
        if (curNode == null)
        {
            scrollItems.AddFirst(scrollItemInstance);
            return;
        }
        else
        {
            // if chat message has greater ID than last, add to the end of the list
            ChatMessage curMessage = scrollItems.Last.Value.GetComponent<ChatMessageScrollItem>().ChatMessage;

            if (chatMessage.id > curMessage.id)
            {
                scrollItems.AddLast(scrollItemInstance);
                return;
            }
        }

        // could not add to first or last, must loop through list to find specific index

        int index = 0; // index used to determine sibling index

        // chat messages must be inserted in descending order based on the chat message ID
        while (curNode != null)
        {
            ChatMessage curMessage = curNode.Value.GetComponent<ChatMessageScrollItem>().ChatMessage;

            if (chatMessage.id < curMessage.id)
            {
                scrollItemInstance.transform.SetSiblingIndex(index);
                scrollItems.AddBefore(curNode, scrollItemInstance);
                return;
            }

            curNode = curNode.Next;
            index++;
        }

        scrollItems.AddLast(scrollItemInstance);
    }

    /// <summary>
    /// Will select the input field at the end of the frame.
    /// </summary>
    IEnumerator R_ActivateInputField()
    {
        yield return new WaitForEndOfFrame();
        messageInput.ActivateInputField();
    }
}
