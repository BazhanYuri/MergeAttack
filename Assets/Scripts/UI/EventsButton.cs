using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;



[AddComponentMenu("UI/EventsButton")]
public class EventsButton : Button
{
    public event Action OnClickedStart;
    public event Action OnClickedEnded;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        OnClickedStart.Invoke();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        OnClickedEnded.Invoke();
    }
}
