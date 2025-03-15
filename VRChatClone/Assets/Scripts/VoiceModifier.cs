using UnityEngine;

public class VoiceModifier : MonoBehaviour
{
    public AudioSource playerVoice;

    public void ApplyEffect(float pitch)
    {
        playerVoice.pitch = pitch;
    }
}
