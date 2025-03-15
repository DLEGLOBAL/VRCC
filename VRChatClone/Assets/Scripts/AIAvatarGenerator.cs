using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class AIAvatarGenerator : MonoBehaviour
{
    public string apiURL = "https://your-ai-avatar-api.com/generate";

    public void GenerateAvatar(string imagePath)
    {
        StartCoroutine(UploadImage(imagePath));
    }

    IEnumerator UploadImage(string filePath)
    {
        byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", imageBytes, "avatar.jpg");

        using (UnityWebRequest www = UnityWebRequest.Post(apiURL, form))
        {
            yield return www.SendWebRequest();
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Avatar Generated!");
            }
        }
    }
}
