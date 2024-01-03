using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;
    private int currentPage = 1;

    public float scrollOffset = 0;

    private Action<PointerEventData> onScroll;
    private Action<PointerEventData> onStartScroll;

    public PaginationInfo paginationInfo;
    public PayWall payWall;

    void Start()
    {
        panelLocation = transform.position;
        onStartScroll += MovePosition;
        onScroll += CalculateScroll;

        paginationInfo.needToAnimate = false;
        paginationInfo.SetDotActive(0);
    }

    private void MovePosition(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnDrag(PointerEventData data)
    {
        onStartScroll?.Invoke(data);
    }
    public void OnEndDrag(PointerEventData data)
    {
        onScroll?.Invoke(data);
    }

    private void CalculateScroll(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width - scrollOffset, 0, 0);

                if (currentPage == totalPages)
                {
                    EndScroll();
                }
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width + scrollOffset, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

        paginationInfo.SetDotActive(currentPage - 1);
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }

    private void EndScroll()
    {
        onScroll -= CalculateScroll;
        onStartScroll -= MovePosition;

        paginationInfo.CloseScreenWithAnimation();

        payWall.StartScreen();
    }
}