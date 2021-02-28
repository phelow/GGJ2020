using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StalinController : MonoBehaviour
{

    // Start is called before the first frame update
   public float destroyTime = 14;
    public TextMeshProUGUI text;
    public float textSpeed = 0.03f;
    public AudioSource stalinAudio;
    public Animation stalinAnimation;

    public CanvasGroup stalinCanvas;
    void Start()
    {
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        var originalString = text.text;
        yield return RevealTextSegment("Hello SPACE-COMRADE! IT IS ME! YOUR BEST FRIEND! THE MAN OF STEEL! COMRADE STALIN!");

        yield return new WaitForSeconds(3.0f);

        yield return RevealTextSegment("I HAVE SPACE-MISSION FOR YOU! IN SPACE!");
        yield return new WaitForSeconds(3.0f);

        yield return RevealTextSegment("Superior SOVIET satellites are being damaged by the puny pathetic bratchny bruiseboys and need to be repaired with the green goodies. Satellites cost a pretty polly and unfortunately you are oddy knocky(on your own).This nochy will determine the future of our GREAT SOVIET UNION! Good luck Comrade cosmonaut, we will be watching on the worldcast.");

        yield return new WaitForSeconds(15.0f);
        stalinCanvas.alpha = 0;

        while (GameObject.FindGameObjectsWithTag("Collectable").Length == 0)
        {
            yield return new WaitForSeconds(1.0f);
        }


        stalinCanvas.alpha = 1;
        yield return RevealTextSegment("SPACE-COMRADE, grab those green goodies by clicking on them.");
        yield return new WaitForSeconds(5.0f);
        while (GameObject.FindGameObjectsWithTag("Collectable").Length != 0)
        {
            yield return new WaitForSeconds(1.0f);
        }
        stalinCanvas.alpha = 0;

        yield return new WaitForSeconds(5.0f);

        stalinCanvas.alpha = 1;
        while (!ResourceTracker.instance.HasCharge())
        {
            yield return new WaitForSeconds(1.0f);
        }
        yield return RevealTextSegment("SPACE-COMRADE, you must shove those green goodies into those space satellites. Ram those goodies up into the satellite holes to repair the superior soviet space satellite.");
        yield return new WaitForSeconds(5.0f);
        while (ResourceTracker.instance.HasCharge())
        {
            yield return new WaitForSeconds(1.0f);
        }
        yield return RevealTextSegment("LIKE BOLSHEVIK ON BICYCLE! GREAT JOB SPACE COMRADE! You must repair all the satellites. Just like Soviet Supreme Laser-Chess Champion Comrade Mikelov Michelvan Cherkov-Storkovski-Shpiel you must be SOVIET SUPREME STRATEGICIAN. Make Mother Russia and Daddy Stalin proud!");
        yield return new WaitForSeconds(15.0f);
    }

    private IEnumerator RevealTextSegment(string originalString)
    {
        stalinAnimation.Play();
        stalinAudio.Play();
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
            stalinAudio.Stop();
        }
    }
}
