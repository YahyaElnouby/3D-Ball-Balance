using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonsControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public string buttonName;
    public float speed;
    bool isLeft = false, isRight = false, isUp = false, isDown = false;
    GameObject ball;
    private void Awake()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
    }
    public void Update()
    {
        if (isLeft)
        {
            Vector3 ballMovement = new Vector3(-1, 0f, 0f);
            ball.GetComponent<Rigidbody>().AddForce(ballMovement * speed);
        }
        else if (isRight)
        {
            Vector3 ballMovement = new Vector3(1, 0f, 0f);
            ball.GetComponent<Rigidbody>().AddForce(ballMovement * speed);
        }
        else if (isUp)
        {
            Vector3 ballMovement = new Vector3(0f, 0f, 1);
            ball.GetComponent<Rigidbody>().AddForce(ballMovement * speed);
        }
        else if (isDown)
        {
            Vector3 ballMovement = new Vector3(0f, 0f, -1);
            ball.GetComponent<Rigidbody>().AddForce(ballMovement * speed);
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(buttonName == "left")
            isLeft = true;
        if (buttonName == "right")
            isRight = true;
        if (buttonName == "up")
            isUp = true;
        if (buttonName == "down")
            isDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isLeft = false;
        isRight = false;
        isUp = false;
        isDown = false;
    }
}
