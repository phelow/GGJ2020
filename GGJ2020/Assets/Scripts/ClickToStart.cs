using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToStart : MonoBehaviour
{
    private void Update()
    {
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("EnemySwarmIntelligenceScene");
        }
    }
}
