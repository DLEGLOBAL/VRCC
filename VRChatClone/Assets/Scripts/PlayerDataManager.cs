using Firebase.Database;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    private DatabaseReference dbRef;

    void Start()
    {
        dbRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SavePlayerData(string playerId, int coins)
    {
        dbRef.Child("players").Child(playerId).Child("coins").SetValueAsync(coins);
    }

    public void LoadPlayerData(string playerId)
    {
        dbRef.Child("players").Child(playerId).GetValueAsync().ContinueWith(task => {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                int coins = int.Parse(snapshot.Child("coins").Value.ToString());
                Debug.Log($"Player Coins: {coins}");
            }
        });
    }
}
