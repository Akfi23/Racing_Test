using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPSystem : GameSystemWithScreen<ShopScreen>, IStoreListener
{
    public static string Coins_100 = "100";
    public static string Coins_200 = "200";
    private static IStoreController storeController;
    private IExtensionProvider extensionProvider;

    public override void OnInit()
    {
        if (storeController == null)
            InitIAP();
    }

    private void InitIAP()
    {
        Debug.Log("Start");
        if (IsInitialized()) return;


        Debug.Log("Init");
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(Coins_100, ProductType.Consumable);
        builder.AddProduct(Coins_200, ProductType.Consumable);

        UnityPurchasing.Initialize(this,builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && extensionProvider != null;
    }

    private void PurchaseProduct(string productId)
    {
        if (!IsInitialized()) return;

        Product product = storeController.products.WithID(productId);

        if(product!=null && product.availableToPurchase)
            storeController.InitiatePurchase(product);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        extensionProvider = extensions;
        Debug.Log("IAP Inited");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
    }

    public void Buy100Coins()
    {
        PurchaseProduct(Coins_100);
    }

    public void Buy200Coins()
    {
        PurchaseProduct(Coins_200);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_100, StringComparison.Ordinal))
        {
            if (PlayerPrefs.HasKey("Coins_100"))
            {
                PlayerPrefs.SetInt("Coins_100", 0);
            }
                player.Money += 100;
                screen.UpdateMoneyInfo(player.Money);
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, Coins_200, StringComparison.Ordinal))
        {
            if (PlayerPrefs.HasKey("Coins_200"))
            {
                PlayerPrefs.SetInt("Coins_200", 0);
            }
                player.Money += 200;
                screen.UpdateMoneyInfo(player.Money);
        }

        return PurchaseProcessingResult.Complete;   
    }
}
