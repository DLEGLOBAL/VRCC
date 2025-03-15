using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class StoreUI : MonoBehaviour
{
    public Button buyAvatarButton;
    public Button buyVRCoinsButton;
    
    void Start()
    {
        buyAvatarButton.onClick.AddListener(() => BuyItem("Cool Avatar", 1000));
        buyVRCoinsButton.onClick.AddListener(() => BuyVRCoins(500));
    }

    void BuyItem(string itemName, int price)
    {
        StartCoroutine(ProcessPurchase(itemName, price));
    }

    void BuyVRCoins(int amount)
    {
        StartCoroutine(ProcessCoinPurchase(amount));
    }

    IEnumerator ProcessPurchase(string itemName, int price)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", "123456"); // Replace with actual user ID
        form.AddField("amount", price);
        form.AddField("itemName", itemName);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/spend-coins", form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Purchase Response: " + www.downloadHandler.text);
        }
    }

    IEnumerator ProcessCoinPurchase(int amount)
    {
        WWWForm form = new WWWForm();
        form.AddField("userId", "123456");
        form.AddField("amount", amount);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:5000/buy-coins", form))
        {
            yield return www.SendWebRequest();
            Debug.Log("Coin Purchase Response: " + www.downloadHandler.text);
        }
    }
}
