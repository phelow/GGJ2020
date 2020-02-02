using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float textSpeed = 0.03f;
    void Start()
    {
        if (!text && gameObject.GetComponent<TextMeshProUGUI>() )
            text = gameObject.GetComponent<TextMeshProUGUI>();
        StartCoroutine(RevealText());

    }

    IEnumerator RevealText()
    {
        var originalString = text.text;
        text.text = "";

        int numCharsRevealed = 0;
        while (numCharsRevealed < originalString.Length)
        {
            while (originalString[numCharsRevealed] == ' ')
                ++numCharsRevealed;

            ++numCharsRevealed;

            text.text = originalString.Substring(0, numCharsRevealed);

            yield return new WaitForSeconds(textSpeed);
        }
        if (Time.time > 1 &&  numCharsRevealed >=originalString.Length)
        {
            Debug.Log("STALIN IS DONE TALKING");
        }
        
       
    }
}

