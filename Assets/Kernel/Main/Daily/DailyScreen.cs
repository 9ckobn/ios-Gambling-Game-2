using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DailyScreen : UIScreen
{
    public MainScreen main;

    [SerializeField] private Button mainButton;
    [SerializeField] private TextMeshProUGUI buttonText, mainText;

    private const string rewardButtonText = "Claim prize";

    //todo add more rewards, convert to list with IReward Objects
    private const string rewardText = "You won! +1 free shape";
  
    [SerializeField] private RectTransform content;

    public override void StartScreen()
    {
        gameObject.SetActive(true);

        Setup();
    }

    private void Setup()
    {
        mainButton.onClick.AddListener(StartScroll);
    }

    private void StartScroll()
    {
        float randomPosX = Random.Range(2, 7);

        mainButton.targetGraphic.DOFade(0, 0.5f);
        mainText.DOFade(0, 0.5f);
        buttonText.DOFade(0, 0.5f);

        content.DOAnchorPosX(12800 - 930 * randomPosX, 7).SetEase(Ease.OutBounce).OnComplete(SetupReward);
    }

    private void SetupReward()
    {
        Debug.Log("End scroll");

        buttonText.text = rewardButtonText;
        mainText.text = rewardText;
        mainButton.targetGraphic.DOFade(1, 0.5f);
        buttonText.DOFade(1, 0.5f);
        mainText.DOFade(1, 0.5f);

        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(async () => await GetRewardAsync());
    }

    private async UniTask GetRewardAsync()
    {
        await CloseScreenWithAnimation();
        main.SetupMenu();
    }
}
