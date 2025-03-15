using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatUI : MonoBehaviour
{
    public InputField chatInput;
    public Text chatHistory;
    public Button sendButton;

    private List<string> messages = new List<string>();

    void Start()
    {
        sendButton.onClick.AddListener(SendMessage);
    }

    void SendMessage()
    {
        string newMessage = chatInput.text;
        if (!string.IsNullOrEmpty(newMessage))
        {
            messages.Add(newMessage);
            UpdateChatHistory();
            chatInput.text = "";
        }
    }

    void UpdateChatHistory()
    {
        chatHistory.text = string.Join("\n", messages);
    }
}
