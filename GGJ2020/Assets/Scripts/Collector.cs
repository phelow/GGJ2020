using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Collector : MonoBehaviour
{
    [Tooltip("How close the mouse has to be to the collectable for it to be clickable")]
    public float mouseDistance = .5f;

    [Tooltip("How close this collector has to be to the collectable for it to be clickable")]
    public float collectionDistance = 2f;

    [Tooltip("Needed to calculate the mouses cursors world position")]
    public float distanceOfCameraFromGround = 10f;

    public Collectable lastHovering;
    public Collectable[] allItems;

    private void Awake()
    {
        allItems = FindObjectsOfType<Collectable>();
    }

    // void Start() { }

    void Update()
    {
        updateItemClickability();

        if (lastHovering != null && Input.GetButtonUp("Fire1"))
        {
            lastHovering.ClickItGood();
        }
    }

    void updateItemClickability()
    {
        float distanceToPlayer;
        List<Collectable> inCollectorRange = new List<Collectable>();

        foreach (var collectable in allItems)
        {
            distanceToPlayer = 
                Vector3.Distance(this.transform.position, collectable.transform.position);

            // highlight objects collecatable by player
            if (distanceToPlayer < collectionDistance
                // TODO: && check is in Line of Sight
                )
            {
                collectable.Clickability = Collectable.ClickabilityEnum.Clickable;
                inCollectorRange.Add(collectable);
            }
            else
            {
                collectable.Clickability = Collectable.ClickabilityEnum.OutOfRange;
            }
        }

        if (inCollectorRange == null || inCollectorRange.Count <= 0)
            return;

        // determine players mouse cursor position in world
        var mouseInWorld = Input.mousePosition;
        mouseInWorld.z = distanceOfCameraFromGround;
        mouseInWorld = Camera.main.ScreenToWorldPoint(mouseInWorld);

        // highlight closest object being hovered over by the mouse
        var closestToMouse = inCollectorRange
            .OrderBy(c => Vector3.Distance(c.transform.position, mouseInWorld))
            .First();

        //Debug.Log(closestToMouse.name + " dist = " + Vector3.Distance(closestToMouse.transform.position, mouseInWorld));
        if (Vector3.Distance(closestToMouse.transform.position, mouseInWorld) < mouseDistance)
        {
            lastHovering = closestToMouse;
            closestToMouse.Clickability = Collectable.ClickabilityEnum.Hovering;
        }
        else
        {
            lastHovering = null;
        }
    }
}
