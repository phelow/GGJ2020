using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary> Can be collected by a collector </summary>
[RequireComponent(typeof(Outline))]
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

    /// <summary> Map click states to colors </summary>
    public readonly Dictionary<ClickabilityEnum, Color> StatusColors = new Dictionary<ClickabilityEnum, Color>()
    {
        { ClickabilityEnum.Error, Color.magenta }
        , { ClickabilityEnum.OutOfRange, Color.blue } // item is out of range to be collected
        , { ClickabilityEnum.Clickable, Color.yellow } // items clickable
        , { ClickabilityEnum.Hovering, Color.green } // item that is currently hovered over
        , { ClickabilityEnum.Clicked, Color.white } // item is clicked / activated
    };

    public ClickabilityEnum Clickability;

    [Tooltip("How long a click is visiable")]
    public float secondsToShowClicked = .2f;
    private float secondsOfClickLeft = 0f;

    public UnityEvent OnClicked;
    public Outline outline;

    void Awake()
    {
        outline = this.GetComponent<Outline>();
    }

    public virtual bool CanClick()
    {
        return true;
    }

    /// <summary> Sets the state of the clickable to clicked </summary>
    public void ClickIt()
    {
        SetClickability(ClickabilityEnum.Clicked);
        secondsOfClickLeft = secondsToShowClicked;

        if (OnClicked != null)
        {
            OnClicked.Invoke();
        }
    }

    public void SetClickability(ClickabilityEnum newClickability)
    {
        if (Clickability == ClickabilityEnum.Clicked && secondsOfClickLeft > 0)
        {
            secondsOfClickLeft -= Time.deltaTime;
        }
        else
        {
            Clickability = newClickability;
            outline.OutlineColor = StatusColors[newClickability];
        }
    }
}
