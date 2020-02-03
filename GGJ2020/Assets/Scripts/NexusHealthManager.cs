using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NexusHealthManager : HealthManager
{
    protected override void OnKilled()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartScreen");
    }
}
