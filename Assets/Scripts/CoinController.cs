using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CoinController : MonoBehaviour
{
    public Text coinsText;

    public static int Quantity
    {
        get
        {
            return (PlayerPrefs.GetInt(Constants.COINS_KEY, 2500));
        }
    }

    private void Start()
    {
        StartCoroutine(DisplayCoins());
    }

    /// <summary>
    /// used to increase the coins in the PlayerPrefs
    /// </summary>
    /// <param name="amount">pass positive value to increase or negative value to decrease</param>
    public static void IncreaseDecreaseCoins(int amount)
    {
        int coins = PlayerPrefs.GetInt(Constants.COINS_KEY, 2500);

        coins += amount;

        PlayerPrefs.SetInt(Constants.COINS_KEY, coins);

        if (amount > 0)
            Popup.Instance.Show("Coins increased +" + amount);

        else if (amount < 0)
            Popup.Instance.Show("Coins decreased " + amount);
    }

    private IEnumerator DisplayCoins()
    {
        while (true)
        {
            int coins = PlayerPrefs.GetInt(Constants.COINS_KEY, 2500);
            coinsText.text = coins.ToString();
            yield return new WaitForSeconds(5);
        }
    }
}