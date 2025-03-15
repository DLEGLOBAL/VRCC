using UnityEngine;
using Fusion;
using Fusion.Sockets;

public class MultiplayerManager : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;

    void Start()
    {
        _runner = gameObject.AddComponent<NetworkRunner>();

        StartGameMode gameMode = StartGameMode.AutoHostOrClient;

        _runner.StartGame(new StartGameArgs()
        {
            GameMode = gameMode,
            Scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex,
            PlayerCount = 50
        });
    }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} connected.");
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        Debug.Log($"Player {player} disconnected.");
    }
}
