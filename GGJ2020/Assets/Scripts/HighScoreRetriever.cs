using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreRetriever : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "High Score:" + PlayerPrefs.GetInt("HighScore", 0);
    }
}
