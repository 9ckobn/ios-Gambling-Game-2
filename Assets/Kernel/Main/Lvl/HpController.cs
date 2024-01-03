using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private float minHp = 0.1f;
    private float maxHp = 0.9f;

    private float currentHp;

    public bool isAlive = true;

    private void OnEnable()
    {
        currentHp = minHp;

        StartCoroutine(HpDecreer());
    }

    public void IncreaseHp(float value)
    {
        currentHp += value;
    }

    IEnumerator HpDecreer()
    {
        isAlive = true;

        while (true)
        {
            if (isAlive && currentHp >= minHp)
            {
                currentHp -= 0.001f;
                slider.value = currentHp;

                if (currentHp >= maxHp)
                {
                    isAlive = false;
                    yield break;
                }
            }
            yield return null;
        }
    }
}
