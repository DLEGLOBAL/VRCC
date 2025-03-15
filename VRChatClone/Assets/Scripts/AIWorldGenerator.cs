using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AIWorldGenerator : MonoBehaviour
{
    public string apiURL = "https://your-ai-world-api.com/generate";

    public void GenerateWorld(string description)
    {
        StartCoroutine(SendText(description));
    }

    IEnumerator SendText(string text)
    {
        WWWForm form = new WWWForm();
        form.AddField("description", text);

        using (UnityWebRequest www = UnityWebRequest.Post(apiURL, form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("World Generated!");
            }
        }
    }
}
