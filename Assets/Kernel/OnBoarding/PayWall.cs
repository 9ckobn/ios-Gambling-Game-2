using System;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PayWall : UIScreen, IDetailedStoreListener
{
    [SerializeField] private Button pay, close, terms, restore, privacy;

    public int MainMenuSceneIndex = 2;
    private WebViewObject webViewObject;

    public string PrivacyUrl, TermsUrl;

    private IStoreController storeController;
    private IExtensionProvider extensionProvider;

    private bool InAppReady = false;

    private const string productID = "com.fullversion.eiti";

    public override void StartScreen()
    {
        SetupButtons();

        gameObject.SetActive(true);
    }

    private void SetupButtons()
    {
        pay.onClick.RemoveAllListeners();
        restore.onClick.RemoveAllListeners();
        terms.onClick.RemoveAllListeners();
        privacy.onClick.RemoveAllListeners();
        close.onClick.RemoveAllListeners();

        close.onClick.AddListener(LoadMainMenu);
        pay.onClick.AddListener(UnlockApp);
        privacy.onClick.AddListener(() => OpenWebView(PrivacyUrl));
        terms.onClick.AddListener(() => OpenWebView(TermsUrl));
        restore.onClick.AddListener(Restore);
    }


    private void UnlockApp()
    {
        if (InAppReady == false)
        {
            UnityServices.InitializeAsync();

            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct(productID, ProductType.NonConsumable);

            UnityPurchasing.Initialize(this, builder);
        }

        storeController.InitiatePurchase(productID);
    }

    private void Restore()
    {
        //todo restore
    }

    private void OpenWebView(string Url)
    {
        webViewObject = (new GameObject("WebView")).AddComponent<WebViewObject>();
        webViewObject.Init(
            err: (msg) =>
            {
                Debug.Log($"Error: {msg}");
                Disable();
            },
            httpErr: (msg) =>
            {
                Debug.Log($"HttpError: {msg}");
                Disable();
            });

        webViewObject.LoadURL(Url);
        webViewObject.SetVisibility(true);
    }

    private void Disable()
    {
        Destroy(webViewObject);
    }

    void LoadMainMenu()
    {
        SceneManager.LoadSceneAsync(MainMenuSceneIndex);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log($"Product with id {productID} was not purchased");

        PlayerStats.isPurchased = false;

        LoadMainMenu();
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("InApp init fail");

        InAppReady = false;
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        InAppReady = false;
        Debug.LogError("Initialization failed: " + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        Debug.Log($"Product with id {productID} was successfully purchased");

        PlayerStats.isPurchased = true;

        LoadMainMenu();

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError("Purchase of product " + product.definition.id + " failed: " + failureReason);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("InApp Ready");

        storeController = controller;
        extensionProvider = extensions;

        InAppReady = true;
    }
}
