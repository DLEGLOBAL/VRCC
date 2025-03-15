using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    public int playerCoins = 1000;

    public void BuyItem(int price)
    {
        if (playerCoins >= price)
        {
            playerCoins -= price;
            Debug.Log("Item Purchased!");
        }
        else
        {
            Debug.Log("Not enough coins!");
        }
    }
}
