using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed;

    Vector3 initialPosition;
    Camera mainCam;
    BoxCollider bounds;
    float boundsHalfWidth;

    public Rigidbody ball;
    float input;
    bool hasBall = true;
    Vector3 direction;

    private void Awake()
    {
        initialPosition = transform.position;
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bounds = GetComponent<BoxCollider>();
        boundsHalfWidth = bounds.size.x / 2;
    }

    private void Start()
    {
        //determine direction the ball will be shot in
        direction = new Vector3(Random.Range(-45, 45), 100);
        direction.Normalize();
    }

    void Update()
    {
        //move paddle
        input = Input.GetAxis("Horizontal");
        transform.Translate(input * speed * Time.deltaTime, 0, 0);

        //shoot ball
        if (hasBall == true && Input.GetButtonDown("Fire1"))
        {
            hasBall = false;
            ball.transform.parent = null;
            ball.velocity = direction * ball.GetComponent<Ball>().Speed;
        }

        //keep paddle within screen boundary
        if (transform.position.x - boundsHalfWidth < mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x)
        {
            transform.position = new Vector3(
                mainCam.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - boundsHalfWidth,
                initialPosition.y, initialPosition.z);
        }

        if (transform.position.x + boundsHalfWidth > mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            transform.position = new Vector3(
                mainCam.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + boundsHalfWidth,
                initialPosition.y, initialPosition.z);
        }
    }
}