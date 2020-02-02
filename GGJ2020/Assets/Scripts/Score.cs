using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Score : MonoBehaviour
{
    int score;
    TMPro.TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
         scoreText = gameObject.GetComponent<TMPro.TextMeshProUGUI>();
        StartCoroutine(IncrementScore());
    }

    private IEnumerator IncrementScore()
    {
        while (true)
        {
            score++;

            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            if (score > highScore)
            {
                PlayerPrefs.SetInt("HighScore", score);
            }

            scoreText.text = score.ToString();
            yield return new WaitForSeconds(1.0f);
        }
    }
}
