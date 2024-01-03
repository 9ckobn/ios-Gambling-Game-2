using UnityEngine;
using UnityEngine.UI;

public class NotificationScreen : UIScreen
{
    [SerializeField] private Button[] toMenu, resume, restart;

    [SerializeField] private GameObject WinScreen, LoseScreen, PauseScreen;

    public override void StartScreen()
    {
        gameObject.SetActive(true);
    }

    public void SetupButtons(UIScreen screenToClose)
    {
        foreach (var item in resume)
        {
            item.onClick.RemoveAllListeners();
            item.onClick.AddListener(CloseScreen);
        }

        foreach (var item in toMenu)
        {
            item.onClick.RemoveAllListeners();

            item.onClick.AddListener(async () =>
            {
                screenToClose.CloseScreen();
                await CloseScreenWithAnimation();

                MainScreen.instance.SetupMenu();
            });
        }

        foreach (var item in restart)
        {
            item.onClick.RemoveAllListeners();

            item.onClick.AddListener(() =>
            {
                WinScreen.SetActive(false);
                PauseScreen.SetActive(false);
                LoseScreen.SetActive(false);

                CloseScreen();

                screenToClose.CloseScreen();
                screenToClose.StartScreen();
            });
        }
    }

    public void ShowLose()
    {
        WinScreen.SetActive(false);
        PauseScreen.SetActive(false);

        LoseScreen.SetActive(true);
        StartScreen();
    }

    public void ShowWin()
    {
        LoseScreen.SetActive(false);
        PauseScreen.SetActive(false);

        WinScreen.SetActive(true);
        StartScreen();
    }

    public void ShowPause()
    {
        WinScreen.SetActive(false);
        PauseScreen.SetActive(false);

        PauseScreen.SetActive(true);
        StartScreen();
    }


}
