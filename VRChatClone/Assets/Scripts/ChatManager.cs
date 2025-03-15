using UnityEngine;
using Photon.Pun;
using System.Text.RegularExpressions;

public class ChatManager : MonoBehaviourPun
{
    public void SendMessage(string message)
    {
        if (ContainsBadWords(message))
        {
            Debug.Log("Message blocked: Inappropriate content.");
            return;
        }
        photonView.RPC("ReceiveMessage", RpcTarget.AllBuffered, message);
    }

    bool ContainsBadWords(string message)
    {
        string[] badWords = { "offensive1", "offensive2" };
        foreach (string word in badWords)
        {
            if (Regex.IsMatch(message, $"\\b{word}\\b", RegexOptions.IgnoreCase))
            {
                return true;
            }
        }
        return false;
    }
}
