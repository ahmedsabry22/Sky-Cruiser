using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public static Shop Instance;
    public GameObject[] ShopItems;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        for (int i = 0; i < ShopItems.Length; i++)
        {
            if (!ShopItems[i].GetComponent<ShopItem>().AircraftData.Locked)
            {
                ShopItems[i].transform.Find("BuyBtn").transform.Find("Text").GetComponent<Text>().text = "Owned";
                ShopItems[i].transform.Find("BuyBtn").GetComponent<Button>().interactable = false;
                ShopItems[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                
            }
        }
    }

    public void BuyItem(int itemIndex)
    {
        if (CoinController.Quantity > ShopItems[itemIndex].GetComponent<ShopItem>().AircraftData.Price)
        {
            ShopItems[itemIndex].transform.Find("BuyBtn").transform.Find("Text").GetComponent<Text>().text = "Owned";
            ShopItems[itemIndex].transform.Find("BuyBtn").GetComponent<Button>().interactable = false;
            ShopItems[itemIndex].GetComponent<Button>().interactable = false;

            CoinController.IncreaseDecreaseCoins(-ShopItems[itemIndex].GetComponent<ShopItem>().AircraftData.Price);

            MissionController.Instance.aircraftPrefabs[itemIndex].GetComponent<Aircraft>().aircraftData.Locked = false;
        }
        else
        {
            Popup.Instance.Show("You don't have enough coins to buy this item!");
        }
    }
}