using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int score;
    TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
         scoreText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
