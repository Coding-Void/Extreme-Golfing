using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHole : MonoBehaviour
{

    SceneLoader theSceneLoader;
    AudioSource goalSound;
    
    // Start is called before the first frame update
    void Start()
    {
        theSceneLoader = FindObjectOfType<SceneLoader>();
        goalSound = GameObject.Find("GoalFX").GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "GolfBall")
        {
            goalSound.Play();
            Destroy(collision.gameObject);
            theSceneLoader.showTheTransition();
        }
    }

}
