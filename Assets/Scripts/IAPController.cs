using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAPController : MonoBehaviour
{
    public void Buy(string productID)
    {
        IAPModal.Instance.BuyCoins(productID);
    }
}