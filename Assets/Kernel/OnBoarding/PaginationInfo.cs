using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PaginationInfo : UIScreen
{
    [SerializeField] private Image[] dots;

    public override void StartScreen()
    {

    }

    public void SetDotActive(int index)
    {
        foreach (var item in dots)
        {
            item.DOFade(0.5f, 0.5f);
        }

        dots[index].DOFade(1, 0.5f);
    }
}
