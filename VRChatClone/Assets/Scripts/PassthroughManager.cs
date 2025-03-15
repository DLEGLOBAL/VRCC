using UnityEngine;
using UnityEngine.Video;

public class PassthroughManager : MonoBehaviour
{
    public VideoPlayer passthroughVideo;

    public void StartPassthrough(string videoURL)
    {
        passthroughVideo.url = videoURL;
        passthroughVideo.Play();
    }
}
