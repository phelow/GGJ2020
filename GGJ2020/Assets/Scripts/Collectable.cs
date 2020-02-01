using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// White item is clicked
/// Green item that is currently hovered over
/// Yellow items clickable
/// Red item is not clickable
/// </summary>
public class Collectable : MonoBehaviour
{
    public enum ClickabilityEnum
    {
        Error = 0,
        OutOfRange,
        Clickable,
        Hovering,
        Clicked,
    }

    public ClickabilityEnum Clickability;

    public UnityEvent OnClicked;

    //void Start() { }
    //void Update() { }

    public void ClickItGood()
    {
        Debug.Log(this.name + " was clicked");
        Clickability = ClickabilityEnum.Clicked;
        if (OnClicked != null)
        {
            OnClicked.Invoke();
        }
    }
}
