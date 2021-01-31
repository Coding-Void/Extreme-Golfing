using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private AudioSource playingSounds;
    public static bool doesExist;
    private AudioClip theMusic;

    float theVolume;

    // Start is called before the first frame update
    void Start()
    {
        if (!doesExist)
        {
            doesExist = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            Destroy(gameObject);
        }
        playingSounds = gameObject.GetComponent<AudioSource>();
        theVolume = playingSounds.volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeTheMusic(AudioClip newMusic) {
        StartCoroutine(fadeAudio(newMusic, true));
    }

    IEnumerator fadeAudio(AudioClip theNewMusic, bool decreasing) {
        if (decreasing)
        {
            for (float i = theVolume; i >= 0; i -= 0.2f)
            {
                playingSounds.volume = i;
                yield return new WaitForSeconds(0.3f);
            }
            playingSounds.clip = theNewMusic;
            playingSounds.Play();
            decreasing = false;
            StartCoroutine(fadeAudio(theNewMusic, decreasing));
        }
        else
        {
            while (playingSounds.volume < theVolume)
            {
                playingSounds.volume += 0.2f;

                yield return new WaitForSeconds(0.3f);
            }
        }
    }
}
