using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindNearestResource : MonoBehaviour
{
    [SerializeField]
    LineRenderer resourceLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        resourceLineRenderer.SetPosition(0, this.transform.position);
        resourceLineRenderer.SetPosition(1, this.transform.position);
        Collider2D [] collisions = Physics2D.OverlapCircleAll(this.transform.position, 50.0f);

        Resource nearestResource = null;
        foreach (Collider2D collider in collisions)
        {
            Resource resource = collider.GetComponent<Resource>();

            if (resource == null)
            {
                continue;
            }

            if (nearestResource == null)
            {
                nearestResource = resource;
                continue;
            }

            if (Vector2.Distance(nearestResource.transform.position, this.transform.position) > Vector2.Distance(resource.transform.position, this.transform.position))
            {
                nearestResource = resource;
            }
        }

        if (nearestResource == null)
        {
            return;
        }

        resourceLineRenderer.SetPosition(1, this.transform.position);
        resourceLineRenderer.SetPosition(0, nearestResource.transform.position);
    }
}
