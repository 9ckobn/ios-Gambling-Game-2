using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : UIScreen
{
    [SerializeField] private lvlSettings lvlSettings;

    [SerializeField] private RectTransform contentRect;

    [SerializeField] private Button back, unlockButton;

    public LvlScreen lvlScreen;

    private bool alreadyCreated = false;

    int maxCard = 5;

    public void SetupContent(MenuScreen menu)
    {
        if (PlayerStats.isPurchased)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            unlockButton.onClick.RemoveAllListeners();
            unlockButton.onClick.AddListener(() =>
            {
                CloseScreen();
                MainScreen.instance.menuScreen.OpenPayWall();
            });
        }

        back.onClick.AddListener(async () =>
        {
            await CloseScreenWithAnimation();
            menu.StartScreen();
        });

        if (!alreadyCreated)
        {
            var currentIndex = 0;

            foreach (var item in lvlSettings.lvls)
            {
                var currentCard = Instantiate(lvlSettings.levelSelectorPrefab, contentRect);

                currentCard.SetupButton(item);
                currentCard.SetupOnClick(async () =>
                {
                    lvlScreen.lvlSettings = item;
                    await CloseScreenWithAnimation();
                    lvlScreen.StartScreen();
                });

                if (currentIndex < maxCard || PlayerStats.isPurchased)
                {
                    currentIndex++;
                    currentCard.EnableCard();
                }
            }

            alreadyCreated = true;
        }
        else
        {
            Debug.Log("already created");
        }

        StartScreen();
    }

    public override void StartScreen()
    {
        gameObject.SetActive(true);
    }
}
