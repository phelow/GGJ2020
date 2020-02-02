using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField]
    private LineRenderer LineRenderer;
    internal void SetTerminus(Vector3 position)
    {
        StartCoroutine(LaserLine(position));
    }

    private IEnumerator LaserLine(Vector3 position)
    {
        LineRenderer.enabled = true;
        LineRenderer.SetPosition(0, this.transform.position);
        LineRenderer.SetPosition(1, position);
        yield return new WaitForSeconds(1.0f);
        LineRenderer.enabled = false;
    }
}
