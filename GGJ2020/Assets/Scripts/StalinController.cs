using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StalinController : MonoBehaviour
{

    // Start is called before the first frame update
   public float destroyTime = 14;
    public TextMeshProUGUI text;
    public float textSpeed = 0.03f;
    public AudioSource stalinAudio;
    public Animation stalinAnimation;
    void Start()
    {
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
        if (Time.time > 1 && numCharsRevealed >= originalString.Length)
        {
            Debug.Log("STALIN IS DONE TALKING");
            stalinAnimation.Stop();
            if (stalinAudio && stalinAudio.isPlaying)
                stalinAudio.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <=0)
            gameObject.SetActive(false);
    }
}
