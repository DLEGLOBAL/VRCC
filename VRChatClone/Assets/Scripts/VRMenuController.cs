using UnityEngine;

public class VRMenuController : MonoBehaviour
{
    public GameObject menuPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) || Input.GetButtonDown("Start")) // Gamepad Start Button
        {
            menuPanel.SetActive(!menuPanel.activeSelf);
        }
    }
}
