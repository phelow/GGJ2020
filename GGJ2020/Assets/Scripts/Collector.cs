using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Collector : MonoBehaviour
{
    [Tooltip("How close the mouse has to be to the collectable for it to be clickable")]
    public float mouseDistance = 1.0f;

    [Tooltip("How close this collector has to be to the collectable for it to be clickable")]
    public float collectionDistance = 1.0f;

    [Tooltip("Needed to calculate the mouses cursors world position")]
    public float distanceOfCameraFromGround = 10f;

    [Tooltip("Input.GetButtonUp to use for clicking")]
    public string ClickButton = "Fire1";

    internal bool TrySendRightClick()
    {
        if (lastHovering != null && lastHovering.CanRightClick())
        {
            lastHovering.RightClick();
            Hammer.instance.SetTerminus(lastHovering.transform.position);
            return true;
        }

        return false;
    }

    [Header("Debug")]
    public Collectable lastHovering;
    public Collectable[] allItems;

    void Start()
    {
        // ensures Physics2D does not always return this Collector
        // Physics2D.queriesStartInColliders = false;
        // Code above enables code below to be functional
        // /// <summary>
        // /// Returns true if there is no obstable between the center of this object and the supplied item
        // /// </summary>
        // private bool isInLineOfSight(Collider2D item)
        // {
        //     var dir = item.transform.position - transform.position;
        //     // Debug.DrawRay(transform.position, dir);
        //     RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1000);
        // 
        //     if (hit.collider != null)
        //     {
        //         // Debug.Log(item.name + " triggered hit on " + hit.collider.name);
        //         // make sure the object hit matches the supplied item
        //         return item.gameObject == hit.collider.gameObject;
        //     }
        // 
        //     return false;
        // }

        allItems = FindObjectsOfType<Collectable>();
        foreach (var item in allItems)
        {
            item.SetClickability(Collectable.ClickabilityEnum.OutOfRange);
        }
    }

    internal bool TrySendClick()
    {
        if (lastHovering != null && lastHovering.CanClick())
        {
            lastHovering.ClickIt();
            return true;
        }

        return false;
    }
    void Update()
    {
        updateItemClickability();
    }

    void updateItemClickability()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var everythingInCollectionRange = Physics2D.OverlapCircleAll(mousePosition, collectionDistance)
            .Where
            (
               i => i.GetComponent<Collectable>() != null
            )
            .Select(i => i.GetComponent<Collectable>()).ToArray();

        // de-activate items
        lastHovering?.SetClickability(Collectable.ClickabilityEnum.OutOfRange);

        // active new items
        Collectable closest = everythingInCollectionRange.FirstOrDefault();
        float shortestDistance = Mathf.Infinity;
        foreach (var collided in everythingInCollectionRange)
        {
            float distance = Vector2.Distance(collided.transform.position, mousePosition);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = collided;
            }
        }

        lastHovering = closest;
        lastHovering?.SetClickability(Collectable.ClickabilityEnum.Clickable);
    }

    [SerializeField]
    private LayerMask ignorePlayer;

    /// <summary>
    /// Returns true if there is no obstable between the center of this object and the supplied item
    /// </summary>
    private bool isInLineOfSight(Collider2D item)
    {
        var dir = item.transform.position - transform.position;
        Debug.DrawRay(transform.position, dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 1000, ignorePlayer);
        if (hit.collider != null)
        {
            // make sure the object hit matches the supplied item
            return item.gameObject == hit.collider.gameObject;
        }

        return false;
    }
}
