using UnityEngine;

public class MainScreen : MonoBehaviour
{
    public DailyScreen dailyScreen;

    public MenuScreen menuScreen;

    public static MainScreen instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        if (PlayerStats.FirstLoginToday)
        {
            dailyScreen.main = this;
            dailyScreen.StartScreen();
        }
        else
            SetupMenu();

    }

    public void SetupMenu()
    {
        menuScreen.StartScreen();
    }
}
