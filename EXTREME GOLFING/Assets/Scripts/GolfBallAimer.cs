using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBallAimer : MonoBehaviour
{

    GameObject golfPointer;
    bool isGolfBallMoving;
    bool isGamePaused;
    private Vector3 rightRotations, leftRotations;
    Rigidbody2D golfBody;
    public float ballPower;
    AudioSource ballFX;

    // Start is called before the first frame update
    void Start()
    {
        isGamePaused = false;
        ballFX = GameObject.Find("FX").GetComponent<AudioSource>();
        golfPointer = GameObject.Find("Aimer");
        rightRotations = new Vector3(0, 0, 100);
        leftRotations = new Vector3(0, 0, -100);
        golfBody = FindObjectOfType<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGamePaused) {
            if (!isGolfBallMoving) {
                if (Input.GetAxisRaw("Horizontal") == 1) {
                    transform.Rotate(leftRotations * Time.deltaTime);
                }
                if (Input.GetAxisRaw("Horizontal") == -1) {
                    transform.Rotate(rightRotations * Time.deltaTime);
                }
                if (Input.GetKeyUp(KeyCode.Space)) {
                    golfBallIsMoving();
                    StartCoroutine(startLaunch());

                }
            } 
        }
    }

    private IEnumerator startLaunch() {
        bool isMoving = true;
        Vector3 golfBallPosOld, golfBallPosNew;

        ballFX.Play();

        float currentDegree = gameObject.transform.rotation.eulerAngles.z;
        currentDegree = (360 - currentDegree) * (Mathf.PI / 180);
        golfBody.velocity = new Vector2(Mathf.Sin(currentDegree) * ballPower, Mathf.Cos(currentDegree) * ballPower);

        while (isMoving) {
            golfBallPosOld = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            yield return new WaitForSeconds(0.3f);
            golfBallPosNew = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            if (golfBallPosNew == golfBallPosOld) {
                isMoving = false;
            }
        }
        golfBallIsNotMoving();
    }

    private void golfBallIsNotMoving() {
        isGolfBallMoving = false;
        golfPointer.GetComponent<Animator>().SetBool("isActive", false);
    }

    private void golfBallIsMoving() {
        isGolfBallMoving = true;
        golfPointer.GetComponent<Animator>().SetBool("isActive", true);
    }

    public void changePausedStatus() {
        if (isGamePaused)
        {
            isGamePaused = false;
        }
        else {
            isGamePaused = true;
        }
    }

}
