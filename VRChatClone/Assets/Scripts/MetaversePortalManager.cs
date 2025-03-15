using UnityEngine;
using UnityEngine.SceneManagement;

public class MetaversePortalManager : MonoBehaviour
{
    public string destinationScene;

    public void Teleport()
    {
        SceneManager.LoadScene(destinationScene);
    }
}
