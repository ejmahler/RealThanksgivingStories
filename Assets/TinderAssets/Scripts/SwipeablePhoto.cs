using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeablePhoto : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum SwipeState { Waiting, Swiping, Dropped }

    public SwipeState CurrentState { get; private set; }

    public RectTransform rectTransform { get; private set; }

    // Use this for initialization
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ResetSwipe() {
        CurrentState = SwipeState.Waiting;
        rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        CurrentState = SwipeState.Swiping;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position - eventData.pressPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        CurrentState = SwipeState.Dropped;
    }
}
