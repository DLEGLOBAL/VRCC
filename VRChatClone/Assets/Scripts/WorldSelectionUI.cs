using UnityEngine;
using UnityEngine.UI;

public class WorldSelectionUI : MonoBehaviour
{
    public Button world1Button;
    public Button world2Button;
    public WorldManager worldManager;

    void Start()
    {
        world1Button.onClick.AddListener(() => worldManager.LoadWorld("World1"));
        world2Button.onClick.AddListener(() => worldManager.LoadWorld("World2"));
    }
}
