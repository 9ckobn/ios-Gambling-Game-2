using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainButton : MonoBehaviour, IPointerClickHandler
{
    public Button alternateButton;

    [SerializeField] private Image fill, main;
    [SerializeField] private HpController hp;

    public float clickValue = 0.03f;

    public LvlScreen lvlScreen;

    public void SetupImage(Sprite sprite)
    {
        alternateButton.onClick.RemoveAllListeners();
        alternateButton.onClick.AddListener(OnClickAction);

        fill.fillAmount = 0;

        fill.sprite = sprite;
        main.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickAction();
    }

    public void Update()
    {
        if (!hp.isAlive)
        {
            lvlScreen.LoseGame();
        }
    }

    private void OnClickAction()
    {
        hp.IncreaseHp(clickValue * 3f);

        fill.fillAmount += clickValue;

        if (fill.fillAmount >= 0.99f)
        {
            lvlScreen.WinGame();
        }
    }
}