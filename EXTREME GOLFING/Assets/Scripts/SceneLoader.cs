using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public int currentScene;
    GameObject pauseMenu;
    GolfBallAimer theGolfBall;
    Animator transitionAnimation;
    Animator pauseMenuTransition;

    public AudioClip changeMusic;
    public AudioClip titleMusic;
    CameraController theCamera;

    private void Start()
    {
        if (currentScene > 0 && currentScene != 7) {
            pauseMenu = GameObject.Find("PauseMenu");
            pauseMenuTransition = pauseMenu.GetComponent<Animator>();
            pauseMenuTransition.SetInteger("pauseMenuCurrentState", 0);
            theGolfBall = FindObjectOfType<GolfBallAimer>();
        }
        transitionAnimation = gameObject.GetComponent<Animator>();
        theCamera = FindObjectOfType<CameraController>();
    }

    public void resumeGame() {
        theGolfBall.changePausedStatus();
        pauseMenuTransition.SetInteger("pauseMenuCurrentState", 2);
    }

    public void pauseGame() {
        theGolfBall.changePausedStatus();
        pauseMenuTransition.SetInteger("pauseMenuCurrentState", 1);
    }

    public void quitApplication() {
        Application.Quit();
    }

    public void showTheTransition() {
        StartCoroutine(sceneTransition());
        if (changeMusic != null) {
            theCamera.changeTheMusic(changeMusic);
        }
    }

    public void showTitleTransition() { 
        StartCoroutine(titleTransition());
        theCamera.changeTheMusic(titleMusic);
    }

    public void reloadLevel() {
        StartCoroutine(restartTransition());
    }

    IEnumerator sceneTransition() {
        transitionAnimation.SetBool("StartTransition", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentScene + 1);
    }

    IEnumerator titleTransition() {
        transitionAnimation.SetBool("StartTransition", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Title");
    }

    IEnumerator restartTransition() { 
        transitionAnimation.SetBool("StartTransition", true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentScene);
    }

}
