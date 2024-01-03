using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LvlScreen : UIScreen
{
    [SerializeField] private int SecondsAmount = 70;

    [SerializeField] private TextMeshProUGUI headerTimer, lvlTimer;

    [SerializeField] private NotificationScreen notificationScreen;

    public Lvl lvlSettings;
    public MainButton button;

    public Button pause;

    public GameObject MainLvl;

    public override void StartScreen()
    {
        pause.onClick.AddListener(PauseGame);

        notificationScreen.SetupButtons(this);

        button.lvlScreen = this;

        button.SetupImage(lvlSettings.sprite);

        gameObject.SetActive(true);

        StartCoroutine(LvlTimer());
    }

    public override void CloseScreen()
    {
        headerTimer.text = "";

        MainLvl.SetActive(false);

        StopAllCoroutines();

        base.CloseScreen();
    }

    private IEnumerator LvlTimer()
    {
        YieldInstruction waitForSec = new WaitForSeconds(1);

        for (int i = 3; i > 0; i--)
        {
            lvlTimer.text = i.ToString();
            yield return waitForSec;
        }

        MainLvl.SetActive(true);
        StartCoroutine(LifeTimer());

        lvlTimer.text = "";

        yield break;
    }

    private IEnumerator LifeTimer()
    {
        YieldInstruction waitForSec = new WaitForSeconds(1);

        for (int i = SecondsAmount; i > 0; i--)
        {
            headerTimer.text = $"{i} sec";
            yield return waitForSec;
        }

        LoseGame();

        headerTimer.text = "";

        yield break;
    }

    public void LoseGame()
    {
        notificationScreen.ShowLose();
    }

    public void WinGame()
    {
        notificationScreen.ShowWin();
    }

    public void PauseGame()
    {
        notificationScreen.ShowPause();
    }
}