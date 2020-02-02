using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    [SerializeField]
    private Collector collector;

    [SerializeField]
    private Player player;

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        if (collector.TrySendClick())
        {
            return;
        }

        if (player.TrySendClick())
        {
            return;
        }
    }
}
