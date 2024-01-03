using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class lvlButton : MonoBehaviour
{
    private Button mainButton;

    [SerializeField] private Image image;

    public void SetupButton(Lvl lvl)
    {
        mainButton = GetComponent<Button>();
        image.sprite = lvl.sprite;
    }

    public void SetupOnClick(Action onClick)
    {
        mainButton.onClick.RemoveAllListeners();
        mainButton.onClick.AddListener(() => onClick?.Invoke());
        mainButton.interactable = false;
    }

    public void EnableCard() => mainButton.interactable = true;
}