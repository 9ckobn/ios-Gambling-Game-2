using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScreen : UIScreen
{
    [SerializeField] private Button start, pay, settings;

    [SerializeField] private LevelSelector levelSelectorScreen;
    [SerializeField] private SettingsHandler settingsScreen;
    [SerializeField] private PayWall payWall;

    public override void StartScreen()
    {
        SetupButtons();
        gameObject.SetActive(true);
    }

    private void SetupButtons()
    {
        start.onClick.AddListener(async () =>
        {
            await CloseScreenWithAnimation();
            levelSelectorScreen.SetupContent(this);
        });

        settings.onClick.AddListener(async () =>
        {
            await CloseScreenWithAnimation();
            settingsScreen.StartScreen();
        });

        if (PlayerStats.isPurchased)
            pay.interactable = false;

        pay.onClick.AddListener(OpenPayWall);
    }

    public void OpenPayWall()
    {
        payWall.StartScreen();
    }
}
