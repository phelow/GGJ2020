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
        , { ClickabilityEnum.OutOfRange, Color.red }
        , { ClickabilityEnum.Clickable, Color.yellow }
        , { ClickabilityEnum.Hovering, Color.green }
        , { ClickabilityEnum.Clicked, Color.white }
    };

    public ClickabilityEnum Clickability;

    [Tooltip("How long a click is visiable")]
    public float secondsToShowClicked = .5f;
    private float secondsOfClickLeft = 0f;

    public UnityEvent OnClicked;
    public Outline outline;

    void Awake()
    {
        outline = this.GetComponent<Outline>();
    }

    //void Update() { }

    /// <summary> Sets the state of the clickable to clicked </summary>
    public void ClickIt()
    {
        SetClickability(ClickabilityEnum.Clicked);
        secondsOfClickLeft = secondsToShowClicked;
        Debug.Log(this.name + " was clicked " + secondsOfClickLeft);
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
