using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    private Rigidbody rb;
    public float ballSpeed;
    public GameObject camera;
    private Vector3 offset;
    int levelNo;
    new Vector3 ballStartPosition;
    public GameObject[] collectables;
    public GameObject gameOverPanel;
    public GameObject controlChoice;
    public GameObject arrows;
    public Text scoreText;
    public int noOfLevels;
    int score;
    public bool isFlat = true;
    bool isArrows = false, isAccel = false;
    void Awake()
    {
        score = 0;
        scoreText.text = "Score 0";
        levelNo = 1;
        ballStartPosition = this.transform.position;
        offset = camera.transform.position - transform.position;
        rb = GetComponent<Rigidbody>();
        gameOverPanel.SetActive(false);
        controlChoice.SetActive(true);
        arrows.SetActive(false);
    }

    private void LateUpdate()
    {
        camera.transform.position = transform.position + offset;
    }

    private void Update()
    {
        Vector3 tilt = Input.acceleration;

        if (isFlat)
            tilt = Quaternion.Euler(135, 0, 0) * tilt;
        if (isAccel)
        {
            rb.AddForce(tilt * ballSpeed);
        }
    }
    void FixedUpdate()
    {
        float moveOnX = Input.GetAxis("Horizontal");
        float moveOnZ = Input.GetAxis("Vertical");
        Vector3 ballMovement = new Vector3(moveOnX, 0f, moveOnZ);
        rb.AddForce(ballMovement * ballSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            score += 10;
            scoreText.text = "Score " + score;
        }
        else if (other.gameObject.CompareTag("Last"))
        {
            other.gameObject.SetActive(false);
            score += 10;
            scoreText.text = "Score " + score;
            if (levelNo != noOfLevels)
            {
                Destroy(GameObject.FindGameObjectWithTag("Level " + levelNo));
                levelNo++;
                Instantiate(Resources.Load("Level " + levelNo));
                respwanBall();
            }
            else
            {
                Destroy(gameObject);
                gameOverPanel.SetActive(true);
            }

        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            Invoke("respwanBall", 2f);
        }
    }

    private void respwanBall()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        this.transform.position = ballStartPosition;
    }

    public void Arrows()
    {
        isArrows = true;
        isAccel = false;
        controlChoice.SetActive(false);
        arrows.SetActive(true);
    }

    public void Accel()
    {
        isAccel = true;
        isArrows = false;
        controlChoice.SetActive(false);
    }
}
