using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class IAPModal : MonoBehaviour, IStoreListener
{
    public static IAPModal Instance;

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    public static string PRODUCT_500_COINS = "coins500";
    public static string PRODUCT_1000_COINS = "coins1000";
    public static string PRODUCT_2000_COINS = "coins2000";
    public static string PRODUCT_5000_COINS = "coins5000";
    public static string PRODUCT_12000_COINS = "coins12000";

    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        if (m_StoreController == null)
        {
            InitializePurchasing();
        }
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


        builder.AddProduct(PRODUCT_500_COINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_1000_COINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_2000_COINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_5000_COINS, ProductType.Consumable);
        builder.AddProduct(PRODUCT_12000_COINS, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        // Only say we are initialized if both the Purchasing references are set.
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    public void BuyCoins(string productID)
    {
        BuyProductID(productID);
    }


    private void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }


    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;

        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }


    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_500_COINS, StringComparison.Ordinal))
        {
            CoinController.IncreaseDecreaseCoins(500);
            Popup.Instance.Show("Done buying 500 coins");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_1000_COINS, StringComparison.Ordinal))
        {
            CoinController.IncreaseDecreaseCoins(1000);
            Popup.Instance.Show("Done buying 1000 coins");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_2000_COINS, StringComparison.Ordinal))
        {
            CoinController.IncreaseDecreaseCoins(2000);
            Popup.Instance.Show("Done buying 2000 coins");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_5000_COINS, StringComparison.Ordinal))
        {
            CoinController.IncreaseDecreaseCoins(5000);
            Popup.Instance.Show("Done buying 5000 coins");
        }

        else if (String.Equals(args.purchasedProduct.definition.id, PRODUCT_12000_COINS, StringComparison.Ordinal))
        {
            CoinController.IncreaseDecreaseCoins(12000);
            Popup.Instance.Show("Done buying 12000 coins");
        }

        else
        {
            Popup.Instance.Show("Failed buying");
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Popup.Instance.Show("Failed buying");
    }
}
